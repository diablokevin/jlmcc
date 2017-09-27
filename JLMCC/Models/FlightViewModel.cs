using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JLMCC.Models
{
 

    public class SingleFlightViewModel
    {
        public Flight Flight { get; set; }

        public List<Service> Services { get; set; }
    }

   
}