using HR.Models.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class TicketPrice
    {
        public int ID { get; set; }
        public ClassType classtype { get; set; }
        public int From_air_port_Idd {get;set;}
        public virtual Air_ports From_Air_port { get; set; }
        public int To_air_port_Idd { get; set; }

        public virtual Air_ports To_Air_port { get; set; }
        public DateTime From_Date { get; set; }
        public DateTime TO_Date { get; set; }
        public double Price { get; set; }
    }

}