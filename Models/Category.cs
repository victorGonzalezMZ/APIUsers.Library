using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIUsers.Library.Models
{
    public class Category
    {
        [JsonProperty("id_")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("create_date")]
        public DateTime CreateDate { get; set; }
        [JsonProperty("update_date")]
        public DateTime UpdateDate { get; set; }

    }
}
