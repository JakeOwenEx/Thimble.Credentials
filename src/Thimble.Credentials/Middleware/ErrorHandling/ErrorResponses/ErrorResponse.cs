using Newtonsoft.Json;

namespace Thimble.UserAccount.Middleware.ErrorHandling.ErrorResponses
{
    public class ErrorResponse
    {
        [JsonProperty] public string Message { get; set; }
    }
}