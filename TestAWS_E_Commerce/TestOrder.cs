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
    public class TestOrder
    {
        [Fact]
        public async void CrudOrderInDB()
        {

            DbContextOptions<AWSDbContext> options =
            new DbContextOptionsBuilder<AWSDbContext>().UseInMemoryDatabase("DbCanSave").Options;

            //Act
            using (AWSDbContext context = new AWSDbContext(options))
            {
                Order order = new Order();
                order.Email = "Test";
                order.UserId = "1";

                ////Act

                context.Orders.Add(order);
                context.SaveChanges();

                var orderName = await context.Orders.FirstOrDefaultAsync(x => x.UserId == order.UserId);

                //Assert
                Assert.Equal("Test", orderName.Email);

                //UPDATE
                order.Email = "Update";
                context.Orders.Update(order);
                context.SaveChanges();

                var updatedOrder = await context.Orders.FirstOrDefaultAsync(x => x.UserId == order.UserId);

                Assert.Equal("Update", updatedOrder.Email);

                //DELETE
                context.Orders.Remove(order);
                context.SaveChanges();

                var deletedBasket = await context.Orders.FirstOrDefaultAsync(x => x.UserId == order.UserId);

                Assert.True(deletedBasket == null);
            }
        }
    }
}

