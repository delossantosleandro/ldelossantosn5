using LdelossantosN5.DAL.Config;
using Microsoft.EntityFrameworkCore;

namespace LdelossantosN5.DAL
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmployeeSecurityEntityConfig());
            modelBuilder.ApplyConfiguration(new PermissionTypeEntityConfig());
            modelBuilder.ApplyConfiguration(new EmployeePermissionEntityConfig());

            //Order matters
            modelBuilder.Entity<PermissionTypeEntity>().HasData(PermissionTypeEntityConfig.SeedData);
            modelBuilder.Entity<EmployeeSecurityEntity>().HasData(EmployeeSecurityEntityConfig.SeedData);
            modelBuilder.Entity<EmployeePermissionEntity>().HasData(EmployeePermissionEntityConfig.SeedData);
        }
    }
}
