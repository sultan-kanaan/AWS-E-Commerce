using AWS_E_Commerce.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWS_E_Commerce.Models.Services
{
    public class CartItemRepository
    {
        public string ShoppingCartId { get; set; }

        private AWSDbContext _context;
        public const string CartSessionKey = "CartId";

        public CartItemRepository(AWSDbContext context)
        {
            _context = context;
        }
        public void AddToCart(int id)
        {
            // Retrieve the product from the database.           
           // ShoppingCartId = GetCartId();

            var cartItem = _context.ShoppingCartItems.SingleOrDefault( c => c.CartId == ShoppingCartId && c.ProductId == id);
            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists.                 
                cartItem = new CartItem
                {
                    ItemId = Guid.NewGuid().ToString(),
                    ProductId = id,
                    CartId = ShoppingCartId,
                    Product = _context.Products.SingleOrDefault(p => p.Id == id),
                    Quantity = 1,
                    DateCreated = DateTime.Now
                };
                _context.ShoppingCartItems.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart,                  
                // then add one to the quantity.                 
                cartItem.Quantity++;
            }
            _context.SaveChanges();
        }
       
       
    }
}

