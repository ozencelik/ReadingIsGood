using Api.Configuration;
using AutoMapper;
using Core.Services.Books;
using Core.Services.Customers;
using Core.Services.Orders;
using Data.Dtos.OrderDtos;
using Data.Entities.Customers;
using Data.Entities.Orders;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class OrderController : Controller
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IBookService _bookService;
        private readonly ICustomerService _customerService;
        private readonly IOrderService _orderService;
        private readonly IStockService _stockService;
        #endregion

        #region Ctor
        public OrderController(IMapper mapper,
            IBookService bookService,
            ICustomerService customerService,
            IOrderService orderService,
            IStockService stockService)
        {
            this._mapper = mapper;
            this._bookService = bookService;
            this._customerService = customerService;
            this._orderService = orderService;
            this._stockService = stockService;
        }
        #endregion

        #region Methods

        [HttpGet(ApiRoutes.Orders.GetById)]
        public async Task<IActionResult> GetOrder(int orderId)
        {
            if (orderId <= 0)
                return BadRequest("Order id must be greater than zero.");

            var customer = await _customerService.GetCurrentCustomerAsync();
            if (customer is null)
                return NotFound("No authenticated customer found !!!");

            // Get order
            var order = await _orderService.GetCustomerOrderByIdAsync(orderId, customer.Id);

            return order is null ?
                BadRequest("Order not found !!!") :
                Ok(_mapper.Map<GetOrderDto>(order));
        }

        [HttpGet(ApiRoutes.Orders.GetMyOrders)]
        public async Task<IActionResult> GetMyOrders()
        {
            var orders = await _orderService.GetCustomerOrdersAsync(1);

            return orders is null ?
                BadRequest("No orders found !!!") :
                Ok(_mapper.Map<IList<GetOrderDto>>(orders));
        }

        [HttpPost(ApiRoutes.Orders.Checkout)]
        public async Task<IActionResult> Checkout([FromBody] CreateOrderDto model)
        {
            if (model is null)
                return BadRequest(nameof(model));

            var customer = await _customerService.GetCustomerByIdAsync(model.CustomerId);

            if (customer is null)
                return NotFound("Customer not found !!!");

            var stockExist = await _stockService.CheckStockAvailablity(model);

            if (!stockExist)
                return NotFound("Stock not found !!!");

            try
            {
                // map model to entity
                var order = await PrepareOrderToCheckout(model, customer);

                // create order
                await _orderService.InsertOrderAsync(order);
            }
            catch (Exception ex)
            {
                return Ok("Order not created.");
            }

            return Ok("Order created ✔");
        }

        [HttpPost(ApiRoutes.Orders.Update)]
        public async Task<IActionResult> Update([FromBody] UpdateOrderDto model)
        {
            if (model is null)
                return BadRequest(nameof(model));

            var customerId = _customerService.CustomerId;
            var order = await _orderService.GetCustomerOrderByIdAsync(model.Id, customerId);

            if (order is null)
                return NotFound("Order not found !!!");
            try
            {
                // map model to entity
                order.OrderNote = model.OrderNote;
                order.OrderStatus = model.OrderStatus;

                // create order
                await _orderService.UpdateOrderAsync(order);
            }
            catch (Exception ex)
            {
                return Ok("Order not updated.");
            }

            return Ok("Order updated ✔");
        }
        #endregion

        #region Private Helper Methods
        private async Task<Order> PrepareOrderToCheckout(CreateOrderDto model, Customer customer)
        {
            if (model is null || customer is null)
                return default;

            var order = _mapper.Map<Order>(model);

            order.CustomerId = customer.Id;

            var books = await _bookService.GetAllBooksAsync();
            foreach (var orderProduct in order.OrderProducts)
            {
                var book = books.FirstOrDefault(b => b.Id == orderProduct.BookId);

                orderProduct.TotalPrice = book.Price * orderProduct.Quantity;
                order.OrderTotal += orderProduct.TotalPrice;
            }

            return order;
        }
        #endregion
    }
}
