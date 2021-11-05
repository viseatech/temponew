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
    public class Etape_MissionController : Controller
    {
        private readonly MvcNoteContext _context;

        public Etape_MissionController(MvcNoteContext context)
        {
            _context = context;
        }

        // GET: Etape_Mission
        public async Task<IActionResult> Index()
        {
            return View(await _context.Etape_Mission.ToListAsync());
        }

        // GET: Etape_Mission/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etape_Mission = await _context.Etape_Mission
                .FirstOrDefaultAsync(m => m.Id == id);
            if (etape_Mission == null)
            {
                return NotFound();
            }

            return View(etape_Mission);
        }

        // GET: Etape_Mission/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Etape_Mission/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,type_Mission_Id")] Etape_Mission etape_Mission)
        {
            if (ModelState.IsValid)
            {
                _context.Add(etape_Mission);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(etape_Mission);
        }

        // GET: Etape_Mission/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etape_Mission = await _context.Etape_Mission.FindAsync(id);
            if (etape_Mission == null)
            {
                return NotFound();
            }
            return View(etape_Mission);
        }

        // POST: Etape_Mission/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Etape_Mission etape_Mission)
        {
            if (id != etape_Mission.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(etape_Mission);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Etape_MissionExists(etape_Mission.Id))
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
            return View(etape_Mission);
        }

        // GET: Etape_Mission/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etape_Mission = await _context.Etape_Mission
                .FirstOrDefaultAsync(m => m.Id == id);
            if (etape_Mission == null)
            {
                return NotFound();
            }

            return View(etape_Mission);
        }

        // POST: Etape_Mission/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var etape_Mission = await _context.Etape_Mission.FindAsync(id);
            _context.Etape_Mission.Remove(etape_Mission);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Etape_MissionExists(int id)
        {
            return _context.Etape_Mission.Any(e => e.Id == id);
        }
    }
}
