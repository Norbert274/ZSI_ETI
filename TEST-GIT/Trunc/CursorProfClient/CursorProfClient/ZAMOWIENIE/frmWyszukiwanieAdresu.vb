
#Region "Imports"
Imports System.Text
Imports System.Data
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid
Imports DevExpress.XtraEditors

#End Region

Public Class frmWyszukiwanieAdresu


#Region "zmienne dla formy"


    Public intIdUzytkownika As Integer = -1
    Public intIdWybranegoAdresu As Integer
    Public adresyListaDS As DataSet
    Private iloscStron As Integer = 1
    Private numerStrony As Integer = 1
    Private iloscNaStronie As Integer = 100
    Private bReagujIloscNaStronie As Boolean = False
    Private bReagujNaSort As Boolean = False
    Private sortPo As String = "Miasto"

    Public dtWybranaPozycja As DataTable


#End Region

#Region "Kontrolki Ustawienia"

    Private Sub SetControlValues()
        bbiStronaNumer.EditValue = numerStrony
        If iloscNaStronie = 0 Then
            bbiStronaIloscLista.EditValue = "Wszystkie"
        Else
            bbiStronaIloscLista.EditValue = String.Format("{0,8:#######0}", iloscNaStronie)
        End If
        bbiSortPoLista.EditValue = sortPo

    End Sub

#End Region

#Region "Grid - kolumny"

    Private Sub FixGridCollumns()

        For Each gcolumn As DevExpress.XtraGrid.Columns.GridColumn In Me.DGV_Lista.Columns
            If cmbSortPo.Items.Contains(gcolumn.FieldName) = False Then
                Me.cmbSortPo.Items.Add(gcolumn.FieldName)
            End If

            gcolumn.OptionsColumn.AllowEdit = False
            gcolumn.OptionsColumn.ReadOnly = True

            Select Case UCase(gcolumn.FieldName)
                Case UCase([Enum].GetName(GetType(EnumSPAdresStrona), EnumSPAdresStrona.adres_id))
                    gcolumn.Caption = "Adres ID"
                    gcolumn.Visible = False
                    Exit Select
                Case UCase([Enum].GetName(GetType(EnumSPAdresStrona), EnumSPAdresStrona.nazwa))
                    gcolumn.Caption = "Nazwa adresu"
                    Exit Select
                Case UCase([Enum].GetName(GetType(EnumSPAdresStrona), EnumSPAdresStrona.kod))
                    gcolumn.Caption = "Kod pocztowy"
                    Exit Select
                Case UCase([Enum].GetName(GetType(EnumSPAdresStrona), EnumSPAdresStrona.miasto))
                    gcolumn.Caption = "Miasto"
                    Exit Select
                Case UCase([Enum].GetName(GetType(EnumSPAdresStrona), EnumSPAdresStrona.adres))
                    gcolumn.Caption = "Adres"
                    Exit Select
                Case [Enum].GetName(GetType(EnumSPAdresStrona), EnumSPAdresStrona.DOMYSLNY)
                    gcolumn.Caption = "Adres domyœlny"
                    Exit Select

            End Select
        Next
        DGV_Lista.BestFitColumns()

    End Sub

#End Region

#Region "Forma - frmWyszukiwanieAdresu"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

    End Sub

    Private Sub frmWyszukiwanieAdresuLista_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        SetControlValues()
    End Sub

    Private Sub frmWyszukiwanieAdresuLista_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown

    End Sub

    Private Sub frmWyszukiwanieAdresuLista_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        Me.Dispose()
    End Sub

#End Region

