namespace Core.Events.Consumers
{
    /// <summary>
    /// Evnt consumer
    /// </summary>
    public interface IConsumer<T>
    {
        /// <summary>
        /// Handle event
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="eventMessage">Event message</param>
        void HandleEvent(T eventMessage);
    }
}
