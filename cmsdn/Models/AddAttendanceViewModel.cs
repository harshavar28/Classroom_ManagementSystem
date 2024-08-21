using System.Collections.Generic;

namespace cmsdn.Models
{
    public class AddAttendanceViewModel
    {
        public Session Session { get; set; }
        public List<Student> Students { get; set; }
        public List<Attendance> AttendanceRecords { get; set; }
        public List<StudentAttendance> StudentsAttendance { get; set; } = new List<StudentAttendance>();
    }

    public class StudentAttendance
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public bool IsPresent { get; set; }
    }
}
