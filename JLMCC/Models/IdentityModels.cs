﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace JLMCC.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // 请注意，authenticationType 必须与 CookieAuthenticationOptions.AuthenticationType 中定义的相应项匹配
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // 在此处添加自定义用户声明
           
            
            return userIdentity;
        }

        public string StaffId { get; set; }
       public string RealName { get; set; }

        //public string test { get; set; }
        //public int DepartmentId { get; set; }
        //public int SubdepartmentId { get; set; }
        //public virtual Department Department { get; set; }
        //public virtual SubDepartment Subdepartment { get; set; }
    }
    public class ApplicationRole:IdentityRole
    {
        public ApplicationRole() : base() { }

        public ApplicationRole(string name) : base(name) { }
        // 在此添加额外属性
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}