using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models.Time_management
{
    public class TransportationMethod:BaseModel
    {
        public virtual List<Business_Trip> Business_Trip { get; set; }
        public virtual List<business_trip_request> business_trip_request { get; set; }
    }
}