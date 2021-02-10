using Data.Context;
using Data.Entities.Books;
using Data.Entities.Customers;
using Data.Entities.Orders;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Data.Test.RepositoryTest
{
    public class OrderRepositoryTest
    {
        #region Fields
        private readonly DbContextOptions<AppDbContext> _contextOptions;
        #endregion

        #region Ctor
        public OrderRepositoryTest()
        {
            _contextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql("Persist Security Info=False;" +
                "Username=afnpctqwttgxxj85;" +
                "Password=ojs6ebsxq2nh8ftv;" +
                "database=sht40aklp29l6ppo;" +
                "server=n2o93bb1bwmn0zle.chr7pe7iynqr.eu-west-1.rds.amazonaws.com;" +
                "Connect Timeout=3600;" +
                "SslMode=Required").Options;
        }
        #endregion

        #region Methods
        [Fact]
        public async Task Can_Add_Order()
        {
            //Arrange
            var testCustomer = new Customer() { Name = "Test Customer to Order" };
            var testBook = new Book() { Name = "Test Book to Order", Price = 15.50M, };

            using (var context = new AppDbContext(_contextOptions))
            {
                var customerRepository = new EfCoreRepository<Customer>(context);
                await customerRepository.InsertAsync(testCustomer);

                var bookRepository = new EfCoreRepository<Book>(context);
                await bookRepository.InsertAsync(testBook);
            }

            var testOrder = new Order()
            {
                CustomerId = testCustomer.Id,
                OrderNote = "Test Order",
                OrderProducts = new List<OrderProduct>()
                {
                    new OrderProduct()
                    {
                        BookId = testBook.Id,
                        TotalPrice = testBook.Price * 5
                    }
                }
            };
            testOrder.OrderTotal = testOrder.OrderProducts.Sum(op => op.TotalPrice);

            using (var context = new AppDbContext(_contextOptions))
            {
                //Act
                var orderRepository = new EfCoreRepository<Order>(context);
                await orderRepository.InsertAsync(testOrder);

                //Assert
                var order = await orderRepository.GetByIdAsync(testOrder.Id);

                Assert.Equal(testOrder.Id, order.Id);
            }
        }

        [Fact]
        public async Task Can_Update_Order()
        {
            //Arrange
            var testCustomer = new Customer() { Name = "Test Customer to Order" };
            var testBook = new Book() { Name = "Test Book to Order", Price = 15.50M, };

            using (var context = new AppDbContext(_contextOptions))
            {
                var customerRepository = new EfCoreRepository<Customer>(context);
                await customerRepository.InsertAsync(testCustomer);

                var bookRepository = new EfCoreRepository<Book>(context);
                await bookRepository.InsertAsync(testBook);
            }

            var testOrder = new Order()
            {
                CustomerId = testCustomer.Id,
                OrderNote = "Test Order",
                OrderProducts = new List<OrderProduct>()
                {
                    new OrderProduct()
                    {
                        BookId = testBook.Id,
                        TotalPrice = testBook.Price * 5
                    }
                }
            };
            testOrder.OrderTotal = testOrder.OrderProducts.Sum(op => op.TotalPrice);

            using (var context = new AppDbContext(_contextOptions))
            {
                //Act
                var repository = new EfCoreRepository<Order>(context);
                var insertedOrderId = await repository.InsertAsync(testOrder);
                var insertedOrder = await repository.GetByIdAsync(insertedOrderId);
                insertedOrder.OrderNote = "Test Updated Order";
                await repository.UpdateAsync(insertedOrder);

                //Assert
                var updatedOrder = await repository.GetByIdAsync(insertedOrderId);

                Assert.Equal(insertedOrder.OrderNote, updatedOrder.OrderNote);
            }
        }
        #endregion
    }
}
