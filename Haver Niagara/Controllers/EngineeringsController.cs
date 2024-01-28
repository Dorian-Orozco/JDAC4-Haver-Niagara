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
    public class EngineeringsController : Controller
    {
        private readonly HaverNiagaraDbContext _context;

        public EngineeringsController(HaverNiagaraDbContext context)
        {
            _context = context;
        }

        // GET: Engineerings
        public async Task<IActionResult> Index()
        {
            var haverNiagaraDbContext = _context.Engineering.Include(e => e.NCR);
            return View(await haverNiagaraDbContext.ToListAsync());
        }

        // GET: Engineerings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Engineering == null)
            {
                return NotFound();
            }

            var engineering = await _context.Engineering
                .Include(e => e.NCR)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (engineering == null)
            {
                return NotFound();
            }

            return View(engineering);
        }

        // GET: Engineerings/Create
        public IActionResult Create()
        {
            ViewData["NCRId"] = new SelectList(_context.NCRs, "ID", "ID");
            return View();
        }

        // POST: Engineerings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,CustomerNotify,DrawUpdate,Disposition,RevisionOriginal,RevisionUpdated,RevisionDate,EngSignature,EngSignatureDate,EngDecision,NCRId")] Engineering engineering)
        {
            if (ModelState.IsValid)
            {
                _context.Add(engineering);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NCRId"] = new SelectList(_context.NCRs, "ID", "ID", engineering.NCRId);
            return View(engineering);
        }

        // GET: Engineerings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Engineering == null)
            {
                return NotFound();
            }

            var engineering = await _context.Engineering.FindAsync(id);
            if (engineering == null)
            {
                return NotFound();
            }
            ViewData["NCRId"] = new SelectList(_context.NCRs, "ID", "ID", engineering.NCRId);
            return View(engineering);
        }

        // POST: Engineerings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CustomerNotify,DrawUpdate,Disposition,RevisionOriginal,RevisionUpdated,RevisionDate,EngSignature,EngSignatureDate,EngDecision,NCRId")] Engineering engineering)
        {
            if (id != engineering.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(engineering);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EngineeringExists(engineering.ID))
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
            ViewData["NCRId"] = new SelectList(_context.NCRs, "ID", "ID", engineering.NCRId);
            return View(engineering);
        }

        // GET: Engineerings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Engineering == null)
            {
                return NotFound();
            }

            var engineering = await _context.Engineering
                .Include(e => e.NCR)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (engineering == null)
            {
                return NotFound();
            }

            return View(engineering);
        }

        // POST: Engineerings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Engineering == null)
            {
                return Problem("Entity set 'HaverNiagaraDbContext.Engineering'  is null.");
            }
            var engineering = await _context.Engineering.FindAsync(id);
            if (engineering != null)
            {
                _context.Engineering.Remove(engineering);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EngineeringExists(int id)
        {
          return (_context.Engineering?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
