using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HR.Areas.suberAdmin.Models
{
    public class Roles
    {
        public int iD { get; set; }
        public Modules roles { get; set; }
    }
    public enum Modules
    {
        Basic=1,
        organization=2,
        personnel=3
    }
}