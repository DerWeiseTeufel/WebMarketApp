using Application.DomainServices;
using AutoMapper;
using Marketplace.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Marketplace.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITaskItemRep taskRep;
        private readonly IMapper mapper;

        public HomeController(ITaskItemRep taskRep, IMapper mapper)
        {
            this.taskRep = taskRep;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Tasks");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}