using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishCenter.Business.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        IQueryable<T> Entities { get; }

        Task<T> GetById(int id);

        Task<IEnumerable<T>> GetAll();

        Task<T> Add(T entity);

        Task AddRange(List<T> list);

        Task<T> Update(T entity);

        Task<T> Delete(T entity);
    }
}
