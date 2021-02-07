namespace Data.Dtos.BookDtos
{
    public class GetBookDto
    {
        /// <summary>
        /// Book id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Book name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Book description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Book author
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Book price
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Book stock
        /// </summary>
        public int StockQuantity { get; set; }
    }
}
