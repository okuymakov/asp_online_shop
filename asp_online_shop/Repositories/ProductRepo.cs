using asp_online_shop.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace asp_online_shop.Repositories
{
    public class ProductRepo : IProductRepo
    {

        private readonly AppDbContext _db;
        public ProductRepo(AppDbContext db)
        {
            _db = db;
        }

        public async Task Create(Product product)
        {
            _db.Add(product);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product != null)
            {
                _db.Products.Remove(product);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<Product> Get(int id)
        {
            return await _db.Products.FirstOrDefaultAsync(p => p.Id == id); 
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _db.Products.ToListAsync();
        }

        public async Task Update(Product product)
        {
            _db.Products.Update(product);
            await _db.SaveChangesAsync();
        }
    }
}
