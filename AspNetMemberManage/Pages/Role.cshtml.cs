using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetMemberManage.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;

namespace AspNetMemberManage.Pages
{
    public class RoleModel : PageModel
    {
        ApplicationDbContext context;

        public RoleModel(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<IdentityRole> Roles { get; set; }

        public void OnGet()
        {
            Roles = context.Roles.OrderBy(r => r.Name).ToList();
        }

        public IActionResult OnPostDeleteAsync(string name)
        {
            var role = context.Roles.Find(name);
            context.Roles.Remove(role);
            context.SaveChanges();
            return RedirectToPage("./Role");
        }
    }
}
