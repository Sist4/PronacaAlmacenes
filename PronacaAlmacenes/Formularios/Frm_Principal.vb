Public Class Frm_Principal
    Private Sub MnPesaje_Click(sender As Object, e As EventArgs) Handles MnPesaje.Click
        With Frm_Pesaje
            .TopLevel = False
            PnlPrincipal.Controls.Add(Frm_Pesaje)
            .BringToFront()
            .Show()
        End With
    End Sub
End Class