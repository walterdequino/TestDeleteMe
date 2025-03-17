namespace TestApp.Domain.Filters
{
    /// <summary>
    /// Internal Validation Exception
    /// </summary>
    public class InternalValidationException : InvalidOperationException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public InternalValidationException()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message"></param>
        public InternalValidationException(string message) : base(message)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public InternalValidationException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}