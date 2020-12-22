using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorEducationSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<StudentEducationInfo> StudentInfos { get; set; }
        public DbSet<TeacherEducationInfo> TeacherInfos { get; set; }
        public DbSet<SubjectGroup> SubjectGroups { get; set; }
        public DbSet<EducationTask> EducationTasks { get; set; }
        public DbSet<Work> Works { get; set; }
        public DbSet<Lesson> Lessons { get; set; }

    }
}
