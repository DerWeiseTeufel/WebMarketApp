using Application.DomainServices;
using Domain.Constants;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.ViewModels;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class AdminController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IUserRep userRep;
        private readonly RoleManager<IdentityRole> roleManager;

        public AdminController(
            UserManager<Domain.Entities.User> userManager,
            IUserRep userRep,
            RoleManager<IdentityRole> roleMgr)
        {
            this.userManager = userManager;
            this.userRep = userRep;
            roleManager = roleMgr;
        }

        public IActionResult Index(RoleVM? roleVM)
        {
            return View(roleVM ?? new RoleVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(RoleVM roleVM)
        {
            if (ModelState.IsValid && roleVM.NewRoleName != null)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = roleVM.NewRoleName
                };

                IdentityResult result = await roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    ViewData["Result"] = "Success";
                    return View("Index");                  
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
           
            return View("Index", roleVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToRole(RoleVM roleVM)
        {
            if (ModelState.IsValid && roleVM.UserEmail != null && roleVM.RoleName != null)
            {
                var userTarget = await userRep.GetByEmailAsync(roleVM.UserEmail);
                IdentityResult result = await userManager.AddToRoleAsync(userTarget, roleVM.RoleName);

                if (result.Succeeded)
                {
                    ViewData["Result"] = "Success";
                    return View("Index");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View("Index", roleVM);
        }
    }
}
