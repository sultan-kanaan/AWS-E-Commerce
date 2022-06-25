using AWS_E_Commerce.Data;
using AWS_E_Commerce.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;
namespace TestAWS_E_Commerce
{
   public class CrudCategoryTest
    {
        [Fact]
        public async void CrudCategoryInDB()
        {
            //Arrange
            //setup our DB
            //set values

            DbContextOptions<AWSDbContext> options =
            new DbContextOptionsBuilder<AWSDbContext>().UseInMemoryDatabase("DbCanSave").Options;

            //Act
            using (AWSDbContext context = new AWSDbContext(options))
            {
                ProductCategory category = new ProductCategory();
                category.Name = "Test category";


                ////Act

                context.Categories.Add(category);
                context.SaveChanges();

                var categoryName = await context.Categories.FirstOrDefaultAsync(x => x.Name == category.Name);

                //Assert
                Assert.Equal("Test category", categoryName.Name);

                //UPDATE
                category.Name = "Update category";
                context.Categories.Update(category);
                context.SaveChanges();

                var updatedcategory = await context.Categories.FirstOrDefaultAsync(x => x.Name == category.Name);

                Assert.Equal("Update category", updatedcategory.Name);

                //DELETE
                context.Categories.Remove(category);
                context.SaveChanges();

                var deletedcategory = await context.Categories.FirstOrDefaultAsync(x => x.Name == category.Name);

                Assert.True(deletedcategory == null);
            }

        }
    }
}
