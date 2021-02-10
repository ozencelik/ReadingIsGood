using Data.Entities.Books;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services.Books
{
    public class BookService : IBookService
    {
        #region Fields
        private readonly IRepository<Book> _bookRepository;
        #endregion

        #region Ctor
        public BookService(IRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }
        #endregion

        #region Methods
        public async Task<IList<Book>> GetAllBooksAsync()
        {
            return await _bookRepository.Table
                .Where(x => !x.Deleted && x.Published).ToListAsync();
        }

        public async Task<Book> GetBookByIdAsync(int bookId)
        {
            return await _bookRepository.Table
                .FirstOrDefaultAsync(x => x.Id == bookId
                && !x.Deleted && x.Published);
        }

        public async Task<int> InsertBookAsync(Book book)
        {
            return await _bookRepository.InsertAsync(book);
        }

        public async Task<int> UpdateBookAsync(Book book)
        {
            return await _bookRepository.UpdateAsync(book);
        }
        #endregion
    }
}
