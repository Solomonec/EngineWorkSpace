using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MetroSupport.BLL.Interfaces;
using MetroSupport.Models;

namespace MetroSupport.BLL.Implements
{
    public class UserRepository:IUserRepository
    {
        private readonly UsersContext _context;

        public UserRepository(UsersContext context)
        {
            _context = context;
        }

        public IQueryable<UserProfile> GetUsers()
        {
            return _context.UserProfiles;
        }

        public IQueryable<UserProfile> GetUsersByDepartment(string department)
        {
            return _context.UserProfiles.Where(x => x.Department == department).OrderBy(x=>x.FullName);
        }

        public IQueryable<UserProfile> GetUsersByDepartmentAndLiteral(string department, string literal)
        {
            return _context.UserProfiles.Where(x => x.Department == department && x.FullName.StartsWith(literal)).OrderBy(x => x.FullName);
        }

        public UserProfile GetUserByName(string username)
        {
            return _context.UserProfiles.FirstOrDefault(x => x.UserName == username);
        }

        public UserProfile GetUserByFullName(string username)
        {
            return _context.UserProfiles.FirstOrDefault(x => x.FullName == username);
        }

        public UserProfile GetUserById(string id)
        {

            return _context.UserProfiles.FirstOrDefault(x => x.UserId.ToString() == id);
        }

        public UserProfile SaveUserProfile(UserProfile profile)
        {
            if (profile.UserId.ToString() != String.Empty)
            {
                UserProfile userProfile = _context.UserProfiles.FirstOrDefault(x => x.UserId == profile.UserId);
                if (userProfile != null)
                {
                    userProfile.FullName = profile.FullName;
                    userProfile.Job = profile.Job;
                    userProfile.Slugba = profile.Slugba;
                    userProfile.Email = profile.Email;
                    userProfile.Department = profile.Department;
                    _context.Entry(userProfile).State = EntityState.Modified;
                    _context.SaveChanges();
                    return userProfile;
                }
                return null;
            }
            return null;
        }
    }
}