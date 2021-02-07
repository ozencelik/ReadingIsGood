using Data.Context;
using Data.Entities.Customers;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace Data.Test.RepositoryTest
{
    public class CustomerRepositoryTest
    {
        #region Fields
        private readonly DbContextOptions<AppDbContext> _contextOptions;
        #endregion

        #region Ctor
        public CustomerRepositoryTest()
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
        public async Task Can_Add_Customer()
        {
            //Arrange
            var testCustomer = new Customer()
            {
                Name = "First Customer"
            };

            using (var context = new AppDbContext(_contextOptions))
            {
                //Act
                var repository = new EfCoreRepository<Customer>(context);
                await repository.InsertAsync(testCustomer);

                //Assert
                var customer = await repository.GetByIdAsync(testCustomer.Id);

                Assert.Equal(testCustomer.Id, customer.Id);
                Assert.Equal(testCustomer.Name, customer.Name);
            }
        }

        [Fact]
        public async Task Can_Update_Customer()
        {
            //Arrange
            var testCustomer = new Customer()
            {
                Name = "First Customer"
            };

            using (var context = new AppDbContext(_contextOptions))
            {
                //Act
                var repository = new EfCoreRepository<Customer>(context);
                var insertedCustomerId = await repository.InsertAsync(testCustomer);
                var insertedCustomer = await repository.GetByIdAsync(insertedCustomerId);
                insertedCustomer.Name = "First Updated Customer";
                await repository.UpdateAsync(insertedCustomer);

                //Assert
                var updatedCustomer = await repository.GetByIdAsync(insertedCustomerId);

                Assert.Equal(insertedCustomer.Name, updatedCustomer.Name);
            }
        }

        [Fact]
        public async Task Can_Delete_Customer()
        {
            //Arrange
            var testCustomer = new Customer()
            {
                Name = "İlk Bölüm"
            };

            using (var context = new AppDbContext(_contextOptions))
            {
                //Act
                var repository = new EfCoreRepository<Customer>(context);
                await repository.InsertAsync(testCustomer);
                await repository.DeleteAsync(testCustomer);

                //Assert
                var deletedCustomer = await repository.GetByIdAsync(testCustomer.Id);

                Assert.Null(deletedCustomer);
            }
        }
        #endregion
    }
}
