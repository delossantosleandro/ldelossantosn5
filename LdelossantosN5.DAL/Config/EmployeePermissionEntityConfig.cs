using LdelossantosN5.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdelossantosN5.DAL.Config
{
    public class EmployeePermissionEntityConfig
        : BaseEntityConfig<EmployeePermissionEntity>
    {
        public override void Configure(EntityTypeBuilder<EmployeePermissionEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable("SEC_EmployeePermissions");

            builder
                .Property(x => x.RequestStatus)
                .IsRequired();

            builder
                .HasOne(x => x.Employee)
                .WithMany(y => y.Permissions)
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade)
                .IsRequired();

            builder
                .HasOne(x => x.PermissionType)
                .WithMany()
                .HasForeignKey(x => x.PermissionTypeId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict)
                .IsRequired();

            //We can't have more than one employee permissionType for the employee
            builder
                .HasAlternateKey(x => new { x.EmployeeId, x.PermissionTypeId });
        }

        public static EmployeePermissionEntity[] SeedData => [
            //employee 1
            new EmployeePermissionEntity() { Id = 1, EmployeeId = 1, PermissionTypeId = 1, RequestStatus = PermissionStatus.permissionRequested, Timestamp = [1, 2, 3, 4, 5, 6, 7, 8] },
            new EmployeePermissionEntity() { Id = 2, EmployeeId = 1, PermissionTypeId = 2, RequestStatus = PermissionStatus.permissionDenied, Timestamp = [1, 2, 3, 4, 5, 6, 7, 8] },
            new EmployeePermissionEntity() { Id = 3, EmployeeId = 1, PermissionTypeId = 3, RequestStatus = PermissionStatus.permissionGranted, Timestamp = [1, 2, 3, 4, 5, 6, 7, 8] },

            //employee 2
            new EmployeePermissionEntity() { Id = 4, EmployeeId = 2, PermissionTypeId = 2, RequestStatus = PermissionStatus.permissionRequested, Timestamp = [1, 2, 3, 4, 5, 6, 7, 8] },
            new EmployeePermissionEntity() { Id = 5, EmployeeId = 3, PermissionTypeId = 3, RequestStatus = PermissionStatus.permissionRequested, Timestamp = [1, 2, 3, 4, 5, 6, 7, 8] },
            new EmployeePermissionEntity() { Id = 6, EmployeeId = 4, PermissionTypeId = 4, RequestStatus = PermissionStatus.permissionGranted, Timestamp = [1, 2, 3, 4, 5, 6, 7, 8] },

        ];
    }
}