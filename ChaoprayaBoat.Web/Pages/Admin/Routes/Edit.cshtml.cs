using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChaoprayaBoat.Library.Models;
using ChaoprayaBoat.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChaoprayaBoat.Web.Pages.Admin.Routes
{
    public class EditModel : PageModel
    {
        ApplicationDbContext db;
        public EditModel(ApplicationDbContext db)
        {
            this.db = db;
        }

        [BindProperty]
        public Route Route { get; set; }

        public void OnGet(int id)
        {
            Route = db.Routes.Find(id);
        }

        public IActionResult OnPost()
        {
            db.Update(Route);
            db.SaveChanges();

            return RedirectToPage("./Index");
        }
    }
}
