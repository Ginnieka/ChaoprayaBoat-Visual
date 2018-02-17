using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChaoprayaBoat.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChaoprayaBoat.Library.Models;

namespace ChaoprayaBoat.Web.Pages.Admin.Routes
{
    public class NewModel : PageModel
    {
        ApplicationDbContext db;

        public NewModel(ApplicationDbContext db)
        {
            this.db = db;
        }

        [BindProperty]
        public Route Route { get; set; }

        public IActionResult OnPost()
        {
            db.Add(Route);
            db.SaveChanges();

            return RedirectToPage("./Index");
            
        }
    }
}
