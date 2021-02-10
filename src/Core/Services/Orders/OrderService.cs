using Data.Entities.Orders;
using Data.Enums.OrderStatuses;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services.Orders
{
    public class OrderService : IOrderService
    {
        #region Fields
        private readonly IRepository<Order> _orderRepository;
        private readonly IStockService _stockService;
        #endregion

        #region Ctor
        public OrderService(IRepository<Order> orderRepository,
            IStockService stockService)
        {
            _orderRepository = orderRepository;
            _stockService = stockService;
        }
        #endregion

        #region Methods
        public async Task<Order> GetCustomerOrderByIdAsync(int orderId, int customerId)
        {
            return await _orderRepository.Table
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Book)
                .FirstOrDefaultAsync(o => o.Id == orderId
                && o.CustomerId == customerId
                && !o.Deleted);
        }

        public async Task<IList<Order>> GetCustomerOrdersAsync(int customerId)
        {
            return await _orderRepository.Table
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Book)
                .Where(o => o.CustomerId == customerId
                && !o.Deleted).ToListAsync();
        }

        public async Task<int> InsertOrderAsync(Order order)
        {
            order.OrderStatus = OrderStatus.Completed;
            await _orderRepository.InsertAsync(order);

            // Update Stocks
            if (order.Id > 0)
            {
                var insertedOrder = await GetCustomerOrderByIdAsync(order.Id, order.CustomerId);

                foreach (var orderProduct in insertedOrder.OrderProducts)
                {
                    await _stockService.UpdateStock(orderProduct.Book, orderProduct.Quantity);
                }
            }

            return order.Id;
        }

        public async Task<int> UpdateOrderAsync(Order order)
        {
            return await _orderRepository.UpdateAsync(order);
        }
        #endregion        
    }
}
