﻿using Newtonsoft.Json;

namespace TestApp.Domain.Entities.RandomUser
{
    /// <summary>
    /// Random User Login Info
    /// </summary>
    public class RandomUserLoginInfo
    {
        [JsonProperty("uuid")]
        public string? Uuid { get; set; }

        [JsonProperty("username")]
        public string? Username { get; set; }

        [JsonProperty("password")]
        public string? Password { get; set; }

        [JsonProperty("salt")]
        public string? Salt { get; set; }

        [JsonProperty("md5")]
        public string? Md5 { get; set; }

        [JsonProperty("sha1")]
        public string? Sha1 { get; set; }

        [JsonProperty("sha256")]
        public string? Sha256 { get; set; }
    }
}
