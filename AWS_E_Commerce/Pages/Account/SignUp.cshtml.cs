using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AWS_E_Commerce.Models.DTOs;
using AWS_E_Commerce.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AWS_E_Commerce.Pages.Account
{
    public class SignUpModel : PageModel
    {
        private IUserService _userService;

        public SignUpModel(IUserService UserService)
        {
            _userService = UserService;

        }
        [BindProperty]
        public RegisterUserDTO register { get; set; }

        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostAsync()
        {
            await _userService.Register(register, this.ModelState);
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "Account");
            }
            return Page();
        }
    }
}
