using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ChaoprayaBoat.Library.Models;

namespace ChaoprayaBoat.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        
        public DbSet<Boat> Boats { get; set; } 
        public DbSet<Emergency> Emergencies { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<MemberType> MemberTypes { get; set; }
        public DbSet<Coordinate> Coordinates { get; set; }
        public DbSet<Cost> Costs  { get; set; }
        public DbSet<Route> Routes  { get; set; }
        public DbSet<RouteCoordinate> RouteCoordinates { get; set; }
        public DbSet<Travel> Travels { get; set; }
        public DbSet<TimeTable> TimeTables { get; set; }
        public DbSet<CoordinateType> CoordinateTypes { get; set; }
        public DbSet<BoatHistory> BoatHistories { get; set; }
        public DbSet<InfoPort> InfoPorts { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
