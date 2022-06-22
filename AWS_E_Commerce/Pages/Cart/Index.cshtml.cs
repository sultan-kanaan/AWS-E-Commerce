using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AWS_E_Commerce.Models.DTOs;
using AWS_E_Commerce.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AWS_E_Commerce.Pages.Cart
{
    public class IndexModel : PageModel
    {
        private readonly IProduct _product;

        public IndexModel(IProduct product)
        {
            _product = product;
        }

        public List<ProductDTO> products;
        public void OnGet()
        {
        }
        
    }
}
