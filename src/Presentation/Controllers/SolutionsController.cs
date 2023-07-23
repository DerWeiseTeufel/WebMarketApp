using Application.UseCases;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Constants;
using Presentation.ViewModels;
using System.Security.Claims;

namespace WebMarket.Controllers
{
    [Authorize]
    public class SolutionsController : Controller
    {
        private readonly IMapper mapper;
        private readonly SolUseCases solUseCases;
        private readonly TaskUseCases taskUseCases;

        public SolutionsController(IMapper mapper, SolUseCases solUseCases, TaskUseCases taskUseCases)
        {
            this.mapper = mapper;
            this.solUseCases = solUseCases;
            this.taskUseCases = taskUseCases;
        }

        public async Task<IActionResult> Index()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var sols = await solUseCases.GetUserSols.GetUserSolsAsync(userId);
            var solutionList = mapper.Map<IEnumerable<SolutionVM>>(sols);
            ViewData["Currency"] = Сurrencies.GoldenCrocs;

            return View(solutionList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> AcceptSol(SolutionVM solVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await solUseCases.Accept.AcceptSolAsync(mapper.Map<Solution>(solVM));
                }
                catch (Exception)
                {
                    return RedirectToAction("Error", "Home");
                }
            }

            return RedirectToAction("TaskDetails", "Tasks", new { taskId = solVM.TaskItemId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> DeclineSol(SolutionVM solVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await solUseCases.Decline.DeclineSolAsync(mapper.Map<Solution>(solVM));
                }
                catch (Exception)
                {
                    return RedirectToAction("Error", "Home");
                }
            }

            return RedirectToAction("TaskDetails", "Tasks", new { taskId = solVM.TaskItemId });
        }

        [HttpGet]
        public async Task<IActionResult> NewSolution(int taskId)
        {
            if (await taskUseCases.GetById.GetByIdAsync(taskId) is null)
            {
                return RedirectToAction("PageNotFound", "Home");
            }

            return View(new SolutionVM() { Id = taskId });
        }

        [HttpGet]
        public async Task<IActionResult> DeleteSol(int solId)
        {
            await solUseCases.Delete.DeleteByIdAsync(solId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveSolution(int taskId, string URL)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                await solUseCases.Add.AddSolutionAsync(taskId, userId, URL);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }

            return RedirectToAction("Index");
        }
    }
}
