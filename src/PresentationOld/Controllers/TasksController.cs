using Application.DomainServices;
using AutoMapper;
using Domain.Constants;
using Domain.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.ViewModels;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class TasksController : Controller
    {
        private readonly ITaskItemRep taskRep;
        private readonly IMapper mapper;

        public TasksController(ITaskItemRep taskRep, IMapper mapper)
        {
            this.taskRep = taskRep;
            this.mapper = mapper;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var taskItems = mapper.Map<IEnumerable<TaskVM>>(taskRep.GetAll());
            ViewData["Currency"] = Сurrencies.GoldenCrocs;

            return View(taskItems);
        }

        [HttpGet]
        [Authorize(Roles = $"{UserRoles.User}, {UserRoles.Admin}")]
        public IActionResult TaskDetails(int taskId)
        {
            var taskItem = taskRep.GetByIdAsync(taskId);

            if (taskItem is null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewData["Currency"] = Сurrencies.GoldenCrocs;

            return View(mapper.Map<TaskVM>(taskItem));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveTask(TaskVM taskVM)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Error"] = "Form validation successfully failed";
                return View("EditTask", taskVM);
            }

            if (taskVM.CreatorId is null)
            {
                taskVM.CreatorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            await taskRep.SaveAsync(mapper.Map<TaskItem>(taskVM));

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> EditTask(int taskId)
        {
            var taskItem = await taskRep.GetByIdAsync(taskId);

            if (taskItem is null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewData["Currency"] = Сurrencies.GoldenCrocs;

            return View(mapper.Map<TaskVM>(taskItem));
        }

        [HttpGet]
        public async Task<IActionResult> DeleteTask(int taskId)
        {
            var taskItem = await taskRep.GetByIdAsync(taskId);

            if (taskItem != null)
            {
                await taskRep.DeleteAsync(taskItem);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
