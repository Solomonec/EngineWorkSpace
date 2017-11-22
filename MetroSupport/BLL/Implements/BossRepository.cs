using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MetroSupport.Models;

namespace MetroSupport.BLL.Implements
{
    public class BossRepository:IBossRepository
    {
          public readonly MetroSupportContext _metro;

          public BossRepository(MetroSupportContext metro)
        {
            _metro = metro;
        }

          public IQueryable<AssignBoss> GetAllBosses()
        {
            return _metro.AssignBoss;
        }

          public AssignBoss GetBossById(string bossid)
        {
            if (bossid != String.Empty)
            {
                return _metro.AssignBoss.FirstOrDefault(x => x.BossId == Guid.Parse(bossid));
            }
            return null;
        }

          public IQueryable<AssignBoss> GetBossesByDepartment(string department)
        {
            if (department != String.Empty)
            {
                return _metro.AssignBoss.Where(x => x.Department == department).OrderBy(x=>x.BossName);
            }
            return null;
        }

        public bool CreateNewBoss(AssignBoss boss)
        {
            if (boss != null)
            {
                _metro.AssignBoss.Add(boss);
                _metro.SaveChanges();

                return true;
            }
            else return false;
        }

        public bool SaveBossChanges(AssignBoss boss)
        {
            AssignBoss assignBoss = _metro.AssignBoss.FirstOrDefault(x =>x.BossId == boss.BossId);
            if (assignBoss != null)
            {
                assignBoss.BossName = boss.BossName;
                assignBoss.Organization = boss.Organization;
                assignBoss.Department = boss.Department;
                _metro.Entry(assignBoss).State = EntityState.Modified;
                _metro.SaveChanges();
                return true;
            }
            else return false;
        }

        public bool DeleteBoss(string bossid)
        {
            if (bossid != String.Empty)
            {
                Guid guidid = Guid.Parse(bossid);
                AssignBoss assignBoss = _metro.AssignBoss.FirstOrDefault(x => x.BossId == guidid);
                if (assignBoss != null)
                {
                    _metro.Entry(assignBoss).State = EntityState.Deleted;
                    _metro.SaveChanges();
                    return true;
                }
                else return false;
            }
            else return false;
        }
    }
}