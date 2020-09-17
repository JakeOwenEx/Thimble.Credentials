using Newtonsoft.Json;

namespace Thimble.Credentials.Controllers.Models.Login
{
    public class LoginResponse
    {
        [JsonProperty] 
        public bool Verified { get; set; }
    }
}