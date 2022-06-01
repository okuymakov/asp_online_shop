using asp_online_shop.Models;
using asp_online_shop.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace asp_online_shop.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepo _repo;

        public CartController(IProductRepo repo)
        {
            _repo = repo;
        }
        public ActionResult Index()
        {
            var cart = HttpContext.Session.GetObjectFromJson<Cart>("cart") ?? new Cart();
            return View(cart.Items);
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] int productId)
        {
            var product = await _repo.Get(productId);

            var cart = HttpContext.Session.GetObjectFromJson<Cart>("cart") ?? new Cart();
            var cartCount = cart.Items.FirstOrDefault(item => item.Product.Id == product.Id)?.Quanity ?? 0;

            if (product.Count > cartCount)
            {
                cart.AddProduct(product);
                HttpContext.Session.SetObjectAsJson("cart", cart);
                return Json(true);
            }
            return Json(false);

        }

        [HttpPost]
        public ActionResult Decrease([FromBody] int productId)
        {
            var cart = HttpContext.Session.GetObjectFromJson<Cart>("cart") ?? new Cart();
            bool res = cart.DecreaseProductCount(productId);
            if (res)
            {
                HttpContext.Session.SetObjectAsJson("cart", cart);
            }
            return Json(res);
        }

        [HttpPost]
        public ActionResult Remove([FromBody] int productId)
        {
            var cart = HttpContext.Session.GetObjectFromJson<Cart>("cart") ?? new Cart();
            bool res = cart.RemoveProduct(productId);
            if (res)
            {
                HttpContext.Session.SetObjectAsJson("cart", cart);
            }
            return Json(res);
        }

        [HttpPost]
        public void Clear()
        {
            var cart = HttpContext.Session.GetObjectFromJson<Cart>("cart") ?? new Cart();
            cart.Clear();
            HttpContext.Session.SetObjectAsJson("cart", cart);           
        }
    }
}
