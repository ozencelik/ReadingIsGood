using Data.Entities.Customers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services.Customers
{
    public interface ICustomerService
    {
        /// <summary>
        /// Delete customer
        /// </summary>
        /// <param name="customer">Customer</param>
        Task<int> DeleteCustomerAsync(Customer customer);

        /// <summary>
        /// Gets all customers
        /// </summary>
        /// <returns>Categories</returns>
        Task<IList<Customer>> GetAllCustomersAsync();

        /// <summary>
        /// Gets a customer
        /// </summary>
        /// <param name="customerId">Customer identifier</param>
        /// <returns>Customer</returns>
        Task<Customer> GetCustomerByIdAsync(int customerId);

        /// <summary>
        /// Gets a customer
        /// </summary>
        /// <param name="userName">Username</param>
        /// <returns>User</returns>
        Task<Customer> GetCustomerByUsernameAsync(string username);

        /// <summary>
        /// Gets a customer
        /// </summary>
        /// <param name="username">Customername</param>
        /// <param name="password">Password</param>
        /// <returns>Customer</returns>
        Task<Customer> LoginCustomerWithUsernameAsync(string username, string password);

        /// <summary>
        /// Inserts customer
        /// </summary>
        /// <param name="customer">Customer</param>
        Task<int> InsertCustomerAsync(Customer customer);

        /// <summary>
        /// Register a customer
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <param name="password">Password</param>
        /// <returns>Customer</returns>
        Task<Customer> RegisterCustomerAsync(Customer customer, string password);

        /// <summary>
        /// Updates the customer
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <param name="password">Password</param>
        /// <returns>Customer Id</returns>
        Task<int> UpdateCustomerAsync(Customer customer, string password = null);
    }
}
