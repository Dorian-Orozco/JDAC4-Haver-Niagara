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
using Haver_Niagara.Utilities;
using Microsoft.AspNetCore.Identity;
using Haver_Niagara.ViewModels;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMyEmailSender _emailSender;

        public NCRsController(HaverNiagaraDbContext context, ILogger<HomeController> logger, IRazorViewRenderer viewRenderService, IHttpContextAccessor httpContextAccessor,
                                UserManager<IdentityUser> userManager, IMyEmailSender emailSender)
        {
            _context = context;
            //Print PDF
            _logger = logger;
            _viewRenderService = viewRenderService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _emailSender = emailSender;
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

                // Choose screen or print CSS media
                //renderer.RenderingOptions.CssMediaType = PdfCssMediaType.Print;

                //render pdf doc based on html string from razor view
                using var pdfDocument = renderer.RenderHtmlAsPdf(html);

                //// Use the FormattedID property to generate the file name
                string fileName = $"NCR_{nCR.FormattedID}.pdf";

                //Response.Headers.Add("Content-Disposition", "inline");

                //output pdf
                return File(pdfDocument.BinaryData, "application/pdf", fileName);

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

            if (nCR.NewNCRID != null) //Getting new ncr id if it exists to display it.
            {
                ViewBag.NewNCRID = nCR.NewNCRID;
            }

            if (nCR == null)
            {
                return NotFound();
            }
            //await RemindUsers(id.Value); //this kept sending emails so instead i need to hook it up to the mark completed button so it can send emails.

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

        public async Task<IActionResult> Create(int? oldNCRID, [Bind("ID,NCR_Date,NCR_Status,NCR_Stage,OldNCRID,NCRSupplierID")]
                NCR nCR, Part part, QualityInspection qualityInspection, List<IFormFile> files, List<string> links, int SelectedDefectID)
        {
            if (!ModelState.IsValid)
            {
                // Populate supplier dropdown list
                ViewBag.DefectList = new SelectList(_context.Defects, "ID", "Name", SelectedDefectID);
                ViewBag.SupplierID = new SelectList(_context.Suppliers, "ID", "Name", nCR.NCRSupplierID);
                ViewBag.SelectedDefectID = SelectedDefectID;
                return View(nCR);
            }
            if (ModelState.IsValid)
            {
                _context.Add(part);
                _context.Add(qualityInspection);
                part.SupplierID = (int)nCR.NCRSupplierID;

                await _context.SaveChangesAsync();

                //When creating a new record set default of engineering here
                nCR.NCR_Stage = NCRStage.Engineering;
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


                //So since the Create proccess only occurs once we can send a email here 
                //Find all employees in the engineering role, then send them emails. 
                var usersInEngineering = await _userManager.GetUsersInRoleAsync("Engineer");
                EmailMessage emailMessage = new EmailMessage
                {
                    Subject = $"New NCR Has Been Created #{nCR.FormattedID}!",
                    Content = $"<p>Dear Engineers,</p>" +
                              $"<p>A new Non-Conformance Report (NCR) has been created.</p>" +
                              $"<p>Please review and fill out your part of the form.</p>"+
                              $"<p>Thank you!</p>"
                };
                foreach (var user in usersInEngineering)
                {
                    if (user.Email == "engineer@outlook.com")
                        continue;
                    emailMessage.ToAddresses.Add(new EmailAddress { Name = user.UserName, Address = user.Email });
                }
                //iF we do not provide an email address it will not work, so put in your email here to test
                emailMessage.ToAddresses.Add(new EmailAddress { Name = "Dorian", Address = "dorianCodeDemo@outlook.com" });

                await _emailSender.SendToManyAsync(emailMessage);  //uncomment for email to work, MAKE SURE you dont email engineer so disable their account.

                return RedirectToAction("List", "Home");
            }
            ViewBag.DefectList = new SelectList(_context.Defects, "ID", "Name");
            return View(nCR);
        }




        // GET: NCRs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.NCRs == null)
                return NotFound();
            //Get all the NCR data from the database that is going to be edited.
            var nCR = await _context.NCRs
                .Include(n => n.Supplier)
                .Include(n => n.Part).ThenInclude(n => n.Medias)
                .Include(n => n.Part).ThenInclude(n => n.Supplier)
                .Include(n => n.Part).ThenInclude(n => n.DefectLists).ThenInclude(n => n.Defect)
                .Include(n => n.QualityInspection)
                .Include(n => n.QualityInspectionFinal)
                .Include(n => n.Engineering)
                .Include(n => n.Operation).ThenInclude(n => n.CAR)
                .Include(n => n.Operation).ThenInclude(n => n.FollowUp)
                .Include(p => p.Procurement)
                .FirstOrDefaultAsync(n => n.ID == id);

            if (nCR == null)
                return NotFound();
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

            var ncrRetrieveStage = _context.NCRs
                .FirstOrDefaultAsync(n => n.ID == id);
            
           

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
                        .Include(n => n.QualityInspectionFinal)
                        .Include(n => n.Engineering)
                        .Include(n => n.Operation)
                            .ThenInclude(n => n.FollowUp)
                        .Include(n => n.Operation)
                            .ThenInclude(n => n.CAR)
                            .Include(n => n.Procurement)
                        .FirstOrDefaultAsync(n => n.ID == id);

                    if (existingNCR == null)
                    {
                        return NotFound();
                    }

                    existingNCR.NCR_Date = nCR.NCR_Date;
                    existingNCR.NCR_Status = nCR.NCR_Status;    //For radio buttons 
                    existingNCR.NCRSupplierID = nCR.NCRSupplierID;

                    if (existingNCR.OldNCRID != null)
                    {
                        existingNCR.OldNCRID = nCR.ID;
                    }

                    //Stores the ID Automatically
                    _context.Update(existingNCR);   //added for safe measures..if other edit properties break try removing this!
                                                    //Updating Part Properties

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
                        if (existingNCR.QualityInspection == null)
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
                        if (existingNCR.Engineering == null)
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
                        if (existingNCR.Operation == null)
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
                        if (!string.IsNullOrEmpty(operation.Name) && operation.OperationDate != DateTime.MinValue) //existing ncrs as well as date.
                        {
                            existingNCR.NCR_Stage = NCRStage.Procurement;
                        }
                    }
                    if (procurement != null)
                    {
                        if (existingNCR.Procurement == null)
                        {
                            existingNCR.Procurement = new Procurement();
                        }

                        if (procurement.ReturnRejected == true)
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



        //Stage system, retrieve the part of the ncr that is being edited, use the ncr id and see if there is data filled out after their section NOT BEFORe
        //because if you create an NCR thats automatically the quality rep stage but once you submit the form it turns into Engineering.
        //So what if someone goes back to change that NCR quality rep info if engineering data already exists? It will overwrite the stage and mess things up
        //So Retrieve the NCR, look for associated objects (engineering table, procurement, etc) if engineering exists, stage = procurement, if procurement exists, stage = qual rep final 
        //if qual rep exists and ncr close selected then stage = complete.


        ///Quality Representative Edit : GET
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
                .FirstOrDefaultAsync(m => m.ID == id);

            if (nCR == null)
                return NotFound();

            ViewBag.DefectList = new SelectList(_context.Defects, "ID", "Name");
            ViewBag.listOfSuppliers = new SelectList(_context.Suppliers, "ID", "Name");
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

            var ncrStageCheck = await _context.NCRs.FirstOrDefaultAsync(n => n.ID == id); //finding the ncr we are editing then checking the ncr stage
            if (ncrStageCheck == null) return NotFound();    //if the ncr stage is not in quality rep final, then remove server side validation for inspector
            //bool isValidationNeeded = ncrStageCheck.NCR_Stage == NCRStage.QualityRepresentative_Final;


            if (id != nCR.ID)
                return NotFound();

            if (!ModelState.IsValid) //If it posts back here and its incorrect, give them back the data (ddls so they arent unsaved)
            {//But since an NCR_Stage is not declared anywhere, it will use the defualt constructor of engineer if it is null 
                nCR.NCR_Stage = ncrStageCheck.NCR_Stage; //If they are editing quality rep final, it will keep that stage if posts back. If the ncr is in engineering it will persist
                // Populate supplier dropdown list              //The ncr stage will get changed in every edit after this. so in engineering, if model state is valid, then at the end change the stage and continue.
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
                    {
                        if (existingNCR.QualityInspection == null)
                        {
                            existingNCR.QualityInspection = new QualityInspection();
                        }
                        existingNCR.QualityInspection.Name = qualityInspection.Name;
                        existingNCR.QualityInspection.Date = qualityInspection.Date;
                        existingNCR.QualityInspection.QualityIdentify = qualityInspection.QualityIdentify;
                        existingNCR.QualityInspection.ItemMarked = qualityInspection.ItemMarked;
                    }

                    //So if the NCR being edited is on the Quality Representative stage, allow it to be updated with information
                    if (existingNCR.NCR_Stage == NCRStage.QualityRepresentative_Final)
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




        ///Engineering Edit : GET
        public async Task<IActionResult> EngineeringEdit(int? id)
        {
            if (id == null || _context.NCRs == null)
            {
                return NotFound();
            }
            var nCR = await _context.NCRs
                .Include(n => n.Engineering)
                .FirstOrDefaultAsync(n=>n.ID == id);



            if (nCR == null)
                return NotFound();


            return View(nCR);
        }

        ///Engineering Edit : POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EngineeringEdit(int id, [Bind("OldNCRID")]
                                                        NCR nCR, Engineering engineering)
        {
            nCR.ID = id;
            if (id != nCR.ID)
                return NotFound();

            //determine if email
            bool emailYesOrNo = false;

            //Gets the NCR that's going to be edited and is used to store the stage
            var ncrStageCheck = await _context.NCRs.FirstOrDefaultAsync(n => n.ID == id);

            if (!ModelState.IsValid)
            {
                //Ensuring the NCR Stage persistent if the form submission goes bad
                nCR.NCR_Stage = ncrStageCheck.NCR_Stage;
                return View(nCR);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var existingNCR = await _context.NCRs
                        .Include(n => n.Engineering)
                        .FirstOrDefaultAsync(n => n.ID == id);

                    if (existingNCR == null)
                        return NotFound();

                    //Check for NCR Stage, since in model state meaning engineering is going to be updated and all fields are REQUIRED
                    if (ncrStageCheck.NCR_Stage == NCRStage.Engineering) //this would mean that the NCR stage should be sent to the next enum (operations)
                    {
                        existingNCR.NCR_Stage = NCRStage.Operations;  //if the ncr was in the engineering stage, since its complete set it to operations
                        emailYesOrNo = true; //meaning yes send email because it is new, and not an edit
                    }
                    if (ncrStageCheck.NCR_Stage != NCRStage.Engineering) //if the ncr stage is not equal to engineering meaning its from a later stage (operations, procurement, final..)
                        existingNCR.NCR_Stage = ncrStageCheck.NCR_Stage;    //so keep it as that value
                    
                    if (existingNCR.OldNCRID != null)
                        existingNCR.OldNCRID = nCR.ID;

                    _context.Update(existingNCR);

                    if (engineering != null) //it should never be null but just in case
                    {   //so since this ncr will probably not have an engineering entity assigned because its fresh off quality rep, create one so we can store the values in the ncr
                        //This shouldnt affect existing NCRs tho because the if checks to see if theres always properties for it defined.
                        if (existingNCR.Engineering == null)
                        {
                            existingNCR.Engineering = new Engineering();
                        }                                                   //assigns all the properties input into the existing ncr we are editing
                        existingNCR.Engineering.Name = engineering.Name;
                        existingNCR.Engineering.Date = engineering.Date;
                        existingNCR.Engineering.CustomerNotify = engineering.CustomerNotify;
                        existingNCR.Engineering.DrawUpdate = engineering.DrawUpdate;
                        existingNCR.Engineering.DispositionNotes = engineering.DispositionNotes;
                        existingNCR.Engineering.RevisionOriginal = engineering.RevisionOriginal;
                        existingNCR.Engineering.RevisionUpdated = engineering.RevisionUpdated;
                        existingNCR.Engineering.RevisionDate = engineering.RevisionDate;
                        existingNCR.Engineering.EngineeringDisposition = engineering.EngineeringDisposition;
                        await _context.SaveChangesAsync();
                    }

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
                if (emailYesOrNo)
                {
                    var usersInOperation = await _userManager.GetUsersInRoleAsync("Operations");
                    EmailMessage emailMessage = new EmailMessage
                    {
                        Subject = $"NCR #{nCR.FormattedID} is ready to be filled!",
                        Content = $"<p>Hey! </p>" +
                                  $"<p>A new Non-Conformance Report (NCR) has entered your stage.</p>" +
                                  $"<p>Please review and fill as soon as possible.</p>" +
                                  $"<p>Thank you!</p>"
                    };
                    foreach (var user in usersInOperation)
                    {
                        if (user.Email == "operations@outlook.com")
                        {
                            continue; 
                        }
                        emailMessage.ToAddresses.Add(new EmailAddress { Name = user.UserName, Address = user.Email });
                    }
                    //hard code ur own email to test it
                    emailMessage.ToAddresses.Add(new EmailAddress { Name = "Dorian", Address = "dorianCodeDemo@outlook.com" });
                    await _emailSender.SendToManyAsync(emailMessage);  //uncomment for email to work, MAKE SURE you dont email procurement so disable their account and create an employee with procurement as ur own.
                }

                TempData["EditSuccessMsg"] = $"<a href='{Url.Action("Details", "NCRs", new { id = nCR.ID })}'>Click Here to View: {nCR.FormattedID}</a>";
                return RedirectToAction("List", "Home");
            }
            return View(nCR);
        }
    


        ///Operations Edut : GET
        public async Task<IActionResult> OperationEdit(int? id)
        {
            if (id == null || _context.NCRs == null)
                return NotFound();

            var nCR = await _context.NCRs
                .Include(n => n.Operation).ThenInclude(n => n.FollowUp)
                .Include(n => n.Operation).ThenInclude(n => n.CAR)
                .FirstOrDefaultAsync(n => n.ID == id);

            if (nCR == null)
                return NotFound();

            if ((int)nCR.NCR_Stage < (int)NCRStage.Operations)
            {

                TempData["ErrorMessage"] = "You cannot edit this NCR because it is not at the operation stage.";
                return RedirectToAction("List", "Home"); // Redirect to another page
            }
            return View(nCR);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OperationEdit(int id, [Bind("OldNCRID")] NCR nCR, Operation operation) //didnt use followup/car objects because they can be null so just did it in code 
        {
            nCR.ID = id;
            if (id != nCR.ID)
                return NotFound();

            bool sendEmailYesNo = false; //default, dont send email

            //Gets the NCR that's going to be edited and is used to store the stage
            var ncrStageCheck = await _context.NCRs.FirstOrDefaultAsync(n => n.ID == id);

            if (!ModelState.IsValid)
            {
                //Ensuring the NCR Stage persistent if the form submission goes bad
                nCR.NCR_Stage = ncrStageCheck.NCR_Stage;
                return View(nCR);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var existingNCR = await _context.NCRs
                        .Include(n => n.Operation).ThenInclude(n => n.FollowUp)
                        .Include(n => n.Operation).ThenInclude(n => n.CAR)
                        .FirstOrDefaultAsync(n => n.ID == id);

                    if (existingNCR == null)
                        return NotFound();

                    //Check for NCR Stage, since in model state meaning engineering is going to be updated and all fields are REQUIRED
                    if (ncrStageCheck.NCR_Stage == NCRStage.Operations) //this would mean that the NCR stage should be sent to the next enum (procurement)
                    {
                        existingNCR.NCR_Stage = NCRStage.Procurement;  //if the ncr was in the operations stage, since its complete set it to procurement
                        sendEmailYesNo = true;  //set to true because this means it isnt an edit but its the first time its being "created"
                    }
                    if (ncrStageCheck.NCR_Stage != NCRStage.Operations) //if the ncr stage is not equal to operations meaning its from a later stage ( procurement, final..)
                        existingNCR.NCR_Stage = ncrStageCheck.NCR_Stage;    //keep it as that value

                    if (existingNCR.OldNCRID != null)
                        existingNCR.OldNCRID = nCR.ID;

                    _context.Update(existingNCR);

                    if (operation != null) //it should never be null but just in case
                    {   //so since this ncr will probably not have an engineering entity assigned because its fresh off quality rep, create one so we can store the values in the ncr
                        //This shouldnt affect existing NCRs tho because the if checks to see if theres always properties for it defined.
                        if (existingNCR.Operation == null)
                        {
                            existingNCR.Operation = new Operation();
                        }                                        //assigns all the properties input into the existing ncr we are editing      
                        existingNCR.Operation.Name = operation.Name;
                        existingNCR.Operation.OperationDate = operation.OperationDate;
                        existingNCR.Operation.OperationDecision = operation.OperationDecision;
                        existingNCR.Operation.OperationNotes = operation.OperationNotes;
                        existingNCR.Operation.OperationCar = operation.OperationCar;
                        existingNCR.Operation.OperationFollowUp = operation.OperationFollowUp;
                        //Updating Follow Up if Not NUll
                        if (operation.FollowUp != null) //means that the the user passes back a true/yes for a follow up
                        {
                            if (operation.OperationFollowUp) //so if true for follow up
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
                        await _context.SaveChangesAsync();
                    }

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
   
                if (sendEmailYesNo)
                {
                    var usersInProcurement = await _userManager.GetUsersInRoleAsync("Procurement");
                    EmailMessage emailMessage = new EmailMessage
                    {
                        Subject = $"NCR #{nCR.FormattedID} is ready to be filled!",
                        Content = $"<p>Hey there!</p>" +
                                  $"<p>A new Non-Conformance Report (NCR) has entered your stage.</p>" +
                                  $"<p>Please review and fill as soon as possible.</p>" +
                                  $"<p>Thank you!</p>"
                    };
                    foreach (var user in usersInProcurement)
                    {
                        if(user.Email == "procurement@outlook.com")
                        {
                            continue;
                        }
                        emailMessage.ToAddresses.Add(new EmailAddress { Name = user.UserName, Address = user.Email });
                    }
                    //hard code ur own email to test
                    emailMessage.ToAddresses.Add(new EmailAddress { Name = "Dorian", Address = "dorianCodeDemo@outlook.com" });
                    await _emailSender.SendToManyAsync(emailMessage);  //uncomment for email to work, MAKE SURE you dont email procurement so disable their account.
                                                                       //and use your own (create through maintain employee and give urself procurement role)
                }
                TempData["EditSuccessMsg"] = $"<a href='{Url.Action("Details", "NCRs", new { id = nCR.ID })}'>Click Here to View: {nCR.FormattedID}</a>";
                return RedirectToAction("List", "Home");
            }
            return View(nCR);
        }


        public async Task<IActionResult> ProcurementEdit(int? id)
        {
            if (id == null || _context.NCRs == null)
                return NotFound();

            var nCR = await _context.NCRs
                .Include(n => n.Procurement)
                .FirstOrDefaultAsync(n => n.ID == id);

            if (nCR == null)
                return NotFound();

            //If the NCR has not finished the previous stage (operation)
            //deny them from editing 
            if((int)nCR.NCR_Stage < (int)NCRStage.Procurement)
            {

                TempData["ErrorMessage"] = "You cannot edit this NCR because it is not at the procurement stage.";
                return RedirectToAction("List", "Home"); // Redirect to another page
            }

            return View(nCR);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcurementEdit(int id, [Bind("OldNCRID")] NCR nCR, Procurement procurement)
        {
            nCR.ID = id;
            if (id != nCR.ID)
                return NotFound();

            bool sendEmailYesNo = false;

            //Gets the NCR that's going to be edited and is used to store the stage
            var ncrStageCheck = await _context.NCRs.FirstOrDefaultAsync(n => n.ID == id);

            if (!ModelState.IsValid)
            {
                //Ensuring the NCR Stage persistent if the form submission goes bad
                nCR.NCR_Stage = ncrStageCheck.NCR_Stage;
                return View(nCR);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var existingNCR = await _context.NCRs
                        .Include(n => n.Procurement)
                        .FirstOrDefaultAsync(n => n.ID == id);

                    if (existingNCR == null)
                        return NotFound();

                    //Check for NCR Stage, since in model state meaning engineering is going to be updated and all fields are REQUIRED
                    if (ncrStageCheck.NCR_Stage == NCRStage.Procurement)//this would mean that the NCR stage should be sent to the next enum (quality final)
                    {                                       
                        existingNCR.NCR_Stage = NCRStage.QualityRepresentative_Final;
                        sendEmailYesNo = true;
                    }

                    if (ncrStageCheck.NCR_Stage != NCRStage.Procurement) //if the ncr stage is not equal to procurement meaning its from a later stage ( , final..)
                        existingNCR.NCR_Stage = ncrStageCheck.NCR_Stage;    //so keep it as that value

                    if (existingNCR.OldNCRID != null)
                        existingNCR.OldNCRID = nCR.ID;

                    _context.Update(existingNCR);

                    if (procurement != null)
                    {
                        if (existingNCR.Procurement == null)
                        {
                            existingNCR.Procurement = new Procurement();
                        }

                        if (procurement.ReturnRejected == true)
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
                    }
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
                if (sendEmailYesNo)
                {
                    var usersInQualityRep = await _userManager.GetUsersInRoleAsync("Quality Representative");
                    EmailMessage emailMessage = new EmailMessage
                    {
                        Subject = $"NCR #{nCR.FormattedID} is ready to be finished!",
                        Content = $"<p>Hey there!</p>" +
                                  $"<p>Non-Conformance Report (NCR) has come back around for your signing.</p>" +
                                  $"<p>Please review and fill as soon as possible.</p>" +
                                  $"<p>Thank you!</p>"
                    };
                    foreach (var user in usersInQualityRep)
                    {
                        if(user.Email == "qualityrepresentative@outlook.com")
                        emailMessage.ToAddresses.Add(new EmailAddress { Name = user.UserName, Address = user.Email });
                    }
                    //hardcode ur own email to test
                    emailMessage.ToAddresses.Add(new EmailAddress { Name = "Dorian", Address = "dorianCodeDemo@outlook.com" });
                    await _emailSender.SendToManyAsync(emailMessage);  //uncomment for email to work, MAKE SURE you dont email quality rep so disable their account.
                                                                       //and use your own (create through maintain employee and give urself quality rep role)
                }
                TempData["EditSuccessMsg"] = $"<a href='{Url.Action("Details", "NCRs", new { id = nCR.ID })}'>Click Here to View: {nCR.FormattedID}</a>";
                return RedirectToAction("List", "Home");
            }
            return View(nCR);

        }

        //Email button on the details page uses this 
        public async Task RemindUsers (int id)
        {
            var nCR = await _context.NCRs.FindAsync(id);
            var ncrStage = nCR.NCR_Stage;

            EmailMessage emailMessage = new EmailMessage   //Default template for ALL stages (if you want to change them, put them inside the if statements (one for each))
            {
                Subject = $"Reminder: #{nCR.FormattedID} has reached your stage!",
                Content = $"<p>If you need any assistance please contact management.</p>" +
                          $"<p>Please review and fill as soon as possible.</p>" +
                          $"<p>Thank you! REMIND USERS METHOD</p>"
            };


            if (ncrStage == NCRStage.Engineering) //then email engineering to remind them
            {   //return engineering employees
                var usersInOperation = await _userManager.GetUsersInRoleAsync("Engineer");
                foreach (var user in usersInOperation)
                {
                    if (user.Email == "engineer@outlook.com")
                        continue;
                    emailMessage.ToAddresses.Add(new EmailAddress { Name = user.UserName, Address = user.Email });
                }
                emailMessage.ToAddresses.Add(new EmailAddress { Name = "Dorian", Address = "dorianCodeDemo@outlook.com" });
                await _emailSender.SendToManyAsync(emailMessage);
            }///////////////////////////////////////////////////////////////////////////////////////////////////////
            else if (ncrStage == NCRStage.Operations)
            { 
                var usersInEngineer = await _userManager.GetUsersInRoleAsync("Operations");
                foreach (var user in usersInEngineer)
                {
                    if (user.Email == "operations@outlook.com")
                        continue;
                    emailMessage.ToAddresses.Add(new EmailAddress { Name = user.UserName, Address = user.Email });
                }
                emailMessage.ToAddresses.Add(new EmailAddress { Name = "Dorian", Address = "dorianCodeDemo@outlook.com" });
                await _emailSender.SendToManyAsync(emailMessage);
            }//////////////////////////////////////////////////////////////////////////////////////////////////////////
            else if (ncrStage == NCRStage.Procurement) 
            { 
                var usersInProcurement = await _userManager.GetUsersInRoleAsync("Procurement");
                foreach (var user in usersInProcurement)
                {
                    if (user.Email == "procurement@outlook.com")
                        continue;
                    emailMessage.ToAddresses.Add(new EmailAddress { Name = user.UserName, Address = user.Email });
                }
                emailMessage.ToAddresses.Add(new EmailAddress { Name = "Dorian", Address = "dorianCodeDemo@outlook.com" });
                await _emailSender.SendToManyAsync(emailMessage);
            }/////////////////////////////////////////////////////////////////////////////////////////////////////////
            else if (ncrStage == NCRStage.QualityRepresentative_Final) 
            {//return procurement quality rep employees
                var usersInQualityRepFinal = await _userManager.GetUsersInRoleAsync("Quality Representative");
                foreach (var user in usersInQualityRepFinal)
                {
                    if (user.Email == "qualityrepresentative@outlook.com")
                        continue;
                    emailMessage.ToAddresses.Add(new EmailAddress { Name = user.UserName, Address = user.Email });
                }
                emailMessage.ToAddresses.Add(new EmailAddress { Name = "Dorian", Address = "dorianCodeDemo@outlook.com" });
                await _emailSender.SendToManyAsync(emailMessage);
            }
            


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
