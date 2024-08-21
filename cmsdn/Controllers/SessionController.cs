using Microsoft.AspNetCore.Mvc;
using cmsdn.Data;
using cmsdn.Models;
using System.Linq;

namespace cmsdn.Controllers
{
    public class SessionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SessionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Display the form to add a new session
        public IActionResult AddSession()
        {
            // Pass the list of teachers to the view
            ViewBag.Teachers = _context.Teachers.ToList();
            return View();
        }

        // Process the form submission to add a new session
        [HttpPost]
        public IActionResult AddSession(Session session)
        {
            if (ModelState.IsValid)
            {
                _context.Sessions.Add(session);
                _context.SaveChanges();
                return RedirectToAction("Index"); // Redirect to the list of sessions after adding
            }

            // Repopulate ViewBag.Teachers if there's an error
            ViewBag.Teachers = _context.Teachers.ToList();
            return View(session);
        }

        // Display the list of sessions
        public IActionResult Index()
        {
            var sessions = _context.Sessions.ToList();
            return View(sessions);
        }

        [HttpPost]
        public IActionResult DeleteSession(int id)
        {
            var session = _context.Sessions.FirstOrDefault(s => s.Id == id);
            if (session != null)
            {
                _context.Sessions.Remove(session);
                _context.SaveChanges();
                return Ok();
            }
            return NotFound();
        }
    }
}
