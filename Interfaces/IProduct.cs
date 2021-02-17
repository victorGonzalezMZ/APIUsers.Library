using System;
using System.Collections.Generic;
using System.Text;

namespace APIUsers.Library.Interfaces
{
    public interface IProduct : IDisposable{
        List<Models.Product> GetAllProducts();

        Models.Product GetProduct(int id);

        List<Models.Product> GetAllProductsByCategory(string category);

        List<Models.Product> GetAllProductsByBrand(string brand);

        List<Models.Product> GetAllProductsBySearch(string search);

        List<Models.Product> GetAllProductsByParams(Models.Parametros parametros);

        int InsertProduct(Models.Product product);

        Boolean DeleteProduct(int id);

        Boolean UpdateProduct(Models.Product product);

        /*Metodos para los apartados*/
        List<Models.Product> GetTop3NewProducts();

        List<Models.Product> GetTop3Random();

        List<Models.Product> GetTop3ByCategory(string category, int id);
    }

}
