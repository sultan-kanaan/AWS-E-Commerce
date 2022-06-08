using AWS_E_Commerce.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWS_E_Commerce.Models.Interfaces
{
    public interface IProduct
    {
        Task<ProductDTO> CreateProduct(Product product);
        Task<ProductDTO> GetProduct(int Id);
        Task<List<ProductDTO>> GetProducts();
        Task<ProductDTO> UpdateProduct(int Id, Product product);
        Task DeleteProduct(int Id);

    }
}
