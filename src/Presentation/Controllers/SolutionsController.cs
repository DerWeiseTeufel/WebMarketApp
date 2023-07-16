using Application.DomainServices;
using AutoMapper;
using Domain.Constants;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.ViewModels;
using System.Security.Claims;

namespace WebMarket.Controllers
{
    [Authorize]
    public class SolutionsController : Controller
    {
        private readonly ISolutionRep solutionRep;
        private readonly IMapper mapper;
        private readonly ITaskItemRep taskItemRep;
        private readonly IUserRep userRep;

        public SolutionsController(ISolutionRep solutionRep, IMapper mapper, ITaskItemRep taskItemRep, IUserRep userRep)
        {
            this.solutionRep = solutionRep;
            this.mapper = mapper;
            this.taskItemRep = taskItemRep;
            this.userRep = userRep;
        }

        public async Task<IActionResult> Index()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await userRep.GetByIdAsync(userId ?? "");
            var solutionList = mapper.Map<IEnumerable<SolutionVM>>(user?.Solutions ?? new List<Solution>());
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
                solVM.Status = SolutionStatuses.Accepted.ToString();
                await solutionRep.UpdateAsync(mapper.Map<Solution>(solVM));
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
                solVM.Status = SolutionStatuses.Rejected.ToString();
                await solutionRep.UpdateAsync(mapper.Map<Solution>(solVM));
            }

            return RedirectToAction("TaskDetails", "Tasks", new { taskId = solVM.TaskItemId });
        }

        [HttpGet]
        public async Task<IActionResult> NewSolution(int taskId)
        {
            var taskItem = await taskItemRep.GetByIdAsync(taskId);
            if (taskItem is null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(new SolutionVM() { Id = taskId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSol(SolutionVM solVM)
        {
            if (ModelState.IsValid)
            {
                await solutionRep.DeleteAsync(mapper.Map<Solution>(solVM));
            }

            return await Index();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]    
        public async Task<IActionResult> SaveSolution(int taskId, string URL)
        {
            var solVM = new SolutionVM()
            {
                TaskItemId = taskId,
                URL = URL,
                ExecutorId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                UploadDate = DateTime.Now,
                Status = SolutionStatuses.UnderReview.ToString(),
                Comment = ""
            };

            await solutionRep.AddAsync(mapper.Map<Solution>(solVM));

            return RedirectToAction("Index");
        }
    }
}
