using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Visea_Expense_Manager.Data;
using Visea_Expense_Manager.Models;
using System.Security.Principal;
using System.Threading;
using System.Globalization;

namespace Visea_Expense_Manager.Controllers
{
    public class NotesController : Controller
    {
        private readonly MvcNoteContext _context;

        public NotesController(MvcNoteContext context)
        {
            _context = context;
        }

        // GET: Notes
        public async Task<IActionResult> Index(string searchString)
        {
            var notes = from n in _context.Note select n; 
            if (!String.IsNullOrEmpty(searchString))
            {
                notes = notes.Where(s => s.Title.Contains(searchString));
            }

            notes = notes.Where(s => s.User.Contains(User.Identity.Name));
            var tuple = new Tuple<IEnumerable<Note>, User>(notes, await _context.Users.FirstOrDefaultAsync(m => m.Email.Contains(User.Identity.Name)));

            return View(tuple);
        }

        public async Task<IActionResult> All()
        {
            var user = await _context.Users.FirstOrDefaultAsync(m => m.Email.Contains(User.Identity.Name));
            if (user.RoleId != 3)
            {
                return NotFound();
            }
            var notes = from n in _context.Note select n;

            return View(await notes.ToListAsync());
        }

        // GET: Notes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await _context.Note
                .FirstOrDefaultAsync(m => m.Id == id);
            var user = await _context.Users.FirstOrDefaultAsync(m => m.Email.Contains(User.Identity.Name));
            if (note == null || (note.User != user.Email && user.RoleId != 3))
            {
                return NotFound();
            }
            var tuple = new Tuple<Note, IEnumerable<Zfile>>(note, await _context.Zfile.ToListAsync());

            return View(tuple);
        }

        // GET: Notes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Notes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Date,Type,Price,Prix,commente")] Note note, List<IFormFile> listfile)
        {
           // System.Diagnostics.Debug.WriteLine("title:");
           // System.Diagnostics.Debug.WriteLine(note.Title);
            note.Files = listfile;
            note.User = User.Identity.Name;
            note.State = "En attente";
            //note.Prix = "10.00"; 
            if (ModelState.IsValid)
            {
                _context.Add(note);
                await _context.SaveChangesAsync();
                await FileUpload(note.Files, note.Id);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(note);
        }

        [HttpPost]
        public async Task<IActionResult> FileUpload(List<IFormFile> listfile, int iid)
        {
            var note = await _context.Note.FindAsync(iid);
            if (note == null)
            {
                return NotFound();
            }

            System.Diagnostics.Debug.WriteLine("title:");
           // System.Diagnostics.Debug.WriteLine(listfile[0].FileName);
            System.Diagnostics.Debug.WriteLine(note.Title);
            long size = listfile.Sum(f => f.Length);

            var filePaths = new List<string>();
            foreach (var formFile in listfile)
            {
                if (formFile.Length > 0)
                {
                    // full path to file in temp location
                    Random aleatoir = new Random();
                    string name = aleatoir.Next(10000) +"_" + formFile.FileName;
                    var filePath = ".\\wwwroot\\Files\\" + name ; // \\Files\\ Path.GetTempFileName(); //we are using Temp file name just for the example. Add your own file path.
                    filePaths.Add(filePath);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                    Zfile zfile = new Zfile();
                    zfile.note = note.Title;
                    zfile.NoteId = note.Id;
                    zfile.Name = name;
                    zfile.path = "~/Files/" + name;
                    zfile.User = note.User;
                    
                    _context.Add(zfile);
                    await _context.SaveChangesAsync();
                }
            }

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.
            //if (edit > 0)
            return RedirectToAction(nameof(Edit), new { id = note.Id });
            //return Ok(new { count = listfile.Count, size, filePaths });
        }
        // GET: Notes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await _context.Note.FindAsync(id);
            var user = await _context.Users.FirstOrDefaultAsync(m => m.Email.Contains(User.Identity.Name));
            if (note == null || (note.User != user.Email && user.RoleId != 3))
            {
                return NotFound();
            }

            var tuple = new Tuple<Note, IEnumerable<Zfile>>(note, await _context.Zfile.ToListAsync());

            return View(tuple);
        }

        // POST: Notes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Date,Type,Price,User,State,commente")] Note note)
        {
            if (id != note.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(note);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoteExists(note.Id))
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
            var tuple = new Tuple<Note, IEnumerable<Zfile>>(note, await _context.Zfile.ToListAsync());

            return View(tuple);
        }

        // GET: Notes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await _context.Note
                .FirstOrDefaultAsync(m => m.Id == id);
            var user = await _context.Users.FirstOrDefaultAsync(m => m.Email.Contains(User.Identity.Name));
            if (note == null || (note.User != user.Email && user.RoleId != 3))
            {
                return NotFound();
            }

            return View(note);
        }

        // POST: Notes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var note = await _context.Note.FindAsync(id);
            foreach (var entity in _context.Zfile)
            {
                if (entity.NoteId == note.Id)
                {
                    string fullPath = ".\\wwwroot\\Files\\" + entity.Name;
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                    _context.Zfile.Remove(entity);
                }
            }
            _context.Note.Remove(note);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NoteExists(int id)
        {
            return _context.Note.Any(e => e.Id == id);
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
            var note = await _context.Note.FindAsync(iid);
            if (note == null)
            {
                return NotFound();
            }
            note.State = "Valider";
            _context.Note.Update(note);
            await _context.SaveChangesAsync();
            //.Diagnostics.Debug.WriteLine("title:");
            // System.Diagnostics.Debug.WriteLine(listfile[0].FileName);
            //System.Diagnostics.Debug.WriteLine(note.Title);
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
            var note = await _context.Note.FindAsync(iid);
            if (note == null)
            {
                return NotFound();
            }
            note.State = "Rejeter";
            _context.Note.Update(note);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(All));
        }
    }
}
