using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorEducationSystem.Data
{
    /// <summary>
    /// Данные по учителю, связанные с его пользовательским аккаунтом и учебными группами
    /// </summary>
    public class TeacherEducationInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Middlename { get; set; }
        public IdentityUser IdentityUser { get; set; }
        public List<SubjectGroup> SubjectGroups { get; set; }
    }
}
