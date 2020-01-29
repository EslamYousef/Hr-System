using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models.ViewModel
{
    public class EOS_VM
    {
        public EOS_Request EOS { get; set; }
        public int selected_employee { get; set; }
        public int selected_EOS_group_interview { get; set; }
        public int selected_EOS_group_checklist { get; set; }

    }
}