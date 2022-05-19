using asp_online_shop.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace asp_online_shop.Repositories
{
    public class OrderRepo : IOrderRepo
    {

        private readonly AppDbContext _db;
        public OrderRepo(AppDbContext db)
        {
            _db = db;
        }

        public async Task Create(Order order)
        {
            _db.Add(order);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var order = await _db.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (order != null)
            {
                _db.Orders.Remove(order);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<Order> Get(int id)
        {
            return await _db.Orders.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _db.Orders.ToListAsync();
        }

        public async Task Update(Order order)
        {
            _db.Orders.Update(order);
            await _db.SaveChangesAsync();
        } 
        
    }
}

                