using APIUsers.Library.Helpers.Datos;
using APIUsers.Library.Interfaces;
using APIUsers.Library.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace APIUsers.Library.Services
{
    public class CheckoutService : ICheckout, IDisposable
    {
        SqlConexion sql = null;
        ConnectionType type = ConnectionType.NONE;

        CheckoutService(){}


        public static CheckoutService CrearInstanciaSQL(SqlConexion sql){
            CheckoutService log = new CheckoutService {
                sql = sql,
                type = ConnectionType.MSSQL
            };
            return log;
        }

        

        public Checkout GetUserByNick(string nick)
        {
            Checkout Checkout = new Checkout();
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            try
            {
                _Parametros.Add(new SqlParameter("@Nick", nick));

                sql.PrepararProcedimiento("[dbo].[USER.GetJSONByNick_]", _Parametros);
                DataTableReader dtr = sql.EjecutarTableReader(CommandType.StoredProcedure);
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        var Json = dtr["Usuario"].ToString();
                        if (Json != string.Empty)
                        {
                        
                           
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

            return Checkout;
        }

        public int getProductValidbyCode(string code)
        {
            int valid = 0;
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            try
            {

                _Parametros.Add(new SqlParameter("@code ", code));

                SqlParameter valreg = new SqlParameter();
                valreg.ParameterName = "@discount";
                valreg.DbType = DbType.Int32;
                valreg.Direction = ParameterDirection.Output;
                _Parametros.Add(valreg);
                sql.PrepararProcedimiento("[dbo].[SHOPPINGCART_Discount]", _Parametros);
                valid = int.Parse(sql.EjecutarProcedimientoOutput().ToString());

            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message, sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return valid;
        }

        #region IDisposable Support
        private bool disposedValue = false; // Para detectar llamadas redundantes

        protected virtual void Dispose(bool disposing)
        {
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

        // TODO: reemplace un finalizador solo si el anterior Dispose(bool disposing) tiene código para liberar los recursos no administrados.
        // ~HidraService()
        // {
        //   // No cambie este código. Coloque el código de limpieza en el anterior Dispose(colocación de bool).
        //   Dispose(false);
        // }

        // Este código se agrega para implementar correctamente el patrón descartable.
        public void Dispose()
        {
            // No cambie este código. Coloque el código de limpieza en el anterior Dispose(colocación de bool).
            Dispose(true);
            // TODO: quite la marca de comentario de la siguiente línea si el finalizador se ha reemplazado antes.
            // GC.SuppressFinalize(this);
        }

        public int InsertCheckout(int idUser, string code, string paymentMethod)
        {
            int IdUser = 0;
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            try
            {
                _Parametros.Add(new SqlParameter("@_id_user", idUser));
                _Parametros.Add(new SqlParameter("@_paying_method", paymentMethod));
                _Parametros.Add(new SqlParameter("@_code_promotion", code));

                SqlParameter valreg = new SqlParameter();
                valreg.ParameterName = "@Result";
                valreg.DbType = DbType.Int32;
                valreg.Direction = ParameterDirection.Output;
                _Parametros.Add(valreg);

                sql.PrepararProcedimiento("dbo.[CHECKOUT]", _Parametros);
                IdUser = int.Parse(sql.EjecutarProcedimientoOutput().ToString());
                return IdUser;
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

        public string getShoping(int idUser){
            var Json = "";
            Checkout Checkout = new Checkout();
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            try{
                _Parametros.Add(new SqlParameter("@_id_user ", idUser));
                sql.PrepararProcedimiento("[dbo].[SALES.ByUser]", _Parametros);
                DataTableReader dtr = sql.EjecutarTableReader(CommandType.StoredProcedure);
                if (dtr.HasRows){
                    while (dtr.Read()){
                        Json = dtr["Result"].ToString();
                    }
                }
            }
            catch (SqlException sqlEx){
                throw new Exception(sqlEx.Message, sqlEx);
            }
            catch (Exception ex){
                throw new Exception(ex.Message, ex);
            }
            return Json;
        }
        #endregion

    }
}
