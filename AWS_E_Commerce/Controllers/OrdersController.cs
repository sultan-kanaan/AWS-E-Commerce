using AWS_E_Commerce.Auth.Models;
using AWS_E_Commerce.Models;
using AWS_E_Commerce.Models.DTOs;
using AWS_E_Commerce.Models.Interfaces;
using AWS_E_Commerce.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AWS_E_Commerce.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IProduct _product;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrder _order;
        private readonly EmailService _email;



        public OrdersController(IProduct product, ShoppingCart shoppingCart, IOrder order, EmailService email)
        {
            _product = product;
            _shoppingCart = shoppingCart;
            _order = order;
            _email = email;
        }

        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);

            var orders = await _order.GetOrdersByUserIdAndRoleAsync(userId, userRole);
            return View(orders);
        }

        public IActionResult ShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var response = new ShoppingCartDTO()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(response);
        }
        [Authorize(Policy = "Customer")]

        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            var item = await _product.GetProduct(id);

            if (item != null)
            {

                _shoppingCart.AddItemToCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await _product.GetProduct(id);

            if (item != null)
            {
                _shoppingCart.RemoveItemFromCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> CompleteOrder()
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

            return View();
        }
    }
}
