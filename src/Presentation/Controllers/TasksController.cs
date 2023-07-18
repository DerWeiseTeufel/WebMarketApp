using Application.Services;
using AutoMapper;
using Presentation.Constants;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.ViewModels;
using System.Data;
using System.Security.Claims;

namespace Presentation.Controllers
{
    public class TasksController : Controller
    {
        private readonly ITaskItemRep taskRep;
        private readonly IMapper mapper;
        private readonly IUserRep userRep;

        public TasksController(ITaskItemRep taskRep, IMapper mapper, IUserRep userRep)
        {
            this.taskRep = taskRep;
            this.mapper = mapper;
            this.userRep = userRep;
        }

        public IActionResult Index()
        {
            var taskItems = mapper.Map<IEnumerable<TaskVM>>(taskRep.GetAllUnremoved()
                .Where(task => task.Deadline > DateTime.Now));            
            ViewData["Currency"] = Сurrencies.GoldenCrocs;

            return View(taskItems);
        }

        [HttpGet]
        [Authorize(Roles=$"{UserRoles.Admin},{UserRoles.User}")]
        public async Task<IActionResult> TaskDetails(int taskId)
        {
            var taskItem = await taskRep.GetByIdAsync(taskId);
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
                await taskRep.AddAsync(mapper.Map<TaskItem>(taskVM));
            }
            else
            {               
                await taskRep.UpdateAsync(mapper.Map<TaskItem>(taskVM));
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
            var taskItem = await taskRep.GetByIdAsync(taskId);

            if (taskItem is null)
            {
                taskItem = new TaskItem();
                taskItem.Deadline = DateTime.Now;                
            }

            ViewData["Currency"] = Сurrencies.GoldenCrocs;

            return View(mapper.Map<TaskVM>(taskItem));
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> TasksHistory()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await userRep.GetByIdAsync(userId ?? "");
            var taskList = mapper.Map<IEnumerable<TaskVM>>(user?.AvailableTasks ?? new List<TaskItem>());
            ViewData["Currency"] = Сurrencies.GoldenCrocs;

            return View(taskList);
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.Admin)]
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
