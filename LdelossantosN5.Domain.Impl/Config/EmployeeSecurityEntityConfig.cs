using LdelossantosN5.Domain.Impl.DbEntities;
using LdelossantosN5.Domain.Impl.DbEntities.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LdelossantosN5.Domain.Impl.DbEntities.Config
{
    public class EmployeeSecurityEntityConfig
        : BaseEntityConfig<EmployeeSecurityEntity>
    {
        public override void Configure(EntityTypeBuilder<EmployeeSecurityEntity> builder)
        {
            base.Configure(builder);
            builder.ToTable("SEC_Employees");

            builder
                .Property(x => x.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder
                .Property(x => x.StartDate)
                .IsRequired();
        }

        public static EmployeeSecurityEntity[] SeedData => [
            new EmployeeSecurityEntity()
            {
                Id = 1,
                Name = "Leandro",
                StartDate = DateTime.Now.Date.AddYears(-5),
                Timestamp = [1,2,3,4,5,6,7,8],
            },
            new EmployeeSecurityEntity()
            {
                Id = 2,
                Name = "Mariela",
                StartDate = DateTime.Now.Date.AddYears(-4),
                Timestamp = [1,2,3,4,5,6,7,8]
            },
            new EmployeeSecurityEntity()
            {
                Id = 3,
                Name = "Alberto",
                StartDate = DateTime.Now.Date.AddYears(-3),
                Timestamp = [1,2,3,4,5,6,7,8]
            },
            new EmployeeSecurityEntity()
            {
                Id = 4,
                Name = "Isabel",
                StartDate = DateTime.Now.Date.AddYears(-2),
                Timestamp = [1,2,3,4,5,6,7,8]
            }
        ];
    }
}
