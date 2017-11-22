using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using MetroSupport.Models;
using MetroSupport.ViewModels;
using Microsoft.Ajax.Utilities;

namespace MetroSupport.Commons
{
    public static class Extensions
    {
      

        public static string ToDateTimeGridStr(this object o)
        {
            return o is DateTime ? ((DateTime)o).ToShortDateString() : o.ToString();
        }

        public static string ToStatusGridStr(this object o)
        {
            switch (Convert.ToInt32(o))
            {
                case 0: return "В процессе создания...";
                case 5: return "Назначена";
                case 4: return "Выполнение отложено";
                case -1: return "Отклонена";
                case 9: return "Ожидается подтверждение ответственного";
                case 12: return "Закрыта";
                default: return "";
            }   
        }

        public static string ToHtmlStatusGridStr(this string status)
        {
            switch (status)
            {
                case "Закрыта": return "<div style='color: green; text-width:bold;'>" + status + "</div>";
                case "Назначена (В работе)": return "<div style='color: blue; text-width:bold;'>" + status + "</div>";
                case "Назначена (Не в работе)": return "<div style='color: blue; text-width:bold;'>" + status + "</div>";
                case "Выполнение отложено": return "<div style='color: cornflowerblue; text-width:bold;'>" + status + "</div>";
                case "Отклонена": return "<div style='color: red; text-width:bold;'>" + status + "</div>";
                case "Ожидается подтверждение ответственного": return "<div style='color: orange; text-width:bold;'>" + status + "</div>";
                default: return status;
            }
        }

        public static Enums.Department ToEnumDepartment(this string department) 
        {
            switch(department)
            {
                case "It": return Enums.Department.It;
                case "Aspp": return Enums.Department.Aspp;
                case "Svyaz": return Enums.Department.Svyaz;
                case "Pa": return Enums.Department.Pa;
                default: return Enums.Department.Default;
            }
            
        }

        public static string ToInternationalDepartmentName(this string department)
        {
            switch (department)
            {
                case "ИТ": return "It";
                case "АСПП": return "Aspp";
                case "Связь": return "Svyaz";
                case "ПА": return "Pa";
                default: return "";
            }

        }

        public static IQueryable<TopCallRequest> ToTopCallRequest(this IQueryable<ItCallRequest> callrequests)
        {
            List<TopCallRequest> requests = new List<TopCallRequest>();
            foreach (var item in callrequests)
            {
                TopCallRequest resultmodel = new TopCallRequest
                {
                    RequestId = item.CallRequestId.ToString(),
                    RequestNumber = item.RequestNumber,
                    Theme = item.TroubleSubject.Length < 20 ? item.TroubleSubject : item.TroubleSubject.Substring(0, 20) + "...",
                    Department = item.TroubleDepartment.ToInternationalDepartmentName()
                };


                requests.Add(resultmodel);
            }
           
            return requests.AsQueryable();
        }

        public static IQueryable<TopCallRequest> ToTopCallRequest(this IQueryable<AsppCallRequest> callrequests)
        {
            List<TopCallRequest> requests = new List<TopCallRequest>();
            foreach (var item in callrequests)
            {
                TopCallRequest resultmodel = new TopCallRequest
                {
                    RequestId = item.CallRequestId.ToString(),
                    RequestNumber = item.RequestNumber,
                    Theme = item.TroubleSubject.Substring(0, 20) + "...",
                    Department = item.TroubleDepartment.ToInternationalDepartmentName()
                };


                requests.Add(resultmodel);
            }

            return requests.AsQueryable();
        }

        public static IQueryable<TopCallRequest> ToTopCallRequest(this IQueryable<SvyazCallRequest> callrequests)
        {
            List<TopCallRequest> requests = new List<TopCallRequest>();
            foreach (var item in callrequests)
            {
                TopCallRequest resultmodel = new TopCallRequest
                {
                    RequestId = item.CallRequestId.ToString(),
                    RequestNumber = item.RequestNumber,
                    Theme = item.TroubleSubject.Substring(0, 20) + "...",
                    Department = item.TroubleDepartment.ToInternationalDepartmentName()
                };


                requests.Add(resultmodel);
            }

            return requests.AsQueryable();
        }

