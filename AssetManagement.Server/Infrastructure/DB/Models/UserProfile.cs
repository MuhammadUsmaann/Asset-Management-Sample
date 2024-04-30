using Microsoft.AspNetCore.Identity;

namespace AssetManagement.Server.Infrastructure.DB.Models
{
    public class UserProfile : IdentityUser<int>
    {
        public string? UserFullname { get; set; }
        public int TeamId { get; set; }
        public int RoleId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
