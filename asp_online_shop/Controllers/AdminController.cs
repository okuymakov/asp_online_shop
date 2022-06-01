using asp_online_shop.Models;
using asp_online_shop.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace asp_online_shop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IProductRepo _productsRepo;
        private readonly IOrderRepo _ordersRepo;

        public AdminController(IProductRepo products, IOrderRepo orders)
        {
            _productsRepo = products;
            _ordersRepo = orders;
        }

        public async Task<IActionResult> ProductsAsync()
        {
            var products = await _productsRepo.GetAll();
            return View(products);
        }

        public ActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddProductAsync(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productsRepo.Create(product);
                return RedirectToAction("Products");
            }
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> ProductsAsync(Product product)
        { 
            var products = await _productsRepo.GetAll();
            return View(products);
        }

        [HttpPut]
        public async Task UpdateProduct([FromQuery(Name = "id")] int id, [FromQuery(Name = "count")] int count)
        {
            var product = await _productsRepo.Get(id);
            product.Count = count;
            await _productsRepo.Update(product);
        }

        public async Task<IActionResult> OrdersAsync()
        {
            var orders = await _ordersRepo.GetAll();
            return View(orders);
        }
        //change products count  // addproducts //get orders


    }
}
