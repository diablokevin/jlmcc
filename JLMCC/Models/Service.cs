using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JLMCC.Models
{
    public enum ServiceType
    {
        ForArrival,ForDepature
    }
    public class Service
    {
        public int ServiceId { get; set; }
        public string Content { get; set; }
        public string StaffId { get; set; }
        public string Name { get; set; }
        public DateTime ServiceTime { get; set; }
        public ServiceType Type { get; set; }
        [Required]
        public int FlightId { get; set; }
        public int FlightIntervalId { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("FlightId")]
        public virtual FlightInfo Flightinfo { get; set; }

    }
}