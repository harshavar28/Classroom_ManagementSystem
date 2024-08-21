using cmsdn.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using cmsdn.Models;
using Microsoft.EntityFrameworkCore;


[Authorize] // Ensure only authorized users can access this controller
public class TeacherSectionController : Controller
{
    private readonly ApplicationDbContext _context;

    public TeacherSectionController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        // Retrieve the logged-in teacher's email from claims
        var teacherEmail = User.FindFirstValue(ClaimTypes.Email);

        if (!string.IsNullOrEmpty(teacherEmail))
        {
            // Fetch the teacher details from the database using the email
            var teacher = _context.Teachers.FirstOrDefault(t => t.Email == teacherEmail);
            if (teacher != null)
            {
                // Fetch the sessions allocated to this teacher
                var sessions = _context.Sessions
                                       .Where(s => s.TeacherName == teacher.Name)
                                       .ToList();

                // Pass teacher details and sessions to the view using a ViewModel
                var viewModel = new TeacherSectionViewModel
                {
                    Teacher = teacher,
                    Sessions = sessions
                };

                return View(viewModel); // Pass ViewModel to the view
            }
        }

        // Handle the case where the teacher details could not be found
        return RedirectToAction("Login", "Account");
    }
    // Action to display the Add Attendance page
    [HttpGet]
    public IActionResult AddAttendance(int sessionId)
    {
        var session = _context.Sessions.FirstOrDefault(s => s.Id == sessionId);
        if (session == null)
        {
            return NotFound();
        }

        var students = _context.Students.Where(s => s.Class == session.Class).ToList();

        var attendanceRecords = _context.Attendance
            .Where(a => a.SessionId == sessionId)
            .ToDictionary(a => a.StudentId, a => a.IsPresent);

        var model = new AddAttendanceViewModel
        {
            Session = session,
            Students = students,
            AttendanceRecords = _context.Attendance.Where(a => a.SessionId == sessionId).ToList(),
            StudentsAttendance = students.Select(s => new StudentAttendance
            {
                StudentId = s.Id,
                StudentName = s.Name,
                IsPresent = attendanceRecords.ContainsKey(s.Id) && attendanceRecords[s.Id]
            }).ToList()
        };

        return View(model);
    }


    [HttpPost]
    public IActionResult AddAttendance(int sessionId, List<StudentAttendance> studentsAttendance)
    {
        var existingAttendanceRecords = _context.Attendance.Where(a => a.SessionId == sessionId).ToList();

        foreach (var studentAttendance in studentsAttendance)
        {
            var existingRecord = existingAttendanceRecords
                .FirstOrDefault(a => a.StudentId == studentAttendance.StudentId);

            if (existingRecord != null)
            {
                existingRecord.IsPresent = studentAttendance.IsPresent;
            }
            else
            {
                _context.Attendance.Add(new Attendance
                {
                    SessionId = sessionId,
                    StudentId = studentAttendance.StudentId,
                    IsPresent = studentAttendance.IsPresent,
                    Date = DateTime.Now
                });
            }
        }

        _context.SaveChanges();

        // Redirect back to the same page to show updated attendance
        return RedirectToAction("AddAttendance", new { sessionId });
    }



    // Action to save the attendance
    [HttpPost]
    public IActionResult SaveAttendance(AddAttendanceViewModel model)
    {
        if (ModelState.IsValid)
        {
            foreach (var studentAttendance in model.StudentsAttendance)
            {
                var attendance = _context.Attendance.FirstOrDefault(a =>
                    a.StudentId == studentAttendance.StudentId && a.SessionId == model.Session.Id);

                if (attendance == null)
                {
                    attendance = new Attendance
                    {
                        StudentId = studentAttendance.StudentId,
                        SessionId = model.Session.Id,
                        Date = model.Session.Date,
                        IsPresent = studentAttendance.IsPresent
                    };
                    _context.Attendance.Add(attendance);
                }
                else
                {
                    attendance.IsPresent = studentAttendance.IsPresent;
                }
            }
            _context.SaveChanges();

            // Redirect to the same AddAttendance page to display the updated attendance
            return RedirectToAction("AddAttendance", new { sessionId = model.Session.Id });
        }

        return View("AddAttendance", model);
    }
    public IActionResult Reports()
    {
        // Retrieve the logged-in teacher's email from claims
        var teacherEmail = User.FindFirstValue(ClaimTypes.Email);

        if (!string.IsNullOrEmpty(teacherEmail))
        {
            // Fetch the teacher details from the database using the email
            var teacher = _context.Teachers.FirstOrDefault(t => t.Email == teacherEmail);
            if (teacher != null)
            {
                // Fetch sessions and attendance records for this teacher
                var reportsData = from session in _context.Sessions
                                  where session.TeacherName == teacher.Name
                                  join attendance in _context.Attendance on session.Id equals attendance.SessionId
                                  join student in _context.Students on attendance.StudentId equals student.Id
                                  select new ReportViewModel
                                  {
                                      SessionId = session.Id,
                                      SessionDate = session.Date,
                                      SessionTime = session.Time.ToString(@"hh\:mm"), // Convert TimeSpan to string
                                      Subject = session.Subject,
                                      Class = session.Class,
                                      StudentName = student.Name,
                                      IsPresent = attendance.IsPresent,
                                      AttendanceDate = attendance.Date
                                  };

                return View(reportsData.ToList()); // Pass the data to the view
            }
        }

        // Handle the case where the teacher details could not be found
        return RedirectToAction("Login", "Account");
    }


}
