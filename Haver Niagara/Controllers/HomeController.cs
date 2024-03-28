using Haver_Niagara.Data;
using Haver_Niagara.Models;
using Haver_Niagara.Utilities;
using IronPdf.Extensions.Mvc.Core; //for pdf generator
using IronPdf.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web.Mvc.Html;
using X.PagedList;
using Microsoft.AspNetCore.Authorization;


namespace Haver_Niagara.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HaverNiagaraDbContext _context; // allows for db access

        //for pdf converter
        private readonly IRazorViewRenderer _viewRenderService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        // CONSTRUCTOR //
        public HomeController(ILogger<HomeController> logger, HaverNiagaraDbContext context, IRazorViewRenderer viewRenderService, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _context = context;
            //for pdf converter
            _viewRenderService = viewRenderService;
            _httpContextAccessor = httpContextAccessor;
        }

        // NCR LIST PDF USING RAZOR VIEW NCRs.cshtml in Home Controller
        public async Task<IActionResult> NCRs(int? page)
        {
            var ncrs = _context.NCRs
             .Where(p => p.IsArchived == false && p.NCR_Status == true) //active ncrs that have not been archived
             .Include(p => p.Part)
             .ThenInclude(s => s.Supplier)
             .Include(p => p.Part.DefectLists)
             .ThenInclude(d => d.Defect)
             .AsQueryable();

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            // Convert the query to a paged list
            var pagedNCRs = await ncrs.ToPagedListAsync(pageNumber, pageSize);

            if (_httpContextAccessor.HttpContext.Request.Method == HttpMethod.Post.Method)
            {
                ChromePdfRenderer renderer = new ChromePdfRenderer();

                renderer.RenderingOptions.MarginTop = 10;
                renderer.RenderingOptions.MarginLeft = 10;
                renderer.RenderingOptions.MarginRight = 10;
                renderer.RenderingOptions.MarginBottom = 10;

                // Choose screen or print CSS media
                renderer.RenderingOptions.CssMediaType = PdfCssMediaType.Print;

                // Render View to PDF document
                PdfDocument pdf = renderer.RenderRazorViewToPdf(_viewRenderService, "Views/Home/NCRs.cshtml", pagedNCRs);
                Response.Headers.Add("Content-Disposition", "inline");
                // Output PDF document
                return File(pdf.BinaryData, "application/pdf", "NCR Log.pdf");
            }
            return View(pagedNCRs);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // MAIN NCR LOG //
        public IActionResult List(string sortOrder, string searchString, string selectedSupplier, string selectedDate, bool? selectedStatus, int? page, string currentFilter, NCRStage? ncrStage)
        {
            ViewBag.FormattedIDSortParam = sortOrder == "FormattedID_Asc" ? "FormattedID_Desc" : "FormattedID_Asc";
            ViewBag.NCRStageSortParam = sortOrder == "NCRStage_Asc" ? "NCRStage_Desc" : "NCRStage_Asc";
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

            // Apply NCRStage filter if selected
            if (ncrStage.HasValue)
            {
                ncrs = ncrs.Where(x => x.NCR_Stage == ncrStage.Value);
            }

            // Search Box
            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();

                // Split the searchString to potentially match the "YYYY-NNN" format
                var parts = searchString.Split('-');
                int yearPart, idPart;

                ncrs = ncrs.Where(x =>
                    x.NCR_Date.ToString().ToLower().Contains(searchString) ||
                    x.Part.ProductNumber.ToString().ToLower().Contains(searchString) ||
                    x.ID.ToString().ToLower().Contains(searchString) ||
                    x.Part.Supplier.Name.ToLower().Contains(searchString) ||
                    (parts.Length == 2 &&
                     int.TryParse(parts[0], out yearPart) &&
                     int.TryParse(parts[1], out idPart) &&
                     x.NCR_Date.Year == yearPart &&
                     x.ID == idPart) // New condition for matching year and ID separately

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
                case "FormattedID_Asc":
                    sortedNCRs = ncrs.OrderBy(x => x.NCR_Date.Year).ThenBy(x => x.ID);
                    break;
                case "FormattedID_Desc":
                    sortedNCRs = ncrs.OrderByDescending(x => x.NCR_Date.Year).ThenByDescending(x => x.ID);
                    break;
                case "NCRStage_Asc":
                    sortedNCRs = ncrs.OrderBy(x => x.NCR_Stage);
                    break;
                case "NCRStage_Desc":
                    sortedNCRs = ncrs.OrderByDescending(x => x.NCR_Stage);
                    break;
                // Existing sort cases
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

            // Dropdown for NCRStages with specific stages and their display names
            var stagesToInclude = new[]
            {
                NCRStage.Engineering,
                NCRStage.Operations,
                NCRStage.Procurement,
                NCRStage.QualityRepresentative_Final
            };
            ViewBag.NCRStageList = ToSelectList(stagesToInclude);

            return View(pagedNCRs);
        }

        // ARCHIVED NCR LOG //
        public IActionResult ListArchive(string sortOrder, string searchString, string selectedSupplier, string selectedDate, bool? selectedStatus, int? page, string currentFilter, NCRStage? ncrStage)
        {
            ViewBag.FormattedIDSortParam = sortOrder == "FormattedID_Asc" ? "FormattedID_Desc" : "FormattedID_Asc";
            ViewBag.NCRStageSortParam = sortOrder == "NCRStage_Asc" ? "NCRStage_Desc" : "NCRStage_Asc";
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
                .Include(p => p.Supplier)
                .Include(p => p.Part)
                .ThenInclude(s => s.Supplier)
                .Include(p => p.Part.DefectLists)
                .ThenInclude(d => d.Defect)
                .ToList();

            var ncrs = _context.NCRs
                .Where(p => p.IsArchived == true)
                .Include(p => p.Supplier)
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
            //ViewBag.SelectedStatus = selectedStatus;

            // Apply NCRStage filter if selected
            if (ncrStage.HasValue)
            {
                ncrs = ncrs.Where(x => x.NCR_Stage == ncrStage.Value);
            }

            // Search Box
            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();

                // Split the searchString to potentially match the "YYYY-NNN" format
                var parts = searchString.Split('-');
                int yearPart, idPart;

                ncrs = ncrs.Where(x =>
                    x.NCR_Date.ToString().ToLower().Contains(searchString) ||
                    x.Part.ProductNumber.ToString().ToLower().Contains(searchString) ||
                    x.ID.ToString().ToLower().Contains(searchString) ||
                    x.Part.Supplier.Name.ToLower().Contains(searchString) ||
                    (parts.Length == 2 &&
                     int.TryParse(parts[0], out yearPart) &&
                     int.TryParse(parts[1], out idPart) &&
                     x.NCR_Date.Year == yearPart &&
                     x.ID == idPart) // New condition for matching year and ID separately

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
                case "FormattedID_Asc":
                    sortedNCRs = ncrs.OrderBy(x => x.NCR_Date.Year).ThenBy(x => x.ID);
                    break;
                case "FormattedID_Desc":
                    sortedNCRs = ncrs.OrderByDescending(x => x.NCR_Date.Year).ThenByDescending(x => x.ID);
                    break;
                case "NCRStage_Asc":
                    sortedNCRs = ncrs.OrderBy(x => x.NCR_Stage);
                    break;
                case "NCRStage_Desc":
                    sortedNCRs = ncrs.OrderByDescending(x => x.NCR_Stage);
                    break;
                // Existing sort cases
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

            // Dropdown for NCRStages with specific stages and their display names
            var stagesToInclude = new[]
            {
                NCRStage.Engineering,
                NCRStage.Operations,
                NCRStage.Procurement,
                NCRStage.QualityRepresentative_Final
            };
            ViewBag.NCRStageList = ToSelectList(stagesToInclude);

            return View(pagedNCRs);
        }

        public IActionResult ClearFilters()
        {
            return RedirectToAction("List");
        }

        #region Dashboard

        // For Dashboard
        public async Task<IActionResult> Index()
        {
            var ncrs = await _context.NCRs
                .Include(n => n.Supplier)
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

        public async Task<IActionResult> GetEngineeringStageCount()
        {
            var engineerStage = await _context.NCRs.CountAsync(n => n.NCR_Status && n.NCR_Date.Year == DateTime.Now.Year && n.NCR_Stage == NCRStage.Engineering);
            return Json(new { count = engineerStage });
        }

        public async Task<IActionResult> GetOperationsStageCount()
        {
            var operationsStage = await _context.NCRs.CountAsync(n => n.NCR_Status && n.NCR_Date.Year == DateTime.Now.Year && n.NCR_Stage == NCRStage.Operations);
            return Json(new { count = operationsStage });
        }

        public async Task<IActionResult> GetProcurementStageCount()
        {
            var procurementStage = await _context.NCRs.CountAsync(n => n.NCR_Status && n.NCR_Date.Year == DateTime.Now.Year && n.NCR_Stage == NCRStage.Procurement);
            return Json(new { count = procurementStage });
        }

        public async Task<IActionResult> GetQualityStageCount()
        {
            var qualityStage = await _context.NCRs.CountAsync(n => n.NCR_Status && n.NCR_Date.Year == DateTime.Now.Year && n.NCR_Stage == NCRStage.QualityRepresentative_Final);
            return Json(new { count = qualityStage });
        }

        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}

        private string GetDisplayName(Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            ?.GetName() ?? enumValue.ToString();
        }

        private SelectList ToSelectList<TEnum>(params TEnum[] filter) where TEnum : struct, Enum, IConvertible
        {
            var values = (filter == null || !filter.Any()) ? Enum.GetValues(typeof(TEnum)).Cast<Enum>() : filter.Cast<Enum>();
            var items = values.Select(value => new SelectListItem
            {
                Text = GetDisplayName(value),
                Value = value.ToString()
            }).ToList();

            return new SelectList(items, "Value", "Text");
        }
    }
}