        public static IQueryable<TopCallRequest> ToTopCallRequest(this IQueryable<PaCallRequest> callrequests)
        {
            List<TopCallRequest> requests = new List<TopCallRequest>();
            foreach (var item in callrequests)
            {
                TopCallRequest resultmodel = new TopCallRequest
                {
                    RequestId = item.CallRequestId.ToString(),
                    RequestNumber = item.RequestNumber,
                    Theme = item.TroubleSubject.Substring(0, 20) + "...",
                    Department = item.TroubleDepartment.ToInternationalDepartmentName()
                };


                requests.Add(resultmodel);
            }

            return requests.AsQueryable();
        }

        public static ManageModel ToManageModel(this UserProfile profile)
        {
            ManageModel resultmodel = new ManageModel
            {
                UserId = profile.UserId,
                UserName = profile.UserName,
                FullName = profile.FullName,
                Job = profile.Job,
                Slugba = profile.Slugba,
                Department = profile.Department,
                Email = profile.Email,
                Active = profile.Active
            };


            return resultmodel;
        }

        public static UserProfile ToUserProfile(this ManageModel manage)
        {
            UserProfile resultmodel = new UserProfile
            {
                UserId = manage.UserId,
                UserName = manage.UserName,
                FullName = manage.FullName,
                Job = manage.Job,
                Slugba = manage.Slugba,
                Email = manage.Email,
                Department = manage.Department,
                Active = manage.Active
            };


            return resultmodel;
        }

        public static IQueryable<ExportResultModel> ToExportResultModel(this IQueryable<ItCallRequest> request)
        {
            List<ExportResultModel> resultmodel = new List<ExportResultModel>();

            foreach (var item in request)
            {
                ExportResultModel resultitem = new ExportResultModel
                {
                    CreationDate = item.Creation.Value.ToShortDateString(),
                    CreationTime = item.Time.Value.ToShortTimeString(),
                    RequestNumber = item.RequestNumber,
                    Status = Status.StatusConvertion(item.Status, item.IsWorkingOn),
                    RequestorName = item.RequestorName,
                    AssignTo = item.AssignTo,
                    AssignBoss = item.AssignBoss,
                    Category = item.Category,
                    TroubleSubject = item.TroubleSubject,
                    TroubleReason = Status.TroubleReasonConvertion(Convert.ToInt32(item.TroubleReason)),
                    SubCategory1 = item.SubCategory1,
                    SubCategory2 = item.SubCategory2,
                    SubCategory3 = item.SubCategory3,
                    SubCategory4 = item.SubCategory4,
                    SubCategory5 = item.SubCategory5,
                    Organization = item.Organization,
                    Department = item.Department,
                    Location = item.Location,
                    TroubleDescription = item.TroubleDescription,
                    TroubleSolution = item.ResolveDescription,
                };

                resultmodel.Add(resultitem);
            }

            return resultmodel.AsQueryable();
        }

