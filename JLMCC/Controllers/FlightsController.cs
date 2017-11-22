using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JLMCC.Models;

namespace JLMCC.Controllers
{
    public class FlightsController : Controller
    {
        private JlmccContext db = new JlmccContext();
       
        // GET: Flights
        public ActionResult Index(string date)
        {
            DateTime daySelected = new DateTime();
            
            if (date==null||date == string.Empty)
            {
                daySelected = DateTime.Now;

            }
            else
            {
                daySelected = Convert.ToDateTime(date);
            }
            List<Flight> flights = GetFlightsByDate(daySelected);

            //不需要在航班页面上显示维护内容了
            //List<SingleFlightViewModel> FlighViews = new List<SingleFlightViewModel>();
            //foreach (Flight item in flights)
            //{
            //    SingleFlightViewModel flightview = new SingleFlightViewModel()
            //    {
            //        Flight = item,
            //        Services = db.Services.Where(m => m.FlightIntervalId == item.FlightId).ToList()
            //    };
            //    FlighViews.Add(flightview);
            //}
            ViewBag.Date = date;
            return View(flights);
        }

        public List<Flight> GetFlightsByDate(DateTime daySelected)
        {
            //定义早6点到第二天早6点起飞的航班为当日航班
            DateTime begin = new DateTime(daySelected.Year, daySelected.Month, daySelected.Day, 6, 0, 0);
            DateTime end = new DateTime(daySelected.AddDays(1).Year, daySelected.AddDays(1).Month, daySelected.AddDays(1).Day, 6, 0, 0);
            List<Flight> flights = db.Flights.Where(m => m.ScheduleDeparture >= begin && m.ScheduleDeparture <= end).ToList();
            return flights;
        }
        
        // GET: Flights/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = db.Flights.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            return View(flight);
        }

