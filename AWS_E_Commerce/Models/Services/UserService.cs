using AWS_E_Commerce.Auth.Models;
using AWS_E_Commerce.Data;
using AWS_E_Commerce.Models.DTOs;
using AWS_E_Commerce.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AWS_E_Commerce.Models.Services
{
    public class UserService : IUserService
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> SignInMngr)
        {
            _userManager = userManager;
            _signInManager = SignInMngr;
        }
        public async Task<UserDTO> Register(RegisterUserDTO data, ModelStateDictionary modelState)
        {
            var user = new ApplicationUser
            {
                UserName = data.Username,
                Email = data.Email,
            };

            var result = await _userManager.CreateAsync(user, data.Password);

            if (result.Succeeded)
            {

                // Administrator
                // Editor
                // Customer
                await _userManager.AddToRoleAsync(user, "Customer");
                return new UserDTO
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Roles = await _userManager.GetRolesAsync(user)

                };
            }
            foreach (var error in result.Errors)
            {
                var errorKey =
                    error.Code.Contains("Password") ? nameof(data.Password) :
                    error.Code.Contains("Email") ? nameof(data.Email) :
                    error.Code.Contains("UserName") ? nameof(data.Username) :
                    "";
                modelState.AddModelError(errorKey, error.Description);
            }
            return null;
        }
        public async Task<UserDTO> Authenticate(string username, string password)
        { 
            var result = await _signInManager.PasswordSignInAsync(username, password, true, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(username);

                return new UserDTO
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Roles = await _userManager.GetRolesAsync(user)
                };
            }

            return null;
        }
        public async Task<UserDTO> GetUser(ClaimsPrincipal principal)
        {
            var user = await _userManager.GetUserAsync(principal);
            return new UserDTO
            {
                Id = user.Id,
                Username = user.UserName
            };
        }
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
