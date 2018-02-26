using System;
using ChaoprayaBoat.Library.Models;
using ChaoprayaBoat.Web.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ChaoprayaBoat.Web.Controllers
{
    // Api on URL
    // action = name method
    [Route("api/[action]")]
    public class ApiController : Controller
    {
        ApplicationDbContext db;
        public ApiController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpPost]
        public IActionResult DriverAuthen(String username, string password)
        {
            //เช็ค username password 
            var member = db.Members
                           .Where(x => x.Email == username && x.Password == password && x.MemberTypeId == 2)
                           .SingleOrDefault();

            if (member == null) return NotFound();


            var timetables = db.TimeTables
                               .Include(x => x.Member)
                               .Include(x => x.Route)
                               .Where(x => x.MemberId == member.Id && x.IsActive && x.DateTime.Date == DateTime.UtcNow.AddHours(7).Date)
                               .OrderBy(x => x.DateTime)
                               .ToList();

            if (timetables.Count() <= 0) return NoContent();

            return Json(timetables);


        }

        [HttpPost]
        public bool AddBoatLocation(double latitude, double longitude, int timetableid)
        {
            //เผื่อรู้timetable ใช้ routeไหน
            var timetable = db.TimeTables.Find(timetableid); //select id show

            if (timetable != null && timetable.IsActive)
            {

                var boatCoordinates = db.BoatHistories
                                           .Where(x => x.TimeTableId == timetableid)
                                           .OrderByDescending(x => x.Id) //max-min
                                           .Select(x => x.Coordinate)
                                           .ToList(); //onlyone

                List<Coordinate> coordinates;
                if (timetable.Destination == "ขาไป")
                {
                    coordinates = db.RouteCoordinates
                                        .Where(x => x.RouteId == timetable.RouteId)
                                        .Select(x => x.Coordinate)
                                        .OrderBy(x => x.Sequence)
                                        .ToList();
                }
                else
                {
                    coordinates = db.RouteCoordinates
                                    .Where(x => x.RouteId == timetable.RouteId)
                                    .Select(x => x.Coordinate)
                                    .OrderByDescending(x => x.Sequence)
                                    .ToList();
                }


                //start
                //insert first boathistories coordinate
                if (boatCoordinates.Count() <= 0)
                {
                    var nextCoordinate = coordinates[0];

                    var boatHistory = new BoatHistory();
                    boatHistory.ArriveDateTime = DateTime.Now;
                    boatHistory.CoordinateId = nextCoordinate.Id;
                    boatHistory.TimeTableId = timetableid;
                    db.Add(boatHistory);
                    db.SaveChanges();

                    return true;
                }
                else
                {
                    //step2
                    //current
                    var currentBoatCoordinate = boatCoordinates[0];

                    //nextcoor
                    List<Coordinate> nextCoordinates;
                    if (timetable.Destination == "ขาไป")
                    {
                        nextCoordinates = coordinates.Where(x => x.Sequence > currentBoatCoordinate.Sequence)
                                                     .OrderBy(x => x.Sequence)
                                                     .ToList();
                    }
                    else
                    {
                        nextCoordinates = coordinates.Where(x => x.Sequence < currentBoatCoordinate.Sequence)
                                                     .OrderByDescending(x => x.Sequence)
                                                     .ToList();
                    }

                    //lastCoordinate
                    if (nextCoordinates.Count() <= 0)
                    {
                        return false;
                    }
                    else
                    {
                        var nextCoordinate = nextCoordinates[0];

                        var currentCoOlat = latitude - currentBoatCoordinate.Latitude;
                        var currentCoOlong = longitude - currentBoatCoordinate.Longtitude;
                        var currentCoSqrt = Math.Sqrt(Math.Pow(currentCoOlat, 2) + Math.Pow(currentCoOlong, 2));
                        var currentCoDistance = (currentCoSqrt / (1.0f / 108.4f)) * 1000;

                        var nextCoOlat = latitude - nextCoordinate.Latitude;
                        var nextCoOlong = longitude - nextCoordinate.Longtitude;
                        var nextCoSqrt = Math.Sqrt(Math.Pow(nextCoOlat, 2) + Math.Pow(nextCoOlong, 2));
                        var nextCoDistance = (nextCoSqrt / (1.0f / 108.4f)) * 1000;

                        if (nextCoDistance < currentCoDistance)
                        {
                            var boatHistory = new BoatHistory();
                            boatHistory.ArriveDateTime = DateTime.Now;
                            boatHistory.CoordinateId = nextCoordinate.Id;
                            boatHistory.TimeTableId = timetableid;
                            db.Add(boatHistory);
                            db.SaveChanges();

                            int coordinateCount;
                            if (timetable.Destination == "ขาไป")
                            {
                                coordinateCount = coordinates.Where(x => x.Sequence > nextCoordinate.Sequence)
                                                             .Count();
                            }
                            else
                            {
                                coordinateCount = coordinates.Where(x => x.Sequence < nextCoordinate.Sequence)
                                                             .Count();
                            }
                            if (coordinateCount <= 0)
                            {
                                timetable.IsActive = false;
                                db.SaveChanges();
                                return false;
                            }
                            else return true;
                        }
                        return true;
                    }
                }
            }
            else return true;
        }

        [HttpPost]
        public List<Coordinate> GetPortNearest(double latitude, double longitude)
        {
            var coordinates = db.Coordinates
                                .Where(x => x.CoordinateTypeId == 1)
                                .ToList();

            var locations = coordinates.ToArray();
            var results = new List<Coordinate>();

            for (int i = 0; i < 3; i++)
            {
                for (int j = i + 1; j < locations.Length; j++)
                {
                    var iDistance = locations[i].GetDistance(latitude, longitude);
                    var jDistance = locations[j].GetDistance(latitude, longitude);

                    if (jDistance < iDistance)
                    {
                        //swap
                        var temp = locations[i];
                        locations[i] = locations[j];
                        locations[j] = temp;
                    }
                }
                results.Add(locations[i]);
            }
            return results;
        }

        [HttpPost]
        public IActionResult MemberAuthen(String username, string password)
        {

            var member = db.Members
                           .Where(x => x.Email == username && x.Password == password && x.MemberTypeId == 1)
                            .SingleOrDefault();

            if (member == null) return NotFound();


            return Json(member);


        }

        [HttpGet]
        public List<Coordinate> GetPorts()
        {
            return db.Coordinates
                     .Where(x => x.CoordinateTypeId == 1)
                     .ToList();
        }

        [HttpGet]
        public List<Travel> GetTravel(int coordinateId)
        {
            return db.Travels
                     .Where(x => x.CoordinateId == coordinateId)
                     .ToList();
        }

        [HttpGet]
        public List<BoatHistory> GetBoatGo(int coordinateId)
        {
            var port = db.Coordinates.Find(coordinateId);


            var histories = db.TimeTables
                              .Include(x => x.Route)
                              .Include(x => x.BoatHistories)
                              .ThenInclude(bh => bh.Coordinate)
                              .Where(x => x.IsActive && x.Destination == "ขาไป" && x.BoatHistories.Count() > 0)
                              .ToList();

            var timetables = histories.Where(x => x.BoatHistories.Max(bh => bh.Coordinate.Sequence) <= port.Sequence)
                            .ToList();

            var result = new List<BoatHistory>();

            foreach (var item in timetables)
            {
                var bh = db.BoatHistories
                           .Include(x => x.Coordinate)
                           .Include(x => x.TimeTable)
                           .ThenInclude(tt => tt.Route)
                           .Where(x => x.Id == item.BoatHistories.Max(b => b.Id))
                           .ToList();
                result.Add(bh[0]);
            }
            return result.OrderByDescending(x => x.Coordinate.Sequence).ToList();
        }

        [HttpGet]
        public List<BoatHistory> GetBoatBack(int coordinateId)
        {
            var port = db.Coordinates.Find(coordinateId);


            var histories = db.TimeTables
                              .Include(x => x.Route)
                              .Include(x => x.BoatHistories)
                              .ThenInclude(bh => bh.Coordinate)
                              .Where(x => x.IsActive && x.Destination == "ขากลับ" && x.BoatHistories.Count() > 0)
                              .ToList();

            var timetables = histories.Where(x => x.BoatHistories.Min(bh => bh.Coordinate.Sequence) >= port.Sequence)
                            .ToList();

            var result = new List<BoatHistory>();

            foreach (var item in timetables)
            {
                var bh = db.BoatHistories
                           .Include(x => x.Coordinate)
                           .Include(x => x.TimeTable)
                           .ThenInclude(tt => tt.Route)
                           .Where(x => x.Id == item.BoatHistories.Max(b => b.Id))
                           .ToList();
                result.Add(bh[0]);
            }
            return result.OrderBy(x => x.Coordinate.Sequence).ToList();

            //var port = db.Coordinates.Find(coordinateId);

            //var histories = db.TimeTables
            //                  .Include(x => x.Route)
            //                  .Include(x => x.BoatHistories)
            //                  .ThenInclude(bh => bh.Coordinate)
            //                  .Where(x => x.IsActive && x.Destination == "ขากลับ" && x.BoatHistories.Count() > 0)
            //                  .ToList();

            //return histories.Where(x => x.BoatHistories.Min(bh => bh.Coordinate.Sequence) >= port.Sequence)
            //.ToList();


        }

        [HttpGet]
        public List<Route> GetNavi(int sourceCoordinateId, int destinationCoordinteId)
        {
            var co1 = db.Coordinates
                        .Where(r => r.Id == sourceCoordinateId)
                        .SingleOrDefault();

            var co2 = db.Coordinates
                        .Where(r => r.Id == destinationCoordinteId)
                        .SingleOrDefault();

            var source = db.Routes
                           .Include(r => r.RouteCoordinates)
                           .Where(r => r.RouteCoordinates.Any(rc => rc.CoordinateId == sourceCoordinateId))
                           .ToList();

            var result = source.Where(r => r.RouteCoordinates.Any(rc => rc.CoordinateId == destinationCoordinteId))
                               .Select(r => new Route
                               {
                                   Id = r.Id,
                                   FlagColor = r.FlagColor,
                                   ColorCode = r.ColorCode,
                                   PriceDesc = r.PriceDesc,
                                   DurationMinute = GetDuration(r.Id, co1, co2)
                               })
                               .OrderBy(r => r.DurationMinute)
                               .ToList();


            return result;
        }

        private double GetDuration(int routeId, Coordinate co1, Coordinate co2)
        {
            var distance = GetDistance(co1.Latitude, co1.Longtitude, co2.Latitude, co2.Longtitude);
            var portCount = GetPortCount(routeId, co1, co2);
            return ((distance / 1000) * 1) + (portCount * 3);


        }

        private int GetPortCount(int routeId, Coordinate co1, Coordinate co2)
        {
            if (co1.Sequence < co2.Sequence)
            {
                return db.RouteCoordinates
                         .Where(rc => rc.RouteId == routeId && rc.Coordinate.CoordinateTypeId == 1 && rc.Coordinate.Sequence > co1.Sequence && rc.Coordinate.Sequence < co2.Sequence)
                         .Count();
            }
            else
            {
                return db.RouteCoordinates
                         .Where(rc => rc.RouteId == routeId && rc.Coordinate.CoordinateTypeId == 1 && rc.Coordinate.Sequence > co2.Sequence && rc.Coordinate.Sequence < co1.Sequence)
                         .Count();
            }
        }

        private double GetDistance(double lat1, double long1, double lat2, double long2)
        {
            var olat = lat1 - lat2;
            var olong = long1 - long2;
            var sqrt = Math.Sqrt(Math.Pow(olat, 2) + Math.Pow(olong, 2));
            return (sqrt / (1.0f / 108.4f)) * 1000;
        }

        [HttpGet]
        public List<Emergency> GetEmergencies()
        {
            var result = db.Emergencies
                            .ToList();

            return result;
        }


        [HttpGet]
        public List<Route> GetRoute()
        {
            var result = db.Routes
                           .ToList();
            return result;
        }

        [HttpGet]
        public List<Coordinate> GetPortByRoute(int routeId)
        {
            var result = db.Coordinates
                           .Where(c => c.RouteCoordinates.Any(rc => rc.RouteId == routeId))
                           .OrderBy(c => c.Sequence)
                           .ToList();
            return result;

        }

        [HttpPost]
        public bool SignUp(Member member)
        {
            if (!db.Members.Any(e => e.Email == member.Email))
            {
                member.MemberTypeId = 1;
                db.Members.Add(member);
                db.SaveChanges();

                return true;
            }
            else
                return false;
        }
    }
}

