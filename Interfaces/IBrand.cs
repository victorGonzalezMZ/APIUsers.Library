using System;
using System.Collections.Generic;
using System.Text;

namespace APIUsers.Library.Interfaces
{
    public interface IBrand : IDisposable{
        List<Models.Brand> obtenerMarcasSelected();
    }
}
