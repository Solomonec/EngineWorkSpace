using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using MetroSupport.Models;
using WebMatrix.WebData;

namespace MetroSupport.Commons
{
    public class DbInitializer
    {
        public static void InitAllMetroDatabases()
        {

                using (UsersContext userscontext = new UsersContext())
                {
                    if (!userscontext.Database.Exists())
                    {
                        ((IObjectContextAdapter) userscontext).ObjectContext.CreateDatabase();
                    }
                }

                WebSecurity.InitializeDatabaseConnection("MetroAccountConnection", "UserProfile", "UserId", "UserName",
                    autoCreateTables: true);

                using (MetroSupportContext metrocontext = new MetroSupportContext())
                {
                    if (!metrocontext.Database.Exists())
                    {
                        metrocontext.Database.Create();
                    }
                }
          
        }
    }
}