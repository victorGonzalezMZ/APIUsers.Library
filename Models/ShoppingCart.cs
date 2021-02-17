using System;
using System.Collections.Generic;
using System.Text;

namespace APIUsers.Library.Models
{
    public class ShoppingCart{
        public int ID { get; set; }

        public int ID_User { get; set; }
        public int ID_Product { get; set; }
        public int Quantity { get; set; }

    }
}
