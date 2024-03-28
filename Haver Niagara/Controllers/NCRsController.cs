using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Haver_Niagara.Data;
using Haver_Niagara.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Identity.Client;
using Microsoft.VisualStudio.Web.CodeGeneration;
using Microsoft.AspNetCore.Authorization;
using IronPdf.Extensions.Mvc.Core;
using IronPdf.Rendering;
using Microsoft.AspNetCore.Http;
using X.PagedList;
using Razor.Templating.Core;

namespace Haver_Niagara.Controllers
{
    [Authorize]
    public class NCRsController : Controller
    {
        private readonly HaverNiagaraDbContext _context;
        //Print PDF
        private readonly ILogger<HomeController> _logger;
        private readonly IRazorViewRenderer _viewRenderService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NCRsController(HaverNiagaraDbContext context, ILogger<HomeController> logger, IRazorViewRenderer viewRenderService, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            //Print PDF
            _logger = logger;
            _viewRenderService = viewRenderService;
            _httpContextAccessor = httpContextAccessor;
        }

        // Print PDF Details View
        public async Task<IActionResult> DetailsPrint(int? id)
        {
            if (id == null || _context.NCRs == null)
            {
                return NotFound();
            }

            var nCR = await _context.NCRs
                .Include(n => n.Supplier)
                .Include(n => n.Engineering)
                .Include(n => n.QualityInspection)
                .Include(n => n.QualityInspectionFinal)
                .Include(n => n.Part)
                    .ThenInclude(n => n.Supplier)
                .Include(n => n.Part)
                    .ThenInclude(n => n.Medias)
                .Include(n => n.Part)
                    .ThenInclude(n => n.DefectLists)
                    .ThenInclude(n => n.Defect)
                .Include(n => n.Operation)
                    .ThenInclude(n => n.FollowUp)
                .Include(n => n.Operation)
                    .ThenInclude(n => n.CAR)
                    .Include(n => n.Procurement)
                .FirstOrDefaultAsync(m => m.ID == id);

            //Print Details View to PDF
            if (_httpContextAccessor.HttpContext.Request.Method == HttpMethod.Post.Method)
            {
                //create html string from DetailsPrint razor view and data from nCR
                var html = await RazorTemplateEngine.RenderAsync("Views/NCRs/DetailsPrint.cshtml", nCR);

                ChromePdfRenderer renderer = new ChromePdfRenderer();

                //margins
                renderer.RenderingOptions.MarginTop = 10;
                renderer.RenderingOptions.MarginLeft = 10;
                renderer.RenderingOptions.MarginRight = 10;
                renderer.RenderingOptions.MarginBottom = 10;

                //Timeout and RenderDelay options
                //renderer.RenderingOptions.Timeout = 90; // seconds (default is 60)
                //renderer.RenderingOptions.WaitFor.RenderDelay(30000); // milliseconds

                //render pdf doc based on html string from razor view
                using var pdfDocument = renderer.RenderHtmlAsPdf(html);

                //// Use the FormattedID property to generate the file name
                string fileName = $"NCR_{nCR.FormattedID}.pdf";

                //output pdf
                return File(pdfDocument.BinaryData, "application/pdf", fileName);

                //return Results.File(pdfDocument.BinaryData, "application/pdf", "ncr.pdf");

                //// Choose screen or print CSS media
                //renderer.RenderingOptions.CssMediaType = PdfCssMediaType.Print;

                //// Render View to PDF document
                //PdfDocument pdf = renderer.RenderRazorViewToPdf(_viewRenderService, "Views/NCRs/DetailsPrint.cshtml", nCR);

                //Response.Headers.Add("Content-Disposition", "inline");
                //// Output PDF document
                //return File(pdf.BinaryData, "application/pdf", fileName);
            }

            return View(nCR);
        }

        // GET: NCRs
        public async Task<IActionResult> Index()
        {
            var haverNiagaraDbContext = _context.NCRs
                .Include(n => n.Engineering)
                .Include(n => n.Operation)
                .Include(n => n.QualityInspection)
                .Include(n => n.Part).ThenInclude(n => n.DefectLists).ThenInclude(n => n.Defect)
                .Include(n => n.Part).ThenInclude(n => n.Medias);
            return View(await haverNiagaraDbContext.ToListAsync());
        }

        // GET: NCRs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.NCRs == null)
            {
                return NotFound();
            }

