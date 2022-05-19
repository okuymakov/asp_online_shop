
using System.Collections.Generic;
using System.Threading.Tasks;

namespace asp_online_shop.Repositories
{
    public interface ICRUDRepo<T>
    {
        public Task Create(T el);
        public Task Update(T el);
        public Task Delete(int id);
        public Task<T> Get(int id);
        public Task<IEnumerable<T>> GetAll();
    }
}
