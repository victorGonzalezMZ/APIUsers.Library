using System;
using System.Collections.Generic;


namespace APIUsers.Library.Services
{

    using Interfaces;
    using Helpers.Datos;
    using Models;
    using System.Data.SqlClient;
    using System.Data;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;


    public class BrandService : IBrand, IDisposable{

        SqlConexion sql = null;
        MySqlConexion mysql = null;
        ConnectionType type = ConnectionType.NONE;


        public static BrandService CrearInstanciaSQL(SqlConexion sql)
        {
            BrandService log = new BrandService
            {
                sql = sql,
                type = ConnectionType.MSSQL
            };
            return log;
        }

        public static BrandService CrearInstanciaMySQL(MySqlConexion mysql)
        {
            BrandService log = new BrandService
            {
                mysql = mysql,
                type = ConnectionType.MYSQL

            };
            return log;

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

        public void Dispose()
        {
            // No cambie este código. Coloque el código de limpieza en el anterior Dispose(colocación de bool).
            Dispose(true);
            // TODO: quite la marca de comentario de la siguiente línea si el finalizador se ha reemplazado antes.
            // GC.SuppressFinalize(this);
        }
        #endregion

        public List<Brand> obtenerMarcasSelected(){
            List<Brand> list = new List<Brand>();
            List<SqlParameter> _Parametros = new List<SqlParameter>();

            list.Add(new Brand(){
                value = 0,
                label = "Todos",
            });

            try{
                sql.PrepararProcedimiento("dbo.[BRAND.GetAllJSON]", _Parametros);
                DataTableReader dtr = sql.EjecutarTableReader(CommandType.StoredProcedure);
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        var Json = dtr["Brand"].ToString();
                        if (Json != string.Empty)
                        {
                            JArray arr = JArray.Parse(Json);

                            foreach (JObject jsonOperaciones in arr.Children<JObject>())
                            {
                                list.Add(new Brand()
                                {
                                    value = Convert.ToInt32(jsonOperaciones["value"].ToString()),
                                    label = jsonOperaciones["label"].ToString(),
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
    }
}