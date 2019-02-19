using Newtonsoft.Json;

namespace Microgroove.Models
{
    public class Timings
    {
        [JsonProperty(PropertyName = "start")]
        public int Start { get; set; }
        [JsonProperty(PropertyName = "stop")]
        public int Stop { get; set; }
        [JsonProperty(PropertyName = "gap")]
        public int Gap { get; set; }
        [JsonProperty(PropertyName = "offset")]
        public int Offset { get; set; }
        [JsonProperty(PropertyName = "pause")]
        public int Pause { get; set; } 
    }
}
