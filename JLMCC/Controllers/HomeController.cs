using JLMCC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JLMCC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
         
            return View();
        }

        public ActionResult About()
        {
            
            ViewBag.Message = "Your application description page.";
            UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
                ViewBag.StaffId = user.StaffId;
            else
                ViewBag.StaffId = "User not found.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
          
                return View();
        }

        public ActionResult Settings()
        {
            return View();
        }
    }
}