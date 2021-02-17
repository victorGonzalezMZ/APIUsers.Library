using System;
using System.Collections.Generic;
using System.Text;

namespace APIUsers.Library.Models
{
    public class Checkout
    {
        public int id { get; set; }
        public string nick { get; set; }
        public string checkoutName { get; set; }
        public string checkoutApellidos { get; set; }
        public string checkoutemail { get; set; }
        public string checkoutaddress { get; set; }
        public string checkoutCity { get; set; }
        public string checkoutState { get; set; }
        public string checkoutZip { get; set; }
        public string paymentMethod { get; set; }
        public string cc_name { get; set; }
        public string cc_number { get; set; }
        public string cc_expiration { get; set; }
        public string cc_cvv { get; set; }
        public int id_shoping_card { get; set; }
        public string code { get; set; }
        public int idUser { get; set; }
    }

}
