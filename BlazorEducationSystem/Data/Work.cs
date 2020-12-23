using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorEducationSystem.Data
{
    public class Work
    {
        public int Id { get; set; }
        public EducationTask Task { get; set; }
        public int StudentId { get; set; }
        public StudentEducationInfo Student { get; set; }
        public float Mark { get; set; }
        public string Answer { get; set; }
    }
}