            var nCR = await _context.NCRs
                .Include(n=> n.Supplier)
                .Include(n => n.Engineering)
                .Include(n => n.QualityInspection)
                .Include(n=>n.QualityInspectionFinal)
                .Include(n => n.Part)
                    .ThenInclude(n => n.Supplier)
                .Include(n => n.Part)
                    .ThenInclude(n => n.Medias)
                .Include(n => n.Part)
                    .ThenInclude(n => n.DefectLists)
                    .ThenInclude(n => n.Defect)
                .Include(n => n.Operation)
                    .ThenInclude(n => n.FollowUp)
                .Include(n => n.Operation)
                    .ThenInclude(n => n.CAR)
                    .Include(n=>n.Procurement)
                .FirstOrDefaultAsync(m => m.ID == id);
            
            //Seed Data, not ALL records have defects associated. 
            //For instance NCR #24 is not PART ID 24, therefore in the seed data

            if(nCR.NewNCRID != null) //Getting new ncr id if it exists to display it.
            {
                ViewBag.NewNCRID = nCR.NewNCRID;
            }

            if (nCR == null)
            {
                return NotFound();
            }

            return View(nCR);
        }

        // GET: NCRs/Create
        public IActionResult Create(int? oldNCRID)
        {
            ViewBag.DefectList = new SelectList(_context.Defects, "ID", "Name");

            // Populate supplier dropdown list
            ViewBag.SupplierID = new SelectList(_context.Suppliers, "ID", "Name"); 
                                                                                   
            //Passes in the OLD NCRID
            ViewBag.OldNCRID = oldNCRID;

            ViewData["EngineeringID"] = new SelectList(_context.Engineerings, "ID", "ID");
            ViewData["OperationID"] = new SelectList(_context.Operations, "ID", "ID");
            ViewData["PartID"] = new SelectList(_context.Parts, "ID", "ID");
            ViewData["QualityInspectionID"] = new SelectList(_context.QualityInspections, "ID", "ID");
            return View();
        }

        // POST: NCRs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]              
     
        public async Task<IActionResult> Create(int? oldNCRID,[Bind("ID,NCR_Date,NCR_Status,NCR_Stage,OldNCRID,NCRSupplierID")]
                NCR nCR, Part part, QualityInspection qualityInspection, List<IFormFile> files, List<string> links, int SelectedDefectID)
        {
            if (!ModelState.IsValid)
            {
                // Populate supplier dropdown list
                ViewBag.DefectList = new SelectList(_context.Defects, "ID", "Name",SelectedDefectID);
                ViewBag.SupplierID = new SelectList(_context.Suppliers, "ID", "Name",nCR.NCRSupplierID);
                ViewBag.SelectedDefectID = SelectedDefectID;
                return View(nCR);
            }
            if (ModelState.IsValid)
            {
                _context.Add(part);                         
                _context.Add(qualityInspection);          
                part.SupplierID = (int)nCR.NCRSupplierID;

                await _context.SaveChangesAsync();
                
                //Assign the generated IDs to this NCR. this allows the NCR to have part and quality
                nCR.PartID = part.ID;                  
                nCR.QualityInspectionID = qualityInspection.ID;

                //Retrieves the old ncr from the GET create. 
                nCR.OldNCRID = oldNCRID ?? null;
                var defectList = new DefectList            
                {                                          
                    PartID = part.ID,                      
                    DefectID = SelectedDefectID
                };
                _context.Add(defectList);               
                _context.Add(nCR);
                await _context.SaveChangesAsync();

                if (oldNCRID != null)
                {   
                    var oldNCR = await _context.NCRs.FirstOrDefaultAsync(n => n.ID == oldNCRID);
                    if (oldNCR != null)
                    {
                        oldNCR.NewNCRID = nCR.ID; 
                        _context.Update(oldNCR);
                        await _context.SaveChangesAsync();
                    }
                }
                //Images function to save
                if (files != null && files.Count > 0)             
                {                                                  
                    await OnPostUploadAsync(files, nCR.ID, links);  
                }
                TempData["CreateSuccessMsg"] = $"<a href='{Url.Action("Details", "NCRs", new { id = nCR.ID })}'>Click Here To View: {nCR.FormattedID}</a>";
                return RedirectToAction("List", "Home");           
            }
            ViewBag.DefectList = new SelectList(_context.Defects, "ID", "Name");
            return View(nCR);
        }

      
        
        
        // GET: NCRs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.NCRs == null)
            {
                return NotFound();
            }
            //Get all the NCR data from the database that is going to be edited.
            var nCR = await _context.NCRs
                .Include(n=> n.Supplier)
                .Include(n => n.Part)
                    .ThenInclude(n => n.Medias)
                .Include(n => n.Part)
                    .ThenInclude(n => n.Supplier)
                .Include(n => n.Part)
                    .ThenInclude(n => n.DefectLists)
                        .ThenInclude(n => n.Defect)
                .Include(n => n.QualityInspection)
                .Include(n=>n.QualityInspectionFinal)
                .Include(n => n.Engineering)

