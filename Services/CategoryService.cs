using APIUsers.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIUsers.Library.Services{

    using Interfaces;
    using Helpers.Datos;
    using Models;
    using System.Data.SqlClient;
    using System.Data;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class CategoryService : ICategory, IDisposable
    {

        #region Constructor y Variables

        SqlConexion sql = null;
        MySqlConexion mysql = null;
        ConnectionType type = ConnectionType.NONE;

        CategoryService(){

         }

        #endregion

        public List<Category> ObtenerCategorias(){
            List<Category> list = new List<Category>();
            Category Category = new Category();
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            try{
                sql.PrepararProcedimiento("dbo.[CATEGORY.GetAllJSON]", _Parametros);
                DataTableReader dtr = sql.EjecutarTableReader(CommandType.StoredProcedure);
                if (dtr.HasRows){
                    while (dtr.Read()){
                        var Json = dtr["Categoria"].ToString();
                        if (Json != string.Empty)
                        {
                            JArray arr = JArray.Parse(Json);
                            foreach (JObject jsonOperaciones in arr.Children<JObject>())
                            {
                                //user = JsonConvert.DeserializeObject<User>(jsonOperaciones);
                                list.Add(new Category()
                                {
                                    Id = Convert.ToInt32(jsonOperaciones["Id"].ToString()),
                                    Name = jsonOperaciones["Category"].ToString(),
                                    CreateDate = DateTime.Parse(jsonOperaciones["CreateDate"].ToString()),
                                    UpdateDate = DateTime.Parse(jsonOperaciones["UpdateDate"].ToString())

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

        public static CategoryService CrearInstanciaSQL(SqlConexion sql)
        {
            CategoryService log = new CategoryService
            {
                sql = sql,
                type = ConnectionType.MSSQL
            };
            return log;
        }
        public static CategoryService CrearInstanciaMySQL(MySqlConexion mysql)
        {
            CategoryService log = new CategoryService
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

        public List<CategorySelect> obtenerCategoriasSelected(){
            List<CategorySelect> list = new List<CategorySelect>();
            List<SqlParameter> _Parametros = new List<SqlParameter>();

            list.Add(new CategorySelect()
            {
                value = 0,
                label = "Todos",
            });

            try
            {
                sql.PrepararProcedimiento("dbo.[CATEGORY.GetBySelect]", _Parametros);
                DataTableReader dtr = sql.EjecutarTableReader(CommandType.StoredProcedure);
                if (dtr.HasRows){
                    while (dtr.Read()){
                        var Json = dtr["Categoria"].ToString();
                        if (Json != string.Empty){
                            JArray arr = JArray.Parse(Json);

                            foreach (JObject jsonOperaciones in arr.Children<JObject>()){
                                list.Add(new CategorySelect(){
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

        public int InsertCategory(Category category){
            int IdCategory = 0;
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            try
            {
                _Parametros.Add(new SqlParameter("@Name", category.Name));
                SqlParameter valreg = new SqlParameter();
                valreg.ParameterName = "@Id";
                valreg.DbType = DbType.Int32;
                valreg.Direction = ParameterDirection.Output;
                _Parametros.Add(valreg);

                sql.PrepararProcedimiento("dbo.[CATEGORY.Insert]", _Parametros);
                IdCategory = int.Parse(sql.EjecutarProcedimientoOutput().ToString());
                return IdCategory;
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

        public bool DeleteCategory(int id)
        {
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            try
            {
                _Parametros.Add(new SqlParameter("@Id", id));
                sql.PrepararProcedimiento("dbo.[CATEGORY.Delete]", _Parametros);
                sql.EjecutarProcedimiento();
            }
            catch (SqlException sqlEx)
            {
                return false;
                throw new Exception(sqlEx.Message, sqlEx);
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception(ex.Message, ex);
            }
            return true;
        }

        public Boolean UpdateCategory(Category category)
        {
           
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            try
            {
                _Parametros.Add(new SqlParameter("@Name", category.Name));
                _Parametros.Add(new SqlParameter("@Id", category.Id));

                sql.PrepararProcedimiento("dbo.[CATEGORY.Update]", _Parametros);
                sql.EjecutarProcedimiento();
             
            }
            catch (SqlException sqlEx)
            {
                return false;
                throw new Exception(sqlEx.Message, sqlEx);
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception(ex.Message, ex);
            }
            return true;

        }

        public List<Category> GetAllCategoriesBySearch(string search)
        {
            List<Category> list = new List<Category>();
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            try
            {
                _Parametros.Add(new SqlParameter("@Title", search));
                sql.PrepararProcedimiento("dbo.[CATEGORY.GetJSONLike]", _Parametros);
                DataTableReader dtr = sql.EjecutarTableReader(CommandType.StoredProcedure);
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        var Json = dtr["Categoria"].ToString();
                        if (Json != string.Empty)
                        {
                            JArray arr = JArray.Parse(Json);
                            foreach (JObject jsonOperaciones in arr.Children<JObject>()){
                                list.Add(new Category()
                                {
                                    Id = Convert.ToInt32(jsonOperaciones["id_"].ToString()),
                                    Name = jsonOperaciones["name"].ToString(),
                                    CreateDate = DateTime.Parse(jsonOperaciones["create_date"].ToString()),
                                    UpdateDate = DateTime.Parse(jsonOperaciones["update_date"].ToString())

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

        public Category GetCategory(int id)
        {
            Category category = new Category();
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            try
            {
                _Parametros.Add(new SqlParameter("@Id", id));
                sql.PrepararProcedimiento("dbo.[CATEGORY.GetJSONById]", _Parametros);
                DataTableReader dtr = sql.EjecutarTableReader(CommandType.StoredProcedure);
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        var Json = dtr["Categoria"].ToString();
                        if (Json != string.Empty)
                            category = JsonConvert.DeserializeObject<Category>(Json);
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

            return category;
        }
        #endregion

    }
}
