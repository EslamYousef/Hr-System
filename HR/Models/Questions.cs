using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class Questions
    {
        public int ID { get; set; }
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string Code { get; set; }
        public string Question { get; set; }
        public string Standart_Question { get; set; }
        public string Attachmentfile { get; set; }
        public virtual Files Files { get; set; }
        public string Filesid { get; set; }
        public int TestId { get; set; }

    }
}