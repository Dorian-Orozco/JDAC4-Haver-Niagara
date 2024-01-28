using Haver_Niagara.Data;
using Haver_Niagara.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Haver_Niagara.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HaverNiagaraDbContext _context; //allows for db access

        public HomeController(ILogger<HomeController> logger, HaverNiagaraDbContext context)
        {
            _logger = logger;
            _context = context;
        }



        public IActionResult Index()
        {
            //Adding functionality to return the list of seed data 
            var ncrs = _context.NCRs
                .Include(p=>p.Product)
                    .ThenInclude(s=>s.Supplier)
                .Include(p=>p.Product)
                    .ThenInclude(d=>d.DefectLists)
                    .ThenInclude(d=>d.Defect)
                .ToList();
            
            //

            return View(ncrs);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
