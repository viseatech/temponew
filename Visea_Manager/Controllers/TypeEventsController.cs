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
    public class TypeEventsController : Controller
    {
        private readonly MvcNoteContext _context;

        public TypeEventsController(MvcNoteContext context)
        {
            _context = context;
        }

        // GET: TypeEvents
        public async Task<IActionResult> Index()
        {
            return View(await _context.TypeEvent.ToListAsync());
        }

        /*
        // GET: TypeEvents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeEvent = await _context.TypeEvent
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeEvent == null)
            {
                return NotFound();
            }

            return View(typeEvent);
        }
        */
        // GET: TypeEvents/Create
        public IActionResult Create()
        {
            return View(_context);
        }

        // POST: TypeEvents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string Titre, string Type, int Classe_Id, int Classe2_Id, int Classe3_Id, int Classe4_Id)
        {
            int Type_Id = 0;
            if (Type != null)
                Type_Id = (await _context.TypeEvent.FirstOrDefaultAsync(eventty => eventty.Name.Contains(Type))).Id;
            if (Type_Id > 0)
            {
                if (Classe_Id > 0)
                {
                    if (Classe2_Id > 0)
                    {
                        if (Classe3_Id > 0)
                        {
                            Etape_Mission tmp = new Etape_Mission();
                            tmp.Name = Titre;
                            tmp.type_Mission_Id = Classe3_Id;
                            _context.Add(tmp);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            Type_Mission tmp = new Type_Mission();
                            tmp.Name = Titre;
                            tmp.Mission_Id = Classe2_Id;
                            _context.Add(tmp);
                            await _context.SaveChangesAsync();
                        }
                    }
                    else
                    {
                        if (Type_Id == 1)
                        {
                            Mission tmp = new Mission();
                            tmp.Name = Titre;
                            tmp.Client_Id = Classe_Id;
                            _context.Add(tmp);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
                else
                {
                    if (Type_Id == 1)
                    {
                        Client tmp = new Client();
                        tmp.Name = Titre;
                        tmp.Type_Id = Type_Id;
                        _context.Add(tmp);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        Formation tmp = new Formation();
                        tmp.Name = Titre;
                        tmp.Type_Id = Type_Id;
                        _context.Add(tmp);
                        await _context.SaveChangesAsync();
                    }

                }
            }
            else
            {
                TypeEvent tmp = new TypeEvent();
                tmp.Name = Titre;
                _context.Add(tmp);
                await _context.SaveChangesAsync();
            }
            return View(_context);
        }

        // GET: TypeEvents/Delete/5
        public async Task<IActionResult> Delete()
        {
            return View(_context);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string Titre, string Type, int Classe_Id, int Classe2_Id, int Classe3_Id, int Classe4_Id)
        {
            int Type_Id = 0;
            if (Type != null)
                Type_Id = (await _context.TypeEvent.FirstOrDefaultAsync(eventty => eventty.Name.Contains(Type))).Id;
            if (Type_Id > 0)
            {
                if (Classe4_Id > 0)
                {
                    Etape_Mission tmp = _context.Etape_Mission.Find(Classe4_Id);
                    if (tmp != null)
                        _context.Remove(tmp);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    if (Classe3_Id > 0)
                    {
                        Type_Mission tmp = _context.Type_Mission.Find(Classe3_Id);
                        if (tmp != null)
                            _context.Remove(tmp);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        if (Classe2_Id > 0)
                        {
                            Mission tmp = _context.Mission.Find(Classe2_Id);
                            if (tmp != null)
                                _context.Remove(tmp);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            if (Classe_Id > 0)
                            {
                                if (Type_Id == 1)
                                {
                                    Client tmp = _context.Client.Find(Classe_Id);
                                    if (tmp != null)
                                        _context.Remove(tmp);
                                    await _context.SaveChangesAsync();
                                }
                                else
                                {
                                    Formation tmp = _context.Formation.Find(Classe_Id);
                                    if (tmp != null)
                                        _context.Remove(tmp);
                                    await _context.SaveChangesAsync();
                                }
                            }
                            else
                            {
                                TypeEvent tmp = _context.TypeEvent.Find(Type_Id);
                                if (tmp != null)
                                    _context.Remove(tmp);
                                await _context.SaveChangesAsync();
                            }
                        }
                    }
                }
            }
            return View(_context);
        }
    }
}
        /*
        // GET: TypeEvents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeEvent = await _context.TypeEvent.FindAsync(id);
            if (typeEvent == null)
            {
                return NotFound();
            }
            return View(typeEvent);
        }

        // POST: TypeEvents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] TypeEvent typeEvent)
        {
            if (id != typeEvent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeEvent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeEventExists(typeEvent.Id))
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
            return View(typeEvent);
        }

        // GET: TypeEvents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            return View(_context);
        }

        // POST: TypeEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typeEvent = await _context.TypeEvent.FindAsync(id);
            _context.TypeEvent.Remove(typeEvent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeEventExists(int id)
        {
            return _context.TypeEvent.Any(e => e.Id == id);
        }
    }
    */
    
