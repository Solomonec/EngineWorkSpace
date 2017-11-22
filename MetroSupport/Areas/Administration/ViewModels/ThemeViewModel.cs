using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcPaging;
using MetroSupport.Models;

namespace MetroSupport.Areas.Administration.ViewModels
{
    public class ThemeViewModel
    {
        public IPagedList<TroubleSubject> TroubleSubjects { get; set; }
        public TroubleSubject TroubleSubject { get; set; }

        public ThemeViewModel()
        {
            TroubleSubject = new TroubleSubject();
        }
    }
}