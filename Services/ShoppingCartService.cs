using APIUsers.Library.Helpers.Datos;
using APIUsers.Library.Interfaces;
using APIUsers.Library.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace APIUsers.Library.Services
{
    public class ShoppingCartService : IShoppingCart, IDisposable
    {

        SqlConexion sql = null;
        MySqlConexion mysql = null;
        ConnectionType type = ConnectionType.NONE;

        ShoppingCartService(){

        }


        public static ShoppingCartService CrearInstanciaSQL(SqlConexion sql){
            ShoppingCartService log = new ShoppingCartService{
                sql = sql,
                type = ConnectionType.MSSQL
            };
            return log;
        }

        public static ShoppingCartService CrearInstanciaMySQL(MySqlConexion mysql){
            ShoppingCartService log = new ShoppingCartService{
                mysql = mysql,
                type = ConnectionType.MYSQL
            };
            return log;
        }

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

        public int addToShoppingCart(int id_user, int id_producto)
        {
            int ID_Return = 0;
            List<SqlParameter> _Parametros = new List<SqlParameter>();

            try
            {
                _Parametros.Add(new SqlParameter("@Id_User", id_user));
                _Parametros.Add(new SqlParameter("@Id_Product", id_producto));
                SqlParameter valreg = new SqlParameter();
                valreg.ParameterName = "@ID_Shopping";
                valreg.DbType = DbType.Int32;
                valreg.Direction = ParameterDirection.Output;
                _Parametros.Add(valreg);

                sql.PrepararProcedimiento("[dbo].[ShoppingCart.Insert]", _Parametros);
                ID_Return = int.Parse(sql.EjecutarProcedimientoOutput().ToString());
                return ID_Return;
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

        public int addToShoppingCart(int id_user, int id_producto, int quantity)
        {
            int ID_Return = 0;
            List<SqlParameter> _Parametros = new List<SqlParameter>();

            try
            {
                _Parametros.Add(new SqlParameter("@Id_User", id_user));
                _Parametros.Add(new SqlParameter("@Id_Product", id_producto));
                _Parametros.Add(new SqlParameter("@Quantity", quantity));
                SqlParameter valreg = new SqlParameter();
                valreg.ParameterName = "@ID_Shopping";
                valreg.DbType = DbType.Int32;
                valreg.Direction = ParameterDirection.Output;
                _Parametros.Add(valreg);

                sql.PrepararProcedimiento("[dbo].[ShoppingCart.InsertLocal]", _Parametros);
                ID_Return = int.Parse(sql.EjecutarProcedimientoOutput().ToString());
                return ID_Return;
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

        public int removeFromShoppingCart(int id_user, int id_product)
        {
            int ID_Return = 0;
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            try
            {
                _Parametros.Add(new SqlParameter("@Id_User", id_user));
                _Parametros.Add(new SqlParameter("@Id_Product", id_product));
                SqlParameter valreg = new SqlParameter();
                valreg.ParameterName = "@ID_ShoppingCart";
                valreg.DbType = DbType.Int32;
                valreg.Direction = ParameterDirection.Output;
                _Parametros.Add(valreg);

                sql.PrepararProcedimiento("[dbo].[ShoppingCart.Delete]", _Parametros);
                ID_Return = int.Parse(sql.EjecutarProcedimientoOutput().ToString());
                return ID_Return;
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

        public List<ShoppingCart> getShoppingCart(int id_user){

            List<ShoppingCart> list = new List<ShoppingCart>();
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            try{
                _Parametros.Add(new SqlParameter("@Id", id_user));
                sql.PrepararProcedimiento("dbo.[ShoppingCart.GetJSONByIdUser]", _Parametros);
                DataTableReader dtr = sql.EjecutarTableReader(CommandType.StoredProcedure);
                if (dtr.HasRows){
                    while (dtr.Read()){
                        var Json = dtr["ShoppingCart"].ToString();
                        if (Json != string.Empty)
                        {
                            JArray arr = JArray.Parse(Json);
                            foreach (JObject jsonOperaciones in arr.Children<JObject>())
                            {
                                list.Add(new ShoppingCart{
                                    ID_Product = Convert.ToInt32(jsonOperaciones["ID_Product"].ToString()),
                                    Quantity = Convert.ToInt32(jsonOperaciones["Quantity"].ToString())
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

        public int updateShoppingCart_ProductQuantity(int id_user, int id_product, int quantity){
            int ID_Return = 0;
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            try
            {
                _Parametros.Add(new SqlParameter("@Id_User", id_user));
                _Parametros.Add(new SqlParameter("@Id_Product", id_product));
                _Parametros.Add(new SqlParameter("@Quantity", quantity));
                SqlParameter valreg = new SqlParameter();
                valreg.ParameterName = "@ID_ShoppingCart";
                valreg.DbType = DbType.Int32;
                valreg.Direction = ParameterDirection.Output;
                _Parametros.Add(valreg);
                 sql.PrepararProcedimiento("dbo.[ShoppingCart.Update]", _Parametros);
                ID_Return = int.Parse(sql.EjecutarProcedimientoOutput().ToString());
                return ID_Return;
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


        #endregion
    }
}
