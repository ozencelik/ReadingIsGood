namespace Data.Dtos.OrderDtos
{
    public class CreateOrderProductDto
    {
        /// <summary>
        /// Book id
        /// </summary>
        public int BookId { get; set; }

        /// <summary>
        /// Book quantity
        /// </summary>
        public int Quantity { get; set; }
    }
}
