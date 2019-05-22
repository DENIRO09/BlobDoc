using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Microsoft.Build.Framework;


namespace AppDev3A.Models
{
    public class PhotoUpload
    {
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Please select file.")]
        public HttpPostedFileBase FileUpload { get; set; }
  
        public string StudentNumber { get; set; }

    }
}