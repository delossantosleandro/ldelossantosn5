using LdelossantosN5.Domain.Impl.DbEntities;
using LdelossantosN5.Domain.Impl.DbEntities.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LdelossantosN5.Domain.Impl.DbEntities.Config
{
    public class PermissionTypeEntityConfig
        : BaseEntityConfig<PermissionTypeEntity>
    {
        public override void Configure(EntityTypeBuilder<PermissionTypeEntity> builder)
        {
            base.Configure(builder);
            builder.ToTable("SEC_PermissionTypes");

            builder.Property(x => x.ShortName)
                .HasMaxLength(64)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(1024)
                .IsRequired();
        }
        public static PermissionTypeEntity[] SeedData => [
            new PermissionTypeEntity() { Id = 1, ShortName = "Create Client", Description = "Enable the creation of new clients", Timestamp = [1,2,3,4,5,6,7,8] },
            new PermissionTypeEntity() { Id = 2, ShortName = "Edit Client", Description = "Enable the edition of clients personal data", Timestamp = [1,2,3,4,5,6,7,8] },
            new PermissionTypeEntity() { Id = 3, ShortName = "Block Client", Description = "Enable to block the client requesting authorization for any financial operation", Timestamp = [1,2,3,4,5,6,7,8] },
            new PermissionTypeEntity() { Id = 4, ShortName = "Mark Client as suspect", Description = "Signal the client as suspicios of fraudulent activities", Timestamp = [1,2,3,4,5,6,7,8] },
        ];
    }
}
