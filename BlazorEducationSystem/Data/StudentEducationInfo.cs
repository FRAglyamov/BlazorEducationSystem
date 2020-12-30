using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorEducationSystem.Data
{
    /// <summary>
    /// Данные по студенту, связанные с его пользовательским аккаунтом и учебными группами
    /// </summary>
    public class StudentEducationInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Middlename { get; set; }
        public IdentityUser IdentityUser { get; set; }
        public List<SubjectGroup> SubjectGroups { get; set; }
    }
}
