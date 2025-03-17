using Newtonsoft.Json;

namespace TestApp.Domain.Entities
{
    /// <summary>
    /// Object Creation Result
    /// </summary>
    public class ObjectCreationResult
    {
        /// <summary>
        /// Id of new Object
        /// </summary>
        [JsonProperty("id")]
        public required string Id { get; set; }
    }
}
