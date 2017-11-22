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
    public class TroubleSubjectRepository: ITroubleSubjectRepository
    {
        private readonly MetroSupportContext _metro;

        public TroubleSubjectRepository(MetroSupportContext metro)
        {
            _metro = metro;
        }

        public IQueryable<TroubleSubject> GetTroubleSubjects()
        {
            return _metro.TroubleSubjects;
        }

        public IQueryable<TroubleSubject> GetTroubleSubjectsByDepartment(string department)
        {
            if (department != String.Empty)
            {
                return _metro.TroubleSubjects.Where(x => x.Department == department).OrderBy(x=>x.SubjectName);

            }
            else return null;
        }

        public TroubleSubject GetTroubleSubjectById(string id)
        {
            if (id != String.Empty)
            {
                Guid guid = Guid.Parse(id);
                return _metro.TroubleSubjects.FirstOrDefault(x => x.SubjectId == guid);

            }
            else return null;
        }

      public bool CreateNewTroubleSubject(TroubleSubject subject)
        {
            if (subject != null)
            {
                _metro.TroubleSubjects.Add(subject);
                _metro.SaveChanges();

                return true;
            }
            else return false;
        }

        public bool SaveTroubleSubject(TroubleSubject subject)
        {
            TroubleSubject currentsubject = _metro.TroubleSubjects.FirstOrDefault(x => x.SubjectId == subject.SubjectId);
            if (currentsubject != null)
            {
                currentsubject.SubjectName = subject.SubjectName;
                currentsubject.Department = subject.Department;
                _metro.Entry(currentsubject).State = EntityState.Modified;
                _metro.SaveChanges();
                return true;
            }
            else return false;
        }

        public bool DeleteTroubleSubject(string id)
        {
           if (id != String.Empty)
            {
                Guid guid = Guid.Parse(id);
                TroubleSubject subject = _metro.TroubleSubjects.FirstOrDefault(x => x.SubjectId == guid);
                if (subject != null)
                {
                    _metro.Entry(subject).State = EntityState.Deleted;
                    _metro.SaveChanges();
                    return true;
                }
                else return false;
            }
            else return false;
        }
    }
}