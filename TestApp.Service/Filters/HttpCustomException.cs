namespace TestApp.Domain.Filters
{
    /// <summary>
    /// Http Custom Exception
    /// </summary>
    public class HttpCustomException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public HttpCustomException()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message"></param>
        public HttpCustomException(string message) : base(message)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public HttpCustomException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}