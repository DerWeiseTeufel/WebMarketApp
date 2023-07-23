using Application.UseCases;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Constants;
using Presentation.ViewModels;
using System.Security.Claims;

namespace Presentation.Controllers
{
    public class TasksController : Controller
    {
        private readonly IMapper mapper;
        private readonly TaskUseCases taskUseCases;

        public TasksController(IMapper mapper, TaskUseCases taskUseCases)
        {
            this.mapper = mapper;
            this.taskUseCases = taskUseCases;
        }

        public IActionResult Index()
        {
            var taskItems = mapper.Map<IEnumerable<TaskVM>>(taskUseCases.GetActive.GetActive());
            ViewData["Currency"] = Сurrencies.GoldenCrocs;

            return View(taskItems);
        }

        [HttpGet]
        [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.User}")]
        public async Task<IActionResult> TaskDetails(int taskId)
        {
            var taskItem = await taskUseCases.GetById.GetByIdAsync(taskId);
            if (taskItem is null)
            {
                return RedirectToAction("PageNotFound", "Home");
            }

            var taskVM = mapper.Map<TaskVM>(taskItem);
            taskVM.Solutions = mapper.Map<ICollection<SolutionVM>>(taskItem.AvailableSolutions);
            ViewData["Currency"] = Сurrencies.GoldenCrocs;

            return View(taskVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = UserRoles.Admin)]
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
                await taskUseCases.Add.AddTaskAsync(mapper.Map<TaskItem>(taskVM));
            }
            else
            {
                await taskUseCases.Update.UpdateAsync(mapper.Map<TaskItem>(taskVM));
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult NewTask()
        {
            return RedirectToAction("EditTask", -1);
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> EditTask(int taskId)
        {
            var taskItem = await taskUseCases.GetById.GetByIdAsync(taskId);
            if(taskItem is null)
            {
                taskItem = new TaskItem();
                taskItem.Deadline = DateTime.Now;
            }

            ViewData["Currency"] = Сurrencies.GoldenCrocs;

            return View(mapper.Map<TaskVM>(taskItem));
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult TasksHistory()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                var taskList = mapper.Map<IEnumerable<TaskVM>>(taskUseCases.GetUserTasks.GetUserTasks(userId));
                ViewData["Currency"] = Сurrencies.GoldenCrocs;

                return View(taskList);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> DeleteTask(int taskId)
        {
            await taskUseCases.Delete.DeleteByIdAsync(taskId);
            return RedirectToAction("Index", "Home");
        }
    }
}