        public static IQueryable<ExportResultModel> ToExportResultModel(this IQueryable<AsppCallRequest> request)
        {
            List<ExportResultModel> resultmodel = new List<ExportResultModel>();

            foreach (var item in request)
            {
                ExportResultModel resultitem = new ExportResultModel
                {
                    CreationDate = item.Creation.Value.ToShortDateString(),
                    CreationTime = item.Time.Value.ToShortTimeString(),
                    RequestNumber = item.RequestNumber,
                    Status = Status.StatusConvertion(item.Status, item.IsWorkingOn),
                    RequestorName = item.RequestorName,
                    AssignTo = item.AssignTo,
                    AssignBoss = item.AssignBoss,
                    Category = item.Category,
                    TroubleSubject = item.TroubleSubject,
                    TroubleReason = Status.TroubleReasonConvertion(Convert.ToInt32(item.TroubleReason)),
                    SubCategory1 = item.SubCategory1,
                    SubCategory2 = item.SubCategory2,
                    SubCategory3 = item.SubCategory3,
                    SubCategory4 = item.SubCategory4,
                    SubCategory5 = item.SubCategory5,
                    Organization = item.Organization,
                    Department = item.Department,
                    Location = item.Location,
                    TroubleDescription = item.TroubleDescription,
                    TroubleSolution = item.ResolveDescription,
                };

                resultmodel.Add(resultitem);
            }

            return resultmodel.AsQueryable();
        }

        public static IQueryable<ExportResultModel> ToExportResultModel(this IQueryable<SvyazCallRequest> request)
        {
            List<ExportResultModel> resultmodel = new List<ExportResultModel>();

            foreach (var item in request)
            {
                ExportResultModel resultitem = new ExportResultModel
                {
                    CreationDate = item.Creation.Value.ToShortDateString(),
                    CreationTime = item.Time.Value.ToShortTimeString(),
                    RequestNumber = item.RequestNumber,
                    Status = Status.StatusConvertion(item.Status, item.IsWorkingOn),
                    RequestorName = item.RequestorName,
                    AssignTo = item.AssignTo,
                    AssignBoss = item.AssignBoss,
                    Category = item.Category,
                    TroubleSubject = item.TroubleSubject,
                    TroubleReason = Status.TroubleReasonConvertion(Convert.ToInt32(item.TroubleReason)),
                    SubCategory1 = item.SubCategory1,
                    SubCategory2 = item.SubCategory2,
                    SubCategory3 = item.SubCategory3,
                    SubCategory4 = item.SubCategory4,
                    SubCategory5 = item.SubCategory5,
                    Organization = item.Organization,
                    Department = item.Department,
                    Location = item.Location,
                    TroubleDescription = item.TroubleDescription,
                    TroubleSolution = item.ResolveDescription,
                };

                resultmodel.Add(resultitem);
            }

            return resultmodel.AsQueryable();
        }

        public static IQueryable<ExportResultModel> ToExportResultModel(this IQueryable<PaCallRequest> request)
        {
            List<ExportResultModel> resultmodel = new List<ExportResultModel>();

            foreach (var item in request)
            {
                ExportResultModel resultitem = new ExportResultModel
                {
                    CreationDate = item.Creation.Value.ToShortDateString(),
                    CreationTime = item.Time.Value.ToShortTimeString(),
                    RequestNumber = item.RequestNumber,
                    Status = Status.StatusConvertion(item.Status, item.IsWorkingOn),
                    RequestorName = item.RequestorName,
                    AssignTo = item.AssignTo,
                    AssignBoss = item.AssignBoss,
                    Category = item.Category,
                    TroubleSubject = item.TroubleSubject,
                    TroubleReason = Status.TroubleReasonConvertion(Convert.ToInt32(item.TroubleReason)),
                    SubCategory1 = item.SubCategory1,
                    SubCategory2 = item.SubCategory2,
                    SubCategory3 = item.SubCategory3,
                    SubCategory4 = item.SubCategory4,
                    SubCategory5 = item.SubCategory5,
                    Organization = item.Organization,
                    Department = item.Department,
                    Location = item.Location,
                    TroubleDescription = item.TroubleDescription,
                    TroubleSolution = item.ResolveDescription,
                };

                resultmodel.Add(resultitem);
            }

            return resultmodel.AsQueryable();
        }