#Region "Forma - zdarzenia"

    Private Sub btnZnajdz_Click(sender As System.Object, e As System.EventArgs) Handles btnZnajdz.Click
        numerStrony = 1
        bbiStronaNumer.EditValue = 1
        AdresyListaOdswiez(txtTekstFiltru.Text)
    End Sub

    Private Sub txtTekstFiltru_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTekstFiltru.KeyPress

        Dim rowHandle As Integer = -1
        Dim szukajTxt As String = ""

        szukajTxt = Trim(NZ(Me.txtTekstFiltru.Text, ""))

        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Enter) Then
            e.Handled = True

            If szukajTxt.Length = 0 Then
                MessageBox.Show("Brak tekstu do wyszukania", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.txtTekstFiltru.Focus()
            Else

                If szukajTxt <> "" Then
                    AdresyListaOdswiez(szukajTxt)
                End If

            End If

        End If

    End Sub

    Private Sub btnShowHideFindPanel_Click(sender As System.Object, e As System.EventArgs) Handles btnShowHideFindPanel.Click
        If btnShowHideFindPanel.Text = "Poka¿ Panel Wyszukiwania" Then
            Me.DGV_Lista.ShowFindPanel()
            btnShowHideFindPanel.Text = "Ukryj Panel Wyszukiwania"
        Else
            Me.DGV_Lista.HideFindPanel()
            btnShowHideFindPanel.Text = "Poka¿ Panel Wyszukiwania"
        End If
    End Sub

#End Region

#Region "Forma - funkcje prywatne"



    Private Function FindRowHandleById(ByVal view As GridView, ByVal id As Integer) As Integer
        If Not id = Nothing Then
            Dim I As Integer
            For I = 0 To view.DataRowCount - 1
                If id = view.GetDataRow(I).Item([Enum].GetName(GetType(EnumSPAdresStrona), EnumSPAdresStrona.adres_id)) Then
                    Return I
                End If
            Next
            Return DevExpress.XtraGrid.GridControl.InvalidRowHandle
        End If
        Return Nothing
    End Function

#End Region

#Region "GridView - obsluga zdarzen"

    Private Sub DGV_Lista_DoubleClick(sender As Object, e As System.EventArgs) Handles DGV_Lista.DoubleClick

        Dim rowHandle As Integer = -1 ' wskaŸnik do aktualnie wybranej pozycji 

        If DGC_Lista.Focused Then
            rowHandle = DGV_Lista.FocusedRowHandle
        End If

        If rowHandle >= 0 Then
            dtWybranaPozycja.Clear()

            Dim DrowView As DataRowView = DGV_Lista.GetRow(rowHandle)
            Dim drow As DataRow = dtWybranaPozycja.NewRow
            drow.ItemArray = DrowView.Row.ItemArray

            dtWybranaPozycja.Rows.Add(drow)
            dtWybranaPozycja.AcceptChanges()

            Me.DialogResult = Windows.Forms.DialogResult.OK
        End If

    End Sub

    Private Sub DGV_Lista_KeyPress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs) Handles DGV_Lista.KeyPress

        Dim rowHandle As Integer = -1 ' wskaŸnik do aktualnie wybranej pozycji

        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Enter) Then
            e.Handled = True
            If DGV_Lista.RowCount > 0 Then
                If DGC_Lista.Focused Then
                    rowHandle = DGV_Lista.FocusedRowHandle
                End If

                If rowHandle >= 0 Then
                    Dim DrowView As DataRowView = DGV_Lista.GetRow(rowHandle)
                    Dim drow As DataRow = dtWybranaPozycja.NewRow
                    drow.ItemArray = DrowView.Row.ItemArray
                    dtWybranaPozycja.Clear()
                    dtWybranaPozycja.Rows.Add(drow)
                    Me.DialogResult = Windows.Forms.DialogResult.OK
                End If
            End If
        End If
    End Sub

    Private Sub DGV_Lista_RowCountChanged(sender As System.Object, e As System.EventArgs) Handles DGV_Lista.RowCountChanged
        Me.lblMasterRowCount.Text = "Liczba: " & Me.DGC_Lista.MainView.RowCount.ToString()
    End Sub

#End Region

#Region "Pobieranie i Zapis Danych"


    Private Function AdresyListaOdswiez(ByVal szukajTekst As String) As Boolean
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        'ws.Url = frmGlowna.strWebservice
        Dim wsWynik As wsCursorProf.AdresStronaWynik

        Dim sortowanieNarastajaco As Boolean = True
        Dim intIdWybranegoWiersza = Nothing
        Dim intNumerWybranejKolumny = Nothing

        'odczyt listy z serwera
        Try
            'budujemy listê ukrytych kolumn
            Dim dsKolumnyUkryte As New DataSet
            dsKolumnyUkryte.Tables.Add()
            dsKolumnyUkryte.Tables(0).Columns.Add("nazwa")

            Cursor = Cursors.WaitCursor
            Application.DoEvents()
            wsWynik = ws.AdresStrona(frmGlowna.sesja, intIdUzytkownika, False, numerStrony, _
                iloscNaStronie, szukajTekst, sortPo, sortowanieNarastajaco, _
                dsKolumnyUkryte)
            Cursor = Cursors.Default
        Catch ex As Exception
            Cursor = Cursors.Default
            MsgBox("B³¹d komunikacji z serwerem." & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegó³y b³êdu:" & ex.Message, MsgBoxStyle.Critical, "Wczytanie listy adresów")
            Return False
        End Try
        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical, "Wczytanie listy adresów")
            Return False
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation, "Wczytanie listy adresów")
        End If

        'czyszczenie kontrolek przed wype³nieniem
        DGC_Lista.DataSource = Nothing

       


        If wsWynik.status = 0 AndAlso Not (wsWynik.dane Is Nothing) Then

            If Not adresyListaDS Is Nothing Then
                adresyListaDS.Clear()
            End If

            adresyListaDS = wsWynik.dane



            If iloscNaStronie > 0 Then
                Dim dblIloscStron As Double = wsWynik.iloscStron
                iloscStron = Math.Ceiling(dblIloscStron)
                bbiTotalLiczbaWierszy.EditValue = wsWynik.ilosc_total_rekordow
                bbiStronaLiczba.EditValue = iloscStron
            Else
                iloscStron = 1
                bbiTotalLiczbaWierszy.EditValue = wsWynik.ilosc_total_rekordow
                bbiStronaLiczba.EditValue = 1
            End If

            Me.Text = "Lista adresów dla u¿ytkownika " & wsWynik.userAdresy
            If wsWynik.dane.Tables.Count > 0 Then

                If adresyListaDS.Tables.Count > 0 Then
                    DGC_Lista.DataSource = adresyListaDS.Tables(0)
                    dtWybranaPozycja = adresyListaDS.Tables(0).Clone
                End If
                FixGridCollumns()
            Else
                MsgBox("B³¹d wewnêtrzny systemu. Serwer nie przes³a³ listy adresów zdefiniowanych dla u¿ytkownika." & frmGlowna.kontaktIt, MsgBoxStyle.Critical, "Lista adresów zdefiniowanych")
                Me.Close()
            End If

        End If

        
        Return True
    End Function

