using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AWS_E_Commerce.Models;
using AWS_E_Commerce.Models.Interfaces;
using AWS_E_Commerce.Models.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AWS_E_Commerce.Pages.Cart
{
    public class CompleteOrderModel : PageModel
    {
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrder _order;
        private readonly EmailService _email;

        public CompleteOrderModel(ShoppingCart shoppingCart, IOrder order, EmailService email)
        {
            _shoppingCart = shoppingCart;
            _order = order;
            _email = email;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {

            var items = _shoppingCart.GetShoppingCartItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);

            await _order.StoreOrderAsync(items, userId, userEmailAddress);
            await _shoppingCart.ClearShoppingCartAsync();

            string message = "Order Summary : <br/> ";

            foreach (ShoppingCartItem shopping in items)
            {
                message += $"you bought a  {shopping.product.Name}  for a price   {shopping.product.Price} <br/>";
            }

            await _email.SendEmail(message, "21029318@student.ltuc.com", "Order Summary");
            await _email.SendEmail(message, "sultan.kanaan@yahoo.com", "Order Summary");
            await _email.SendEmail(message, userEmailAddress, "Order Summary");

            return Page();
        }
    }
}
