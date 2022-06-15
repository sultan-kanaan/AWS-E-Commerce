using AWS_E_Commerce.Models.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AWS_E_Commerce.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; }

        public Product product { get; set; }

        public int productId { get; set; }

        public int Amount { get; set; }


        public string ShoppingCartId { get; set; }
    }
}
