using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JLMCC.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<SubDepartment> SubDepartments { get; set; }
    }

    public class SubDepartment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
     
        public virtual Department Department { get; set; }
    }
}