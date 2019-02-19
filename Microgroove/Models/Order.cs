using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Microgroove.Models
{
    public class Order
    {
        [JsonProperty(PropertyName ="date")]
        public DateTime OrderDate { get; set; }

        [JsonProperty(PropertyName = "code")]
        public string OrderCode { get; set; }

        [JsonProperty(PropertyName = "number")]
        public string OrderNumber { get; set; }

        [JsonProperty(PropertyName = "buyer")]
        public Buyer OrderBuyer { get; set; }

        [JsonProperty(PropertyName = "items")]
        public List<LineItem> OrderLineItems { get; set; }

        [JsonProperty(PropertyName = "timings")]
        public Timings OrderTimings { get; set; }
    }
}
