using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AWS_E_Commerce.Data;
using AWS_E_Commerce.Models;
using AWS_E_Commerce.Models.Interfaces;
using AWS_E_Commerce.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace AWS_E_Commerce.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProduct _product;
        private readonly AWSDbContext _context;
        private readonly IConfiguration _Configuration;


        public ProductsController(IProduct product, AWSDbContext context, IConfiguration config)
        {
            _context = context;
            _product = product;
            _Configuration = config;

        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            List<ProductDTO> products = await _product.GetProducts();

            return View(products);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int id)
        {
            ProductDTO products = await _product.GetProduct(id);
            return View(products);
        }

        // GET: Products/Create
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create()
        {
            //if (!User.IsInRole("Administrator"))
            //{ 
            //    return RedirectToAction("Login", "Users");
            //}
            var product = new ProductDTO
            {
             ProductCategory = await _context.Categories.OrderBy(p => p.Name).ToListAsync()
            };
            return View(product);
        }

        // POST: Products/Create
        [HttpPost]
        public async Task<IActionResult> Create(Product product, IFormFile file)
        {
            BlobContainerClient container = new BlobContainerClient(_Configuration.GetConnectionString("AzureBlob"), "attac");
            await container.CreateIfNotExistsAsync();
            BlobClient blob = container.GetBlobClient(file.FileName);
            using var stream = file.OpenReadStream();

            BlobUploadOptions options = new BlobUploadOptions()
            {
                HttpHeaders = new BlobHttpHeaders() { ContentType = file.ContentType }
            };


            if (!blob.Exists())
            {
                await blob.UploadAsync(stream, options);
            }

            product.ProductImage = blob.Uri.ToString();
            if (ModelState.IsValid)
            {
                await _product.CreateProduct(product);
                return RedirectToAction("Index");
            }
            stream.Close();
            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize(Policy = "Editor")]
        public async Task<IActionResult> Edit(int id)
        {
            var editproduct = await _product.GetProduct(id);
            var product = new ProductDTO
            {
                Id = editproduct.Id,
                Name = editproduct.Name,
                color = editproduct.color,
                Price = editproduct.Price,
                size = editproduct.size,
                ProductImage = editproduct.ProductImage,
                ProductCategoryId = editproduct.ProductCategoryId,
                ProductCategory = await _context.Categories.OrderBy(p =>p.Name).ToListAsync()
            };
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Product product, IFormFile file)
        {
            BlobContainerClient container = new BlobContainerClient(_Configuration.GetConnectionString("AzureBlob"), "attac");
            await container.CreateIfNotExistsAsync();
            BlobClient blob = container.GetBlobClient(file.FileName);
            using var stream = file.OpenReadStream();

            BlobUploadOptions options = new BlobUploadOptions()
            {
                HttpHeaders = new BlobHttpHeaders() { ContentType = file.ContentType }
            };


            if (!blob.Exists())
            {
                await blob.UploadAsync(stream, options);
            }

            product.ProductImage = blob.Uri.ToString();
            await _product.UpdateProduct(id, product);
            stream.Close();
            return RedirectToAction("Index");
        }

        // GET: Products/Delete/5
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> Delete(int id)
        {
            await _product.GetProduct(id);
                return View();
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _product.DeleteProduct(id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Filter(string searchString)
        {
            var allProducts = await _product.GetProducts();

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResultNew = allProducts.Where(y => y.Name.Contains(searchString)).ToList();

                return View("Index", filteredResultNew);
            }

            return View("Index", allProducts);
        }
    }

}
