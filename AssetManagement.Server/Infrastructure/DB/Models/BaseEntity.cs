using System.ComponentModel.DataAnnotations;

namespace AssetManagement.Server.Infrastructure.DB.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
