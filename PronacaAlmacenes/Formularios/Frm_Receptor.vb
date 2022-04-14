Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports System.Threading.Tasks
Imports Negocios
Imports System.IO
Imports System.Text

Public Class Frm_Receptor
    Private Declare Function SetProcessWorkingSetSize Lib "kernel32.dll" (
  ByVal process As IntPtr,
  ByVal minimumWorkingSetSize As Integer,
  ByVal maximumWorkingSetSize As Integer) As Integer

    Private _Listener As TcpListener
    Private _Connections As New List(Of ConnectionInfo)
    Private _ConnectionMontior As Task
    '  Dim Consulta As New Negocios_Consultas()
    Dim Ruta As String = Application.StartupPath & "\Conexion.txt"


    Private Sub Crear_Archivo()
        Try
            Dim fs As FileStream
            fs = New FileStream(Ruta, FileMode.Create, FileAccess.Write)
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try
    End Sub

    Private Sub Verificar_Conexion()
        Try
            If File.Exists(Ruta) Then


            Else
                Crear_Archivo()
                Me.Hide()
                Frm_Conexion.ShowDialog()

            End If

        Catch ex As Exception
            MsgBox(ex.Message())

        End Try
    End Sub

    Private Sub Frm_Receptor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Verificar_Conexion()
    End Sub

    Private Sub StartStopButton_CheckedChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub PortTextBox_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs)
        Dim deltaPort As Integer
        If Not Integer.TryParse(1920, deltaPort) OrElse deltaPort < 1 OrElse deltaPort > 65535 Then
            MessageBox.Show("Port number must be an integer between 1 and 65535.", "Invalid Port Number", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            'PortTextBox.SelectAll()
            e.Cancel = True
        End If
    End Sub
    '**************************
    Private Sub ListenForClient(monitor As MonitorInfo)
        Dim info As New ConnectionInfo(monitor)
        _Listener.BeginAcceptTcpClient(AddressOf DoAcceptClient, info)
    End Sub
    '**************
    Private Sub DoAcceptClient(result As IAsyncResult)
        Dim monitorInfo As MonitorInfo = CType(_ConnectionMontior.AsyncState, MonitorInfo)
        If monitorInfo.Listener IsNot Nothing AndAlso Not monitorInfo.Cancel Then
            Dim info As ConnectionInfo = CType(result.AsyncState, ConnectionInfo)
            monitorInfo.Connections.Add(info)
            info.AcceptClient(result)
            ListenForClient(monitorInfo)
            info.AwaitData()
            Dim doUpdateConnectionCountLabel As New Action(AddressOf UpdateConnectionCountLabel)
            Invoke(doUpdateConnectionCountLabel)
        End If
    End Sub
    '*******
    Private Sub DoMonitorConnections()
        'Create delegate for updating output display
        Dim doAppendOutput As New Action(Of String)(AddressOf AppendOutput)
        'Create delegate for updating connection count label
        Dim doUpdateConnectionCountLabel As New Action(AddressOf UpdateConnectionCountLabel)

        'Get MonitorInfo instance from thread-save Task instance
        Dim monitorInfo As MonitorInfo = CType(_ConnectionMontior.AsyncState, MonitorInfo)

        'Report progress
        'Me.Invoke(doAppendOutput, "Monitor Started.")

        'Implement client connection processing loop
        'Do
        'Create temporary list for recording closed connections
        Dim lostCount As Integer = 0
        'Examine cada conexión para su procesamiento
        For index As Integer = monitorInfo.Connections.Count - 1 To 0 Step -1
            Dim info As ConnectionInfo = monitorInfo.Connections(index)
            If info.Client.Connected = True And info.Client.Connected <> Nothing Then
                'Proceso del cliente connectado 
                If info.DataQueue.Count > 0 Then
                    'El código en este If-Block debe ser modificado para construir objetos
                    ' mensaje Según el protocolo que haya definido para sus transmisiones de datos.
                    'Este ejemplo simplemente envía todos los bytes de mensajes pendientes al cuadro de texto de salida.
                    'Sin un protocolo no podemos saber qué constituye un mensaje completo, por lo que
                    'con varios clientes activos pudimos ver parte del primer mensaje de client1,
                    'luego parte de un mensaje del cliente2, seguido del resto de clientes1
                    'primer mensaje (asumiendo que cliente1 envió más de 64 bytes).
                    Dim messageBytes As New List(Of Byte)
                    While info.DataQueue.Count > 0
                        Dim value As Byte
                        If info.DataQueue.TryDequeue(value) Then
                            messageBytes.Add(value)
                        End If
                    End While
                    Me.Invoke(doAppendOutput, System.Text.Encoding.ASCII.GetString(messageBytes.ToArray))
                End If
            Else
                'Clean-up any closed client connections
                monitorInfo.Connections.Remove(info)
                lostCount += 1
            End If
        Next
        If lostCount > 0 Then
            Invoke(doUpdateConnectionCountLabel)
        End If

        'Throttle loop to avoid wasting CPU time
        _ConnectionMontior.Wait(1)
        ' Loop While Not monitorInfo.Cancel

        'Close all connections before exiting monitor
        For Each info As ConnectionInfo In monitorInfo.Connections
            info.Client.Close()
        Next
        monitorInfo.Connections.Clear()

        'Update the connection count label and report status
        Invoke(doUpdateConnectionCountLabel)
        'Me.Invoke(doAppendOutput, "Monitor Stopped.")

    End Sub
    '*****************

    'RECIBIMOS EL MENSAJE VAMOS LLENANDO LAS CAJAS DE TEXTO
    Private Sub AppendOutput(message As String)

        '  Txt_Consulta.Text += message


    End Sub
    '*********************
    Private Sub UpdateConnectionCountLabel()
        '****
        '  ConnectionCountLabel.Text = String.Format("{0} Connections", _Connections.Count)
    End Sub

    Private Sub Btn_Conectar_Click(sender As Object, e As EventArgs) Handles Btn_Conectar.Click
        If Btn_Conectar.Text = "Start" Then
            Btn_Conectar.Text = "Stop"
            ' StartStopButton.Image = My.Resources.Resources.StopServer
            _Listener = New TcpListener(IPAddress.Any, CInt(1920))
            _Listener.Start()
            Dim monitor As New MonitorInfo(_Listener, _Connections)
            ListenForClient(monitor)
            _ConnectionMontior = Task.Factory.StartNew(AddressOf DoMonitorConnections, monitor, TaskCreationOptions.LongRunning)
            Lbl_Estado.Text = "CONECTADO"
            Lbl_Estado.ForeColor = Color.Green
        Else
            Btn_Conectar.Text = "Start"
            'StartStopButton.Image = My.Resources.Resources.StartServer
            CType(_ConnectionMontior.AsyncState, MonitorInfo).Cancel = True
            _Listener.Stop()
            _Listener = Nothing
            Lbl_Estado.Text = "DESCONECTADO"
            Lbl_Estado.ForeColor = Color.Red
        End If
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub
End Class
'Provides a simple container object to be used for the state object passed to the connection monitoring thread



