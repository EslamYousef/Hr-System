using HR.Models.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class check_request_status:BaseModel
    {
        public check_status status { get; set; }
    }
}