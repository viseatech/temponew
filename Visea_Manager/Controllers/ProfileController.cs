using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Visea_Expense_Manager.Data;
using Visea_Expense_Manager.Models;
using ExcelDataReader;

namespace Visea_Expense_Manager.Controllers
{
    public class ProfileController : Controller
    {
        private readonly MvcNoteContext _context;

        public ProfileController(MvcNoteContext context)
        {
            _context = context;
        }
        // GET: Profile
        public ActionResult Index()
        {
            var user = _context.Users.FirstOrDefault(m => m.Email.Contains(User.Identity.Name));
            Boolean isadmine = true;
            if (user.RoleId != 3)
            {
                isadmine = false;
            }
            var tuple = new Tuple<User, Boolean>(user, isadmine);

            return View(tuple);
        }

        // GET: Profile/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            var userr = await _context.Users.FirstOrDefaultAsync(m => m.Email.Contains(User.Identity.Name));

            if (user == null || (user.RoleId != userr.RoleId && userr.RoleId != 3))
            {
                return NotFound();
            }

            return View(user);
        }
        public async Task<IActionResult> Liste()
        {
            var user = await _context.Users.FirstOrDefaultAsync(m => m.Email.Contains(User.Identity.Name));

            Boolean isadmine = true;
            if (user.RoleId != 3)
            {
                isadmine = false;
            }
            var tuple = new Tuple<IEnumerable<User>,Boolean>(await _context.Users.ToListAsync(),isadmine);

            return View(tuple);
        }

        // GET: Profile/Create
        public ActionResult Create()
        {
            return NotFound();
            return View();
        }

        // POST: Profile/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            return NotFound();
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Profile/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            var userr = await _context.Users.FirstOrDefaultAsync(m => m.Email.Contains(User.Identity.Name));

            if (user == null || (user.RoleId != userr.RoleId && userr.RoleId != 3))
            {
                return NotFound();
            }

