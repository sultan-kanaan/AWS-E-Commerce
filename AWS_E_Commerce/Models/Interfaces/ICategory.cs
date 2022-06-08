using AWS_E_Commerce.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWS_E_Commerce.Models.Interfaces
{
    public interface ICategory
    {
        Task<ProductCategoryDTO> CreateProductCategory(ProductCategoryDTO productCategory);
        Task<ProductCategoryDTO> GetProductCategory(int Id);
        Task<List<ProductCategoryDTO>> GetProductCategories();
        Task<ProductCategoryDTO> UpdateProductCategory(int Id, ProductCategoryDTO productCategory);
        Task DeleteProductCategory(int Id);
    }
}
