using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AWS_E_Commerce.Auth.Models;
using AWS_E_Commerce.Models.DTOs;
using AWS_E_Commerce.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AWS_E_Commerce.Pages.Account
{
    public class IndexModel : PageModel
    {
        private IUserService _userService;
        public IndexModel(IUserService UserService)
        {
            _userService = UserService;
        }
       

        public void OnGet()
        {

        }
        [BindProperty]
        public LoginDataDTO login { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userService.Authenticate(login.Username, login.Password);

            if (user != null)
            {
                return RedirectToAction("Index","Category");
            }
            ModelState.AddModelError(nameof(login.Password), "Email or Password was incorrect.");
            return Page();
        }
    }
}