        public static IQueryable<SearchResultModel> ToSearchResultModel(this IQueryable<ItCallRequest> request)
        {
            List<SearchResultModel> resultmodel = new List<SearchResultModel>();
            
            foreach (var item in request)
            {
                SearchResultModel resultitem = new SearchResultModel
                {
                    CallRequestId = item.CallRequestId,
                    Creation = item.Creation,
                    Time = item.Time,
                    RequestNumber = item.RequestNumber,
                    Status = Status.StatusConvertion(item.Status,item.IsWorkingOn),
                    RequestorName = item.RequestorName,
                    AssignTo = item.AssignTo,
                    AssignBoss = item.AssignBoss,
                    Category = item.Category,
                    TroubleSubject = item.TroubleSubject,
                    Comment = item.Comment
                };
              
                resultmodel.Add(resultitem);
            }

            return resultmodel.AsQueryable();
        }

        public static IQueryable<SearchResultModel> ToSearchResultModel(this IQueryable<AsppCallRequest> request)
        {
            List<SearchResultModel> resultmodel = new List<SearchResultModel>();

            foreach (var item in request)
            {
                SearchResultModel resultitem = new SearchResultModel
                {
                    CallRequestId = item.CallRequestId,
                    Creation = item.Creation,
                    Time = item.Time,
                    RequestNumber = item.RequestNumber,
                    Status = Status.StatusConvertion(item.Status, item.IsWorkingOn),
                    RequestorName = item.RequestorName,
                    AssignTo = item.AssignTo,
                    AssignBoss = item.AssignBoss,
                    Category = item.Category,
                    TroubleSubject = item.TroubleSubject,
                    Comment = item.Comment
                };
                resultmodel.Add(resultitem);
            }

            return resultmodel.AsQueryable();
        }

        public static IQueryable<SearchResultModel> ToSearchResultModel(this IQueryable<PaCallRequest> request)
        {
            List<SearchResultModel> resultmodel = new List<SearchResultModel>();

            foreach (var item in request)
            {
                SearchResultModel resultitem = new SearchResultModel
                {
                    CallRequestId = item.CallRequestId,
                    Creation = item.Creation,
                    Time = item.Time,
                    RequestNumber = item.RequestNumber,
                    Status = Status.StatusConvertion(item.Status, item.IsWorkingOn),
                    RequestorName = item.RequestorName,
                    AssignTo = item.AssignTo,
                    AssignBoss = item.AssignBoss,
                    Category = item.Category,
                    TroubleSubject = item.TroubleSubject,
                    Comment = item.Comment
                };
                resultmodel.Add(resultitem);
            }

            return resultmodel.AsQueryable();
        }

        public static IQueryable<SearchResultModel> ToSearchResultModel(this IQueryable<SvyazCallRequest> request)
        {
            List<SearchResultModel> resultmodel = new List<SearchResultModel>();

            foreach (var item in request)
            {
                SearchResultModel resultitem = new SearchResultModel
                {
                    CallRequestId = item.CallRequestId,
                    Creation = item.Creation,
                    Time = item.Time,
                    RequestNumber = item.RequestNumber,
                    Status = Status.StatusConvertion(item.Status, item.IsWorkingOn),
                    RequestorName = item.RequestorName,
                    AssignTo = item.AssignTo,
                    AssignBoss = item.AssignBoss,
                    Category = item.Category,
                    TroubleSubject = item.TroubleSubject,
                    Comment = item.Comment
                };
                resultmodel.Add(resultitem);
            }

            return resultmodel.AsQueryable();
        }

        public static DataTable ListToDataTable<T>(this List<T> data)
        {
            DataTable datatable = new DataTable();
            if (data == null)
            {
                return null;
            }

            PropertyDescriptorCollection propcol = TypeDescriptor.GetProperties(typeof(T));
            foreach (PropertyDescriptor prop in propcol)
            {
                datatable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            object[] datavalues = new object[propcol.Count];
            foreach (T item in data)
            {

                for (int i = 0; i < datavalues.Length; i++)
                {
                    datavalues[i] = propcol[i].GetValue(item);
                }

                datatable.Rows.Add(datavalues);
            }

            return datatable;

        }
    } 
}