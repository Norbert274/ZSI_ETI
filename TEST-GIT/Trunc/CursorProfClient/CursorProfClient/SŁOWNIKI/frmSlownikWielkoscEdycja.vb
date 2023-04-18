Public Class frmSlownikWielkoscEdycja
    Public dtDane As New DataTable
    Private Sub btnAnuluj_Click(sender As System.Object, e As System.EventArgs) Handles btnAnuluj.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub


    Private Sub btnZapisz_Click(sender As System.Object, e As System.EventArgs) Handles btnZapisz.Click

        If txtNazwa.Text = String.Empty Then
            MsgBox("Proszę podać nazwę wielkości!", MsgBoxStyle.Exclamation, "Brak nazwy wielkości")
            Exit Sub
        End If

        For Each r As DataRow In dtDane.Rows
            If r.Item("NAZWA") = txtNazwa.Text Then
                MsgBox("W systemie istnieje już wielkość o nazwie: " & txtNazwa.Text, MsgBoxStyle.Exclamation, "wielkość")
                Exit Sub
            End If
        Next
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub frmSlownikWielkoscEdycja_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        txtNazwa.Focus()
    End Sub
End Class