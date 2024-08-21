using Microsoft.AspNetCore.Mvc;
using cmsdn.Data;
using cmsdn.Models;
using System.Linq;

namespace cmsdn.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeacherController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Display the form to add a new teacher
        public IActionResult AddTeacher()
        {
            return View();
        }

        // Process the form submission
        [HttpPost]
        public IActionResult AddTeacher(Teacher teacher)
        {
            // Check for duplicate name, email, or class teacher of
            if (_context.Teachers.Any(t => t.Name == teacher.Name))
            {
                ModelState.AddModelError("Name", "A teacher with this name already exists.");
            }

            if (_context.Teachers.Any(t => t.Email == teacher.Email))
            {
                ModelState.AddModelError("Email", "A teacher with this email already exists.");
            }

            if (_context.Teachers.Any(t => t.ClassTeacherOf == teacher.ClassTeacherOf))
            {
                ModelState.AddModelError("ClassTeacherOf", "A teacher for this class is already assigned.");
            }

            if (ModelState.IsValid)
            {
                _context.Teachers.Add(teacher);
                _context.SaveChanges();
                return RedirectToAction("Index"); // Redirect to the list of teachers after adding
            }

            return View(teacher);
        }

        // Display the list of teachers
        public IActionResult Index()
        {
            var teachers = _context.Teachers.ToList();
            return View(teachers);
        }

        [HttpPost]
        public IActionResult DeleteTeacher(int id)
        {
            var teacher = _context.Teachers.FirstOrDefault(t => t.Id == id);
            if (teacher != null)
            {
                _context.Teachers.Remove(teacher);
                _context.SaveChanges();
                return Ok();
            }
            return NotFound();
        }
    }
}
