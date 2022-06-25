using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AWS_E_Commerce.Models;
using AWS_E_Commerce.Models.DTOs;
using AWS_E_Commerce.Models.Interfaces;
using AWS_E_Commerce.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AWS_E_Commerce.Pages.Cart
{
    public class ShoppingCartModel : PageModel
    {
        private readonly IProduct _product;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrder _order;
        private readonly EmailService _email;

        public ShoppingCartModel(IProduct product, ShoppingCart shoppingCart, IOrder order, EmailService email)
        {
            _product = product;
            _shoppingCart = shoppingCart;
            _order = order;
            _email = email;
        }
        [BindProperty]
        public ShoppingCart shoppingCarts { get; set; }
        [BindProperty]
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
        [BindProperty]
        public ShoppingCartDTO shoppingCartsdto { get; set; }
        public IActionResult OnGet()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var response = new ShoppingCartDTO()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return Page();
        }
       
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
    }
}
