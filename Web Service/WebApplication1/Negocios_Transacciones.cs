using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using System.Data;
namespace Negocios
{
    public class Negocios_Transacciones
    {
        Datos_Transacciones Datos_Pesaje = new Datos_Transacciones();

        public DataSet Consulta_Pesajes(string SKU, string IdAjusteBalanza)
        {

            return Datos_Pesaje.Consulta_Pesajes(SKU, IdAjusteBalanza);
        }
        public bool Insertar_Pesaje(string IdAjuste,string Od_OrdenDespcho, string IdAjusteBalanza, string SKU, string Producto, int Unidades, float Peso)
        {
            return Datos_Pesaje.Insertar_Pesaje(IdAjuste, Od_OrdenDespcho, IdAjusteBalanza, SKU, Producto, Unidades, Peso); 
        }

        public void Insertar_Cabecera(string  IdEmpresa,string  IdEstablecimiento,string IdPuntoOperacion, String IdAjusteBalanza, String CodigoLN, String Tipo_Transaccion, String Od_OrdenDespcho, String Fecha_Ingreso, String Cab_Estado)
        {
             Datos_Pesaje.Insertar_Cabecera(IdEmpresa, IdEstablecimiento, IdPuntoOperacion, IdAjusteBalanza, CodigoLN, Tipo_Transaccion, Od_OrdenDespcho, Fecha_Ingreso, Cab_Estado);      
        }
        public string ConsutarOrden(string Od_OrdenDespacho)
        {
            return Datos_Pesaje.Consultar_Orden(Od_OrdenDespacho);
        }
    }
}
