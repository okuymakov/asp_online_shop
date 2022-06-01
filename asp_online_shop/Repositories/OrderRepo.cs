using asp_online_shop.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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
            foreach (var orderItem in order.OrderItems)
            {            
                orderItem.ProductId = orderItem.Product.Id;
                var product = orderItem.Product;
                product.Count -= orderItem.Quanity;
                _db.Products.Update(product);
                orderItem.Product = null;              
            }
            _db.Orders.Add(order);
            
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var order = await _db.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Product).FirstOrDefaultAsync(o => o.Id == id);
            if (order != null)
            {
                foreach (var orderItem in order.OrderItems)
                {             
                    var product = orderItem.Product;
                    product.Count += orderItem.Quanity;
                    _db.Products.Update(product);
                }
                _db.Orders.Remove(order);             
                await _db.SaveChangesAsync();               
            }
        }

        public async Task<Order> Get(int id)
        {
            return await _db.Orders.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Order>> GetByUserId(string userId)
        {
            return await _db.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Product).Where(o => o.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _db.Orders.Include(o => o.OrderItems).ThenInclude(oi => oi.Product).ToListAsync();
        }

        public async Task Update(Order order)
        {
            _db.Orders.Update(order);
            await _db.SaveChangesAsync();
        }
        
    }
}

                