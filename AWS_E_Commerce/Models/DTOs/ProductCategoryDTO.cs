using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWS_E_Commerce.Models.DTOs
{
    public class ProductCategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<ProductDTO> products { get; set; }
    }
}
