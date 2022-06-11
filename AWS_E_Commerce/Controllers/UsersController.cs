using AWS_E_Commerce.Models.DTOs;
using AWS_E_Commerce.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWS_E_Commerce.Controllers
{
    public class UsersController : Controller

    {
        private IUserService _userService;
        public UsersController(IUserService UserService)
        {
            _userService = UserService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> SignUp(RegisterUserDTO register)
        {
            var user = await _userService.Register(register, this.ModelState);
            if (ModelState.IsValid)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult<UserDTO>> Login(LoginDataDTO data)
        {
            var user = await _userService.Authenticate(data.Username, data.Password);

            if (user != null)
            {
                return RedirectToAction("Index", "Products");
            }
            ModelState.AddModelError(nameof(data.Password), "Email or Password was incorrect.");
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await _userService.Logout();
            return RedirectToAction("Index", "Home");
        }
      
    }
}
