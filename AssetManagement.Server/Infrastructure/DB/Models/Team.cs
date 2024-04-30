namespace AssetManagement.Server.Infrastructure.DB.Models
{
    public class Team : BaseEntity
    {
        public string? Name { get; set; }
        public bool IsActive { get; set; }
    }
}
