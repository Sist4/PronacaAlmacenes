Public Class Frm_Pruebas

    'Dim webService As New WsRp3.ServiceClient()

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click




        '   Dim svc_ZWS_BAS_01 As New WsRp3.SendBalanceData


        'cargo los datos en el objeto (datos que envio para que me responda)
        
        'llamo al metodo
        'Dim proxy As New WsRp3.ServiceClient()
        'le paso el usuario y el password
        'proxy.ClientCredentials.UserName.UserName = "********"
        'proxy.ClientCredentials.UserName.Password = "********"

        'obtengo el resultado de las variables
        ' Dim resultado = proxy.SendBalanceData("<![CDATA[<Balance IdAjuste=""1"" CodigoLN=""1000000"" SKU=""0305"" UnidadesConfirmadas=""30"" PesoConfirmado=""30.30"" TarCodigo=""3"" TraOperador=""3"" TraFecha=""2020-01-30"" TraBalanza=""3"" TraEstado=""E""></Balance>]]>")
        'MsgBox(resultado.Data)
        ''MsgBox(resultado.O_BAS_ERROR.MESSAGE)
        ''MsgBox(resultado.O_BAS_HEADER.MAKT_HDR)




        'webService.SendBalanceData("<![CDATA[<Balance IdAjuste=""1"" CodigoLN=""1000000"" SKU=""0305"" UnidadesConfirmadas=""30"" PesoConfirmado=""30.30"" TarCodigo=""3"" TraOperador=""3"" TraFecha=""2020-01-30"" TraBalanza=""3"" TraEstado=""E""></Balance>]]>")




    End Sub
End Class