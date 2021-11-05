using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Visea_Expense_Manager.Data;
using Visea_Expense_Manager.Models;

namespace Visea_Expense_Manager.Controllers
{
    public class Type_formationController : Controller
    {
        private readonly MvcNoteContext _context;

        public Type_formationController(MvcNoteContext context)
        {
            _context = context;
        }

        // GET: Type_formation
        public async Task<IActionResult> Index()
        {
            return View(await _context.Type_formation.ToListAsync());
        }

        // GET: Type_formation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var type_formation = await _context.Type_formation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (type_formation == null)
            {
                return NotFound();
            }

            return View(type_formation);
        }

        // GET: Type_formation/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Type_formation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,formation_Id")] Type_formation type_formation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(type_formation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(type_formation);
        }

        // GET: Type_formation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var type_formation = await _context.Type_formation.FindAsync(id);
            if (type_formation == null)
            {
                return NotFound();
            }
            return View(type_formation);
        }

        // POST: Type_formation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Type_formation type_formation)
        {
            if (id != type_formation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(type_formation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Type_formationExists(type_formation.Id))
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
            return View(type_formation);
        }

        // GET: Type_formation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var type_formation = await _context.Type_formation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (type_formation == null)
            {
                return NotFound();
            }

            return View(type_formation);
        }

        // POST: Type_formation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var type_formation = await _context.Type_formation.FindAsync(id);
            _context.Type_formation.Remove(type_formation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Type_formationExists(int id)
        {
            return _context.Type_formation.Any(e => e.Id == id);
        }
    }
}
