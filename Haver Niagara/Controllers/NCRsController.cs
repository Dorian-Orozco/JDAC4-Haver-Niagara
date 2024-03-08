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

namespace Haver_Niagara.Controllers
{
    public class NCRsController : Controller
    {
        private readonly HaverNiagaraDbContext _context;

        public NCRsController(HaverNiagaraDbContext context)
        {
            _context = context;
        }



        // GET: NCRs
        public async Task<IActionResult> Index()
        {
            var haverNiagaraDbContext = _context.NCRs
                .Include(n => n.Engineering)
                .Include(n => n.Operation)
                .Include(n=>n.QualityInspection) //////////////remove if breaks
                .Include(n => n.Part)
                    .ThenInclude(n => n.Medias);
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
                .Include(n => n.Engineering)
                .Include(n => n.QualityInspection)     //////////remove if break
                .Include(n => n.Part)
                    .ThenInclude(n => n.Supplier)
                .Include(n => n.Part)
                    .ThenInclude(n => n.Medias)
                .Include(n => n.Part)
                    .ThenInclude(n => n.DefectLists)
                    .ThenInclude(n => n.Defect)
                .Include(n => n.Operation)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (nCR == null)
            {
                return NotFound();
            }

            return View(nCR);
        }

        // GET: NCRs/Create
        public IActionResult Create()
        {
            ///Populate list of defects
            ViewBag.DefectList = new SelectList(_context.Defects, "ID", "Name");

            // Populate supplier dropdown list
            ViewBag.listOfSuppliers = new SelectList(_context.Suppliers, "ID", "Name");

            ViewData["QualityInspectionID"] = new SelectList(_context.QualityInspections, "ID", "ID");
            ViewData["EngineeringID"] = new SelectList(_context.Engineerings, "ID", "ID");
            ViewData["OperationID"] = new SelectList(_context.Operations, "ID", "ID");
            ViewData["PartID"] = new SelectList(_context.Parts, "ID", "ID");
           
            return View();
        }

        // POST: NCRs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,NCR_Number,NCR_Date,NCR_Status,NCR_Stage,PartID,OperationID,EngineeringID,QualityInspectionID")]
                NCR nCR, Part part, [Bind("Name,Date,ItemMarked,QualityIdentify")] QualityInspection qualityInspection, List<IFormFile> files, List<string> links,
                int defectID) 
        {
            if (ModelState.IsValid)
            {
                // Add the part to the context
                _context.Add(part);
                _context.Add(qualityInspection);
                
                await _context.SaveChangesAsync();
                //Assign the generated IDs to this NCR
                nCR.PartID = part.ID;
                nCR.QualityInspectionID = qualityInspection.ID;
                //Add the NCR to the context
                _context.Add(nCR);

                var defectList = new DefectList
                {
                    PartID = part.ID,
                    DefectID = defectID
                };
                _context.Add(defectList);

                await _context.SaveChangesAsync();

                //Images function to save
                if (files != null && files.Count > 0)
                {
                    await OnPostUploadAsync(files, nCR.ID, links);
                }
                return RedirectToAction("List", "Home");

                //return RedirectToAction(nameof(Index));
            }
            ViewData["EngineeringID"] = new SelectList(_context.Engineerings, "ID", "ID", nCR.EngineeringID);
            ViewData["OperationID"] = new SelectList(_context.Operations, "ID", "ID", nCR.OperationID);
            ViewData["PartID"] = new SelectList(_context.Parts, "ID", "ID", nCR.PartID);
            ViewData["QualityInspectionID"] = new SelectList(_context.QualityInspections, "ID", "ID", nCR.QualityInspectionID);
            ViewBag.listOfSuppliers = new SelectList(_context.Suppliers, "ID", "Name");
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
                .Include(n => n.QualityInspection)
                .Include(n=>n.Part)
                    .ThenInclude(n=>n.Medias)
                .Include(n=>n.Part)
                    .ThenInclude(n=>n.Supplier)
                .Include(n=>n.Part)
                    .ThenInclude(n=>n.DefectLists)
                        .ThenInclude(n=>n.Defect)
                .FirstOrDefaultAsync(n=>n.ID == id);

            ///Populate list of defects
            ViewBag.DefectList = new SelectList(_context.Defects, "ID", "Description");

            // Populate supplier dropdown list
            ViewBag.listOfSuppliers = new SelectList(_context.Suppliers, "ID", "Name");

            if (nCR == null)
            {
                return NotFound();
            }
            ViewData["EngineeringID"] = new SelectList(_context.Engineerings, "ID", "ID", nCR.EngineeringID);
            ViewData["OperationID"] = new SelectList(_context.Operations, "ID", "ID", nCR.OperationID);
            ViewData["PartID"] = new SelectList(_context.Parts, "ID", "ID", nCR.PartID);
            ViewData["QualityInspectionID"] = new SelectList(_context.QualityInspections, "ID", "ID", nCR.QualityInspectionID);
            return View(nCR);
        }

