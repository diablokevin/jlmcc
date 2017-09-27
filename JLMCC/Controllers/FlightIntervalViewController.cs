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
                daySelected = DateTime.Now;

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
                daySelected = DateTime.Now;

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
                    ServicesForPreFlight = item.PreFlight==null?null: db.Services.Where(m => m.FlightId == item.PreFlight.FlightId && m.Type == ServiceType.ForArrival).ToList(),
                    ServicesForNextFlight = item.NextFlight == null ? null : db.Services.Where(m => m.FlightId == item.NextFlight.FlightId && m.Type == ServiceType.ForDepature).ToList()
                 };

                Views.Add(view);
             }
            ViewBag.Date = date;
            return View(Views);
        }
        //跟据参数读取航班数据，动态生成航班间隔
        public List<FlightIntervalViewModel> GetFlightInterval(DateTime daySelected, string station)
        {
        

            FlightsController FlightsController = new FlightsController();

            List<Flight> flights =FlightsController.GetFlightsByDate(daySelected);
            if (station == null) { station = "长春"; }
            List<Flight> flightOrdered = flights.OrderBy(m => m.PlaneNO).ThenBy(m => m.ScheduleDeparture).ToList();
            List<FlightIntervalViewModel> flightIntervalViews = new List<FlightIntervalViewModel>();
            for (int i = 0; i < flightOrdered.Count; i++)
            {

                if (flightOrdered[i].DepartureCity == station)  //出发为本场时，无前序航班为为航前，有前序航班为过站
                {
                    FlightIntervalViewModel flightIntervalView = new FlightIntervalViewModel()
                    {
                        PlaneNO = flightOrdered[i].PlaneNO,
                        PlaneType = flightOrdered[i].PlaneType,
                        NextFlight = flightOrdered[i],
                        Station = station
                    };
                    if (i == 0 || (flightOrdered[i - 1].PlaneNO != flightOrdered[i].PlaneNO))
                    {
                        flightIntervalView.Type = FlightIntervalType.航前;

                    }
                    else if (flightOrdered[i - 1].PlaneNO == flightOrdered[i].PlaneNO && flightOrdered[i - 1].ArriveCity == station)
                    {
                        flightIntervalView.Type = FlightIntervalType.过站;
                        flightIntervalView.PreFlight = flightOrdered[i - 1];
                    }

                    flightIntervalViews.Add(flightIntervalView);



                }
                else if (flightOrdered[i].ArriveCity == station)  //落地为本场时，无后序航班为航后，有后序航班为过站，航班间隔将在后序航班中创建
                {
                    if (i == (flightOrdered.Count - 1) || flightOrdered[i].PlaneNO != flightOrdered[i + 1].PlaneNO)
                    {
                        FlightIntervalViewModel flightIntervalView = new FlightIntervalViewModel()
                        {
                            PlaneNO = flightOrdered[i].PlaneNO,
                            PlaneType = flightOrdered[i].PlaneType,
                            PreFlight = flightOrdered[i],
                            Station = station,
                            Type = FlightIntervalType.航后
                        };

                        flightIntervalViews.Add(flightIntervalView);
                    }

                }
            }

           // ViewBag.Date = date;

            return flightIntervalViews.OrderBy(i => i.PreFlight == null ? i.NextFlight.ScheduleDeparture : i.PreFlight.ScheduleArrive).ToList();
        }

    }
}