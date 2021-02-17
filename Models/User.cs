using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIUsers.Library.Models
{
    public class UserMin
    {
        public int ID { get; set; }
        public string Nick { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Address { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string City { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string State { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Country { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Zip { get; set; }

        public string Role { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Imagen { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Phone { get; set; }
    }
    public class User : UserMin
    {
        public DateTime CreateDate { get; set; }
        public AccountType accountType { get; set; }
        public string JWT { get; set; }
    }

    public enum AccountType : int
    {
        Basic = 0,
        Administrator = 1,
    }
}