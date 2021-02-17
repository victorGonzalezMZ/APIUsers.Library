using APIUsers.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIUsers.Library.Interfaces
{
    public interface IShoppingCart : IDisposable{

        int addToShoppingCart(int id_user, int id_producto);
        int addToShoppingCart(int id_user, int id_producto, int quantity);
        int removeFromShoppingCart(int id_user, int id_producto);
        int updateShoppingCart_ProductQuantity(int id_user, int id_product, int quantity);
        List<ShoppingCart> getShoppingCart(int id_user);

    }
}
