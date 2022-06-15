using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AWS_E_Commerce.Data;
using AWS_E_Commerce.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AWS_E_Commerce.Controllers
{
    public class AccountController : Controller
    {
        private AWSDbContext _context;

       
        private IUserService _userService;
        public AccountController(IUserService UserService, AWSDbContext context)
        {
            _userService = UserService;
            _context = context;

        }
        public IActionResult Login()
        {
            return RedirectToAction("Login", "Users");
        }
        public IActionResult Register()
        { 
            return RedirectToAction("SignUp", "Users");
        }
        public async Task<IActionResult> Logout()
        {
            await _userService.Logout();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }


    }
}
