using AWS_E_Commerce.Data;
using AWS_E_Commerce.Models.DTOs;
using AWS_E_Commerce.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWS_E_Commerce.Models.Services
{
    public class ProductRepository : IProduct
    {
        private AWSDbContext _context;

        public ProductRepository(AWSDbContext context)
        {
            _context = context;
        }
        public async Task<ProductDTO> CreateProduct(Product product)
        {
            ProductDTO Newproduct = new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                color = product.color,
                Price = product.Price,
                size = product.size,
                ProductCategoryName = _context.Categories.FirstOrDefault(c => c.Id == product.ProductCategoryId).Name
            };
            _context.Entry(product).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return Newproduct;
        }

        public  async Task<ProductDTO> GetProduct(int Id)
        {
            return await _context.Products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                color = p.color,
                Price = p.Price,
                size = p.size,
                ProductCategoryName = _context.Categories.FirstOrDefault(c => c.Id == p.ProductCategoryId).Name

                // CategoryName = _context.Categories.FirstOrDefault(c => c.Id == p.CategoryId).Name

            }).FirstOrDefaultAsync(x => x.Id == Id);
        
        }

        public async Task<List<ProductDTO>> GetProducts()
        {
            return await _context.Products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                color = p.color,
                Price = p.Price,
                size = p.size,
                ProductCategoryName = _context.Categories.FirstOrDefault(c => c.Id == p.ProductCategoryId).Name

            }).ToListAsync();
        }
    

        public async Task DeleteProduct(int Id)
        {
            Product product = await _context.Products.FindAsync(Id);
            _context.Entry(product).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<ProductDTO> UpdateProduct(int Id, Product product)
        {
            ProductDTO updateproduct = new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                color = product.color,
                Price = product.Price,
                size = product.size,
                ProductCategoryName = _context.Categories.FirstOrDefault(c => c.Id == product.ProductCategoryId).Name
            };
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return updateproduct;
        }
        

    }
}