#End Region

#Region "Funkcje Pomocnicze"

    Public Function NZ(ByVal S As Object, ByVal Def As Object) As Object
        If IsDBNull(S) Then
            Return Def
        Else
            If Not (S Is Nothing) Then
                Return (S)
            Else
                Return Def
            End If
        End If
    End Function

#End Region

#Region "Stronicowanie"

    Private Sub bbiStronaNumer_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles bbiStronaNumer.EditValueChanged

        Dim intNumerStrony As Integer

        If Not Integer.TryParse(bbiStronaNumer.EditValue, intNumerStrony) Then
            MsgBox("Numer ekranu musi byæ liczb¹", MsgBoxStyle.Critical)
            Return
        End If

        If intNumerStrony = numerStrony Then Return 'u¿ytkownik nie zmieni³ numeru strony, nic nie robimy

        numerStrony = intNumerStrony
        AdresyListaOdswiez(txtTekstFiltru.Text)

    End Sub

    Private Sub bbiPierwszaStrona_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbiPierwszaStrona.ItemClick
        If bbiStronaNumer.EditValue = 1 Then
            MsgBox("To jest pierwsza strona.", MsgBoxStyle.Exclamation)
            Return
        End If
        bbiStronaNumer.EditValue = 1
    End Sub

    Private Sub bbiPoprzedniaStrona_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbiPoprzedniaStrona.ItemClick
        If bbiStronaNumer.EditValue = 1 Then
            MsgBox("To jest pierwsza strona. Nie mo¿esz przejœæ do poprzedniej strony.", MsgBoxStyle.Exclamation)
            Return
        End If
        bbiStronaNumer.EditValue = bbiStronaNumer.EditValue - 1
    End Sub

    Private Sub bbiNastepnaStrona_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbiNastepnaStrona.ItemClick
        If bbiStronaNumer.EditValue >= iloscStron Then
            MsgBox("To jest ostatnia strona. Nie mo¿esz przejœæ do nastêpnej strony.", MsgBoxStyle.Exclamation)
            Return
        End If
        bbiStronaNumer.EditValue = bbiStronaNumer.EditValue + 1
    End Sub

    Private Sub bbiOstatniaStrona_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles bbiOstatniaStrona.ItemClick
        If bbiStronaNumer.EditValue = iloscStron Then
            MsgBox("To jest ostatnia strona.", MsgBoxStyle.Exclamation)
            Return
        End If
        bbiStronaNumer.EditValue = iloscStron
    End Sub

    Private Sub bbiSortPoLista_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles bbiSortPoLista.EditValueChanged

        sortPo = bbiSortPoLista.EditValue

        If bReagujNaSort Then
            numerStrony = 1
            bbiStronaNumer.EditValue = 1
            AdresyListaOdswiez(txtTekstFiltru.Text)
        End If

    End Sub

    Private Sub bbiStronaIloscLista_EditValueChanged(sender As System.Object, e As System.EventArgs) Handles bbiStronaIloscLista.EditValueChanged

        iloscNaStronie = Integer.Parse(IIf(bbiStronaIloscLista.EditValue = "Wszystkie", "0", LTrim(bbiStronaIloscLista.EditValue)))

        If bReagujIloscNaStronie Then
            numerStrony = 1
            bbiStronaNumer.EditValue = 1
            AdresyListaOdswiez(txtTekstFiltru.Text)
        End If

    End Sub

#End Region

    Private Sub DGC_Lista_Click(sender As System.Object, e As System.EventArgs) Handles DGC_Lista.Click

    End Sub
End Class