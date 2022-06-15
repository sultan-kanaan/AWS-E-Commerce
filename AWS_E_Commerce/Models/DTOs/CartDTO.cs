using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWS_E_Commerce.Models.DTOs
{
    public class CartDTO
    {
        public static List<ProductDTO> products { get; set; }
        static CartDTO()
        {
            products = new List<ProductDTO>();
        }
    }
}
