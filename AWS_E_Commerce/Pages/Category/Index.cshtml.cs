using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AWS_E_Commerce.Data;
using AWS_E_Commerce.Models.DTOs;
using AWS_E_Commerce.Models.Interfaces;
using AWS_E_Commerce.Models;

namespace AWS_E_Commerce.Pages.Category
{
    public class IndexModel : PageModel
    {
        private readonly ICategory _category;

        public IndexModel(ICategory category)
        {
            _category = category;
        }

        public List<ProductCategoryDTO> Category { get;set; }

        public async Task OnGetAsync()
        {
            Category = await _category.GetProductCategories();
        }
    }
}
