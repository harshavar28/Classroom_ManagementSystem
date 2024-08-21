namespace cmsdn.Models
{
    public class ReportViewModel
    {
        public int SessionId { get; set; }
        public DateTime SessionDate { get; set; }
        public string SessionTime { get; set; } // Ensure this is a string
        public string Subject { get; set; }
        public string Class { get; set; }
        public string StudentName { get; set; }
        public bool IsPresent { get; set; }
        public DateTime AttendanceDate { get; set; }
    }
}
