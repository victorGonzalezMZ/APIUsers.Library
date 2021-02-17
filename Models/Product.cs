using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIUsers.Library.Models
{
    public class Product{

        [JsonProperty("id_")]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Sku { get; set; }
        public string Description { get; set; }
        [JsonProperty("create_date")]
        public DateTime CreateDate { get; set; }
        public string Brand { get; set; }
        [JsonProperty("update_date")]
        public DateTime UpdateDate { get; set; }
        [JsonProperty("id_category")]
        public int IdCategory { get; set; }
        public int Ranking { get; set; }
        public double Price { get; set; }
        [JsonProperty("seeling_price")]
        public double SeelingPrice { get; set; }
        public string Status { get; set; }
        [JsonProperty("image")]
        public string Imagen { get; set; }
        public string Category { get; set; }

    }
}
