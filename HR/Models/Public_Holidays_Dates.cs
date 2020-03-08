using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.Models
{
    public class Public_Holidays_Dates
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessageResourceType = typeof(HR.Resource.Basic), ErrorMessageResourceName = "error_message")]
        [Display(Name = "Holidays Code")]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string Holidays_Code { get; set; }
        [Display(Name = "Holidays Code")]
        public int Holidaysyear { get; set; }
        [Display(Name = "Holidays description")]
        public string Holiday_description { get; set; }
        [Display(Name = "Holiday alternative description")]
        public string Holiday_alternative_description { get; set; }
        [ForeignKey("Public_Holiday_Events")]
        [Display(Name = "Holiday / Event Code")]
        public int PublicHolidayEventsId { get; set; }
        public virtual Public_Holiday_Events Public_Holiday_Events { get; set; }
        public virtual List<Append_Public_Holidays_Dates> Append_Public_Holidays_Dates { get; set; }


    }
}