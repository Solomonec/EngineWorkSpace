using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcPaging;
using MetroSupport.Models;

namespace MetroSupport.Areas.Administration.ViewModels
{
    public class RequestOwnerViewModel
    {
        public IPagedList<RequestOwner> RequestOwners { get; set; }
        public RequestOwner RequestOwner { get; set; }

        public RequestOwnerViewModel()
        {
            RequestOwner = new RequestOwner();
        }
    }
}