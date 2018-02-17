using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetMemberManage.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AspNetMemberManage.Pages
{
    public class IndexModel : PageModel
    {
        ApplicationDbContext db;
        UserManager<ApplicationUser> userManager;

        public IndexModel(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public IList<UserInfo> UserInfos { get; set; }

        public async Task OnGetAsync()
        {
            var users = await userManager.Users
                                         .OrderBy(x => x.UserName)
                                         .ToListAsync();
            await LoadUserInfos(users);
        }

        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            var user = await userManager.FindByNameAsync(id);
            await userManager.DeleteAsync(user);
            return RedirectToPage("./Index");
        }

        async Task LoadUserInfos(IList<ApplicationUser> users)
        {
            var staffs = new List<UserInfo>();
            foreach (var item in users)
            {
                var roles = await userManager.GetRolesAsync(item);
                staffs.Add(new UserInfo
                {
                    UserName = item.UserName,
                    Role = roles[0]
                });
            }
            UserInfos = staffs.OrderBy(x => x.Role).ToList();
        }

        public class UserInfo
        {
            public string UserName { get; set; }
            public string Role { get; set; }
        }
    }
}
