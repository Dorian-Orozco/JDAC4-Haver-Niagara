using Haver_Niagara.Data;
using Haver_Niagara.Models;
using Haver_Niagara.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using X.PagedList;

namespace Haver_Niagara.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HaverNiagaraDbContext _context; // allows for db access

        public HomeController(ILogger<HomeController> logger, HaverNiagaraDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult List(string sortOrder, string searchString, string selectedSupplier, string selectedDate, bool? selectedStatus, int? page, string currentFilter)
        {
            // Sorting Functionality
            ViewBag.POSortParam = sortOrder == "ProductNum_Asc" ? "ProductNum_Desc" : "ProductNum_Asc";
            ViewBag.SupplierSortParam = sortOrder == "Supplier_Asc" ? "Supplier_Desc" : "Supplier_Asc";
            ViewBag.StageSortParam = sortOrder == "Stage_Asc" ? "Stage_Desc" : "Stage_Asc";
            ViewBag.DateSortParam = sortOrder == "Date_Asc" ? "Date_Desc" : "Date_Asc";
            ViewBag.CurrentFilter = currentFilter;
            ViewBag.SelectedSupplier = selectedSupplier;
            ViewBag.SelectedDate = selectedDate;
            ViewBag.SelectedStatus = selectedStatus;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var originalNCRs = _context.NCRs
                .Where(p => p.IsArchived == false)
                .Include(p => p.Part)
                .ThenInclude(s => s.Supplier)
                .Include(p => p.Part.DefectLists)
                .ThenInclude(d => d.Defect)
                .ToList();

            var ncrs = _context.NCRs
                .Where(p => p.IsArchived == false) 
                .Include(p => p.Part)
                .ThenInclude(s => s.Supplier)
                .Include(p => p.Part.DefectLists)
                .ThenInclude(d => d.Defect)
                .AsQueryable();

            // Apply filters
            if (!String.IsNullOrEmpty(selectedSupplier) && selectedSupplier != "Select Supplier")
            {
                ncrs = ncrs.Where(x => x.Part.Supplier.Name == selectedSupplier);
            }

            if (!selectedStatus.HasValue)
            {
                ncrs = ncrs.Where(x => x.NCR_Status == true);
            }
            else if (selectedStatus.HasValue)
            {
                ncrs = ncrs.Where(x => x.NCR_Status == selectedStatus.Value);
            }
            ViewBag.SelectedStatus = selectedStatus;

            // Search Box
            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();

                ncrs = ncrs.Where(x =>
                    x.NCR_Date.ToString().ToLower().Contains(searchString) ||
                    x.Part.ProductNumber.ToString().ToLower().Contains(searchString) ||
                    x.ID.ToString().ToLower().Contains(searchString) ||
                    x.Part.Supplier.Name.ToLower().Contains(searchString)
                );
            }

            // Filter by Date
            if (!String.IsNullOrEmpty(selectedDate))
            {
                DateTime parsedDate;
                if (DateTime.TryParseExact(selectedDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
                {
                    ncrs = ncrs.Where(x => x.NCR_Date.Date == parsedDate.Date);
                }
            }

            // Sorting
            IOrderedQueryable<NCR> sortedNCRs;
            switch (sortOrder)
            {
                case "ProductNum_Desc":
                    sortedNCRs = ncrs.OrderByDescending(b => b.Part.ProductNumber);
                    break;
                case "ProductNum_Asc":
                    sortedNCRs = ncrs.OrderBy(b => b.Part.ProductNumber);
                    break;
                case "Supplier_Desc":
                    sortedNCRs = ncrs.OrderByDescending(s => s.Part.Supplier.Name);
                    break;
                case "Supplier_Asc":
                    sortedNCRs = ncrs.OrderBy(n => n.Part.Supplier.Name);
                    break;
                case "Stage_Desc":
                    sortedNCRs = ncrs.OrderByDescending(b => b.NCR_Status);
                    break;
                case "Stage_Asc":
                    sortedNCRs = ncrs.OrderBy(b => b.NCR_Status);
                    break;
                case "Date_Asc":
                    sortedNCRs = ncrs.OrderBy(b => b.NCR_Date);
                    break;
                case "Date_Desc":
                    sortedNCRs = ncrs.OrderByDescending(b => b.NCR_Date);
                    break;
                default:
                    sortedNCRs = ncrs.OrderBy(b => b.ID);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var pagedNCRs = sortedNCRs.ToPagedList(pageNumber, pageSize);

            // Create a separate list for dropdown options
            var suppliersForDropdown = originalNCRs
                .Where(x => x.Part != null && x.Part.Supplier != null)
                .Select(x => x.Part.Supplier.Name)
                .Distinct()
                .ToList();

            // Update the ViewBag.SupplierList with the dropdown options
            ViewBag.SupplierList = new SelectList(suppliersForDropdown, selectedSupplier);

            // Get all unique suppliers for the dropdown list
            var allSuppliers = originalNCRs
                .Where(x => x.Part != null && x.Part.Supplier != null && x.Part.Supplier.Name != null)
                .Select(x => x.Part.Supplier.Name)
                .Distinct()
                .ToList();

            ViewBag.SupplierList = new SelectList(allSuppliers, selectedSupplier);

            return View(pagedNCRs);
        }

        public IActionResult ListArchive(string sortOrder, string searchString, string selectedSupplier, string selectedDate, bool? selectedStatus, int? page, string currentFilter)
        {
            // Sorting Functionality
            ViewBag.POSortParam = sortOrder == "ProductNum_Asc" ? "ProductNum_Desc" : "ProductNum_Asc";
            ViewBag.SupplierSortParam = sortOrder == "Supplier_Asc" ? "Supplier_Desc" : "Supplier_Asc";
            ViewBag.StageSortParam = sortOrder == "Stage_Asc" ? "Stage_Desc" : "Stage_Asc";
            ViewBag.DateSortParam = sortOrder == "Date_Asc" ? "Date_Desc" : "Date_Asc";
            ViewBag.CurrentFilter = currentFilter;
            ViewBag.SelectedSupplier = selectedSupplier;
            ViewBag.SelectedDate = selectedDate;
            ViewBag.SelectedStatus = selectedStatus;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var originalNCRs = _context.NCRs
                .Where(p => p.IsArchived == true)
                .Include(p => p.Part)
                .ThenInclude(s => s.Supplier)
                .Include(p => p.Part.DefectLists)
                .ThenInclude(d => d.Defect)
                .ToList();

            var ncrs = _context.NCRs
                .Where(p => p.IsArchived == true)
                .Include(p => p.Part)
                .ThenInclude(s => s.Supplier)
                .Include(p => p.Part.DefectLists)
                .ThenInclude(d => d.Defect)
                .AsQueryable();

            // Apply filters
            if (!String.IsNullOrEmpty(selectedSupplier) && selectedSupplier != "Select Supplier")
            {
                ncrs = ncrs.Where(x => x.Part.Supplier.Name == selectedSupplier);
            }

            if (!selectedStatus.HasValue)
            {
                ncrs = ncrs.Where(x => x.NCR_Status == false); //archive list shows closed ncr's on default
            }
            //else if (selectedStatus.HasValue)
            //{
            //    ncrs = ncrs.Where(x => x.NCR_Status == selectedStatus.Value);
            //}
            ViewBag.SelectedStatus = selectedStatus;

            // Search Box
            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();

                ncrs = ncrs.Where(x =>
                    x.NCR_Date.ToString().ToLower().Contains(searchString) ||
                    x.Part.ProductNumber.ToString().ToLower().Contains(searchString) ||
                    x.ID.ToString().ToLower().Contains(searchString) ||
                    x.Part.Supplier.Name.ToLower().Contains(searchString)
                );
            }

            // Filter by Date
            if (!String.IsNullOrEmpty(selectedDate))
            {
                DateTime parsedDate;
                if (DateTime.TryParseExact(selectedDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
                {
                    ncrs = ncrs.Where(x => x.NCR_Date.Date == parsedDate.Date);
                }
            }

            // Sorting
            IOrderedQueryable<NCR> sortedNCRs;
            switch (sortOrder)
            {
                case "ProductNum_Desc":
                    sortedNCRs = ncrs.OrderByDescending(b => b.Part.ProductNumber);
                    break;
                case "ProductNum_Asc":
                    sortedNCRs = ncrs.OrderBy(b => b.Part.ProductNumber);
                    break;
                case "Supplier_Desc":
                    sortedNCRs = ncrs.OrderByDescending(s => s.Part.Supplier.Name);
                    break;
                case "Supplier_Asc":
                    sortedNCRs = ncrs.OrderBy(n => n.Part.Supplier.Name);
                    break;
                case "Stage_Desc":
                    sortedNCRs = ncrs.OrderByDescending(b => b.NCR_Status);
                    break;
                case "Stage_Asc":
                    sortedNCRs = ncrs.OrderBy(b => b.NCR_Status);
                    break;
                case "Date_Asc":
                    sortedNCRs = ncrs.OrderBy(b => b.NCR_Date);
                    break;
                case "Date_Desc":
                    sortedNCRs = ncrs.OrderByDescending(b => b.NCR_Date);
                    break;
                default:
                    sortedNCRs = ncrs.OrderBy(b => b.ID);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            var pagedNCRs = sortedNCRs.ToPagedList(pageNumber, pageSize);

            // Create a separate list for dropdown options
            var suppliersForDropdown = originalNCRs
                .Where(x => x.Part != null && x.Part.Supplier != null)
                .Select(x => x.Part.Supplier.Name)
                .Distinct()
                .ToList();

            // Update the ViewBag.SupplierList with the dropdown options
            ViewBag.SupplierList = new SelectList(suppliersForDropdown, selectedSupplier);

            // Get all unique suppliers for the dropdown list
            var allSuppliers = originalNCRs
                .Where(x => x.Part != null && x.Part.Supplier != null && x.Part.Supplier.Name != null)
                .Select(x => x.Part.Supplier.Name)
                .Distinct()
                .ToList();

            ViewBag.SupplierList = new SelectList(allSuppliers, selectedSupplier);

            return View(pagedNCRs);
        }

        public IActionResult ClearFilters()
        {
            return RedirectToAction("List");
        }

        // For Dashboard
		public async Task<IActionResult> Index()
		{
			var ncrs = await _context.NCRs
				.Include(n => n.Part)
					.ThenInclude(p => p.Supplier)
				.Where(n => n.NCR_Status == true)
				.OrderBy(n => n.NCR_Date)
				.Take(5)
				.ToListAsync();

			return View(ncrs);
		}

        public async Task<IActionResult> GetOpenNCRCount()
        {
            var openNCRCount = await _context.NCRs.CountAsync(n => n.NCR_Status && n.NCR_Date.Year == DateTime.Now.Year);
            return Json(new { count = openNCRCount });
        }

        public async Task<IActionResult> GetClosedNCRCount()
        {
            var closedNCRCount = await _context.NCRs.CountAsync(n => !n.NCR_Status && n.NCR_Date.Year == DateTime.Now.Year);
            return Json(new { count = closedNCRCount });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}