using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using cmsdn.Models;
using Microsoft.EntityFrameworkCore;
using cmsdn.Data;

[Authorize]
public class StudentSectionController : Controller
{
    private readonly ApplicationDbContext _context;

    public StudentSectionController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var studentEmail = User.FindFirstValue(ClaimTypes.Email);

        if (!string.IsNullOrEmpty(studentEmail))
        {
            var student = _context.Students.FirstOrDefault(s => s.Email == studentEmail);
            if (student != null)
            {
                // Fetch sessions directly
                var sessions = _context.Sessions
                                       .Where(s => s.Class == student.Class)
                                       .ToList();

                var viewModel = new StudentSectionViewModel
                {
                    Student = student,
                    Sessions = sessions
                };

                return View(viewModel);
            }
        }

        // If student details not found, redirect to login page
        return RedirectToAction("Login", "Account");
    }

    public IActionResult Reports()
    {
        var studentEmail = User.FindFirstValue(ClaimTypes.Email);

        if (!string.IsNullOrEmpty(studentEmail))
        {
            var student = _context.Students.FirstOrDefault(s => s.Email == studentEmail);
            if (student != null)
            {
                var reportsData = from attendance in _context.Attendance
                                  join session in _context.Sessions on attendance.SessionId equals session.Id
                                  where attendance.StudentId == student.Id
                                  select new StudentReportViewModel
                                  {
                                      SessionId = session.Id,
                                      SessionDate = session.Date,
                                      SessionTime = session.Time.ToString(@"hh\:mm"), // Convert TimeSpan to string
                                      Subject = session.Subject,
                                      Class = session.Class,
                                      IsPresent = attendance.IsPresent,
                                      AttendanceDate = attendance.Date
                                  };

                return View(reportsData.ToList()); // Pass the data to the view
            }
        }

        // Handle the case where the student details could not be found
        return RedirectToAction("Login", "Account");
    }
}



