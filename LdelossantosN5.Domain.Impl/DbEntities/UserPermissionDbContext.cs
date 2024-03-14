using LdelossantosN5.Domain.Impl.DbEntities.Config;
using Microsoft.EntityFrameworkCore;

namespace LdelossantosN5.Domain.Impl.DbEntities
{
    public class UserPermissionDbContext
        : DbContext
    {
        public UserPermissionDbContext(DbContextOptions options)
            : base(options)
        {

        }
        /// <summary>
        /// Employee security settings
        /// </summary>
        public DbSet<EmployeeSecurityEntity> EmployeeSecuritySet { get; set; }
        /// <summary>
        /// Master of permission types
        /// </summary>
        public DbSet<PermissionTypeEntity> PermissionTypeSet { get; set; }
        /// <summary>
        /// Avoid EmployeeSet reading, and use FK Ids
        /// </summary>
        public DbSet<EmployeePermissionEntity> EmployeePermissionSet { get; set; }

        /// <summary>
        /// Simulate a documental repository for fast retrieval of security permissions
        /// In real applications this information must be in another database, If this were the only scenario, a documental storage could be a better option
        /// </summary>
        public DbSet<CQRSSEmployeeSecurityEntity> CQRSSEmployeeSecurityEntitySet { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmployeeSecurityEntityConfig());
            modelBuilder.ApplyConfiguration(new PermissionTypeEntityConfig());
            modelBuilder.ApplyConfiguration(new EmployeePermissionEntityConfig());
            modelBuilder.ApplyConfiguration(new CQRSSEmployeeSecuritySimulatedRepoConfig());

            //Order matters
            modelBuilder.Entity<PermissionTypeEntity>().HasData(PermissionTypeEntityConfig.SeedData);
            modelBuilder.Entity<EmployeeSecurityEntity>().HasData(EmployeeSecurityEntityConfig.SeedData);
            modelBuilder.Entity<EmployeePermissionEntity>().HasData(EmployeePermissionEntityConfig.SeedData);
        }
    }
}
