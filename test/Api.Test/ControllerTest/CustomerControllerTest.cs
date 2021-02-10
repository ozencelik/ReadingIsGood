using Api.Configuration;
using Api.Controllers;
using AutoMapper;
using Core.Services.Customers;
using Data.Dtos.CustomerDtos;
using Data.Entities.Customers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace Api.IntegrationTest.ControllerTest
{
    public class CustomerControllerTest
    {
        [Fact]
        public async Task GetCustomer_ReturnNotFoundResult_WhenResultIsNotFound()
        {
            // Arrange
            var mapper = Substitute.For<IMapper>();
            var customerService = Substitute.For<ICustomerService>();
            var appSettings = Substitute.For<IOptions<AppSettings>>();
            var customerController = new CustomerController(mapper, customerService, appSettings);

            // Act
            var result = await customerController.GetCustomer(-1);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        public async Task GetCustomer_ReturnOkResult_WhenResultIsFound()
        {
            // Arrange
            var mapper = Substitute.For<IMapper>();
            var customerService = Substitute.For<ICustomerService>();
            var appSettings = Substitute.For<IOptions<AppSettings>>();
            var customerController = new CustomerController(mapper, customerService, appSettings);

            var customer = new CreateCustomerDto()
            {
                Name = "Test Customer",
                Username = "test",
                Password = "test"
            };

            await customerController.Register(customer);

            // Act
            var result = await customerController.GetCustomer(1);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task InsertCustomer_ReturnOkResult()
        {
            // Arrange
            var mapper = Substitute.For<IMapper>();
            var customerService = Substitute.For<ICustomerService>();
            var appSettings = Substitute.For<IOptions<AppSettings>>();
            var customerController = new CustomerController(mapper, customerService, appSettings);

            // Act
            var customer = new CreateCustomerDto() { Username = "test1", Name = "Test1 Customer", Password = "Test1 Password" };
            var insertedCustomer = await customerController.Register(customer);

            // Assert
            Assert.IsType<OkObjectResult>(insertedCustomer);
        }

        [Fact]
        public async Task InsertCustomer_ReturnBadRequest_WhenCustomerUsernameIsAlreadyExist()
        {
            // Arrange
            var mapper = Substitute.For<IMapper>();
            var customerService = Substitute.For<ICustomerService>();
            var appSettings = Substitute.For<IOptions<AppSettings>>();
            var customerController = new CustomerController(mapper, customerService, appSettings);

            // Act
            var customer = new CreateCustomerDto() { Username = "test1", Name = "Test Customer", Password = "Test Password" };
            var insertedCustomer = await customerController.Register(customer);

            // Assert
            Assert.IsType<OkObjectResult>(insertedCustomer);
        }
    }
}
