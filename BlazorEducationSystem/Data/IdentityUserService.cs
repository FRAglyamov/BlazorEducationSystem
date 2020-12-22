using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorEducationSystem.Data
{
    public class IdentityUserService
    {
        ApplicationDbContext _db;
        UserManager<IdentityUser> _userManager;
        public IdentityUserService(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<List<IdentityUser>> GetUsersAsync()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task<string> GetUserRoleAsync(IdentityUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return roles.FirstOrDefault();
        }
        public async Task<Dictionary<string, string>> GetUsersRolesAsync(List<IdentityUser> users)
        {
            Dictionary<string, string> roles = new Dictionary<string, string>();
            foreach (var user in users)
            {
                roles.Add(user.Id, await GetUserRoleAsync(user));
            }
            return roles;
        }
        public async Task<string> GetUserRoleAsync(string userId)
        {
            var user = await _db.Users.FindAsync(userId);
            var userRole = await _db.UserRoles.FirstOrDefaultAsync(x => x.UserId == user.Id);
            var role = await _db.Roles.FirstOrDefaultAsync(x => x.Id == userRole.RoleId);
            return role.Name;
        }
        //public async Task<Dictionary<string, string>> GetUsersRolesAsync(List<IdentityUser> users)
        //{
        //    Dictionary<string, string> roles = new Dictionary<string, string>();
        //    foreach (var user in users)
        //    {
        //        roles.Add(user.Id, await GetUserRoleAsync(user.Id));
        //    }
        //    return roles;
        //}

        public async Task<IdentityUser> GetUserByIdAsync(string id)
        {
            return await _db.Users.FindAsync(id);
        }


        public async Task<string> CreateUserAsync(IdentityUser user, string password, string role)
        {
            await _userManager.CreateAsync(user, password);
            _db.TeacherInfos.Add(new TeacherEducationInfo { IdentityUser = user });
            _db.StudentInfos.Add(new StudentEducationInfo { IdentityUser = user });
            await _db.SaveChangesAsync();
            return "Create successfully";
        }
        public async Task<string> CreateUserRoleAsync(IdentityUserRole<string> role)
        {
            _db.UserRoles.Add(role);
            await _db.SaveChangesAsync();
            return "Create role successfully";
        }

        public async Task<string> UpdateUserAsync(IdentityUser u, string role)
        {
            await _userManager.UpdateAsync(u);
            var userRoleToRemove = _db.UserRoles.FirstOrDefault(x => x.UserId == u.Id);
            _db.UserRoles.Remove(userRoleToRemove);
            await _userManager.AddToRoleAsync(u, role);
            return "Update successfully";
        }


        public async Task<string> DeleteUserAsync(IdentityUser u)
        {
            await _userManager.DeleteAsync(u);
            return "Delete successfully";
        }
    }
}
