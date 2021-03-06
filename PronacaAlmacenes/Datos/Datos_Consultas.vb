
Imports System.Data.SqlClient

Public Class Datos_Consultas
    Dim ConexionSql As SqlConnection = Nothing
    Dim ComandoSql As SqlCommand = Nothing
    Dim query As String = Nothing
    Dim LectorDatos As SqlDataReader = Nothing
    Dim AdaptadorSql As SqlDataAdapter = Nothing
    Dim DatoAlmacenado As DataSet = Nothing
    Private CadenaSql As New Datos_Conexion()

    'consulta operador -> QO,O 
    Public Function Consulta_Usuario(Usuario As String, Pass As String) As String
        Dim Respuesta As String
        Try
            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                ComandoSql = New SqlCommand("select * from usuarios where id_usu='" & Usuario & "' and pss_usu='" & Pass & "'", ConexionSql)
                ConexionSql.Open()
                Respuesta = Convert.ToString(ComandoSql.ExecuteScalar())
                ConexionSql.Close()
                SqlConnection.ClearAllPools()
                If Respuesta.Equals("") Then
                    Respuesta = Chr(21) & "Usuario no encontrado"
                End If
            End Using
        Finally
            If ConexionSql IsNot Nothing AndAlso ConexionSql.State <> ConnectionState.Closed Then
                ConexionSql.Close()
            End If
        End Try
        Return Respuesta
    End Function
    'Consulta taras -> QT;Codgo del la tara 
    Public Function Consulta_Tara(Codigo_Tara As String) As String
        Dim Respuesta As String = ""
        Try
            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                ComandoSql = New SqlCommand("Select distinct(Tar_Peso) from taras where Tar_Codigo='" & Codigo_Tara & "'", ConexionSql)
                ConexionSql.Open()
                Respuesta = Convert.ToString(ComandoSql.ExecuteScalar())
                ConexionSql.Close()
                SqlConnection.ClearAllPools()
            End Using
        Finally
            If ConexionSql IsNot Nothing AndAlso ConexionSql.State <> ConnectionState.Closed Then
                ConexionSql.Close()
            End If
        End Try
        Return Respuesta
    End Function
    'Consulta de ordenes especificas

    Public Function Consulta_OrdenesdeProduccion(ID_ESTABLECIMIENTO As String) As String
        Dim Respuesta As String = ""
        Try
            Respuesta = "SELECT STUFF((SELECT ','+Od_OrdenDespcho FROM Cabecera where Cab_Estado ='A' and IdEstablecimiento='" & ID_ESTABLECIMIENTO & "' FOR XML PATH('')),1,1, '') AS Orden_Despacho"

            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                ConexionSql.Open()
                Dim Comando_Sql As SqlCommand = New SqlCommand(Respuesta, ConexionSql)
                Respuesta = Convert.ToString(Comando_Sql.ExecuteScalar())
                ConexionSql.Close()
                SqlConnection.ClearAllPools()
            End Using
        Finally
            If ConexionSql IsNot Nothing AndAlso ConexionSql.State <> ConnectionState.Closed Then
                ConexionSql.Close()
            End If
        End Try
        Return Respuesta
    End Function


    Public Function N_Pesajes(Orden_Despacho As String) As String
        Dim Respuesta As String = ""
        Try
            Respuesta = "SELECT count(*)FROM Transacciones WHERE IdAjusteBalanza IN (SELECT IdAjusteBalanza FROM Cabecera where Cab_Estado='A' AND Od_OrdenDespcho='" & Orden_Despacho & "') AND  Tra_Estado='A' AND Od_OrdenDespcho='" & Orden_Despacho & "'"

            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                ConexionSql.Open()
                Dim Comando_Sql As SqlCommand = New SqlCommand(Respuesta, ConexionSql)
                Respuesta = Convert.ToString(Comando_Sql.ExecuteScalar())
                ConexionSql.Close()
                SqlConnection.ClearAllPools()
            End Using
        Finally
            If ConexionSql IsNot Nothing AndAlso ConexionSql.State <> ConnectionState.Closed Then
                ConexionSql.Close()
            End If
        End Try
        Return Respuesta
    End Function

    Public Function N_PesajesTemporales(Orden_Despacho As String, Establecimiento As String) As String
        Dim Respuesta As String = ""
        Try
            Respuesta = "SELECT COUNT(*)FROM TransaccionesTemporales INNER JOIN Cabecera ON TransaccionesTemporales.Od_OrdenDespcho=Cabecera.Od_OrdenDespcho WHERE cabecera.Od_OrdenDespcho= '" & Orden_Despacho & "' AND IdEstablecimiento='" & Establecimiento & "' AND TransaccionesTemporales.Tra_Estado='A'"
            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                ConexionSql.Open()
                Dim Comando_Sql As SqlCommand = New SqlCommand(Respuesta, ConexionSql)
                Respuesta = Convert.ToString(Comando_Sql.ExecuteScalar())
                ConexionSql.Close()
                SqlConnection.ClearAllPools()
            End Using
        Finally
            If ConexionSql IsNot Nothing AndAlso ConexionSql.State <> ConnectionState.Closed Then
                ConexionSql.Close()
            End If
        End Try
        Return Respuesta
    End Function

    Public Function Nombre_Producto(Cod_Producto As String, Orden_Despacho As String) As String
        Dim Respuesta As String = ""
        Try
            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                'ComandoSql = New SqlCommand("select Producto from Transacciones where SKU='" & Cod_Producto & "' and  Od_OrdenDespcho='" & Orden_Despacho & "' and tra_estado ='A' ", ConexionSql)
                ComandoSql = New SqlCommand("select concat(producto,';','/',unidades,';',peso,';') from Transacciones inner join Cabecera on Transacciones.IdAjusteBalanza=Cabecera.IdAjusteBalanza where Transacciones.SKU='" & Cod_Producto & "' and  Cabecera.Od_OrdenDespcho='" & Orden_Despacho & "' and tra_estado ='A' ", ConexionSql)
                ConexionSql.Open()
                Respuesta = Convert.ToString(ComandoSql.ExecuteScalar())
                ConexionSql.Close()
                SqlConnection.ClearAllPools()
            End Using
        Finally
            If ConexionSql IsNot Nothing AndAlso ConexionSql.State <> ConnectionState.Closed Then
                ConexionSql.Close()
            End If
        End Try
        Return Respuesta
    End Function

    Public Function Nombre_ProductoTemporal(Cod_Producto As String, Orden_Despacho As String, Establecimiento As String) As String
        Dim Respuesta As String = ""
        Try
            Respuesta = "SELECT CONCAT(producto,';','/',unidades,';',peso,';') FROM TransaccionesTemporales INNER JOIN Cabecera ON TransaccionesTemporales.Od_OrdenDespcho=Cabecera.Od_OrdenDespcho WHERE TransaccionesTemporales.SKU='" & Cod_Producto & "' AND  Cabecera.Od_OrdenDespcho='" & Orden_Despacho & "' AND tra_estado ='A' AND Cabecera.IdEstablecimiento='" & Establecimiento & "' "
            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                ConexionSql.Open()
                Dim Comando_Sql As SqlCommand = New SqlCommand(Respuesta, ConexionSql)
                Respuesta = Convert.ToString(Comando_Sql.ExecuteScalar())
                ConexionSql.Close()
                SqlConnection.ClearAllPools()
            End Using
        Finally
            If ConexionSql IsNot Nothing AndAlso ConexionSql.State <> ConnectionState.Closed Then
                ConexionSql.Close()
            End If
        End Try
        Return Respuesta
    End Function

    '*******************************************************Consulta para enviar datos x XML*********************************************************** 



    Public Function Orden_Despacho(Orden As String) As DataSet
        Dim Dato_Almacenado As DataSet = New DataSet()
        Dim consulta As String = ""
        Try
            consulta = "select * from Cabecera where Od_OrdenDespcho='" & Orden & "'"
            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                ConexionSql.Open()
                Dim Comando_Sql As SqlCommand = New SqlCommand(consulta, ConexionSql)
                Dim Adaptador_Sql As SqlDataAdapter = New SqlDataAdapter(Comando_Sql)
                Adaptador_Sql.Fill(Dato_Almacenado)
                ConexionSql.Close()
                SqlConnection.ClearAllPools()
            End Using

        Finally

            If ConexionSql IsNot Nothing AndAlso ConexionSql.State <> ConnectionState.Closed Then
                ConexionSql.Close()
            End If
        End Try

        Return Dato_Almacenado
    End Function

    Public Function Consultar_UnidadesConfirmadas(Producto As String, Orden As String) As Integer
        Dim Dato_Almacenado As Integer = 0
        Dim consulta As String = ""
        Try
            'consulta = "select * from Cabecera where Od_OrdenDespcho='" & Orden & "'"
            consulta = "SELECT UnidadesConfirmadas FROM Transacciones WHERE Tra_Estado='P' AND Od_OrdenDespcho='" & Orden & "'" & " AND SKU='" & Producto & "'"
            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                ConexionSql.Open()
                Dim Comando_Sql As SqlCommand = New SqlCommand(consulta, ConexionSql)
                Dato_Almacenado = Convert.ToInt32(Comando_Sql.ExecuteScalar())
                ConexionSql.Close()
                SqlConnection.ClearAllPools()
            End Using

        Finally

            If ConexionSql IsNot Nothing AndAlso ConexionSql.State <> ConnectionState.Closed Then
                ConexionSql.Close()
            End If
        End Try

        Return Dato_Almacenado
    End Function

    Public Function Consultar_PesoConfirmado(Producto As String, Orden As String) As Double
        Dim Dato_Almacenado As Double = 0
        Dim consulta As String = ""
        Try
            'consulta = "select * from Cabecera where Od_OrdenDespcho='" & Orden & "'"
            consulta = "SELECT PesoConfirmado FROM Transacciones WHERE Tra_Estado='P' AND Od_OrdenDespcho='" & Orden & "'" & " AND SKU='" & Producto & "'"
            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                ConexionSql.Open()
                Dim Comando_Sql As SqlCommand = New SqlCommand(consulta, ConexionSql)
                Dato_Almacenado = Convert.ToDouble(Comando_Sql.ExecuteScalar())
                ConexionSql.Close()
                SqlConnection.ClearAllPools()
            End Using

        Finally

            If ConexionSql IsNot Nothing AndAlso ConexionSql.State <> ConnectionState.Closed Then
                ConexionSql.Close()
            End If
        End Try

        Return Dato_Almacenado
    End Function
    Public Function Consultar_LoteConfirmado(Producto As String, Orden As String) As String
        Dim Dato_Almacenado As String = ""
        Dim consulta As String = ""
        Try
            'consulta = "select * from Cabecera where Od_OrdenDespcho='" & Orden & "'"
            consulta = "SELECT Lote FROM Transacciones WHERE Tra_Estado='P' AND Od_OrdenDespcho='" & Orden & "'" & " AND SKU='" & Producto & "'"
            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                ConexionSql.Open()
                Dim Comando_Sql As SqlCommand = New SqlCommand(consulta, ConexionSql)
                Dato_Almacenado = Convert.ToString(Comando_Sql.ExecuteScalar())
                ConexionSql.Close()
                SqlConnection.ClearAllPools()
            End Using

        Finally

            If ConexionSql IsNot Nothing AndAlso ConexionSql.State <> ConnectionState.Closed Then
                ConexionSql.Close()
            End If
        End Try

        Return Dato_Almacenado
    End Function

    '****************************detalle**********************************************************
    Public Function Detalle(Orden As String, Sku As String) As DataSet
        Dim Dato_Almacenado As DataSet = New DataSet()
        Dim consulta As String = ""
        Try
            'consulta = "select * from Transacciones where SKU='" & Sku & "'"
            consulta = "select Transacciones.IdAjuste,Cabecera.CodigoLN from Transacciones inner join Cabecera on Transacciones.IdAjusteBalanza=Cabecera.IdAjusteBalanza where Transacciones.SKU='" & Sku & "' and Cabecera.Od_OrdenDespcho='" & Orden & "'"
            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                ConexionSql.Open()
                Dim Comando_Sql As SqlCommand = New SqlCommand(consulta, ConexionSql)
                Dim Adaptador_Sql As SqlDataAdapter = New SqlDataAdapter(Comando_Sql)
                Adaptador_Sql.Fill(Dato_Almacenado)
                ConexionSql.Close()
                SqlConnection.ClearAllPools()
            End Using

        Finally

            If ConexionSql IsNot Nothing AndAlso ConexionSql.State <> ConnectionState.Closed Then
                ConexionSql.Close()
            End If
        End Try

        Return Dato_Almacenado
    End Function

    '**********************************************************************************************



    Public Function Gestion_Pesos(ID_Indicador As String, secuencial As String, Cod_Operador As String, Cod_Producto As String, Orden_Produccion As String, Cod_Tara As String, Peso As String, Unidades As String, estado As String, pes_gaveta As String, lote As String) As Integer
        Dim Respuesta As Integer
        Try
            '*******************TIPOS DE ESTADO ******************
            ' A=Activo cuando a un no serealiza ningun pesaje 
            ' P=PESADO cuando ya se realizo el pesaje 
            ' D=Cuando se descarto el pesaje la balanza envia el pesaje en 0 

            'Dim Estado As String
            'If Peso > 0 Then
            '    Estado = "P"
            'Else
            '    Estado = "D"

            'End If
            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                ComandoSql = New SqlCommand("exec P_Pesaje  '" & ID_Indicador & "','" & secuencial & "','" & Cod_Operador & "','" & Cod_Producto & "','" & Orden_Produccion & "','" & Cod_Tara & "','" & Peso & "','" & Unidades & "','" & estado & "','" & pes_gaveta & "','" & lote & "'", ConexionSql)
                ConexionSql.Open()
                Respuesta = ComandoSql.ExecuteNonQuery()
                ConexionSql.Close()
            End Using
        Finally
            If ConexionSql IsNot Nothing AndAlso ConexionSql.State <> ConnectionState.Closed Then
                ConexionSql.Close()
            End If
        End Try
        Return Respuesta
    End Function



    Public Function Gestion_PesosTemporales(ID_Indicador As String, secuencial As String, Cod_Operador As String, Cod_Producto As String, Orden_Produccion As String, Cod_Tara As String, Peso As String, Unidades As String, estado As String, pes_gaveta As String, lote As String) As Integer
        Dim Respuesta As Integer
        Try
            '*******************TIPOS DE ESTADO ******************
            ' A=Activo cuando a un no serealiza ningun pesaje 
            ' P=PESADO cuando ya se realizo el pesaje 
            ' D=Cuando se descarto el pesaje la balanza envia el pesaje en 0 

            'Dim Estado As String
            'If Peso > 0 Then
            '    Estado = "P"
            'Else
            '    Estado = "D"

            'End If
            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                ComandoSql = New SqlCommand("exec P_Pesaje  '" & ID_Indicador & "','" & secuencial & "','" & Cod_Operador & "','" & Cod_Producto & "','" & Orden_Produccion & "','" & Cod_Tara & "','" & Peso & "','" & Unidades & "','" & estado & "','" & pes_gaveta & "','" & lote & "'", ConexionSql)
                ConexionSql.Open()
                Respuesta = ComandoSql.ExecuteNonQuery()
                ConexionSql.Close()
            End Using
        Finally
            If ConexionSql IsNot Nothing AndAlso ConexionSql.State <> ConnectionState.Closed Then
                ConexionSql.Close()
            End If
        End Try
        Return Respuesta
    End Function
    Public Function probar_Conexion(cadena As String) As Boolean

        Try
            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                ConexionSql.Open()
                ConexionSql.Close()
            End Using
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    '***************************************************TABLA DE REGISTRO DE ERRORES***********************************************
    Public Function Insertar_error(Log_Body As String, Log_Resultado As String, Log_Error As String) As Integer
        Dim Respuesta As Integer
        Try
            '*******************TIPOS DE ESTADO ******************
            ' A=Activo cuando a un no serealiza ningun pesaje 
            ' P=PESADO cuando ya se realizo el pesaje 
            ' D=Cuando se descarto el pesaje la balanza envia el pesaje en 0 

            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                ComandoSql = New SqlCommand("INSERT INTO [dbo].[TCPRM_LOGWS] ([Log_Body],[Log_Resultado],[Log_Error])VALUES ()", ConexionSql)
                ConexionSql.Open()
                Respuesta = ComandoSql.ExecuteNonQuery()
                ConexionSql.Close()
            End Using
        Finally
            If ConexionSql IsNot Nothing AndAlso ConexionSql.State <> ConnectionState.Closed Then
                ConexionSql.Close()
            End If
        End Try
        Return Respuesta
    End Function

    '***************************************************opciones de la retrasmision de los pesajes pendientes******************

    '****************************Cabecera**********************************************************
    Public Function CabeceraRetrasmision() As DataSet
        Dim Dato_Almacenado As DataSet = New DataSet()
        Dim consulta As String = ""
        Try
            'consulta = "select * from Transacciones where SKU='" & Sku & "'"
            consulta = "select Cabecera.IdEmpresa,Cabecera.IdEstablecimiento,Cabecera.IdPuntoOperacion,Cabecera.IdAjusteBalanza,Cabecera.Tipo_Transaccion from Cabecera  where  IdAjusteBalanza in ( select IdAjusteBalanza from Transacciones where  Tra_Envio IN ('ERROR','',NULL) OR Tra_Envio IS NULL)"
            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                ConexionSql.Open()
                Dim Comando_Sql As SqlCommand = New SqlCommand(consulta, ConexionSql)
                Dim Adaptador_Sql As SqlDataAdapter = New SqlDataAdapter(Comando_Sql)
                Adaptador_Sql.Fill(Dato_Almacenado)
                ConexionSql.Close()
                SqlConnection.ClearAllPools()
            End Using

        Finally

            If ConexionSql IsNot Nothing AndAlso ConexionSql.State <> ConnectionState.Closed Then
                ConexionSql.Close()
            End If
        End Try

        Return Dato_Almacenado
    End Function

    '********************************************** DETALLLE*****************************************

    Public Function DetalleRetrasmision() As DataSet
        Dim Dato_Almacenado As DataSet = New DataSet()
        Dim consulta As String = ""
        Try
            'consulta = "select * from Transacciones where SKU='" & Sku & "'"
            consulta = "select Cabecera.IdAjusteBalanza,transacciones.IdAjuste,Cabecera.CodigoLN,Transacciones.SKU,Transacciones.PesoConfirmado,Transacciones.Tar_Codigo,Transacciones.IdAjusteBalanza,Transacciones.Tra_Estado,CONVERT(nvarchar,CONVERT(DATE,Transacciones.FecIng,103)) AS 'Fecha',Transacciones.Tra_Operador,Transacciones.UnidadesConfirmadas,Transacciones.Tra_Estado from transacciones INNER JOIN Cabecera on Transacciones.IdAjusteBalanza=Cabecera.IdAjusteBalanza where Tra_Envio IN ('ERROR','',NULL) OR Tra_Envio IS NULL"
            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                ConexionSql.Open()
                Dim Comando_Sql As SqlCommand = New SqlCommand(consulta, ConexionSql)
                Dim Adaptador_Sql As SqlDataAdapter = New SqlDataAdapter(Comando_Sql)
                Adaptador_Sql.Fill(Dato_Almacenado)
                ConexionSql.Close()
                SqlConnection.ClearAllPools()
            End Using

        Finally

            If ConexionSql IsNot Nothing AndAlso ConexionSql.State <> ConnectionState.Closed Then
                ConexionSql.Close()
            End If
        End Try

        Return Dato_Almacenado
    End Function
    '******************************Canviamos los estados dependiendo la respuesta del soap
    Public Function Gestion_PesosSoap(IdAjusteBalanza As String, IdAjuste As String, tra_envio As String) As Integer
        Dim Respuesta As Integer
        Try

            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                'ComandoSql = New SqlCommand("exec P_Estados '" & IdAjusteBalanza & "','" & IdAjuste & "','" & tra_envio & "' ", ConexionSql)
                ComandoSql = New SqlCommand("exec P_Estados '" & IdAjusteBalanza & "','" & IdAjuste & "','" & tra_envio & "' ", ConexionSql)
                ConexionSql.Open()
                Respuesta = ComandoSql.ExecuteNonQuery()
                ConexionSql.Close()
            End Using
        Finally
            If ConexionSql IsNot Nothing AndAlso ConexionSql.State <> ConnectionState.Closed Then
                ConexionSql.Close()
            End If
        End Try
        Return Respuesta
    End Function

    Public Function CambiarEstadoEnvio(Orden As String, Producto As String, Estado As String) As String
        Dim Dato_Almacenado As Integer = 0
        Dim consulta As String = ""
        Try
            consulta = "update TOP (1) Transacciones set Tra_Envio=@estado where Od_OrdenDespcho=@orden and sku=@producto"
            Using ConexionSql = New SqlConnection(CadenaSql.String_Conexion())
                ConexionSql.Open()
                Dim Comando_Sql As SqlCommand = New SqlCommand(consulta, ConexionSql)
                Comando_Sql.Parameters.Add(New SqlParameter("@orden", Orden))
                Comando_Sql.Parameters.Add(New SqlParameter("@producto", Producto))
                Comando_Sql.Parameters.Add(New SqlParameter("@estado", Estado))
                Dato_Almacenado = ComandoSql.ExecuteNonQuery()
                ConexionSql.Close()
                SqlConnection.ClearAllPools()

            End Using

        Finally

            If ConexionSql IsNot Nothing AndAlso ConexionSql.State <> ConnectionState.Closed Then
                ConexionSql.Close()
            End If
        End Try

        Return Dato_Almacenado
    End Function


End Class
