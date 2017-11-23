using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace JLMCC.Models
{
    public class JlmccContext:DbContext
    {
        public JlmccContext()
            :base("DefaultConnection")
        {

        }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Service> Services { get; set; }

        public DbSet<FlightInfo> FlightInfos { get; set; }

        //public DbSet<FlightInterval> FlightIntervals { get; set; }

        public System.Data.Entity.DbSet<JLMCC.Models.Department> Departments { get; set; }

        public System.Data.Entity.DbSet<JLMCC.Models.SubDepartment> SubDepartments { get; set; }
    }
}