using Data.Dtos.OrderDtos;
using Data.Entities.Orders;
using System.Collections.Generic;

namespace Data.Dtos.CustomerDtos
{
    public class GetCustomerDto
    {
        /// <summary>
        /// User name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Username a unique value.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Customer's orders
        /// </summary>
        public IList<GetOrderDto> Orders { get; set; }
    }
}
