using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AWS_E_Commerce.Models;
using AWS_E_Commerce.Models.DTOs;
using AWS_E_Commerce.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AWS_E_Commerce.Pages.Cart
{
    public class IndexModel : PageModel
    {
        private readonly IProduct _product;
        private readonly IOrder _order;


        public IndexModel(IProduct product, IOrder order)
        {
            _product = product;
            _order = order;
        }

        public List<ProductDTO> products;
        public List<Order> Orders;
        public async Task<IActionResult>  OnGet()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);

           await _order.GetOrdersByUserIdAndRoleAsync(userId, userRole);
            return Page();
        }
        
    }
}
