using Microsoft.AspNetCore.Mvc;
using cmsdn.Data;
using cmsdn.Models;
using System.Linq;

namespace cmsdn.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Display the form to add a new student
        public IActionResult AddStudent()
        {
            return View();
        }

        // Process the form submission
        [HttpPost]
        public IActionResult AddStudent(Student student)
        {
            // Check for duplicate name, email, or class for student
            if (_context.Students.Any(s => s.Name == student.Name))
            {
                ModelState.AddModelError("Name", "A student with this name already exists.");
            }

            if (_context.Students.Any(s => s.Email == student.Email))
            {
                ModelState.AddModelError("Email", "A student with this email already exists.");
            }

            if (ModelState.IsValid)
            {
                _context.Students.Add(student);
                _context.SaveChanges();
                return RedirectToAction("Index"); // Redirect to the list of students after adding
            }

            return View(student);
        }


        // Display the list of students
        public IActionResult Index()
        {
            var students = _context.Students.ToList();
            return View(students);
        }


        [HttpPost]
        public IActionResult DeleteStudent(int id)
        {
            var student = _context.Students.FirstOrDefault(s => s.Id == id);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
                return Ok();
            }
            return NotFound();
        }
    }
}
