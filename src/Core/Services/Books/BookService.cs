using Data.Entities.Books;
using Data.Repositories;
using System;
using System.Collections.Generic;
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
            return await _bookRepository.GetAllAsync();
        }

        public async Task<Book> GetBookByIdAsync(int bookId)
        {
            return await _bookRepository.GetByIdAsync(bookId);
        }
        #endregion
    }
}
