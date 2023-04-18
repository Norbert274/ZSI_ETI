Public Class frmDaneDpdUprawnienia
    Public user_id As Integer
    Private Dok_Zwrotne_Visible As Integer
    Private Dok_Zwrotne_Enable As Integer
    Private Prz_Zwrotne_Visible As Integer
    Private Prz_Zwrotne_Enable As Integer
    Private Osob_Pryw_Visible As Integer
    Private Osob_Pryw_Enable As Integer
    Private Wartosc_Visible As Integer
    Private Wartosc_Enable As Integer
    Private COD_Visible As Integer
    Private COD_Enable As Integer
    Private Dost_Gw_Visible As Integer
    Private Dost_Gw_Enable As Integer



    Private Function wczytaj() As Boolean
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.UserDaneDpdWczytajWynik
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.UserDaneDpdWczytaj(frmGlowna.sesja, user_id)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Return False
            Exit Function
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
            Return False
            Exit Function
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
        End If
        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
            Return False
            Exit Function
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
        End If

        Dok_Zwrotne_Visible = wsWynik.Dok_Zwrotne_Visible
        chkDokZwrotneVisible.Checked = Dok_Zwrotne_Visible = 1

        Dok_Zwrotne_Enable = wsWynik.Dok_Zwrotne_Enable
        chkDokZwrotneEnabled.Checked = Dok_Zwrotne_Enable = 1

        Prz_Zwrotne_Visible = wsWynik.Prz_Zwrotne_Visible
        chkPrzZwrotnaVisable.Checked = Prz_Zwrotne_Visible = 1

        Prz_Zwrotne_Enable = wsWynik.Prz_Zwrotne_Enable
        chkPrzZwrotnaEnabled.Checked = Prz_Zwrotne_Enable = 1

        Osob_Pryw_Visible = wsWynik.Osob_Pryw_Visible
        chkOsPrywVisable.Checked = Osob_Pryw_Visible = 1

        Osob_Pryw_Enable = wsWynik.Osob_Pryw_Enable
        chkOsPrywEnabled.Checked = Osob_Pryw_Enable = 1

        Wartosc_Visible = wsWynik.Wartosc_Visible
        chkWartoscVisable.Checked = Wartosc_Visible = 1

        Wartosc_Enable = wsWynik.Wartosc_Enable
        chkWartoscEnabled.Checked = Wartosc_Enable = 1

        COD_Visible = wsWynik.COD_Visible
        chkCODVisable.Checked = COD_Visible = 1

        COD_Enable = wsWynik.COD_Enable
        chkCODEnabled.Checked = COD_Enable = 1

        Dost_Gw_Visible = wsWynik.Dost_Gw_Visible
        chkDorGwVisable.Checked = Dost_Gw_Visible = 1

        Dost_Gw_Enable = wsWynik.Dost_Gw_Enable
        chkDorGwEnabled.Checked = Dost_Gw_Enable = 1

        Return True

        Return True
    End Function



    Private Sub zapisz()
        If chkCODVisable.Checked = False And chkDokZwrotneVisible.Checked = False And chkDorGwVisable.Checked = False And chkOsPrywVisable.Checked = False And chkPrzZwrotnaVisable.Checked = False And chkWartoscVisable.Checked = False Then
            MsgBox("Przynajmniej jedno pole musi być oznaczone jako widoczne", MsgBoxStyle.Critical, Me.Text)
            Exit Sub
        End If
        If chkCODEnabled.Checked = False And chkDokZwrotneEnabled.Checked = False And chkDorGwEnabled.Checked = False And chkOsPrywEnabled.Checked = False And chkPrzZwrotnaEnabled.Checked = False And chkWartoscEnabled.Checked = False Then
            MsgBox("Przynajmniej jedno pole musi być oznaczone jako włączone", MsgBoxStyle.Critical, Me.Text)
            Exit Sub
        End If
        If chkDokZwrotneVisible.Checked = Dok_Zwrotne_Visible = 1 _
           And chkDokZwrotneEnabled.Checked = Dok_Zwrotne_Enable = 1 _
           And chkPrzZwrotnaVisable.Checked = Prz_Zwrotne_Visible = 1 _
           And chkPrzZwrotnaEnabled.Checked = Prz_Zwrotne_Enable = 1 _
           And chkOsPrywVisable.Checked = Osob_Pryw_Visible = 1 _
           And chkOsPrywEnabled.Checked = Osob_Pryw_Enable = 1 _
           And chkWartoscVisable.Checked = Wartosc_Visible = 1 _
           And chkWartoscEnabled.Checked = Wartosc_Enable = 1 _
           And chkCODVisable.Checked = COD_Visible = 1 _
           And chkCODEnabled.Checked = COD_Enable = 1 _
           And chkDorGwVisable.Checked = Dost_Gw_Visible = 1 _
           And chkDorGwEnabled.Checked = Dost_Gw_Enable = 1 Then
            MsgBox("Dane nie zmieniły sie zapis nie był potrzebny", MsgBoxStyle.Information, Me.Text)
            Exit Sub
        End If

        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.UserDaneDpdZapiszWynik
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.UserDaneDpdZapisz(frmGlowna.sesja, user_id, IIf(chkDokZwrotneVisible.Checked, 1, 0), _
            IIf(chkDokZwrotneEnabled.Checked, 1, 0), IIf(chkPrzZwrotnaVisable.Checked, 1, 0), IIf(chkPrzZwrotnaEnabled.Checked, 1, 0), _
            IIf(chkOsPrywVisable.Checked, 1, 0), IIf(chkOsPrywEnabled.Checked, 1, 0), IIf(chkWartoscVisable.Checked, 1, 0), _
            IIf(chkWartoscEnabled.Checked, 1, 0), IIf(chkCODVisable.Checked, 1, 0), IIf(chkCODEnabled.Checked, 1, 0), _
            IIf(chkDorGwVisable.Checked, 1, 0), IIf(chkDorGwEnabled.Checked, 1, 0))
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("Błąd komunikacji z serwerem" & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)

            Exit Sub
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)

            Exit Sub
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
        End If
        MsgBox("Dane zapisano", MsgBoxStyle.Information, Me.Text)
        Me.Close()

    End Sub


    Private Sub btnZapisz_Click(sender As Object, e As System.EventArgs) Handles btnZapisz.Click
        zapisz()
    End Sub

    Private Sub btnAnuluj_Click(sender As Object, e As System.EventArgs) Handles btnAnuluj.Click
        Me.Close()
    End Sub

    
    Private Sub frmDaneDpdUprawnienia_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        If Not wczytaj() = True Then
            Me.Close()
        End If
    End Sub

    
    Private Sub chkCODEnabled_Click(sender As Object, e As System.EventArgs) Handles chkCODEnabled.Click
        If chkCODEnabled.Checked = True Then
            chkCODVisable.Checked = True
        End If
    End Sub

    Private Sub chkCODVisable_Click(sender As Object, e As System.EventArgs) Handles chkCODVisable.Click
        If chkCODVisable.Checked = False Then
            chkCODEnabled.Checked = False
        End If
    End Sub

    Private Sub chkWartoscEnabled_Click(sender As Object, e As System.EventArgs) Handles chkWartoscEnabled.Click
        If chkWartoscEnabled.Checked = True Then
            chkWartoscVisable.Checked = True
        End If
    End Sub

    Private Sub chkWartoscVisable_Click(sender As Object, e As System.EventArgs) Handles chkWartoscVisable.Click
        If chkWartoscVisable.Checked = False Then
            chkWartoscEnabled.Checked = False
        End If
    End Sub

    Private Sub chkOsPrywEnabled_Click(sender As Object, e As System.EventArgs) Handles chkOsPrywEnabled.Click
        If chkOsPrywEnabled.Checked = True Then
            chkOsPrywVisable.Checked = True
        End If
    End Sub

    Private Sub chkOsPrywVisable_Click(sender As Object, e As System.EventArgs) Handles chkOsPrywVisable.Click
        If chkOsPrywVisable.Checked = False Then
            chkOsPrywEnabled.Checked = False
        End If
    End Sub

    Private Sub chkDokZwrotneEnabled_Click(sender As Object, e As System.EventArgs) Handles chkDokZwrotneEnabled.Click
        If chkDokZwrotneEnabled.Checked = True Then
            chkDokZwrotneVisible.Checked = True
        End If
    End Sub

    Private Sub chkDokZwrotneVisable_Click(sender As Object, e As System.EventArgs) Handles chkDokZwrotneVisible.Click
        If chkDokZwrotneVisible.Checked = False Then
            chkDokZwrotneEnabled.Checked = False
        End If
    End Sub

    Private Sub chkDorGwEnabled_Click(sender As Object, e As System.EventArgs) Handles chkDorGwEnabled.Click
        If chkDorGwEnabled.Checked = True Then
            chkDorGwVisable.Checked = True
        End If
    End Sub

    Private Sub chkDorGwVisable_Click(sender As Object, e As System.EventArgs) Handles chkDorGwVisable.Click
        If chkDorGwVisable.Checked = False Then
            chkDorGwEnabled.Checked = False
        End If
    End Sub

    Private Sub chkPrzZwrotnaEnabled_Click(sender As Object, e As System.EventArgs) Handles chkPrzZwrotnaEnabled.Click
        If chkPrzZwrotnaEnabled.Checked = True Then
            chkPrzZwrotnaVisable.Checked = True
        End If
    End Sub

    Private Sub chkPrzZwrotnaVisable_Click(sender As Object, e As System.EventArgs) Handles chkPrzZwrotnaVisable.Click
        If chkPrzZwrotnaVisable.Checked = False Then
            chkPrzZwrotnaEnabled.Checked = False
        End If
    End Sub
End Class