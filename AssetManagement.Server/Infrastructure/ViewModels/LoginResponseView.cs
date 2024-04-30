namespace AssetManagement.Server.Infrastructure.ViewModels
{
    public class LoginResponseView
    {
        public long Id { get; set; }
        public string? Token { get; set; }
        public DateTime? Expiration { get; set; }
        public string? UserFullName { get; set; }
        public string? Email { get; set; }
        public int? Role { get; set; }
    }
}
