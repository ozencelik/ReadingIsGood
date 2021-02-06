namespace Data.Enums
{
    public enum OrderStatus
    {
        // Completed status refers
        // that an order get paid and delivered
        // to customer succesfully
        Completed,

        // Cancelled status refers
        // that an order cancelled for some reason
        // and products return to warehouse
        // as well as their stocks.
        Cancelled
    }
}
