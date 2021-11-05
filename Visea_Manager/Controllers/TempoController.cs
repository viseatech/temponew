    using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using Visea_Expense_Manager.Data;
using Visea_Expense_Manager.Models;

namespace Visea_Expense_Manager.Controllers
{
    public class TempoController : Controller
    {
        private readonly MvcNoteContext _context;

        public TempoController(MvcNoteContext context)
        {
            _context = context;
        }
        public IActionResult Index(string date, List<int> listoffail)
        {
            DateTime datee = new DateTime();
            if (date == null)
                datee = DateTime.Now;
            else
                datee = DateTime.Parse(date);
            var tuple = new Tuple<MvcNoteContext, DateTime,IEnumerable<int>>(_context, datee,listoffail);
            return View(tuple);
        }

        [HttpGet]
        public IActionResult Create(String datepicker2)
        {
            System.Diagnostics.Debug.WriteLine("date:");
            System.Diagnostics.Debug.WriteLine(datepicker2);
            Event evente = new Event();
            Client cliente = new Client();
            DateTime datee = DateTime.Parse(datepicker2);
            var tuple = new Tuple<Event, Client, MvcNoteContext,DateTime>(evente, cliente, _context,datee);

            return PartialView(tuple);
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nature,Date,Type,Classe_Id,Classe2_Id,Classe3_Id,Classe4_Id,Heures,commente")] Event evente)
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
                _context.Add(evente);
                await _context.SaveChangesAsync();

                return new ContentResult
                {
                    ContentType = "text/html",
                    StatusCode = (int)HttpStatusCode.OK,
                    Content = "<html><script>parent.ClosePopup();\nlocation.href = location.href + \"?datepicker2=" + evente.Date.ToShortDateString()+"\";</script></ html>"
                };
            }
            Client cliente = new Client();
            DateTime datee = evente.Date; 
            var tuple = new Tuple<Event, Client, MvcNoteContext, DateTime>(evente, cliente, _context, datee);

            return PartialView(tuple);
        }

        [HttpPost]
        public int Add(int number1, int number2)
        {
            return number1 + number2;
        }

        [HttpGet]
        public JsonResult getlistbydate(string datee)
        {
            System.Diagnostics.Debug.WriteLine("getlistbtdate");
            DateTime convertion = DateTime.Parse(datee);
            User userexist = _context.Users.FirstOrDefault(m => m.Email.Contains(User.Identity.Name));
            System.Diagnostics.Debug.WriteLine("day");
            System.Diagnostics.Debug.WriteLine(convertion.Day);
            
            System.Diagnostics.Debug.WriteLine("Monthe");
            System.Diagnostics.Debug.WriteLine(convertion.Month);
            
            System.Diagnostics.Debug.WriteLine("Year");
            System.Diagnostics.Debug.WriteLine(convertion.Year);
            
            System.Diagnostics.Debug.WriteLine("Id");
            System.Diagnostics.Debug.WriteLine(userexist.Id);
            
            List<Event> list_of_event_in_date = _context.Event.Where(m => m.Date.Day == convertion.Day && m.Date.Month == convertion.Month && m.Date.Year == convertion.Year && m.UserId == userexist.Id).ToList();
            return Json(list_of_event_in_date);
        }
        public JsonResult getlistbyMonth(string datee)
        {
            System.Diagnostics.Debug.WriteLine("getlistbtmONTH");
            DateTime convertion = DateTime.Parse(datee);
            User userexist = _context.Users.FirstOrDefault(m => m.Email.Contains(User.Identity.Name));
            System.Diagnostics.Debug.WriteLine("day");
            System.Diagnostics.Debug.WriteLine(convertion.Day);

            System.Diagnostics.Debug.WriteLine("Monthe");
            System.Diagnostics.Debug.WriteLine(convertion.Month);

            System.Diagnostics.Debug.WriteLine("Year");
            System.Diagnostics.Debug.WriteLine(convertion.Year);

            System.Diagnostics.Debug.WriteLine("Id");
            System.Diagnostics.Debug.WriteLine(userexist.Id);

            List<Event> list_of_event_in_date = _context.Event.Where(m => m.Date.Month == convertion.Month && m.Date.Year == convertion.Year && m.UserId == userexist.Id).ToList();
            return Json(list_of_event_in_date);
        }

