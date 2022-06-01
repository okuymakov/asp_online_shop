using asp_online_shop.Areas.Identity.Data;
using asp_online_shop.Models;
using asp_online_shop.Repositories;
using asp_online_shop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace asp_online_shop.Controllers
{

   [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IOrderRepo _orderRepo;


        public ProfileController(UserManager<User> userManager, IOrderRepo orderRepo)
        {
            _userManager = userManager;
            _orderRepo = orderRepo;
        }

        public async Task<ActionResult> OrdersAsync()
        {       
            var orders = await _orderRepo.GetByUserId(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View(orders ?? new List<Order>());
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
                var user = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
                user.Firstname = model.Firstname;
                user.Surname = model.Surname;
                user.Patronymic = model.Patronymic;
                user.Dob = model.Dob;
                user.Gender = model.Gender;
                user.Address = model.Address;
                user.Email = model.Email;
                user.Phone = model.Phone;            
                await _userManager.UpdateAsync(user);

                var claims = new List<Claim>
                {
                    new Claim("Firstname", user.Firstname),
                    new Claim("Surname", user.Surname),
                    new Claim("Patronymic", user.Patronymic ?? ""),
                    new Claim("Phone", user.Phone),
                    new Claim("Address", user.Address ?? ""),
                };
                await _userManager.RemoveClaimsAsync(user, await _userManager.GetClaimsAsync(user));
                await _userManager.AddClaimsAsync(user, claims);
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
