using Newtonsoft.Json;

namespace TestApp.Domain.Entities.RandomUser
{
    /// <summary>
    /// Coordinates
    /// </summary>
    public class Coordinates
    {
        [JsonProperty("latitude")]
        public string? Latitude { get; set; }

        [JsonProperty("longitude")]
        public string? Longitude { get; set; }
    }
}
