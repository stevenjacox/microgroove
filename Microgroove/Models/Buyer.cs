using Newtonsoft.Json;

namespace Microgroove.Models
{
    public class Buyer
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "street")]
        public string Street { get; set; }
        [JsonProperty(PropertyName = "zip")]
        public string Zip { get; set; }
    }
}
