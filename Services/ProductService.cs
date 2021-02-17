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

    public class ProductService : IProduct, IDisposable{

        #region Constructor y Variables

        SqlConexion sql = null;
        MySqlConexion mysql = null;
        ConnectionType type = ConnectionType.NONE;

        ProductService(){

        }
        #endregion

        public Product GetProduct(int id){
            Product product = new Product();
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            try
            {
                _Parametros.Add(new SqlParameter("@Id",id));
                sql.PrepararProcedimiento("dbo.[PRODUCT.GetJSONById]", _Parametros);
                DataTableReader dtr = sql.EjecutarTableReader(CommandType.StoredProcedure);
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        var Json = dtr["Producto"].ToString();
                        if (Json != string.Empty)
                            product = JsonConvert.DeserializeObject<Product>(Json);
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

            return product;
        }

        public List<Product> GetAllProducts(){
            List<Product> list = new List<Product>();
            Product Category = new Product();
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            try{
                sql.PrepararProcedimiento("dbo.[PRODUCT.GetAllJSON]", _Parametros);
                DataTableReader dtr = sql.EjecutarTableReader(CommandType.StoredProcedure);
                if (dtr.HasRows) {
                    while (dtr.Read()){
                        var Json = dtr["Producto"].ToString();
                        if (Json != string.Empty){
                            JArray arr = JArray.Parse(Json);
                            foreach (JObject jsonOperaciones in arr.Children<JObject>()){
                                list.Add(new Product() {
                                    Id = Convert.ToInt32(jsonOperaciones["id_"].ToString()),
                                    Title = jsonOperaciones["title"].ToString(),
                                    Sku = jsonOperaciones["sku"].ToString(),
                                    Description = jsonOperaciones["description"].ToString(),
                                    CreateDate = DateTime.Parse(jsonOperaciones["create_date"].ToString()),
                                    Brand = jsonOperaciones["brand"].ToString(),
                                    UpdateDate = DateTime.Parse(jsonOperaciones["update_date"].ToString()),
                                    IdCategory = Convert.ToInt32(jsonOperaciones["id_category"].ToString()),
                                    Ranking = Convert.ToInt32(jsonOperaciones["ranking"].ToString()),
                                    Price = Convert.ToDouble(jsonOperaciones["price"].ToString()),
                                    SeelingPrice = Convert.ToDouble(jsonOperaciones["seeling_price"].ToString()),
                                    Status = jsonOperaciones["status"].ToString(),
                                    Imagen = jsonOperaciones["image"].ToString(),
                                    Category = jsonOperaciones["category"].ToString()
                                }) ;
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

        public List<Product> GetAllProductsByParams(Models.Parametros parametros)
        {
            List<Product> list = new List<Product>();
            Product Category = new Product();
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            try
            {
                if(parametros.Category != null)
                    _Parametros.Add(new SqlParameter("@Category",parametros.Category));
                if (parametros.Brand != null)
                    _Parametros.Add(new SqlParameter("@Brand", parametros.Brand));
                if (parametros.ShortBy != null)
                    _Parametros.Add(new SqlParameter("@ShortBy", parametros.ShortBy));
                if (parametros.ShortByDirection != null)
                    _Parametros.Add(new SqlParameter("@ShortByDirection", parametros.ShortByDirection));
                if (parametros.MinValor != null)
                    _Parametros.Add(new SqlParameter("@MinValor", parametros.MinValor));
                if (parametros.MaxValor > 0)
                    _Parametros.Add(new SqlParameter("@MaxValor", parametros.MaxValor));

                sql.PrepararProcedimiento("dbo.[PRODUCT.GetByParams]", _Parametros);
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
                                list.Add(new Product()
                                {
                                    Id = Convert.ToInt32(jsonOperaciones["id_"].ToString()),
                                    Title = jsonOperaciones["title"].ToString(),
                                    Sku = jsonOperaciones["sku"].ToString(),
                                    Description = jsonOperaciones["description"].ToString(),
                                    CreateDate = DateTime.Parse(jsonOperaciones["create_date"].ToString()),
                                    Brand = jsonOperaciones["brand"].ToString(),
                                    UpdateDate = DateTime.Parse(jsonOperaciones["update_date"].ToString()),
                                    IdCategory = Convert.ToInt32(jsonOperaciones["id_category"].ToString()),
                                    Ranking = Convert.ToInt32(jsonOperaciones["ranking"].ToString()),
                                    Price = Convert.ToDouble(jsonOperaciones["price"].ToString()),
                                    SeelingPrice = Convert.ToDouble(jsonOperaciones["seeling_price"].ToString()),
                                    Status = jsonOperaciones["status"].ToString(),
                                    Imagen = jsonOperaciones["image"].ToString(),
                                    Category = jsonOperaciones["category"].ToString()
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


        public List<Product> GetAllProductsByCategory(string category) {
            List<Product> list = new List<Product>();
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            try{
                _Parametros.Add(new SqlParameter("@Category",category));
                sql.PrepararProcedimiento("dbo.[PRODUCT.GetJSONByCategory]", _Parametros);
                DataTableReader dtr = sql.EjecutarTableReader(CommandType.StoredProcedure);
                if (dtr.HasRows){
                    while (dtr.Read()){
                        var Json = dtr["Producto"].ToString();
                        if (Json != string.Empty){
                            JArray arr = JArray.Parse(Json);
                            foreach (JObject jsonOperaciones in arr.Children<JObject>()){
                                list.Add(new Product()
                                {
                                    Id = Convert.ToInt32(jsonOperaciones["id_"].ToString()),
                                    Title = jsonOperaciones["title"].ToString(),
                                    Sku = jsonOperaciones["sku"].ToString(),
                                    Description = jsonOperaciones["description"].ToString(),
                                    CreateDate = DateTime.Parse(jsonOperaciones["create_date"].ToString()),
                                    Brand = jsonOperaciones["brand"].ToString(),
                                    UpdateDate = DateTime.Parse(jsonOperaciones["update_date"].ToString()),
                                    IdCategory = Convert.ToInt32(jsonOperaciones["id_category"].ToString()),
                                    Ranking = Convert.ToInt32(jsonOperaciones["ranking"].ToString()),
                                    Price = Convert.ToDouble(jsonOperaciones["price"].ToString()),
                                    SeelingPrice = Convert.ToDouble(jsonOperaciones["seeling_price"].ToString()),
                                    Status = jsonOperaciones["status"].ToString(),
                                    Imagen = jsonOperaciones["image"].ToString(),
                                    Category = jsonOperaciones["category"].ToString()
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

        public List<Product> GetAllProductsByBrand(string brand)
        {
            List<Product> list = new List<Product>();
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            try
            {
                _Parametros.Add(new SqlParameter("@Brand", brand));
                sql.PrepararProcedimiento("dbo.[PRODUCT.GetJSONByBrand]", _Parametros);
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
                                list.Add(new Product()
                                {
                                    Id = Convert.ToInt32(jsonOperaciones["id_"].ToString()),
                                    Title = jsonOperaciones["title"].ToString(),
                                    Sku = jsonOperaciones["sku"].ToString(),
                                    Description = jsonOperaciones["description"].ToString(),
                                    CreateDate = DateTime.Parse(jsonOperaciones["create_date"].ToString()),
                                    Brand = jsonOperaciones["brand"].ToString(),
                                    UpdateDate = DateTime.Parse(jsonOperaciones["update_date"].ToString()),
                                    IdCategory = Convert.ToInt32(jsonOperaciones["id_category"].ToString()),
                                    Ranking = Convert.ToInt32(jsonOperaciones["ranking"].ToString()),
                                    Price = Convert.ToDouble(jsonOperaciones["price"].ToString()),
                                    SeelingPrice = Convert.ToDouble(jsonOperaciones["seeling_price"].ToString()),
                                    Status = jsonOperaciones["status"].ToString(),
                                    Imagen = jsonOperaciones["image"].ToString(),
                                    Category = jsonOperaciones["category"].ToString()
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

        public List<Product> GetTop3NewProducts()
        {
            List<Product> list = new List<Product>();
            Product Category = new Product();
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            try
            {
                sql.PrepararProcedimiento("dbo.[PRODUCT.GetTop3NewProducts]", _Parametros);
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
                                list.Add(new Product()
                                {
                                    Id = Convert.ToInt32(jsonOperaciones["id_"].ToString()),
                                    Title = jsonOperaciones["title"].ToString(),
                                    Sku = jsonOperaciones["sku"].ToString(),
                                    Description = jsonOperaciones["description"].ToString(),
                                    CreateDate = DateTime.Parse(jsonOperaciones["create_date"].ToString()),
                                    Brand = jsonOperaciones["brand"].ToString(),
                                    UpdateDate = DateTime.Parse(jsonOperaciones["update_date"].ToString()),
                                    IdCategory = Convert.ToInt32(jsonOperaciones["id_category"].ToString()),
                                    Ranking = Convert.ToInt32(jsonOperaciones["ranking"].ToString()),
                                    Price = Convert.ToDouble(jsonOperaciones["price"].ToString()),
                                    SeelingPrice = Convert.ToDouble(jsonOperaciones["seeling_price"].ToString()),
                                    Status = jsonOperaciones["status"].ToString(),
                                    Imagen = jsonOperaciones["image"].ToString(),
                                    Category = jsonOperaciones["category"].ToString()
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
        public List<Product> GetTop3Random()
        {
            List<Product> list = new List<Product>();
            Product Category = new Product();
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            try
            {
                sql.PrepararProcedimiento("dbo.[PRODUCT.GetTop3Random]", _Parametros);
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
                                list.Add(new Product()
                                {
                                    Id = Convert.ToInt32(jsonOperaciones["id_"].ToString()),
                                    Title = jsonOperaciones["title"].ToString(),
                                    Sku = jsonOperaciones["sku"].ToString(),
                                    Description = jsonOperaciones["description"].ToString(),
                                    CreateDate = DateTime.Parse(jsonOperaciones["create_date"].ToString()),
                                    Brand = jsonOperaciones["brand"].ToString(),
                                    UpdateDate = DateTime.Parse(jsonOperaciones["update_date"].ToString()),
                                    IdCategory = Convert.ToInt32(jsonOperaciones["id_category"].ToString()),
                                    Ranking = Convert.ToInt32(jsonOperaciones["ranking"].ToString()),
                                    Price = Convert.ToDouble(jsonOperaciones["price"].ToString()),
                                    SeelingPrice = Convert.ToDouble(jsonOperaciones["seeling_price"].ToString()),
                                    Status = jsonOperaciones["status"].ToString(),
                                    Imagen = jsonOperaciones["image"].ToString(),
                                    Category = jsonOperaciones["category"].ToString()
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
        public List<Product> GetTop3ByCategory(string category, int id)
        {
            List<Product> list = new List<Product>();
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            try
            {
                _Parametros.Add(new SqlParameter("@Category", category));
                _Parametros.Add(new SqlParameter("@Id", id));
                sql.PrepararProcedimiento("dbo.[PRODUCT.GetTop3RandomByCategory]", _Parametros);
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
                                list.Add(new Product()
                                {
                                    Id = Convert.ToInt32(jsonOperaciones["id_"].ToString()),
                                    Title = jsonOperaciones["title"].ToString(),
                                    Sku = jsonOperaciones["sku"].ToString(),
                                    Description = jsonOperaciones["description"].ToString(),
                                    CreateDate = DateTime.Parse(jsonOperaciones["create_date"].ToString()),
                                    Brand = jsonOperaciones["brand"].ToString(),
                                    UpdateDate = DateTime.Parse(jsonOperaciones["update_date"].ToString()),
                                    IdCategory = Convert.ToInt32(jsonOperaciones["id_category"].ToString()),
                                    Ranking = Convert.ToInt32(jsonOperaciones["ranking"].ToString()),
                                    Price = Convert.ToDouble(jsonOperaciones["price"].ToString()),
                                    SeelingPrice = Convert.ToDouble(jsonOperaciones["seeling_price"].ToString()),
                                    Status = jsonOperaciones["status"].ToString(),
                                    Imagen = jsonOperaciones["image"].ToString(),
                                    Category = jsonOperaciones["category"].ToString()
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


        public List<Product> GetAllProductsBySearch(string search){
            List<Product> list = new List<Product>();
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            try{
                _Parametros.Add(new SqlParameter("@Title", search));
                sql.PrepararProcedimiento("dbo.[PRODUCT.GetJSONLike]", _Parametros);
                DataTableReader dtr = sql.EjecutarTableReader(CommandType.StoredProcedure);
                if (dtr.HasRows){
                    while (dtr.Read()){
                        var Json = dtr["Producto"].ToString();
                        if (Json != string.Empty){
                            JArray arr = JArray.Parse(Json);
                            foreach (JObject jsonOperaciones in arr.Children<JObject>())
                            {
                                list.Add(new Product()
                                {
                                    Id = Convert.ToInt32(jsonOperaciones["id_"].ToString()),
                                    Title = jsonOperaciones["title"].ToString(),
                                    Sku = jsonOperaciones["sku"].ToString(),
                                    Description = jsonOperaciones["description"].ToString(),
                                    CreateDate = DateTime.Parse(jsonOperaciones["create_date"].ToString()),
                                    Brand = jsonOperaciones["brand"].ToString(),
                                    UpdateDate = DateTime.Parse(jsonOperaciones["update_date"].ToString()),
                                    IdCategory = Convert.ToInt32(jsonOperaciones["id_category"].ToString()),
                                    Ranking = Convert.ToInt32(jsonOperaciones["ranking"].ToString()),
                                    Price = Convert.ToDouble(jsonOperaciones["price"].ToString()),
                                    SeelingPrice = Convert.ToDouble(jsonOperaciones["seeling_price"].ToString()),
                                    Status = jsonOperaciones["status"].ToString(),
                                    Imagen = jsonOperaciones["image"].ToString(),
                                    Category = jsonOperaciones["category"].ToString()
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


        public int InsertProduct(Models.Product product)
        {
            int IdUser = 0;
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            try{
                _Parametros.Add(new SqlParameter("@Title", product.Title));
                _Parametros.Add(new SqlParameter("@SKU", product.Sku));
                _Parametros.Add(new SqlParameter("@Description", product.Description));
                _Parametros.Add(new SqlParameter("@Brand", product.Brand));
                _Parametros.Add(new SqlParameter("@Ranking",product.Ranking));
                _Parametros.Add(new SqlParameter("@Price",product.Price));
                _Parametros.Add(new SqlParameter("@SeelingPrice",product.SeelingPrice));
                _Parametros.Add(new SqlParameter("@Status",product.Status));
                _Parametros.Add(new SqlParameter("@Image",product.Imagen));
                _Parametros.Add(new SqlParameter("@IdCategory",product.IdCategory));

                SqlParameter valreg = new SqlParameter();
                valreg.ParameterName = "@Id";
                valreg.DbType = DbType.Int32;
                valreg.Direction = ParameterDirection.Output;
                _Parametros.Add(valreg);

                sql.PrepararProcedimiento("dbo.[Product.Insert]", _Parametros);
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

        public bool UpdateProduct(Product product){
           
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            try{
                _Parametros.Add(new SqlParameter("@Title", product.Title));
                _Parametros.Add(new SqlParameter("@SKU", product.Sku));
                _Parametros.Add(new SqlParameter("@Description", product.Description));
                _Parametros.Add(new SqlParameter("@Brand", product.Brand));
                _Parametros.Add(new SqlParameter("@Ranking", product.Ranking));
                _Parametros.Add(new SqlParameter("@Price", product.Price));
                _Parametros.Add(new SqlParameter("@SeelingPrice", product.SeelingPrice));
                _Parametros.Add(new SqlParameter("@Status", product.Status));
                _Parametros.Add(new SqlParameter("@Image", product.Imagen));
                _Parametros.Add(new SqlParameter("@IdCategory", product.IdCategory));
                _Parametros.Add(new SqlParameter("@Id", product.Id));
                sql.PrepararProcedimiento("dbo.[PRODUCT.Update]", _Parametros);
                sql.EjecutarProcedimiento();
                return true;
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

        }


        public Boolean DeleteProduct(int id){
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            try
            {
                _Parametros.Add(new SqlParameter("@Id", id));
                sql.PrepararProcedimiento("dbo.[Product.Delete]", _Parametros);
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


        public static ProductService CrearInstanciaSQL(SqlConexion sql)
        {
            ProductService log = new ProductService{
                sql = sql,
                type = ConnectionType.MSSQL
            };
            return log;
        }

        public static ProductService CrearInstanciaMySQL(MySqlConexion mysql){
            ProductService log = new ProductService{
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


    }
}