        // GET: Flights/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Flights/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FlightId,Date,FlightNO,PlaneNO,PlaneType,DepartureCity,DepartureStandNO,ScheduleDeparture,ArriveCity,ArriveStandNO,ScheduleArrive")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                db.Flights.Add(flight);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(flight);
        }

        // GET: Flights/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = db.Flights.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            return View(flight);
        }

        // POST: Flights/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FlightId,Date,FlightNO,PlaneNO,PlaneType,DepartureCity,DepartureStandNO,ScheduleDeparture,ArriveCity,ArriveStandNO,ScheduleArrive")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                db.Entry(flight).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(flight);
        }

        // GET: Flights/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = db.Flights.Find(id);
            if (flight == null)
            {
                return HttpNotFound();
            }
            return View(flight);
        }

        // POST: Flights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
            Flight flight = db.Flights.Find(id);
            db.Flights.Remove(flight);
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

        public ActionResult Multi()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Multi(FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                string content = Request["List"];
                string action = collection["action"];
                string station = Request["Station"];
                DateTime daySelected =Convert.ToDateTime( Request["date"]);

                ViewBag.ContentString = content;
                    List<string> t = content.Split('\r', '\n').ToList();
                    ViewBag.Content = t;
                    ViewBag.Count = t.Count;
                int skipCount = 0;
                int faultCount = 0;
                int submitCount = 0;
                if (action == "提交")
                {
                    foreach (string item in t)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            Flight flight = new Flight()
                            {
                                Date = DateTime.Today,
                                FlightNO = item.Split('\t')[2].Trim(),
                                PlaneNO = item.Split('\t')[3].Trim(),
                                PlaneType = item.Split('\t')[4].Trim(),
                                DepartureCity = item.Split('\t')[5].Trim(),
                                DepartureStandNO = item.Split('\t')[6].Trim(),
                                ScheduleDeparture = SocTimeStringToDatetime(item.Split('\t')[7].Trim(), daySelected),
                                ArriveCity = item.Split('\t')[14].Trim(),
                                ArriveStandNO = item.Split('\t')[15].Trim(),
                                ScheduleArrive = SocTimeStringToDatetime(item.Split('\t')[16].Trim(), daySelected)
                            };

                            if(flight.ScheduleArrive==DateTime.MinValue||flight.ScheduleDeparture==DateTime.MinValue)
                            {
                                faultCount += 1;
                                continue;
                            }

                            var flightInDb = from f in db.Flights
                                              where f.Date == flight.Date &&
                                                    f.FlightNO == flight.FlightNO &&
                                                    f.DepartureCity==flight.DepartureCity &&
                                                    f.ScheduleDeparture==flight.ScheduleDeparture &&
                                                    f.ArriveCity==flight.ArriveCity &&
                                                    f.ScheduleArrive==flight.ScheduleArrive                                             
                                             select f;

                            if(flightInDb.Count()==0)
                            { db.Flights.Add(flight);
                               
                            }
                            else
                            {
                                skipCount += 1;
                            }
                                           
                           
                        }
                    }
                    submitCount = db.SaveChanges();
                   //不再随批量录入航班时生成航班间隔
                    //ViewBag.IntervalsCount=CreateFlightIntervalFromFlight(GetFlightsByDate(daySelected), station);
                }
                ViewBag.SubmitCount = submitCount;
                ViewBag.SkipCount = skipCount;
                ViewBag.FaultCount = faultCount;
                return View();
            }


            return View();
        }

        //随逻辑改变弃用，2017.9.27
        //批量录入航班时，批量生成航班间隔
        //public int CreateFlightIntervalFromFlight(List<Flight> flights, string station)
        //{
        //    List<Flight> flightOrdered = flights.OrderBy(m => m.PlaneNO).ThenBy(m => m.ScheduleDeparture).ToList();
        //    int x = 0;  //统计创建的数量
        //    for (int i = 0; i < flightOrdered.Count; i++)
        //    {

        //        if (flightOrdered[i].DepartureCity == station)  //出发为本场时，无前序航班为为航前，有前序航班为过站
        //        {
        //            FlightInterval flightInterval = new FlightInterval()
        //            {
        //                PlaneNO = flightOrdered[i].PlaneNO,
        //                PlaneType = flightOrdered[i].PlaneType,
        //                NextFlightId = flightOrdered[i].FlightId,
        //                Station = station
        //            };
        //            if (i == 0 || (flightOrdered[i - 1].PlaneNO != flightOrdered[i].PlaneNO))
        //            {
        //                flightInterval.Type = FlightIntervalType.航前;

        //            }
        //            else if (flightOrdered[i - 1].PlaneNO == flightOrdered[i].PlaneNO && flightOrdered[i - 1].ArriveCity == station)
        //            {
        //                flightInterval.Type = FlightIntervalType.过站;
        //                flightInterval.PreFlightId = flightOrdered[i - 1].FlightId;
        //            }


        //            if (!IsExistInFlightIntevals(flightInterval))
        //            {
        //                db.FlightIntervals.Add(flightInterval);
        //                x += db.SaveChanges();
        //            }

        //        }
        //        else if (flightOrdered[i].ArriveCity == station)  //落地为本场时，无后序航班为航后，有后序航班为过站，航班间隔将在后序航班中创建
        //        {
        //            if (i == (flightOrdered.Count - 1) || flightOrdered[i].PlaneNO != flightOrdered[i + 1].PlaneNO)
        //            {
        //                FlightInterval flightInterval = new FlightInterval()
        //                {
        //                    PlaneNO = flightOrdered[i].PlaneNO,
        //                    PlaneType = flightOrdered[i].PlaneType,
        //                    PreFlightId = flightOrdered[i].FlightId,
        //                    Station = station,
        //                    Type = FlightIntervalType.航后
        //                };
        //                if (!IsExistInFlightIntevals(flightInterval))
        //                {
        //                    db.FlightIntervals.Add(flightInterval);
        //                    x += db.SaveChanges();
        //                }

        //            }

        //        }
        //    }
        //    return x;
        //}

        //随逻辑改变弃用，2017.9.27
        //判断是否有重复导入航班间隔
        //private bool IsExistInFlightIntevals(FlightInterval flightinterval)
        //{
        //    var interval = from i in db.FlightIntervals
        //                   where i.PlaneNO == flightinterval.PlaneNO &&
        //                         i.Station == flightinterval.Station &&
        //                         i.NextFlightId == flightinterval.NextFlightId &&
        //                         i.PreFlightId == flightinterval.PreFlightId
        //                   select i;
        //    if(interval.Count()>0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        private DateTime SocTimeStringToDatetime(string soctime,DateTime daySelected)
        {
            //将soc上时间字符串转换为datatime
            int day = Convert.ToInt32( soctime.Substring(0, 2));
            int hour = Convert.ToInt32(soctime.Substring(3,2));
            int min = Convert.ToInt32(soctime.Substring(5, 2));
            if (day == daySelected.Day)
            {
                DateTime t = new DateTime(daySelected.Year, daySelected.Month, day, hour, min, 0);
                return t;
            }
            else if(day== daySelected.AddDays(1).Day)
            {
                DateTime t = new DateTime(daySelected.AddDays(1).Year, daySelected.AddDays(1).Month, day, hour, min, 0);
                return t;
            }
            else
            {
                return DateTime.MinValue;
            }
          
            
        }

        public JsonResult GetFlightsToJson(string date, string station)
        {
            DateTime daySelected = new DateTime();
            if (date == null || date == "")
            {
                daySelected = DateTime.Now;

            }
            else
            {
                daySelected = Convert.ToDateTime(date);
            }

            if (station == null || station == "")
            {
                station = "长春";
            }
            List<Flight> flights = GetFlightsByDate(daySelected).Where(m => m.ArriveCity == station || m.DepartureCity == station).ToList();


            var planes = flights.OrderBy(m => m.PlaneNO).Select(m => m.PlaneNO).Distinct();

            List<Section> sections = new List<Section>();
            int i = 0;
            foreach (var p in planes)
            {
                Section s = new Section()
                {
                    key = i,
                    label = p.ToString()
                };
                sections.Add(s);
                i = i + 1;
            }

            List<FlightTimeLine> timeLines = new List<FlightTimeLine>();

            foreach (Flight flight in flights)
            {
                FlightTimeLine timeline = new FlightTimeLine()
                {
                    section_id = sections.Where(m => m.label == flight.PlaneNO).First().key,
                    text = string.Format("{0}{1}{2}", flight.DepartureCity, flight.FlightNO, flight.ArriveCity),
                    start_date = flight.ScheduleDeparture.ToString(),
                    end_date = flight.ScheduleArrive.ToString(),
                    PlaneNO = flight.PlaneNO,
                    FlightId = flight.FlightId                    
                    
                };
                timeLines.Add(timeline);

            }
            FlightsJson result = new FlightsJson
            {
                Sections = sections,
                Flights = timeLines

        };


            return Json(result, JsonRequestBehavior.AllowGet);

        }

    

        public ActionResult Gantt()
        {

            return View();
        }

        public int ChangePlanNO(int flightId,string planeNO)
        {
            Flight flight = db.Flights.Find(flightId);
            flight.PlaneNO = planeNO;
            return db.SaveChanges();
        }
    }
}
