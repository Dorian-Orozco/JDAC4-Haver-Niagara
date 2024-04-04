﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Haver_Niagara.Data;
using Haver_Niagara.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Haver_Niagara.CustomController;

namespace Haver_Niagara.Controllers
{
    public class DefectController : LookupsController
    {
        private readonly HaverNiagaraDbContext _context;

        public DefectController(HaverNiagaraDbContext context)
        {
            _context = context;
        }

        // GET: Defects
        public IActionResult Index()
        {
            return Redirect(ViewData["returnURL"].ToString());
        }

        // GET: Defects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Defects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] Defect defect)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(defect);
                    await _context.SaveChangesAsync();
                    return Redirect(ViewData["returnURL"].ToString());
                }
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("UNIQUE constraint failed"))
                {
                    ModelState.AddModelError("Name", "Unable to save changes. "
                        + "You cannot have duplicate Defect Names");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, " +
                        "If the problem persists contact your administrator.");
                }
            }
            if(!ModelState.IsValid && Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                string errorMessage = "";
                foreach (var modelState in ViewData.ModelState.Values)
                {
                    foreach(ModelError error in modelState.Errors)
                    {
                        errorMessage += error.ErrorMessage + "|";
                    }
                }
                return BadRequest(errorMessage);
            }
            return View(defect);
        }

        // GET: Defects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Defects == null)
            {
                return NotFound();
            }

            var defect = await _context.Defects.FindAsync(id);
            if (defect == null)
            {
                return NotFound();
            }
            return View(defect);
        }

        // POST: Defects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name")] Defect defect)
        {
            if (id != defect.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(defect);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DefectExists(defect.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (DbUpdateException dex)
                {
                    if (dex.GetBaseException().Message.Contains("UNIQUE constraint failed"))
                    {
                        ModelState.AddModelError("Name", "Unable to save changes. "
                            + "You cannot have duplicate Defect Names");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to save changes. Try again, " +
                            "If the problem persists contact your administrator.");
                    }
                }
                if (!ModelState.IsValid && Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    string errorMessage = "";
                    foreach (var modelState in ViewData.ModelState.Values)
                    {
                        foreach (ModelError error in modelState.Errors)
                        {
                            errorMessage += error.ErrorMessage + "|";
                        }
                    }
                    return BadRequest(errorMessage);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(defect);
        }

        // GET: Defects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Defects == null)
            {
                return NotFound();
            }

            var defect = await _context.Defects
        .FirstOrDefaultAsync(m => m.ID == id);
            if (defect == null)
            {
                return NotFound();
            }
            // Check if the defect is associated with any DefectList
            var defectList = await _context.DefectLists
                .FirstOrDefaultAsync(dl => dl.DefectID == id);
            if (defectList != null)
            {
                // If there's a DefectList associated with the defect, check if it's used in any NCRs
                var associatedNCR = await _context.NCRs
                    .FirstOrDefaultAsync(ncr => ncr.Part.DefectLists.Any(d => d.DefectID == id));

                if (associatedNCR != null)
                {
                    var defectName = defect.Name;
                    TempData["ErrorMessage"] = $"{defectName} cannot be deleted because it is associated with an NCR.";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(defect);
        }

        // POST: Defects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Defects == null)
            {
                return Problem("Entity set 'HaverNiagaraDbContext.Defects'  is null.");
            }
            var defect = await _context.Defects.FindAsync(id);
            if (defect != null)
            {
                _context.Defects.Remove(defect);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DefectExists(int id)
        {
          return _context.Defects.Any(e => e.ID == id);
        }
    }
}
