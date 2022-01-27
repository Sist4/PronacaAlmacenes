﻿Imports Datos
Public Class Negocios_Consultas
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
    Public Function Consulta_OrdenesdeProduccion(ID_BALANZA As String) As String
        Return Consultas.Consulta_OrdenesdeProduccion(ID_BALANZA)
    End Function

    Public Function N_Pesajes(Orden_Despacho As String) As String

        Return Consultas.N_Pesajes(Orden_Despacho)
    End Function
    Public Function Nombre_Producto(Cod_Producto As String, Orden_Despacho As String) As String

        Return Consultas.Nombre_Producto(Cod_Producto, Orden_Despacho)
    End Function
    Public Function Gestion_Pesos(ID_Indicador As String, secuencial As String, Cod_Operador As String, Cod_Producto As String, Orden_Produccion As String, Cod_Tara As String, Peso As String, Unidades As String, estado As String, Pes_Gaveta As String) As Integer

        Return Consultas.Gestion_Pesos(ID_Indicador, secuencial, Cod_Operador, Cod_Producto, Orden_Produccion, Cod_Tara, Peso, Unidades, estado, Pes_Gaveta)
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


End Class
