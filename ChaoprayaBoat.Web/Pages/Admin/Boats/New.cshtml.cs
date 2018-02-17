using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChaoprayaBoat.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChaoprayaBoat.Library.Models;

namespace ChaoprayaBoat.Web.Pages.Admin.Boats
{
    public class NewModel : PageModel
    {
        ApplicationDbContext db;

        public NewModel(ApplicationDbContext db)
        {
            this.db = db;
        }

        [BindProperty]
        public Boat Boat { get; set; }

        public IActionResult OnPost()
        {
            db.Add(Boat);
            db.SaveChanges();

            return RedirectToPage("./Index");
        }


    }
}
