using Newtonsoft.Json;

namespace Thimble.Credentials.logging
{
    public class BaseLog
    {
        [JsonProperty] 
        public string TraceId { get; set; }
        
        [JsonProperty] 
        public string ProductId { get; set; }
        
        [JsonProperty] 
        public string Timestamp { get; set; }
        
        [JsonProperty] 
        public string Service { get; set; }
        
        [JsonProperty] 
        public string Action { get; set; }
    }
}