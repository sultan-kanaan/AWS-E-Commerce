﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AWS_E_Commerce.Models.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string size { get; set; }
        public string color { get; set; }

        [Display(Name = "Image")]
        public string ProductImage { get; set; }

        [Display(Name = "Category")]
        public int ProductCategoryId { get; set; }

        [Display(Name ="Category")]
        public string ProductCategoryName { get; set; }


        //Navigation Property that makes a ProductCategoryId a Foreign Key
        public IEnumerable<ProductCategory> ProductCategory { get; set; }
    }
}
