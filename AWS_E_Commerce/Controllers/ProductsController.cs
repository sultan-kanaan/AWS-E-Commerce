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

namespace AWS_E_Commerce.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProduct _product;
        private readonly AWSDbContext _context;

        public ProductsController(IProduct product, AWSDbContext context)
        {
            _context = context;
            _product = product;
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
        [Authorize(Policy = "Administrator")]
        public async Task<IActionResult> Create()
        {
            var product = new ProductDTO
            {
             ProductCategory = await _context.Categories.OrderBy(p => p.Name).ToListAsync()
            };
            return View(product);
        }

        // POST: Products/Create
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                await _product.CreateProduct(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize(Policy = "Editor")]
        public async Task<IActionResult> Edit(int id)
        {
            var product = new ProductDTO
            {         
             ProductCategory = await _context.Categories.OrderBy(p =>p.Name).ToListAsync()
            };
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            await _product.UpdateProduct(id, product);
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
    } 
}
