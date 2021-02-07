namespace Api.Configuration
{
    public static class ApiRoutes
    {
        public const string Root = "api";

        public const string Version = "v1";

        public const string Base = Root + "/" + Version;

        public const string CustomerBase = Base + "/customer/";

        public static class Customers
        {
            public const string Delete = CustomerBase;

            public const string GetAll = CustomerBase;

            public const string GetById = CustomerBase + "/{customer-id}";

            public const string Login = CustomerBase + "/login";

            public const string Register = CustomerBase + "/register";

            public const string Update = CustomerBase;
        }

        public static class AuditLogs
        {
            public const string GetById = Base + "/audit-logs/{audit-log-id}";

            public const string GetAll = Base + "/audit-logs";
        }
    }
}
