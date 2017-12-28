using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace JLMCC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            //自动迁移
            Database.SetInitializer<JLMCC.Models.ApplicationDbContext>(new MigrateDatabaseToLatestVersion<JLMCC.Models.ApplicationDbContext, JLMCC.UserContextMigrations.Configuration>());
            var dbMigrator1 = new DbMigrator(new JLMCC.UserContextMigrations.Configuration());
            dbMigrator1.Update();

            Database.SetInitializer<JLMCC.Models.JlmccContext>(new MigrateDatabaseToLatestVersion<JLMCC.Models.JlmccContext, JLMCC.DataContextMigrations.Configuration>());
            var dbMigrator2 = new DbMigrator(new JLMCC.DataContextMigrations.Configuration());
            dbMigrator2.Update();


        }
    }
}
