using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dars_5ososy_API.Infrastructure.Repositories
{
    public interface IGenericRepository<TResult>
    {
        public Task<List<TResult>> GetAllAsync();
        public Task<TResult?> GetByIdAsync(long id);
        public Task<TResult> CreateAsync(TResult entity);
        public Task<TResult> UpdateAsync(TResult entity);
        public Task<bool> DeleteAsync(long id);
    }
}