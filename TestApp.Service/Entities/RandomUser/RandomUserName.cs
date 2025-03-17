using Newtonsoft.Json;

namespace TestApp.Domain.Entities.RandomUser
{
    /// <summary>
    /// User Name
    /// </summary>
    public class RandomUserName
    {
        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("first")]
        public string? First { get; set; }

        [JsonProperty("last")]
        public string? Last { get; set; }
    }
}
