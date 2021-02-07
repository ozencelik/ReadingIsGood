using Data.Entities.Customers;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Customers
{
    public class CustomerService : ICustomerService
    {
        #region Fields
        private readonly IRepository<Customer> _customerRepository;
        #endregion

        #region Ctor
        public CustomerService(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }
        #endregion

        #region Methods
        public async Task<int> DeleteCustomerAsync(Customer customer)
        {
            customer.Deleted = true;
            return await UpdateCustomerAsync(customer);
        }

        public async Task<IList<Customer>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetAllAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            return await _customerRepository.GetByIdAsync(customerId);
        }

        public async Task<Customer> GetCustomerByUsernameAsync(string username)
        {
            return await _customerRepository.Table
                .Where(u => string.Equals(u.Username, username)
                && !u.Deleted)?.FirstOrDefaultAsync();
        }

        public async Task<string> GetCustomernameByCustomerIdAsync(int customerId)
        {
            var customer = await _customerRepository.Table
                .Where(u => u.Id == customerId
                && !u.Deleted)?.FirstOrDefaultAsync();

            if (customer is null)
                return default;

            return customer.Username;
        }

        public async Task<Customer> LoginCustomerWithEmailAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username)
                || string.IsNullOrEmpty(password))
                return null;

            var customer = await GetCustomerByUsernameAsync(username);

            // check if email exists
            if (customer is null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, customer.PasswordHash, customer.PasswordSalt))
                return null;

            // login/authentication successful
            return customer;
        }

        public async Task<Customer> LoginCustomerWithUsernameAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username)
                || string.IsNullOrEmpty(password))
                return null;

            var customer = await GetCustomerByUsernameAsync(username);

            // check if customername exists
            if (customer is null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, customer.PasswordHash, customer.PasswordSalt))
                return null;

            // login/authentication successful
            return customer;
        }

        public async Task<int> InsertCustomerAsync(Customer customer)
        {
            if (customer is null)
                throw new ArgumentNullException("Customer is required");

            return await _customerRepository.InsertAsync(customer);
        }

        public async Task<Customer> RegisterCustomerAsync(Customer customer, string password)
        {
            // validation
            if (customer is null)
                throw new ArgumentNullException("Customer is required");

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException("Password is required");

            if (await GetCustomerByUsernameAsync(customer.Username) != null)
                throw new ArgumentNullException("Username \"" + customer.Username + "\" is already taken");

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            customer.PasswordHash = passwordHash;
            customer.PasswordSalt = passwordSalt;

            await InsertCustomerAsync(customer);

            return customer;
        }

        public async Task<int> UpdateCustomerAsync(Customer customer, string password = null)
        {
            var updatedCustomer = await GetCustomerByIdAsync(customer.Id);

            if (updatedCustomer is null)
                throw new ArgumentNullException("Customer not found.");

            // update customername if it has changed
            if (!string.IsNullOrWhiteSpace(updatedCustomer.Username)
                && !string.Equals(updatedCustomer.Username, customer.Username))
            {
                // throw error if the new customername is already taken
                if (await GetCustomerByUsernameAsync(customer.Username) is null)
                    updatedCustomer.Username = customer.Username;
                else
                    throw new ArgumentNullException("Username " + customer.Username + " is already taken");
            }

            // update customer properties if provided
            if (!string.IsNullOrWhiteSpace(customer.Name))
                updatedCustomer.Name = customer.Name;

            // update password if provided
            if (!string.IsNullOrWhiteSpace(password))
            {
                CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

                updatedCustomer.PasswordHash = passwordHash;
                updatedCustomer.PasswordSalt = passwordSalt;
            }

            return await _customerRepository.UpdateAsync(updatedCustomer);
        }
        #endregion

        #region Helper Methods
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null)
                throw new ArgumentNullException("password");

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null)
                throw new ArgumentNullException("password");

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            if (storedHash.Length != 64)
                throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");

            if (storedSalt.Length != 128)
                throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordSalt");

            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i])
                        return false;
                }
            }

            return true;
        }
        #endregion
    }
}
