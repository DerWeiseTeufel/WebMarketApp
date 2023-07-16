using Application.DomainServices;
using AutoMapper;
using Domain.Constants;
using Domain.Entities;
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

        public TasksController(ITaskItemRep taskRep, IMapper mapper)
        {
            this.taskRep = taskRep;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var taskItems = mapper.Map<IEnumerable<TaskVM>>(taskRep.GetAll()
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
                return RedirectToAction("Index");
            }

            var taskVM = mapper.Map<TaskVM>(taskItem);
            taskVM.Solutions = mapper.Map<ICollection<SolutionVM>>(taskVM.Solutions);
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
