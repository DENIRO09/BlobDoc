using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using System.Configuration;
using System.IO;

namespace AppDev3A.Models
{
    public class VM
    {
        [Key]
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "Surname")]
        public string Surname { get; set; }
        [JsonProperty(PropertyName = "Email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "Telephone Number")]
        public string TelephoneNumber { get; set; }
        [JsonProperty(PropertyName = "Celphone Number")]
        public string CellphoneNumber { get; set; }
        [JsonProperty(PropertyName = "isActive")]
        public bool isActive { get; set; }

        //[System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Please select file.")]
        public byte[] FileUpload { get; set; }
        public string StudentNumber { get; set; }
        public string NameView { get; set; } //For Displaying
        public string URI { get; set; } //For Displaying
    }
}