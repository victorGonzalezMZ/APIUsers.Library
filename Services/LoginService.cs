using System;
using System.Collections.Generic;
using System.Text;

namespace APIUsers.Library.Services
{
    using Interfaces;
    using Helpers.Datos;
    using Models;
    using System.Data.SqlClient;
    using System.Data;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class LoginService: ILogin, IDisposable {
        
        #region Constructor y Variables

        SqlConexion sql = null;
        MySqlConexion mysql = null;
        ConnectionType type = ConnectionType.NONE;

        LoginService(){

        }

        #endregion

        public User EstablecerLogin(string nick, string password){
            User user = new User();
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            try{
                _Parametros.Add(new SqlParameter("@Nick", nick));
                _Parametros.Add(new SqlParameter("@Password", password));
                sql.PrepararProcedimiento("dbo.[USER.GetJSON]", _Parametros);
                DataTableReader dtr = sql.EjecutarTableReader(CommandType.StoredProcedure);
                if (dtr.HasRows){
                    while (dtr.Read()){
                        var Json = dtr["Usuario"].ToString();
                        if (Json != string.Empty)
                            user = JsonConvert.DeserializeObject<User>(Json);
                    }
                }


            }
            catch(SqlException sqlEx)
            {

                throw new Exception(sqlEx.Message, sqlEx);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return user;
        }

        public List<User> ObtenerUsers()
        {

            List<User> list = new List<User>();
            User user = new User();
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
                        if(Json != string.Empty)
                        {
                            JArray arr = JArray.Parse(Json);
                            foreach (JObject jsonOperaciones in arr.Children<JObject>()) {
                                list.Add(new User()
                                {
                                    ID = Convert.ToInt32(jsonOperaciones["Id"].ToString()),
                                    FirstName = jsonOperaciones["Name"].ToString(),
                                    CreateDate = DateTime.Parse(jsonOperaciones["CreateDate"].ToString())
                                });
                            }
                        }
                    }
                }
            }catch(SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message, sqlEx);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return list;
        }
        public static LoginService CrearInstanciaSQL(SqlConexion sql){
           LoginService log = new LoginService{
                sql = sql,
                type = ConnectionType.MSSQL
            };
            return log;
        }

        public static LoginService CrearInstanciaMySQL(MySqlConexion mysql){
            LoginService log = new LoginService{
                mysql = mysql,
                type = ConnectionType.MYSQL

            };
            return log;

        }

        #region IDisposable Support
        private bool disposedValue = false; // Para detectar llamadas redundantes

        protected virtual void Dispose(bool disposing){
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (sql != null)
                    {
                        sql.Desconectar();
                        sql.Dispose();
                    }// TODO: elimine el estado administrado (objetos administrados).
                }

                // TODO: libere los recursos no administrados (objetos no administrados) y reemplace el siguiente finalizador.
                // TODO: configure los campos grandes en nulos.

                disposedValue = true;
            }
        }

        public void Dispose(){
            // No cambie este código. Coloque el código de limpieza en el anterior Dispose(colocación de bool).
            Dispose(true);
            // TODO: quite la marca de comentario de la siguiente línea si el finalizador se ha reemplazado antes.
            // GC.SuppressFinalize(this);
        }

      
        #endregion

    }
}
