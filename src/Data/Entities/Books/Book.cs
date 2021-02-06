using Data.Entities.Base;

namespace Data.Entities.Books
{
    public class Book : AuditableEntity
    {
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

        /// <summary>
        /// Determine the book is published.
        /// </summary>
        public bool Published { get; set; } = true;
    }
}
