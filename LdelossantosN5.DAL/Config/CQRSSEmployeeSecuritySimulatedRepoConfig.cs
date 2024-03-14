using LdelossantosN5.Domain.Impl.DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdelossantosN5.Domain.Impl.DbEntities.Config
{
    public class CQRSSEmployeeSecuritySimulatedRepoConfig
        : IEntityTypeConfiguration<CQRSSEmployeeSecurityEntity>
    {
        public void Configure(EntityTypeBuilder<CQRSSEmployeeSecurityEntity> builder)
        {
            builder
                .HasKey(x => x.EmployeeId);

            builder
                .Property(x => x.EmployeeId)
                .ValueGeneratedNever()
                .IsRequired();

            builder
                .Property(x => x.Data)
                .HasColumnType("nvarchar(max)");
        }
    }
}
