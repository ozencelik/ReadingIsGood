namespace Api.Configuration
{
    public static class ApiRoutes
    {
        private const string Root = "api";

        public const string Version = "v1";

        private const string Base = Root + "/" + Version;

        private const string BookBase = Base + "/book";

        private const string CustomerBase = Base + "/customer";

        private const string OrderBase = Base + "/order";

        public static class Books
        {
            public const string GetAll = BookBase;

            public const string GetById = BookBase + "/{bookId}";
        }

        public static class Customers
        {
            public const string Delete = CustomerBase;

            public const string GetAll = CustomerBase;

            public const string GetById = CustomerBase + "/{customerId}";

            public const string Login = CustomerBase + "/login";

            public const string Register = CustomerBase + "/register";

            public const string Update = CustomerBase;
        }

        public static class Orders
        {
            public const string Cancel = OrderBase;

            public const string GetMyOrders = OrderBase;

            public const string GetById = OrderBase + "/{orderId}";

            public const string Checkout = OrderBase + "/checkout";

            public const string Update = OrderBase + "/update";
        }

        public static class AuditLogs
        {
            public const string GetById = Base + "/audit-logs/{auditLogId}";

            public const string GetAll = Base + "/audit-logs";
        }
    }
}
