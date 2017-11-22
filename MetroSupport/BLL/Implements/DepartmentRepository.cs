using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MetroSupport.BLL.Interfaces;
using MetroSupport.Models;

namespace MetroSupport.BLL.Implements
{
    public class DepartmentRepository: IDepartmentRepository
    {
        private MetroSupportContext _metro;

        public DepartmentRepository(MetroSupportContext metro)
        {
            _metro = metro;
        }

        public IQueryable<Department> GetDepartments()
        {
            return _metro.Departments;
        }

        public IQueryable<Department> GetBaseDepartments()
        {
           return _metro.Departments.Where(x=>x.DepartmentType=="Base");
        }

        public IQueryable<Department> GetSubDepartments()
        {
           return _metro.Departments.Where(x => x.DepartmentType == "Sub");
        }

        public IQueryable<Department> GetDepartmentsByName(string name)
        {
            return _metro.Departments.Where(x => x.DepartmentName == name && x.DepartmentType == "Sub").OrderBy(x=>x.DepartmentName);
        }

        public Department GetDepartmentById(string id)
        {
            if (id != String.Empty)
            {
                Guid guid = Guid.Parse(id);
                return _metro.Departments.FirstOrDefault(x => x.DepartmentId == guid);
            }
            else
                return null;

        }

        public bool CreateNewDepartment(Department department)
        {
            if (department != null)
            {
                _metro.Departments.Add(department);
                _metro.SaveChanges();
                return true;
            }
            else return false;
        }

        public bool EditDepartment(Department department)
        {
            Department currentdepartment = _metro.Departments.FirstOrDefault(x => x.DepartmentId == department.DepartmentId);
            if (currentdepartment != null)
            {
                currentdepartment.DepartmentName = department.DepartmentName;
                currentdepartment.DepartmentType = department.DepartmentType;
                currentdepartment.SubDepartmentName = department.SubDepartmentName;
                _metro.Entry(currentdepartment).State = EntityState.Modified;
                _metro.SaveChanges();
                return true;
            }
            else return false;
        }

        public bool DeleteDepartment(string id)
        {
            if (id != String.Empty)
            {
                Guid guid = Guid.Parse(id);
                Department department = _metro.Departments.FirstOrDefault(x => x.DepartmentId == guid);
                if (department != null)
                {
                    _metro.Entry(department).State = EntityState.Deleted;
                    _metro.SaveChanges();
                    return true;
                }
                else return false;
            }
            else return false;
        }
    }
}