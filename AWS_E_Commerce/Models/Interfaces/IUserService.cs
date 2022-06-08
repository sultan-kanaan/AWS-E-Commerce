using AWS_E_Commerce.Models.DTOs;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AWS_E_Commerce.Models.Interfaces
{
    public interface IUserService
    {
        public Task<UserDTO> Register(RegisterUserDTO data, ModelStateDictionary modelState);
      
        public Task<UserDTO> Authenticate(string username, string password);

        public Task<UserDTO> GetUser(ClaimsPrincipal user);
    }
}
