using LdelossantosN5.Domain.Patterns;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdelossantosN5.DAL.Patterns
{
    public class EfRepositoryBase<TEntity>
        : IRepository<TEntity>
        where TEntity : DbEntity, new()
    {
        protected DbSet<TEntity> DbSet { get; set; }
        public EfRepositoryBase(UserPermissionDbContext ctx)
        {
            this.DbSet = ctx.Set<TEntity>();
        }
        public void Add(TEntity entity)
        {
            this.DbSet.Add(entity);
        }
        public async Task<TEntity> FindAsync(int id)
        {
            return await this.DbSet.FindAsync(id)
                ?? throw new NotFoundException(typeof(TEntity), id);
        }
        public void Remove(TEntity entity)
        {
            this.DbSet.Remove(entity);
        }
    }
}