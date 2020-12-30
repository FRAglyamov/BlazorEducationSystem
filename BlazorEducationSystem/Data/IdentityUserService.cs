using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorEducationSystem.Data
{
    /// <summary>
    /// Service для управления основными данными пользователя и его ролями
    /// </summary>
    public class IdentityUserService
    {
        ApplicationDbContext _db;
        UserManager<IdentityUser> _userManager;
        public IdentityUserService(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        /// <summary>
        /// Получение списка всех пользователей из БД
        /// </summary>
        /// <returns></returns>
        public async Task<List<IdentityUser>> GetUsersAsync()
        {
            return await _db.Users.ToListAsync();
        }
        /// <summary>
        /// Получение роли пользователя по его IdentityUser
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<string> GetUserRoleAsync(IdentityUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return roles.FirstOrDefault();
        }
        /// <summary>
        /// Получение словаря userId: role по списку пользователей
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, string>> GetUsersRolesAsync(List<IdentityUser> users)
        {
            Dictionary<string, string> roles = new Dictionary<string, string>();
            foreach (var user in users)
            {
                roles.Add(user.Id, await GetUserRoleAsync(user));
            }
            return roles;
        }
        /// <summary>
        /// Получение роли пользователя по его id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<string> GetUserRoleAsync(string userId)
        {
            var user = await _db.Users.FindAsync(userId);
            var userRole = await _db.UserRoles.FirstOrDefaultAsync(x => x.UserId == user.Id);
            var role = await _db.Roles.FirstOrDefaultAsync(x => x.Id == userRole.RoleId);
            return role.Name;
        }
        /// <summary>
        /// Получения пользователя по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IdentityUser> GetUserByIdAsync(string id)
        {
            return await _db.Users.FindAsync(id);
        }

        /// <summary>
        /// Создание нового пользователя, а также профилей учителя и студента для него
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<string> CreateUserAsync(IdentityUser user, string password, string role)
        {
            await _userManager.CreateAsync(user, password);
            _db.TeacherInfos.Add(new TeacherEducationInfo { IdentityUser = user });
            _db.StudentInfos.Add(new StudentEducationInfo { IdentityUser = user });
            await _db.SaveChangesAsync();
            return "Create successfully";
        }
        /// <summary>
        /// Добавление роли пользователю
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<string> CreateUserRoleAsync(IdentityUserRole<string> role)
        {
            _db.UserRoles.Add(role);
            await _db.SaveChangesAsync();
            return "Create role successfully";
        }
        /// <summary>
        /// Обновление данных пользователя и его роли
        /// </summary>
        /// <param name="u"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<string> UpdateUserAsync(IdentityUser u, string role)
        {
            await _userManager.UpdateAsync(u);
            var userRoleToRemove = _db.UserRoles.FirstOrDefault(x => x.UserId == u.Id);
            _db.UserRoles.Remove(userRoleToRemove);
            await _userManager.AddToRoleAsync(u, role);
            return "Update successfully";
        }

        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public async Task<string> DeleteUserAsync(IdentityUser u)
        {
            await _userManager.DeleteAsync(u);
            return "Delete successfully";
        }
    }
}
