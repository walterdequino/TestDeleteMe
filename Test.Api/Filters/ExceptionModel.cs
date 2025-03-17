using System.Net;

using Test.Api.Enums;

namespace Test.Api.Filters
{
    /// <summary>
    /// Exception Model
    /// </summary>
    public class ExceptionModel
    {
        /// <summary>
        /// Txt Id
        /// </summary>
        public required string TxId { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        public ErrorTypes Type { get; set; }

        /// <summary>
        /// Status Code
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Status Code Description
        /// </summary>
        public string StatusCodeDescription => StatusCode.ToString();

        /// <summary>
        /// Title
        /// </summary>
        public required string Title { get; set; }

        /// <summary>
        /// Error Code
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public required string Description { get; set; }

        /// <summary>
        /// Detail
        /// </summary>
        public required string Detail { get; set; }

        /// <summary>
        /// Trace
        /// </summary>
        public required string Trace { get; set; }

        /// <summary>
        /// Inner Ex
        /// </summary>
        public required string InnerEx { get; set; }

        /// <summary>
        /// Ex Type
        /// </summary>
        public required string ExType { get; set; }
    }
}