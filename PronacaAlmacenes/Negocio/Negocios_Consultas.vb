﻿Public Class Negocios_Consultas
    Dim Consultas As New Datos_Consultas

    Public Function Consulta_Usuario(Usuario As String, Pass As String) As String
        'consulta operador -> QO,O 
        Return Consultas.Consulta_Usuario(Usuario, Pass)
    End Function

    'consulta tara ->  QT,CodTara,
    Public Function Consulta_Tara(Codigo_Tara As String) As String
        Return Consultas.Consulta_Tara(Codigo_Tara)
    End Function
    'CONSULTA DE LA ORDEN ESPECIFICA
    Public Function Consulta_OrdenesdeProduccion(Establecimiento As String) As String
        Return Consultas.Consulta_OrdenesdeProduccion(Establecimiento)
    End Function

    Public Function N_Pesajes(Orden_Despacho As String) As String

        Return Consultas.N_Pesajes(Orden_Despacho)
    End Function

    Public Function N_PesajesTemporales(Estado As String, Orden_Despacho As String, Establecimiento As String) As Integer

        Return Consultas.N_PesajesTemporales(Estado, Orden_Despacho, Establecimiento)
    End Function

    Public Function Nombre_ProductoTemporal(Cod_Producto As String, Orden_Despacho As String, Establecimiento As String) As String

        Return Consultas.Nombre_ProductoTemporal(Cod_Producto, Orden_Despacho, Establecimiento)
    End Function
    Public Function Nombre_Producto(Cod_Producto As String, Orden_Despacho As String) As String

        Return Consultas.Nombre_Producto(Cod_Producto, Orden_Despacho)
    End Function
    Public Function Gestion_Pesos(Estado As String, ID_Indicador As String, secuencial As String, Cod_Operador As String, Cod_Producto As String, Orden_Produccion As String, Cod_Tara As String, Peso As String, Unidades As String, Pes_Gaveta As String, Lote As String) As Integer
        Return Consultas.Gestion_Pesos(Estado, ID_Indicador, secuencial, Cod_Operador, Cod_Producto, Orden_Produccion, Cod_Tara, Peso, Unidades, Pes_Gaveta, Lote)
    End Function
    Public Function Consultar_UnidadesConfirmadas(Cod_Producto As String, Orden_Produccion As String) As Integer
        Return Consultas.Consultar_UnidadesConfirmadas(Cod_Producto, Orden_Produccion)
    End Function
    Public Function Consultar_PesoConfirmado(Cod_Producto As String, Orden_Produccion As String) As Double
        Return Consultas.Consultar_PesoConfirmado(Cod_Producto, Orden_Produccion)
    End Function
    Public Function Consultar_LoteConfirmado(Cod_Producto As String, Orden_Produccion As String) As String
        Return Consultas.Consultar_LoteConfirmado(Cod_Producto, Orden_Produccion)
    End Function
    Public Function Gestion_PesosTemporales(ID_Indicador As String, secuencial As String, Cod_Operador As String, Cod_Producto As String, Orden_Produccion As String, Cod_Tara As String, Peso As String, Unidades As String, estado As String, Pes_Gaveta As String, Lote As String) As Integer

        Return Consultas.Gestion_PesosTemporales(ID_Indicador, secuencial, Cod_Operador, Cod_Producto, Orden_Produccion, Cod_Tara, Peso, Unidades, estado, Pes_Gaveta, Lote)
    End Function

    Public Function Insertar_error(Log_Body As String, Log_Resultado As String, Log_Error As String) As Integer

        Return Consultas.Insertar_error(Log_Body, Log_Resultado, Log_Error)
    End Function

    Public Function Orden_Despacho(Orden As String) As DataSet
        Return Consultas.Orden_Despacho(Orden)
    End Function
    Public Function Detalle(Orden As String, Sku As String) As DataSet
        Return Consultas.Detalle(Orden, Sku)
    End Function
    Public Function CabeceraRetrasmision() As DataSet
        Return Consultas.CabeceraRetrasmision()
    End Function

    Public Function DetalleRetrasmision() As DataSet
        Return Consultas.DetalleRetrasmision()
    End Function

    Public Function Gestion_PesosSoap(IdAjusteBalanza As String, IdAjuste As String, tra_envio As String) As Integer

        Return Consultas.Gestion_PesosSoap(IdAjusteBalanza, IdAjuste, tra_envio)
    End Function

    Public Function CambiarEstadoEnvio(Orden As String, Producto As String, Estado As String) As Integer

        Return Consultas.CambiarEstadoEnvio(Orden, Producto, Estado)
    End Function
    Public Function GuardarXML(Establecimiento As String, Orden As String, SKU As String, Ajuste As String, Enviado As String, Recibido As String, Fase As String, Estado As String) As Integer

        Return Consultas.GuardarXML(Establecimiento, Orden, SKU, Ajuste, Enviado, Recibido, Fase, Estado)
    End Function

    Public Function GuardarError(Info As String, Trace As String) As Integer
        Return Consultas.GuardarError(Info, Trace)
    End Function
    Public Function ObtenerProductoPesado(Cod_Producto As String, Orden_Despacho As String, Establecimiento As String) As String
        Return Consultas.ProductoPesado(Cod_Producto, Orden_Despacho, Establecimiento)
    End Function

    Public Function ObtenerMensajeXMLProductoPesado(Cod_Producto As String, Orden_Despacho As String, Establecimiento As String) As String
        Return Consultas.MensajeXMLProductoPesado(Cod_Producto, Orden_Despacho, Establecimiento)
    End Function

    Public Function ProductoTemporal(Estado As String, Orden_Despacho As String, Establecimiento As String) As String
        Return Consultas.ProductoTemporal(Estado, Orden_Despacho, Establecimiento)
    End Function
    Public Function SiguienteProductoTemporal(Estado As String, Sku As String, Orden_Despacho As String, Establecimiento As String) As String
        Return Consultas.SiguienteProductoTemporal(Estado, Sku, Orden_Despacho, Establecimiento)
    End Function

    Public Function WriteToEventLog(Entry As String, AppName As String, EventType As EventLogEntryType, LogName As String) As Boolean
        Dim objEventLog As New EventLog
        Try
            'Register the App as an Event Source
            If Not objEventLog.SourceExists(AppName) Then
                objEventLog.CreateEventSource(AppName, LogName)
            End If
            objEventLog.Source = AppName
            'WriteEntry is overloaded; this is one of 10 ways to call it
            objEventLog.WriteEntry(Entry, EventType)
            Return True
        Catch Ex As Exception
            Return False
        End Try
    End Function
End Class

