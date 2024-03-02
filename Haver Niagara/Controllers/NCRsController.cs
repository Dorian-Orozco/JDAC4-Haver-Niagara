using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Haver_Niagara.Data;
using Haver_Niagara.Models;

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
                .Include(n => n.Part)
                    .ThenInclude(n=>n.Medias)
                .Include(n => n.Purchasing);
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
                .Include(n => n.Part)
                    .ThenInclude(n=>n.Supplier)
                .Include(n=>n.Part)
                    .ThenInclude(n=>n.Medias)
                .Include(n=>n.Part)
                    .ThenInclude(n=>n.DefectLists)
                    .ThenInclude(d => d.Defect)
                .Include(n => n.Purchasing)
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
            //Creating Employee Names
            var employeeNameOptions = new List<string>
            {
                "John Snow",
                "Sandy Road",
                "Bob Frank",
                "Jimmy Garden",
                "Pony Smith",
                "Asgard Fiy",
                "Alex Baxter",
                "Sam Queen"
            };
            ViewBag.EmployeeNameOptions = employeeNameOptions;


            ViewData["EngineeringID"] = new SelectList(_context.Engineerings, "ID", "ID");
            ViewData["PartID"] = new SelectList(_context.Parts, "ID", "ID");
            ViewData["PurchasingID"] = new SelectList(_context.Purchasings, "ID", "ID");
            return View();
        }

        // POST: NCRs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,NCR_Number,SalesOrder,InspectName,InspectDate,NCRClosed,QualSignature,QualDate,PartID,PurchasingID,EngineeringID")] NCR nCR, List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nCR);
                await _context.SaveChangesAsync();

                if(files != null && files.Count > 0)
                {
                    await OnPostUploadAsync(files, nCR.ID);
                }
                return RedirectToAction("List", "Home");
                //return RedirectToAction(nameof(Index));   //change to not view list
            }
            ViewData["EngineeringID"] = new SelectList(_context.Engineerings, "ID", "ID", nCR.EngineeringID);
            ViewData["PartID"] = new SelectList(_context.Parts, "ID", "ID", nCR.PartID);
            ViewData["PurchasingID"] = new SelectList(_context.Purchasings, "ID", "ID", nCR.PurchasingID);
            return View(nCR);
        }

        // GET: NCRs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.NCRs == null)
            {
                return NotFound();
            }

            //Creating Employee Names
            var employeeNameOptions = new List<string>
            {
                "John Snow",
                "Sandy Road",
                "Bob Frank",
                "Jimmy Garden",
                "Pony Smith",
                "Asgard Fiy",
                "Alex Baxter",
                "Sam Queen"
            };
            ViewBag.EmployeeNameOptions = employeeNameOptions;

            var nCR = await _context.NCRs.FindAsync(id);
            if (nCR == null)
            {
                return NotFound();
            }
            ViewData["EngineeringID"] = new SelectList(_context.Engineerings, "ID", "ID", nCR.EngineeringID);
            ViewData["PartID"] = new SelectList(_context.Parts, "ID", "ID", nCR.PartID);
            ViewData["PurchasingID"] = new SelectList(_context.Purchasings, "ID", "ID", nCR.PurchasingID);
            return View(nCR);
        }

        // POST: NCRs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,NCR_Number,SalesOrder,InspectName,InspectDate,NCRClosed,QualSignature,QualDate,PartID,PurchasingID,EngineeringID")] NCR nCR, List<IFormFile> files)
        {
            if (id != nCR.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nCR);
                    await OnPostUploadAsync(files, nCR.ID);
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
                //return RedirectToAction(nameof(Index));   //change to not view list
            }
            ViewData["EngineeringID"] = new SelectList(_context.Engineerings, "ID", "ID", nCR.EngineeringID);
            ViewData["PartID"] = new SelectList(_context.Parts, "ID", "ID", nCR.PartID);
            ViewData["PurchasingID"] = new SelectList(_context.Purchasings, "ID", "ID", nCR.PurchasingID);
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
                .Include(n => n.Part)
                    .ThenInclude(n => n.Medias)
                .Include(n => n.Purchasing)
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
            //return RedirectToAction(nameof(Index));   //change to not view list
        }

        //https://learn.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-8.0
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostUploadAsync(List<IFormFile> files, int ncrID) //Accepting multiple Iformfiles, and an NCR ID to relate data
        {
            var ncr = await _context.NCRs  
                .Include(n=>n.Part)
                    .ThenInclude(n=>n.Medias)
                .FirstOrDefaultAsync(n=>n.ID == ncrID);

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
            await _context.SaveChangesAsync();

            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { count = files.Count, size });
        }

        



        private bool NCRExists(int id)
        {
          return _context.NCRs.Any(e => e.ID == id);
        }
    }
}
