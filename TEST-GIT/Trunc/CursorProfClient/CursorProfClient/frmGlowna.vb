Imports System.Threading
Imports System.Reflection
Imports System.IO
'Po dodaniu certyfikatu 20151228
Public Class frmGlowna
    Inherits frmBase
    Public Shared sesja As Byte()
    Public Shared bBiuro As Boolean = False
    Public Shared intIdUzytkownikZalogowany As Integer = -1
    Public Shared strUzytkownikZalogowany As String = ""
    Public Shared strTelefon As String = ""
    Public Shared strWebservice As String = ""
    Public Shared bProdukcja As Boolean = True
    Public Shared frmAwizaZarzadzanie As frmZarzadzanieAwizami
    Public Shared frmZamowieniaLista As frmZamowienia
    Public Shared frmAwizaNew As frmAwizacje
    Public Shared frmKoszyk As frmZamowienie 'jeœli jest otwarty koszyk u¿ytkownika, to tutaj znajduje siê odwo³anie do niego (¿eby nie otwieraæ koszyka po kilka razy)
    Dim Parametry As New ZmienneGlobalne
    Public kontaktIt As String = Parametry._kontaktIT
	Public Shared frmKoszykINV As frmZamowienieINV
    Public czyPierwszy As Boolean
    Public strKomunikatDlaUzytkownika As String
    'Dim sprawdz As New c_sprawdz_funkcje
    Public watek As Thread
    Private WybranyItem As String
    Private Zaznaczony As String
    Private _color As System.Drawing.Color
    '    Private blue As System.Drawing.Color = Color.DodgerBlue
    Private blue As System.Drawing.Color = System.Drawing.Color.Black
    Private MojeItemyWMenu As DataTable
    Private dlugoscItemowMenu As Integer = 0

    Private Sub OProgramieToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim frm As New frmOProgramie
        'frm.MdiParent = Me
        'frm.Show()
    End Sub

    Private Sub timer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer.Tick
        lblStatus.Text = ""
        timer.Stop()
    End Sub

    Private Sub TestyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TestyToolStripMenuItem.Click
        InputBox("", "Identyfikator bie¿¹cej sesji", "0x" & BitConverter.ToString(sesja).Replace("-", ""))
    End Sub

    Private Sub UzytkownicyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UzytkownicyToolStripMenuItem.Click
        Dim frm As frmUzytkownicy = New frmUzytkownicy
        frm.MdiParent = Me
        frm.Show()
    End Sub



    Private Sub ZakonczToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ZakonczToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub ZalogujPonownieToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ZalogujPonownieToolStripMenuItem.Click

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '' zamykamy wszystkie otwarte formy 
        '' robimy to na pocz¹tku procedury, aby nie by³y zamykane w kontekœcie innego u¿ytkownika po ponownym zalogowaniu, 
        '' bo powodowa³oby b³êdy przy zapisie np. koszyka
        For i = System.Windows.Forms.Application.OpenForms.Count - 1 To 1 Step -1
            Dim f As Form = System.Windows.Forms.Application.OpenForms(i)
            f.Close()
        Next i

        If System.Windows.Forms.Application.OpenForms.Count - 1 > 0 Then
            MsgBox("Proszê zamkn¹æ wszystkie otwarte okienka przed ponownym zalogowaniem!", MsgBoxStyle.Exclamation, "Zamkniêcie otwartych okienek")
            Exit Sub
        End If
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


        Dim frm As New frmLogowanie
        frm.ShowDialog()

        'PW: wywo³ujê metodê klasy c_sprawdz_funkcje aby sprawdziæ uprawnienia u¿ytkownika
        'sprawdz.sprawdz_uprawnienia(sesja)
        Dim alWidoczne As New ArrayList
        alWidoczne.Add("SystemToolStripMenuItem")
        alWidoczne.Add("KontaktToolStripMenuItem")
        alWidoczne.Add("PomocToolStripMenuItem")
        For Each Item As ToolStripMenuItem In ms.Items
            If Not alWidoczne.Contains(Item.Name) Then
                Item.Visible = False
            End If
        Next

        'UzytkownicyToolStripMenuItem.Visible = False
        'GrupyToolStripMenuItem.Visible = False
        'StanyToolStripMenuItem.Visible = False
        'KoszykToolStripMenuItem.Visible = False
        'ZamowieniaToolStripMenuItem.Visible = False
        'RaportyToolStripMenuItem.Visible = False
        'PlikiDoPobraniaToolStripMenuItem.Visible = False
        'ZwrotyToolStripMenuItem.Visible = False
        'NewsletterToolStripMenuItem.Visible = False
        'WiadomosciToolStripMenuItem.Visible = False
        'LimityToolStripMenuItem.Visible = False
        'AwizacjeToolStripMenuItem.Visible = False
        'ZamowieniaZPlikuExcelaToolStripMenuItem.Visible = False
        'LimityLogistyczneToolStripMenuItem.Visible = False
        'SlownikiToolStripMenuItem.Visible = False
        'KomunikatyStripMenuItem.Visible = False
        MojeItemyWMenu = Nothing
        HScrollBar1.Visible = False



        For Each frmChild As Form In Me.MdiChildren
            Dim m As MethodInfo() = frmChild.GetType.GetMethods()
            For licznik As Integer = 0 To m.GetUpperBound(0)
                If m(licznik).Name = "odswiezUprawnienia" Then
                    m(licznik).Invoke(frmChild, Nothing)
                End If
            Next
        Next
        MyBase.Wlacz(sesja)
        CzyPotrzebnyScrollWMenu()
    End Sub
    Public Sub showOrRefresh()
        If Me.Visible = False Then
            Me.Show()
        Else
            odswierz()
        End If


        If Not czy_osoba_z_biura() Then
            bBiuro = False
        End If

        If IIf(IsDBNull(strKomunikatDlaUzytkownika), "", strKomunikatDlaUzytkownika) <> "" Then
            Dim f As New frmKomunikatDlaUzytkownika
            f.rtbKomunikat.Rtf = strKomunikatDlaUzytkownika
            f.ShowDialog()
        End If

    End Sub

    Private Function czy_osoba_z_biura() As Boolean

        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.CzyBiuroWynik

        Try
            Cursor = Cursors.WaitCursor
            wsWynik = ws.CzyBiuro(frmGlowna.intIdUzytkownikZalogowany)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            'MsgBox("B³¹d komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegó³y b³êdu:" & ex.Message, MsgBoxStyle.Critical)
            Return False
        End Try

        If wsWynik.czy_biuro = 0 Then
            bBiuro = False
            Return False
        End If
        bBiuro = True
        Return True

    End Function
    Private Sub odswierz()
        Dim frm As New frmZmianaHasla_Tel_Email
        If Me.czyPierwszy Then
            'if
            frm.pierwszy = True
            'frm.MdiParent = Me
            frm.ShowDialog(Me)
        Else
            frm.pierwszy = False
        End If

        MyBase.Wlacz(sesja)
        CzyPotrzebnyScrollWMenu()
    End Sub

    Private Sub frmGlowna_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        lblStatus.Text = ""
        'PW: wywo³ujê metodê klasy c_sprawdz_funkcje aby sprawdziæ uprawnienia u¿ytkownika
        'pobieram czy po raz pierwszy

        'Dim i As Integer = Canvas.GetZIndex(pboxLogo)
        'pboxLogo.
        odswierz()
        'sprawdz.sprawdz_uprawnienia(sesja)
    End Sub

    'Private Sub StanyToolStripMenuItem1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles StanyToolStripMenuItem1.Click
    '    Dim frm As frmStan = New frmStan
    '    frm.MdiParent = Me
    '    frm.Show()
    'End Sub

    Private Sub ZamowieniaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ZamowieniaToolStripMenuItem.Click
        'Dim frm As frmZamowienia = New frmZamowienia
        'frm.MdiParent = Me
        'frm.Show()
        If frmGlowna.frmZamowieniaLista Is Nothing Then
            'Nie, forma nie jest otwarta - ³adujemy
            Dim frm As New frmZamowienia
            frmGlowna.frmZamowieniaLista = frm
            frm.MdiParent = Me
            frm.frmRodzic = Me
            frm.Show()

        Else
            'Tak, forma otwarta - pokazujemy
            frmGlowna.frmZamowieniaLista.WindowState = FormWindowState.Normal
            frmGlowna.frmZamowieniaLista.Activate()
        End If
    End Sub

    Private Sub KoszykToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KoszykToolStripMenuItem.Click
        'pokazujemy koszyk; czy koszyk jest ju¿ otwarty?
        If frmGlowna.frmKoszyk Is Nothing Then
            'Nie, koszyk nie jest otwarty - ³adujemy go
            Dim frm As New frmZamowienie
            frm.intIdZamowienia = -1
            frm.MdiParent = Me
            frm.frmRodzic = Me
            frm.Show()

        Else
            'Tak, koszyk otwarty - pokazujemy go
            frmGlowna.frmKoszyk.WindowState = FormWindowState.Normal
            frmGlowna.frmKoszyk.Activate()
        End If
    End Sub

    'Private Sub DodajLimitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DodajLimitToolStripMenuItem.Click
    '    Dim frm As New frmLimityZasilKontoUczestnika
    '    'frm.intIdZamowienia = -1
    '    frm.MdiParent = Me
    '    'frm.frmRodzic = Me
    '    frm.Show()
    'End Sub

    Private Sub DodajLimitUzytkownikowiToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DodajLimitUzytkownikowiToolStripMenuItem.Click
        Dim frm As New frmLimityDodajUzytkownik
        'frm.intIdZamowienia = -1
        frm.MdiParent = Me
        'frm.frmRodzic = Me
        frm.Show()
    End Sub

    Private Sub DodajLimitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DodajLimitToolStripMenuItem.Click
        Dim frm As New frmLimity
        'frm.intIdZamowienia = -1
        frm.MdiParent = Me
        'frm.frmRodzic = Me
        frm.Show()
    End Sub

    Private Sub ZmienHasloToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KontoToolStripMenuItem.Click
        Dim frm As New frmUzytkownikPodstawoweDane
        frm.intIdUzytkownika = intIdUzytkownikZalogowany
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub GrupyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GrupyToolStripMenuItem.Click
        Dim frm As New frmGrupy
        'frm.intIdZamowienia = -1
        frm.MdiParent = Me
        'frm.frmRodzic = Me
        frm.Show()
    End Sub

    Private Sub NewsletterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewsletterToolStripMenuItem.Click
        Dim frm As New frmNewsleterLista
        'frm.intIdZamowienia = -1
        frm.MdiParent = Me
        'frm.frmRodzic = Me
        frm.Show()
    End Sub

    Private Sub TestToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim frm As New frmNewsletter
        'frm.intIdZamowienia = -1
        frm.MdiParent = Me
        'frm.frmRodzic = Me
        frm.Show()
    End Sub

    Private Sub WiadomosciToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WiadomosciToolStripMenuItem.Click
        Dim frm As New frmWiadomosci
        'frm.intIdZamowienia = -1
        frm.MdiParent = Me
        'frm.frmRodzic = Me
        frm.Show()
    End Sub

    Private Sub PlikiDoPobraniaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PlikiDoPobraniaToolStripMenuItem.Click
        Dim frm As New frmPlikiDoPobrania
        'frm.intIdZamowienia = -1
        frm.MdiParent = Me
        'frm.frmRodzic = Me
        frm.Show()
    End Sub

    Private Sub Stany_POSMToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim frm As New frmRaportStanow
        'frm.intIdZamowienia = -1
        frm.MdiParent = Me
        'frm.frmRodzic = Me
        frm.Show()
    End Sub

    'Private Sub RozchodyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RozchodyToolStripMenuItem.Click
    '    
    'End Sub


    'Private Sub RaportZamowieniaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RaportZamowieniaToolStripMenuItem.Click

    'End Sub

    'Private Sub ZwrotyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim frm As New frmZwroty
    '    frm.MdiParent = Me
    '    frm.Show()
    'End Sub

    Private Sub ZwrotyZZamowienToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ZwrotyZZamowienToolStripMenuItem.Click
        Dim frm As New frmZwroty
        'frm.intIdZamowienia = -1
        frm.MdiParent = Me
        'frm.frmRodzic = Me
        frm.Show()
    End Sub

    Private Sub RaportStanyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RaportStanyToolStripMenuItem.Click
        Dim frm As New frmRaportStanow
        'frm.intIdZamowienia = -1
        frm.MdiParent = Me
        'frm.frmRodzic = Me
        frm.Show()
    End Sub

    Private Sub StanyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StanyToolStripMenuItem.Click
        Dim frm As New frmStan
        'frm.intIdZamowienia = -1
        frm.MdiParent = Me
        'frm.frmRodzic = Me
        frm.Show()
    End Sub

    Private Sub RozchodyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RaportRozchodyToolStripMenuItem.Click
        Dim frm As New frmRaportRozchodFiltr
        'frm.intIdZamowienia = -1
        frm.MdiParent = Me
        'frm.frmRodzic = Me
        frm.Show()
    End Sub

    Private Sub RapotrZamownieniaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RaportZamownieniaToolStripMenuItem.Click
        Dim frm As New frmRaportZamowienFiltr
        'frm.intIdZamowienia = -1
        frm.MdiParent = Me
        frm.bWersjaZDanymiUzytkownika = False
        frm.bWersjaZPozycjami = False
        'frm.frmRodzic = Me
        frm.Show()
    End Sub


    Private Sub RaportLimityToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RaportLimityToolStripMenuItem.Click
        Dim frm As New frmRaportLimityFiltr
        'frm.intIdZamowienia = -1
        frm.MdiParent = Me
        'frm.frmRodzic = Me
        frm.Show()
    End Sub


    Private Sub ZwrotyBezZamowienToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ZwrotyBezZamowienToolStripMenuItem.Click
        Dim frm As New frmZwrotyBezZamowien
        'frm.intIdZamowienia = -1
        frm.MdiParent = Me
        'frm.frmRodzic = Me
        frm.Show()
    End Sub

    Private Sub SkladanieZamowienToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SkladanieZamowienToolStripMenuItem.Click
        Dim frm As New frmZamowieniaZExcela
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub PobieraniePlikowToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles PobieraniePlikowToolStripMenuItem.Click
        Dim frm As New frmPobieraniePlikowExcelaZBazy
        frm.MdiParent = Me
        frm.Show()
    End Sub



    'Private Sub ms_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles ms.ItemClicked
    '    For Each btn As ToolStripMenuItem In ms.Items
    '        If e.ClickedItem.Text = btn.Text Then
    '            WybranyItem = btn.Text
    '            btn.ForeColor = _color
    '            Exit Sub
    '        End If
    '    Next
    'End Sub

    'Private Sub ms_MenuActivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles ms.MenuActivate
    '    For Each btn As ToolStripMenuItem In ms.Items
    '        If Not Zaznaczony Is Nothing Then
    '            If btn.Text = WybranyItem Then
    '                btn.ForeColor = _color
    '            End If

    '            'btn.ForeColor = Color.White
    '        End If
    '    Next
    'End Sub

    'Private Sub ms_MenuDeactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles ms.MenuDeactivate
    '    For Each btn As ToolStripMenuItem In ms.Items
    '        If btn.Text = WybranyItem Then
    '            btn.ForeColor = Color.White
    '        End If
    '    Next
    'End Sub

    'Private Sub ms_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles ms.MouseHover

    '    For Each btn As ToolStripMenuItem In ms.Items
    '        _color = btn.BackColor
    '        If Not btn.IsOnDropDown Then
    '            If btn.Text = WybranyItem Then
    '                btn.ForeColor = Color.White
    '            End If
    '        Else
    '            btn.ForeColor = _color
    '        End If


    '        If btn.Selected = True Then
    '            btn.BackColor = _color
    '            Zaznaczony = btn.Text
    '        Else
    '            btn.BackColor = _color
    '        End If

    '        If Not WybranyItem Is Nothing Then
    '            If WybranyItem = Zaznaczony Then
    '                btn.ForeColor = _color
    '            End If
    '        Else
    '            btn.ForeColor = Color.White
    '            WybranyItem = Nothing
    '        End If

    '        If btn.IsOnDropDown = True Then

    '            btn.ForeColor = _color
    '        Else : btn.ForeColor = Color.White
    '        End If

    '    Next
    '    WybranyItem = Nothing
    'End Sub

    'Private Sub ms_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles ms.MouseLeave
    '    For Each btn As ToolStripMenuItem In ms.Items
    '        If Not btn.Text = WybranyItem Then
    '            btn.ForeColor = Color.White
    '        End If
    '    Next
    '    'Zaznaczony = Nothing
    'End Sub


    Private Sub PomocToolStripMenuItem_DropDownOpening(ByVal sender As Object, ByVal e As System.EventArgs) Handles PomocToolStripMenuItem.DropDownOpening
        PomocToolStripMenuItem.ForeColor = blue
    End Sub

    Private Sub PomocToolStripMenuItem_DropDownClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles PomocToolStripMenuItem.DropDownClosed
        PomocToolStripMenuItem.ForeColor = System.Drawing.Color.White
    End Sub

    Private Sub LimityToolStripMenuItem_DropDownClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles LimityToolStripMenuItem.DropDownClosed
        LimityToolStripMenuItem.ForeColor = System.Drawing.Color.White
    End Sub
    ''''
    Private Sub LimityToolStripMenuItem_DropDownOpening(ByVal sender As Object, ByVal e As System.EventArgs) Handles LimityToolStripMenuItem.DropDownOpening
        LimityToolStripMenuItem.ForeColor = blue
    End Sub

    'Private Sub GrupyToolStripMenuItem_DropDownClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles GrupyToolStripMenuItem.DropDownClosed
    '    GrupyToolStripMenuItem.ForeColor = Color.White
    'End Sub

    'Private Sub GrupyToolStripMenuItem_DropDownOpening(ByVal sender As Object, ByVal e As System.EventArgs) Handles GrupyToolStripMenuItem.DropDownOpening
    '    GrupyToolStripMenuItem.ForeColor = bordo
    'End Sub

    'Private Sub NewsletterToolStripMenuItem_DropDownClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles NewsletterToolStripMenuItem.DropDownClosed
    '    NewsletterToolStripMenuItem.ForeColor = Color.White
    'End Sub

    'Private Sub NewsletterToolStripMenuItem_DropDownOpening(ByVal sender As Object, ByVal e As System.EventArgs) Handles NewsletterToolStripMenuItem.DropDownOpening
    '    NewsletterToolStripMenuItem.ForeColor = bordo
    'End Sub

    'Private Sub PlikiDoPobraniaToolStripMenuItem_DropDownClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles PlikiDoPobraniaToolStripMenuItem.DropDownClosed
    '    PlikiDoPobraniaToolStripMenuItem.ForeColor = Color.White
    'End Sub

    'Private Sub PlikiDoPobraniaToolStripMenuItem_DropDownOpening(ByVal sender As Object, ByVal e As System.EventArgs) Handles PlikiDoPobraniaToolStripMenuItem.DropDownOpening
    '    PlikiDoPobraniaToolStripMenuItem.ForeColor = bordo
    'End Sub


    'Private Sub RaportyToolStripMenuItem_DropDownClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles RaportyToolStripMenuItem.DropDownClosed
    '    RaportLimityToolStripMenuItem.ForeColor = Color.White
    'End Sub

    'Private Sub RaportyToolStripMenuItem_DropDownOpening(ByVal sender As Object, ByVal e As System.EventArgs) Handles RaportyToolStripMenuItem.DropDownOpening
    '    RaportLimityToolStripMenuItem.ForeColor = bordo
    'End Sub

    'Private Sub StanyToolStripMenuItem_DropDownClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles StanyToolStripMenuItem.DropDownClosed
    '    StanyToolStripMenuItem.ForeColor = Color.White
    'End Sub

    'Private Sub StanyToolStripMenuItem_DropDownOpening(ByVal sender As Object, ByVal e As System.EventArgs) Handles StanyToolStripMenuItem.DropDownOpening
    '    StanyToolStripMenuItem.ForeColor = bordo
    'End Sub

    Private Sub SystemToolStripMenuItem_DropDownClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles SystemToolStripMenuItem.DropDownClosed
        SystemToolStripMenuItem.ForeColor = System.Drawing.Color.White
    End Sub

    Private Sub SystemToolStripMenuItem_DropDownOpening(ByVal sender As Object, ByVal e As System.EventArgs) Handles SystemToolStripMenuItem.DropDownOpening
        SystemToolStripMenuItem.ForeColor = blue
    End Sub


    'Private Sub UzytkownicyToolStripMenuItem_DropDownClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles UzytkownicyToolStripMenuItem.DropDownClosed
    '    UzytkownicyToolStripMenuItem.ForeColor = Color.White
    'End Sub

    'Private Sub UzytkownicyToolStripMenuItem_DropDownOpening(ByVal sender As Object, ByVal e As System.EventArgs) Handles UzytkownicyToolStripMenuItem.DropDownOpening
    '    UzytkownicyToolStripMenuItem.ForeColor = bordo
    'End Sub

    'Private Sub WiadomosciToolStripMenuItem_DropDownClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles WiadomosciToolStripMenuItem.DropDownClosed
    '    WiadomosciToolStripMenuItem.ForeColor = Color.White
    'End Sub

    'Private Sub WiadomosciToolStripMenuItem_DropDownOpening(ByVal sender As Object, ByVal e As System.EventArgs) Handles WiadomosciToolStripMenuItem.DropDownOpening
    '    WiadomosciToolStripMenuItem.ForeColor = bordo
    'End Sub

    'Private Sub ZamowieniaToolStripMenuItem_DropDownClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles ZamowieniaToolStripMenuItem.DropDownClosed
    '    ZamowieniaToolStripMenuItem.ForeColor = Color.White
    'End Sub

    'Private Sub ZamowieniaToolStripMenuItem_DropDownOpening(ByVal sender As Object, ByVal e As System.EventArgs) Handles ZamowieniaToolStripMenuItem.DropDownOpening
    '    ZamowieniaToolStripMenuItem.ForeColor = bordo
    'End Sub

    'Private Sub KoszykToolStripMenuItem_DropDownClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles KoszykToolStripMenuItem.DropDownClosed
    '    KoszykToolStripMenuItem.ForeColor = Color.White
    'End Sub

    'Private Sub KoszykToolStripMenuItem_DropDownOpening(ByVal sender As Object, ByVal e As System.EventArgs) Handles KoszykToolStripMenuItem.DropDownOpening
    '    KoszykToolStripMenuItem.ForeColor = bordo
    'End Sub

    Private Sub ZwrotyToolStripMenuItem_DropDownClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles ZwrotyToolStripMenuItem.DropDownClosed
        ZwrotyToolStripMenuItem.ForeColor = System.Drawing.Color.White
    End Sub

    Private Sub ZwrotyToolStripMenuItem_DropDownOpening(ByVal sender As Object, ByVal e As System.EventArgs) Handles ZwrotyToolStripMenuItem.DropDownOpening
        ZwrotyToolStripMenuItem.ForeColor = blue
    End Sub

    Private Sub RaportyToolStripMenuItem_DropDownClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles RaportyToolStripMenuItem.DropDownClosed
        RaportyToolStripMenuItem.ForeColor = System.Drawing.Color.White
    End Sub


    Private Sub RaportyToolStripMenuItem_DropDownOpening(ByVal sender As Object, ByVal e As System.EventArgs) Handles RaportyToolStripMenuItem.DropDownOpening
        RaportyToolStripMenuItem.ForeColor = blue
    End Sub

    Private Sub ZamówieniaZPlikuExcelaToolStripMenuItem_DropDownClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles ZamowieniaZPlikuExcelaToolStripMenuItem.DropDownClosed
        ZamowieniaZPlikuExcelaToolStripMenuItem.ForeColor = System.Drawing.Color.White
    End Sub


    Private Sub ZamówieniaZPlikuExcelaToolStripMenuItem_DropDownOpening(ByVal sender As Object, ByVal e As System.EventArgs) Handles ZamowieniaZPlikuExcelaToolStripMenuItem.DropDownOpening
        ZamowieniaZPlikuExcelaToolStripMenuItem.ForeColor = blue
    End Sub


    'Private Sub SkladanieZamowienToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SkladanieZamowienToolStripMenuItem.Click
    '    Dim frm As New frmZamowieniaZExcela
    '    frm.MdiParent = Me
    '    frm.Show()
    'End Sub

    'Private Sub PobieraniePlikowToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles PobieraniePlikowToolStripMenuItem.Click
    '    Dim frm As New frmPobieraniePlikowExcelaZBazy
    '    frm.MdiParent = Me
    '    frm.Show()
    'End Sub


    Private Sub AwizacjeToolStripMenuItem_DropDownClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles AwizacjeToolStripMenuItem.DropDownClosed
        AwizacjeToolStripMenuItem.ForeColor = System.Drawing.Color.White
    End Sub

    Private Sub AwizacjeToolStripMenuItem_DropDownOpening(ByVal sender As Object, ByVal e As System.EventArgs) Handles AwizacjeToolStripMenuItem.DropDownOpening
        AwizacjeToolStripMenuItem.ForeColor = blue
    End Sub

    Private Sub PodrecznikUzytkownikaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PodrecznikUzytkownikaToolStripMenuItem.Click

        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik As New wsCursorProf.PobierzPlikWynik

        'pobieranie pliku przez usera
        Try
            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.PobierzPlik(Parametry._NazwaPodrecznika)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("B³¹d komunikacji z serwerem" & Me.kontaktIt & vbNewLine & vbNewLine & "Szczegó³y b³êdu:" & ex.Message, MsgBoxStyle.Critical, "Pobieranie pliku")

        End Try
        If wsWynik.status = -1 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Pobieranie pliku")
        End If
        If wsWynik.status = 0 Then

            Dim plik() As Byte = wsWynik.plik

            Dim fs As FileStream = File.Create(My.Computer.FileSystem.SpecialDirectories.MyDocuments & Parametry._NazwaPodrecznika, FileAccess.Write)

            fs.Write(plik, 0, plik.Length)
            fs.Flush()
            fs.Close()
            fs.Dispose()

            System.Diagnostics.Process.Start(My.Computer.FileSystem.SpecialDirectories.MyDocuments & Parametry._NazwaPodrecznika)

        End If
    End Sub

    Private Sub KontaktToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KontaktToolStripMenuItem.Click
        Dim frm As New frmKontakt
        'frm.intIdZamowienia = -1
        frm.MdiParent = Me
        'frm.frmRodzic = Me
        frm.Show()
    End Sub

    Private Sub DodajAwizoToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DodajAwizoToolStripMenuItem1.Click
        If frmAwizaNew Is Nothing Then

            'Nie, koszyk nie jest otwarty - ³adujemy go
            Dim frm As New frmAwizacje
            frmAwizaNew = frm
            frm.MdiParent = Me
            frm.strFunkcjaPowiadomieniaOGotowosci = "odswiezListy"
            frm.frmRodzic = Me
            frm.Show()

        Else
            'Tak, koszyk otwarty - pokazujemy go
            frmGlowna.frmAwizaNew.WindowState = FormWindowState.Normal
            frmGlowna.frmAwizaNew.Activate()
        End If
    End Sub

    Private Sub ZarzadzanieAwizamiToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ZarzadzanieAwizamiToolStripMenuItem1.Click
        If frmAwizaZarzadzanie Is Nothing Then
            'Nie, koszyk nie jest otwarty - ³adujemy go
            Dim frm As New frmZarzadzanieAwizami
            frmAwizaZarzadzanie = frm
            frm.MdiParent = Me
            frm.frmRodzic = Me
            frm.Show()
        Else
            'Tak, koszyk otwarty - pokazujemy go
            frmGlowna.frmAwizaZarzadzanie.WindowState = FormWindowState.Normal
            frmGlowna.frmAwizaZarzadzanie.Activate()
        End If
    End Sub


    Private Sub frmGlowna_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim ctl As Control
        Dim ctlMDI As MdiClient

        ' Loop through all of the form's controls looking
        ' for the control of type MdiClient.
        For Each ctl In Me.Controls
            Try
                ' Attempt to cast the control to type MdiClient.
                ctlMDI = CType(ctl, MdiClient)

                ' Set the BackColor of the MdiClient control.
                ctlMDI.BackColor = Me.BackColor

            Catch exc As InvalidCastException
                ' Catch and ignore the error if casting failed.
            End Try
        Next
    End Sub

    Private Sub LimityToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles LimityToolStripMenuItem.Click

    End Sub

    Private Sub ZamowieniaZDanymiUseraToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ZamowieniaZDanymiUseraToolStripMenuItem.Click
        Dim frm As New frmRaportZamowienFiltr
        frm.MdiParent = Me
        frm.bWersjaZDanymiUzytkownika = True
        frm.bWersjaZPozycjami = False
        frm.Text = "Raport zamówieñ z danymi u¿ytkownika"
        frm.Show()
    End Sub

    Private Sub EdycjaKomunikatowToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)
        Dim frm As New frmKomunikatyLista
        frm.MdiParent = Me
        frm.Show()
    End Sub


    Private Sub KomunikatyStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles KomunikatyStripMenuItem.Click
        Dim frm As New frmKomunikatyLista
        frm.MdiParent = Me
        frm.Show()
    End Sub

    'Private Sub LimityLogistyczneToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles LimityLogistyczneToolStripMenuItem.Click
    '    Dim frm As New frmLimityLogistyczneDlaSku
    '    frm.MdiParent = Me
    '    frm.Show()
    'End Sub

    Private Sub RaportPrzyjecToolStripMenuItem_Click(sender As Object, e As System.EventArgs) Handles RaportPrzyjecToolStripMenuItem.Click
        Dim frm As New frmRaportPrzyjeciaPoDatachFiltr
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub RaportWejscWyjscToolStripMenuItem_Click(sender As Object, e As System.EventArgs) Handles RaportWejscWyjscToolStripMenuItem.Click
        Dim frm As New frmRaportWejsciaWyjscia
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub RaportZamowieniaPozycjeToolStripMenuItem_Click(sender As Object, e As System.EventArgs) Handles RaportZamowieniaPozycjeToolStripMenuItem.Click
        Dim frm As New frmRaportZamowienFiltr
        frm.MdiParent = Me
        frm.bWersjaZDanymiUzytkownika = False
        frm.bWersjaZPozycjami = True
        frm.Show()
    End Sub

    Private Sub RaportRentownosciToolStripMenuItem_Click(sender As Object, e As System.EventArgs) Handles RaportRentownosciToolStripMenuItem.Click
        Dim frm As New frmRaportRentownosci
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub HScrollBar1_Scroll(sender As System.Object, e As System.Windows.Forms.ScrollEventArgs) Handles HScrollBar1.Scroll

        For Each ctr As Control In Me.Controls
            If (Not TypeOf ctr Is StatusStrip) And (TypeOf ctr Is MenuStrip Or TypeOf ctr Is ToolStrip) Then
                If TypeOf ctr Is MenuStrip Then
                    For Each item As ToolStripItem In CType(ctr, MenuStrip).Items
                        If e.ScrollOrientation = 0 Then
                            Dim i As Integer = 0
                            Do While i < MojeItemyWMenu.Rows.Count
                                If MojeItemyWMenu.Rows(i).Item("item") = item.Name Then
                                    If i < e.NewValue Then
                                        item.Visible = False
                                    Else
                                        item.Visible = True
                                    End If
                                End If
                                i = i + 1
                            Loop
                        End If
                    Next
                End If
            End If

        Next
    End Sub

    Sub CzyPotrzebnyScrollWMenu()

        Dim odjac As Integer = 0
        ' MojeItemyWMenu = Nothing
        MojeItemyWMenu = New DataTable
        dlugoscItemowMenu = 0
        If MojeItemyWMenu Is Nothing OrElse MojeItemyWMenu.Columns.Contains("item") = False Then MojeItemyWMenu.Columns.Add("item", GetType(String))

        MojeItemyWMenu.Rows.Clear()

        For Each ctr As Control In Me.Controls
            If (Not TypeOf ctr Is StatusStrip) And (TypeOf ctr Is MenuStrip Or TypeOf ctr Is ToolStrip) Then
                If TypeOf ctr Is MenuStrip Then
                    For Each item As ToolStripItem In CType(ctr, MenuStrip).Items
                        If item.Visible = True Then

                            dlugoscItemowMenu = dlugoscItemowMenu + item.Width
                            Dim wiersz As DataRow = MojeItemyWMenu.NewRow
                            wiersz("item") = item.Name
                            MojeItemyWMenu.Rows.Add(wiersz)

                            If dlugoscItemowMenu < (Me.Size.Width / 2) Then odjac = odjac + 1

                        End If
                    Next
                Else
                    For Each item As ToolStripItem In CType(ctr, MenuStrip).Items
                        If item.Visible = True Then

                            dlugoscItemowMenu = dlugoscItemowMenu + item.Width
                            Dim wiersz As DataRow = MojeItemyWMenu.NewRow
                            wiersz("item") = item.Name
                            MojeItemyWMenu.Rows.Add(wiersz)

                            If dlugoscItemowMenu < (Me.Size.Width / 2) Then odjac = odjac + 1

                        End If
                    Next
                End If

            End If

        Next
        If Me.Size.Width - (dlugoscItemowMenu + 22) < 0 Then
            HScrollBar1.Visible = True
            HScrollBar1.Maximum = MojeItemyWMenu.Rows.Count '- odjac
            HScrollBar1.LargeChange = MojeItemyWMenu.Rows.Count / 2
        Else
            HScrollBar1.Visible = False
        End If

    End Sub

    Private Sub frmGlowna_SizeChanged(sender As Object, e As System.EventArgs) Handles Me.SizeChanged
        If Me.Size.Width - (dlugoscItemowMenu + 22) < 0 Then
            HScrollBar1.Visible = True
            HScrollBar1.Maximum = MojeItemyWMenu.Rows.Count '- odjac
            HScrollBar1.LargeChange = MojeItemyWMenu.Rows.Count / 2
        Else
            HScrollBar1.Visible = False
            For Each ctr As Control In Me.Controls
                If (Not TypeOf ctr Is StatusStrip) And (TypeOf ctr Is MenuStrip Or TypeOf ctr Is ToolStrip) Then
                    If TypeOf ctr Is MenuStrip Then
                        For Each item As ToolStripItem In CType(ctr, MenuStrip).Items
                            If Not IsNothing(MojeItemyWMenu) AndAlso Not IsDBNull(MojeItemyWMenu) AndAlso MojeItemyWMenu.Rows.Count > 0 Then
                                Dim i As Integer = 0
                                Do While i < MojeItemyWMenu.Rows.Count
                                    If MojeItemyWMenu.Rows(i).Item("item") = item.Name Then

                                        item.Visible = True

                                    End If
                                    i = i + 1
                                Loop
                            End If
                        Next
                    End If
                End If

            Next

        End If
        Me.Refresh()
    End Sub

    Private Sub RaportPodzialowToolStripMenuItem_Click(sender As Object, e As System.EventArgs) Handles RaportPodzialowToolStripMenuItem.Click
        Dim frm As New frmRaportPodzialowFiltr
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub RaportAwizToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles RaportAwizToolStripMenuItem.Click
        Dim frm As New frmRaportAwiz
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub RaportPodzia³ówWgSkuIGrupyToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles RaportPodzia³ówWgSkuIGrupyToolStripMenuItem.Click
        Dim frm As New frmRaportPodzialySkuGrupaObdzielana
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub RaportHistoriiMaterialuToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles RaportHistoriiMaterialuToolStripMenuItem.Click
        Dim frm As New frmRaportHistoriiMaterialu
        frm.MdiParent = Me
        frm.sku = ""
        frm.Show()
    End Sub

    Private Sub NewsletterToolStripMenuItem_Click_1(sender As System.Object, e As System.EventArgs) Handles NewsletterToolStripMenuItem.Click

    End Sub

    Private Sub RaportPaletodniToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles RaportPaletodniToolStripMenuItem.Click
        Dim frm As New frmRaportPaletodniFiltr
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub NotyfikacjeToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles NotyfikacjeToolStripMenuItem.Click
        Dim frm As New frmNotyfikacjeUzytkownikow
        frm.MdiParent = Me
        frm.Show()
    End Sub

	Private Sub ZamowienieINVToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ZamowienieINVToolStripMenuItem.Click
        'pokazujemy koszyk; czy koszyk jest ju¿ otwarty?
        If frmGlowna.frmKoszykINV Is Nothing Then
            'Nie, koszyk nie jest otwarty - ³adujemy go
            Dim frm As New frmZamowienieINV
            frm.intIdZamowienia = -1
            frm.MdiParent = Me
            frm.frmRodzic = Me
            frm.Show()
        Else
            'Tak, koszyk otwarty - pokazujemy go
            frmGlowna.frmKoszykINV.WindowState = FormWindowState.Normal
            frmGlowna.frmKoszykINV.Activate()
        End If
    End Sub

 Private Sub KopiujAdresyToolStripMenuItem_Click(sender As Object, e As System.EventArgs) Handles KopiujAdresyToolStripMenuItem.Click
        Dim frm As New frmAdresKopiuj
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub RaportAwizaDostawyToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles RaportAwizaDostawyToolStripMenuItem.Click
        Dim frm As New frmRaportAwizaDostawa
        frm.MdiParent = Me
        frm.Show()
    End Sub

	Private Sub RaportCursorToolStripMenuItem_DropDownClosed(sender As Object, e As System.EventArgs) Handles RaportCursorToolStripMenuItem.DropDownClosed
        RaportCursorToolStripMenuItem.ForeColor = System.Drawing.Color.White
    End Sub

    Private Sub RaportCursorToolStripMenuItem_DropDownOpening(sender As Object, e As System.EventArgs) Handles RaportCursorToolStripMenuItem.DropDownOpening
        RaportCursorToolStripMenuItem.ForeColor = blue
    End Sub

Private Sub SlownikToolStripMenuItem_Click(sender As Object, e As System.EventArgs) Handles SlownikToolStripMenuItem.Click
        Dim frm As New frmSlownik
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub RaportRozliczeniowyToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles RaportRozliczeniowyToolStripMenuItem.Click
        Dim frm As New frmRaportRozliczenieFiltr
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub RaportTerminowoœciToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles RaportTerminowoœciToolStripMenuItem.Click
        Dim frm As New frmRaportTerminowosciFedEx
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub RaportStanówMagazynowychCa³oœciowyToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles RaportStanówMagazynowychCa³oœciowyToolStripMenuItem.Click
        Dim frm As New frmRaportStanowbezGrupy
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub RaportPodzia³yzamówieniaToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles RaportPodzia³yzamówieniaToolStripMenuItem.Click
        Dim frm As New frmRaportPodzialyZamowieniaPerUser
        frm.MdiParent = Me
        frm.Show()
    End Sub
End Class