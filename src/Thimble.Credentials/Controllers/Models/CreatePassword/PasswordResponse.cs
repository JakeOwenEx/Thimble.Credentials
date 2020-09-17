using Newtonsoft.Json;

namespace Thimble.Credentials.Controllers.Models.CreatePassword
{
    public class PasswordResponse
    {
        [JsonProperty] 
        public string UserId { get; set; }
    }
}