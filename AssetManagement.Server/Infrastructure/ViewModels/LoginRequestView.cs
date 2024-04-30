using System.ComponentModel.DataAnnotations;

namespace AssetManagement.Server.Infrastructure.ViewModels
{
    public class LoginRequestView
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
