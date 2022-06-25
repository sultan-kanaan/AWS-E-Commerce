using AWS_E_Commerce.Models;
using Xunit;

namespace TestAWS_E_Commerce
{
    public class ProductCategoryTest
    {
        [Fact]
        public void CanGetCategory()
        {
            //Arrange
            ProductCategory category = new ProductCategory();
            category.Name = "MEN";

            //Assert
            Assert.Equal("MEN", category.Name);
        }
        [Fact]
        public void CanSetCategory()
        {
            //Arrange
            ProductCategory category = new ProductCategory()
            {
                Name = "MEN",

            };
            category.Name = "Women";
            //Assert
            Assert.Equal("Women", category.Name);
        }
    }
    
}
