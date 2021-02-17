using APIUsers.Library.Helpers.Datos;
using APIUsers.Library.Interfaces;
using APIUsers.Library.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace APIUsers.Library.Services{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class UserService: IUser, IDisposable {

        #region Constructor y Variables

        SqlConexion sql = null;
        MySqlConexion mysql = null;
        ConnectionType type = ConnectionType.NONE;

        UserService() {

        }

        public User GetUser(int id){
            User user = new User();
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            try
            {
                _Parametros.Add(new SqlParameter("@Id", id));
                sql.PrepararProcedimiento("dbo.[USER.GetJSONById]", _Parametros);
                DataTableReader dtr = sql.EjecutarTableReader(CommandType.StoredProcedure);
                if (dtr.HasRows)
                {
                    while (dtr.Read()){
                        var Json = dtr["Usuario"].ToString();
                        if (Json != string.Empty)
                            user = JsonConvert.DeserializeObject<User>(Json);
                    }
                }


            }
            catch (SqlException sqlEx)
            {

                throw new Exception(sqlEx.Message, sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return user;
        }

        public User GetUser(string Nick)
        {
            User user = new User();
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            try
            {
                _Parametros.Add(new SqlParameter("@Nick", Nick));
                sql.PrepararProcedimiento("dbo.[USER.GetJSONByNick_]", _Parametros);
                DataTableReader dtr = sql.EjecutarTableReader(CommandType.StoredProcedure);
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        var Json = dtr["Usuario"].ToString();
                        if (Json != string.Empty)
                        {
                            user = JsonConvert.DeserializeObject<User>(Json);
                            user.Password = "";
                            user.RefreshToken = "";
                        }
                         
                    }
                }


            }
            catch (SqlException sqlEx)
            {

                throw new Exception(sqlEx.Message, sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return user;
        }

        public List<User> GetUsers(){
            List<User> list = new List<User>();
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            try
            {
                sql.PrepararProcedimiento("dbo.[USER.GetAllJSON]", _Parametros);
                DataTableReader dtr = sql.EjecutarTableReader(CommandType.StoredProcedure);
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        var Json = dtr["Usuario"].ToString();
                        if (Json != string.Empty)
                        {
                            JArray arr = JArray.Parse(Json);
                            foreach (JObject jsonOperaciones in arr.Children<JObject>()){
                                
                                JToken token = jsonOperaciones["Imagen"];
                                String imagn = "";
                                if(token != null)
                                {
                                    imagn = jsonOperaciones["Imagen"].ToString();
                                }
                                

                                list.Add(new User(){
                                    ID = Convert.ToInt32(jsonOperaciones["ID"].ToString()),
                                    Nick = jsonOperaciones["Nick"].ToString(),
                                    CreateDate = DateTime.Parse(jsonOperaciones["CreateDate"].ToString()),
                                    FirstName = jsonOperaciones["Firstname"].ToString(),
                                    Lastname = jsonOperaciones["Lastname"].ToString(),
                                    Email = jsonOperaciones["Email"].ToString(),
                                    Address = jsonOperaciones["Address"].ToString(),
                                    City = jsonOperaciones["City"].ToString(),
                                    State = jsonOperaciones["State"].ToString(),
                                    Country = jsonOperaciones["Country"].ToString(),
                                    Zip = jsonOperaciones["Zip"].ToString(),
                                    Role = jsonOperaciones["Role"].ToString(),
                                    Imagen = imagn
                                });

                            }

                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message, sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return list;
        }

        public User CheckRefreshToken(string nick)
        {
            User user = new User();
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            try{
                _Parametros.Add(new SqlParameter("@Nick", nick));
                sql.PrepararProcedimiento("dbo.[USER.GetJSONByNick]", _Parametros);
                DataTableReader dtr = sql.EjecutarTableReader(CommandType.StoredProcedure);
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        var Json = dtr["Usuario"].ToString();
                        if (Json != string.Empty)
                            user = JsonConvert.DeserializeObject<User>(Json);
                    }
                }


            }
            catch (SqlException sqlEx)
            {

                throw new Exception(sqlEx.Message, sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return user;
        }

        public bool UpdateUser(UserMin user){
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            try
            {
                _Parametros.Add(new SqlParameter("@Nick", user.Nick));
                _Parametros.Add(new SqlParameter("@Password", user.Password));
                _Parametros.Add(new SqlParameter("@Name", user.FirstName));
                _Parametros.Add(new SqlParameter("@Last", user.Lastname));
                _Parametros.Add(new SqlParameter("@email", user.Email));
                _Parametros.Add(new SqlParameter("@Address", user.Address));
                _Parametros.Add(new SqlParameter("@City", user.City));
                _Parametros.Add(new SqlParameter("@State", user.State));
                _Parametros.Add(new SqlParameter("@Country", user.Country));
                _Parametros.Add(new SqlParameter("@Zip", user.Zip));
                _Parametros.Add(new SqlParameter("@Imagen", user.Imagen));
                _Parametros.Add(new SqlParameter("@Id", user.ID));
                _Parametros.Add(new SqlParameter("@Phone", user.Phone));
                sql.PrepararProcedimiento("dbo.[USER.Update]", _Parametros);
                sql.EjecutarProcedimiento();
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message, sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return true;
        }
        public int InsertUser(UserMin user){
            int IdUser = 0;
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            try{
                _Parametros.Add(new SqlParameter("@Nick", user.Nick));
                _Parametros.Add(new SqlParameter("@Password", user.Password));
                _Parametros.Add(new SqlParameter("@Name", user.FirstName));
                _Parametros.Add(new SqlParameter("@Last", user.Lastname));
                _Parametros.Add(new SqlParameter("@email", user.Email));
                _Parametros.Add(new SqlParameter("@Address", user.Address));
                _Parametros.Add(new SqlParameter("@City", user.City));
                _Parametros.Add(new SqlParameter("@State", user.State));
                _Parametros.Add(new SqlParameter("@Country", user.Country));
                _Parametros.Add(new SqlParameter("@Zip", user.Zip));
                _Parametros.Add(new SqlParameter("@Imagen", user.Imagen));
                _Parametros.Add(new SqlParameter("@Role", user.Role));
                _Parametros.Add(new SqlParameter("@Phone", user.Phone));

                SqlParameter valreg = new SqlParameter();
                valreg.ParameterName = "@Id";
                valreg.DbType = DbType.Int32;
                valreg.Direction = ParameterDirection.Output;
                _Parametros.Add(valreg);

                sql.PrepararProcedimiento("dbo.[USER.Insert]", _Parametros);
                IdUser = int.Parse(sql.EjecutarProcedimientoOutput().ToString());
                return IdUser;
            }
            catch (SqlException sqlEx){
                throw new Exception(sqlEx.Message, sqlEx);
            }
            catch (Exception ex){
                throw new Exception(ex.Message, ex);
            }
          
        }
        public bool DeleteUser(int id) { 
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            try{
                _Parametros.Add(new SqlParameter("@Id", id));
                sql.PrepararProcedimiento("dbo.[USER.Delete]", _Parametros);
                sql.EjecutarProcedimiento();
            }
            catch (SqlException sqlEx)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public static UserService CrearInstanciaSQL(SqlConexion sql) {
            UserService log = new UserService {
                sql = sql,
                type = ConnectionType.MSSQL
            };
            return log;

        }

        public static UserService CrearInstanciaMySQL(MySqlConexion mysql){ 
            UserService log = new UserService {
                mysql = mysql,
                type = ConnectionType.MYSQL

            };
            return log;

        }

        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // Para detectar llamadas redundantes

            protected virtual void Dispose(bool disposing){
                if (!disposedValue){
                    if (disposing){
                        if (sql != null){
                            sql.Desconectar();
                            sql.Dispose();
                        }// TODO: elimine el estado administrado (objetos administrados).
                    }
                    // TODO: libere los recursos no administrados (objetos no administrados) y reemplace el siguiente finalizador.
                    // TODO: configure los campos grandes en nulos.
                    disposedValue = true;
                }
            }

            // TODO: reemplace un finalizador solo si el anterior Dispose(bool disposing) tiene código para liberar los recursos no administrados.
            // ~HidraService()
            // {
            // // No cambie este código. Coloque el código de limpieza en el anterior Dispose(colocación de bool).
            // Dispose(false);
            // }

            // Este código se agrega para implementar correctamente el patrón descartable.
            public void Dispose(){
                // No cambie este código. Coloque el código de limpieza en el anterior Dispose(colocación de bool).
                Dispose(true);
                // TODO: quite la marca de comentario de la siguiente línea si el finalizador se ha reemplazado antes.
                // GC.SuppressFinalize(this);
            }

        void IUser.UpdateUserRefreshToken(User user){
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            try
            {
                _Parametros.Add(new SqlParameter("@RefreshToken", user.RefreshToken));
                _Parametros.Add(new SqlParameter("@RefreshTokenExpiryTime", user.RefreshTokenExpiryTime));
                _Parametros.Add(new SqlParameter("@Id", user.ID));
                sql.PrepararProcedimiento("dbo.[USER.UpdRefreshToken]", _Parametros);
                sql.EjecutarProcedimiento();
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message, sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }

        public bool UpdateUser_domicilio(User user)
        {
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            try
            {
                _Parametros.Add(new SqlParameter("@Address", user.Address));
                _Parametros.Add(new SqlParameter("@City", user.City));
                _Parametros.Add(new SqlParameter("@State", user.State));
                _Parametros.Add(new SqlParameter("@Country", user.Country));
                _Parametros.Add(new SqlParameter("@Zip", user.Zip));
                _Parametros.Add(new SqlParameter("@Id", user.ID));
                _Parametros.Add(new SqlParameter("@Phone", user.Phone));
                sql.PrepararProcedimiento("dbo.[USER.Update_domicilio]", _Parametros);
                sql.EjecutarProcedimiento();
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message, sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return true;
        }






        #endregion

    }



}
