using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models.ViewModel
{
    public class CheckRequestVM
    {
        public int iD { get; set; }
        public string requestNumber { get; set; }
        public string requestDate { get; set; }
        public string checkType { get; set; }
        public double amount { get; set; }
        public string checkNumber { get; set; }
        public string checkRequestStatues { get; set; }

    }
}