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
                consulta = "SELECT Id_Ajuste,IdAjusteBalanza,SKU,UnidadesConfirmadas,PesoConfirmado,Tar_Codigo,Tra_Fecha,Tra_Estado FROM TRANSACCIONES where SKU ='" + SKU + "'";
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
                using (var Conn = new SqlConnection(CadenaSql.String_Conexion()))
                {
                    Conn.Open();
                    using (var command = new SqlCommand("INSERT INTO dbo.Transacciones(IdAjuste,IdAjusteBalanza,Od_OrdenDespcho,SKU,Producto,UnidadesEstimadas,PesoEstimado,FecIng,Tra_Estado) VALUES(@id_ajuste,@id_ajuste_balanza,@od_orden_despacho,@sku,@producto,@unidades_estimadas,@peso_estimado,GETDATE(),'A')", Conn))
                    {
                        command.Parameters.Add(new SqlParameter("@id_ajuste", IdAjuste));
                        command.Parameters.Add(new SqlParameter("@id_ajuste_balanza", IdAjusteBalanza));
                        command.Parameters.Add(new SqlParameter("@od_orden_despacho", Od_OrdenDespcho));
                        command.Parameters.Add(new SqlParameter("@sku", SKU));
                        command.Parameters.Add(new SqlParameter("@producto", Producto));
                        command.Parameters.Add(new SqlParameter("@unidades_estimadas", Unidades));
                        command.Parameters.Add(new SqlParameter("@peso_estimado", Peso));
                        int rowsAdded = command.ExecuteNonQuery();
                        ConexionSql.Close();
                        if (rowsAdded < 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
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

        public void Insertar_Cabecera(string  IdEmpresa,string  IdEstablecimiento,string IdPuntoOperacion , String IdAjusteBalanza, String CodigoLN, String Tipo_Transaccion, String Od_OrdenDespcho, String Fecha_Ingreso, String Cab_Estado)
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
                        consulta = "no se realizó la consulta";
                    }
                    else
                    {
                        consulta="si se realizó la consulta";
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
        public string Consultar_Orden(string Od_OrdenDespacho)
        {
            using (var Conn = new SqlConnection(CadenaSql.String_Conexion()))
            {
                Conn.Open();
                using (var command = new SqlCommand("SELECT Od_OrdenDespcho FROM Cabecera WHERE Od_OrdenDespcho=@orden", Conn))
                {
                    command.Parameters.Add(new SqlParameter("@orden", Od_OrdenDespacho));
                    string orden = Convert.ToString(command.ExecuteScalar());
                    return orden;
                }
            }
        }






    }
}
