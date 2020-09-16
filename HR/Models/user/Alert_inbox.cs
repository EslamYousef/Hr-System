using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models.user
{
    public class Alert_inbox
    {
        public int ID { get; set; }
        public string title { get; set; }
        public string Subject { get; set; }
        public bool Read { get; set; } = false;
        public DateTime? until { get; set; }
        public string send_to_user_id { get; set; }
        public string send_from_user_id { get; set; }
        public string check_name { get; set; }
    }
}