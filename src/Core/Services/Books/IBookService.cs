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
        /// <returns>Categories</returns>
        Task<IList<Book>> GetAllBooksAsync();

        /// <summary>
        /// Gets a book
        /// </summary>
        /// <param name="bookId">Book identifier</param>
        /// <returns>Book</returns>
        Task<Book> GetBookByIdAsync(int bookId);
    }
}
