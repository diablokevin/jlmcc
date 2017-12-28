using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JLMCC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
namespace JLMCC.Controllers
{
    public class ServicesController : Controller
    {
        private JlmccContext db = new JlmccContext();

        // GET: Services
        public ActionResult Index(string date)
        {
        
            return View();
        }
      
        // GET: Services/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // GET: Services/Create
        public ActionResult Create()
        {
            ViewBag.FlightId = new SelectList(db.FlightInfoes, "Id", "FltNr");
            return View();
        }

        // POST: Services/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ServiceId,Content,Name,ServiceTime,FlightId")] Service service)
        {
            if (ModelState.IsValid)
            {
                db.Services.Add(service);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FlightId = new SelectList(db.FlightInfoes, "Id", "FltNr", service.FlightIntervalId);
            return View(service);
        }

        // GET: Services/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            ViewBag.FlightId = new SelectList(db.FlightInfoes, "Id", "FltNr", service.FlightIntervalId);
            return View(service);
        }

        // POST: Services/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ServiceId,Content,Name,ServiceTime,FlightId")] Service service)
        {
            if (ModelState.IsValid)
            {
                db.Entry(service).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FlightId = new SelectList(db.FlightInfoes, "Id", "FltNr", service.FlightIntervalId);
            return View(service);
        }

        // GET: Services/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Service service = db.Services.Find(id);
            db.Services.Remove(service);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //更改services与flight的关联逻辑
        //public ActionResult Enter(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    FlightInterval flightInterval = db.FlightIntervals.Find(id);
        //    if (flightInterval == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    FlightIntervalAndSerivcesViewModel view = new FlightIntervalAndSerivcesViewModel()
        //    {
        //        FlightInterval = flightInterval,
        //        Services = db.Services.Where(m => m.FlightIntervalId == flightInterval.Id).ToList()
        //    };
        //    return View(view);
        //}
        public ActionResult Enter(int PreFlightId, int NextFlightId,string backUrl)
        {
            if (PreFlightId == -1&& NextFlightId==-1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlightIntervalViewModel flightIntervalView = new FlightIntervalViewModel();
          
            FlightIntervalAndSerivcesViewModel view = new FlightIntervalAndSerivcesViewModel();
            if (PreFlightId==-1&& NextFlightId!=-1)
            {
                
                flightIntervalView.Type = FlightIntervalType.航前;
                flightIntervalView.NextFlight = db.FlightInfoes.Find(NextFlightId);
                flightIntervalView.PlaneNO = flightIntervalView.NextFlight.LatestTailNr;
                flightIntervalView.PlaneType = flightIntervalView.NextFlight.LatestEqpCd;
                flightIntervalView.Station = flightIntervalView.NextFlight.ArcDepCityName;
                
                view.FlightInterval = flightIntervalView;
                view.ServicesForNextFlight = db.Services.Where(m => m.FlightId == NextFlightId&&m.Type==ServiceType.ForDepature).ToList();

            }
            else if (PreFlightId != -1 && NextFlightId != -1)
            {
                flightIntervalView.Type = FlightIntervalType.过站;
                flightIntervalView.PreFlight = db.FlightInfoes.Find(PreFlightId);
                flightIntervalView.NextFlight = db.FlightInfoes.Find(NextFlightId);
                flightIntervalView.PlaneNO = flightIntervalView.NextFlight.LatestTailNr;
                flightIntervalView.PlaneType = flightIntervalView.NextFlight.LatestEqpCd;
                flightIntervalView.Station = flightIntervalView.NextFlight.ArcDepCityName;

                view.FlightInterval = flightIntervalView;
                view.ServicesForPreFlight = db.Services.Where(m => m.FlightId == PreFlightId && m.Type == ServiceType.ForArrival).ToList();
                view.ServicesForNextFlight = db.Services.Where(m => m.FlightId == NextFlightId && m.Type == ServiceType.ForDepature).ToList();



            }
            else if (PreFlightId != -1 && NextFlightId == -1)
            {
                flightIntervalView.Type = FlightIntervalType.航后;
                flightIntervalView.PreFlight = db.FlightInfoes.Find(PreFlightId);
                flightIntervalView.PlaneNO = flightIntervalView.PreFlight.LatestTailNr;
                flightIntervalView.PlaneType = flightIntervalView.PreFlight.LatestEqpCd;
                flightIntervalView.Station = flightIntervalView.PreFlight.ArcArvCityName;
                view.FlightInterval = flightIntervalView;
                view.ServicesForPreFlight = db.Services.Where(m => m.FlightId == PreFlightId).ToList();
                
            }
            ViewBag.backUrl = backUrl;

            return View(view);
        }

        //不知道是否有用，先注释一下，以后再删除 2017.9.26
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Enter(Flight flight)
        //{
            
        //    if (ModelState.IsValid)
        //    {
        //        Service service = new Service();

        //        UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        //        var user = UserManager.FindById(User.Identity.GetUserId());
        //        service.Name =user.RealName;
        //        service.StaffId = user.StaffId;
        //        service.ServiceTime = DateTime.Now;
        //        service.FlightIntervalId = flight.FlightId;
        //       // service.Content = collection["action"];

        //        db.Services.Add(service);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(flight);
        //}
        [Authorize]
        public ActionResult CreateService(int flightId,string content,ServiceType type)
        {//跟据内容创建不同的service
            Service service = new Service();
            UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = UserManager.FindById(User.Identity.GetUserId());

            service.Name = user.RealName;
            service.StaffId = user.StaffId;
            service.ServiceTime = DateTime.Now;
            service.FlightId = flightId;
            service.Content = content;
            service.Type = type;

            db.Services.Add(service);
            db.SaveChanges();
            return Redirect(HttpContext.Request.UrlReferrer.ToString());
        }
        [Authorize]
        public async Task<string> CreateServiceByAjax(int flightId, string content, ServiceType type)
        {
            Service service = new Service();
            UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user =await UserManager.FindByIdAsync(User.Identity.GetUserId());

            service.Name = user.RealName;
            service.StaffId = user.StaffId;
            service.ServiceTime = DateTime.Now;
            service.FlightId = flightId;
            service.Content = content;
            service.Type = type;

            db.Services.Add(service);
            int a=db.SaveChanges();
            if(a>=1)
            {
                //string s=string.Format("{0},{1},{2},{3},{4}",service.Name,service.StaffId,service.ServiceTime, service.FlightIntervalId, service.Content);
                string s= service.ServiceTime.ToShortTimeString();
                return s;
            }
            else
            {
                return "error";
            }
            
        }
    }
}
