using Newtonsoft.Json;

namespace TestApp.Domain.Entities.RandomUser
{
    /// <summary>
    /// Location
    /// </summary>
    public class RandomUserLocation
    {
        [JsonProperty("street")]
        public RandomUserStreet? RandomUserStreet { get; set; }

        [JsonProperty("city")]
        public string? City { get; set; }

        [JsonProperty("state")]
        public string? State { get; set; }

        [JsonProperty("country")]
        public string? Country { get; set; }

        [JsonProperty("postcode")]
        public string? Postcode { get; set; }

        [JsonProperty("coordinates")]
        public Coordinates? Coordinates { get; set; }

        [JsonProperty("timezone")]
        public Timezone? Timezone { get; set; }
    }
}
