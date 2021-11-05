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
    public class ZfilesController : Controller
    {
        private readonly MvcNoteContext _context;

        public ZfilesController(MvcNoteContext context)
        {
            _context = context;
        }

        // GET: Zfiles
        public async Task<IActionResult> Index()
        {
            return View(await _context.Zfile.ToListAsync());
        }

        // GET: Zfiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zfile = await _context.Zfile
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zfile == null)
            {
                return NotFound();
            }

            return View(zfile);
        }

        // GET: Zfiles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Zfiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,note,path")] Zfile zfile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zfile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(zfile);
        }

        // GET: Zfiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zfile = await _context.Zfile.FindAsync(id);
            if (zfile == null)
            {
                return NotFound();
            }
            return View(zfile);
        }

        // POST: Zfiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,note,path")] Zfile zfile)
        {
            if (id != zfile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zfile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZfileExists(zfile.Id))
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
            return View(zfile);
        }

        // GET: Zfiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zfile = await _context.Zfile
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zfile == null)
            {
                return NotFound();
            }
          
            return View(zfile);
        }

        // POST: Zfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zfile = await _context.Zfile.FindAsync(id);

            string fullPath = ".\\wwwroot\\Files\\" + zfile.Name;
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            _context.Zfile.Remove(zfile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZfileExists(int id)
        {
            return _context.Zfile.Any(e => e.Id == id);
        }
    }
}
