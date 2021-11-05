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
    public class MonthValidationsController : Controller
    {
        private readonly MvcNoteContext _context;

        public MonthValidationsController(MvcNoteContext context)
        {
            _context = context;
        }

        // GET: MonthValidations
        public async Task<IActionResult> Index()
        {
            var user = await _context.Users.FirstOrDefaultAsync(m => m.Email.Contains(User.Identity.Name));
            if (user.RoleId != 3)
            {
                return NotFound();
            }
            return View(await _context.MonthValidations.ToListAsync());
        }

        // GET: MonthValidations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(m => m.Email.Contains(User.Identity.Name));
            if (user.RoleId != 3)
            {
                return NotFound();
            }
            if (id == null)
            {
                return NotFound();
            }

            var monthValidation = await _context.MonthValidations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (monthValidation == null)
            {
                return NotFound();
            }

            return View(monthValidation);
        }

        // GET: MonthValidations/Create
        public IActionResult Create()
        {
            var user = _context.Users.FirstOrDefault(m => m.Email.Contains(User.Identity.Name));
            if (user.RoleId != 3)
            {
                return NotFound();
            }
            return View();
        }

        // POST: MonthValidations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,Date")] MonthValidation monthValidation)
        {
            var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == monthValidation.UserId);
            if (user == null)
            {
                return NotFound();
            }
            monthValidation.Userstr = user.Name;
            if (ModelState.IsValid)
            {
                _context.Add(monthValidation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(monthValidation);
        }

        // GET: MonthValidations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(m => m.Email.Contains(User.Identity.Name));
            if (user.RoleId != 3)
            {
                return NotFound();
            }
            if (id == null)
            {
                return NotFound();
            }

            var monthValidation = await _context.MonthValidations.FindAsync(id);
            if (monthValidation == null)
            {
                return NotFound();
            }
            return View(monthValidation);
        }

        // POST: MonthValidations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Date")] MonthValidation monthValidation)
        {
            var user = await _context.Users.FirstOrDefaultAsync(m => m.Email.Contains(User.Identity.Name));
            if (user.RoleId != 3)
            {
                return NotFound();
            }
            if (id != monthValidation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(monthValidation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MonthValidationExists(monthValidation.Id))
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
            return View(monthValidation);
        }

        // GET: MonthValidations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(m => m.Email.Contains(User.Identity.Name));
            if (user.RoleId != 3)
            {
                return NotFound();
            }
            if (id == null)
            {
                return NotFound();
            }

            var monthValidation = await _context.MonthValidations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (monthValidation == null)
            {
                return NotFound();
            }

            return View(monthValidation);
        }

        // POST: MonthValidations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(m => m.Email.Contains(User.Identity.Name));
            if (user.RoleId != 3)
            {
                return NotFound();
            }
            var monthValidation = await _context.MonthValidations.FindAsync(id);
            _context.MonthValidations.Remove(monthValidation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MonthValidationExists(int id)
        {
            return _context.MonthValidations.Any(e => e.Id == id);
        }
    }
}
