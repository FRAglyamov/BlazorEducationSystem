using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorEducationSystem.Data
{
    /// <summary>
    /// Учебные группы
    /// </summary>
    public class SubjectGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TeacherEducationInfo Teacher { get; set; }
        public List<StudentEducationInfo> Students { get; set; }
        public List<EducationTask> Tasks { get; set; }
    }
}
