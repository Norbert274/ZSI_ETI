Imports System.Windows.Forms

Public Class frmGaleriaDialog
    Public sku_id As Integer
    Public czy_otwarta As Boolean = False

  

    Public Sub wczytaj_zdjecia()
        CtrImgGaleriaDialog.inputIdSKU = sku_id
        CtrImgGaleriaDialog.nr_biezacego_zdjecia = 1
        CtrImgGaleriaDialog.PokazZdjecie()
        'Me.Location = New Point(32, 34)
    End Sub

    Private Sub btnZamknij_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZamknij.Click
        Me.Close()
    End Sub

    Private Sub frmGaleriaDialog_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'For Each _f As Form In frmGlowna.MdiChildren
        '    If _f.Name = "frmStan" Then
        '        frmStan.ctr.czy_otwarta_Galeria(False)
        '        frmStan.sprawdz_czy_byl_mouse_move(False)
        '    End If
        'Next
    End Sub

    Private Sub frmGaleriaDialog_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub frmGaleriaDialog_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        'For Each _f As Form In frmGlowna.MdiChildren
        '    If _f.Name = "frmStan" Then
        '        frmStan.ctr.czy_otwarta_Galeria(False)
        '    End If
        'Next
    End Sub
End Class