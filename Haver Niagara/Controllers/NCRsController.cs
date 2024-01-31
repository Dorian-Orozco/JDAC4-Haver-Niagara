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
            var haverNiagaraDbContext = _context.NCRs.Include(n => n.Engineering).Include(n => n.Product).Include(n => n.Purchasing);
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
                .Include(n => n.Product)
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
            ViewData["EngineeringID"] = new SelectList(_context.Engineering, "ID", "ID");
            ViewData["ProductID"] = new SelectList(_context.Products, "ID", "ID");
            ViewData["PurchasingID"] = new SelectList(_context.Purchasings, "ID", "ID");
            return View();
        }

        // POST: NCRs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,NCR_Number,SalesOrder,InspectName,InspectDate,NCRClosed,QualSignature,QualDate,ProductID,PurchasingID,EngineeringID")] NCR nCR)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nCR);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EngineeringID"] = new SelectList(_context.Engineering, "ID", "ID", nCR.EngineeringID);
            ViewData["ProductID"] = new SelectList(_context.Products, "ID", "ID", nCR.ProductID);
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

            var nCR = await _context.NCRs.FindAsync(id);
            if (nCR == null)
            {
                return NotFound();
            }
            ViewData["EngineeringID"] = new SelectList(_context.Engineering, "ID", "ID", nCR.EngineeringID);
            ViewData["ProductID"] = new SelectList(_context.Products, "ID", "ID", nCR.ProductID);
            ViewData["PurchasingID"] = new SelectList(_context.Purchasings, "ID", "ID", nCR.PurchasingID);
            return View(nCR);
        }

        // POST: NCRs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,NCR_Number,SalesOrder,InspectName,InspectDate,NCRClosed,QualSignature,QualDate,ProductID,PurchasingID,EngineeringID")] NCR nCR)
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["EngineeringID"] = new SelectList(_context.Engineering, "ID", "ID", nCR.EngineeringID);
            ViewData["ProductID"] = new SelectList(_context.Products, "ID", "ID", nCR.ProductID);
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
                .Include(n => n.Product)
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
            return RedirectToAction(nameof(Index));
        }

        private bool NCRExists(int id)
        {
          return _context.NCRs.Any(e => e.ID == id);
        }
    }
}
