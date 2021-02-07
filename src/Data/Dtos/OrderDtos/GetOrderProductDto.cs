using Data.Dtos.BookDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos.OrderDtos
{
    public class GetOrderProductDto
    {
        /// <summary>
        /// Order product id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Order id
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// OrderProduct's order
        /// </summary>
        public GetOrderDto Order { get; set; }

        /// <summary>
        /// Book id
        /// </summary>
        public int BookId { get; set; }

        /// <summary>
        /// OrderProduct's book
        /// </summary>
        public GetBookDto Book { get; set; }

        /// <summary>
        /// Book quantity
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Book total price
        /// </summary>
        public decimal TotalPrice { get; set; }
    }
}
