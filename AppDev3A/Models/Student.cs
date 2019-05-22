using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
namespace AppDev3A.Models
{
    public class Student
    {
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

    }
}