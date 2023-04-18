Public Class frmNowaWiadomosc
    Public dtWiadomosci As DataTable
    Public strTytul As String
    Public strTresc As String
    Private Sub btnZamknij_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZamknij.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btnZapiszWiadomosc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZapiszWiadomosc.Click
        If txtTytul.Text.Length = 0 Then
            MessageBox.Show(Me, "Tytuł wiadomości nie moze być pusty.", "BŁĄD", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ElseIf rtxtTresc.Text.Length = 0 Then
            MessageBox.Show(Me, "Treść wiadomości nie moze być pusta.", "BŁĄD", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            strTytul = txtTytul.Text
            strTresc = rtxtTresc.Text
            'If dtWiadomosci.Select(String.Format("TYTUL ='{0}'", strTytul)).GetLength(0) > 0 Then
            '    MessageBox.Show(Me, "Podany tytuł wiadomości juz istnieje", "BŁĄD", MessageBoxButtons.OK, MessageBoxIcon.Error)
            'Else
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
            'End If
        End If
    End Sub

    Private Sub frmNowaWiadomosc_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        txtTytul.Focus()
    End Sub
End Class