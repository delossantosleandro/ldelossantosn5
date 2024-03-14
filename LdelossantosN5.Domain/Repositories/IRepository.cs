using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdelossantosN5.Domain.Patterns
{
    //IDBSet already support this and more
    public interface IRepository<TEntity>
        where TEntity : class, new()
    {
        /// <summary>
        /// Search the entity by Id
        /// </summary>
        /// <param name="id">The Unique identifier of the entity</param>
        /// <returns>The entity</returns>
        /// <exception cref="ArgumentException">Throws exception if Id doesn't exist</exception>
        Task<TEntity> FindAsync(int id);
        /// <summary>
        /// Add new record to the entity
        /// </summary>
        /// <param name="entity">Entity to insert</param>
        void Add(TEntity entity);
        /// <summary>
        /// Remove the record fromt the entity
        /// </summary>
        /// <param name="entity">The record to remove</param>
        void Remove(TEntity entity);
    }
}