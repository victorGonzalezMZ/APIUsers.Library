using System;
using System.Collections.Generic;


namespace APIUsers.Library.Interfaces
{
    public interface ICategory : IDisposable{
        List<Models.Category> ObtenerCategorias();

        Models.Category GetCategory(int id);

        List<Models.CategorySelect> obtenerCategoriasSelected();

        int InsertCategory(Models.Category category);

        Boolean DeleteCategory(int id);

        Boolean UpdateCategory(Models.Category category);

        List<Models.Category> GetAllCategoriesBySearch(string search);

    }
    
}
