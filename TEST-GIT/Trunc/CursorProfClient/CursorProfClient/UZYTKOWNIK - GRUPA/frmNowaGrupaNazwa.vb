Public Class frmNowaGrupaNazwa
    Public Nazwa As String
    Public dtGrupy As DataTable
    Private Sub frmNowaGrupaNazwa_Shown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Shown
        If Not Nazwa Is Nothing Then
            tbNazwa.Text = Nazwa
        End If
        tbNazwa.Focus()
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        If tbNazwa.Text.Length = 0 Then
            MessageBox.Show(Me, "Nazwa grupy nie moze byc pusta", "B£¥D", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            Nazwa = tbNazwa.Text
            If dtGrupy.Select(String.Format("Nazwa ='{0}'", Nazwa)).GetLength(0) > 0 Then
                MessageBox.Show(Me, "Podana grupa juz istnieje", "B£¥D", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
            End If
        End If
    End Sub

    Private Sub btnAnuluj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnuluj.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
End Class