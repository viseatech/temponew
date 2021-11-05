using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using Visea_Expense_Manager.Data;
using Visea_Expense_Manager.Models;

namespace Visea_Expense_Manager.Controllersu
{
    public class EventsController : Controller
    {
        private readonly MvcNoteContext _context;

        public EventsController(MvcNoteContext context)
        {
            _context = context;
        }

        // GET: Events
        public async Task<IActionResult> Index()
        {
            IEnumerable<Event> listevent = Enumerable.Empty<Event>(); //_context.Event.Where(s => s.User.Contains(User.Identity.Name));
            IEnumerable<User> listuser = Enumerable.Empty<User>();
            IEnumerable<Client> listClient = _context.Client; 

            var user = await _context.Users.FirstOrDefaultAsync(m => m.Email.Contains(User.Identity.Name));
            Boolean cadmin = false;
            if (user.RoleId == 3)
            {
                listuser = _context.Users.OrderBy(user => user.Name);
                cadmin = true;
            }
            
            var tuple = new Tuple<IEnumerable<Event>, IEnumerable<User>,Boolean,MvcNoteContext>(listevent,listuser,cadmin,_context);

            return View(tuple);
            //return View(await _context.Event.Where(s => s.User.Contains(User.Identity.Name)).ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Index(string date_debut, string date_fin, string? idstr,string Type_Id,int Classe_Id, int Classe2_Id, int Classe3_Id, int Classe4_Id)
        {

            /*
            foreach (var entity in _context.Event)
                _context.Event.Remove(entity);
            _context.SaveChanges();
            */
            var user = await _context.Users.FirstOrDefaultAsync(m => m.Email.Contains(User.Identity.Name));
            System.Diagnostics.Debug.WriteLine("listecount:");
            System.Diagnostics.Debug.WriteLine(idstr);
            DateTime dateTimeDebut = new DateTime();
            DateTime dateTimeFin = new DateTime();
            int idint = user.Id;
            if (date_debut != null)
                dateTimeDebut = DateTime.Parse(date_debut);
            if (date_fin != null)
                dateTimeFin = DateTime.Parse(date_fin);
            Boolean adminull = false;
            if (idstr != null && idstr != "null")
            {
                idint = (int)Int64.Parse(idstr); 
            }
            else if (user.Role == "Administrator")
            {
                adminull = true;
            }

            IEnumerable<Event> listevent = _context.Event.Where(s => ( (adminull || s.UserId == idint) 
                                                                    && ( date_debut == null || dateTimeDebut <= s.Date )
                                                                    && ( date_fin == null || dateTimeFin >= s.Date)
                                                                    && ( Type_Id == null || Type_Id == s.Type)
                                                                    && ( Classe_Id  == 0 || Classe_Id == s.Classe_Id)
                                                                    && ( Classe2_Id == 0 || Classe2_Id == s.Classe2_Id)
                                                                    && ( Classe3_Id == 0 || Classe3_Id == s.Classe3_Id)
                                                                    && ( Classe4_Id == 0 || Classe4_Id == s.Classe4_Id)));

            IEnumerable<User> listuser = Enumerable.Empty<User>();
            Boolean cadmin = false;
            if (user.Role == "Administrator")
            {
                //listevent = _context.Event;
                listuser = _context.Users.OrderBy(user => user.Name);
                cadmin = true;
            }

            var tuple = new Tuple<IEnumerable<Event>, IEnumerable<User>, Boolean,MvcNoteContext>(listevent.OrderBy(evente => evente.Date), listuser, cadmin,_context);

            return View(tuple);
            //return View(await _context.Event.Where(s => s.User.Contains(User.Identity.Name)).ToListAsync());
        }
        public bool is_Valid_Month(DateTime datee)
        {
            var user = _context.Users.FirstOrDefault(m => m.Email.Contains(User.Identity.Name));
            var month = _context.MonthValidations.SingleOrDefault(m => m.UserId == user.Id && m.Date.Month == datee.Month && datee.Year == m.Date.Year);
            if (month == null)
            {
                return false;
            }
            return true;
        }
        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event
                .FirstOrDefaultAsync(m => m.Id == id);
            var user = await _context.Users.FirstOrDefaultAsync(m => m.Email.Contains(User.Identity.Name));
            if (@event == null || (@event.UserId != user.Id && user.RoleId != 3))
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            return NotFound();
            /*
            var client = from n in _context.Client select n;
            var mission = from n in _conteh,xt.Mission select n;
            var type_mis = from n in _context.Type_Mission select n;
            var etape_mis = from n in _context.Etape_Mission select n;
            var formation = from n in _context.Formation select n;
            var type_form = from n in _context.Type_formation select n;
            */
            Event evente = new Event();
            Client cliente = new Client();
            var tuple = new Tuple<Event, Client, MvcNoteContext>(evente, cliente, _context);

            return View(tuple);
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nature,Date,Date_of_creation,Type,Type_Id,Classe_Id,User,Heures,commente")] Event @event)
        {
            return NotFound();
            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@event);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event.FindAsync(id);
            var user = await _context.Users.FirstOrDefaultAsync(m => m.Email.Contains(User.Identity.Name));
            if (@event == null || (@event.UserId != user.Id && user.RoleId != 3))
            {
                return NotFound();
            }
            //return View(@event);
            var tuple = new Tuple<Event, MvcNoteContext>(@event, _context);
            return View(tuple);
        }


        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nature,Date,Type,Classe_Id,Classe2_Id,Classe3_Id,Classe4_Id,Heures,commente")] Event evente)
        {
            switch (evente.Type)
            {
                case "Clientèle":

                    var tmpc = _context.Client.Find(evente.Classe_Id);
                    var tmpm = _context.Mission.Find(evente.Classe2_Id);
                    var tmpt = _context.Type_Mission.Find(evente.Classe3_Id);
                    var tmpe = _context.Etape_Mission.Find(evente.Classe4_Id);
                    System.Diagnostics.Debug.WriteLine("Event classe 4");
                    System.Diagnostics.Debug.WriteLine(evente.Classe4_Id);
                    if (tmpc != null)
                        evente.Classe_str = tmpc.Name;
                    if (tmpm != null)
                        evente.Classe2_str = tmpm.Name;
                    if (tmpt != null)
                        evente.Classe3_str = tmpt.Name;
                    if (tmpe != null)
                        evente.Classe4_str = tmpe.Name;
                    break;
                case "Formation reçue":
                    var tmpf = _context.Formation.Find(evente.Classe_Id);
                    if (tmpf != null)
                        evente.Classe_str = tmpf.Name;
                    break;
                default:
                    break;
            }
            if (evente.Type != null)
                evente.Type_Id = (await _context.TypeEvent.FirstOrDefaultAsync(eventty => eventty.Name.Contains(evente.Type))).Id;
            evente.User = User.Identity.Name;
            evente.UserId = (await _context.Users.FirstOrDefaultAsync(m => m.Email.Contains(User.Identity.Name))).Id;
            evente.Date_of_creation = DateTime.Now;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(evente.Id))
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
            return View(evente);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Event.FindAsync(id);
            _context.Event.Remove(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.Event.Any(e => e.Id == id);
        }

    }
}
