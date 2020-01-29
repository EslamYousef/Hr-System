﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HR.Models
{
    
        public class FileDetailsModel
        {
            public int Id { get; set; }
            [Display(Name = "Uploaded File")]
            public string FileName { get; set; }
            public byte[] FileContent { get; set; }


        }
    
}