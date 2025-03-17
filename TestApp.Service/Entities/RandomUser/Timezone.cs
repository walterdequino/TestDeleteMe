using Newtonsoft.Json;

namespace TestApp.Domain.Entities.RandomUser
{
    /// <summary>
    /// Time Zone
    /// </summary>
    public class Timezone
    {
        [JsonProperty("offset")]
        public string? Offset { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }
    }
}
