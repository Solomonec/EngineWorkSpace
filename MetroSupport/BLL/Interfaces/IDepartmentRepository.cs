using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetroSupport.Models;

namespace MetroSupport.BLL.Interfaces
{
    public interface IDepartmentRepository
    {
        IQueryable<Department> GetDepartments();
        IQueryable<Department> GetBaseDepartments();
        IQueryable<Department> GetSubDepartments();
        IQueryable<Department> GetDepartmentsByName(string name);
        Department GetDepartmentById(string id);
        bool CreateNewDepartment(Department department);
        bool EditDepartment(Department department);
        bool DeleteDepartment(string id);
    }
}