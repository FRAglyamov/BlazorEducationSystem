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
            var user = await _userManager.GetUserAsync(claimsPrincipal);
            var teacher = await _db.TeacherInfos.FirstOrDefaultAsync(x => x.IdentityUser == user);
            var subjectGroups = await _db.SubjectGroups.Where(x => x.Teacher == teacher).ToListAsync();
            return subjectGroups;
        }

        public async Task<List<SubjectGroup>> GetCurrentStudentSubjectGroups(ClaimsPrincipal claimsPrincipal)
        {
            var user = await _userManager.GetUserAsync(claimsPrincipal);
            var student = await _db.StudentInfos.FirstOrDefaultAsync(x => x.IdentityUser == user);
            var subjectGroups = await _db.SubjectGroups.Where(x => x.Students.Contains(student)).ToListAsync();
            return subjectGroups;
        }

        public async Task<List<Lesson>> GetLessonsBySubjectGroupId(SubjectGroup subjectGroup)
        {
            var lessons = await _db.Lessons.Where(x => x.SubjectGroup == subjectGroup).ToListAsync();
            return lessons;
        }

        public async Task<string> UpdateTaskAsync(EducationTask task)
        {
            _db.EducationTasks.Update(task);
            await _db.SaveChangesAsync();
            return "Update successfully";
        }

        public async Task<string> DeleteTaskAsync(EducationTask task)
        {
            _db.EducationTasks.Remove(task);
            await _db.SaveChangesAsync();
            return "Delete successfully";
        }
        public async Task<Work> GetWorkAsync(EducationTask task, ClaimsPrincipal claimsPrincipal)
        {
            var user = await _userManager.GetUserAsync(claimsPrincipal);
            var student = await _db.StudentInfos.FirstOrDefaultAsync(x => x.IdentityUser == user);
            if (_db.Works.Any(x => x.Task == task && x.Student == student))
            {
                return await _db.Works.FirstOrDefaultAsync(x => x.Task == task && x.Student == student);
            }
            else
            {
                Work newWork = new Work();
                newWork.Student = student;
                newWork.Task = task;
                return newWork;
            }
        }
        public async Task<List<Work>> GetWorksAsync(EducationTask task)
        {
            List<Work> works = new List<Work>();
            if (_db.Works.Any(x => x.Task == task))
            {
                works = await _db.Works.Where(x => x.Task == task).ToListAsync();
            }
            return works;
        }
        public async Task<string> UpdateWorkAsync(Work work)
        {
            _db.Update(work);
            await _db.SaveChangesAsync();
            return "Update successfully";
        }
        public async Task<StudentEducationInfo> GetStudentByWorkAsync(Work work)
        {
            var student = await _db.StudentInfos.FirstOrDefaultAsync(x=>x.Id==work.StudentId);
            return student;
        }
        public async Task<string> AnswerTaskAsync(EducationTask task, Work work, ClaimsPrincipal claimsPrincipal)
        {
            var user = await _userManager.GetUserAsync(claimsPrincipal);
            var student = await _db.StudentInfos.FirstOrDefaultAsync(x => x.IdentityUser == user);
            if(_db.Works.Any(x => x.Task == task && x.Student == student))
            {
                _db.Works.Update(work);
                await _db.SaveChangesAsync();
                return "Update successfully";
            }
            else
            {
                _db.Works.Add(work);
                await _db.SaveChangesAsync();
                return "Create successfully";
            }
        }

    }
}
