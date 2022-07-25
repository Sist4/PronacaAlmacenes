Imports System.Configuration
Imports System.Reflection
Imports Microsoft.IdentityModel.Protocols

Public Class Datos_Conexion
    Public Function String_Conexion() As String
        Dim Conexion As String = ConfigurationManager.AppSettings("Conexion")
        'Return Conexion
        'Return "data source =DESKTOP-48JP9GK\SQL2019ES; initial catalog = PRONACA_TIENDAS; user id = sa; password = Sistemas123"

        'Return "data source = 1S1-INTEGRARP3; initial catalog = PRONACA_TIENDAS; User Id=sa;Password=Pronaca2020" '
        'Return "data source = 1S1-INTEGRARP3; initial catalog = Pronaca_Almacenes; User Id=Precitrol;Password=Precitrol2022" '
        Return "data source = PRECITROLSIST4\SQLEXPRESS; initial catalog = Pronaca_Almacenes; User Id=Precitrol;Password=Precitrol2022"
        'Return "data source = 1S1-INTEGRARP3; initial catalog = Pronaca_Almacenes; User Id=Precitrol;Password=Precitrol2022" '
    End Function




End Class
