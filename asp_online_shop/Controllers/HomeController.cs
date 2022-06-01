using asp_online_shop.Areas.Identity.Data;
using asp_online_shop.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace asp_online_shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepo _repo;
        private readonly SignInManager<User> _signInManager;

        public HomeController(IProductRepo repo,  SignInManager<User> signInManager)
        {
            _repo = repo;
            _signInManager = signInManager;
        }

        public async Task<ActionResult> Index()
        {
            var products = await _repo.GetAll();
            return View(products);
        }

        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }
    }
}
