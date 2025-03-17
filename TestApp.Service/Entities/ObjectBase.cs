using Newtonsoft.Json;

namespace TestApp.Domain.Entities
{
    /// <summary>
    /// Object Base
    /// </summary>
    public class ObjectBase<T>
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonProperty("id")]
        public required string Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty("name")]
        public required string Name { get; set; }

        /// <summary>
        /// Data
        /// </summary>
        [JsonProperty("data")]
        public required T Data { get; set; }
    }
}
