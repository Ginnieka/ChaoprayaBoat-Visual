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
    public class EditModel : PageModel
    {
        ApplicationDbContext db;

        public EditModel(ApplicationDbContext db)
        {
            this.db = db;
        }

        [BindProperty]
        public Member Member { get; set; }

        public SelectList MemberType { get; set; }


        public void OnGet(int id) 
        {
            Member = db.Members.Find(id);

            var memberTypes = db.MemberTypes.ToList();

            MemberType = new SelectList(memberTypes, "Id", "Name");
        }

        public IActionResult OnPost() //โชว์ เมื่อเรากดsubmit
        {
            db.Update(Member);
            db.SaveChanges();

            return RedirectToPage("./Index");
        }
    }
}
