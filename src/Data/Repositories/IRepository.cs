using Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    /// <summary>
    /// Represents an entity repository
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    public partial interface IRepository<T> where T : BaseEntity
    {
        #region Methods
        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns>Entity</returns>
        Task<IList<T>> GetAllAsync();

        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        Task<int> InsertAsync(T entity);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        Task<int> UpdateAsync(T entity);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        Task<int> DeleteAsync(T entity);
        #endregion

        #region Properties
        /// <summary>
        /// Gets a table
        /// </summary>
        IQueryable<T> Table { get; }
        #endregion
    }
}
