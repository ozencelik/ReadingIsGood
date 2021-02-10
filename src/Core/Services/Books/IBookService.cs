using Data.Entities.Books;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services.Books
{
    public interface IBookService
    {
        /// <summary>
        /// Gets all categories
        /// </summary>
        /// <returns>Books</returns>
        Task<IList<Book>> GetAllBooksAsync();

        /// <summary>
        /// Gets a book
        /// </summary>
        /// <param name="bookId">Book identifier</param>
        /// <returns>Book</returns>
        Task<Book> GetBookByIdAsync(int bookId);

        /// <summary>
        /// Insert the book
        /// </summary>
        /// <returns>Book Id</returns>
        Task<int> InsertBookAsync(Book book);

        /// <summary>
        /// Updates the book
        /// </summary>
        /// <returns>Book Id</returns>
        Task<int> UpdateBookAsync(Book book);
    }
}
