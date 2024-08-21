using cmsdn.Data;
using cmsdn.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace cmsdn.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Login(string Email, string Password, string Role)
        {
            var commonPassword = _context.Passwords.FirstOrDefault()?.CommonPassword;

            if (Role == "Admin")
            {
                var admin = _context.Admins.FirstOrDefault(a => a.Email == Email && a.Password == Password);
                if (admin != null)
                {
                    // Redirect to admin portal
                    return RedirectToAction("Index", "Admin");
                }
            }
            else if (Role == "Teacher")
            {
                var teacher = _context.Teachers.FirstOrDefault(t => t.Email == Email);

                if (teacher != null && Password == commonPassword)
                {
                    // Create claims for the logged-in teacher
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, teacher.Name),
                new Claim(ClaimTypes.Email, teacher.Email),
                new Claim(ClaimTypes.Role, "Teacher")
            };

                    // Create the identity and principal
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    // Sign the user in with cookie authentication
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    // Redirect to the teacher section portal
                    return RedirectToAction("Index", "TeacherSection");
                }
            }
            else if (Role == "Student")
            {
                var student = _context.Students.FirstOrDefault(s => s.Email == Email);

                if (student != null && Password == commonPassword)
                {
                    // Create claims for the logged-in student
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, student.Name),
                new Claim(ClaimTypes.Email, student.Email),
                new Claim(ClaimTypes.Role, "Student")
            };

                    // Create the identity and principal
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    // Sign the user in with cookie authentication
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    // Stay on the current page after login
                    return RedirectToAction("Index", "StudentSection"); // Or stay on the current page
                }
            }

            // Return the login view for all outcomes
            return View();
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
       

    }
}