using Data.Context;
using Data.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class EfCoreRepository<T> : IRepository<T> where T : BaseEntity
    {
        #region Fields
        private readonly AppDbContext _dbContext;
        #endregion

        #region Ctor
        public EfCoreRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Methods
        public async Task<int> DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);

            return await _dbContext.SaveChangesAsync();
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await Table
                .Where(e => !e.Deleted)?
                .ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await Table
                .FirstOrDefaultAsync(e => e.Id == id
                && !e.Deleted);
        }

        public async Task<int> InsertAsync(T entity)
        {
            _dbContext.Add(entity);

            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(T entity)
        {
            _dbContext.Update(entity);

            return await _dbContext.SaveChangesAsync();
        }
        #endregion

        #region Properties
        public IQueryable<T> Table => _dbContext.Set<T>().AsNoTracking();
        #endregion
    }
}
