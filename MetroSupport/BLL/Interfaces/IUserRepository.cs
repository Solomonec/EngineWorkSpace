using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetroSupport.Models;

namespace MetroSupport.BLL.Interfaces
{
    public interface IUserRepository
    {
        IQueryable<UserProfile> GetUsers();
        IQueryable<UserProfile> GetUsersByDepartment(string department);
        IQueryable<UserProfile> GetUsersByDepartmentAndLiteral(string department, string literal);
        UserProfile GetUserById(string id);
        UserProfile GetUserByName(string username);
        UserProfile GetUserByFullName(string username);
        UserProfile SaveUserProfile(UserProfile profile);
    }
}