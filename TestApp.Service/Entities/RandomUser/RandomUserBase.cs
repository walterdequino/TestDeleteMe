using Newtonsoft.Json;

namespace TestApp.Domain.Entities.RandomUser
{
    /// <summary>
    /// Random User
    /// </summary>
    public class RandomUser
    {
        [JsonProperty("gender")]
        public string? Gender { get; set; }

        [JsonProperty("name")]
        public RandomUserName? Name { get; set; }

        [JsonProperty("location")]
        public RandomUserLocation? Location { get; set; }

        [JsonProperty("email")]
        public string? Email { get; set; }

        [JsonProperty("login")]
        public RandomUserLoginInfo? Login { get; set; }

        [JsonProperty("dob")]
        public RandomUserDateOfBirth? Dob { get; set; }

        [JsonProperty("registered")]
        public RandomUserRegistration? Registered { get; set; }

        [JsonProperty("phone")]
        public string? Phone { get; set; }

        [JsonProperty("cell")]
        public string? Cell { get; set; }

        [JsonProperty("id")]
        public RandomUserIdentification? Id { get; set; }

        [JsonProperty("picture")]
        public RandomUserPicture? Picture { get; set; }

        [JsonProperty("nat")]
        public string? Nat { get; set; }
    }
}
