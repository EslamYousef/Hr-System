//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HR
{
    using System;
    using System.Collections.Generic;
    
    public partial class TicketPrice
    {
        public int ID { get; set; }
        public int classtype { get; set; }
        public int From_air_port_Idd { get; set; }
        public int To_air_port_Idd { get; set; }
        public System.DateTime From_Date { get; set; }
        public System.DateTime TO_Date { get; set; }
        public double Price { get; set; }
        public Nullable<int> From_Air_port_ID { get; set; }
        public Nullable<int> To_Air_port_ID { get; set; }
    
        public virtual Air_ports Air_ports { get; set; }
        public virtual Air_ports Air_ports1 { get; set; }
    }
}