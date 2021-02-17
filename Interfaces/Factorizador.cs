using System;
using System.Collections.Generic;
using System.Text;

namespace APIUsers.Library.Interfaces
{
    using Helpers.Datos;
    using Services;
    public static class Factorizador
    {
        public static IUser CrearConexionServicio(Models.ConnectionType type, string connectionString)
        {
            IUser nuevoMotor = null; ;
            switch (type)
            {
                case Models.ConnectionType.NONE:
                    break;
                case Models.ConnectionType.MSSQL:
                    SqlConexion sql = SqlConexion.Conectar(connectionString);
                    nuevoMotor = UserService.CrearInstanciaSQL(sql);
                    break;
                case Models.ConnectionType.MYSQL:

                    break;
                default:
                    break;
            }

            return nuevoMotor;
        }


        public static ICategory CrearConexionServicioCategory(Models.ConnectionType type, string connectionString)
        {
            ICategory nuevoMotor = null; ;
            switch (type)
            {
                case Models.ConnectionType.NONE:
                    break;
                case Models.ConnectionType.MSSQL:
                    SqlConexion sql = SqlConexion.Conectar(connectionString);
                    nuevoMotor = CategoryService.CrearInstanciaSQL(sql);
                    break;
                case Models.ConnectionType.MYSQL:

                    break;
                default:
                    break;
            }

            return nuevoMotor;
        }


        public static IBrand CrearConexionServicioBrand(Models.ConnectionType type, string connectionString)
        {
            IBrand nuevoMotor = null; ;
            switch (type)
            {
                case Models.ConnectionType.NONE:
                    break;
                case Models.ConnectionType.MSSQL:
                    SqlConexion sql = SqlConexion.Conectar(connectionString);
                    nuevoMotor = BrandService.CrearInstanciaSQL(sql);
                    break;
                case Models.ConnectionType.MYSQL:

                    break;
                default:
                    break;
            }

            return nuevoMotor;
        }


        public static IProduct CrearConexionServicioProduct(Models.ConnectionType type, string connectionString)
        {
            IProduct nuevoMotor = null; ;
            switch (type)
            {
                case Models.ConnectionType.NONE:
                    break;
                case Models.ConnectionType.MSSQL:
                    SqlConexion sql = SqlConexion.Conectar(connectionString);
                    nuevoMotor = ProductService.CrearInstanciaSQL(sql);
                    break;
                case Models.ConnectionType.MYSQL:

                    break;
                default:
                    break;
            }

            return nuevoMotor;
        }


        public static IShoppingCart CrearConexionServicioShoppingCart(Models.ConnectionType type, string connectionString)
        {
            IShoppingCart nuevoMotor = null; ;
            switch (type)
            {
                case Models.ConnectionType.NONE:
                    break;
                case Models.ConnectionType.MSSQL:
                    SqlConexion sql = SqlConexion.Conectar(connectionString);
                    nuevoMotor = ShoppingCartService.CrearInstanciaSQL(sql);
                    break;
                case Models.ConnectionType.MYSQL:

                    break;
                default:
                    break;
            }

            return nuevoMotor;
        }





        public static ILogin CrearConexionServicioLogin(Models.ConnectionType type, string connectionString)
        {
            ILogin nuevoMotor = null; ;
            switch (type)
            {
                case Models.ConnectionType.NONE:
                    break;
                case Models.ConnectionType.MSSQL:
                    SqlConexion sql = SqlConexion.Conectar(connectionString);
                    nuevoMotor = LoginService.CrearInstanciaSQL(sql);
                    break;
                case Models.ConnectionType.MYSQL:

                    break;
                default:
                    break;
            }

            return nuevoMotor;
        }


        public static IWishList CrearConexionServicioWishList(Models.ConnectionType type, string connectionString)
        {
            IWishList nuevoMotor = null; ;
            switch (type)
            {
                case Models.ConnectionType.NONE:
                    break;
                case Models.ConnectionType.MSSQL:
                    SqlConexion sql = SqlConexion.Conectar(connectionString);
                    nuevoMotor = WishListService.CrearInstanciaSQL(sql);
                    break;
                case Models.ConnectionType.MYSQL:

                    break;
                default:
                    break;
            }

            return nuevoMotor;
        }

        public static IPromotion CrearConexionServicio3(Models.ConnectionType type, string connectionString)
        {
            IPromotion nuevoMotor = null;
            switch (type)
            {
                case Models.ConnectionType.NONE:
                    break;
                case Models.ConnectionType.MSSQL:
                    SqlConexion sql = SqlConexion.Conectar(connectionString);
                    nuevoMotor = PromotionService.CrearInstanciaSQL(sql);
                    break;
                case Models.ConnectionType.MYSQL:

                    break;
                default:
                    break;
            }

            return nuevoMotor;
        }


        public static ICheckout CrearConexionServicio2(Models.ConnectionType type, string connectionString)
        {
            ICheckout nuevoMotor2 = null;
            switch (type)
            {
                case Models.ConnectionType.NONE:
                    break;
                case Models.ConnectionType.MSSQL:
                    SqlConexion sql = SqlConexion.Conectar(connectionString);
                    nuevoMotor2 = CheckoutService.CrearInstanciaSQL(sql);
                    break;
                case Models.ConnectionType.MYSQL:

                    break;
                default:
                    break;
            }

            return nuevoMotor2;
        }


    }



}
