namespace TestApp.Domain.Filters
{
    /// <summary>
    /// Service Down Exception
    /// </summary>
    public class ServiceDownException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ServiceDownException()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message"></param>
        public ServiceDownException(string message) : base(message)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public ServiceDownException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}