            Boolean isadmine = true;
            if (userr.RoleId != 3 && userr.Email != "zlayadi@viseaconsulting.com")
            {
                isadmine = false;
            }
            var tuple = new Tuple<User, Boolean>(user,isadmine);
            return View(tuple);
        }
        public async Task<IActionResult> Editdeux(int? id)
        {
            return NotFound();
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        // POST: Profile/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,RoleId,Email,Director,Phone")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            switch(user.RoleId)
            {
                case 1:
                    user.Role = "Consultant";
                    break;
                case 2:
                    user.Role = "Director";
                    break;
                case 3:
                    user.Role = "Administrator";
                    break;
            }
           
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                    /*
                    if (!ProfileExists(zfile.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }*/
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editdeux(int id, [Bind("Id,Name,Role,Email,Director,Phone")] User user)
        {
            return NotFound();
            /*
            var userrole = await _context.Users.FirstOrDefaultAsync(m => m.Email.Contains(User.Identity.Name));
            if (userrole.Role != "Administrator")
            {
                return NotFound();
            }*/
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                    /*
                    if (!ProfileExists(zfile.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }*/
                    throw;
                }
                return RedirectToAction(nameof(Liste));
            }
            return View(user);
        }
        // GET: Profile/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var userrole = await _context.Users.FirstOrDefaultAsync(m => m.Email.Contains(User.Identity.Name));
            if (userrole.RoleId != 3)
            {
                return NotFound();
            }

            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }


        // POST: Profile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var users = await _context.Users.FirstOrDefaultAsync(m => m.Email.Contains(User.Identity.Name));
            if (users.RoleId != 3)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Liste));
        }

        public async Task<IActionResult> Flushdb(string code)
        {
            if (User.Identity.Name == "zlayadi@viseaconsulting.com" && code == "Jemangeunepomme1")
            {
                foreach (var entity in _context.Event)
                    _context.Event.Remove(entity);
                _context.SaveChanges();
                foreach (var entity in _context.TypeEvent)
                    _context.TypeEvent.Remove(entity);
                _context.SaveChanges();
                foreach (var entity in _context.Client)
                    _context.Client.Remove(entity);
                _context.SaveChanges();
                foreach (var entity in _context.Mission)
                    _context.Mission.Remove(entity);
                _context.SaveChanges();
                foreach (var entity in _context.Type_Mission)
                    _context.Type_Mission.Remove(entity);
                _context.SaveChanges();
                foreach (var entity in _context.Etape_Mission)
                    _context.Etape_Mission.Remove(entity);
                _context.SaveChanges();
                foreach (var entity in _context.Formation)
                    _context.Formation.Remove(entity);
                _context.SaveChanges();
                foreach (var entity in _context.Type_formation)
                    _context.Type_formation.Remove(entity);
                _context.SaveChanges();
                foreach (var entity in _context.Users)
                    if (entity.Email != User.Identity.Name)
                        _context.Users.Remove(entity);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Import(IFormFile file)
        {
   
            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                stream.Position = 0;
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    reader.Read();
                    while (reader.Read()) //Each row of the file
                    {


                        /*
                        if (Tligne.Length < 12)
                        {
                            System.Diagnostics.Debug.WriteLine("Ligne taille:");
                            System.Diagnostics.Debug.WriteLine(Tligne.Length);
                            for (int i = 0; i < Tligne.Length; i++)
                                System.Diagnostics.Debug.WriteLine(Tligne[i]);
                            System.Diagnostics.Debug.WriteLine(Tligne);
                            continue;
                        }*/
                        if (reader.FieldCount < 12)
                            continue;
                        if(reader.GetValue(0) == null || reader.GetValue(1) == null || reader.GetValue(2) == null || reader.GetValue(3) == null || reader.GetValue(4) == null || reader.GetValue(9) == null || reader.GetValue(11) == null)
                        {
                            continue;
                        }
                        var userexist = _context.Users.FirstOrDefault(m => m.Email.Contains(reader.GetValue(0).ToString().ToLower() + "@viseaconsulting.com"));

                        if (userexist == null)
                        {
                            User user = new User();
                            user.Name = reader.GetValue(2).ToString().ToLower() + " " + reader.GetValue(1).ToString();
                            user.Role = "Consultant";
                            user.RoleId = 1;
                            user.Email = reader.GetValue(0).ToString().ToLower() + "@viseaconsulting.com";
                            user.Director = "nodirector";

                            _context.Add(user);
                            _context.SaveChanges();
                            userexist = _context.Users.FirstOrDefault(m => m.Email.Contains(reader.GetValue(0).ToString().ToLower() + "@viseaconsulting.com"));
                        }

                        string nameval = reader.GetValue(4).ToString();
                        TypeEvent Type = await _context.TypeEvent.FirstOrDefaultAsync(m => m.Name == nameval);

                        if (Type == null)
                        {
                            _context.UpdateRange(
                                new TypeEvent
                                {
                                    Name = reader.GetValue(4).ToString(),
                                }
                            );
                            _context.SaveChanges();
                            Type = await _context.TypeEvent.FirstOrDefaultAsync(m => m.Name == reader.GetValue(4).ToString());
                        }

                        if (Type.Name == "Clientèle" && reader.GetValue(5) != null && reader.GetValue(6) != null && reader.GetValue(7) != null && reader.GetValue(8) != null)
                        {
                            var Classe1 = await _context.Client.FirstOrDefaultAsync(m => m.Name == reader.GetValue(5).ToString());

                            if (Classe1 == null)
                            {
                                _context.UpdateRange(
                                    new Client
                                    {
                                        Name = reader.GetValue(5).ToString(),
                                        Type_Id = Type.Id,
                                        Type = Type.Name,
                                    }
                                );
                                _context.SaveChanges();
                                Classe1 = await _context.Client.FirstOrDefaultAsync(m => m.Name == reader.GetValue(5).ToString());
                            }


                            var Classe2 = await _context.Mission.FirstOrDefaultAsync(m => m.Name == reader.GetValue(6).ToString() && m.Client_Id == Classe1.Id);

                            if (Classe2 == null)
                            {
                                _context.AddRange(
                                    new Mission
                                    {
                                        Name = reader.GetValue(6).ToString(),
                                        Client_Id = Classe1.Id,
                                    }
                                );
                                _context.SaveChanges();
                                Classe2 = await _context.Mission.FirstOrDefaultAsync(m => m.Name == reader.GetValue(6).ToString() && m.Client_Id == Classe1.Id);
                            }


                            var Classe3 = await _context.Type_Mission.FirstOrDefaultAsync(m => m.Name == reader.GetValue(7).ToString() && m.Mission_Id == Classe2.Id);

                            if (Classe3 == null)
                            {
                                _context.AddRange(
                                    new Type_Mission
                                    {
                                        Name = reader.GetValue(7).ToString(),
                                        Mission_Id = Classe2.Id,
                                    }
                                );
                                _context.SaveChanges();
                                Classe3 = await _context.Type_Mission.FirstOrDefaultAsync(m => m.Name == reader.GetValue(7).ToString() && m.Mission_Id == Classe2.Id);
                            }


                            var Classe4 = await _context.Etape_Mission.FirstOrDefaultAsync(m => m.Name == reader.GetValue(8).ToString() && m.type_Mission_Id == Classe3.Id);

                            if (Classe4 == null)
                            {
                                _context.AddRange(
                                    new Etape_Mission
                                    {
                                        Name = reader.GetValue(8).ToString(),
                                        type_Mission_Id = Classe3.Id,
                                    }
                                );
                                _context.SaveChanges();
                                Classe4 = await _context.Etape_Mission.FirstOrDefaultAsync(m => m.Name == reader.GetValue(8).ToString() && m.type_Mission_Id == Classe3.Id);
                            }

                            var now = DateTime.Now;
                            DateTime dateValue = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
                            //Tligne = ligne.Split(splitter);
                            decimal tmpdecimal = new decimal(0);
                           
                            tmpdecimal = new decimal(float.Parse(reader.GetValue(11).ToString()));
                            TimeSpan span = TimeSpan.FromHours((double)tmpdecimal);
                            //dateValue.AddHours(float.Parse(reader.GetValue(11).ToString()));
                            DateTime datee = DateTime.Parse(reader.GetValue(3).ToString());
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
                                    User = reader.GetValue(0).ToString().ToLower() + "@viseaconsulting.com",
                                    UserId = userexist.Id,
                                    Heures = dateValue + span,
                                    commente = com,
                                }
                            );


                        }

                        else if (Type.Name == "Formation reçue" && reader.GetValue(4) != null )
                        {
                            var Classe1 = await _context.Formation.FirstOrDefaultAsync(m => m.Name == reader.GetValue(4).ToString());

                            if (Classe1 == null)
                            {
                                _context.UpdateRange(
                                    new Formation
                                    {
                                        Name = reader.GetValue(4).ToString(),
                                        Type_Id = Type.Id,
                                    }
                                );
                                _context.SaveChanges();
                                Classe1 = await _context.Formation.FirstOrDefaultAsync(m => m.Name == reader.GetValue(4).ToString());
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
                            var now = DateTime.Now;
                            DateTime dateValue = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
                            //Tligne = ligne.Split(splitter);
                            decimal tmpdecimal = new decimal(0);
                            //if (Tligne.Length > 11)
                            tmpdecimal = new decimal(float.Parse(reader.GetValue(11).ToString()));
                            TimeSpan span = TimeSpan.FromHours((double)tmpdecimal);
                            //dateValue.AddHours(float.Parse(reader.GetValue(11).ToString()));
                            DateTime datee = DateTime.Parse(reader.GetValue(3).ToString());
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
                                    User = reader.GetValue(0).ToString().ToLower() + "@viseaconsulting.com",
                                    UserId = userexist.Id,
                                    Heures = dateValue + span,
                                    commente = com,
                                }
                            );
                        }
                        else
                        {
                            var now = DateTime.Now;
                            DateTime dateValue = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
                            //Tligne = ligne.Split(splitter);
                            decimal tmpdecimal = new decimal(0);
                            //if (Tligne.Length > 11)
                            tmpdecimal = new decimal(float.Parse(reader.GetValue(11).ToString()));
                            TimeSpan span = TimeSpan.FromHours((double)tmpdecimal);
                            //dateValue.AddHours(float.Parse(reader.GetValue(11).ToString()));
                            DateTime datee = DateTime.Parse(reader.GetValue(3).ToString());
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
                                    User = reader.GetValue(0).ToString().ToLower() + "@viseaconsulting.com",
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
            return RedirectToAction(nameof(Index));
        }
    }
}