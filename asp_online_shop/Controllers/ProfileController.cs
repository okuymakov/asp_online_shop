using asp_online_shop.Areas.Identity.Data;
using asp_online_shop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace asp_online_shop.Controllers
{

   [Authorize]
    public class ProfileController : Controller
    {
        private UserManager<User> _userManager;

        
        public ProfileController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ActionResult> OrdersAsync()
        {       
            var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View(user.Orders);
        }

        [HttpGet]
        public async Task<ActionResult> Edit()
        {
            var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var model = new CustomerEditViewModel
            {
                Firstname = user.Firstname,
                Surname = user.Surname,
                Patronymic = user.Patronymic,
                Dob = user.Dob,
                Gender = user.Gender,
                Address = user.Address,
                Email = user.Email,
                Phone = user.Phone,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(CustomerEditViewModel model)
        {
            if(ModelState.IsValid)
            {  
                var user = new User
                {
                    Id = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    Firstname = model.Firstname,
                    Surname = model.Surname,
                    Patronymic = model.Patronymic,
                    Dob = model.Dob,
                    Gender = model.Gender,
                    Address = model.Address,
                    Email = model.Email,
                    Phone = model.Phone,
                };
                await _userManager.UpdateAsync(user);               
            }
            return View(model);
        }

        
        public async Task<ActionResult> Delete()
        {
            var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _userManager.DeleteAsync(user);
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }
    }
}
