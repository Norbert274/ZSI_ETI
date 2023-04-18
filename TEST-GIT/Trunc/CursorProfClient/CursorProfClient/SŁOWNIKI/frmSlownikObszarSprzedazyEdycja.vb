Public Class frmSlownikObszarSprzedazyEdycja
    Public dtObszary As New DataTable
    Private Sub btnAnuluj_Click(sender As System.Object, e As System.EventArgs) Handles btnAnuluj.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btnZapisz_Click(sender As System.Object, e As System.EventArgs) Handles btnZapisz.Click

        If txtNazwaObszaru.Text = String.Empty Then
            MsgBox("Proszę podać nazwę obszaru sprzedaży!", MsgBoxStyle.Exclamation, "Brak nazwy obszaru sprzedaży")
            Exit Sub
        End If

        For Each r As DataRow In dtObszary.Rows
            If r.Item("NAZWA") = txtNazwaObszaru.Text Then
                MsgBox("W systemie istnieje już obszar sprzedaży o nazwie: " & txtNazwaObszaru.Text, MsgBoxStyle.Exclamation, "Obszar sprzedaży")
                Exit Sub
            End If
        Next
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub frmSlownikObszarSprzedazyEdycja_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        txtNazwaObszaru.Focus()
    End Sub
End Class