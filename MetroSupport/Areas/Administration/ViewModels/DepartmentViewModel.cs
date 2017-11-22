using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcPaging;
using MetroSupport.Models;

namespace MetroSupport.Areas.Administration.ViewModels
{
    public class DepartmentViewModel
    {
        public IPagedList<Department> Departments { get; set; }
        public Department Department { get; set; }

        public DepartmentViewModel()
        {
            Department = new Department();
        }
    }
}