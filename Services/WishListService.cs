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
    public class WishListService : IWishList, IDisposable
    {
        SqlConexion sql = null;
        MySqlConexion mysql = null;
        ConnectionType type = ConnectionType.NONE;

        WishListService()
        {

        }


        public static WishListService CrearInstanciaSQL(SqlConexion sql)
        {
            WishListService log = new WishListService
            {
                sql = sql,
                type = ConnectionType.MSSQL
            };
            return log;
        }

        public static WishListService CrearInstanciaMySQL(MySqlConexion mysql)
        {
            WishListService log = new WishListService
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

        // TODO: reemplace un finalizador solo si el anterior Dispose(bool disposing) tiene código para liberar los recursos no administrados.
        // ~HidraService()
        // {
        // // No cambie este código. Coloque el código de limpieza en el anterior Dispose(colocación de bool).
        // Dispose(false);
        // }

        // Este código se agrega para implementar correctamente el patrón descartable.
        public void Dispose()
        {
            // No cambie este código. Coloque el código de limpieza en el anterior Dispose(colocación de bool).
            Dispose(true);
            // TODO: quite la marca de comentario de la siguiente línea si el finalizador se ha reemplazado antes.
            // GC.SuppressFinalize(this);
        }

        public int addToWishList(int id_user, int id_producto){
            int ID_Return = 0;

            List<SqlParameter> _Parametros = new List<SqlParameter>();

            try{
                _Parametros.Add(new SqlParameter("@Id_User", id_user));
                _Parametros.Add(new SqlParameter("@Id_Product", id_producto));
                SqlParameter valreg = new SqlParameter();
                valreg.ParameterName = "@ID_WishList";
                valreg.DbType = DbType.Int32;
                valreg.Direction = ParameterDirection.Output;
                _Parametros.Add(valreg);
                sql.PrepararProcedimiento("[dbo].[WishList.Insert]", _Parametros);
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

        public List<Product> getProducts_Wishlist(int id_user)
        {
            List<Product> list = new List<Product>();
            Product product = new Product();
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            try
            {
                SqlConnection conn = new SqlConnection("Server=tcp:serversqlmtwdm.database.windows.net,1433;Initial Catalog=dbazure;Persist Security Info=False;User ID=userazuresql;Password=norieemm03059@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT "
                    + "[Product].[id_] as ID, [Product].[title] as Product, [Product].[seeling_price] as Price, [Product].[image] as Picture "
                    + "FROM [dbo].[WishList_Detail] "
                    + "INNER Join[dbo].[WishList] on [WishList].[ID] = [WishList_Detail].[ID_Wishlist] "
                    + "INNER Join [dbo].[Product] ON [Product].[id_] = [WishList_Detail].[ID_Product] "
                    + "Where [WishList].ID_User = " + id_user, conn);

                using (SqlDataReader reader = command.ExecuteReader()){
                    if (reader.HasRows)
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        int numRows = dt.Rows.Count;

                        foreach (DataRow dr in dt.Rows)
                        {
                            list.Add(new Product()
                            {
                                Id = Convert.ToInt32(dr["ID"]),
                                Title = dr["Product"].ToString(),
                                SeelingPrice = float.Parse(dr["Price"].ToString()),
                                Imagen = dr["Picture"].ToString()
                            });
                        }

                    }
                }

                conn.Close();
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

        public List<Product> getProducts_Wishlist_ByOrder(int id, string order)
        {
            List<Product> list = new List<Product>();
            Product product = new Product();
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            try
            {
                SqlConnection conn = new SqlConnection("Server=tcp:serversqlmtwdm.database.windows.net,1433;Initial Catalog=dbazure;Persist Security Info=False;User ID=userazuresql;Password=norieemm03059@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
                conn.Open();

                string selectQuery = "SELECT "
                    + "[Product].[id_] as ID, [Product].[title] as Product, [Product].[seeling_price] as Price, [Product].[image] as Picture, [WishList_Detail].[Add_Date] as Date "
                    + "FROM [dbo].[WishList_Detail]"
                    + "INNER Join [dbo].[WishList] on [WishList].[ID] = [WishList_Detail].[ID_Wishlist]"
                    + "Inner Join [dbo].[Product] on [Product].[id_] = [WishList_Detail].[ID_Product] ";

             
                SqlCommand command;

                switch (order)
                {
                    case "alfabetico":
                        command = new SqlCommand(selectQuery + "Where [WishList].ID_User = " + id + " ORDER BY [Product].[title] ASC", conn);
                        break;
                    case "alfabetico_invertido":
                        command = new SqlCommand(selectQuery + "Where [WishList].ID_User = " + id + " ORDER BY [Product].[title] DESC", conn);
                        break;
                    case "precio_bajo":
                        command = new SqlCommand(selectQuery + "Where [WishList].ID_User = " + id + " ORDER BY [Product].[seeling_price] ASC", conn);
                        break;
                    case "precio_alto":
                        command = new SqlCommand(selectQuery + "Where [WishList].ID_User = " + id + " ORDER BY [Product].[seeling_price] DESC", conn);
                        break;
                    case "recientes":
                        command = new SqlCommand(selectQuery + "Where [WishList].ID_User = " + id + " ORDER BY [WishList_Detail].[Add_Date] DESC", conn);
                        break;
                    case "antiguos":
                        command = new SqlCommand(selectQuery + "Where [WishList].ID_User = " + id + " ORDER BY [WishList_Detail].[Add_Date] ASC", conn);
                        break;
                    default:
                        command = new SqlCommand(selectQuery + "Where [WishList].ID_User = " + id + " ORDER BY [Product].[title] ASC", conn);
                        break;
                }

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        int numRows = dt.Rows.Count;

                        foreach (DataRow dr in dt.Rows)
                        {
                            list.Add(new Product()
                            {
                                Id = Convert.ToInt32(dr["ID"]),
                                Title = dr["Product"].ToString(),
                                SeelingPrice = float.Parse(dr["Price"].ToString()),
                                Imagen = dr["Picture"].ToString()
                            });
                        }

                    }
                }

                conn.Close();
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
    

        public int removeFromWishList(int id_user, int id_producto){
            int ID_Return = 0;

            List<SqlParameter> _Parametros = new List<SqlParameter>();

            try{
                _Parametros.Add(new SqlParameter("@Id_User", id_user));
                _Parametros.Add(new SqlParameter("@Id_Product", id_producto));
                SqlParameter valreg = new SqlParameter();
                valreg.ParameterName = "@ID_WishList";
                valreg.DbType = DbType.Int32;
                valreg.Direction = ParameterDirection.Output;
                _Parametros.Add(valreg);
                sql.PrepararProcedimiento("[dbo].[WishList.Delete]", _Parametros);
                ID_Return = int.Parse(sql.EjecutarProcedimientoOutput().ToString());
                return ID_Return;
            }
            catch (SqlException sqlEx){
                throw new Exception(sqlEx.Message, sqlEx);
            }
            catch (Exception ex)
           {
                throw new Exception(ex.Message, ex);
            }
        }

        public List<Product> searchProducts_Wishlist(string busqueda, int id)
        {
            List<Product> list = new List<Product>();
            try
            {
                SqlConnection conn = new SqlConnection("Server=tcp:serversqlmtwdm.database.windows.net,1433;Initial Catalog=dbazure;Persist Security Info=False;User ID=userazuresql;Password=norieemm03059@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT "
                    + "[Product].[id_] as ID, [Product].[title] as Product, [Product].[seeling_price] as Price, [Product].[image] as Picture "
                    + "FROM [dbo].[WishList_Detail] "
                    + "INNER Join[dbo].[WishList] on [WishList].[ID] = [WishList_Detail].[ID_Wishlist] "
                    + "INNER Join [dbo].[Product] ON [Product].[id_] = [WishList_Detail].[ID_Product] "
                    + "Where [Product].[title] LIKE '%" + busqueda + "%' AND [WishList].[ID_User] = " + id, conn);


                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        int numRows = dt.Rows.Count;



                        foreach (DataRow dr in dt.Rows)
                        {
                            list.Add(new Product()
                            {
                                Id = Convert.ToInt32(dr["ID"]),
                                Title = dr["Product"].ToString(),
                                SeelingPrice = float.Parse(dr["Price"].ToString()),
                                Imagen = dr["Picture"].ToString()
                            });
                        }



                    }
                }



                conn.Close();
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
        #endregion


    }
}
