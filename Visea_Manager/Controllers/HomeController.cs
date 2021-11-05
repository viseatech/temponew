using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Visea_Expense_Manager.Models;
using System.Security.Principal;
using System.Security;
using Visea_Expense_Manager.Data;

namespace Visea_Expense_Manager.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MvcNoteContext _context;
        public HomeController(ILogger<HomeController> logger, MvcNoteContext context)
        {
            _logger = logger;
            _context = context;
            /*
            foreach (var entity in context.Event)
                context.Event.Remove(entity);
            context.SaveChanges();
            */
        }

        public  IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var note = _context.Users.FirstOrDefault(m => m.Email.Contains(User.Identity.Name));

                if (note == null)
                {
                    User user = new User();
                    user.Name = User.Identity.Name;
                    user.Role = "Consultant";
                    user.RoleId = 1;
                    user.Email = User.Identity.Name;
                    user.Director = "nodirector";

                    _context.Add(user);
                    _context.SaveChanges();
                }
            }
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
