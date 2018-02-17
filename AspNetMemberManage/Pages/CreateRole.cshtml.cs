using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AspNetMemberManage.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetMemberManage.Pages
{
    public class CreateRoleModel : PageModel
    {
        ApplicationDbContext context;

        public CreateRoleModel(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void OnGet()
        {
        }

        [BindProperty]
        [Required]
        public string Name { get; set; }

        public IActionResult OnPost()
        {
            context.Roles.Add(new IdentityRole
            {
                Name = Name
            });

            context.SaveChanges();

            return RedirectToPage("./Role");
        }
    }
}
