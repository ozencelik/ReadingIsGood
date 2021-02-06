using Data.Entities.Base;
using Data.Entities.Books;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities.Orders
{
    public class OrderProduct : AuditableEntity
    {
        /// <summary>
        /// Order id
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// OrderProduct's order
        /// </summary>
        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        /// <summary>
        /// Book id
        /// </summary>
        public int BookId { get; set; }

        /// <summary>
        /// OrderProduct's book
        /// </summary>
        [ForeignKey("BookId")]
        public Book Book { get; set; }

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
