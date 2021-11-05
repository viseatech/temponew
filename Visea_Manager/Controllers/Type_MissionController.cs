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
    public class Type_MissionController : Controller
    {
        private readonly MvcNoteContext _context;

        public Type_MissionController(MvcNoteContext context)
        {
            _context = context;
        }

        // GET: Type_Mission
        public async Task<IActionResult> Index()
        {
            return View(await _context.Type_Mission.ToListAsync());
        }

        // GET: Type_Mission/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var type_Mission = await _context.Type_Mission
                .FirstOrDefaultAsync(m => m.Id == id);
            if (type_Mission == null)
            {
                return NotFound();
            }

            return View(type_Mission);
        }

        // GET: Type_Mission/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Type_Mission/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Type,Type_Id,Mission_Id")] Type_Mission type_Mission)
        {
            if (ModelState.IsValid)
            {
                _context.Add(type_Mission);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(type_Mission);
        }

        // GET: Type_Mission/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var type_Mission = await _context.Type_Mission.FindAsync(id);
            if (type_Mission == null)
            {
                return NotFound();
            }
            return View(type_Mission);
        }

        // POST: Type_Mission/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Type,Type_Id")] Type_Mission type_Mission)
        {
            if (id != type_Mission.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(type_Mission);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Type_MissionExists(type_Mission.Id))
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
            return View(type_Mission);
        }

        // GET: Type_Mission/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var type_Mission = await _context.Type_Mission
                .FirstOrDefaultAsync(m => m.Id == id);
            if (type_Mission == null)
            {
                return NotFound();
            }

            return View(type_Mission);
        }

        // POST: Type_Mission/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var type_Mission = await _context.Type_Mission.FindAsync(id);
            _context.Type_Mission.Remove(type_Mission);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Type_MissionExists(int id)
        {
            return _context.Type_Mission.Any(e => e.Id == id);
        }
    }
}
