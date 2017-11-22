using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetroSupport.Models;

namespace MetroSupport.BLL.Interfaces
{
    public interface ITroubleSubjectRepository
    {
        IQueryable<TroubleSubject> GetTroubleSubjects();
        IQueryable<TroubleSubject> GetTroubleSubjectsByDepartment(string department);
        TroubleSubject GetTroubleSubjectById(string id);
        bool CreateNewTroubleSubject(TroubleSubject subject);
        bool SaveTroubleSubject(TroubleSubject subject);
        bool DeleteTroubleSubject(string id);
    }
}