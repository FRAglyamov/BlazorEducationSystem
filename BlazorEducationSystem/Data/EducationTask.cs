using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorEducationSystem.Data
{
    /// <summary>
    /// Задания со сроками, создающиеся учителем для группы
    /// </summary>
    public class EducationTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public int SubjectGroupId { get; set; }
        public SubjectGroup SubjectGroup { get; set; }
    }
}
