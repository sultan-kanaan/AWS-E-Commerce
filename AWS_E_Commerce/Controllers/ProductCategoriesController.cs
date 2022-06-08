using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AWS_E_Commerce.Models.Interfaces;
using AWS_E_Commerce.Models.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace AWS_E_Commerce.Controllers
{
    public class ProductCategoriesController : Controller
    {
        private readonly ICategory _category;

        public ProductCategoriesController(ICategory category)
        {
            _category = category;
        }

        // GET: ProductCategories
        public async Task<IActionResult> Index()
        {
            List<ProductCategoryDTO> products = await _category.GetProductCategories();
            return View(products);
        }

        // GET: ProductCategories/Details/5
        public async Task<IActionResult> Details(int id)
        {
            ProductCategoryDTO products = await _category.GetProductCategory(id);
            return View(products);
           
        }


        // GET: ProductCategories/Create
        [Authorize(Policy ="Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductCategories/Create
        [HttpPost]
        public async Task<IActionResult> Create(ProductCategoryDTO productCategory)
        {
            if (ModelState.IsValid)
            {
                await _category.CreateProductCategory(productCategory);
                return RedirectToAction("Index");
            }
            return View(productCategory);
        }

        // GET: ProductCategories/Edit/5
        [Authorize(Policy = "Editor")]
        public async Task<IActionResult> Edit(int id)
        {
            ProductCategoryDTO category = await _category.GetProductCategory(id);
            return View(category);
        }

        // POST: ProductCategories/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id,  ProductCategoryDTO productCategory)
        {
            await _category.UpdateProductCategory(id,productCategory);
            return RedirectToAction("Index");
           
        }

        // GET: ProductCategories/Delete/5
        [Authorize(Policy = "Administrator")]
        public async  Task<IActionResult> Delete(int id)
        {
            await _category.GetProductCategory(id);
            return View();
        }

        // POST: ProductCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _category.DeleteProductCategory(id);
            return RedirectToAction("Index");
        }
    }
}
