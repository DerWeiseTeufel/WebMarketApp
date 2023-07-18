using Application.DomainServices;
using AutoMapper;
using Marketplace.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Marketplace.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {
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

        public IActionResult PageNotFound()
        {
            return View();
        }
    }
}