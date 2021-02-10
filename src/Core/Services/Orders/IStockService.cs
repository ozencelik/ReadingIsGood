using Data.Dtos.OrderDtos;
using Data.Entities.Books;
using System.Threading.Tasks;

namespace Core.Services.Orders
{
    public interface IStockService
    {
        /// <summary>
        /// Checks stock existance
        /// for the books in an order
        /// </summary>
        /// <returns>bool</returns>
        Task<bool> CheckStockAvailablity(CreateOrderDto order);

        /// <summary>
        /// Checks the stock availability
        /// </summary>
        /// <returns>bool</returns>
        Task<bool> StockIsAvailable(int bookId, int quantity);

        /// <summary>
        /// Update the stock of an entity
        /// </summary>
        /// <returns>bool</returns>
        Task UpdateStock(Book book, int quantity, bool decrease = true);
    }
}
