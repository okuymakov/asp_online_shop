using asp_online_shop.Models;
using asp_online_shop.Repositories;
using asp_online_shop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace asp_online_shop.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderRepo _repo;

        public OrderController(IOrderRepo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(OrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var cart = HttpContext.Session.GetObjectFromJson<Cart>("cart");

                List<OrderItem> orderItems = cart.Items;

                var order = new Order
                {
                    OrderDate = System.DateTime.Now,
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    OrderStatus = "Подтвержден",
                    ShippedDate = null,
                    OrderItems = orderItems,
                    Address = model.Address,
                    DeliveryPrice = 0,
                    DeliveryMethod = model.DeliveryMethod,
                    PaymentMethod = model.PaymentMethod,
                };
                await _repo.Create(order);

                cart.Clear();
                HttpContext.Session.SetObjectAsJson("cart", cart);
                return RedirectToAction("index", "cart");
            }
            return View(model);
        }

        [HttpDelete]
        public async Task<JsonResult> Cancel([FromBody] int id)
        {
            await _repo.Delete(id);
            return Json(true);
        }

        [HttpPost]
        public async Task<JsonResult> GetAll()
        {
            return Json(await _repo.GetAll());
        }
    }

}
