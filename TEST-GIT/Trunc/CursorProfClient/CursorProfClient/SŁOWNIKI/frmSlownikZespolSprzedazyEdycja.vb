Public Class frmSlownikZespolSprzedazyEdycja
    Public dtZespoly As New DataTable
    Private Sub btnAnuluj_Click(sender As System.Object, e As System.EventArgs) Handles btnAnuluj.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btnZapisz_Click(sender As System.Object, e As System.EventArgs) Handles btnZapisz.Click

        If txtNazwaZespolu.Text = String.Empty Then
            MsgBox("Proszę podać nazwę zespołu sprzedaży!", MsgBoxStyle.Exclamation, "Brak nazwy zespółu sprzedaży")
            Exit Sub
        End If

        For Each r As DataRow In dtZespoly.Rows
            If r.Item("NAZWA") = txtNazwaZespolu.Text Then
                MsgBox("W systemie istnieje już zespół sprzedaży o nazwie: " & txtNazwaZespolu.Text, MsgBoxStyle.Exclamation, "Zespół sprzedaży")
                Exit Sub
            End If
        Next
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub frmSlownikZespolSprzedazyEdycja_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        txtNazwaZespolu.Focus()
    End Sub
End Class