using EnglisCenter.Accessor;
using EnglishCenter.Business.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishCenter.Business.Services
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly EnglishForStudentDB _dbContext;

        public BaseRepository(EnglishForStudentDB dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> Entities => _dbContext.Set<T>();

        public async Task<T> Add(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task AddRange(List<T> list)
        {
            _dbContext.Set<T>().AddRange(list);
            await _dbContext.SaveChangesAsync();           
        }

        public async Task<T> Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _dbContext.Set<T>()
                .FindAsync(id);
        }

        public async Task<T> Update(T entity)
        {
            _dbContext.Entry(entity).CurrentValues.SetValues(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}