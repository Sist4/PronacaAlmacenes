using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using Negocios;

namespace WebService
{
    /// <summary>
    /// Descripción breve de WebService1
    /// </summary>
    [WebService(Namespace = "http://ln.gesalm.integracion.pronaca.com.ec")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]

    public class WebService1 : System.Web.Services.WebService
    {
        Negocios_Transacciones Transacciones = new Negocios_Transacciones();


        [WebMethod]
        public string InsertItems(string dato)
        {



            //*********************************************************************************
            
            string IdAjusteBalanza;
            string CodigoLN;
            string Tipo_Transaccion;
            string Od_OrdenDespcho;
            string Fecha_Ingreso;
            string IdEstablecimiento;
            string IdPuntoOperacion;
            string IdEmpresa;

            string test = dato.Replace("mrm:", "");
            test = test.Replace("xmlns:mrm=http://ln.gesalm.integracion.pronaca.com.ec", "");
            test = test.Replace(@"xmlns:mrm=""http://ln.gesalm.integracion.pronaca.com.ec""", "");

            test = test.Replace("< GesImpSKU   >", "<GesImpSKU>") ;
            test = test.Replace("< GesImpSKU >", "<GesImpSKU>");




            XmlDocument xmltest = new XmlDocument();
            //xmltest.ReadNode("");
            xmltest.LoadXml(test);




            string xmlCavecera = @"<mrm:GesImpSKU xmlns:mrm=""http://ln.gesalm.integracion.pronaca.com.ec"">";
            string respuesta_recibida = ""; 
            string xmlRes = "";
         
            //EMPEZAMOS A leer la CABECERA 


            //Elementos de la cabecera 
            //XmlNodeList Cab1 = xmltest.GetElementsByTagName("IdAjuste");
            XmlNodeList Cab2 = xmltest.GetElementsByTagName("IdAjusteBalanza");
            XmlNodeList Cab3 = xmltest.GetElementsByTagName("CodigoLN");
            XmlNodeList Cab4 = xmltest.GetElementsByTagName("Tipo_Transaccion");
            XmlNodeList Cab5 = xmltest.GetElementsByTagName("Od_OrdenDespcho");
            XmlNodeList Cab6 = xmltest.GetElementsByTagName("Fecha_Ingreso");
            XmlNodeList Cab7 = xmltest.GetElementsByTagName("IdEstablecimiento");
            XmlNodeList Cab8 = xmltest.GetElementsByTagName("IdPuntoOperacion");
            XmlNodeList Cab9 = xmltest.GetElementsByTagName("IdEmpresa");





            //LEEMOS LOS ITEMS UNO X UNO
            xmlRes += "<Cabecera>";

            for (int i = 0; i < Cab2.Count; i++)
            {

                //IdAjuste = Cab[i].InnerXml;
                IdAjusteBalanza = Cab2[i].InnerXml;
                CodigoLN = Cab3[i].InnerXml;
                Tipo_Transaccion = Cab4[i].InnerXml;
                Od_OrdenDespcho = Cab5[i].InnerXml;
                Fecha_Ingreso = Cab6[i].InnerXml;
                IdEstablecimiento = Cab7[i].InnerXml;
                IdPuntoOperacion = Cab8[i].InnerXml;
                IdEmpresa = Cab9[i].InnerXml;


                try
                {

                    bool Respuesta = Transacciones.Insertar_Cabera(IdEmpresa, IdEstablecimiento, IdPuntoOperacion, IdAjusteBalanza, CodigoLN, Tipo_Transaccion, Od_OrdenDespcho, Fecha_Ingreso, "A");
                    //  xmlRes += "<ItemsCabecera>";
                    respuesta_recibida = "EXITO";

                    //xmlRes += "<IdAjuste>" + IdAjuste + "</IdAjuste>";
                    xmlRes += "<IdAjusteBalanza>" + IdAjusteBalanza + "</IdAjusteBalanza>";
                    xmlRes += "<CodigoLN>" + CodigoLN + "</CodigoLN>";
                    xmlRes += "<Tipo_Transaccion>" + Tipo_Transaccion + "</Tipo_Transaccion>";
                    xmlRes += "<Od_OrdenDespcho>" + Od_OrdenDespcho + "</Od_OrdenDespcho>";
                    xmlRes += "<Fecha_Ingreso>" + Fecha_Ingreso + "</Fecha_Ingreso>";
                    xmlRes += "<Fase>1</Fase>";
                    xmlRes += "<Estado>3</Estado>";//si no hubo ningun incoveniente el estado va quedar en 3
                    xmlRes += "<Error></Error>";
                    xmlRes += "<FechaProceso>" + DateTime.Now.ToString("yyyy-MM-dd")  + "</FechaProceso>";
                    //  xmlRes += "</ItemsCabecera>";


                }
                catch (Exception ex)
                {
                    respuesta_recibida = "ERROR";
                    // xmlRes += "<ItemsCabecera>";
                 //   xmlRes += "<IdAjuste>" + IdAjuste + "</IdAjuste>";
                    xmlRes += "<IdAjusteBalanza>" + IdAjusteBalanza + "</IdAjusteBalanza>";
                    xmlRes += "<CodigoLN>" + CodigoLN + "</CodigoLN>";
                    xmlRes += "<Tipo_Transaccion>" + Tipo_Transaccion + "</Tipo_Transaccion>";
                    xmlRes += "<Od_OrdenDespcho>" + Od_OrdenDespcho + "</Od_OrdenDespcho>";
                    xmlRes += "<Fecha_Ingreso>" + Fecha_Ingreso + "</Fecha_Ingreso>";


                    xmlRes += "<Fase>1</Fase>";
                    xmlRes += "<Estado>2</Estado>";//si no hubo ningun incoveniente el estado va quedar en 3
                    xmlRes += "<Error>" + ex.ToString() + "</Error>";
                    xmlRes += "<FechaProceso>" + DateTime.Now.ToString("yyyy-MM-dd") + "</FechaProceso>";
                    //   xmlRes += "</ItemsCabecera>";

                }

            }

            xmlRes += "</Cabecera>";
            // XmlNodeList elemlist1 = xmltest.GetElementsByTagName("Almacen");
            XmlNodeList elemlist1 = xmltest.GetElementsByTagName("Od_OrdenDespchoLinea");
            XmlNodeList elemlist2 = xmltest.GetElementsByTagName("SKU");
            XmlNodeList elemlist3 = xmltest.GetElementsByTagName("Productos");
            XmlNodeList elemlist4 = xmltest.GetElementsByTagName("UnidadesEstimadas");
            XmlNodeList elemlist5 = xmltest.GetElementsByTagName("PesoEstimado");
            XmlNodeList elemlist6 = xmltest.GetElementsByTagName("IdAjuste");
            XmlNodeList elemlist7 = xmltest.GetElementsByTagName("IdAjusteBalanzaD");


            xmlRes += "<DetallesCabecera>";

            for (int i = 0; i < elemlist1.Count; i++)
            {

                //string Almacen = elemlist1[i].InnerXml;
                string Od_OrdenDespchoLinea = elemlist1[i].InnerXml;
                string IdAjusteBalanzaD = elemlist7[i].InnerXml;


                string IdAjuste = elemlist6[i].InnerXml;

                string SKU = elemlist2[i].InnerXml;
                string Producto = elemlist3[i].InnerXml;
                int UnidadesEstimadas = Convert.ToInt32(elemlist4[i].InnerXml);
                float PesoEstimado = Convert.ToSingle(elemlist5[i].InnerXml);
                try
                {
                    //enviamos datos para q ue se guarde en la tabla transacciones
                    bool Respuesta = Transacciones.Insertar_Pesaje(IdAjuste, Od_OrdenDespchoLinea, IdAjusteBalanzaD, SKU, Producto, UnidadesEstimadas, PesoEstimado);
                    //Si es el dato se gurda en correctamente la respuesta va ser True
                    //Armamos el cuerpo del xml 
                    respuesta_recibida = "EXITO";

                    xmlRes += "<DetalleCabecera>";
                    xmlRes += "<IdAjuste>" + IdAjuste + "</IdAjuste>";
                    xmlRes += "<IdAjusteBalanzaD>" + IdAjusteBalanzaD + "</IdAjusteBalanzaD>";
                    xmlRes += "<Od_OrdenDespchoLinea>" + Od_OrdenDespchoLinea + "</Od_OrdenDespchoLinea>";
                    xmlRes += "<SKU>" + SKU + "</SKU>";
                    xmlRes += "<Productos>" + Producto + "</Productos>";
                    xmlRes += "<UnidadesEstimadas>" + UnidadesEstimadas + "</UnidadesEstimadas>";
                    xmlRes += "<PesoEstimado>" + PesoEstimado + "</PesoEstimado>";
                    xmlRes += "<Fase>1</Fase>";
                    xmlRes += "<Estado>3</Estado>";//si no hubo ningun incoveniente el estado va quedar en 3
                    xmlRes += "<Error></Error>";
                    xmlRes += "<FechaProceso>" + DateTime.Now + "</FechaProceso>";
                    xmlRes += "</DetalleCabecera>";
                }
                catch (Exception ex)
                {
                    respuesta_recibida = "ERROR";

                    xmlRes += "<DetalleCabecera>";
                    xmlRes += "<IdAjuste>" + elemlist6 + "</IdAjuste>";
                    xmlRes += "<IdAjusteBalanzaD>" + elemlist7 + "</IdAjusteBalanzaD>";
                    xmlRes += "<Od_OrdenDespchoLinea>" + Od_OrdenDespchoLinea + "</Od_OrdenDespchoLinea>";
                    xmlRes += "<SKU>" + SKU + "</SKU>";
                    xmlRes += "<Productos>" + Producto + "</Productos>";
                    xmlRes += "<UnidadesEstimadas>" + UnidadesEstimadas + "</UnidadesEstimadas>";
                    xmlRes += "<PesoEstimado>" + PesoEstimado + "</PesoEstimado>";
                    xmlRes += "<Fase>1</Fase>";
                    xmlRes += "<Estado>2</Estado>";//si no hubo ningun incoveniente el estado va quedar en 3
                    xmlRes += "<Error>" + ex.ToString() + "</Error>";
                    xmlRes += "<FechaProceso>" + DateTime.Now + "</FechaProceso>";
                    xmlRes += "</DetalleCabecera>";

                }


            }
            xmlRes += "</DetallesCabecera>";
            xmlRes += "</mrm:GesImpSKU>";

            xmlCavecera += "<ControlProceso>";
            xmlCavecera += "<CodigoCompania>602</CodigoCompania>";
            xmlCavecera += "<CodigoSistema>RP3</CodigoSistema>";
            xmlCavecera += "<CodigoServicio>GesImpSKU  </CodigoServicio>";
            xmlCavecera += "<Proceso>INSERTAR/EJECUTAR</Proceso>";
            xmlCavecera += "<Resultado>" + respuesta_recibida + "</Resultado>";//PENDIENTE
            xmlCavecera += "</ControlProceso>";



            return xmlCavecera + xmlRes;
            //********************************************************************************************

        }
    }
}
