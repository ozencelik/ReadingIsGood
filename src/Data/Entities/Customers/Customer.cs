using Data.Entities.Base;
using Data.Entities.Orders;
using System.Collections.Generic;

namespace Data.Entities.Customers
{
    public  class Customer : AuditableEntity
    {
        /// <summary>
        /// Customer unique user name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Customer's orders
        /// </summary>
        public IList<Order> Orders { get; set; }
    }
}
