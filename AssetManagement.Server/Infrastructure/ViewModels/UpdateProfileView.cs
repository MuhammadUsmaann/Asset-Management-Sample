using System.ComponentModel.DataAnnotations;

namespace AssetManagement.Server.Infrastructure.ViewModels
{
    public class UpdateProfileView
    {
        [Required]
        public int Id { get; set; }

        [EmailAddress]
        [Required]
        public string? Email { get; set; }
        public string? UserFullname { get; set; }
        public int? RoleId { get; set; }
        public int? StatusId { get; set; }
        public string? Remarks { get; set; }
    }
}
