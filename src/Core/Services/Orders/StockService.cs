using Core.Services.Books;
using Data.Dtos.OrderDtos;
using Data.Entities.Books;
using System;
using System.Threading.Tasks;

namespace Core.Services.Orders
{
    public class StockService : IStockService
    {
        #region Fields
        private readonly IBookService _bookService;
        #endregion

        #region Ctor
        public StockService(IBookService bookService)
        {
            _bookService = bookService;
        }
        #endregion

        #region Methods
        public async Task<bool> CheckStockAvailablity(CreateOrderDto order)
        {
            if (order is null)
                throw new ArgumentNullException("order", "Order cannot be null");

            foreach (var book in order.OrderProducts)
            {
                var stockExist = await StockIsAvailable(book.BookId, book.Quantity);
                if (!stockExist)
                    return false;
            }
            return true;
        }

        public async Task<bool> StockIsAvailable(int bookId, int quantity)
        {
            var book = await _bookService.GetBookByIdAsync(bookId);

            if (book is null)
                return false;

            return book.StockQuantity >= quantity;
        }

        public async Task UpdateStock(Book book, int quantity, bool decrease = true)
        {
            if (decrease)
                book.StockQuantity -= quantity;
            else
                book.StockQuantity += quantity;

            await _bookService.UpdateBookAsync(book);
        }


        #endregion
    }
}
