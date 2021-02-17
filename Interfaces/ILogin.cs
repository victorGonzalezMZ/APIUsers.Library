using APIUsers.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIUsers.Library.Interfaces
{
    public interface ILogin : IDisposable{
        Models.User EstablecerLogin(string nick, string password);
        List<Models.User> ObtenerUsers();
    }
}
