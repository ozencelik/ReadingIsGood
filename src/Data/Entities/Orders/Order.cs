using Data.Entities.Base;
using Data.Entities.Customers;
using Data.Enums.OrderStatuses;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities.Orders
{
    public class Order : AuditableEntity
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
        /// Order's customer
        /// </summary>
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

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
        public IList<OrderProduct> OrderProducts { get; set; }
    }
}
