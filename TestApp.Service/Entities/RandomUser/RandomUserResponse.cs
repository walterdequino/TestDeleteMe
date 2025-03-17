using Newtonsoft.Json;

namespace TestApp.Domain.Entities.RandomUser
{
    /// <summary>
    /// Random User Response
    /// </summary>
    public class RandomUserResponse
    {
        /// <summary>
        /// Results
        /// </summary>
        [JsonProperty("results")]
        public List<RandomUser> Results = [];
    }
}
