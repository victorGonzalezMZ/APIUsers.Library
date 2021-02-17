using System;
using System.Collections.Generic;
using System.Text;

namespace APIUsers.Library.Interfaces
{
   public  interface IUser: IDisposable{
        List<Models.User> GetUsers();

        Models.User GetUser(int id);

        Models.User GetUser(string nick);

        int InsertUser(Models.UserMin user);

        Boolean UpdateUser(Models.UserMin user);

        Boolean DeleteUser(int id);

        void UpdateUserRefreshToken(Models.User user);

        Models.User CheckRefreshToken(string nick);

        Boolean UpdateUser_domicilio(Models.User user);
       
    }

}
