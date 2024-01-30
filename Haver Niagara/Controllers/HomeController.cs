using Haver_Niagara.Data;
using Haver_Niagara.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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



        public IActionResult Index(string sortOrder, string searchString)
        {
            //Sorting Functionality , Tutorial Also included adding search box might be handy later. 
            //https://learn.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/sorting-filtering-and-paging-with-the-entity-framework-in-an-asp-net-mvc-application

            //Better view bag sortOrder assignment (tutorial didnt work) https://stackoverflow.com/questions/38082611/asp-net-mvc-sort-not-work?rq=3

            //Product Number Sort
            ViewBag.POSortParam = sortOrder == "ProductNum_Asc" ? "ProductNum_Desc" : "ProductNum_Asc";
            //Supplier Name
            ViewBag.SupplierSortParam = sortOrder == "Supplier_Asc" ? "Supplier_Desc" : "Supplier_Asc";
            //Order by open or closed 
            ViewBag.StageSortParam = sortOrder == "Stage_Asc" ? "Stage_Desc" : "Stage_Asc";
            //Order by date
            ViewBag.DateSortParam = sortOrder == "Date_Asc" ? "Date_Desc" : "Date_Asc";


            //Adding functionality to return the list of seed data 
            var ncrs = _context.NCRs
                .Include(p => p.Product)
                    .ThenInclude(s => s.Supplier)
                .Include(p => p.Product)
                    .ThenInclude(d => d.DefectLists)
                    .ThenInclude(d => d.Defect)
                .ToList();

            //Search Box
            if (!String.IsNullOrEmpty(searchString))
            {
                //You can add more columns to search, as long as the table relationship is established if it not an NCR
                //Furthermore might consider adding a clear button? 
                searchString = searchString.ToLower();

                ncrs = ncrs.Where(x =>
                 x.InspectDate.ToString().ToLower().Contains(searchString) ||
                 x.Product.ProductNumber.ToString().ToLower().Contains(searchString) ||
                 x.NCR_Number.ToString().ToLower().Contains(searchString) ||
                 x.Product.Supplier.Name.ToLower().Contains(searchString)
                 ).ToList();
            }
            

            //Determines the sorting order
            switch (sortOrder)       
            {
                //Product Number
                case "ProductNum_Desc":
                    ncrs = ncrs.OrderByDescending(b => b.Product.ProductNumber).ToList();
                    break;
                case "ProductNum_Asc":
                    ncrs = ncrs.OrderBy(b=>b.Product.ProductNumber).ToList();
                    break;

                //Supplier Name
                case "Supplier_Desc":
                    ncrs = ncrs.OrderByDescending(s => s.Product.Supplier.Name).ToList();
                    break;
                case "Supplier_Asc":
                    ncrs = ncrs.OrderBy(n=>n.Product.Supplier.Name).ToList();
                    break;

                //NCR stage open or closed (bool)
                case "Stage_Desc":
                    ncrs = ncrs.OrderByDescending(b => b.NCRClosed).ToList();
                    break;
                case "Stage_Asc":
                    ncrs = ncrs.OrderBy(b=>b.NCRClosed).ToList();
                    break;
                case "Date_Asc":
                    ncrs = ncrs.OrderByDescending(b=>b.InspectDate).ToList();
                    break;
                case "Date_Desc":
                    ncrs = ncrs.OrderBy(b => b.InspectDate).ToList();
                    break;
            }

            return View(ncrs.ToList());
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