        [HttpPost]
        public async Task<IActionResult> Valid_Month(String datepicker3)
        {
            System.Diagnostics.Debug.WriteLine("date:");
            System.Diagnostics.Debug.WriteLine(datepicker3);
            var user = _context.Users.FirstOrDefault(m => m.Email.Contains(User.Identity.Name));
            
            MonthValidation new_month = new MonthValidation();
            DateTime datee = DateTime.Parse(datepicker3);
            new_month.Date = datee;
            new_month.UserId = user.Id;
            new_month.Userstr = user.Name; 
            _context.Add(new_month);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public bool is_Valid_Month(String datepicker3)
        {
            System.Diagnostics.Debug.WriteLine("date:");
            System.Diagnostics.Debug.WriteLine(datepicker3);
            DateTime datee = DateTime.Parse(datepicker3);
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
            if (@event == null)
            {
                return NotFound();
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
            var tuple = new Tuple<Event,MvcNoteContext>(@event, _context);
            return PartialView(tuple);
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

                return new ContentResult
                {
                    ContentType = "text/html",
                    StatusCode = (int)HttpStatusCode.OK,
                    Content = "<html><script>parent.ClosePopup();\nlocation.href = location.href + \"?datepicker2=" + evente.Date.ToShortDateString() + "\";</script></ html>"
                };
            }
            var tuple = new Tuple<Event, MvcNoteContext>(evente, _context);
            return PartialView(tuple);
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
            var user = await _context.Users.FirstOrDefaultAsync(m => m.Email.Contains(User.Identity.Name));
            if (@event == null || (@event.UserId != user.Id && user.RoleId != 3))
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
        /*
        public async Task<IActionResult> Import(IFormFile file)
        {
            /*
            foreach (var entity in context.SAPusers)
                context.SAPusers.Remove(entity);
            context.SaveChanges();
            /
            string[] Tligne = new string[100]; // tableau qui va contenir les sous-chaines extraites d'une ligne.
            char[] splitter = { ';' }; // délimiteur du fichier texte
            //string fic_serveur = "C:\\Users\\ziane\\source\\repos\\Visea_User_Tools\\export_user\\CtxUser.txt"; // chaine qui contient le nom du fichier csv à ouvrir

            StreamReader line = new StreamReader(file.OpenReadStream());
            string ligne;
            //line.ReadLine();

            while ((ligne = line.ReadLine()) != null)
            {
                DateTime dateValue = new DateTime();
                Tligne = ligne.Split(splitter);
                dateValue.AddHours(float.Parse(Tligne[7]));

                _context.UpdateRange(
                    new Event
                    {
                        Date = DateTime.Now.Date,
                        Nature = Tligne[0],
                        Type = "other",
                        Type_Id = 3,
                        User = User.Identity.Name,
                        Heures = dateValue,
                        commente = "Status " + Tligne[1] + " Priority " + Tligne[2] + " Contact " + Tligne[9] + " " + Tligne[10],
                    }
                );

            }
            line.Close();

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        */

        public async void Copier(int eventid)
        {
            System.Diagnostics.Debug.WriteLine("Copier:");
            System.Diagnostics.Debug.WriteLine(eventid);
            
            User currentprofil = _context.Users.FirstOrDefault(m => m.Email.Contains(User.Identity.Name));
            currentprofil.CopierId = eventid;
            _context.Update(currentprofil); 
            _context.SaveChanges();
        }

        public async void Coller(string datecoller)
        {
            System.Diagnostics.Debug.WriteLine("Coller:");

            System.Diagnostics.Debug.WriteLine(datecoller);

            User currentprofil = _context.Users.FirstOrDefault(m => m.Email.Contains(User.Identity.Name));
            System.Diagnostics.Debug.WriteLine(currentprofil.CopierId);

            Event CopierEvent = _context.Event.Find(currentprofil.CopierId);
            if (CopierEvent != null)
            {
                Event collerevent = new Event();
                collerevent.Date = DateTime.Parse(datecoller);
                collerevent.Date_of_creation = DateTime.Now;
                collerevent.Nature = CopierEvent.Nature;
                collerevent.Heures = CopierEvent.Heures;
                collerevent.Type = CopierEvent.Type;
                collerevent.Type_Id = CopierEvent.Type_Id;
                collerevent.User = CopierEvent.User;
                collerevent.UserId = CopierEvent.UserId;
                collerevent.Classe_Id = CopierEvent.Classe_Id;
                collerevent.Classe2_Id = CopierEvent.Classe2_Id;
                collerevent.Classe3_Id = CopierEvent.Classe3_Id;
                collerevent.Classe4_Id = CopierEvent.Classe4_Id;
                collerevent.Classe_str = CopierEvent.Classe_str;
                collerevent.Classe2_str = CopierEvent.Classe2_str;
                collerevent.Classe3_str = CopierEvent.Classe3_str;
                collerevent.Classe4_str = CopierEvent.Classe4_str;
                collerevent.commente = CopierEvent.commente;
                _context.Add(collerevent);
                _context.SaveChanges();
            }

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
        public async Task<IActionResult> Import(IFormFile file)
        {
            if (file == null)
                return RedirectToAction(nameof(Index));
            var listoffails = new List<int>();
            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                stream.Position = 0;
                int idligne = 1;
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    
                    reader.Read();
                    while (reader.Read()) //Each row of the file
                    {
                        idligne++;
                        var now = DateTime.Now;
                        DateTime dateValue = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
                        //Tligne = ligne.Split(splitter);
                        decimal tmpdecimal = new decimal(0);
                        //if (Tligne.Length > 11)
                        tmpdecimal = new decimal(float.Parse(reader.GetValue(11).ToString()));
                        TimeSpan span = TimeSpan.FromHours((double)tmpdecimal);
                        //dateValue.AddHours(float.Parse(reader.GetValue(11).ToString()));
                        DateTime datee = DateTime.Parse(reader.GetValue(3).ToString());
                        if (is_Valid_Month(datee))
                        {
                            listoffails.Add(idligne);
                            continue;
                        }
                        if (reader.FieldCount < 12)
                            continue;
                        if (reader.GetValue(0) == null || reader.GetValue(1) == null || reader.GetValue(2) == null || reader.GetValue(3) == null || reader.GetValue(4) == null || reader.GetValue(9) == null || reader.GetValue(11) == null)
                        {
                            listoffails.Add(idligne);
                            continue;
                        }

                        var userexist = _context.Users.FirstOrDefault(m => m.Email.Contains(User.Identity.Name));

                        if (userexist == null)
                        {
                            listoffails.Add(idligne);
                            continue;
                        }

                        string nameval = reader.GetValue(4).ToString();
                        TypeEvent Type = await _context.TypeEvent.FirstOrDefaultAsync(m => m.Name == nameval);

                        if (Type == null)
                        {
                            listoffails.Add(idligne);
                            continue;
                        }

                        if (Type.Name == "Clientèle" && reader.GetValue(5) != null && reader.GetValue(6) != null && reader.GetValue(7) != null && reader.GetValue(8) != null)
                        {
                            var Classe1 = await _context.Client.FirstOrDefaultAsync(m => m.Name == reader.GetValue(5).ToString());

                            if (Classe1 == null)
                            {
                                listoffails.Add(idligne);
                                continue;
                            }


                            var Classe2 = await _context.Mission.FirstOrDefaultAsync(m => m.Name == reader.GetValue(6).ToString());

                            if (Classe2 == null)
                            {
                                listoffails.Add(idligne);
                                continue;
                            }


                            var Classe3 = await _context.Type_Mission.FirstOrDefaultAsync(m => m.Name == reader.GetValue(7).ToString());

                            if (Classe3 == null)
                            {
                                listoffails.Add(idligne);
                                continue;
                            }


                            var Classe4 = await _context.Etape_Mission.FirstOrDefaultAsync(m => m.Name == reader.GetValue(8).ToString());

                            if (Classe4 == null)
                            {
                                listoffails.Add(idligne);
                                continue;
                            }


                            string com = "";
                            if (reader.GetValue(10) != null)
                                com = reader.GetValue(10).ToString();
                            _context.UpdateRange(
                                new Event
                                {
                                    Date = datee,
                                    Date_of_creation = DateTime.Now.Date,
                                    Nature = reader.GetValue(9).ToString(),
                                    Type = Type.Name,
                                    Type_Id = Type.Id,
                                    Classe_str = Classe1.Name,
                                    Classe_Id = Classe1.Id,
                                    Classe2_str = Classe2.Name,
                                    Classe2_Id = Classe2.Id,
                                    Classe3_str = Classe3.Name,
                                    Classe3_Id = Classe3.Id,
                                    Classe4_str = Classe4.Name,
                                    Classe4_Id = Classe4.Id,
                                    User = userexist.Email,
                                    UserId = userexist.Id,
                                    Heures = dateValue + span,
                                    commente = com,
                                }
                            );
                        }

                        else if (Type.Name == "Formation reçue" && reader.GetValue(4) != null)
                        {
                            var Classe1 = await _context.Formation.FirstOrDefaultAsync(m => m.Name == reader.GetValue(4).ToString());

                            if (Classe1 == null)
                            {
                                listoffails.Add(idligne);
                                continue;
                            }
                            /*
                            var Classe2 = await _context.Type_formation.FirstOrDefaultAsync(m => m.Name == reader.GetValue(4).ToString());

                            if (Classe2 == null)
                            {
                                _context.UpdateRange(
                                    new Type_formation
                                    {
                                        Name = reader.GetValue(4).ToString(),
                                        formation_Id = Classe1.Id,
                                    }
                                );
                                _context.SaveChanges();
                                Classe2 = await _context.Type_formation.FirstOrDefaultAsync(m => m.Name == reader.GetValue(4).ToString());
                            }
                            */

                            string com = "";
                            if (reader.GetValue(10) != null)
                                com = reader.GetValue(10).ToString();
                            _context.UpdateRange(
                                new Event
                                {
                                    Date = datee,
                                    Date_of_creation = DateTime.Now.Date,
                                    Nature = reader.GetValue(9).ToString(),
                                    Type = Type.Name,
                                    Type_Id = Type.Id,
                                    Classe_str = Classe1.Name,
                                    Classe_Id = Classe1.Id,
                                    User = userexist.Email,
                                    UserId = userexist.Id,
                                    Heures = dateValue + span,
                                    commente = com,
                                }
                            );
                        }
                        else
                        {
                            string com = "";
                            if (reader.GetValue(10) != null)
                                com = reader.GetValue(10).ToString();
                            _context.UpdateRange(
                                new Event
                                {
                                    Date = datee,
                                    Date_of_creation = DateTime.Now.Date,
                                    Nature = reader.GetValue(9).ToString(),
                                    Type = Type.Name,
                                    Type_Id = Type.Id,
                                    User = userexist.Email,
                                    UserId = userexist.Id,
                                    Heures = dateValue + span,
                                    commente = com,
                                }
                            );
                        }
                    }
                }
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Tempo", new
                    {
                        listoffail = listoffails
                    });
        }

    }
}