Public Class frmKomunikatEdycja
    Public rtf_komunikat As String = ""
    Private Sub btnZamknij_Click(sender As System.Object, e As System.EventArgs) Handles btnZamknij.Click
        If bylyZmiany() = True Then
            Dim result As DialogResult = MsgBox("Od ostatniego odczytu z serwera wprowadzono zmiany w tym komunikacie. Czy zapisać wprowadzone zmiany?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1, Me.Text)

            If result = Windows.Forms.DialogResult.No Then
                Me.Close()
            Else
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
            End If
        End If

        Me.Close()
    End Sub

    Private Sub btnZapisz_Click(sender As System.Object, e As System.EventArgs) Handles btnZapisz.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Function bylyZmiany() As Boolean
        If ctrEdytorKomunikatu.rtbTekst.Rtf <> rtf_komunikat Then
            Return True
        End If
        Return False

    End Function

    Private Sub frmKomunikatEdycja_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        ctrEdytorKomunikatu.rtbTekst.Rtf = rtf_komunikat
    End Sub
End Class