Imports System.Reflection
Imports System


Public Enum frmZamowienieTrybPracy
    edycjaWlasnegoKoszyka = 1
    podgladObcegoKoszyka = 2
    podgladZamowienia = 3
End Enum

Public Enum frmZamowienieTypZamowienia
    transfer = 1
    odbiorWlasny = 2
    dostawaNaAdresZdefiniowany = 3
    dostawaNaAdresInny = 4
    wydanieSpecjalneNaUtylizacje = 5
    'wydanieSpecjalneRemontStandow = 6
    'wydanieSpecjalneKlasycznyCopacking = 7
    zamowienieGrupowe = 7
    odbiorWlasnyOddzial = 6
End Enum

Public Class frmZamowienie
    Inherits frmBase
    Public frmRodzic As Form
    Public strFunkcjaPowiadomieniaOGotowosci As String = Nothing 'jeœli zmienna ustawiona, to znaczy ¿e mamy notyfikowaæ okno rodzica o zakoñczeniu przygotowywania ekranu
    Public intIdZamowienia As Integer = -1 'tylko do odczytu: przekazane z formy rodzica; jeœli =-1 to znaczy koszyk
    Private intIdBlokady As Integer = -1 'tylko do odczytu: id blokady wygenerowane w bazie na potrzeby edycji tego rekordu; -1 oznacza brak blokady
    Private strUserNazwa As String 'nazwa u¿ytkownika do którego nale¿y zamówienie
    Private enumTrybPracy As frmZamowienieTrybPracy 'tylko do odczytu: œwiadczy o tym, jak rysowaæ ekran
    Private bCentrala As Boolean 'tylko do odczytu: czy u¿ytkownik jest cz³onkiem grupy centrala?
    Private dtMagazyny As DataTable 'magazyny do pokazania w ComboBox
    Private dtOddzialy As DataTable 'magazyny do pokazania w ComboBox
    Private dtAdresy As DataTable 'adresy zdefiniowane do pokazania w ComboBox
    Private enumTypZamowienia As frmZamowienieTypZamowienia 'rodzaj zamówienia (odbiór w³asny, dostawa, itp.)
    Private bReagujNaZmianyComboMagazyn As Boolean = True
    Private intIdOstatnioWybranegoMagazynu As Integer = -1
    Public intIdUzytkownika As Integer = -1
    'kopia zmiennych z momentu wczytania z bazy
    Public intIdMagazynu As Integer = -1
    Private enumTypZamowieniaKopia As frmZamowienieTypZamowienia
    Private intIdMagazynuDocelowego As Integer = -1
    Private intIdOddzialuDocelowego As Integer = -1
    Private intIdAdresuZdefiniowanego As Integer = -1
    Private strNazwa As String
    Private strAdres As String
    Private strKod As String
    Private strMiasto As String
    Private dtAdresyGrupowe As DataTable
    Private dtAdresyGrupoweKopia As DataTable
    Private strOsobaKontaktowa As String
    Private strTelefonKontaktowy As String
    Private strUwagi As String
    Private dataRealizacji As DateTime
    Private kosztDostawy As Decimal
    Public dtPozycjeZamowienia As DataTable 'pozycje zamówienia u¿ytkownika w momencie otwarcia okna
    Public BlokujZapisz As Boolean = False
    Dim Parametry As New ZmienneGlobalne
    Public idMagazyn As Integer = Parametry.idMagazyn
	Private intDokZw As Integer
    Private intPrzZw As Integer
    Private intOsPryw As Integer
    Private dblWartosc As Decimal
    Private dblCOD As Decimal
    Private strTypDPD As String
    Private intDokZwOrg As Integer
    Private intPrzZwOrg As Integer
    Private intOsPrywOrg As Integer
    Private dblWartoscOrg As Decimal
    Private dblCODOrg As Decimal
    Private strTypDPDOrg As String
    Public bMamDaneDPD As Boolean
    Private dtkodyPocztowe As DataTable
    Private bKodvalid As Boolean = False
    Private users_ids As String
    Private warunek_grupowy As String = ""
    Private grupy As String = String.Empty
    Private typy As String = String.Empty
    Private wielkosc As String = String.Empty
    Private strusers_ids As String
    Private strgrupy As String
    Private strtypy As String
    Private strwielkosc As String
	
    Private odKodPocz As Boolean = False
    Private bFormShown As Boolean = False

    Public blue As Color = Color.DodgerBlue
    Public CzyUserOddzial As Boolean = False
    Private intUwagiPozostaloZnakow As Integer = 170

    Private uzywajComboAdresy As Boolean = False

    Private Function sprawdz_czy_user_typ_oddzial() As Boolean
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.CzyUserOddzialWynik

        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.CzyUserOddzial(frmGlowna.sesja)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("B³¹d komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegó³y b³êdu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Me.Close()
            Return False
            Exit Function
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
            Return False
        End If
        If wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
        End If
        If wsWynik.status = 0 Then
            If wsWynik.czy_oddzial = 1 Then
                CzyUserOddzial = True
            ElseIf wsWynik.czy_oddzial = 0 Then
                CzyUserOddzial = False
            Else
                CzyUserOddzial = True
            End If
        Else
            MsgBox("Nie ustalono typu", MsgBoxStyle.Exclamation, Me.Text)
        End If
        Return True
    End Function

    Private Function doczytajMagazyny() As Boolean
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.MagazynyOdczytajWynik

        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.MagazynyOdczytaj(frmGlowna.sesja)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("B³¹d komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegó³y b³êdu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Me.Close()
            Return False
            Exit Function
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
            Return False
        End If
        If wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
        End If
        dtMagazyny = wsWynik.dane.Tables(0)

        Return True
    End Function

    Private Function doczytajOddzialy() As Boolean
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.OddzialyOdczytajWynik

        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.OddzialyOdczytaj(frmGlowna.sesja)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("B³¹d komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegó³y b³êdu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Me.Close()
            Return False
            Exit Function
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
            Return False
        End If
        If wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
        End If
        dtOddzialy = wsWynik.dane.Tables(0).Clone
        If enumTrybPracy = frmZamowienieTrybPracy.edycjaWlasnegoKoszyka Then
            'dtOddzialy = wsWynik.dane.Tables(0)
            For Each row As DataRow In wsWynik.dane.Tables(0).Select("Aktywny = 1")
                dtOddzialy.ImportRow(row)
            Next
        Else
            If intIdOddzialuDocelowego > -1 Then
                For Each row As DataRow In wsWynik.dane.Tables(0).Select("oddzial_id = " & CStr(intIdOddzialuDocelowego))
                    dtOddzialy.ImportRow(row)
                Next
            End If
        End If

        Return True
    End Function

    Private Function doczytajAdresy() As Boolean
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.AdresyZamowieniaOdczytajWynik

        'If intIdAdresuZdefiniowanego < 0 Then Return True
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.AdresyZamowieniaOdczytaj(frmGlowna.sesja, intIdZamowienia, intIdAdresuZdefiniowanego)
            uzywajComboAdresy = wsWynik.wyswietlaj_combo
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("B³¹d komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegó³y b³êdu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Me.Close()
            Return False
            Exit Function
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
            Return False
        End If
        If wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
        End If
        dtAdresy = wsWynik.dane.Tables(0)

        Return True
    End Function



    Private Sub odbiorWlasny()
        enumTypZamowienia = frmZamowienieTypZamowienia.odbiorWlasny
        'pod³¹czamy listê magazynów do Combo

        If cmbMagazynOdbiorWlasny.DataSource Is Nothing Then
            If dtMagazyny Is Nothing Then
                If Not doczytajMagazyny() Then Exit Sub
            End If
            cmbMagazynOdbiorWlasny.ValueMember = "magazyn_id"
            cmbMagazynOdbiorWlasny.DisplayMember = "nazwa"
            cmbMagazynOdbiorWlasny.DataSource = dtMagazyny.Copy
        End If


        If enumTrybPracy = frmZamowienieTrybPracy.edycjaWlasnegoKoszyka Then
            cmbMagazynOdbiorWlasny.Enabled = True
            txtUwagi.Enabled = True
            txtOsobaKontaktowa.Enabled = True
            txtTelefonKontaktowy.Enabled = True
            btnZapiszZmiany.Enabled = True
            btnZapiszZmiany.BackColor = blue
        End If

        'wy³¹czamy resztê kontrolek
        cmbMagazynOdbiorWlasnyDPD.Enabled = False
        cmbDostawaNaZdefiniowanyAdres.Enabled = False
        sbSzukajAdresu.Enabled = False
        txtNazwa.Enabled = False
        txtAdres.Enabled = False
        txtKodPocztowy.Enabled = False
        txtMiasto.Enabled = False
        btnDaneDpd.Enabled = False
        lblOdbiorcy.Enabled = False
        btnOdbiorcy.Enabled = False


    End Sub

    Private Sub odbiorWlasnyOddzial()
        enumTypZamowienia = frmZamowienieTypZamowienia.odbiorWlasnyOddzial
        'pod³¹czamy listê magazynów do Combo
        If cmbMagazynOdbiorWlasnyDPD.DataSource Is Nothing Then
            If dtOddzialy Is Nothing Then
                If Not doczytajOddzialy() Then Exit Sub
            End If
            cmbMagazynOdbiorWlasnyDPD.ValueMember = "oddzial_id"
            cmbMagazynOdbiorWlasnyDPD.DisplayMember = "nazwa"
            cmbMagazynOdbiorWlasnyDPD.DataSource = dtOddzialy.Copy
        End If

        If enumTrybPracy = frmZamowienieTrybPracy.edycjaWlasnegoKoszyka Then
            cmbMagazynOdbiorWlasnyDPD.Enabled = True
            'uaktywniamy guzik "zapisz zmiany"
            odswiezUprawnienia()
            btnDaneDpd.Enabled = btnDaneDpd.Visible
            txtUwagi.Enabled = True
            txtOsobaKontaktowa.Enabled = True
            txtTelefonKontaktowy.Enabled = True
            btnZapiszZmiany.Enabled = True
            btnZapiszZmiany.BackColor = blue
        End If
        'wy³¹czamy resztê kontrolek
        cmbMagazynOdbiorWlasny.Enabled = False
        cmbDostawaNaZdefiniowanyAdres.Enabled = False
        sbSzukajAdresu.Enabled = False
        txtNazwa.Enabled = False
        txtAdres.Enabled = False
        txtKodPocztowy.Enabled = False
        txtMiasto.Enabled = False
        lblOdbiorcy.Enabled = False
        btnOdbiorcy.Enabled = False
    End Sub

    Private Sub dostawaNaAdresZdefiniowany()
        enumTypZamowienia = frmZamowienieTypZamowienia.dostawaNaAdresZdefiniowany
        'pod³¹czamy listê adresów do Combo
        If cmbDostawaNaZdefiniowanyAdres.DataSource Is Nothing Then
            If dtAdresy Is Nothing Then
                If Not doczytajAdresy() Then Exit Sub
            End If
            cmbDostawaNaZdefiniowanyAdres.ValueMember = "adres_id"
            cmbDostawaNaZdefiniowanyAdres.DisplayMember = "nazwa"
            cmbDostawaNaZdefiniowanyAdres.DataSource = dtAdresy
        End If

        If enumTrybPracy = frmZamowienieTrybPracy.edycjaWlasnegoKoszyka Then
            cmbDostawaNaZdefiniowanyAdres.Enabled = uzywajComboAdresy
            sbSzukajAdresu.Enabled = IIf(uzywajComboAdresy = True, False, True)
            sbSzukajAdresu.Visible = IIf(uzywajComboAdresy = True, False, True)
            'uaktywniamy guzik "zapisz zmiany"
            odswiezUprawnienia()
            btnDaneDpd.Enabled = btnDaneDpd.Visible
            btnZapiszZmiany.Enabled = True
            txtUwagi.Enabled = True
            txtOsobaKontaktowa.Enabled = True
            txtTelefonKontaktowy.Enabled = True
            btnZapiszZmiany.BackColor = blue
        End If

        'wy³¹czamy resztê kontrolek
        cmbMagazynOdbiorWlasny.Enabled = False
        cmbMagazynOdbiorWlasnyDPD.Enabled = False
        txtNazwa.Enabled = False
        txtAdres.Enabled = False
        txtKodPocztowy.Enabled = False
        txtMiasto.Enabled = False
        btnOdbiorcy.Enabled = False
        lblOdbiorcy.Enabled = False

    End Sub

    Private Sub dostawaNaAdresInny()
        enumTypZamowienia = frmZamowienieTypZamowienia.dostawaNaAdresInny

        If enumTrybPracy = frmZamowienieTrybPracy.edycjaWlasnegoKoszyka Then
            txtNazwa.Enabled = True
            txtAdres.Enabled = True
            txtKodPocztowy.Enabled = True
            txtMiasto.Enabled = True

            'uaktywniamy guzik "zapisz zmiany"
            odswiezUprawnienia()
            btnDaneDpd.Enabled = btnDaneDpd.Visible
            btnZapiszZmiany.Enabled = True
            txtUwagi.Enabled = True
            txtOsobaKontaktowa.Enabled = True
            txtTelefonKontaktowy.Enabled = True
            btnZapiszZmiany.BackColor = blue
        End If

        'wy³¹czamy resztê kontrolek
        cmbMagazynOdbiorWlasny.Enabled = False
        cmbMagazynOdbiorWlasnyDPD.Enabled = False
        cmbDostawaNaZdefiniowanyAdres.Enabled = False
        sbSzukajAdresu.Enabled = False
        lblOdbiorcy.Enabled = False
        btnOdbiorcy.Enabled = False


    End Sub


    Private Sub wylaczKontrolki()
        cmbDostawaNaZdefiniowanyAdres.DroppedDown = False

        'cmbZamowienieZMagazynu.Enabled = False
        btnDodajPozycje.Enabled = False
        btnUsunPozycje.Enabled = False
        rbOdbiorWlasny.Enabled = False
        rbOdbiorWlasnyDPD.Enabled = False
        cmbMagazynOdbiorWlasny.Enabled = False
        cmbMagazynOdbiorWlasnyDPD.Enabled = False
        rbDostawaNaZdefiniowanyAdres.Enabled = False
        cmbDostawaNaZdefiniowanyAdres.Enabled = False
        sbSzukajAdresu.Enabled = False
        rbDostawaNaAdres.Enabled = False
        rbZamowienieGrupowe.Enabled = False
        txtNazwa.Enabled = False
        txtAdres.Enabled = False
        txtKodPocztowy.Enabled = False
        txtMiasto.Enabled = False
        txtOsobaKontaktowa.Enabled = False
        txtTelefonKontaktowy.Enabled = False
        txtUwagi.Enabled = False
        btnZlozZamowienie.Enabled = False
        dgv.ReadOnly = True
        dtpDataRealizacji.Enabled = False
    End Sub

    Private Sub frmZamowienie_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If BlokujZapisz = False Then
            If bylyZmiany() Then
                Dim odp As MsgBoxResult = MsgBox("Od ostatniego odczytu z serwera wprowadzono zmiany w tym zamówieniu. Czy zapisaæ wprowadzone zmiany?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNoCancel + MsgBoxStyle.DefaultButton1, Me.Text)
                If odp = MsgBoxResult.Yes Then
                    If Not zapiszZmiany() Then
                        e.Cancel = True
                        Exit Sub
                    End If
                ElseIf odp = MsgBoxResult.Cancel Then
                    e.Cancel = True
                    Exit Sub
                End If
            End If

            If intIdBlokady >= 0 Then
                'zwalniamy blokadê rekordu
                System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
                System.Net.ServicePointManager.Expect100Continue = False
                ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
                ws.Proxy.Credentials = CredentialCache.DefaultCredentials
                'ws.Url = frmGlowna.strWebservice
                Try
                    ws.ZamowienieEdytujAnuluj(frmGlowna.sesja, intIdBlokady)
                Catch ex As Exception
                End Try
            End If
            If Me.Equals(frmGlowna.frmKoszyk) Then frmGlowna.frmKoszyk = Nothing
        End If
    End Sub

    Private Sub frmZamowienie_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If intIdUzytkownika < 0 Then intIdUzytkownika = frmGlowna.intIdUzytkownikZalogowany
        If Not wczytaj() Then
            BlokujZapisz = True
            Me.Close()
            Exit Sub
        End If
        If enumTrybPracy = frmZamowienieTrybPracy.edycjaWlasnegoKoszyka Then
            If sprawdz_czy_user_typ_oddzial() Then
                If CzyUserOddzial Then
                    rbDostawaNaAdres.Checked = False
                    rbDostawaNaAdres.Enabled = False
                    txtAdres.Enabled = False
                    txtKodPocztowy.Enabled = False
                    txtMiasto.Enabled = False
                    txtNazwa.Enabled = False
                Else
                    rbDostawaNaAdres.Enabled = True
                    txtAdres.Enabled = True
                    txtKodPocztowy.Enabled = True
                    txtMiasto.Enabled = True
                    txtNazwa.Enabled = True
                End If
            End If
        End If

        If enumTrybPracy = frmZamowienieTrybPracy.edycjaWlasnegoKoszyka Then
            If frmGlowna.frmKoszyk Is Nothing Then
                frmGlowna.frmKoszyk = Me
            Else
                'coœ nie tak - jest ju¿ otwarte inne okno koszyka, a my w³aœnie otworzyliœmy koszyk po raz kolejny - tak mia³o nie byæ!
                'poka¿my tamto okno i zamknijmy te
                If frmGlowna.frmKoszyk.WindowState = FormWindowState.Minimized Then frmGlowna.frmKoszyk.WindowState = FormWindowState.Normal
                frmGlowna.frmKoszyk.Activate()
                Me.Close()
                Exit Sub
            End If
        End If

        'czy mamy powiadomniæ ekran stan, ¿e nasz ekran gotowy?
        If Not strFunkcjaPowiadomieniaOGotowosci Is Nothing Then
            Dim m As MethodInfo() = frmRodzic.GetType.GetMethods()
            For licznik As Integer = 0 To m.GetUpperBound(0)
                If m(licznik).Name = strFunkcjaPowiadomieniaOGotowosci Then
                    m(licznik).Invoke(frmRodzic, Nothing)
                End If
            Next
        End If
        'WYLACZANIE KONTROLEK ZGODNIE Z BAZA DANYCH
        If enumTrybPracy = frmZamowienieTrybPracy.edycjaWlasnegoKoszyka Then
            MyBase.Wlacz(frmGlowna.sesja)
        End If
        If btnZapiszZmiany.Enabled Then
            btnZapiszZmiany.BackColor = blue
        Else
            btnZapiszZmiany.BackColor = Color.LightGray
        End If
    End Sub

    Private Function wczytaj() As Boolean
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.ZamowienieWczytajWynik

        intIdOstatnioWybranegoMagazynu = intIdMagazynu 'linia maj¹ca znaczenie przy otwarciu formy

        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.ZamowienieWczytaj(frmGlowna.sesja, intIdZamowienia, intIdMagazynu, intIdBlokady)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("B³¹d komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegó³y b³êdu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Me.Close()
            Return False
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
            Return False
        End If
        If wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
        End If

        intIdZamowienia = wsWynik.zamowienie_id
        intIdMagazynu = wsWynik.magazyn_id
        intIdOstatnioWybranegoMagazynu = wsWynik.magazyn_id
        intIdBlokady = wsWynik.blokada_id
        strUserNazwa = wsWynik.wlasciciel_nazwa
        lblStatusZamowienia.Text = wsWynik.zamowienie_status
        lblStatusZamowienia.ToolTipText = wsWynik.zamowienie_status_opis
        strOsobaKontaktowa = wsWynik.osoba_kontaktowa
        txtOsobaKontaktowa.Text = strOsobaKontaktowa
        strTelefonKontaktowy = wsWynik.telefon_kontaktowy
        txtTelefonKontaktowy.Text = strTelefonKontaktowy
        strusers_ids = wsWynik.users_ids
        users_ids = strusers_ids
        strgrupy = wsWynik.grupy
        grupy = strgrupy
        strtypy = wsWynik.typy
        typy = strtypy
        strwielkosc = wsWynik.wielkosci
        wielkosc = strwielkosc
        strUwagi = wsWynik.uwagi
        txtUwagi.Text = strUwagi
        dataRealizacji = wsWynik.data_realizacji
        kosztDostawy = wsWynik.koszt_dostawy
        If dataRealizacji > New DateTime(1, 1, 1) Then
            dtpDataRealizacji.Value = dataRealizacji
        Else
            dtpDataRealizacji.Value = minimalnaDataRealizacji()
            dataRealizacji = minimalnaDataRealizacji()
        End If
        If intIdMagazynu <> 1 Then
            dtpDataRealizacji.Enabled = False
        End If

        bCentrala = wsWynik.centrala
        intDokZw = wsWynik.DokZw
        intPrzZw = wsWynik.PrzZw
        intOsPryw = wsWynik.OsPryw
        dblCOD = wsWynik.DPDKwotaCOD
        dblWartosc = wsWynik.DPDWartosc
        intDokZwOrg = wsWynik.DokZw
        intPrzZwOrg = wsWynik.PrzZw
        intOsPrywOrg = wsWynik.OsPryw
        dblCODOrg = wsWynik.DPDKwotaCOD
        dblWartoscOrg = wsWynik.DPDWartosc
        bMamDaneDPD = wsWynik.maDaneDpd = 1
        strTypDPD = wsWynik.DPDTyp
        strTypDPDOrg = wsWynik.DPDTyp

        If wsWynik.dane.Tables.Count > 0 Then
            dgv.DataSource = Nothing
            dgv.DataSource = wsWynik.dane.Tables(0)
            dgv.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)
            If dgv.Columns.Contains("grupa_id") Then
                dgv.Columns("grupa_id").Visible = False
            Else
                MsgBox("B³¹d wewnêtrzny systemu. Przes³ana lista pozycji zamówienia nie zawiera kolumny grupa_id." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
                Return False
            End If
            If dgv.Columns.Contains("sku_id") Then
                dgv.Columns("sku_id").Visible = False
            Else
                MsgBox("B³¹d wewnêtrzny systemu. Przes³ana lista pozycji zamówienia nie zawiera kolumny sku_id." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
                Return False
            End If

            'ustawiamy w³aœciwoœæ "tylko do odczytu" na wszystkich kolumnach oprócz iloœci
            For Each dgvColumn As DataGridViewColumn In dgv.Columns
                If dgvColumn.HeaderText.ToLower <> "ilosc" Then dgvColumn.ReadOnly = True
            Next

            If dgv.Columns.Contains("sku") Then dgv.Columns("sku").HeaderText = "Sku"
            If dgv.Columns.Contains("sku_nazwa") Then dgv.Columns("sku_nazwa").HeaderText = "Nazwa"
            If dgv.Columns.Contains("J.M.") Then dgv.Columns("J.M.").HeaderText = "J.m."
            If dgv.Columns.Contains("ilosc") Then dgv.Columns("ilosc").HeaderText = "Iloœæ"
            If dgv.Columns.Contains("ilosc_dostepna") Then dgv.Columns("ilosc_dostepna").HeaderText = "Iloœæ dostêpna"
            If dgv.Columns.Contains("grupa") Then dgv.Columns("grupa").HeaderText = "Grupa"
            If dgv.Columns.Contains("limit") Then dgv.Columns("limit").HeaderText = "Limit"
            If dgv.Columns.Contains("koszt punktowy") Then dgv.Columns("koszt punktowy").HeaderText = "Koszt punktowy"
            If dgv.Columns.Contains("sztuk_w_opakowaniu") Then dgv.Columns("sztuk_w_opakowaniu").HeaderText = "Iloœæ sztuk w opakowaniu"

            'zachowujemy kopiê pozycji zamówieñ wczytanych przy otwieraniu okna
            dtPozycjeZamowienia = wsWynik.dane.Tables(0).Copy
        Else
            MsgBox("B³¹d wewnêtrzny systemu. Serwer nie przes³a³ pozycji zamówienia." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
            Return False
        End If

        If wsWynik.dane.Tables.Count > 1 Then
            bReagujNaZmianyComboMagazyn = False
            'cmbZamowienieZMagazynu.ComboBox.ValueMember = "magazyn_id"
            'cmbZamowienieZMagazynu.ComboBox.DisplayMember = "nazwa"
            'cmbZamowienieZMagazynu.ComboBox.DataSource = wsWynik.dane.Tables(1)
            'If intIdMagazynu > 0 Then
            '    cmbZamowienieZMagazynu.ComboBox.SelectedValue = intIdMagazynu
            'End If
            If intIdMagazynu = 1 Or intIdMagazynu = 16 Then
                dtpDataRealizacji.Enabled = True
            Else
                dtpDataRealizacji.Enabled = False
            End If
            dtMagazyny = wsWynik.dane.Tables(1).Copy
            bReagujNaZmianyComboMagazyn = True
        Else
            MsgBox("B³¹d wewnêtrzny systemu. Serwer nie przes³a³ listy dostêpnych magazynów." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
            Return False
        End If


        If wsWynik.dane.Tables.Count > 2 Then

            'dtkodyPocztowe = wsWynik.dane.Tables(2).Copy

        Else
            MsgBox("B³¹d wewnêtrzny systemu. Serwer nie przes³a³ listy dostêpnych kodów pocztowych." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
            Cursor = Cursors.Default
            Return False
        End If

        'inicjalne ustawienie typu zamówienia
        rbOdbiorWlasny.Checked = False
        rbDostawaNaZdefiniowanyAdres.Checked = False
        rbDostawaNaAdres.Checked = False
        Select Case wsWynik.tryb_pracy
            Case 1
                enumTrybPracy = frmZamowienieTrybPracy.edycjaWlasnegoKoszyka
                Me.Text = "Koszyk"
            Case 2
                enumTrybPracy = frmZamowienieTrybPracy.podgladObcegoKoszyka
                Me.Text = "Podgl¹d koszyka u¿ytkownika " & strUserNazwa
                wylaczKontrolki()
            Case 3
                enumTrybPracy = frmZamowienieTrybPracy.podgladZamowienia
                Me.Text = "Podgl¹d zamówienia numer " & intIdZamowienia & " u¿ytkownika " & strUserNazwa
                wylaczKontrolki()
            Case Else
                MsgBox("B³¹d wewnêtrzny systemu. Serwer przes³a³ nieznany tryb pracy (" & wsWynik.tryb_pracy.ToString & ")." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
                Return False
        End Select
        'ustawienie kontrolek na podstawie wczytanego typu zamówienia
        Select Case wsWynik.typ_zamowienia
            Case 1
                enumTypZamowieniaKopia = frmZamowienieTypZamowienia.transfer
                rbOdbiorWlasny.Checked = False

            Case 2
                enumTypZamowieniaKopia = frmZamowienieTypZamowienia.odbiorWlasny
                intIdMagazynuDocelowego = wsWynik.magazyn_docelowy_id
                rbOdbiorWlasny.Checked = True
                cmbMagazynOdbiorWlasny.SelectedValue = intIdMagazynuDocelowego
            Case 3
                enumTypZamowieniaKopia = frmZamowienieTypZamowienia.dostawaNaAdresZdefiniowany
                intIdAdresuZdefiniowanego = wsWynik.adres_id
                rbDostawaNaZdefiniowanyAdres.Checked = True
                cmbDostawaNaZdefiniowanyAdres.SelectedValue = intIdAdresuZdefiniowanego
            Case 4
                enumTypZamowieniaKopia = frmZamowienieTypZamowienia.dostawaNaAdresInny
                rbDostawaNaAdres.Checked = True
                strNazwa = wsWynik.nazwa
                strAdres = wsWynik.adres
                strKod = wsWynik.kod
                strMiasto = wsWynik.miasto
                txtNazwa.Text = strNazwa
                txtAdres.Text = strAdres
                txtKodPocztowy.Text = strKod
                txtMiasto.Text = strMiasto
            Case 6
                enumTypZamowieniaKopia = frmZamowienieTypZamowienia.odbiorWlasnyOddzial
                intIdOddzialuDocelowego = wsWynik.oddzial_docelowy_id
                rbOdbiorWlasnyDPD.Checked = True
                cmbMagazynOdbiorWlasnyDPD.SelectedValue = intIdOddzialuDocelowego
            Case 7
                enumTypZamowieniaKopia = frmZamowienieTypZamowienia.zamowienieGrupowe
                rbZamowienieGrupowe.Checked = True
                lblOdbiorcy.Text = "Wybranych odbiorców: " & users_ids.Split(">").Length - 1
                txtOsobaKontaktowa.Enabled = False
                txtTelefonKontaktowy.Enabled = False
                txtUwagi.Enabled = False


            Case Else
                MsgBox("B³¹d wewnêtrzny systemu. Serwer przes³a³ nieznany typ zamówienia (" & wsWynik.typ_zamowienia.ToString & ")." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
                Return False
        End Select


        SprawdzDlugoscUwag()
        btnZapiszZmiany.Enabled = False
        btnZapiszZmiany.BackColor = Color.LightGray
        lblLimit.Text = wsWynik.limit
        Return True
    End Function
    Private Sub SprawdzDlugoscUwag()
        'obs³uga d³ugosci uwag
        If enumTrybPracy = frmZamowienieTrybPracy.edycjaWlasnegoKoszyka And enumTypZamowienia <> frmZamowienieTypZamowienia.zamowienieGrupowe Then
            intUwagiPozostaloZnakow = 170 - txtUwagi.Text.Length - frmGlowna.strUzytkownikZalogowany.Length - txtOsobaKontaktowa.Text.Length - txtTelefonKontaktowy.Text.Length
            lblUwagiPozostaloZnakow.Text = "Pozosta³o " & CStr(intUwagiPozostaloZnakow) & " znaków."
            If intUwagiPozostaloZnakow <= 0 Then
                lblUwagiPozostaloZnakow.ForeColor = Color.Red
            Else
                lblUwagiPozostaloZnakow.ForeColor = Color.Black
            End If
            If 170 - frmGlowna.strUzytkownikZalogowany.Length - txtOsobaKontaktowa.Text.Length - txtTelefonKontaktowy.Text.Length > 0 Then
                txtUwagi.MaxLength = 170 - frmGlowna.strUzytkownikZalogowany.Length - txtOsobaKontaktowa.Text.Length - txtTelefonKontaktowy.Text.Length
            Else
                txtUwagi.MaxLength = 0
            End If
            If 170 - txtUwagi.Text.Length - frmGlowna.strUzytkownikZalogowany.Length - txtTelefonKontaktowy.Text.Length > 0 Then
                txtOsobaKontaktowa.MaxLength = 170 - txtUwagi.Text.Length - frmGlowna.strUzytkownikZalogowany.Length - txtTelefonKontaktowy.Text.Length
            Else
                txtOsobaKontaktowa.MaxLength = 0
            End If

            If 170 - txtUwagi.Text.Length - frmGlowna.strUzytkownikZalogowany.Length - txtOsobaKontaktowa.Text.Length > 0 Then
                txtTelefonKontaktowy.MaxLength = 170 - txtUwagi.Text.Length - frmGlowna.strUzytkownikZalogowany.Length - txtOsobaKontaktowa.Text.Length
            Else
                txtTelefonKontaktowy.MaxLength = 0
            End If

        End If
    End Sub

    Private Function zapiszZmiany() As Boolean

        If bylyZmiany() Then

            System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
            System.Net.ServicePointManager.Expect100Continue = False
            ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
            ws.Proxy.Credentials = CredentialCache.DefaultCredentials
            'ws.Url = frmGlowna.strWebservice
            Dim wsWynik As wsCursorProf.KoszykZapiszWynik
            Try
                Dim ds As New DataSet
                Dim dataTmp As New DateTime(1, 1, 1)
                'If rbOdbiorWlasnyDPD.Checked = True Then
                '    If czyOddzialAktywny() = False Then
                '        Return False
                '    End If
                'End If

                Dim dtn As DateTime
                dtn = DateTime.Now()
                'Dim dataJutro As DateTime = New DateTime(dtn.Year, dtn.Month, dtn.Day + 1)


                If dtpDataRealizacji.Value.Date < minimalnaDataRealizacji().Date Then
                    MsgBox(String.Format("Nie mo¿na wybraæ przewidywanej daty dostawy wczeœniejszej ni¿ {0}", minimalnaDataRealizacji().ToString("yyyy-MM-dd")), MsgBoxStyle.Exclamation, Me.Text)
                    Return False
                End If
                dataTmp = dtpDataRealizacji.Value
                ds.Tables.Add(dgv.DataSource.Copy)
                If ds.Tables(0).Columns.Contains("nazwa") Then ds.Tables(0).Columns.Remove(ds.Tables(0).Columns("nazwa"))
                If ds.Tables(0).Columns.Contains("ilosc_dostepna") Then ds.Tables(0).Columns.Remove(ds.Tables(0).Columns("ilosc_dostepna"))
                Cursor = Cursors.WaitCursor
                Application.DoEvents()
                'IIf(cmbZamowienieZMagazynu.ComboBox.SelectedValue Is Nothing, -1, cmbZamowienieZMagazynu.ComboBox.SelectedValue), _
                Select Case enumTypZamowienia
                    Case frmZamowienieTypZamowienia.odbiorWlasny
                        wsWynik = ws.KoszykZapisz(frmGlowna.sesja, intIdBlokady, _
                            idMagazyn, _
                            IIf(cmbMagazynOdbiorWlasny.SelectedValue Is Nothing, -1, cmbMagazynOdbiorWlasny.SelectedValue), _
                            -1, "", "", "", "", txtOsobaKontaktowa.Text, txtTelefonKontaktowy.Text, txtUwagi.Text, _
                            enumTypZamowienia, ds, dataTmp, -1, IIf(bMamDaneDPD, 1, 0), intDokZw, intOsPryw, intPrzZw, dblCOD, dblWartosc, strTypDPD, users_ids, grupy, typy, wielkosc, warunek_grupowy)
                    Case frmZamowienieTypZamowienia.odbiorWlasnyOddzial
                        wsWynik = ws.KoszykZapisz(frmGlowna.sesja, intIdBlokady, _
                            idMagazyn, _
                            IIf(cmbMagazynOdbiorWlasny.SelectedValue Is Nothing, -1, cmbMagazynOdbiorWlasny.SelectedValue), _
                            -1, "", "", "", "", txtOsobaKontaktowa.Text, txtTelefonKontaktowy.Text, txtUwagi.Text, _
                            enumTypZamowienia, ds, dataTmp, IIf(cmbMagazynOdbiorWlasnyDPD.SelectedValue Is Nothing, -1, cmbMagazynOdbiorWlasnyDPD.SelectedValue), IIf(bMamDaneDPD, 1, 0), intDokZw, intOsPryw, intPrzZw, dblCOD, dblWartosc, strTypDPD, users_ids, grupy, typy, wielkosc, warunek_grupowy)
                    Case frmZamowienieTypZamowienia.dostawaNaAdresZdefiniowany
                        wsWynik = ws.KoszykZapisz(frmGlowna.sesja, intIdBlokady, _
                            idMagazyn, _
                            -1, IIf(cmbDostawaNaZdefiniowanyAdres.SelectedValue Is Nothing, -1, cmbDostawaNaZdefiniowanyAdres.SelectedValue), _
                            "", "", "", "", txtOsobaKontaktowa.Text, txtTelefonKontaktowy.Text, txtUwagi.Text, _
                            enumTypZamowienia, ds, dataTmp, -1, IIf(bMamDaneDPD, 1, 0), intDokZw, intOsPryw, intPrzZw, dblCOD, dblWartosc, strTypDPD, users_ids, grupy, typy, wielkosc, warunek_grupowy)
                    Case frmZamowienieTypZamowienia.dostawaNaAdresInny
                        wsWynik = ws.KoszykZapisz(frmGlowna.sesja, intIdBlokady, _
                            idMagazyn, _
                            -1, -1, txtNazwa.Text, txtAdres.Text, txtKodPocztowy.Text, txtMiasto.Text, txtOsobaKontaktowa.Text, _
                            txtTelefonKontaktowy.Text, txtUwagi.Text, enumTypZamowienia, ds, dataTmp, -1, IIf(bMamDaneDPD, 1, 0), intDokZw, intOsPryw, intPrzZw, dblCOD, dblWartosc, strTypDPD, users_ids, grupy, typy, wielkosc, warunek_grupowy)
                    Case frmZamowienieTypZamowienia.zamowienieGrupowe

                        wsWynik = ws.KoszykZapisz(frmGlowna.sesja, intIdBlokady, _
                            idMagazyn, _
                            -1, -1, txtNazwa.Text, txtAdres.Text, txtKodPocztowy.Text, txtMiasto.Text, txtOsobaKontaktowa.Text, _
                            txtTelefonKontaktowy.Text, txtUwagi.Text, enumTypZamowienia, ds, dataTmp, -1, IIf(bMamDaneDPD, 1, 0), intDokZw, intOsPryw, intPrzZw, dblCOD, dblWartosc, strTypDPD, users_ids, grupy, typy, wielkosc, warunek_grupowy)
                    Case Else
                        MsgBox("B³¹d wewnêtrzny systemu. Zmienna enumTypZamowienia przyjê³a niespodziewan¹ wartoœæ: " & enumTypZamowienia.ToString & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
                        Return False
                End Select
                Cursor = Cursors.Default
            Catch ex As Exception
                Cursor = Cursors.Default
                MsgBox("B³¹d komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegó³y b³êdu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
                Return False
            End Try

            If wsWynik.status < 0 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Zapisywanie zamówienia")
                Return False
            ElseIf wsWynik.status > 0 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, "Zapisywanie zamówienia")
            Else
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Information, "Zapisywanie zamówienia")
            End If

            Select Case enumTrybPracy
                Case frmZamowienieTrybPracy.edycjaWlasnegoKoszyka
                    frmGlowna.lblStatus.Text = "Koszyk zapisany poprawnie."
                Case frmZamowienieTrybPracy.podgladObcegoKoszyka
                    frmGlowna.lblStatus.Text = "Koszyk u¿ytkownika " & strUserNazwa & " zapisany poprawnie."
                Case frmZamowienieTrybPracy.podgladZamowienia
                    frmGlowna.lblStatus.Text = "Zamówienia numer " & intIdZamowienia & " u¿ytkownika " & strUserNazwa & " zapisane poprawnie."
            End Select
            frmGlowna.timer.Interval = 3000 'komunikat zniknie po 3s
            frmGlowna.timer.Start()
            btnZapiszZmiany.Enabled = False

            'odœwie¿amy okno rodzica (je¿eli jego okno prezentuje tak¹ metodê)
            If Not frmRodzic Is Nothing Then
                Dim m As MethodInfo() = frmRodzic.GetType.GetMethods()
                For licznik As Integer = 0 To m.GetUpperBound(0)
                    If m(licznik).Name = "odswiezListy" Then
                        m(licznik).Invoke(frmRodzic, Nothing)
                    End If
                Next
            End If

            'odœwie¿amy zamówienie z bazy
            wczytaj()
        Else
            Select Case enumTrybPracy
                Case frmZamowienieTrybPracy.edycjaWlasnegoKoszyka
                    frmGlowna.lblStatus.Text = "Zawartoœæ koszyka nie uleg³a zmianie, zapis do bazy nie by³ konieczny."
                Case frmZamowienieTrybPracy.podgladObcegoKoszyka
                    frmGlowna.lblStatus.Text = frmGlowna.lblStatus.Text = "Zawartoœæ koszyka u¿ytkownika " & strUserNazwa & " nie uleg³a zmianie, zapis do bazy nie by³ konieczny."
                Case frmZamowienieTrybPracy.podgladZamowienia
                    frmGlowna.lblStatus.Text = "Zamówienie numer " & intIdZamowienia & " u¿ytkownika " & strUserNazwa & " nie uleg³o zmianie, zapis do bazy nie by³ konieczny."
            End Select
            frmGlowna.timer.Interval = 3000 'komunikat zniknie po 3s
            frmGlowna.timer.Start()
            btnZapiszZmiany.Enabled = False
        End If
        If btnZapiszZmiany.Enabled Then
            btnZapiszZmiany.BackColor = blue
        Else
            btnZapiszZmiany.BackColor = Color.LightGray
        End If

        'odœwie¿amy kontrolkê dgv z formy frmZamowienia
        If Not frmGlowna.frmZamowieniaLista Is Nothing Then
            Dim m As MethodInfo() = frmGlowna.frmZamowieniaLista.GetType.GetMethods()
            For licznik As Integer = 0 To m.GetUpperBound(0)
                If m(licznik).Name = "wczytaj" Then
                    m(licznik).Invoke(frmGlowna.frmZamowieniaLista, Nothing)
                End If
            Next
        End If

        Return True
    End Function

    Private Function bylyZmiany() As Boolean
        Dim bBylyZmiany As Boolean = False
        Dim dtSku As DataTable
        If enumTrybPracy <> frmZamowienieTrybPracy.edycjaWlasnegoKoszyka Then
            Return False
        End If
        'czy zmieni³ siê magazyn Ÿród³owy?
        ' IIf(cmbZamowienieZMagazynu.ComboBox.SelectedValue Is Nothing, -1, cmbZamowienieZMagazynu.ComboBox.SelectedValue)
        If intIdMagazynu <> idMagazyn Then
            Return True
        End If

        'zmieni³a siê iloœæ pozycji?
        If dgv.Rows.Count <> dtPozycjeZamowienia.Rows.Count Then
            Return True
        End If

        'a mo¿e zmieni³y siê iloœci w pozycjach?
        bBylyZmiany = False
        For Each dgvWiersz As DataGridViewRow In dgv.Rows
            dtPozycjeZamowienia.DefaultView.RowFilter = "sku_id=" & dgvWiersz.Cells("sku_id").Value & "AND grupa_id=" & dgvWiersz.Cells("grupa_id").Value
            dtSku = dtPozycjeZamowienia.DefaultView.ToTable
            If dtSku.Rows.Count > 1 Then
                MsgBox("B³¹d wewnêtrzny aplikacji. Dla SKU " & dgvWiersz.Cells("sku").Value & " i grupy " & dgvWiersz.Cells("grupa").Value & " istnieje " & dtSku.Rows.Count & " wierszy, a powinien istnieæ tylko jeden." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
                Return True 'lepiej zwróciæ, ¿e by³y zmiany ...
            ElseIf dtSku.Rows.Count < 1 Then
                bBylyZmiany = True
                Exit For
            End If
            If dtSku.Rows(0).Item("ilosc") <> dgvWiersz.Cells("ilosc").Value Then
                bBylyZmiany = True
                Exit For
            End If
        Next
        If bBylyZmiany Then Return bBylyZmiany

        'czy zmieni³ siê tryb pracy?
        If enumTypZamowienia <> enumTypZamowieniaKopia Then
            Return True
        End If

        'jeœli nie zmieni³ siê tryb pracy, to mo¿e zmieni³y siê dane adresowe (w ka¿dym trybie trochê inne)?
        Select Case enumTypZamowienia
            Case frmZamowienieTypZamowienia.odbiorWlasny
                If IIf(cmbMagazynOdbiorWlasny.SelectedValue Is Nothing, -1, _
                    cmbMagazynOdbiorWlasny.SelectedValue) <> intIdMagazynuDocelowego Then Return True
            Case frmZamowienieTypZamowienia.odbiorWlasnyOddzial
                If IIf(cmbMagazynOdbiorWlasnyDPD.SelectedValue Is Nothing, -1, _
                    cmbMagazynOdbiorWlasnyDPD.SelectedValue) <> intIdOddzialuDocelowego Then Return True
            Case frmZamowienieTypZamowienia.dostawaNaAdresZdefiniowany
                If IIf(cmbDostawaNaZdefiniowanyAdres.SelectedValue Is Nothing, -1, _
                    cmbDostawaNaZdefiniowanyAdres.SelectedValue) <> intIdAdresuZdefiniowanego Then Return True
            Case frmZamowienieTypZamowienia.dostawaNaAdresInny
                If txtNazwa.Text <> strNazwa OrElse _
                txtAdres.Text <> strAdres OrElse _
                (txtKodPocztowy.Text <> "  -" AndAlso txtKodPocztowy.Text <> strKod) OrElse _
                txtMiasto.Text <> strMiasto Then
                    Return True
                End If
            Case frmZamowienieTypZamowienia.zamowienieGrupowe
                If strusers_ids <> users_ids OrElse _
                    strgrupy <> grupy OrElse _
                    strtypy <> typy OrElse _
                    strwielkosc <> wielkosc Then
                    Return True
                End If
        End Select

        'mo¿e zmieni³y siê dane kontaktowe?
        If txtOsobaKontaktowa.Text <> strOsobaKontaktowa OrElse _
        txtTelefonKontaktowy.Text <> strTelefonKontaktowy OrElse _
        txtUwagi.Text <> strUwagi Then
            Return True
        End If
        'moze zmienila sie data realizacji
        Dim aktualnaData As DateTime = DateTime.Now()
        If dtpDataRealizacji.Value <> dataRealizacji And dtpDataRealizacji.Enabled = True And dtpDataRealizacji.Value.Date <> DateTime.Now.Date Then
            Return True
        End If
        If btnZapiszZmiany.Enabled Then
            btnZapiszZmiany.BackColor = blue
        Else
            btnZapiszZmiany.BackColor = Color.LightGray
        End If
        'nic siê nie zmieni³o
        Return False

    End Function

    Private Sub dgv_CellValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles dgv.CellValidating
        If dgv.Columns(e.ColumnIndex).HeaderText.ToLower = "ilosc" Then
            Dim intIlosc As Integer
            If Not Integer.TryParse(e.FormattedValue, intIlosc) Then
                MsgBox("Podaj liczbê ca³kowit¹ wiêksz¹ b¹dŸ równ¹ zero.", MsgBoxStyle.Exclamation, Me.Text)
                e.Cancel = True
                Exit Sub
            End If
            If intIlosc < 0 Then
                MsgBox("Podaj liczbê ca³kowit¹ wiêksz¹ b¹dŸ równ¹ zero.", MsgBoxStyle.Exclamation, Me.Text)
                e.Cancel = True
                Exit Sub
            End If
        End If
    End Sub

    Private Sub dgv_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgv.CellEndEdit
        'zatwierdzamy zmiany na poziomie komórki
        BindingContext(dgv.DataSource).EndCurrentEdit()
        'odblokowujemy kontrolki
        btnZapiszZmiany.Enabled = True
        If btnZapiszZmiany.Enabled Then
            btnZapiszZmiany.BackColor = blue
        Else
            btnZapiszZmiany.BackColor = Color.LightGray
        End If
        UstawWartosc()
    End Sub

    Private Sub btnDodajPozycje_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDodajPozycje.Click
        'szukamy okna o nazwie frmStan
        Dim frm As frmStan = Nothing
        For Each frmOkno As Form In Me.MdiParent.MdiChildren
            If frmOkno.Name = "frmStan" Then
                frm = frmOkno
                If frm.ctr.intIdMagazynu = idMagazyn And frm.ctr.bStanDlaKoszykINV = False Then
                    'przywo³ujemy to okno na pierwszy plan
                    frm.WindowState = FormWindowState.Normal
                    frm.Activate()
                    Exit Sub
                Else
                    frm = Nothing
                End If
            End If
        Next
        If frm Is Nothing Then
            'otwieramy nowe okno stany
            frm = New frmStan
            frm.MdiParent = Me.MdiParent
            frm.ctr.intIdMagazynu = idMagazyn
            frm.bStanDlaKoszykINV = False
            frm.Show()
        Else
        End If
        CType(frm.ctr.Controls("ts"), ToolStrip).Items("btnDoKoszyka").Enabled = True
        'CType(frm.ctr.Controls("ts"), ToolStrip).Items("btnDoKoszykaGrupy").Enabled = False
    End Sub

    Private Sub btnUsunPozycje_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUsunPozycje.Click
        If dgv.CurrentCell Is Nothing Then
            MsgBox("Zaznacz pozycjê do usuniêcia.", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        dgv.Rows.RemoveAt(dgv.CurrentCell.RowIndex)
        btnZapiszZmiany.Enabled = True
        dgv.DataSource.AcceptChanges()
        UstawWartosc()
    End Sub

    Private Sub rbOdbiorWlasnyDPD_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbOdbiorWlasnyDPD.CheckedChanged
        If rbOdbiorWlasnyDPD.Checked Then
            odbiorWlasnyOddzial()
        End If
        UstawWartosc()
    End Sub

    Private Sub rbDostawaNaZdefiniowanyAdres_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbDostawaNaZdefiniowanyAdres.CheckedChanged
        If rbDostawaNaZdefiniowanyAdres.Checked Then
            dostawaNaAdresZdefiniowany()
        End If
        UstawWartosc()
    End Sub

    Private Sub rbDostawaNaAdres_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbDostawaNaAdres.CheckedChanged
        If rbDostawaNaAdres.Checked Then
            dostawaNaAdresInny()
            txtNazwa.Focus()
            'txtNazwa.SelectAll()
        End If
        UstawWartosc()
    End Sub

    Private Sub txtNazwa_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNazwa.TextChanged
        btnZapiszZmiany.Enabled = True
        btnZapiszZmiany.BackColor = blue
    End Sub

    Private Sub txtMiasto_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMiasto.TextChanged
        btnZapiszZmiany.Enabled = True
        btnZapiszZmiany.BackColor = blue
    End Sub

    Private Sub txtAdres_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAdres.TextChanged
        btnZapiszZmiany.Enabled = True
        btnZapiszZmiany.BackColor = blue
    End Sub

    Private Sub txtOsobaKontaktowa_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtOsobaKontaktowa.TextChanged
        btnZapiszZmiany.Enabled = True
        btnZapiszZmiany.BackColor = blue
        SprawdzDlugoscUwag()
    End Sub

    Private Sub txtTelefonKontaktowy_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTelefonKontaktowy.TextChanged
        btnZapiszZmiany.Enabled = True
        btnZapiszZmiany.BackColor = blue
        SprawdzDlugoscUwag()
    End Sub

    Private Sub txtUwagi_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUwagi.TextChanged
        btnZapiszZmiany.Enabled = True
        btnZapiszZmiany.BackColor = blue
        SprawdzDlugoscUwag()
    End Sub

    Private Sub cmbZamowienieZMagazynu_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If bReagujNaZmianyComboMagazyn Then
            'zbieramy zawartoœæ koszyka
            Dim dtSku As New DataTable
            dtSku.Columns.Add("sku_id")
            dtSku.Columns.Add("sku")
            dtSku.Columns.Add("sku_nazwa")
            For Each dgvRow As DataGridViewRow In dgv.Rows
                dtSku.Rows.Add(dgvRow.Cells("sku_id").Value, dgvRow.Cells("sku").Value, dgvRow.Cells("sku_nazwa").Value)
            Next
            If dtSku.Rows.Count < 1 Then
                'mamy pusty koszyk, nic nie musimy doczytywaæ
                intIdOstatnioWybranegoMagazynu = idMagazyn 'cmbZamowienieZMagazynu.ComboBox.SelectedValue
                Exit Sub
            End If

            'doczytujemy iloœci dostêpne z bazy
            Dim ds As New DataSet
            ds.Tables.Add(dtSku)
            System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
            System.Net.ServicePointManager.Expect100Continue = False
            ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
            ws.Proxy.Credentials = CredentialCache.DefaultCredentials
            'ws.Url = frmGlowna.strWebservice
            Dim wsWynik As wsCursorProf.StanSkuGrupaWynik
            Try
                Cursor = Cursors.WaitCursor
                Application.DoEvents()
                'cmbZamowienieZMagazynu.ComboBox.SelectedValue
                wsWynik = ws.StanSkuGrupa(frmGlowna.sesja, idMagazyn, ds)
                Cursor = Cursors.Default
            Catch ex As Exception
                Cursor = Cursors.Default
                MsgBox("B³¹d komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegó³y b³êdu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
                'bReagujNaZmianyComboMagazyn = False
                'cmbZamowienieZMagazynu.ComboBox.SelectedValue = intIdOstatnioWybranegoMagazynu
                'bReagujNaZmianyComboMagazyn = True
                Exit Sub
            End Try
            Cursor = Cursors.Default
            If wsWynik.status < 0 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
                'bReagujNaZmianyComboMagazyn = False
                'cmbZamowienieZMagazynu.ComboBox.SelectedValue = intIdOstatnioWybranegoMagazynu
                'bReagujNaZmianyComboMagazyn = True
                Exit Sub
            ElseIf wsWynik.status > 0 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
            End If
            If wsWynik.dane.Tables.Count < 1 Then
                MsgBox("B³¹d wewnêtrzny systemu: serwer nie zwróci³ listy dostêpnych iloœci dla wybranych sku." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, Me.Text)
                'bReagujNaZmianyComboMagazyn = False
                'cmbZamowienieZMagazynu.ComboBox.SelectedValue = intIdOstatnioWybranegoMagazynu
                'bReagujNaZmianyComboMagazyn = True
                Exit Sub
            End If

            'zmiana magazynu powiod³a siê
            intIdOstatnioWybranegoMagazynu = idMagazyn 'cmbZamowienieZMagazynu.ComboBox.SelectedValue


            'aktualizujemy grid
            Dim dtDostepne As DataTable = wsWynik.dane.Tables(0)
            For Each dgvRow As DataGridViewRow In dgv.Rows
                dgvRow.Cells("ilosc_dostepna").Value = 0
                For Each dtRow As DataRow In dtDostepne.Rows
                    If dtRow("sku_id") = dgvRow.Cells("sku_id").Value Then
                        dgvRow.Cells("ilosc_dostepna").Value = dtRow("ilosc_dostepna")
                        Exit For
                    End If
                Next
            Next

            btnZapiszZmiany.Enabled = True
            If intIdOstatnioWybranegoMagazynu = 1 Or intIdOstatnioWybranegoMagazynu = 16 Then
                dtpDataRealizacji.Enabled = True
            Else
                dtpDataRealizacji.Enabled = False
            End If

        End If

        If btnZapiszZmiany.Enabled Then
            btnZapiszZmiany.BackColor = blue
        Else
            btnZapiszZmiany.BackColor = Color.LightGray
        End If
    End Sub

    Private Sub cmbMagazynOdbiorWlasny_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbMagazynOdbiorWlasny.SelectedValueChanged
        btnZapiszZmiany.Enabled = True
    End Sub

    Private Sub cmbDostawaNaZdefiniowanyAdres_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbDostawaNaZdefiniowanyAdres.SelectedValueChanged
        btnZapiszZmiany.Enabled = True
    End Sub

    Private Sub btnZapiszZmiany_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZapiszZmiany.Click
        zapiszZmiany()
    End Sub

    Private Sub btnZatwierdzZamowienie_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZlozZamowienie.Click

        'If cmbZamowienieZMagazynu.ComboBox.SelectedValue < 0 Then
        '    MsgBox("Wybierz magazyn z którego ma zostaæ pobrany towar.", MsgBoxStyle.Exclamation)
        '    Exit Sub
        'End If
        SprawdzDlugoscUwag()
        If intUwagiPozostaloZnakow < 0 Then
            MsgBox("Pole uwagi zawiera za duzo znaków", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        'kontrola pól zale¿nych od typu zamówienia
        Dim intTest As Integer
        Select Case enumTypZamowienia
            Case frmZamowienieTypZamowienia.odbiorWlasny
                If IIf(cmbMagazynOdbiorWlasny.SelectedValue Is Nothing, -1, cmbMagazynOdbiorWlasny.SelectedValue) < 1 Then
                    MsgBox("Wybierz magazyn w którym chcesz odebraæ towar.", MsgBoxStyle.Exclamation, Me.Text)
                    Exit Sub
                End If
            Case frmZamowienieTypZamowienia.dostawaNaAdresZdefiniowany
                If IIf(cmbDostawaNaZdefiniowanyAdres.SelectedValue Is Nothing, -1, _
                    cmbDostawaNaZdefiniowanyAdres.SelectedValue) < 1 Then
                    MsgBox("Wybierz adres, na który nale¿y dostarczyæ towar.", MsgBoxStyle.Exclamation, Me.Text)
                    Exit Sub
                End If
            Case frmZamowienieTypZamowienia.dostawaNaAdresInny
                If txtNazwa.Text.Length < 1 Then
                    MsgBox("Wype³nij pole Nazwa Odbiorcy.", MsgBoxStyle.Exclamation, Me.Text)
                    txtNazwa.Focus()
                    Exit Sub
                End If
                If txtNazwa.Text.Length > 25 Then
                    MsgBox("Zgodnie z wymogiem systemu Qguara pole Nazwa odbiorcy nie mo¿e zawieraæ wiêcej ni¿ 25 znaków. Proszê skróciæ nazwê.", MsgBoxStyle.Exclamation, Me.Text)
                    txtNazwa.Focus()
                    Exit Sub
                End If
                If txtAdres.Text.Length < 1 Then
                    MsgBox("Wype³nij pole Adres.", MsgBoxStyle.Exclamation, Me.Text)
                    txtAdres.Focus()
                    Exit Sub
                End If
                If txtKodPocztowy.Text.Length <> 6 Then
                    MsgBox("Wype³nij poprawnie pole ""Kod"". Podaj kod pocztowy w postaci XX-XXX.", MsgBoxStyle.Exclamation, Me.Text)
                    txtKodPocztowy.Focus()
                    Exit Sub
                End If
                If Not Integer.TryParse(txtKodPocztowy.Text.Substring(0, 2) & txtKodPocztowy.Text.Substring(3, 3), intTest) Then
                    MsgBox("Wype³nij poprawnie pole ""Kod"". Podaj kod pocztowy w postaci XX-XXX.", MsgBoxStyle.Exclamation, Me.Text)
                    txtKodPocztowy.Focus()
                    Exit Sub
                End If
                If txtMiasto.Text.Length < 1 Then
                    MsgBox("Wype³nij pole Miasto.", MsgBoxStyle.Exclamation, Me.Text)
                    txtMiasto.Focus()
                    Exit Sub
                End If
               
                'pozosta³e tryby nie wymagaj¹ dodatkowych parametrów
        End Select

        If txtOsobaKontaktowa.Text.Length < 1 Then
            MsgBox("Wype³nij pole ""Osoba kontaktowa"".", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If txtTelefonKontaktowy.Text.Length < 7 OrElse txtTelefonKontaktowy.Text.Length > 9 Then
            MsgBox("Wype³nij poprawnie pole ""Telefon kontaktowy""." & vbNewLine & vbNewLine & "Telefon stacjonarny powinien byæ podany wraz z numerem kierunkowym bez spacji, nawiasów i kresek, np.: 223352275" & vbNewLine & vbNewLine & "Telefon komórkowy powinien byæ podany w postaci 9-cyfrowego numeru bez zera, spacji i kresek, np.: 601601601", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        'czy s¹ jakieœ niezapisane zmiany? Jeœli tak, zapiszmy je
        If bylyZmiany() Then
            If Not zapiszZmiany() Then Exit Sub
        End If

        'zatwierdzamty zamówienie
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As wsCursorProf.KoszykZatwierdzWynik

        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.KoszykZatwierdz(frmGlowna.sesja, intIdBlokady)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("B³¹d komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegó³y b³êdu:" & ex.Message, MsgBoxStyle.Critical)
            Me.Close()
            Exit Sub
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Zatwierdzenie zamówienia")
            Exit Sub
        End If
        If wsWynik.status = 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Information, "Zatwierdzenie zamówienia")
        End If

        If wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Information, "Zatwierdzenie zamówienia")
        End If

        intIdBlokady = -1

        Select Case enumTrybPracy
            Case frmZamowienieTrybPracy.edycjaWlasnegoKoszyka
                frmGlowna.lblStatus.Text = "Pomyœlnie zatwierdzono koszyk."
            Case frmZamowienieTrybPracy.podgladObcegoKoszyka
                frmGlowna.lblStatus.Text = "Pomyœlnie zatwierdzono koszyk u¿ytkownika " & strUserNazwa & "."
            Case frmZamowienieTrybPracy.podgladZamowienia
                frmGlowna.lblStatus.Text = "Pomyœlnie zapisano zmiany w zamówieniu " & intIdZamowienia & " u¿ytkownika " & strUserNazwa & "."
        End Select
        frmGlowna.timer.Interval = 3000 'komunikat zniknie po 3s
        frmGlowna.timer.Start()

        If Not frmGlowna.frmKoszyk Is Nothing Then frmGlowna.frmKoszyk = Nothing
        wczytaj()

        'odœwie¿amy kontrolkê dgv z formy frmZamowienia
        If Not frmGlowna.frmZamowieniaLista Is Nothing Then
            Dim m As MethodInfo() = frmGlowna.frmZamowieniaLista.GetType.GetMethods()
            For licznik As Integer = 0 To m.GetUpperBound(0)
                If m(licznik).Name = "wczytaj" Then
                    m(licznik).Invoke(frmGlowna.frmZamowieniaLista, Nothing)
                End If
            Next
        End If

    End Sub

    Private Sub dtpDataRealizacji_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpDataRealizacji.ValueChanged
        If dtpDataRealizacji.Value.DayOfWeek = DayOfWeek.Saturday Or dtpDataRealizacji.Value.DayOfWeek = DayOfWeek.Sunday Then
            MsgBox("Nie mo¿na wybraæ jako dnia wysy³ki soboty lub niedzieli. Data zostanie zmieniona", MsgBoxStyle.Critical)
            dtpDataRealizacji.Value = minimalnaDataRealizacji()
        Else
            btnZapiszZmiany.Enabled = True
        End If
        If btnZapiszZmiany.Enabled Then
            btnZapiszZmiany.BackColor = blue
        Else
            btnZapiszZmiany.BackColor = Color.LightGray
        End If
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub odswiezUprawnienia()
        MyBase.Wlacz(frmGlowna.sesja)
    End Sub

    Private Sub cmbMagazynOdbiorWlasnyDPD_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbMagazynOdbiorWlasnyDPD.SelectedValueChanged
        'czyOddzialAktywny()
        UstawWartosc()
    End Sub

    'Private Function czyOddzialAktywny() As Boolean
    '    If cmbMagazynOdbiorWlasnyDPD.Items.Count > 0 Then
    '        If CType(dtOddzialy.Select(String.Format("oddzial_id = {0}", cmbMagazynOdbiorWlasnyDPD.SelectedValue.ToString))(0).Item("Aktywny"), Boolean) = False Then
    '            MsgBox(String.Format("Oddzia³ DPD '{0}' jest nieaktywny", cmbMagazynOdbiorWlasnyDPD.SelectedText), MsgBoxStyle.Critical)
    '            Return False
    '        End If
    '    End If
    '    Return True
    'End Function

    'Private Function minimalnaDataRealizacji() As Date
    '    Dim dataAktualna As DateTime = DateTime.Now
    '    Dim dodawaneDni As Integer = 1
    '    If dataAktualna.Hour > 15 Then
    '        dodawaneDni = 2
    '    End If
    '    If dataAktualna.AddDays(dodawaneDni).DayOfWeek = DayOfWeek.Saturday Then
    '        dodawaneDni = dodawaneDni + 2
    '    End If
    '    If dataAktualna.AddDays(dodawaneDni).DayOfWeek = DayOfWeek.Sunday Then
    '        dodawaneDni = dodawaneDni + 1
    '    End If
    '    Return dataAktualna.AddDays(dodawaneDni).Date
    'End Function

    Private Function minimalnaDataRealizacji() As Date
        Dim wynikData As Date = Date.Now

        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As wsCursorProf.MinimalnaDataRealizacjiWynik

        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.MinimalnaDataRealizacji(frmGlowna.sesja)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("B³¹d komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegó³y b³êdu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Me.Close()
            wynikData = DateTime.Now
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
            wynikData = DateTime.Now
        End If
        If wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
            wynikData = DateTime.Now
        End If
        wynikData = wsWynik.data


        Return wynikData
    End Function

    Private Sub UstawWartosc()
        Dim wartosc As Decimal
        wartosc = 0
        If enumTypZamowienia <> frmZamowienieTypZamowienia.odbiorWlasny Then
            wartosc = kosztDostawy
        End If
        For Each row As DataGridViewRow In dgv.Rows
            wartosc = wartosc + (CType(row.Cells("koszt punktowy").Value, Decimal) * CType(row.Cells("ilosc").Value, Decimal))
        Next
        lblWartosc.Text = Format(wartosc, "0.00")
    End Sub

    Private Sub rbOdbiorWlasny_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbOdbiorWlasny.CheckedChanged
        If rbOdbiorWlasny.Checked Then
            odbiorWlasny()
        End If
        UstawWartosc()
    End Sub

    Private Sub btnDaneDpd_Click(sender As System.Object, e As System.EventArgs) Handles btnDaneDpd.Click
        Dim frm As New frmDaneDpd
        frm.bMamDane = bMamDaneDPD
        frm.intZamowienieId = intIdZamowienia
        frm.bInEdit = enumTrybPracy = frmZamowienieTrybPracy.edycjaWlasnegoKoszyka
        frm.intDokZw = intDokZw
        frm.intOsPryw = intOsPryw
        frm.intPrzZw = intPrzZw
        frm.dblCOD = dblCOD
        frm.dblWartosc = dblWartosc
        frm.strTyp = strTypDPD
        frm.kwota_zam = CDec(lblWartosc.Text)


        If enumTypZamowienia = frmZamowienieTypZamowienia.odbiorWlasnyOddzial Then
            frm.odb_oddzial = 1
        Else
            frm.odb_oddzial = 0
        End If
        If enumTypZamowienia = frmZamowienieTypZamowienia.dostawaNaAdresZdefiniowany And (Not cmbDostawaNaZdefiniowanyAdres.SelectedValue Is Nothing) Then
            If dtAdresy.Select(String.Format("adres_id = {0}", cmbDostawaNaZdefiniowanyAdres.SelectedValue)).Length = 1 Then
                frm.sKod = dtAdresy.Select(String.Format("adres_id = {0}", cmbDostawaNaZdefiniowanyAdres.SelectedValue))(0).Item("Kod").ToString
                odswierzKodPocztowy(frm.sKod)
                validKodPocztowy(frm.sKod)
            End If
        End If
        If enumTypZamowienia = frmZamowienieTypZamowienia.dostawaNaAdresInny Then
            frm.sKod = txtKodPocztowy.Text
            odswierzKodPocztowy(frm.sKod)
            validKodPocztowy(frm.sKod)
        End If
        frm.bkodvalid = bKodvalid
        If (enumTypZamowienia = frmZamowienieTypZamowienia.dostawaNaAdresZdefiniowany Or enumTypZamowienia = frmZamowienieTypZamowienia.dostawaNaAdresInny) And bKodvalid = True Then
            frm.dtKody = dtkodyPocztowe

        End If
        If frm.ShowDialog = Windows.Forms.DialogResult.OK Then
            If intDokZw <> frm.intDokZw Or intOsPryw <> frm.intOsPryw Or intPrzZw <> frm.intPrzZw Or dblCOD <> frm.dblCOD Or dblWartosc <> frm.dblWartosc Or strTypDPD <> frm.strTyp Then

                intDokZw = frm.intDokZw
                intOsPryw = frm.intOsPryw
                intPrzZw = frm.intPrzZw
                dblCOD = frm.dblCOD
                dblWartosc = frm.dblWartosc
                strTypDPD = frm.strTyp
                bMamDaneDPD = True
                btnZapiszZmiany.Enabled = True
                btnZapiszZmiany.BackColor = blue
            End If
        End If
    End Sub

    Private Sub validKodPocztowy(ByVal kod As String)
        bKodvalid = False

        If kod.Length = 6 Then
            If dtkodyPocztowe.Select(String.Format("KOD_POCZTOWY = '{0}'", kod)).Length = 1 Then
                If enumTypZamowienia = frmZamowienieTypZamowienia.dostawaNaAdresInny Then
                    txtMiasto.Text = dtkodyPocztowe.Select(String.Format("KOD_POCZTOWY = '{0}'", kod))(0).Item("MIASTO").ToString
                End If
                bKodvalid = True
            Else
                MsgBox("Nieprawid³owy kod pocztowy.", MsgBoxStyle.Critical, "Nieprawid³owe dane")
                txtKodPocztowy.Text = ""
                txtKodPocztowy.Focus()
            End If

        End If

    End Sub

    Private Function odswierzKodPocztowy(ByVal kod As String) As Boolean
        odKodPocz = True
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.KodyPocztoweOdczytajWynik

        Try
            'Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.KodyPocztoweOdczytaj(frmGlowna.sesja, kod)
            'Cursor = Cursors.Default
        Catch ex As Exception
            'Cursor = Cursors.Default
            MsgBox("B³¹d komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegó³y b³êdu:" & ex.Message, MsgBoxStyle.Critical, Me.Text)
            Me.Close()
            odKodPocz = False
            Return False
            Exit Function
        End Try

        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, Me.Text)
            Return False
            odKodPocz = False
        End If
        If wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, Me.Text)
        End If
        dtkodyPocztowe = wsWynik.dane.Tables(0)
        If enumTypZamowienia = frmZamowienieTypZamowienia.dostawaNaAdresInny Then

            txtKodPocztowy.Text = kod
            txtKodPocztowy.SelectionStart = kod.Length
            txtKodPocztowy.SelectionLength = 0
        End If
        'cmbKodPocztowy.SelectionStart = kod.Length
        'cmbKodPocztowy.SelectionLength = 0
        'odswierzKodPocztowy(kod)
        'cmbKodPocztowy.DataSource = Nothing
        'cmbKodPocztowy.DataSource = dtkodyPocztowe
        'cmbKodPocztowy.DisplayMember = "KOD_POCZTOWY"
        'cmbKodPocztowy.ValueMember = "KOD_POCZTOWY"
        'cmbKodPocztowy.SelectedValue = kod
        odKodPocz = False
        Return True
    End Function

    Private Sub btnOdbiorcy_Click(sender As System.Object, e As System.EventArgs) Handles btnOdbiorcy.Click
        Dim frm As New frmZamowienieOdbiorcy
        frm.frmRodzic = Me
        frm.intIdZamowienie = intIdZamowienia
        frm.uzytkownicy_ids = users_ids
        frm.grupy = grupy
        frm.typy = typy
        frm.warunek = warunek_grupowy
        frm.wielkosc = wielkosc
        If enumTrybPracy = frmZamowienieTrybPracy.edycjaWlasnegoKoszyka Then
            frm.InEdit = True
        End If
        If frm.ShowDialog() = Windows.Forms.DialogResult.OK Then
            If users_ids <> frm.uzytkownicy_ids Then
                btnZapiszZmiany.Enabled = True
                btnZapiszZmiany.BackColor = blue
                rbZamowienieGrupowe.Checked = True
            End If
            users_ids = frm.uzytkownicy_ids
            warunek_grupowy = frm.warunek
            grupy = frm.grupy
            typy = frm.typy
            wielkosc = frm.wielkosc
            If frm.uzytkownicy_cnt > 0 Then
                lblOdbiorcy.Text = "Wybranych odbiorców: " & frm.uzytkownicy_cnt
            Else
                lblOdbiorcy.Text = "Nie wybrano odbiorców"
            End If
        End If
    End Sub

    Private Sub rbZamowienieGrupowe_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbZamowienieGrupowe.CheckedChanged
        If rbZamowienieGrupowe.Checked Then
            zamowienieGrupowe()
        End If
        btnOdbiorcy.Enabled = rbZamowienieGrupowe.Checked
        If bFormShown Then
            txtOsobaKontaktowa.Text = ""
            txtTelefonKontaktowy.Text = ""
            cmbMagazynOdbiorWlasny.SelectedValue = -1
            cmbDostawaNaZdefiniowanyAdres.SelectedValue = -1
            cmbMagazynOdbiorWlasnyDPD.SelectedValue = -1
            txtAdres.Text = ""
            txtMiasto.Text = ""
            txtNazwa.Text = ""
            txtKodPocztowy.Text = ""
        End If
    End Sub

    Private Sub zamowienieGrupowe()
        enumTypZamowienia = frmZamowienieTypZamowienia.zamowienieGrupowe

        cmbDostawaNaZdefiniowanyAdres.Enabled = False
        sbSzukajAdresu.Enabled = False
        'wy³¹czamy resztê kontrolek
        cmbMagazynOdbiorWlasny.Enabled = False
        cmbMagazynOdbiorWlasnyDPD.Enabled = False
        txtNazwa.Enabled = False
        txtAdres.Enabled = False
        txtKodPocztowy.Enabled = False
        If enumTrybPracy = frmZamowienieTrybPracy.edycjaWlasnegoKoszyka Then
            lblOdbiorcy.Enabled = True

            btnZapiszZmiany.Enabled = True
            btnZapiszZmiany.BackColor = blue
        End If

        txtMiasto.Enabled = False
        'uaktywniamy guzik "zapisz zmiany"
        btnDaneDpd.Enabled = False

        txtOsobaKontaktowa.Enabled = False
        txtTelefonKontaktowy.Enabled = False
        txtUwagi.Enabled = False
    End Sub

    Private Sub rbZamowienieGrupowe_EnabledChanged(sender As Object, e As System.EventArgs) Handles rbZamowienieGrupowe.EnabledChanged
        If enumTrybPracy = frmZamowienieTrybPracy.edycjaWlasnegoKoszyka Then
            lblOdbiorcy.Enabled = rbZamowienieGrupowe.Enabled
        Else
            lblOdbiorcy.Enabled = False
            rbZamowienieGrupowe.Enabled = False
        End If

    End Sub

    Private Sub rbZamowienieGrupowe_VisibleChanged(sender As Object, e As System.EventArgs) Handles rbZamowienieGrupowe.VisibleChanged
        If enumTrybPracy = frmZamowienieTrybPracy.edycjaWlasnegoKoszyka Then
            lblOdbiorcy.Visible = rbZamowienieGrupowe.Visible
            btnOdbiorcy.Visible = rbZamowienieGrupowe.Visible
        End If
    End Sub

    Private Sub PrzyciskEnabledChanged(sender As Object, e As System.EventArgs) Handles btnZapiszZmiany.EnabledChanged, btnZlozZamowienie.EnabledChanged, btnOdbiorcy.EnabledChanged
        If sender.Enabled Then
            sender.BackColor = blue
            sender.ForeColor = Color.White
        Else
            sender.BackColor = Color.LightGray
            sender.ForeColor = Color.Gray
        End If
    End Sub

    Private Sub btnZamknij_Click(sender As System.Object, e As System.EventArgs) Handles btnZamknij.Click
        Me.Close()
    End Sub

    Private Sub sbSzukajAdresu_Click(sender As System.Object, e As System.EventArgs) Handles sbSzukajAdresu.Click
        If intIdUzytkownika < 0 Then
            MsgBox("B³¹d wewnêtrzny systemu. Zmienna intIdUzytkownika jest nie ustawiona.", MsgBoxStyle.Critical, Me.Text)
            Exit Sub
        End If
        Dim frm As New frmWyszukiwanieAdresu
        frm.intIdUzytkownika = intIdUzytkownika

        frm.ShowDialog()
        If frm.DialogResult = Windows.Forms.DialogResult.OK Then
            If frm.dtWybranaPozycja.Rows.Count = 1 Then
                intIdAdresuZdefiniowanego = frm.dtWybranaPozycja.Rows(0)("adres_id")
                doczytajAdresy()
                cmbDostawaNaZdefiniowanyAdres.DataSource = dtAdresy
                cmbDostawaNaZdefiniowanyAdres.SelectedValue = intIdAdresuZdefiniowanego
                intIdAdresuZdefiniowanego = -1 'To po to zeby dalo sie zapisac zmiany (inaczej bylyZmiany zwroci False)
            End If
        End If

    End Sub


    Private Sub cmbKodPocztowy_TextChanged1(sender As Object, e As System.EventArgs) Handles txtKodPocztowy.TextChanged
        If odKodPocz = False Then
            odswierzKodPocztowy(txtKodPocztowy.Text)
            txtMiasto.Text = String.Empty
            validKodPocztowy(txtKodPocztowy.Text)
            btnZapiszZmiany.Enabled = True
            btnZapiszZmiany.BackColor = blue
        End If
    End Sub

End Class