using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChaoprayaBoat.Library.Models;
using ChaoprayaBoat.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ChaoprayaBoat.Web.Pages.Admin.TimeTables.BoatHistories
{
    public class IndexModel : PageModel
    {
        ApplicationDbContext db;
        public IndexModel(ApplicationDbContext db)
        {
            this.db = db;
        }

        [BindProperty]
        public int Month { get; set; }
        [BindProperty]
        public int Year { get; set; }
        [BindProperty]
        public string Destination { get; set; }

        public List<BoatHistory> BoatHistories { get; set; }

        public void OnGet(int timeTableId, int month, int year,string dest)
        {
            Month = month;
            Year = year;
            Destination = dest;

            BoatHistories = db.BoatHistories
                              .Include(x => x.Coordinate)
                              .ThenInclude(c => c.CoordinateType)
                              .Where(x => x.TimeTableId == timeTableId)
                              .OrderBy(x => x.Id)
                              .ToList();
        }
    }
}
