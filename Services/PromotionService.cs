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
    public class PromotionService : IPromotion, IDisposable
    {
        SqlConexion sql = null;
        ConnectionType type = ConnectionType.NONE;
        PromotionService()
        {
        }


        public static PromotionService CrearInstanciaSQL(SqlConexion sql)
        {
            PromotionService log = new PromotionService
            {
                sql = sql,
                type = ConnectionType.MSSQL
            };

            return log;
        }
        public List<Promotion> GetPromotions()
        {
            List<Promotion> list = new List<Promotion>();
            Promotion Promotion = new Promotion();
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            try
            {
                sql.PrepararProcedimiento("dbo.[Promotion.GetAllJSON]", _Parametros);
                DataTableReader dtr = sql.EjecutarTableReader(CommandType.StoredProcedure);
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        var Json = dtr["Producto"].ToString();
                        if (Json != string.Empty)
                        {
                            JArray arr = JArray.Parse(Json);
                            foreach (JObject jsonOperaciones in arr.Children<JObject>())
                            {
                                //Promotion = JsonConvert.DeserializeObject<Promotion>(jsonOperaciones);
                                list.Add(new Promotion()
                                {
                                    id = Convert.ToInt32(jsonOperaciones["id_"].ToString()),
                                    title = jsonOperaciones["title"].ToString(),
                                    code = jsonOperaciones["code"].ToString(),
                                    description = jsonOperaciones["description"].ToString(),
                                    expires_date = DateTime.Parse(jsonOperaciones["expires_date"].ToString()),
                                    theme = jsonOperaciones["theme"].ToString(),
                                    discount = Convert.ToInt32(jsonOperaciones["discount"].ToString()),
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
        public List<Promotion> GetPromotionsById(int id)
        {
            List<Promotion> list = new List<Promotion>();
            Promotion Promotion = new Promotion();
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            try
            {
                _Parametros.Add(new SqlParameter("@Id", id));

                sql.PrepararProcedimiento("dbo.[PROMOTION.GetJSONById]", _Parametros);
                DataTableReader dtr = sql.EjecutarTableReader(CommandType.StoredProcedure);
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        var Json = dtr["Promotion"].ToString();
                        if (Json != string.Empty)
                        {
                            JArray arr = JArray.Parse(Json);
                            foreach (JObject jsonOperaciones in arr.Children<JObject>())
                            {
                                //Promotion = JsonConvert.DeserializeObject<Promotion>(jsonOperaciones);
                                list.Add(new Promotion()
                                {
                                    id = Convert.ToInt32(jsonOperaciones["id_"].ToString()),
                                    title = jsonOperaciones["title"].ToString(),
                                    code = jsonOperaciones["code"].ToString(),
                                    description = jsonOperaciones["description"].ToString(),
                                    expires_date = DateTime.Parse(jsonOperaciones["expires_date"].ToString()),
                                    theme = jsonOperaciones["theme"].ToString(),
                                    discount = Convert.ToInt32(jsonOperaciones["discount"].ToString()),
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
        public int InsertPromotion(string code,
                                    string titulo,
                                    string descripcion,
                                    DateTime expires_date,
                                    string theme,
                                    int discount
                                    )
        {
            int IdPromotion = 0;
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            try
            {

                _Parametros.Add(new SqlParameter("@Title", titulo));
                _Parametros.Add(new SqlParameter("@Code", code));
                _Parametros.Add(new SqlParameter("@Description", descripcion));
                _Parametros.Add(new SqlParameter("@Expires_date", expires_date));
                _Parametros.Add(new SqlParameter("@Theme", theme));
                _Parametros.Add(new SqlParameter("@Discount", discount));

                SqlParameter valreg = new SqlParameter();
                valreg.ParameterName = "@Id";
                valreg.DbType = DbType.Int32;
                valreg.Direction = ParameterDirection.Output;
                _Parametros.Add(valreg);

                sql.PrepararProcedimiento("dbo.[Promotion.Insert]", _Parametros);

                IdPromotion = int.Parse(sql.EjecutarProcedimientoOutput().ToString());
                return IdPromotion;
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
        public int UpdatePromotion(int id,
                                    string code,
                                    string titulo,
                                    string descripcion,
                                    DateTime expires_date,
                                    string theme,
                                    int discount
                                    )
        {
            int IdPromotion = 0;
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            try
            {

                _Parametros.Add(new SqlParameter("@Title", titulo));
                _Parametros.Add(new SqlParameter("@Code", code));
                _Parametros.Add(new SqlParameter("@Description", descripcion));
                _Parametros.Add(new SqlParameter("@Expires_date", expires_date));
                _Parametros.Add(new SqlParameter("@Theme", theme));
                _Parametros.Add(new SqlParameter("@Discount", discount));
                _Parametros.Add(new SqlParameter("@Id_", id));


                sql.PrepararProcedimiento("dbo.[PROMOTION.Update]", _Parametros);
                sql.EjecutarProcedimientoOutput();
                IdPromotion = 1;

            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message, sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return IdPromotion;
        }
        public int removePromotion(int id_promotion)
        {

            int result = 0;

            List<SqlParameter> _Parametros = new List<SqlParameter>();

            try
            {

                _Parametros.Add(new SqlParameter("@Id", id_promotion));

                sql.PrepararProcedimiento("[dbo].[PROMOTION.Delete]", _Parametros);
                sql.EjecutarProcedimientoOutput();
                result = 1;

            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message, sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return result;
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
        #endregion
    }
}