using Newtonsoft.Json;

namespace EtcdProvider
{
    public class Response
    {
        [JsonProperty(PropertyName = "action")]
        public string Action { get; set; }

        [JsonProperty(PropertyName = "node")]
        public Node Node { get; set; }

        [JsonProperty(PropertyName = "prevNode")]
        public Node PrevNode { get; set; }
    }
}