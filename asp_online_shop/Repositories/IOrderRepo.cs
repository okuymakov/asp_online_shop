using asp_online_shop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace asp_online_shop.Repositories
{
    public interface IOrderRepo : ICRUDRepo<Order>
    {
        public Task<IEnumerable<Order>> GetByUserId(string userId);
    }
}
