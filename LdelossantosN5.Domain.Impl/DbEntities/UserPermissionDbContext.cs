using Elastic.Clients.Elasticsearch;
using LdelossantosN5.Domain.Impl.DbEntities.Config;
using LdelossantosN5.Domain.Impl.Repositories;
using LdelossantosN5.Domain.Models;
using LdelossantosN5.Domain.Patterns;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LdelossantosN5.Domain.Impl.DbEntities
{
    public class UserPermissionDbContext
        : DbContext,
        IEmployeePermissionRepository
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
        /// Update or delete the permission entry
        /// </summary>
        /// <param name="permissionId">EmployeePermission Id</param>
        /// <param name="employeeId">Employee Id</param>
        /// <param name="status">PermissionStatus (granted or denied)</param>
        /// <returns>True when update or delete, false if record not found</returns>
        /// <exception cref="ArgumentException">PermissionStatus can't be permissionRequested</exception>
        /// <exception cref="NotFoundException">Raise the error when the record it's not found</exception>
        async Task<int> IEmployeePermissionRepository.UpdateEmployeePermissionAsync(int permissionId, int employeeId, PermissionStatus status)
        {
            if (status == PermissionStatus.permissionRequested) throw new ArgumentException("PermissionStatus cannot be permissionRequested");
            var idParam = new SqlParameter("@Id", permissionId);
            var employeeIdParam = new SqlParameter("@EmployeeId", employeeId);
            var requestStatusParam = new SqlParameter("@RequestStatus", (int)status);
            var returnValueParam = new SqlParameter { ParameterName = "@ReturnValue", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output };

            // Execute the stored procedure
            await this.Database.ExecuteSqlRawAsync(
                "EXEC UpdateOrDeleteEmployeePermission @Id, @EmployeeId, @RequestStatus, @ReturnValue OUTPUT",
                idParam, employeeIdParam, requestStatusParam, returnValueParam
            );

            int result = (int)returnValueParam.Value;
            if (result == 0) throw new NotFoundException(typeof(EmployeePermissionEntity), permissionId);
            return result; // This will return the number of affected rows
        }

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
