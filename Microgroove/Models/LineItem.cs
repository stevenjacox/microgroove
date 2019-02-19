using Newtonsoft.Json;

namespace Microgroove.Models
{
    public class LineItem
    {
        [JsonProperty(PropertyName = "sku")]
        public string LineItemSKU { get; set; }
        [JsonProperty(PropertyName = "qty")]
        public int LineItemQuantity { get; set; }
    }
}
