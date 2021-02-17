using System;
using System.Collections.Generic;


namespace APIUsers.Library.Interfaces
{
    public interface ICheckout : IDisposable{
  
        Models.Checkout GetUserByNick(string nick);
        int getProductValidbyCode(string code);

        int InsertCheckout(int idUser,string code,string paymentMethod);
     
        string getShoping(int idUser);
     
    }
}
