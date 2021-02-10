using Api.Controllers;
using AutoMapper;
using Core.Services.Books;
using Core.Services.Customers;
using Core.Services.Orders;
using Data.Dtos.OrderDtos;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Api.IntegrationTest.ControllerTest
{
    public class OrderControllerTest
    {
        [Fact]
        public async Task CheckoutOrder_ReturnBadRequestResult_WhenModelIsNull()
        {
            // Arrange
            var mapper = Substitute.For<IMapper>();
            var bookService = Substitute.For<IBookService>();
            var customerService = Substitute.For<ICustomerService>();
            var orderService = Substitute.For<IOrderService>();
            var stockService = Substitute.For<IStockService>();
            var orderController = new OrderController(mapper, bookService, customerService, orderService, stockService);

            // Act
            var result = await orderController.Checkout(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CheckoutOrder_ReturnNotFound_WhenCustomerIsNotFound()
        {
            // Arrange
            var mapper = Substitute.For<IMapper>();
            var bookService = Substitute.For<IBookService>();
            var customerService = Substitute.For<ICustomerService>();
            var orderService = Substitute.For<IOrderService>();
            var stockService = Substitute.For<IStockService>();
            var orderController = new OrderController(mapper, bookService, customerService, orderService, stockService);

            // Act
            var order = new CreateOrderDto()
            {
                OrderNote = "Test Order",
                CustomerId = -1,
                OrderProducts = new List<CreateOrderProductDto>()
                {
                    new CreateOrderProductDto() { BookId = 1, Quantity = 1 },
                    new CreateOrderProductDto() { BookId = 2, Quantity = 1 }
                }
            };

            var insertedOrder = await orderController.Checkout(order);

            // Assert
            Assert.IsType<NotFoundObjectResult>(insertedOrder);
        }
    }
}
