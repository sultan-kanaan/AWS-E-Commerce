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
    public class CategoryRepository : ICategory
    {
        private AWSDbContext _context;

        public CategoryRepository(AWSDbContext context)
        {
            _context = context;
        }
        public  async Task<ProductCategoryDTO> CreateProductCategory(ProductCategoryDTO productCategory)
        {
            ProductCategory NewProductCategory = new ProductCategory
            {
                Id = productCategory.Id,
                Name = productCategory.Name
            };
            _context.Entry(NewProductCategory).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return productCategory;
        }

        public async Task DeleteProductCategory(int Id)
        {
            ProductCategory productCategory = await _context.Categories.FindAsync(Id);
            _context.Entry(productCategory).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<List<ProductCategoryDTO>> GetProductCategories()
        {
            return await _context.Categories.Select(C => new ProductCategoryDTO
            {
                Id = C.Id,
                Name = C.Name,
                products = C.products.Select(p => new ProductDTO
                {
                    Name = p.Name,
                    ProductImage = p.ProductImage
                    
                    

                }).ToList()

            }).OrderBy(p => p.Name).ToListAsync();
            }

        public async Task<ProductCategoryDTO> GetProductCategory(int Id)
        {
            return await _context.Categories.Select(C => new ProductCategoryDTO
            {
                Id = C.Id,
                Name = C.Name,
                products = C.products.Select(p => new ProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    color = p.color,
                    size = p.size,
                    ProductImage = p.ProductImage


                }).ToList()

            }).FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<ProductCategoryDTO> UpdateProductCategory(int Id, ProductCategoryDTO productCategory)
        {
            ProductCategory UpdateProductCategory = new ProductCategory
            {
                Id = productCategory.Id,
                Name = productCategory.Name
            };
            _context.Entry(UpdateProductCategory).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return productCategory;
        }

    }
}
