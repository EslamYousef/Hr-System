using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.Models
{
    public class Append_Public_Holidays_Dates
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Holiday / Event Code")]
        public string HolidayEventCode { get; set; }
        [Display(Name = "Holiday / Event Description")]
        public string HolidayEventDescriptioned { get; set; }  
        [Display(Name = "From date")]
        public DateTime Fromdate { get; set; }
        [Display(Name = "To date")]
        public DateTime Todate { get; set; }
        public string Notes { get; set; }
        public virtual Public_Holidays_Dates Public_Holidays_Dates { get; set; }

    }
}