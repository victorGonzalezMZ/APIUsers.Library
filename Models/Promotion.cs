using System;
using System.Collections.Generic;
using System.Text;

namespace APIUsers.Library.Models
{
    public class Promotion
    {
        public int id { get; set; }
        public string code { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime expires_date { get; set; }
        public string theme { get; set; }
        public int discount { get; set; }

    }
}
