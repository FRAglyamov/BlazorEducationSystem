using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlazorEducationSystem.Data
{
    public class EducationTaskService
    {
        ApplicationDbContext _db;
        UserManager<IdentityUser> _userManager;
        public EducationTaskService(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public async Task<List<EducationTask>> GetTasksAsync()
        {
            return await _db.EducationTasks.ToListAsync();
        }
        public async Task<EducationTask> GetTaskByIdAsync(int id)
        {
            var task = await _db.EducationTasks.FindAsync(id);
            return task;
        }
        public async Task<SubjectGroup> GetSubjectGroupById(int id)
        {
            var subjectGroup = await _db.SubjectGroups.FindAsync(id);
            return subjectGroup;
        }
        public async Task<string> CreateTaskAsync(EducationTask task)
        {
            _db.EducationTasks.Add(task);
            await _db.SaveChangesAsync();
            return "Create successfully";
        }
        public async Task<List<SubjectGroup>> GetCurrentTeacherSubjectGroups(ClaimsPrincipal claimsPrincipal)
        {
            //_userManager.GetUserAsync();
            var user = await _userManager.GetUserAsync(claimsPrincipal);
            var teacher = await _db.TeacherInfos.FirstOrDefaultAsync(x => x.IdentityUser == user);
            var subjectGroups = await _db.SubjectGroups.Where(x => x.Teacher == teacher).ToListAsync();
            return subjectGroups;

        }

        public async Task<string> UpdateTaskAsync(EducationTask task)
        {
            _db.EducationTasks.Update(task);
            await _db.SaveChangesAsync();
            return "Update successfully";
        }

        public async Task<string> DeleteTaskAsync(EducationTask task)
        {
            //var task = await _db.EducationTasks.FindAsync(id);
            //if (task == null)
            //    return null;
            _db.EducationTasks.Remove(task);
            await _db.SaveChangesAsync();
            return "Delete successfully";
        }
    }
}
