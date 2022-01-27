using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
  public  class Datos_Transacciones
    {
        SqlConnection ConexionSql = null;
        SqlCommand ComandoSql = null;
        String query = null;
        SqlDataReader LectorDatos = null;
        SqlDataAdapter AdaptadorSql = null;
        DataSet DatoAlmacenado = null;
        private Conexion CadenaSql = new Conexion();


        public DataSet Consulta_Pesajes(string SKU, string IdAjusteBalanza)
        {
            DataSet Dato_Almacenado = new DataSet();
            string consulta;
            try
            {
                consulta = "SELECT CodigoLN,Id_Ajuste,IdAjusteBalanza,SKU,UnidadesConfirmadas,PesoConfirmado,Tar_Codigo,Tra_Fecha,Tra_Estado FROM TRANSACCIONES where CodigoLN ='" + IdAjusteBalanza + "'";
                using (ConexionSql = new SqlConnection(CadenaSql.String_Conexion()))
                {
                    ConexionSql.Open();
                    SqlCommand Comando_Sql = new SqlCommand(consulta, ConexionSql);
                    SqlDataAdapter Adaptador_Sql = new SqlDataAdapter(Comando_Sql);
                    Adaptador_Sql.Fill(Dato_Almacenado);
                    ConexionSql.Close();
                }
            }
            finally
            {
                if (ConexionSql != null && ConexionSql.State != ConnectionState.Closed)
                {
                    ConexionSql.Close();
                }
            }
            return Dato_Almacenado;
        }


        public bool Insertar_Pesaje(  string IdAjuste, string Od_OrdenDespcho, string IdAjusteBalanza, string SKU, string Producto, int Unidades, float Peso)
        {
          try 
          {


                using (ConexionSql = new SqlConnection(CadenaSql.String_Conexion()))
                {
                    string consulta = "INSERT INTO dbo.Transacciones(IdAjuste,Od_OrdenDespcho,IdAjusteBalanza,SKU,Producto,Unidades,Peso,FecIng,Tra_Estado )VALUES('" + IdAjuste + "','" + Od_OrdenDespcho + "','"+ IdAjusteBalanza  + "','" + SKU + "','" + Producto + "','" + Unidades + "','" + Peso + "',getdate(),'A')";
                    ConexionSql.Open();
                    SqlCommand Comando_Sql = new SqlCommand(consulta, ConexionSql);
                    var res = Comando_Sql.ExecuteNonQuery();  

                    ConexionSql.Close();
                     if(res < 0)
                     {
                          return true;
                     }
                     else
                     {
                        return false;
                     }
                }

            }
            finally
            {
                if (ConexionSql != null && ConexionSql.State != ConnectionState.Closed)
                {
                    ConexionSql.Close();
                }
            
            }   
        }


      ///--------------Insertamos la cabecera

        public bool Insertar_Cabera(string  IdEmpresa,string  IdEstablecimiento,string IdPuntoOperacion , String IdAjusteBalanza, String CodigoLN, String Tipo_Transaccion, String Od_OrdenDespcho, String Fecha_Ingreso, String Cab_Estado)
        {
            try
            {


                using (ConexionSql = new SqlConnection(CadenaSql.String_Conexion()))
                {
                    string consulta = "INSERT INTO dbo.Cabecera(IdEmpresa,IdEstablecimiento,IdPuntoOperacion,IdAjusteBalanza,CodigoLN,Tipo_Transaccion,Od_OrdenDespcho,Fecha_Ingreso,Cab_Estado)values( '" + IdEmpresa + "','" + IdEstablecimiento + "','" + IdPuntoOperacion + "','" + IdAjusteBalanza + "','" + CodigoLN + "','" + Tipo_Transaccion + "','" + Od_OrdenDespcho + "','" + Fecha_Ingreso + "','" + Cab_Estado + "')";
                    ConexionSql.Open();
                    SqlCommand Comando_Sql = new SqlCommand(consulta, ConexionSql);
                    var res = Comando_Sql.ExecuteNonQuery();

                    ConexionSql.Close();
                    if (res < 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

            }
            finally
            {
                if (ConexionSql != null && ConexionSql.State != ConnectionState.Closed)
                {
                    ConexionSql.Close();
                }

            }
        }







    }
}
