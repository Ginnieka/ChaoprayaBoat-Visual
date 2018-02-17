using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChaoprayaBoat.Library.Models;
using ChaoprayaBoat.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChaoprayaBoat.Web.Pages.Admin.Members
{
    public class NewModel : PageModel
    {
        ApplicationDbContext db;

        public NewModel(ApplicationDbContext db)
        {
            this.db = db;
        }

        [BindProperty]
        public Member Member { get; set; }

        public SelectList MemberType { get; set; }


        public void OnGet() //กดขึ้นตอนโหลดpageครั้งแรก
        {
            var memberTypes = db.MemberTypes.ToList();

            MemberType  = new SelectList(memberTypes, "Id", "Name");
        }

        public IActionResult OnPost() //โชว์ เมื่อเรากดsubmit
        {
            db.Add(Member);
            db.SaveChanges();

            return RedirectToPage("./Index");
        }
    }
}
