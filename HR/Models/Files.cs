using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace HR.Models
{
    public class Files
    {
        public int id { get; set; }
        public string filename { get; set; }
        public byte[] File { get; set; }
        

    }
}