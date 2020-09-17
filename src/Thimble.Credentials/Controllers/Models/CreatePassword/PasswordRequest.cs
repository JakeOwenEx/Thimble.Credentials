using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Thimble.Credentials.Controllers.Models.CreatePassword
{
    public class PasswordRequest
    {
        public PasswordRequest()
        {
            UserId = System.Guid.NewGuid().ToString();
        }
        [Required]
        [JsonProperty] 
        [EmailValidation]
        [StringLength(100)]
        public string Email { get; set; }
        
        [Required]
        [JsonProperty] 
        [StringLength(100)]
        public string Password { get; set; }

        [JsonProperty] public string UserId { get; set; }
    }
}