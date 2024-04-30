using AssetManagement.Server.Infrastructure.DB.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AssetManagement.Server.Infrastructure.DB.Context
{
    public class AssetManagementDbContext : IdentityDbContext<UserProfile, IdentityRole<int>, int>
    {
        public AssetManagementDbContext(DbContextOptions<AssetManagementDbContext> options)
       : base(options)
        {
        }

        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<Team> Teams { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken()) =>
        await base.SaveChangesAsync(cancellationToken);

        public virtual async Task<int> SaveChangesAsync(string username, CancellationToken cancellationToken = new CancellationToken())
        {
            SetModifiedInformation(username);
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void SetModifiedInformation(string username)
        {
            var entityEntries = ChangeTracker.Entries()
                                             .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entityEntry in entityEntries)
            {
                if (entityEntry.State == EntityState.Added)
                {
                    if (entityEntry.Metadata.FindProperty("CreatedBy") != null)
                        entityEntry.Property("CreatedBy").CurrentValue = username;

                    if (entityEntry.Metadata.FindProperty("CreatedAt") != null)
                        entityEntry.Property("CreatedAt").CurrentValue = DateTime.UtcNow;
                }

                if (entityEntry.State == EntityState.Modified)
                {
                    if (entityEntry.Metadata.FindProperty("UpdatedBy") != null)
                        entityEntry.Property("UpdatedBy").CurrentValue = username;

                    if (entityEntry.Metadata.FindProperty("UpdatedAt") != null)
                        entityEntry.Property("UpdatedAt").CurrentValue = DateTime.UtcNow;
                }
            }
        }
    }
}
