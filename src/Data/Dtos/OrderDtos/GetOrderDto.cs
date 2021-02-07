using Data.Dtos.CustomerDtos;
using Data.Enums.OrderStatuses;
using System.Collections.Generic;

namespace Data.Dtos.OrderDtos
{
    public class GetOrderDto
    {
        /// <summary>
        /// Order id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Order note
        /// </summary>
        public string OrderNote { get; set; }

        /// <summary>
        /// Order's customer id
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Order's customer
        /// </summary>
        public GetCustomerDto Customer { get; set; }

        /// <summary>
        /// Order status
        /// </summary>
        public OrderStatus OrderStatus { get; set; }

        /// <summary>
        /// Total price of order
        /// </summary>
        public decimal OrderTotal { get; set; }

        /// <summary>
        /// Order's order products
        /// </summary>
        public IList<GetOrderProductDto> OrderProducts { get; set; }

    }
}
