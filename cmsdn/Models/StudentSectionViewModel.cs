using System;
using System.Collections.Generic;

namespace cmsdn.Models
{
    public class StudentSectionViewModel
    {
        public Student Student { get; set; }
        public IEnumerable<Session> Sessions { get; set; } // Adjust as needed
    }
}
