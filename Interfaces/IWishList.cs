using APIUsers.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIUsers.Library.Interfaces
{
    public interface IWishList : IDisposable{
        int addToWishList(int id_user, int id_producto);

        List<Product> getProducts_Wishlist(int id_user);

        List<Product> getProducts_Wishlist_ByOrder(int id, string order);

        int removeFromWishList(int id_user, int id_producto);

        List<Product> searchProducts_Wishlist(string busqueda, int id);


    }
}
