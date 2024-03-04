using Haver_Niagara.Data;
using Haver_Niagara.Models;
using Haver_Niagara.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Globalization;
using X.PagedList;


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




        public IActionResult List(string sortOrder, string searchString, string selectedSupplier, string selectedDate, bool? selectedStatus, int? page, string currentFilter)
        {
            //Sorting Functionality , Tutorial Also included adding search box might be handy later. 
            //https://learn.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/sorting-filtering-and-paging-with-the-entity-framework-in-an-asp-net-mvc-application

            //Better view bag sortOrder assignment (tutorial didnt work) https://stackoverflow.com/questions/38082611/asp-net-mvc-sort-not-work?rq=3

            //Part Number Sort
            ViewBag.POSortParam = sortOrder == "ProductNum_Asc" ? "ProductNum_Desc" : "ProductNum_Asc";
            //Supplier Name
            ViewBag.SupplierSortParam = sortOrder == "Supplier_Asc" ? "Supplier_Desc" : "Supplier_Asc";
            //Order by open or closed 
            ViewBag.StageSortParam = sortOrder == "Stage_Asc" ? "Stage_Desc" : "Stage_Asc";
            //Order by date
            ViewBag.DateSortParam = sortOrder == "Date_Asc" ? "Date_Desc" : "Date_Asc";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;


            //Adding functionality to return the list of seed data 
            var ncrs = _context.NCRs
                .Include(p => p.Part)
                    .ThenInclude(s => s.Supplier)
                .Include(p => p.Part)
                    .ThenInclude(d => d.DefectLists)
                    .ThenInclude(d => d.Defect)
                .ToList();

            // Create a list to store all NCRs with suppliers
            var originalNCRs = _context.NCRs
                .Include(p => p.Part)
                .ThenInclude(s => s.Supplier)
                .ToList();

            // Create a separate list for dropdown options
            var suppliersForDropdown = originalNCRs.Select(x => x.Part.Supplier.Name).Distinct().ToList();

            // Update the ViewBag.SupplierList with the dropdown options
            ViewBag.SupplierList = new SelectList(suppliersForDropdown, "Select Supplier");

            // Filter by Supplier
            if (!String.IsNullOrEmpty(selectedSupplier) && selectedSupplier != "Select Supplier")
            {
                // Apply the supplier filter to the main list
                ncrs = originalNCRs.Where(x => x.Part.Supplier.Name == selectedSupplier).ToList();
            }

            // Get all unique suppliers for the dropdown list
            var allSuppliers = originalNCRs.Select(x => x.Part.Supplier.Name).Distinct().ToList();

            ViewBag.SupplierList = new SelectList(allSuppliers, selectedSupplier);

            //Search Box
            if (!String.IsNullOrEmpty(searchString))
            {


                //You can add more columns to search, as long as the table relationship is established if it not an NCR
                //Furthermore might consider adding a clear button? 
                searchString = searchString.ToLower();

                ncrs = ncrs.Where(x =>
                 x.NCR_Date.ToString().ToLower().Contains(searchString) ||
                 x.Part.ProductNumber.ToString().ToLower().Contains(searchString) ||
                 x.NCR_Number.ToString().ToLower().Contains(searchString) ||
                 x.Part.Supplier.Name.ToLower().Contains(searchString)
                 ).ToList();
            }

            // Filter by Date
            if (!String.IsNullOrEmpty(selectedDate))
            {
                DateTime parsedDate;
                if (DateTime.TryParseExact(selectedDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
                {
                    ncrs = ncrs.Where(x => x.NCR_Date.Date == parsedDate.Date).ToList();
                }
            }

            // Filter by Status
            if (selectedStatus.HasValue)
            {
                ncrs = ncrs.Where(x => x.NCR_Status == selectedStatus.Value).ToList();
            }

            //Determines the sorting order
            switch (sortOrder)       
            {
                //Part Number
                case "ProductNum_Desc":
                    ncrs = ncrs.OrderByDescending(b => b.Part.ProductNumber).ToList();
                    break;
                case "ProductNum_Asc":
                    ncrs = ncrs.OrderBy(b=>b.Part.ProductNumber).ToList();
                    break;

                //Supplier Name
                case "Supplier_Desc":
                    ncrs = ncrs.OrderByDescending(s => s.Part.Supplier.Name).ToList();
                    break;
                case "Supplier_Asc":
                    ncrs = ncrs.OrderBy(n=>n.Part.Supplier.Name).ToList();
                    break;

                //Since we do not have a BOOLEAN value in NCR, this must be changed, currently using Status (open/closed atm) 
                case "Stage_Desc":              
                    ncrs = ncrs.OrderByDescending(b => b.NCR_Status).ToList();
                    break;
                case "Stage_Asc":
                    ncrs = ncrs.OrderBy(b=>b.NCR_Status).ToList();
                    break;
                case "Date_Asc":
                    ncrs = ncrs.OrderByDescending(b=>b.NCR_Date).ToList();
                    break;
                case "Date_Desc":
                    ncrs = ncrs.OrderBy(b => b.NCR_Date).ToList();
                    break;
            }




            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(ncrs.ToPagedList(pageNumber, pageSize));
        }

        public IActionResult ClearFilters()
        {
            return RedirectToAction("List", new { sortOrder = "", searchString = "", selectedSupplier = "", selectedDate = "", selectedStatus = "" });
        }

        public IActionResult Index()
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
