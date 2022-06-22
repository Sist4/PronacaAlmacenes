Imports System.Net.Sockets
Imports System.Xml
Imports System.Net
Imports System.IO

'Provides a container object to serve as the state object for async client and stream operations

Public Class ConnectionInfo
    'hold a reference to entire monitor instead of just the listener
    Dim Consultas As New Negocios_Consultas()
    ' CONEXION AL WEB SERVICE DE RP3
    '  Dim webService As New WsRp3.SendBalanceData()
    Dim Usuario As String = "balanzas_test"

    Dim Pass As String = "QmFsYW56NHNQZXNvc1ByMG5hY2Ek"

    Private Function Cabecera(IdAjusteBalanza As String, TipoTransaccion As String,
                          IdEmpresa As String, IdEstablecimiento As String, IdPuntoOperacion As String, Fase As String, Estado As String, Errorr As String, FechaProceso As String) As String

        Dim XmlCabecera As String = ""
        XmlCabecera += "<IdAjusteBalanza>" & IdAjusteBalanza & "</IdAjusteBalanza>"
        XmlCabecera += "<TipoTransaccion>" & TipoTransaccion & "</TipoTransaccion>"
        XmlCabecera += "<IdEmpresa>" & IdEmpresa & "</IdEmpresa>"
        XmlCabecera += "<IdEstablecimiento>" & IdEstablecimiento & "</IdEstablecimiento>"
        XmlCabecera += "<IdPuntoOperacion>" & IdPuntoOperacion & "</IdPuntoOperacion>"
        XmlCabecera += "<Fase>" & Fase & "</Fase>"
        XmlCabecera += "<Estado>" & Estado & "</Estado>"
        XmlCabecera += "<Error>" & Errorr & "</Error>"
        XmlCabecera += "<FechaProceso>" & FechaProceso & "</FechaProceso>"
        Return XmlCabecera
    End Function

    Private Function Detalle(IdAjusteBalanza As String, IdAjuste As String,
                             CodigoLN As String, SKU As String,
                             PesoConfirmado As String, TarCodigo As String, TraBalanza As String, TraEstado As String, TraFecha As String,
                             TraOperador As String, UnidadesConfirmadas As String, LoteConfirmado As String,
                             Fase As String, Estado As String, Errorr As String, FechaProceso As String) As String
        Dim XmlDetalle As String = "<DetalleCabecera>"
        XmlDetalle += "<IdAjusteBalanza>" & IdAjusteBalanza & "</IdAjusteBalanza>"
        XmlDetalle += "<IdAjuste>" & IdAjuste & "</IdAjuste>"
        XmlDetalle += "<CodigoLN>" & CodigoLN & "</CodigoLN>"
        XmlDetalle += "<SKU>" & SKU & "</SKU>"
        XmlDetalle += "<PesoConfirmado>" & PesoConfirmado & "</PesoConfirmado>"
        XmlDetalle += "<TarCodigo>" & TarCodigo & "</TarCodigo>"
        XmlDetalle += "<TraBalanza>" & TraBalanza & "</TraBalanza>"
        XmlDetalle += "<TraEstado>" & TraEstado & "</TraEstado>"
        XmlDetalle += "<TraFecha>" & TraFecha & "</TraFecha>"
        XmlDetalle += "<TraOperador>" & TraOperador & "</TraOperador>"
        XmlDetalle += "<UnidadesConfirmadas>" & UnidadesConfirmadas & "</UnidadesConfirmadas>"
        XmlDetalle += "<Lote>" & LoteConfirmado & "</Lote>"
        ''XmlDetalle += "<TraFecha>" & TraFecha & "</TraFecha>"
        'XmlDetalle += "<TraOperador>" & TraOperador & "</TraOperador>"
        'XmlDetalle += "<UnidadesConfirmadas>" & UnidadesConfirmadas & "</UnidadesConfirmadas>"
        XmlDetalle += "<Fase>" & Fase & "</Fase>"
        XmlDetalle += "<Estado>" & Estado & "</Estado>"
        XmlDetalle += "<Error>" & Errorr & "</Error>"
        XmlDetalle += "<FechaProceso>" & FechaProceso & "</FechaProceso>"
        XmlDetalle += "</DetalleCabecera>"

        Return XmlDetalle
    End Function


    Private Function leer_Xml(Xml As String, Orden As String, Producto As String)
        Try
            Dim xmltest As XmlDocument = New XmlDocument()
            Dim m_nodelist As XmlNodeList
            Dim NodoCabecera As XmlNodeList
            Dim NodoDetalle As XmlNodeList

            Dim e_Cambio As String = Xml
            Dim Texto As String = "<GesImpPesBal xmlns:mrm=""http://ln.gesalm.integracion.pronaca.com.ec"">"

            e_Cambio = Xml.Replace(Texto, "<Respuesta>").Replace("</GesImpPesBal>", "</Respuesta>")




            'xmltest.LoadXml(Xml.Replace(Texto, "<Respuesta>").Replace("mrm:GesImpPesBal", "Respuesta"))
            xmltest.LoadXml(e_Cambio)


            ' Obtenemos la lista de los nodos "name"
            'm_nodelist = xmltest.SelectNodes("/Respuesta/ControlProceso")

            'For Each m_node In m_nodelist
            '' Obtenemos el resultado ERROR O EXITO
            'Dim Resultado As Object = m_node.ChildNodes.Item(4).InnerText

            'If Resultado.Equals("ERROR") Then
            'el campo de la cabecera se va a poner 
            NodoDetalle = xmltest.SelectNodes("/Respuesta/DetallesCabecera/DetalleCabecera")

            For Each m_nodeDetalle In NodoDetalle
                Dim resultado_detalle As String = m_nodeDetalle.ChildNodes.Item(14).InnerText

                If resultado_detalle <> ("") Then
                    resultado_detalle = "ERROR"
                Else
                    resultado_detalle = "EXITO"
                End If

                'Dim res As Integer = Consultas.Gestion_PesosSoap(m_nodeDetalle.ChildNodes.Item(0).InnerText, m_nodeDetalle.ChildNodes.Item(1).InnerText, resultado_detalle)
                ' Dim res As Integer = Consultas.Gestion_PesosSoap(Orden, Codigo, resultado_detalle)
                Dim res As Integer = Consultas.CambiarEstadoEnvio(Orden, Producto, resultado_detalle)
                'respuesta
                MessageBox.Show(Xml)
                'Next



                'ElseIf Resultado.Equals("EXITO") Then

                '  End If

            Next





        Catch ex As Exception

        End Try
    End Function




    Private Function CreateSOAPWebRequest() As HttpWebRequest
        Throw New NotImplementedException()
    End Function

    Private _Monitor As MonitorInfo
    Public ReadOnly Property Monitor As MonitorInfo
        Get
            Return _Monitor
        End Get
    End Property

    Private _Client As TcpClient
    Public ReadOnly Property Client As TcpClient
        Get
            Return _Client
        End Get
    End Property

    Private _Stream As NetworkStream
    Public ReadOnly Property Stream As NetworkStream
        Get
            Return _Stream
        End Get
    End Property

    Private _DataQueue As System.Collections.Concurrent.ConcurrentQueue(Of Byte)
    Public ReadOnly Property DataQueue As System.Collections.Concurrent.ConcurrentQueue(Of Byte)
        Get
            Return _DataQueue
        End Get
    End Property

    Private _LastReadLength As Integer
    Public ReadOnly Property LastReadLength As Integer
        Get
            Return _LastReadLength
        End Get
    End Property

    'The buffer size is an arbitrary value which should be selected based on the
    'amount of data you need to transmit, the rate of transmissions, and the
    'anticipalted number of clients. These are the considerations for designing
    'the communicaition protocol for data transmissions, and the size of the read
    'buffer must be based upon the needs of the protocol.
    Private _Buffer(1024) As Byte

    Public Sub New(monitor As MonitorInfo)
        _Monitor = monitor
        _DataQueue = New System.Collections.Concurrent.ConcurrentQueue(Of Byte)
    End Sub

    Public Sub AcceptClient(result As IAsyncResult)
        _Client = _Monitor.Listener.EndAcceptTcpClient(result)
        If _Client IsNot Nothing AndAlso _Client.Connected Then
            _Stream = _Client.GetStream
        End If
    End Sub

    Public Sub AwaitData()
        _Stream.BeginRead(_Buffer, 0, _Buffer.Length, AddressOf DoReadData, Me)
    End Sub

    Private Sub DoReadData(result As IAsyncResult)
        Dim info As ConnectionInfo = CType(result.AsyncState, ConnectionInfo)
        Try
            'If the stream is valid for reading, get the current data and then
            'begin another async read
            If info.Stream IsNot Nothing AndAlso info.Stream.CanRead Then
                info._LastReadLength = info.Stream.EndRead(result)
                For index As Integer = 0 To _LastReadLength - 1
                    info._DataQueue.Enqueue(info._Buffer(index))
                Next

                'obtenemos la consulta del ind570

                Dim Datos_Recibidos As String = System.Text.Encoding.ASCII.GetString(info._Buffer)
                'Datos_Recibidos = Datos_Recibidos.Replace(texto, "")
                Dim Consulta_Dato As String() = (Datos_Recibidos.Replace(",", ";")).Split(";")

                Select Case Consulta_Dato(0)
                    Case "QO" 'Operador Específico
                        Dim Respuesta As String = Consultas.Consulta_Usuario(Consulta_Dato(1), Consulta_Dato(2))
                        If Respuesta.Equals("") Then
                            info.SendMessage(Chr(21) & "Usuario No Encontrado")
                            Exit Sub
                        Else
                            info.SendMessage(Respuesta)
                            Exit Sub
                        End If
                        '  Exit Sub
                        'Frm_Receptor.AppendOutput(Respuesta)'
                    Case "QP" 'PRODUCTO
                        'codigo producto; orden despacho; ID establecimiento
                        Dim Respuesta As String = Consultas.Nombre_ProductoTemporal(Consulta_Dato(1), Consulta_Dato(2), Consulta_Dato(3))

                        If Respuesta.Equals("") Then
                            info.SendMessage(Chr(21) & "No Exite Informacion")
                            info.AwaitData()
                            Exit Sub

                        Else
                            ' info.SendMessage(Respuesta + "; /3;2;")
                            info.SendMessage(Respuesta)
                            info.AwaitData()
                            Exit Sub

                        End If

                        Exit Sub

                    Case "QT" 'TARA DEL PRODUCTO


                        Dim Respuesta As String = Consultas.Consulta_Tara(Consulta_Dato(1))
                        If Respuesta.Equals("") Then
                            info.SendMessage(Consulta_Dato(1))
                            'info.SendMessage(Chr(21) & "Tara No Encontrada")
                            info.AwaitData()
                            Exit Sub

                        Else
                            '  info.SendMessage(Respuesta)
                            info.SendMessage(Consulta_Dato(1))
                            info.AwaitData()
                            Exit Sub

                        End If
                        '  Exit Sub

                    Case "QS" 'CODIGO DEL SUPERVISOR
                        Exit Sub

                    Case "QD" 'ORDEN DEL DESPACHO
                        '0;ID balanza
                        'orden despacho;ID establecimiento ;
                        If Consulta_Dato(1).Equals("0") Then
                            Dim Respuesta As String = Consultas.Consulta_OrdenesdeProduccion(Consulta_Dato(2))
                            If Respuesta.Equals("") Then
                                info.SendMessage(Chr(21) & "No Exite OD Pendientes")
                                Exit Sub

                            Else
                                info.SendMessage(Respuesta)
                                Exit Sub

                            End If
                        Else
                            Dim Respuesta As String = Consultas.N_PesajesTemporales(Consulta_Dato(1), Consulta_Dato(2))
                            If Respuesta.Equals("0") Or Respuesta = Nothing Then
                                info.SendMessage(Chr(21) & "No hay ITEMS Para Pesar")
                                Exit Sub

                            Else
                                info.SendMessage(Respuesta)
                                Exit Sub

                            End If

                        End If
                        'Exit Sub

                    Case "PA"
                        'ID balanza;izq asd;numero secuencial+1;operador;cod prod; orden despacho;tara;peso;kilgramos;cantidad unidades;peso gavetas;lote
                        Dim Estado As String = "A"
                        Dim Respuesta As String = Consultas.Gestion_Pesos(Consulta_Dato(1), Consulta_Dato(3), Consulta_Dato(4), Consulta_Dato(5), Consulta_Dato(6), Consulta_Dato(7), Consulta_Dato(8), Consulta_Dato(10), Estado, Consulta_Dato(11), Consulta_Dato(12))
                        info.SendMessage("OK;")

                    Case "P"
                        'P;A;D;344;ADM1;2;OR002;1234;26.15; Kg.;0;
                        'P;A;D;344;ADM1;000001;RC-10000;1234;26.15;Kg.;0;
                        'P;16;R;4;1;03053543;TRA-1054253;1;48.92; Kg.;2;2.260;123; 
                        'P;16;R;4;1;8595;TRA-1066572;1;2;kg.;2;2.60;1234;
                        'P;16;R;79;1;8595;TRA-1066572;0;10;Kg.;10;2.9;1234;
                        Dim Estado As String = "P"
                        Dim Respuesta As String = Consultas.Gestion_Pesos(Consulta_Dato(1), Consulta_Dato(3), Consulta_Dato(4), Consulta_Dato(5), Consulta_Dato(6), Consulta_Dato(7), Consulta_Dato(8), Consulta_Dato(10), Estado, Consulta_Dato(11), Consulta_Dato(12))
                        Dim unidadesConfirmadas As Integer = Consultas.Consultar_UnidadesConfirmadas(Consulta_Dato(5), Consulta_Dato(6))
                        Dim pesoConfirmado As Double = Consultas.Consultar_PesoConfirmado(Consulta_Dato(5), Consulta_Dato(6))
                        Dim loteConfirmado As String = Consultas.Consultar_LoteConfirmado(Consulta_Dato(5), Consulta_Dato(6))
                        info.SendMessage("OK;")
                        Dim NumeroPesajes As String = Consultas.N_PesajesTemporales(Consulta_Dato(6), Consulta_Dato(1))
                        If NumeroPesajes.Equals("0") Or NumeroPesajes = Nothing Then
                            envioXML(Consulta_Dato, "PF", unidadesConfirmadas, pesoConfirmado, loteConfirmado)
                            Exit Sub
                        Else
                            envioXML(Consulta_Dato, "P", unidadesConfirmadas, pesoConfirmado, loteConfirmado)
                            Exit Sub

                        End If


                    Case "R"
                        'id; operador
                        'armamos el xml para enviar todo
                        '***********************************************************

                        Dim IdAjusteBalanza As String
                        Dim TipoTransaccion As String
                        Dim idEmpresa As String
                        Dim IdEstablecimineto As String
                        Dim IdPuntoOperacion As String

                        Dim StringCabecera As String

                        Dim Orden As String
                        Dim Codigo As String

                        'Dim IdAjuste As String
                        Dim Datos_Clientes As DataSet = Consultas.CabeceraRetrasmision()
                        For Each myReader As DataRow In Datos_Clientes.Tables(0).Rows
                            idEmpresa = myReader(0).ToString()
                            IdEstablecimineto = myReader(1).ToString()
                            IdPuntoOperacion = myReader(2).ToString()
                            IdAjusteBalanza = myReader(3).ToString()
                            TipoTransaccion = myReader(4).ToString()

                            Orden = Consulta_Dato(6).ToString()
                            Codigo = Consulta_Dato(5).ToString()
                            StringCabecera += Cabecera(IdAjusteBalanza, TipoTransaccion, idEmpresa, IdEstablecimineto, IdPuntoOperacion, "1", "1", "", DateTime.Now.ToString("yyyy-MM-dd"))
                        Next



                        Dim StringDetatlle As String

                        Dim Datos_Detalle As DataSet = Consultas.DetalleRetrasmision()
                        For Each myReader As DataRow In Datos_Detalle.Tables(0).Rows

                            IdAjusteBalanza = myReader(0).ToString()
                            Dim IdAjuste As String = myReader(1).ToString()
                            Dim CodigoLN As String = myReader(2).ToString()
                            Dim SKU As String = myReader(3).ToString()
                            Dim PesoConfirmado As String = myReader(4).ToString()
                            Dim TarCodigo As String = myReader(5).ToString()
                            Dim TraBalanza As String = myReader(6).ToString()
                            Dim TraEstado As String = myReader(7).ToString()
                            Dim TraFecha As String = myReader(8) '.Date.Now.ToString("yyyy-MM-dd")
                            Dim TraOperador As String = myReader(9).ToString()
                            Dim UnidadesConfirmadas As String = myReader(10).ToString()
                            Dim Lote As String = myReader(13).ToString()
                            Dim Fase As String = "1"
                            Dim Estado As String = "1"
                            Dim Errorr As String = ""
                            Dim FechaProceso As String = DateTime.Now.ToString("yyyy-MM-dd")
                            StringDetatlle += Detalle(IdAjusteBalanza, IdAjuste, CodigoLN, SKU, PesoConfirmado, TarCodigo, TraBalanza, TraEstado, TraFecha, TraOperador, UnidadesConfirmadas, Lote, Fase, Estado, Errorr, FechaProceso)
                        Next


                        Dim servicioPortClient As New WsRp3.Proporcionar_servicioPortClient()
                        Dim sendBalanceData As New WsRp3.SendBalanceData()
                        servicioPortClient.ClientCredentials.UserName.UserName = Usuario
                        servicioPortClient.ClientCredentials.UserName.Password = Pass
                        '***********************************************************
                        Dim StringXml As String = "<mrm:GesImpPesBal xmlns:mrm=""http://ln.gesalm.integracion.pronaca.com.ec"">"
                        StringXml += "<ControlProceso>"
                        StringXml += "<CodigoCompania>602</CodigoCompania>"
                        StringXml += "<CodigoSistema>BALANZAS</CodigoSistema>"
                        StringXml += "<CodigoServicio>GESIMPPESBAL</CodigoServicio>"
                        StringXml += "<Proceso>ACTUALIZAR</Proceso><Resultado></Resultado>"
                        StringXml += "</ControlProceso>"
                        StringXml += "<Cabecera>"
                        StringXml += StringCabecera
                        StringXml += "</Cabecera>"
                        StringXml += "<DetallesCabecera>"
                        StringXml += StringDetatlle
                        StringXml += "</DetallesCabecera>"
                        StringXml += "</mrm:GesImpPesBal>"
                        sendBalanceData.value = StringXml
                        Dim response As WsRp3.SendBalanceDataResponse = servicioPortClient.Proporcionar_servicio(sendBalanceData)
                        Dim XmlRespuesta As String = response.SendBalanceDataResult
                        '**********************************Comparamos la Respuestas***************************
                        '**** leemos la respuesta xml****
                        leer_Xml(XmlRespuesta, Orden, Codigo)

                        MsgBox(StringXml)





                End Select
                'The example responds to all data reception with the number of bytes received;
                'you would likely change this behavior when implementing your own protocol.
                'info.SendMessage("Received " & System.Text.Encoding.ASCII.GetString(info._Buffer) & " Bytes")

                ' info.AwaitData()
                Exit Sub
            Else
                'If we cannot read from the stream, the example assumes the connection is
                'invalid and closes the client connection. You might modify this behavior
                'when implementing your own protocol.
                info.Client.Close()
                Exit Sub

            End If
        Catch ex As Exception
            info.Client.Close()
            '  info._LastReadLength = -1
            Exit Sub

        End Try
    End Sub

    Private Sub SendMessage(message As String)
        If _Stream IsNot Nothing Then
            Dim messageData() As Byte = System.Text.Encoding.ASCII.GetBytes(message)
            Stream.Write(messageData, 0, messageData.Length)
        End If
    End Sub

    Public Sub envioXML(Consulta_Dato As String(), Estado As String, UnidadesConfirmadas As Integer, PesoConfirmado As Double, LoteConfirmado As String)
        Dim IdAjusteBalanza As String
        Dim TipoTransaccion As String
        Dim idEmpresa As String
        Dim IdEstablecimineto As String
        Dim IdPuntoOperacion As String

        Dim Orden As String
        Dim Codigo As String

        'Dim IdAjuste As String
        Dim Datos_Clientes As DataSet = Consultas.Orden_Despacho(Consulta_Dato(6))
        For Each myReader As DataRow In Datos_Clientes.Tables(0).Rows
            idEmpresa = myReader(1).ToString()
            IdEstablecimineto = myReader(2).ToString()
            IdPuntoOperacion = myReader(3).ToString()
            IdAjusteBalanza = myReader(4).ToString()
            TipoTransaccion = myReader(6).ToString()

            Orden = Consulta_Dato(6).ToString()
            Codigo = Consulta_Dato(5).ToString()


        Next
        '***********************************************************


        Dim idajuste As String
        Dim codigoLN As String

        Dim Datos_Detalle As DataSet = Consultas.Detalle(Consulta_Dato(6), Consulta_Dato(5))
        For Each myReader As DataRow In Datos_Detalle.Tables(0).Rows
            idajuste = myReader(0).ToString()
            codigoLN = myReader(1).ToString()


        Next

        ' Exit Sub

        Try
            'llamo al metodo
            ' Dim proxy As New WsRp3Prueba.GesImpPesBalClient
            Dim servicioPortClient As New WsRp3.Proporcionar_servicioPortClient()
            Dim sendBalanceData As New WsRp3.SendBalanceData()
            '                            servicioPortClient.ClientCredentials = New NetworkCredential()
            servicioPortClient.ClientCredentials.UserName.UserName = Usuario
            servicioPortClient.ClientCredentials.UserName.Password = Pass

            'Generamos la estructura del xml 
            Dim StringXml As String = "<mrm:GesImpPesBal xmlns:mrm=""http://ln.gesalm.integracion.pronaca.com.ec"">"
            'Dim StringXml As String = "<mrm:GesImpPesBal xmlns:mrm=""http://ln.gesalm.integracion.pronaca.com.ec"">"

            StringXml += "<ControlProceso>"
            StringXml += "<CodigoCompania>602</CodigoCompania>"
            StringXml += "<CodigoSistema>BALANZAS</CodigoSistema>"
            StringXml += "<CodigoServicio>GESIMPPESBAL</CodigoServicio>"
            StringXml += "<Proceso>ACTUALIZAR</Proceso><Resultado></Resultado>"
            StringXml += "</ControlProceso>"
            StringXml += "<Cabecera>"
            StringXml += Cabecera(IdAjusteBalanza, TipoTransaccion, idEmpresa, IdEstablecimineto, IdPuntoOperacion, "1", "1", "", DateTime.Now.ToString("yyyy-MM-dd"))
            StringXml += "</Cabecera>"
            StringXml += "<DetallesCabecera>"
            StringXml += Detalle(IdAjusteBalanza, idajuste, Consulta_Dato(5), Consulta_Dato(5), PesoConfirmado.ToString(), Consulta_Dato(5), Consulta_Dato(1), Estado, DateTime.Now.ToString("yyyy-MM-dd"), Consulta_Dato(4), UnidadesConfirmadas.ToString(), LoteConfirmado, "1", "1", "", DateTime.Now.ToString("yyyy-MM-dd"))
            StringXml += "</DetallesCabecera>"
            StringXml += "</mrm:GesImpPesBal>"
            sendBalanceData.value = StringXml
            Dim response As WsRp3.SendBalanceDataResponse = servicioPortClient.Proporcionar_servicio(sendBalanceData)
            Dim XmlRespuesta As String = response.SendBalanceDataResult


            '**********************************Comparamos la Respuestas***************************
            '**** leemos la respuesta xml****
            leer_Xml(XmlRespuesta, Orden, Codigo)

            'si los pesajes se enviaron correctamente x el web service se pone le pesaje de compleatdo 
            'Respuesta = Consultas.Gestion_Pesos(Consulta_Dato(1), Consulta_Dato(3), Consulta_Dato(4), Consulta_Dato(5), Consulta_Dato(6), Consulta_Dato(7), Consulta_Dato(8), Consulta_Dato(10))
            'Respuesta = Consultas.Gestion_Pesos(Consulta_Dato(1), Consulta_Dato(3), Consulta_Dato(4), Consulta_Dato(5), Consulta_Dato(6), Consulta_Dato(7), Consulta_Dato(8), Consulta_Dato(10), "C")

            'envío
            MsgBox(XmlRespuesta)

        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub


End Class