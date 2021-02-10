using Core.Services.Customers;
using Data.Entities.Customers;
using Data.Repositories;
using Microsoft.AspNetCore.Http;
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
            var httpContextAccesor = Substitute.For<IHttpContextAccessor>();
            var customerService = new CustomerService(customerRepository, httpContextAccesor);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => customerService.InsertCustomerAsync(null));
        }
    }
}
