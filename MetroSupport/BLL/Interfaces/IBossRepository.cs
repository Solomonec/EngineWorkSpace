using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MetroSupport.Models
{
    public interface IBossRepository
    {
        IQueryable<AssignBoss> GetAllBosses();
        AssignBoss GetBossById(string bossid);
        IQueryable<AssignBoss> GetBossesByDepartment(string department);
        bool CreateNewBoss(AssignBoss boss);
        bool SaveBossChanges(AssignBoss boss);
        bool DeleteBoss(string bossid);
    }
}