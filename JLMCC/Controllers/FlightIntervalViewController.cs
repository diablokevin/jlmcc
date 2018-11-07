using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JLMCC.Models;
using System.Data;
using System.Data.Entity;

namespace JLMCC.Controllers
{
    public class FlightIntervalViewController : Controller
    {
        private JlmccContext db = new JlmccContext();
        // GET: FlightIntervalView
        public ActionResult Index(string date, string station)
        {
          
            DateTime daySelected = new DateTime();
            if (date == null||date=="")
            {
                daySelected = DateTime.Today;

            }
            else
            {
                daySelected = Convert.ToDateTime(date);
            }


            List<FlightIntervalViewModel> flightIntervals = GetFlightInterval(daySelected, station);
            ViewBag.Date = date;
            return View(flightIntervals);
        }

        public ActionResult IndexWithServices(string date, string station)
        {

            DateTime daySelected = new DateTime();
            if (date == null || date == "")
            {
                daySelected = DateTime.Today;

            }
            else
            {
                daySelected = Convert.ToDateTime(date);
            }

            List<FlightIntervalViewModel> flightIntervals = GetFlightInterval(daySelected, station);
            List<FlightIntervalAndSerivcesViewModel> Views = new List<FlightIntervalAndSerivcesViewModel>();

            foreach (var item in flightIntervals)
            {
                FlightIntervalAndSerivcesViewModel view = new FlightIntervalAndSerivcesViewModel()
                {
                    FlightInterval = item,
                    ServicesForPreFlight = item.PreFlight==null?null: db.Services.Where(m => m.FlightId == item.PreFlight.Id && m.Type == ServiceType.ForArrival).ToList(),
                    ServicesForNextFlight = item.NextFlight == null ? null : db.Services.Where(m => m.FlightId == item.NextFlight.Id && m.Type == ServiceType.ForDepature).ToList()
                 };

                Views.Add(view);
             }
            ViewBag.Date = daySelected.ToShortDateString();
            return View(Views);
        }
        //跟据参数读取航班数据，动态生成航班间隔
        public List<FlightIntervalViewModel> GetFlightInterval(DateTime daySelected, string station)
        {
        

            FlightInfoesController FlightinfoesController = new FlightInfoesController();
            //获取所选日期的航班新信息，去掉航班状态代码为C(取消)和DEL(改直飞)的航班
            List<FlightInfo> flightinfoes = FlightinfoesController.GetFlightInfoes(daySelected).Where(m=>m.LegStsCd != "C"&&m.LegStsCd != "DEL").ToList();
            if (station == null) { station = "长春"; }
            List<FlightInfo> flightinfoesOrdered = flightinfoes.OrderBy(m => m.LatestTailNr).ThenBy(m => m.SchDepDt.Value).ToList();
            List<FlightIntervalViewModel> flightIntervalViews = new List<FlightIntervalViewModel>();
            for (int i = 0; i < flightinfoesOrdered.Count; i++)
            {

                if (flightinfoesOrdered[i].ArcDepCityName == station)  //出发为本场时，无前序航班为为航前，有前序航班为过站
                {
                    FlightIntervalViewModel flightIntervalView = new FlightIntervalViewModel()
                    {
                        PlaneNO = flightinfoesOrdered[i].LatestTailNr,
                        PlaneType = flightinfoesOrdered[i].LatestEqpCd,
                        NextFlight = flightinfoesOrdered[i],
                        Station = station
                    };
                    if (i == 0 || (flightinfoesOrdered[i - 1].LatestTailNr != flightinfoesOrdered[i].LatestTailNr))
                    {
                        flightIntervalView.Type = FlightIntervalType.航前;

                    }
                    else if (flightinfoesOrdered[i - 1].LatestTailNr == flightinfoesOrdered[i].LatestTailNr && flightinfoesOrdered[i - 1].ArcArvCityName == station)
                    {
                        flightIntervalView.Type = FlightIntervalType.过站;
                        flightIntervalView.PreFlight = flightinfoesOrdered[i - 1];
                    }

                    flightIntervalViews.Add(flightIntervalView);



                }
                else if (flightinfoesOrdered[i].ArcArvCityName == station)  //落地为本场时，无后序航班为航后，有后序航班为过站，航班间隔将在后序航班中创建
                {
                    if (i == (flightinfoesOrdered.Count - 1) || flightinfoesOrdered[i].LatestTailNr != flightinfoesOrdered[i + 1].LatestTailNr)
                    {
                        FlightIntervalViewModel flightIntervalView = new FlightIntervalViewModel()
                        {
                            PlaneNO = flightinfoesOrdered[i].LatestTailNr,
                            PlaneType = flightinfoesOrdered[i].LatestEqpCd,
                            PreFlight = flightinfoesOrdered[i],
                            Station = station,
                            Type = FlightIntervalType.航后
                        };

                        flightIntervalViews.Add(flightIntervalView);
                    }

                }
            }

           // ViewBag.Date = date;

            return flightIntervalViews.OrderBy(i => i.PreFlight == null ? i.NextFlight.SchDepDt : i.PreFlight.SchArvDt).ToList();
        }

        public JsonResult GetFlightIntervalToJson(string date, string station)
        {
            DateTime daySelected = new DateTime();
            if (date == null || date == "")
            {
                daySelected = DateTime.Today;

            }
            else
            {
                daySelected = Convert.ToDateTime(date);
            }

            List<FlightIntervalViewModel> flightIntervals = GetFlightInterval(daySelected, station);
            ViewBag.Date = date;
           

            return Json(flightIntervals,JsonRequestBehavior.AllowGet);
        }

    }
}