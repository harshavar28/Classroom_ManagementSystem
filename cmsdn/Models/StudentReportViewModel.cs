using System;

namespace cmsdn.Models
{
    public class StudentReportViewModel
    {
        public int SessionId { get; set; }
        public DateTime SessionDate { get; set; }
        public string SessionTime { get; set; }
        public string Subject { get; set; }
        public string Class { get; set; }
        public bool IsPresent { get; set; }
        public DateTime AttendanceDate { get; set; }
    }
}
