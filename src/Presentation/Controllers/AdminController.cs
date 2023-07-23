using Application.Services;
using Presentation.Constants;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.ViewModels;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Application.UseCases.Common;
using Application.UseCases;

namespace Presentation.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class AdminController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly UserUseCases userUseCases;
        private readonly RoleManager<IdentityRole> roleManager;

        public AdminController(
            UserManager<Domain.Entities.User> userManager,
            UserUseCases userUseCases,
            RoleManager<IdentityRole> roleMgr)
        {
            this.userManager = userManager;
            this.userUseCases = userUseCases;
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
                if (await roleManager.FindByNameAsync(roleVM.RoleName) is null)
                {
                    ModelState.AddModelError("", "The role doesn`t exist");
                }
                else
                {
                    var userTarget = await userUseCases.GetByEmail.GetByEmailAsync(roleVM.UserEmail);
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
            }

            return View("Index", roleVM);
        }
    }
}
