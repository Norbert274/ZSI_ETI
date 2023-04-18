Public Class frmSlownikSiecSprzedazyEdycja
    Public dtSieci As New DataTable
    Private Sub btnAnuluj_Click(sender As System.Object, e As System.EventArgs) Handles btnAnuluj.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btnZapisz_Click(sender As System.Object, e As System.EventArgs) Handles btnZapisz.Click

        If txtNazwaSieci.Text = String.Empty Then
            MsgBox("Proszę podać nazwę sieci sprzedaży!", MsgBoxStyle.Exclamation, "Brak nazwy sieci sprzedaży")
            Exit Sub
        End If

        For Each r As DataRow In dtSieci.Rows
            If r.Item("NAZWA") = txtNazwaSieci.Text Then
                MsgBox("W systemie istnieje już sieć sprzedaży o nazwie: " & txtNazwaSieci.Text, MsgBoxStyle.Exclamation, "Sieć sprzedaży")
                Exit Sub
            End If
        Next
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub frmSlownikSiecSprzedazyEdycja_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        txtNazwaSieci.Focus()
    End Sub
End Class