                .Include(n => n.Operation)
                    .ThenInclude(n => n.CAR)
                .Include(n => n.Operation)
                    .ThenInclude(n => n.FollowUp)
                    .Include(p=>p.Procurement)
                .FirstOrDefaultAsync(n => n.ID == id);

            if (nCR == null)
            {
                return NotFound();
            }
            //Populate list of defects and suppliers
            ViewBag.DefectList = new SelectList(_context.Defects, "ID", "Name");
            ViewBag.listOfSuppliers = new SelectList(_context.Suppliers, "ID", "Name");
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "ID", "Name");
            return View(nCR);
        }


        // POST: NCRs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NCR_Date,NCR_Status,OldNCRID, NCRSupplierID")] //ids have to be included here and in the edit (hidden but MUST be there so the database gives the right id to right NCR.)
                    NCR nCR, Part part, QualityInspection qualityInspection, Engineering engineering, Operation operation, Procurement procurement, QualityInspectionFinal qualityInspectionFinal,
                    List<IFormFile> files, List<string> links, int SelectedDefectID)
        {
            nCR.ID = id;
            if (id != nCR.ID)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    var existingNCR = await _context.NCRs
                        .Include(n => n.Part)
                            .ThenInclude(n => n.Medias)
                        .Include(n => n.Part)
                            .ThenInclude(n => n.Supplier)
                        .Include(n => n.Part)
                            .ThenInclude(n => n.DefectLists)
                                .ThenInclude(n => n.Defect)
                        .Include(n => n.QualityInspection)
                        .Include(n=>n.QualityInspectionFinal)
                        .Include(n => n.Engineering)
                        .Include(n => n.Operation)
                            .ThenInclude(n => n.FollowUp)
                        .Include(n => n.Operation)
                            .ThenInclude(n => n.CAR)
                            .Include(n=>n.Procurement)
                        .FirstOrDefaultAsync(n => n.ID == id);

                    if (existingNCR == null)
                    {
                        return NotFound();
                    }
   
                    existingNCR.NCR_Date = nCR.NCR_Date;
                    existingNCR.NCR_Status = nCR.NCR_Status;    //For radio buttons 
                    existingNCR.NCRSupplierID = nCR.NCRSupplierID;

                    if(existingNCR.OldNCRID != null)
                    {
                        existingNCR.OldNCRID = nCR.ID;
                    }

                    //Stores the ID Automatically
                    _context.Update(existingNCR);   //added for safe measures..if other edit properties break try removing this!
                                                    //Updating Part Properties

                    if (part != null)
                    {
                        if(existingNCR.Part == null)
                        {
                            existingNCR.Part = new Part();
                        }
                        existingNCR.Part.Name = part.Name;
                        existingNCR.Part.PartNumber = part.PartNumber;
                        existingNCR.Part.SAPNumber = part.SAPNumber;
                        existingNCR.Part.PurchaseNumber = part.PurchaseNumber;
                        existingNCR.Part.SalesOrder = part.SalesOrder;
                        existingNCR.Part.ProductNumber = part.ProductNumber;
                        existingNCR.Part.QuantityRecieved = part.QuantityRecieved;
                        existingNCR.Part.QuantityDefect = part.QuantityDefect;
                        existingNCR.Part.Description = part.Description;
                        existingNCR.Part.SupplierID = (int)existingNCR.NCRSupplierID;

            
                        //existingNCR.Part.SupplierID = part.SupplierID; //crashes after saving changes because of this line
                        await _context.SaveChangesAsync();
                        existingNCR.Part.DefectLists.Clear(); //removed existing defect 
                        try
                        {
                            if (SelectedDefectID != 0)
                            {
                                // Find the selected Defect from the database
                                Defect selectedDefect = await _context.Defects.FindAsync(SelectedDefectID);

                                if (selectedDefect != null)
                                {
                                    // Create a new DefectList instance
                                    var newDefectList = new DefectList
                                    {
                                        PartID = existingNCR.Part.ID,
                                        DefectID = selectedDefect.ID
                                    };

                                    _context.DefectLists.Add(newDefectList);
                                    await _context.SaveChangesAsync();

                                    // Add the new DefectList to the Part's DefectLists collection
                                    existingNCR.Part.DefectLists.Add(newDefectList);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            // Log the exception or print it to the console for debugging purposes
                            Console.WriteLine(ex.Message);

                            // Check for inner exceptions
                            Exception innerException = ex.InnerException;
                            while (innerException != null)
                            {
                                Console.WriteLine("Inner Exception: " + innerException.Message);
                                innerException = innerException.InnerException;
                            }
                        }

                    }
                    if (qualityInspection != null)
                    {
                        if(existingNCR.QualityInspection == null)
                        {
                            existingNCR.QualityInspection = new QualityInspection();
                        }
                        existingNCR.QualityInspection.Name = qualityInspection.Name;
                        existingNCR.QualityInspection.Date = qualityInspection.Date;
                        existingNCR.QualityInspection.QualityIdentify = qualityInspection.QualityIdentify;
                        existingNCR.QualityInspection.ItemMarked = qualityInspection.ItemMarked;
                    }
                    if (engineering != null)
                    {
                        if(existingNCR.Engineering == null)
                        {
                            existingNCR.Engineering = new Engineering();
                        }
                        existingNCR.Engineering.Name = engineering.Name;
                        existingNCR.Engineering.Date = engineering.Date;
                        existingNCR.Engineering.CustomerNotify = engineering.CustomerNotify;
                        existingNCR.Engineering.DrawUpdate = engineering.DrawUpdate;
                        existingNCR.Engineering.DispositionNotes = engineering.DispositionNotes;
                        existingNCR.Engineering.RevisionOriginal = engineering.RevisionOriginal;
                        existingNCR.Engineering.RevisionUpdated = engineering.RevisionUpdated;
                        existingNCR.Engineering.RevisionDate = engineering.RevisionDate;
                        existingNCR.Engineering.EngineeringDisposition = engineering.EngineeringDisposition;

                        //Temporary If These Inputs Are Filled, then change NCR Status to Indicate that the Engineering Section is done
                        //Compliments AJAX code that checks for NCR status //only 2 inputs to not mess up existing ncrs
                        if (!string.IsNullOrEmpty(engineering.Name) && engineering.Date != DateTime.MinValue && engineering.EngineeringDisposition.ToString() != null)
                        {
                            existingNCR.NCR_Stage = NCRStage.Operations;
                        }

                    }
                    if (operation != null)
                    {
                        if(existingNCR.Operation == null)
                        {
                            existingNCR.Operation = new Operation();
                        }
                        existingNCR.Operation.Name = operation.Name;
                        existingNCR.Operation.OperationDate = operation.OperationDate;
                        existingNCR.Operation.OperationDecision = operation.OperationDecision;
                        existingNCR.Operation.OperationNotes = operation.OperationNotes;
                        existingNCR.Operation.OperationCar = operation.OperationCar;
                        existingNCR.Operation.OperationFollowUp = operation.OperationFollowUp;
                        //Updating Follow Up if Not NUll
                        if (operation.FollowUp != null) //means that the the user passes back a true/yes for a follow up
                        {
                            if (operation.OperationFollowUp)
                            {
                                //If they choose follow up, and there is no Follow Up Object in the NCR it will crash so we have to assign one
                                if (existingNCR.Operation.FollowUp == null)
                                {
                                    existingNCR.Operation.FollowUp = new FollowUp(); //create new folloow up
                                }
                                if (operation.FollowUp.FollowUpDate != DateTime.MinValue) //if date isnt null
                                {
                                    existingNCR.Operation.FollowUp.FollowUpDate = operation.FollowUp.FollowUpDate; //assign it
                                }
                                if (!string.IsNullOrEmpty(operation.FollowUp.FollowUpType)) //if follow up type is NOT empty
                                {
                                    existingNCR.Operation.FollowUp.FollowUpType = operation.FollowUp.FollowUpType; //assign it
                                }
                            }
                            else
                            {
                                existingNCR.Operation.FollowUp = null;
                            }
                        }
                        //Updating CAR if user says yes car required
                        if (operation.CAR != null)
                        {
                            if (operation.OperationCar) //yes / no checkbox // if yes do all of these
                            {
                                //create and assign new car to existing NCR
                                if (existingNCR.Operation.CAR == null)
                                {
                                    existingNCR.Operation.CAR = new CAR();

                                }
                                if (operation.CAR.CARNumber != null) //if not null assign
                                {
                                    existingNCR.Operation.CAR.CARNumber = operation.CAR.CARNumber;
                                }
                                if (operation.CAR.Date != DateTime.MinValue)
                                {
                                    existingNCR.Operation.CAR.Date = operation.CAR.Date;
                                }
                            }
                            else //else remove car. 
                            {
                                existingNCR.Operation.CAR = null;
                            }
                        }
                        //Checking to see if inputs are filled out(could do more but as long as the name property is filled out it wont affect
                        if(!string.IsNullOrEmpty(operation.Name) && operation.OperationDate != DateTime.MinValue) //existing ncrs as well as date.
                        {
                            existingNCR.NCR_Stage = NCRStage.Procurement;
                        }
                    }
                    if(procurement != null)
                    {
                        if(existingNCR.Procurement == null)
                        {
                            existingNCR.Procurement = new Procurement();
                        }

                        if(procurement.ReturnRejected == true)
                        {
                            existingNCR.Procurement.ReturnRejected = procurement.ReturnRejected;
                            existingNCR.Procurement.RMANumber = procurement.RMANumber;
                            existingNCR.Procurement.CarrierName = procurement.CarrierName;
                            existingNCR.Procurement.CarrierPhone = procurement.CarrierPhone;
                            existingNCR.Procurement.AccountNumber = procurement.AccountNumber;
                            existingNCR.Procurement.DisposeOnSite = procurement.DisposeOnSite;
                        }
                        else
                        {
                            existingNCR.Procurement.ReturnRejected = false;
                            existingNCR.Procurement.RMANumber = null;
                            existingNCR.Procurement.CarrierName = null;
                            existingNCR.Procurement.CarrierPhone = null;
                            existingNCR.Procurement.AccountNumber = null;
                            existingNCR.Procurement.DisposeOnSite = false;
                        }
                        existingNCR.Procurement.ToReceiveDate = procurement.ToReceiveDate;
                        existingNCR.Procurement.SuppReturnCompletedSAP = procurement.SuppReturnCompletedSAP;
                        existingNCR.Procurement.ExpectSuppCredit = procurement.ExpectSuppCredit;
                        existingNCR.Procurement.BillSupplier = procurement.BillSupplier;

                        if (procurement.ReturnRejected == true || procurement.ReturnRejected == false)//as long as its checked then change it
                        {
                            existingNCR.NCR_Stage = NCRStage.QualityRepresentative_Final;
                        }
                    }
                    if (qualityInspectionFinal != null)
                    {
                        if (existingNCR.QualityInspectionFinal == null)
                        {
                            existingNCR.QualityInspectionFinal = new QualityInspectionFinal();
                        }
                        existingNCR.QualityInspectionFinal.Department = qualityInspectionFinal.Department;
                        existingNCR.QualityInspectionFinal.DepartmentDate = qualityInspectionFinal.DepartmentDate;
                        existingNCR.QualityInspectionFinal.InspectorName = qualityInspectionFinal.InspectorName;
                        existingNCR.QualityInspectionFinal.InspectorDate = qualityInspectionFinal.InspectorDate;
                        existingNCR.QualityInspectionFinal.ReInspected = qualityInspectionFinal.ReInspected;
                    }



                    if (existingNCR.NCR_Status) //If yes the NCR is being kept open
                    {
                        if (!qualityInspectionFinal.ReInspected) //if this is false (no) then redirect to Create
                        {
                            return RedirectToAction("Create", new { oldNCRID = id });
                        }
                    }
                    if (!existingNCR.NCR_Status) //if no, dont keep ncr open
                    {
                        existingNCR.NCR_Stage = NCRStage.Closed_NCR; //set stage to complete
                        _context.Update(existingNCR); //save changes
                        if (!qualityInspectionFinal.ReInspected)
                        {
                            return RedirectToAction("Create", new { oldNCRID = id });
                        }
                    }
                    await OnPostUploadAsync(files, nCR.ID, links);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NCRExists(nCR.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["EditSuccessMsg"] = $"<a href='{Url.Action("Details", "NCRs", new { id = nCR.ID })}'>Click Here to View: {nCR.FormattedID}</a>";
                return RedirectToAction("List", "Home");
            }
            //Populate viewbag for list of suppliers
            ViewBag.listOfSuppliers = new SelectList(_context.Suppliers, "ID", "Name");
            ViewBag.DefectList = new SelectList(_context.Defects, "ID", "Name");
            ViewData["EngineeringID"] = new SelectList(_context.Engineerings, "ID", "ID", nCR.EngineeringID);
            ViewData["OperationID"] = new SelectList(_context.Operations, "ID", "ID", nCR.OperationID);
            ViewData["PartID"] = new SelectList(_context.Parts, "ID", "ID", nCR.PartID);
            ViewData["QualityInspectionID"] = new SelectList(_context.QualityInspections, "ID", "ID", nCR.QualityInspectionID);
            return View(nCR);
        }





        ///Quality Representative Edit GET:
        public async Task<IActionResult> QualityRepresentativeEdit(int? id)
        {
            if (id == null || _context.NCRs == null)
                return NotFound();

            var nCR = await _context.NCRs
                .Include(n => n.Supplier)
                .Include(n => n.QualityInspection)
                .Include(n => n.QualityInspectionFinal)
                .Include(n => n.Part).ThenInclude(n => n.Supplier)
                .Include(n => n.Part).ThenInclude(n => n.Medias)
                .Include(n => n.Part).ThenInclude(n => n.DefectLists).ThenInclude(n => n.Defect)
                .Include(n => n.Operation).ThenInclude(n => n.FollowUp)
                .Include(n => n.Operation).ThenInclude(n => n.CAR)
                .Include(n => n.Procurement)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (nCR == null)
                return NotFound();

            ViewBag.DefectList = new SelectList(_context.Defects, "ID", "Name");
            ViewBag.listOfSuppliers = new SelectList(_context.Suppliers, "ID", "Name");
            //ViewData["SupplierID"] = new SelectList(_context.Suppliers, "ID", "Name");
            ViewBag.SupplierID = new SelectList(_context.Suppliers, "ID", "Name");
            return View(nCR);
        }

        ///Quality Representative Edit : POST 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> QualityRepresentativeEdit(int id, [Bind("NCR_Date,NCR_Status,OldNCRID, NCRSupplierID")] 
                          NCR nCR, Part part, QualityInspection qualityInspection, QualityInspectionFinal qualityInspectionFinal,
                          List<IFormFile> files, List<string> links, int SelectedDefectID)
        {
            nCR.ID = id;
            if (id != nCR.ID)
                return NotFound();

            if (!ModelState.IsValid) //If it posts back here and its incorrect, give them back the data (ddls so they arent unsaved)
            {
                // Populate supplier dropdown list
                ViewBag.DefectList = new SelectList(_context.Defects, "ID", "Name", SelectedDefectID);
                ViewBag.SupplierID = new SelectList(_context.Suppliers, "ID", "Name", nCR.NCRSupplierID);
                ViewBag.SelectedDefectID = SelectedDefectID;
                return View(nCR);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingNCR = await _context.NCRs
                        .Include(n => n.Part).ThenInclude(n => n.Medias)
                        .Include(n => n.Part).ThenInclude(n => n.Supplier)
                        .Include(n => n.Part).ThenInclude(n => n.DefectLists).ThenInclude(n => n.Defect)
                        .Include(n => n.QualityInspection)
                        .Include(n => n.QualityInspectionFinal)
                        .Include(n => n.Engineering)
                        .FirstOrDefaultAsync(n => n.ID == id);

                    if (existingNCR == null)
                        return NotFound();

                    existingNCR.NCR_Date = nCR.NCR_Date;
                    existingNCR.NCR_Status = nCR.NCR_Status;    
                    existingNCR.NCRSupplierID = nCR.NCRSupplierID;

                    if (existingNCR.OldNCRID != null)
                        existingNCR.OldNCRID = nCR.ID;

                    //Stores the ID Automatically
                    _context.Update(existingNCR);   
                                                   
                    if (part != null)
                    {
                        if (existingNCR.Part == null)
                        {
                            existingNCR.Part = new Part();
                        }
                        existingNCR.Part.Name = part.Name;
                        existingNCR.Part.PartNumber = part.PartNumber;
                        existingNCR.Part.SAPNumber = part.SAPNumber;
                        existingNCR.Part.PurchaseNumber = part.PurchaseNumber;
                        existingNCR.Part.SalesOrder = part.SalesOrder;
                        existingNCR.Part.ProductNumber = part.ProductNumber;
                        existingNCR.Part.QuantityRecieved = part.QuantityRecieved;
                        existingNCR.Part.QuantityDefect = part.QuantityDefect;
                        existingNCR.Part.Description = part.Description;
                        existingNCR.Part.SupplierID = (int)existingNCR.NCRSupplierID;
      
                        await _context.SaveChangesAsync();
                        existingNCR.Part.DefectLists.Clear();  
                        try
                        {
                            if (SelectedDefectID != 0)
                            {
                                // Find the selected Defect from the database
                                Defect selectedDefect = await _context.Defects.FindAsync(SelectedDefectID);
                                if (selectedDefect != null)
                                {
                                    var newDefectList = new DefectList
                                    {
                                        PartID = existingNCR.Part.ID,
                                        DefectID = selectedDefect.ID
                                    };
                                    _context.DefectLists.Add(newDefectList);
                                    await _context.SaveChangesAsync();
                                    existingNCR.Part.DefectLists.Add(newDefectList);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            // Check for inner exceptions
                            Exception innerException = ex.InnerException;
                            while (innerException != null)
                            {
                                Console.WriteLine("Inner Exception: " + innerException.Message);
                                innerException = innerException.InnerException;
                            }
                        }

                    }
                    if (qualityInspection != null)
                        if (existingNCR.QualityInspection == null)
                            existingNCR.QualityInspection = new QualityInspection();

                        existingNCR.QualityInspection.Name = qualityInspection.Name;
                        existingNCR.QualityInspection.Date = qualityInspection.Date;
                        existingNCR.QualityInspection.QualityIdentify = qualityInspection.QualityIdentify;
                        existingNCR.QualityInspection.ItemMarked = qualityInspection.ItemMarked;


                    //So if the NCR being edited is on the Quality Representative stage, allow it to be updated with information
                    if (existingNCR.NCR_Stage == NCRStage.QualityRepresentative_Final)
                        if (qualityInspectionFinal != null)
                            if (existingNCR.QualityInspectionFinal == null)
                            {
                                existingNCR.QualityInspectionFinal = new QualityInspectionFinal();
                            }
                            existingNCR.QualityInspectionFinal.Department = qualityInspectionFinal.Department;
                            existingNCR.QualityInspectionFinal.DepartmentDate = qualityInspectionFinal.DepartmentDate;
                            existingNCR.QualityInspectionFinal.InspectorName = qualityInspectionFinal.InspectorName;
                            existingNCR.QualityInspectionFinal.InspectorDate = qualityInspectionFinal.InspectorDate;
                            existingNCR.QualityInspectionFinal.ReInspected = qualityInspectionFinal.ReInspected;
                    
                    //If the NCR is being kept open 
                    if (existingNCR.NCR_Status) 
                        if (!qualityInspectionFinal.ReInspected)   //And re-inspect was not acceptable then redirect and create an NCR with an Old ID attached to it
                            return RedirectToAction("Create", new { oldNCRID = id });

                    if (!existingNCR.NCR_Status) //if no, dont keep ncr open
                    {
                        existingNCR.NCR_Stage = NCRStage.Closed_NCR; //set stage to complete
                        _context.Update(existingNCR); 
                        if (!qualityInspectionFinal.ReInspected)
                            return RedirectToAction("Create", new { oldNCRID = id });
                    }
                    await OnPostUploadAsync(files, nCR.ID, links);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NCRExists(nCR.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["EditSuccessMsg"] = $"<a href='{Url.Action("Details", "NCRs", new { id = nCR.ID })}'>Click Here to View: {nCR.FormattedID}</a>";
                return RedirectToAction("List", "Home");
            }
            ViewBag.DefectList = new SelectList(_context.Defects, "ID", "Name");
            ViewData["PartID"] = new SelectList(_context.Parts, "ID", "ID", nCR.PartID);
            ViewData["QualityInspectionID"] = new SelectList(_context.QualityInspections, "ID", "ID", nCR.QualityInspectionID);
            return View(nCR);

        }
























        // GET: NCRs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.NCRs == null)
            {
                return NotFound();
            }

            var nCR = await _context.NCRs
                .Include(n => n.Engineering)
                .Include(n => n.Operation)
                .Include(n => n.QualityInspection)
                .Include(n => n.Part)
                    .ThenInclude(n => n.Medias)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (nCR == null)
            {
                return NotFound();
            }

            return View(nCR);
        }



        // POST: NCRs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.NCRs == null)
            {
                return Problem("Entity set 'HaverNiagaraDbContext.NCRs'  is null.");
            }
            var nCR = await _context.NCRs.FindAsync(id);
            if (nCR != null)
            {
                _context.NCRs.Remove(nCR);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("List", "Home");
            //return RedirectToAction(nameof(Index));
        }
        //https://learn.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-8.0

        //Create methods that return each SelectList separately and one method to put them all into ViewData
        //This approach allows for AJAX requests to refresh DDL Data at a later date 
        //https://www.youtube.com/watch?v=JnOKiME3guQ&list=PL16MVmKNvI0JTGdka0_MykIhKX1S-gfli&index=69
        //14:19
        private SelectList SupplierSelectList(int? selectedId)
        {
            return new SelectList(_context.Suppliers
                .OrderBy(s => s.Name), "ID", "Name", selectedId);       //retrieves suppliers and orders them by their name?
        }

        private SelectList DefectSelectList(int? selectedDefectID)
        {
            return new SelectList(_context.Defects
                .OrderBy(s => s.Name), "ID", "Name", selectedDefectID);
        }

        private void PopulateDropDownLists(NCR ncr = null)
        {
            //Sets the view data for supplierID from the method which retrieves it
            ViewData["SupplierID"] = SupplierSelectList(ncr?.NCRSupplierID);

            //check to see if it exists 
            if(ncr.Part != null && ncr.Part.DefectLists != null)
            {
                var defectID = ncr.Part.DefectLists.Select(dl => dl.DefectID);

                //Pass the defect IDS..
                ViewData["DefectID"] = DefectSelectList(defectID.FirstOrDefault());
            }
            else
            {
                //if null then didnt work
                ViewData["DefectID"] = DefectSelectList(null);
            }
        }

        //add JsonResult GetSuppliers to return a new copy of the SelectList for SupplierID
        [HttpGet]
        public JsonResult GetSuppliers(int? id)
        {
            return Json(SupplierSelectList(id));
        }

        [HttpGet]
        public JsonResult GetDefects(int? id)
        {
            return Json(DefectSelectList(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostUploadAsync(List<IFormFile> files, int ncrID, List<string> links) //Accepting multiple Iformfiles, and an NCR ID to relate data
        {                                                                   //added ability to take in multiple links as well.
            var ncr = await _context.NCRs
                .Include(n => n.Part)
                    .ThenInclude(n => n.Medias)
                .FirstOrDefaultAsync(n => n.ID == ncrID);

            if (ncr == null)
            {
                return NotFound();
            }

            long size = files.Sum(f => f.Length);

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var filePath = Path.GetTempFileName();

                    using (var stream = new MemoryStream())
                    {
                        await formFile.CopyToAsync(stream);

                        var media = new Media
                        {
                            Content = stream.ToArray(),
                            MimeType = formFile.ContentType,
                            Description = formFile.FileName
                        };

                        ncr.Part.Medias.Add(media);
                    }
                }
            }

            //dorian !
            //ai prompt - how can i get the links and split them so theyre put into arrays and saved
            //applied here to save links to media
            if (links != null)
            {
                foreach (var linkGroup in links)
                {
                    if (linkGroup != null)
                    {
                        foreach (var link in linkGroup.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            if (!string.IsNullOrEmpty(link))
                            {
                                var media = new Media
                                {
                                    Links = link.Trim() // Trim whitespace from each link
                                };
                                ncr.Part.Medias.Add(media);
                            }
                        }
                    }
                }
            }
            await _context.SaveChangesAsync();

            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { count = files.Count, size });
        }


        [HttpPost]
        public async Task<IActionResult> RemoveImage(int ncrID, int imageID)
        {
            //Getting NCRs
            var ncr = await _context.NCRs
                    .Include(n => n.Part)
                        .ThenInclude(n => n.Medias)
                    .FirstOrDefaultAsync(n => n.ID == ncrID);

            if (ncr != null && ncr.Part != null)
            {
                //Since a user can only delete one image at a time, we dont have to worry about looping through images in media
                var imageToRemove = ncr.Part.Medias.FirstOrDefault(n => n.ID == imageID);
                if (imageToRemove != null)
                {
                    _context.Medias.Remove(imageToRemove);  //Removes the image retrieved
                    await _context.SaveChangesAsync(); //saves changes. forgot to add. i spent so long trying to figure out WHY it wasnt saving. 
                    return Ok();
                }
            }
            return NotFound();  //else not found
        }

        [HttpPost]
        public async Task<IActionResult> RemoveLink(int ncrID, int linkID) //Could have probably made this into one function, if time will do later. *FIX*
        {
            //Getting NCRs
            var ncr = await _context.NCRs
                    .Include(n => n.Part)
                        .ThenInclude(n => n.Medias)
                    .FirstOrDefaultAsync(n => n.ID == ncrID);

            if (ncr != null && ncr.Part != null)
            {
                //Since a user can only delete one image at a time, we dont have to worry about looping through images in media
                var linkToRemove = ncr.Part.Medias.FirstOrDefault(n => n.ID == linkID);
                if (linkToRemove != null)
                {
                    _context.Medias.Remove(linkToRemove);  //Removes the image retrieved
                    await _context.SaveChangesAsync(); //saves changes. forgot to add. i spent so long trying to figure out WHY it wasnt saving. 
                    return Ok();
                }
            }
            return NotFound();

        }



        private bool NCRExists(int id)
        {
            return _context.NCRs.Any(e => e.ID == id);
        }
    }
}
