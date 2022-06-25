using AWS_E_Commerce.Data;
using AWS_E_Commerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestAWS_E_Commerce
{
    public class ProductTest
    {
        [Fact]
        public void CanGetProduct()
        {
            //Arrange
            Product product = new Product();
            product.Name = "Men jeans";

            //Assert
            Assert.Equal("Men jeans", product.Name);
        }
        [Fact]
        public void CanSetProduct()
        {
            //Arrange
            Product product = new Product()
            {
                Name = "Men jeans",
                ProductImage = "https://sultan.blob.core.windows.net/attac/jeans.png",
                Price = 19.99,
                size = "M",
                color = "red"
            };
            product.Name = "Baby Jacket";
            //Assert
            Assert.Equal("Baby Jacket", product.Name);
        }
        [Fact]
        public async void CrudProductInDB()
        {
            //Arrange
            //setup our DB
            //set values

            DbContextOptions<AWSDbContext> options =
            new DbContextOptionsBuilder<AWSDbContext>().UseInMemoryDatabase("DbCanSave").Options;

            //Act
            using (AWSDbContext context = new AWSDbContext(options))
            {
                Product product = new Product();
                product.Name = "Test Product";
                product.Price = 23.99;
                product.ProductImage = "https://sultan.blob.core.windows.net/attac/jeans.png";
                product.color = "red";
                product.size = "M";

                ////Act

                context.Products.Add(product);
                context.SaveChanges();

                var productName = await context.Products.FirstOrDefaultAsync(x => x.Name == product.Name);

                //Assert
                Assert.Equal("Test Product", productName.Name);

                //UPDATE
                product.Name = "Update Product";
                context.Products.Update(product);
                context.SaveChanges();

                var updatedHotel = await context.Products.FirstOrDefaultAsync(x => x.Name == product.Name);

                Assert.Equal("Update Product", updatedHotel.Name);

                //DELETE
                context.Products.Remove(product);
                context.SaveChanges();

                var deletedProduct = await context.Products.FirstOrDefaultAsync(x => x.Name == product.Name);

                Assert.True(deletedProduct == null);
            }

        }
    }
    
}
