namespace cmsdn.Models
{
    public class TeacherSectionViewModel
    {
        public Teacher Teacher { get; set; }
        public IEnumerable<Session> Sessions { get; set; }
    }
}
