using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Visea_Expense_Manager.Data;
using Visea_Expense_Manager.Models;

namespace Visea_Expense_Manager.Controllers
{
    public class CongersController : Controller
    {
        private readonly MvcNoteContext _context;

        public CongersController(MvcNoteContext context)
        {
            _context = context;
        }

        // GET: Congers
        public async Task<IActionResult> Index()
        {
            var congers = from n in _context.Conger select n;
            congers = congers.Where(s => s.User.Contains(User.Identity.Name));
            var tuple = new Tuple<IEnumerable<Conger>, User>(congers, await _context.Users.FirstOrDefaultAsync(m => m.Email.Contains(User.Identity.Name)));

            return View(tuple);
        }

        public async Task<IActionResult> teletravail()
        {
            var teletravail = from n in _context.teletravail select n;
            var users = from n in _context.Users select n;
            var tuple = new Tuple<IEnumerable<Teletravail>, IEnumerable<User>>(teletravail, users);

            return View(tuple);
        }

        public async Task<IActionResult> All()
        {
            var user = await _context.Users.FirstOrDefaultAsync(m => m.Email.Contains(User.Identity.Name));
            if (user.RoleId != 3)
            {
                return NotFound();
            }
            var conger = from n in _context.Conger select n;

            return View(await conger.ToListAsync());
        }

        // GET: Congers/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var conger = await _context.Conger
                .FirstOrDefaultAsync(m => m.Id == id);
            var user = await _context.Users.FirstOrDefaultAsync(m => m.Email.Contains(User.Identity.Name));
            if (conger == null || (conger.User != user.Email && user.RoleId != 3))
            {
                return NotFound();
            }
            var tuple = new Tuple<Conger, IEnumerable<Zfile>>(conger, await _context.Zfile.ToListAsync());

            return View(tuple);
        }

        // GET: Congers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Congers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Date_Debut,Demijourne_Debut,Date_Fin,Demijourne_Fin,Type,commente")] Conger conger, List<IFormFile> listfile, List<string> responsable)
        {
            System.Diagnostics.Debug.WriteLine("listecount:");
            System.Diagnostics.Debug.WriteLine(listfile.Count);

            if ((conger.Date_Debut > conger.Date_Fin) || ((conger.Date_Debut == conger.Date_Fin) && conger.Demijourne_Debut == conger.Demijourne_Fin))
            {
                return View(conger);
            }

            TimeSpan diff = conger.Date_Fin.Subtract(conger.Date_Debut);
            conger.time = diff.Days;
            if ((conger.Demijourne_Debut == 1 && conger.Demijourne_Fin == 3))
                conger.time += 1;
            else if (conger.Demijourne_Debut != conger.Demijourne_Fin)
                conger.time += 0.5f;


            conger.State = "En attente";
            var user = await _context.Users.FirstOrDefaultAsync(m => m.Email.Contains(User.Identity.Name));

            conger.User = user.Email;
            conger.UserId = user.Id;
            conger.Director = user.Director;

            if (ModelState.IsValid)
            {
                _context.Add(conger);
                await _context.SaveChangesAsync();
                await FileUpload(listfile, conger.Id);
                await _context.SaveChangesAsync();
                Sendzmail(responsable);

                return RedirectToAction(nameof(Index));
            }
            return View(conger);
        }

        [HttpPost]
        public async Task<IActionResult> FileUpload(List<IFormFile> listfile, int iid)
        {
            var conger = await _context.Conger.FindAsync(iid);
            if (conger == null)
            {
                return NotFound();
            }

            System.Diagnostics.Debug.WriteLine("title:");
            // System.Diagnostics.Debug.WriteLine(listfile[0].FileName);
            System.Diagnostics.Debug.WriteLine(conger.commente);
            System.Diagnostics.Debug.WriteLine(listfile.Count);
            long size = listfile.Sum(f => f.Length);

            var filePaths = new List<string>();
            foreach (var formFile in listfile)
            {
                if (formFile.Length > 0)
                {
                    Random aleatoir = new Random();
                    string name = aleatoir.Next(10000) + "_" + formFile.FileName;
                    // full path to file in temp location
                    var filePath = ".\\wwwroot\\Files\\" + name; // \\Files\\ Path.GetTempFileName(); //we are using Temp file name just for the example. Add your own file path.
                    filePaths.Add(filePath);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                    Zfile zfile = new Zfile();
                    zfile.note = conger.Title;
                    zfile.NoteId = conger.Id;
                    zfile.Name = name;
                    zfile.path = "~/Files/" + name;
                    zfile.User = conger.User;

                    _context.Add(zfile);
                    await _context.SaveChangesAsync();
                }
            }

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.
            //if (edit > 0)
            return RedirectToAction(nameof(Edit), new { id = conger.Id });
            //return Ok(new { count = listfile.Count, size, filePaths });
        }
        // GET: Congers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conger = await _context.Conger.FindAsync(id);
            var user = await _context.Users.FirstOrDefaultAsync(m => m.Email.Contains(User.Identity.Name));
            if (conger == null || (conger.User != user.Email && user.RoleId != 3))
            {
                return NotFound();
            }

