using Data.Entities.Orders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services.Orders
{
    public interface IOrderService
    {
        /// <summary>
        /// Gets customer orders
        /// </summary>
        /// <returns>Categories</returns>
        Task<IList<Order>> GetCustomerOrdersAsync(int customerId);

        /// <summary>
        /// Gets a order
        /// </summary>
        /// <param name="orderId">Order identifier</param>
        /// <returns>Order</returns>
        Task<Order> GetCustomerOrderByIdAsync(int orderId, int customerId);


        /// <summary>
        /// Inserts order
        /// </summary>
        /// <param name="order">Order</param>
        Task<int> InsertOrderAsync(Order order);


        /// <summary>
        /// Updates the order
        /// </summary>
        /// <param name="order">Order</param>
        /// <returns>Order Id</returns>
        Task<int> UpdateOrderAsync(Order order);
    }
}
