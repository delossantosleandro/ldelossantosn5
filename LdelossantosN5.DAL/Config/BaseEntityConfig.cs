using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LdelossantosN5.DAL.Config
{
    public class BaseEntityConfig<TEntity>
        : IEntityTypeConfiguration<TEntity>
        where TEntity : DbEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Timestamp).IsRowVersion();
        }
    }
}