            var tuple = new Tuple<Conger, IEnumerable<Zfile>>(conger, await _context.Zfile.ToListAsync());

            return View(tuple);
        }

        // POST: Congers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Date_Debut,Demijourne_Debut,Date_Fin,Demijourne_Fin,Type,User,UserId,Director,time,State,commente")] Conger conger)
        {
            if (id != conger.Id)
            {
                return NotFound();
            }

            if ((conger.Date_Debut > conger.Date_Fin) || ((conger.Date_Debut == conger.Date_Fin) && conger.Demijourne_Debut == conger.Demijourne_Fin))
            {
                return View(conger);
            }

            TimeSpan diff = conger.Date_Fin.Subtract(conger.Date_Debut);
            conger.time = diff.Days;
            if ((conger.Demijourne_Debut == 1 && conger.Demijourne_Fin == 3))
                conger.time += 1;
            else if (conger.Demijourne_Debut != conger.Demijourne_Fin)
                conger.time += 0.5f;


            if (ModelState.IsValid && (conger.Date_Debut <= conger.Date_Fin))
            {
                try
                {
                    _context.Update(conger);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CongerExists(conger.Id))
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
            var tuple = new Tuple<Conger, IEnumerable<Zfile>>(conger, await _context.Zfile.ToListAsync());

            return View(tuple);
        }

        // GET: Congers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conger = await _context.Conger
                .FirstOrDefaultAsync(m => m.Id == id);
            var user = await _context.Users.FirstOrDefaultAsync(m => m.Email.Contains(User.Identity.Name));
            if (conger == null || (conger.User != user.Email && user.RoleId != 3))
            {
                return NotFound();
            }

            return View(conger);
        }

        // POST: Congers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var conger = await _context.Conger.FindAsync(id);
            foreach (var entity in _context.Zfile)
            {
                if (entity.NoteId == conger.Id)
                {
                    string fullPath = ".\\wwwroot\\Files\\" + entity.Name;
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                    _context.Zfile.Remove(entity);
                }
            }
            _context.Conger.Remove(conger);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CongerExists(int id)
        {
            return _context.Conger.Any(e => e.Id == id);
        }
        [HttpPost, ActionName("Deleteimg")]
        public async Task<IActionResult> Deleteimg(int iid, int zid)
        {
            var zfile = await _context.Zfile.FindAsync(zid);

            string fullPath = ".\\wwwroot\\Files\\" + zfile.Name;
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            _context.Zfile.Remove(zfile);
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Edit), new { id = iid });
        }

        [HttpPost]
        public async Task<IActionResult> Valider(int iid)
        {
            var user = await _context.Users.FirstOrDefaultAsync(m => m.Email.Contains(User.Identity.Name));
            if (user.RoleId != 3)
            {
                return NotFound();
            }
            var conger = await _context.Conger.FindAsync(iid);
            if (conger == null)
            {
                return NotFound();
            }
            conger.State = "Valider";
            _context.Conger.Update(conger);
            await _context.SaveChangesAsync();
            if (conger.time > 0)
            {
                TimeSpan spanmid = TimeSpan.FromHours(12);
                DateTime tmpd = conger.Date_Debut;
                DateTime tmpf = conger.Date_Fin;
                Double tmpt = conger.time;

                TimeSpan span = TimeSpan.FromHours(4);
                if (conger.Demijourne_Debut == 2)
                {
                    //tmpd -= spanmid;
                    _context.UpdateRange(
                                new Event
                                {
                                    Date = tmpd,
                                    Date_of_creation = DateTime.Now.Date,
                                    Nature = conger.Type,
                                    User = conger.User,
                                    UserId = conger.UserId,
                                    Heures = Convert.ToDateTime(span.ToString()),
                                    commente = conger.commente,
                                }
                            );
                    await _context.SaveChangesAsync();
                    tmpt -= 0.5;
                    tmpd = tmpd.AddDays(1.0);

                }
                if (conger.Demijourne_Fin == 2)
                {
                    //tmpf -= spanmid;    
                    _context.UpdateRange(
                                new Event
                                {
                                    Date = tmpf,
                                    Date_of_creation = DateTime.Now.Date,
                                    Nature = conger.Type,
                                    User = conger.User,
                                    UserId = conger.UserId,
                                    Heures = Convert.ToDateTime(span.ToString()),
                                    commente = conger.commente,
                                }
                            );
                    await _context.SaveChangesAsync();
                    tmpt -= 0.5;

                }
                while (tmpt >= 1)
                {
                    TimeSpan spanh = TimeSpan.FromHours(8);
                    _context.UpdateRange(
                                new Event
                                {
                                    Date = tmpd,
                                    Date_of_creation = DateTime.Now.Date,
                                    Nature = conger.Type,
                                    User = conger.User,
                                    UserId = conger.UserId,
                                    Heures = Convert.ToDateTime(spanh.ToString()),
                                    commente = conger.commente,
                                }
                            );
                    await _context.SaveChangesAsync();
                    tmpd = tmpd.AddDays(1.0);
                    tmpt -= 1;
                }
            }


            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Rejeter(int iid)
        {
            var user = await _context.Users.FirstOrDefaultAsync(m => m.Email.Contains(User.Identity.Name));
            if (user.RoleId != 3)
            {
                return NotFound();
            }
            var note = await _context.Conger.FindAsync(iid);
            if (note == null)
            {
                return NotFound();
            }
            note.State = "Rejeter";
            _context.Conger.Update(note);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(All));
        }

        static bool mailSent = false;
        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            String token = (string)e.UserState;

            if (e.Cancelled)
            {
                Console.WriteLine("[{0}] Send canceled.", token);
            }
            if (e.Error != null)
            {
                Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
            }
            else
            {
                Console.WriteLine("Message sent.");
            }
            mailSent = true;
        }

        public void Sendzmail(List<string> responsable)
        {
            var user = _context.Users.FirstOrDefault(m => m.Email.Contains(User.Identity.Name));
            // Command-line argument must be the SMTP host.
            SmtpClient client = new SmtpClient("smtp.office365.com", 587);
            client.EnableSsl = true;
            client.Credentials = new System.Net.NetworkCredential("zlayadi@viseaconsulting.com", "Aezretry1");            // Specify the email sender.
            // Create a mailing address that includes a UTF8 character
            // in the display name.
            MailAddress from = new MailAddress(user.Email,
               user.Name,
            System.Text.Encoding.UTF8);
            foreach (string mailto in responsable)
            {
                // Set destinations for the email message.
                MailAddress to = new MailAddress(mailto);
                // Specify the message content.
                MailMessage message = new MailMessage(from, to);
                message.Body = "Ceci est un mail automatique de tempo pour une demande d'absence de " + user.Name;

                message.Body += Environment.NewLine;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.Subject = "Tempo demande d'absence";
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                // Set the method that is called back when the send operation ends.
                client.SendCompleted += new
                SendCompletedEventHandler(SendCompletedCallback);
                // The userState can be any object that allows your callback
                // method to identify this send operation.
                // For this example, the userToken is a string constant.
                //string userState = "test message1";
                client.Send(message);
                // Console.WriteLine("Sending message... press c to cancel mail. Press any other key to exit.");
                //string answer = Console.ReadLine();
                // If the user canceled the send, and mail hasn't been sent yet,
                // then cancel the pending operation.
                //if (answer.StartsWith("c") && mailSent == false)
                //{
                //    client.SendAsyncCancel();
                //}
                // Clean up.
                message.Dispose();
            }
        }

        [HttpPost]
        public int AddteleTravail(Teletravail teletravail)
        {
            
            var user = _context.Users.FirstOrDefault(m => m.Email.Contains(User.Identity.Name));
            if (teletravail == null || (teletravail.User != user.Email && user.RoleId != 3))
            {
                return 0;
            }
            else if (ModelState.IsValid && (teletravail.start <= teletravail.end))
            {
                teletravail.UserId = user.Id;
                _context.Update(teletravail);
                _context.SaveChanges();
            }
            return user.Id;
            
            /*
            if ((conger.Date_Debut > conger.Date_Fin) || ((conger.Date_Debut == conger.Date_Fin) && conger.Demijourne_Debut == conger.Demijourne_Fin))
            {
                return View(conger);
            }*/

        }
        public async Task<IActionResult> DeleteTeletravail (int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conger = await _context.Conger
                .FirstOrDefaultAsync(m => m.Id == id);
            var user = await _context.Users.FirstOrDefaultAsync(m => m.Email.Contains(User.Identity.Name));
            if (conger == null || (conger.User != user.Email && user.RoleId != 3))
            {
                return NotFound();
            }

            return View(conger);
        }
    }
}
