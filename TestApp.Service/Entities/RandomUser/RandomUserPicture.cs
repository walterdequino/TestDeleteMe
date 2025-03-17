using Newtonsoft.Json;

namespace TestApp.Domain.Entities.RandomUser
{
    /// <summary>
    /// Random User Picture
    /// </summary>
    public class RandomUserPicture
    {
        [JsonProperty("large")]
        public string? Large { get; set; }

        [JsonProperty("medium")]
        public string? Medium { get; set; }

        [JsonProperty("thumbnail")]
        public string? Thumbnail { get; set; }
    }
}
