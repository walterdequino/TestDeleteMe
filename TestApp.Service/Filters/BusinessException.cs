namespace TestApp.Domain.Filters
{
    /// <summary>
    /// Http Custom Exception
    /// </summary>
    public class BusinessException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public BusinessException()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message"></param>
        public BusinessException(string message) : base(message)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public BusinessException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}