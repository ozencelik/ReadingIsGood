using Data.Entities.Base;
using Data.Entities.Orders;
using System.Collections.Generic;

namespace Data.Entities.Customers
{
    public  class Customer : AuditableEntity
    {
        /// <summary>
        /// User name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Customer unique user name.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// PasswordHash
        /// </summary>
        public byte[] PasswordHash { get; set; }

        /// <summary>
        /// PasswordSalt
        /// </summary>
        public byte[] PasswordSalt { get; set; }

        /// <summary>
        /// Customer's orders
        /// </summary>
        public IList<Order> Orders { get; set; }
    }
}
