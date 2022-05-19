using asp_online_shop.Models;
using asp_online_shop.Repositories;
using asp_online_shop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace asp_online_shop.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private IOrderRepo _repo;

        public OrderController(IOrderRepo repo)
        {
            _repo = repo;
        }

        public ActionResult Index()
        {
            return Json(5);
        }
        public async Task<JsonResult> Create(OrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var busket = HttpContext.Session.GetObjectFromJson<Busket>("busket");

                List<OrderItem> orderItems = busket.Items;

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
                return Json(true);
            }
            return Json(false);
        }

        public async Task<JsonResult> GetAll()
        {
            return Json(await _repo.GetAll());
        }
    }

}
