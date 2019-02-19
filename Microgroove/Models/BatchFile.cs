using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microgroove.Models
{
    public class BatchFile
    {
        [JsonProperty(PropertyName = "date")]
        public DateTime BatchFileDate { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string BatchFileType { get; set; }

        [JsonProperty(PropertyName = "orders")]
        public List<Order> Orders { get; set; }

        [JsonProperty(PropertyName = "ender")]
        public Ender FileEnder { get; set; }
    }
}