        // POST: NCRs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        
        [HttpPost]  
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,NCR_Number,NCR_Date,PartID,OperationID")] //ids have to be included here and in the edit (hidden but MUST be there so the database gives the right id to right NCR.)
                    NCR nCR, Part part, List<IFormFile> files, List<string> links)
        {
            if (id != nCR.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingNCR = await _context.NCRs
                        .Include(n=>n.Part)
                            .ThenInclude(p=>p.Medias)
                        .Include(n=>n.Part)
                            .ThenInclude(n=>n.DefectLists)
                                .ThenInclude(n=>n.Defect)
                        .FirstOrDefaultAsync(n=>n.ID == id);

                    if(existingNCR == null)
                    {
                        return NotFound();
                    }

                    //detaching to prevent tracking issues
                    _context.Entry(existingNCR).State = EntityState.Detached;   
                                                                                
                    //Update NCR Properties
                    existingNCR.NCR_Number = nCR.NCR_Number;                    
                    existingNCR.NCR_Date = nCR.NCR_Date;   

                    

                    //ASSOCIATED TABLES PAST THIS POINT.
                    //Updating Part Properties, gets old values so they are not overwritten if new val null
                    if(part != null)
                    {
                        existingNCR.Part.Name = part.Name;
                        existingNCR.Part.PartNumber = part.PartNumber;
                        existingNCR.Part.SAPNumber = part.SAPNumber;
                        existingNCR.Part.PurchaseNumber = part.PurchaseNumber;
                        existingNCR.Part.SalesOrder = part.SalesOrder;
                        existingNCR.Part.ProductNumber = part.ProductNumber;
                        existingNCR.Part.QuantityRecieved = part.QuantityRecieved;
                        existingNCR.Part.QuantityDefect = part.QuantityDefect;
                        existingNCR.Part.Description = part.Description;
                        existingNCR.Part.SupplierID = part.SupplierID;

                        if (part.DefectLists != null && part.DefectLists.Any())
                        {
                            existingNCR.Part.DefectLists.Clear();
                            var selectedDefect = part.DefectLists.First();
                            existingNCR.Part.DefectLists.Add(selectedDefect);
                        }
                    }
               
                    //if (qualityInspection != null)
                    //{
                    //    _context.Add(qualityInspection);
                    //    await _context.SaveChangesAsync();
                    //    await _context.Entry(qualityInspection).GetDatabaseValuesAsync();
                    //    existingNCR.QualityInspectionID = qualityInspection.ID;
                    //    existingNCR.QualityInspection.Name = qualityInspection.Name;
                    //    existingNCR.QualityInspection.Date = qualityInspection.Date;
                    //    existingNCR.QualityInspection.ItemMarked = qualityInspection.ItemMarked;
                    //    existingNCR.QualityInspection.ReInspected = qualityInspection.ReInspected;
                    //    existingNCR.QualityInspection.QualityIdentify = qualityInspection.QualityIdentify;
                    //}

                    _context.Attach(existingNCR).State = EntityState.Modified;
                    if (existingNCR.Part != null)
                    {
                        _context.Attach(existingNCR.Part).State = EntityState.Modified;
                    }
                    //if(existingNCR.QualityInspection != null)
                    //{
                    //    _context.Attach(existingNCR.QualityInspection).State = EntityState.Modified;
                    //}


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
                return RedirectToAction("List", "Home");
            }
            //Populate viewbag for list of suppliers
            ViewBag.listOfSuppliers = new SelectList(_context.Suppliers, "ID", "Name");

            ViewData["EngineeringID"] = new SelectList(_context.Engineerings, "ID", "ID", nCR.EngineeringID);
            ViewData["OperationID"] = new SelectList(_context.Operations, "ID", "ID", nCR.OperationID);
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
                .Include(n=>n.QualityInspection)
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
                    .Include(n=>n.Part)
                        .ThenInclude(n=>n.Medias)
                    .FirstOrDefaultAsync(n=>n.ID == ncrID);

            if (ncr != null && ncr.Part != null)
            {
                //Since a user can only delete one image at a time, we dont have to worry about looping through images in media
                var imageToRemove = ncr.Part.Medias.FirstOrDefault(n=>n.ID == imageID);
                if(imageToRemove != null)
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
