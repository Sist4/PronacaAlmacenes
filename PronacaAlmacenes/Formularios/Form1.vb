Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim Consultas As New Negocios_Consultas()
        Dim Orden As String = txtOrden.Text
        Dim Producto As String = txtSKU.Text
        Dim resultado_detalle As String = "EXITO"
        Dim res As Integer = Consultas.CambiarEstadoEnvio(Orden, Producto, resultado_detalle)
        MessageBox.Show("se guardo")
    End Sub
End Class