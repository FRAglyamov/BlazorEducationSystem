using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorEducationSystem.Data
{
    /// <summary>
    /// Занятия групп с временем их проведения (день недели, начало и конец)
    /// </summary>
    public class Lesson
    {
        public int Id { get; set; }
        public Weekday Weekday { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int SubjectGroupId { get; set; }
        public SubjectGroup SubjectGroup { get; set; }
    }
}
