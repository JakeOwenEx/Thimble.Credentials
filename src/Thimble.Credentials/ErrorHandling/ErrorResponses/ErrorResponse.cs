using Newtonsoft.Json;

namespace Thimble.Credentials.ErrorHandling.ErrorResponses
{
    public class ErrorResponse
    {
        [JsonProperty] public string Message { get; set; }
    }
}