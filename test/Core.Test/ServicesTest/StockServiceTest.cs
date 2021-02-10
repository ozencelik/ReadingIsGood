using Core.Services.Books;
using Core.Services.Orders;
using Data.Entities.Books;
using Data.Repositories;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace Core.IntegrationTest.ServicesTest
{
    public class StockServiceTest
    {
        public async Task Can_Update_Stock()
        {
            // Arrange
            var bookRepository = Substitute.For<IRepository<Book>>();
            var bookService = new BookService(bookRepository);
            var stockService = new StockService(bookService);

            int testStockQuantity = 5;
            var testBook = new Book()
            {
                Name = "Test Book",
                StockQuantity = testStockQuantity,
                Price = 100
            };
            await bookService.InsertBookAsync(testBook);

            // Act
            await stockService.UpdateStock(testBook, 1);

            // Assert
            var updatedBook = await bookService.GetBookByIdAsync(testBook.Id);
            Assert.Equal(updatedBook.StockQuantity, testStockQuantity);
        }
    }
}
