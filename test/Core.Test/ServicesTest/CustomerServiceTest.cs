using Core.Services.Customers;
using Data.Entities.Customers;
using Data.Repositories;
using NSubstitute;
using System;
using Xunit;

namespace Core.Test.ServicesTest
{
    public class CustomerServiceTest
    {
        [Fact]
        public void Create_Customer_Throws_ArgumentNullException_When_Model_IsNull()
        {
            // Arrange
            var customerRepository = Substitute.For<IRepository<Customer>>();
            var customerService = new CustomerService(customerRepository);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => customerService.InsertCustomerAsync(null));
        }
    }
}
