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
    public class PurchasingsController : Controller
    {
        private readonly HaverNiagaraDbContext _context;

        public PurchasingsController(HaverNiagaraDbContext context)
        {
            _context = context;
        }

        // GET: Purchasings
        public async Task<IActionResult> Index()
        {
              return _context.Purchasings != null ? 
                          View(await _context.Purchasings.ToListAsync()) :
                          Problem("Entity set 'HaverNiagaraDbContext.Purchasings'  is null.");
        }

        // GET: Purchasings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Purchasings == null)
            {
                return NotFound();
            }

            var purchasing = await _context.Purchasings
                .FirstOrDefaultAsync(m => m.ID == id);
            if (purchasing == null)
            {
                return NotFound();
            }

            return View(purchasing);
        }

        // GET: Purchasings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Purchasings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,PurchaseSignature,SignatureDate,PurchasingDec")] Purchasing purchasing)
        {
            if (ModelState.IsValid)
            {
                _context.Add(purchasing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(purchasing);
        }

        // GET: Purchasings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Purchasings == null)
            {
                return NotFound();
            }

            var purchasing = await _context.Purchasings.FindAsync(id);
            if (purchasing == null)
            {
                return NotFound();
            }
            return View(purchasing);
        }

        // POST: Purchasings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,PurchaseSignature,SignatureDate,PurchasingDec")] Purchasing purchasing)
        {
            if (id != purchasing.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchasing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchasingExists(purchasing.ID))
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
            return View(purchasing);
        }

        // GET: Purchasings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Purchasings == null)
            {
                return NotFound();
            }

            var purchasing = await _context.Purchasings
                .FirstOrDefaultAsync(m => m.ID == id);
            if (purchasing == null)
            {
                return NotFound();
            }

            return View(purchasing);
        }

        // POST: Purchasings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Purchasings == null)
            {
                return Problem("Entity set 'HaverNiagaraDbContext.Purchasings'  is null.");
            }
            var purchasing = await _context.Purchasings.FindAsync(id);
            if (purchasing != null)
            {
                _context.Purchasings.Remove(purchasing);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchasingExists(int id)
        {
          return (_context.Purchasings?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
