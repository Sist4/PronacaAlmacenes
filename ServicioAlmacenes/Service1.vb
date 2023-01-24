Imports System.Net
Imports System.Net.Sockets

Public Class Service1
    Private Declare Function SetProcessWorkingSetSize Lib "kernel32.dll" (
  ByVal process As IntPtr,
  ByVal minimumWorkingSetSize As Integer,
  ByVal maximumWorkingSetSize As Integer) As Integer

    Private _Listener As TcpListener
    Private _Connections As New List(Of ConnectionInfo)
    Private _ConnectionMontior As Task

    Private Sub ListenForClient(monitor As MonitorInfo)
        Dim info As New ConnectionInfo(monitor)
        _Listener.BeginAcceptTcpClient(AddressOf DoAcceptClient, info)
    End Sub

    Private Sub DoAcceptClient(result As IAsyncResult)
        Dim monitorInfo As MonitorInfo = CType(_ConnectionMontior.AsyncState, MonitorInfo)
        If monitorInfo.Listener IsNot Nothing AndAlso Not monitorInfo.Cancel Then
            Dim info As ConnectionInfo = CType(result.AsyncState, ConnectionInfo)
            monitorInfo.Connections.Add(info)
            info.AcceptClient(result)
            ListenForClient(monitorInfo)
            info.AwaitData()
        End If
    End Sub

    Private Sub DoMonitorConnections()

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
                End If
            Else
                'Clean-up any closed client connections
                monitorInfo.Connections.Remove(info)
                lostCount += 1
            End If
        Next


        'Throttle loop to avoid wasting CPU time
        _ConnectionMontior.Wait(1)
        ' Loop While Not monitorInfo.Cancel

        'Close all connections before exiting monitor
        For Each info As ConnectionInfo In monitorInfo.Connections
            info.Client.Close()
        Next
        monitorInfo.Connections.Clear()


    End Sub
    Protected Overrides Sub OnStart(ByVal args() As String)
        ' Agregue el código aquí para iniciar el servicio. Este método debería poner
        ' en movimiento los elementos para que el servicio pueda funcionar.

        ' StartStopButton.Image = My.Resources.Resources.StopServer
        _Listener = New TcpListener(IPAddress.Any, CInt(1923))
        _Listener.Start()
        Dim monitor As New MonitorInfo(_Listener, _Connections)
        ListenForClient(monitor)
        _ConnectionMontior = Task.Factory.StartNew(AddressOf DoMonitorConnections, monitor, TaskCreationOptions.LongRunning)
    End Sub

    Protected Overrides Sub OnStop()
        ' Agregue el código aquí para realizar cualquier anulación necesaria para detener el servicio.
        CType(_ConnectionMontior.AsyncState, MonitorInfo).Cancel = True
        _Listener.Stop()
        _Listener = Nothing
    End Sub

End Class
