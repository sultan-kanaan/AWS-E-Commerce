using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AWS_E_Commerce.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string size { get; set; }
        public string color { get; set; }
        public string ProductImage { get; set; }
        //public byte[] ProductImage { get; set; }

        public string description { get; set; }
        [Display(Name ="Category")]
        public int ProductCategoryId { get; set; }

        //Navigation Property that makes a ProductCategoryId a Foreign Key
        public ProductCategory ProductCategory { get; set; } 

    }
}
