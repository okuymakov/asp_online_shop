using asp_online_shop.Models;
using asp_online_shop.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace asp_online_shop.Controllers
{
    public class HomeController : Controller
    {
        private IProductRepo _repo;

        public HomeController(IProductRepo repo)
        {
            _repo = repo;
        }

        public async Task<ActionResult> Index()
        {
            var products = await _repo.GetAll();
            return View(products);
        }


        public ActionResult Cart()
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            var busket = HttpContext.Session.GetObjectFromJson<Busket>("busket") ?? new Busket();
            return View(busket.Items);
        }

        public async  Task AddToCart(int productId)
        {
            var product = await _repo.Get(productId);
            if(product.Count > 0)
            {
                var busket = HttpContext.Session.GetObjectFromJson<Busket>("busket");
                if(busket == null)
                {
                    busket = new Busket();
                    HttpContext.Session.SetObjectAsJson("busket", busket);
                }
                busket.AddProduct(product);
                HttpContext.Session.SetObjectAsJson("busket", busket);
            }
        }
    }
}
