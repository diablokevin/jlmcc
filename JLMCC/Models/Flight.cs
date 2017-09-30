using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace JLMCC.Models
{
    public class Flight
    {
        public int FlightId { get; set; }
        [Display(Name ="日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime Date { get; set; }

        [Display(Name = "航班号")]
        public string FlightNO { get; set; }
        [Display(Name = "飞机号")]
        public string PlaneNO { get; set; }
        [Display(Name = "机型")]
        public string PlaneType { get; set; }

        [Display(Name = "出发站")]
        public string DepartureCity { get; set; }

        [Display(Name = "停机位")]
        public string DepartureStandNO { get; set; }
        [Display(Name = "计划出发")]
        public DateTime ScheduleDeparture { get; set; }

        [Display(Name = "到达站")]
        public string ArriveCity { get; set; }

        [Display(Name = "停机位")]
        public string ArriveStandNO { get; set; }


        [Display(Name = "计划到达")]
        public DateTime ScheduleArrive { get; set; }
       

        public virtual ICollection<Service> Services { get; set; }

    }

    public enum FlightIntervalType
    {
        航前,
        过站,
        航后

    }
    //更改逻辑后，待删除
    //public class FlightInterval
    //{
    //    public int Id { get; set; }

    //    [Display(Name = "飞机号")]
    //    public string PlaneNO { get; set; }
    //    [Display(Name = "机型")]
    //    public string PlaneType { get; set; }

    //    [Display(Name = "维护类型")]
    //    public FlightIntervalType Type { get; set; }

    //    [Display(Name = "前序航班")]
    //    public int? PreFlightId { get; set; }
    //    [Display(Name = "后序航班")]
    //    public int? NextFlightId { get; set; }
    //    [Display(Name = "机场")]
    //    public string Station { get; set; }

    //    [System.ComponentModel.DataAnnotations.Schema.ForeignKey("PreFlightId")]
    //    public virtual Flight PreFlight { get; set; }
    //    [System.ComponentModel.DataAnnotations.Schema.ForeignKey("NextFlightId")]
    //    public virtual Flight NextFlight { get; set; }
    //}
    public class Section
    {
        public int key { get; set; }
        public string label { get; set; }

    }

    public class FlightTimeLine
    {
        public string text { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }

        public int section_id { get; set; }
    }

    public class FlightsJson
    {
      public List<Section> Sections { get; set; }
      public List<FlightTimeLine> Flights { get; set; }

    }
}