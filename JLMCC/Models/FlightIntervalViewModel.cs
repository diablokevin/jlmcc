using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JLMCC.Models
{
    public enum FlightIntervalType
    {
        航前,
        过站,
        航后

    }
    public class FlightIntervalViewModel
    {
        
        

            [Display(Name = "飞机号")]
            public string PlaneNO { get; set; }
            [Display(Name = "机型")]
            public string PlaneType { get; set; }

            [Display(Name = "维护类型")]
            public FlightIntervalType Type { get; set; }

            [Display(Name = "前序航班")]
            public  FlightInfo PreFlight { get; set; }
            [Display(Name = "后序航班")]
            public  FlightInfo NextFlight { get; set; }
            [Display(Name = "机场")]
            public string Station { get; set; }
       

    }
    public class FlightIntervalAndSerivcesViewModel
    {
        public FlightIntervalViewModel FlightInterval { get; set; }

        public List<Service> ServicesForPreFlight { get; set; }
        public List<Service> ServicesForNextFlight { get; set; }
    }
}