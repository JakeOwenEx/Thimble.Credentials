using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Thimble.Credentials.Controllers.Models.Login
{
    public class LoginRequest
    {
        [Required]
        [JsonProperty] 
        [EmailValidation]
        [StringLength(100)]
        public string Email { get; set; }
        
        [Required]
        [JsonProperty] 
        [StringLength(100)]
        public string Password { get; set; }

    }
}