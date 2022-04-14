Imports System.Data.SqlClient
Imports System.Data.OleDb
Public Class Frm_Pesaje
    Dim strComando As String
    Dim conexion As String
    Dim adapter As SqlDataAdapter
    Dim data As DataSet
    Private CadenaSql As New Datos_Conexion()
    Private Sub Frm_Pesaje_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conexion = CadenaSql.String_Conexion()
        strComando = "SELECT *from Cabecera WHERE Cab_Estado='A'"
        adapter = New System.Data.SqlClient.SqlDataAdapter(strComando, conexion)
        data = New DataSet
        adapter.Fill(data)
        cmbOrden.DataSource = data.Tables(0)
        cmbOrden.DisplayMember = "Od_OrdenDespcho"
        cmbOrden.ValueMember = "Cab_Codigo"

    End Sub
End Class