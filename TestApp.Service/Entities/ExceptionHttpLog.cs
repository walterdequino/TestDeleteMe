namespace TestApp.Domain.Entities
{
    /// <summary>
    /// Exception Http Log
    /// </summary>
    public class ExceptionHttpLog
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="requestType"></param>
        /// <param name="requestUrl"></param>
        /// <param name="requestSended"></param>
        /// <param name="responseReceived"></param>
        public ExceptionHttpLog(string requestType, string requestUrl, object requestSended, string responseReceived)
        {
            RequestSended = requestSended;
            ResponseReceived = responseReceived;
            RequestUrl = requestUrl;
            RequestType = requestType;
        }

        /// <summary>
        /// Request Type
        /// </summary>
        public string RequestType { get; set; }

        /// <summary>
        /// Request Url
        /// </summary>
        public string RequestUrl { get; set; }

        /// <summary>
        /// Request Sended
        /// </summary>
        public object RequestSended { get; set; }

        /// <summary>
        /// Response Received
        /// </summary>
        public string ResponseReceived { get; set; }
    }
}
