using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorEducationSystem.Data
{
    public class StudentEducationInfo
    {
        public int Id { get; set; }
        public IdentityUser IdentityUser { get; set; }
        public List<SubjectGroup> SubjectGroups { get; set; }
    }
}
