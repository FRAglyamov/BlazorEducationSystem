using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlazorEducationSystem.Data
{
    /// <summary>
    /// Service для управления заданиями
    /// </summary>
    public class EducationTaskService
    {
        ApplicationDbContext _db;
        UserManager<IdentityUser> _userManager;
        public EducationTaskService(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }


        #region EducationTask
        /// <summary>
        /// Получение списка всех заданий
        /// </summary>
        /// <returns></returns>
        public async Task<List<EducationTask>> GetTasksAsync()
        {
            return await _db.EducationTasks.ToListAsync();
        }
        /// <summary>
        /// Получение задания по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<EducationTask> GetTaskByIdAsync(int id)
        {
            return await _db.EducationTasks.FindAsync(id);
        }
        /// <summary>
        /// Создание нового задания
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public async Task<string> CreateTaskAsync(EducationTask task)
        {
            _db.EducationTasks.Add(task);
            await _db.SaveChangesAsync();
            return "Create successfully";
        }
        /// <summary>
        /// Обновление задания
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public async Task<string> UpdateTaskAsync(EducationTask task)
        {
            _db.EducationTasks.Update(task);
            await _db.SaveChangesAsync();
            return "Update successfully";
        }
        /// <summary>
        /// Удаление задания
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public async Task<string> DeleteTaskAsync(EducationTask task)
        {
            _db.EducationTasks.Remove(task);
            await _db.SaveChangesAsync();
            return "Delete successfully";
        }
        #endregion


        #region SubjectGroup
        /// <summary>
        /// Получение списка всех учебных групп
        /// </summary>
        /// <returns></returns>
        public async Task<List<SubjectGroup>> GetSubjectGroupsAsync()
        {
            return await _db.SubjectGroups.ToListAsync();
        }
        /// <summary>
        /// Получение группы по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<SubjectGroup> GetSubjectGroupByIdAsync(int id)
        {
            return await _db.SubjectGroups.FindAsync(id);
        }
        /// <summary>
        /// Получение списка групп, связанных с учителем
        /// </summary>
        /// <param name="claimsPrincipal"></param>
        /// <returns></returns>
        public async Task<List<SubjectGroup>> GetCurrentTeacherSubjectGroupsAsync(ClaimsPrincipal claimsPrincipal)
        {
            var user = await _userManager.GetUserAsync(claimsPrincipal);
            var teacher = await _db.TeacherInfos.FirstOrDefaultAsync(x => x.IdentityUser == user);
            var subjectGroups = await _db.SubjectGroups.Where(x => x.Teacher == teacher).ToListAsync();
            return subjectGroups;
        }
        /// <summary>
        /// Получение списка групп, связанных со студентом
        /// </summary>
        /// <param name="claimsPrincipal"></param>
        /// <returns></returns>
        public async Task<List<SubjectGroup>> GetCurrentStudentSubjectGroupsAsync(ClaimsPrincipal claimsPrincipal)
        {
            var user = await _userManager.GetUserAsync(claimsPrincipal);
            var student = await _db.StudentInfos.FirstOrDefaultAsync(x => x.IdentityUser == user);
            var subjectGroups = await _db.SubjectGroups.Where(x => x.Students.Contains(student)).ToListAsync();
            return subjectGroups;
        }
        #endregion


        #region Lessons
        /// <summary>
        /// Получение списка всех занятий
        /// </summary>
        /// <returns></returns>
        public async Task<List<Lesson>> GetLessonsAsync()
        {
            return await _db.Lessons.ToListAsync();
        }
        /// <summary>
        /// Получение занятий по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Lesson> GetLessonByIdAsync(int id)
        {
            return await _db.Lessons.FindAsync(id);
        }
        /// <summary>
        /// Получение списка занятий по учебной группе
        /// </summary>
        /// <param name="subjectGroup"></param>
        /// <returns></returns>
        public async Task<List<Lesson>> GetLessonsBySubjectGroupIdAsync(SubjectGroup subjectGroup)
        {
            return await _db.Lessons.Where(x => x.SubjectGroup == subjectGroup).ToListAsync();
        }
        /// <summary>
        /// Получение списка всех занятий, отсортированных по времени начала занятия
        /// </summary>
        /// <returns></returns>
        public async Task<List<Lesson>> GetLessonsSortedByStartTimeAsync()
        {
            return await _db.Lessons.OrderBy(x=>x.StartTime).ToListAsync();
        }
        /// <summary>
        /// Создание нового занятия
        /// </summary>
        /// <param name="lesson"></param>
        /// <returns></returns>
        public async Task<string> CreateLessonAsync(Lesson lesson)
        {
            _db.Lessons.Add(lesson);
            await _db.SaveChangesAsync();
            return "Create successfully";
        }
        /// <summary>
        /// Обновление занятия
        /// </summary>
        /// <param name="lesson"></param>
        /// <returns></returns>
        public async Task<string> UpdateLessonAsync(Lesson lesson)
        {
            _db.Lessons.Update(lesson);
            await _db.SaveChangesAsync();
            return "Update successfully";
        }
        /// <summary>
        /// Удаление занятия
        /// </summary>
        /// <param name="lesson"></param>
        /// <returns></returns>
        public async Task<string> DeleteLessonAsync(Lesson lesson)
        {
            _db.Lessons.Remove(lesson);
            await _db.SaveChangesAsync();
            return "Delete successfully";
        }

        #endregion


        #region Work
        /// <summary>
        /// Получение работы студента по заданию
        /// </summary>
        /// <param name="task"></param>
        /// <param name="claimsPrincipal"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Получение списка всех работ по заданию
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public async Task<List<Work>> GetWorksAsync(EducationTask task)
        {
            List<Work> works = new List<Work>();
            if (_db.Works.Any(x => x.Task == task))
            {
                works = await _db.Works.Where(x => x.Task == task).ToListAsync();
            }
            return works;
        }
        /// <summary>
        /// Обновление работы
        /// </summary>
        /// <param name="work"></param>
        /// <returns></returns>
        public async Task<string> UpdateWorkAsync(Work work)
        {
            _db.Update(work);
            await _db.SaveChangesAsync();
            return "Update successfully";
        }
        /// <summary>
        /// Получение данных студента по работе
        /// </summary>
        /// <param name="work"></param>
        /// <returns></returns>
        public async Task<StudentEducationInfo> GetStudentByWorkAsync(Work work)
        {
            var student = await _db.StudentInfos.FirstOrDefaultAsync(x=>x.Id==work.StudentId);
            return student;
        }
        /// <summary>
        /// Добавление/обновление работы при ответе на задание
        /// </summary>
        /// <param name="task"></param>
        /// <param name="work"></param>
        /// <param name="claimsPrincipal"></param>
        /// <returns></returns>
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
        #endregion

    }
}
