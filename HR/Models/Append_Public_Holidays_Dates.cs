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
        //[ForeignKey("Public_Holiday_Events")]
        //[Display(Name = "Holiday / Event Code")]
        public int Public_Holidays_DatesId { get; set; }
        //[ForeignKey("Public_Holiday_Events")]
        //[Display(Name = "Holiday / Event Description")]
        public int Public_Holiday_EventsId { get; set; }


        public virtual Public_Holiday_Events Public_Holiday_Events { get; set; }
        [Display(Name = "From date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Fromdate { get; set; }
        [Display(Name = "To date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Todate { get; set; }
        public string Notes { get; set; }
        public virtual Public_Holidays_Dates Public_Holidays_Dates { get; set; }

    }
}