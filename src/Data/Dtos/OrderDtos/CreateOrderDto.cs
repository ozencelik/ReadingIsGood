using System.Collections.Generic;

namespace Data.Dtos.OrderDtos
{
    public class CreateOrderDto
    {
        /// <summary>
        /// Order note
        /// </summary>
        public string OrderNote { get; set; }

        /// <summary>
        /// Order's customer id
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Order's order products
        /// </summary>
        public IList<CreateOrderProductDto> OrderProducts { get; set; }
    }
}
