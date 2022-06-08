using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWS_E_Commerce.Models
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Product> products { get; set; }

    }
}
