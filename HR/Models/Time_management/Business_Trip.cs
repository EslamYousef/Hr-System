using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models.Time_management
{
    public class Business_Trip:BaseModel
    {
        public double alloeancereateberdaye { get; set; }
        public virtual TransportationMethod TransportationMethod { get; set; }
        public int? TransportationMethodID { get; set; }

        public int? transporation_return_ID { get; set; }

        public string transporation_return_name { get; set; }
        public virtual Shiftdaystatus Shiftdaystatus { get; set; }
        public int? ShiftdaystatusID { get; set; }
        public bool linkedtomnothelypayroll { get; set; }
        public bool linkedtomanyalpayment { get; set; }

    }
}