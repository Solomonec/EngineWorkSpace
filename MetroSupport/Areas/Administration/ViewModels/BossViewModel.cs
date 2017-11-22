using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcPaging;
using MetroSupport.Models;

namespace MetroSupport.Areas.Administration.ViewModels
{
    public class BossViewModel
    {
        public IPagedList<AssignBoss> Bosses { get; set; }
        public AssignBoss Boss { get; set; }

        public BossViewModel()
        {
            Boss = new AssignBoss();
            
        }

    }
}