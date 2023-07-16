using Application.DomainServices;
using AutoMapper;
using Domain.Entites;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.ViewModels;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebMarket.Controllers
{
    public class SolutionsController : Controller
    {
        private readonly ISolutionRep solutionRep;
        private readonly IMapper mapper;

        public SolutionsController(ISolutionRep solutionRep, IMapper mapper)
        {
            this.solutionRep = solutionRep;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null)
            {
                RedirectToAction("Error", "Home");
            }

            var solutionList = mapper.Map<IEnumerable<SolutionVM>>(solutionRep.GetAll().Where(sol => sol.ExecutorId == userId));

            return View(solutionList);
        }

        [HttpGet]
        public IActionResult GetMySolutions()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId is null)
            {
                RedirectToAction("Error", "Home");
            }

            var solutionList = mapper.Map<IEnumerable<SolutionVM>>(solutionRep.GetAll().Where(sol => sol.ExecutorId == userId));

            return View(solutionList);
        }
    }
}
