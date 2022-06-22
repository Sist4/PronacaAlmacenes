Imports System.Configuration
Imports System.Reflection
Imports Microsoft.IdentityModel.Protocols

Public Class Datos_Conexion
    Public Function String_Conexion() As String
        Dim Conexion As String = ConfigurationManager.AppSettings("Conexion")
        'Return Conexion
        'Return "data source =DESKTOP-48JP9GK\SQL2019ES; initial catalog = PRONACA_TIENDAS; user id = sa; password = Sistemas123"

        'Return "data source = 1S1-INTEGRARP3; initial catalog = PRONACA_TIENDAS; User Id=sa;Password=Pronaca2020" '
        Return "data source = PRECITROL\SQLEXPRESS; initial catalog = PRONACA_TIENDAS; User Id=sa;Password=Sistemas123"
    End Function




End Class
