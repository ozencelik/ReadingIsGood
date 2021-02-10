using Data.Enums.OrderStatuses;

namespace Data.Dtos.OrderDtos
{
    public class UpdateOrderDto
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
        /// Order status
        /// </summary>
        public OrderStatus OrderStatus { get; set; }
    }
}
