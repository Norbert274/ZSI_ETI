Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.IO
Imports log4net

<System.Web.Services.WebService(Namespace:="https://superzsi.cursor.pl/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class CursorService
    Inherits System.Web.Services.WebService
    Private Shared ReadOnly logger As ILog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)

    'Private database As String = "SUPERZSI_PROD"
    Private Url As String
    Private SciezkaZalaczniki As String = "\\nbg-srv-32\Projekty\ETI\ZalacznikiAplikacji\"
    Public connectionStringQguar = "Server=qguar55;Uid=zsi55;Pwd=cursor"
    Private connectionString = "Data Source=NBG-SRV-32;Initial Catalog=ETI_PROD;User Id=ETI_USER;Password=W!n2eTuu;Connection Timeout=240;"
    Public kontaktIt = " Przekaż ten problem do systemu zgłoszeń działu IT firmy Cursor: it@cursor.pl"


    <WebMethod()> _
    Public Function HelloWorld() As String
        Return "Hello World"
    End Function


    <WebMethod()> _
    Public Function Zaloguj_szablon(ByVal login As String, ByVal haslo As String, ByVal wersja As String, _
           ByVal komputer As String) As ZalogujWynik
        Dim wynik As New ZalogujWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
            logger.Info("Zaczynamy logowanie")
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("Zaloguj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_ZALOGUJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandTimeout = 10
        cmd.Parameters.AddWithValue("@login", login)
        cmd.Parameters.AddWithValue("@haslo", haslo)
        cmd.Parameters.AddWithValue("@wersja", wersja)
        cmd.Parameters.AddWithValue("@komputer", komputer)
        cmd.Parameters.AddWithValue("@adres", Me.Context.Request.UserHostAddress)
        cmd.Parameters.Add("@sesja", SqlDbType.VarBinary, 16).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@uzytkownik_id", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@uzytkownik", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@telefon", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@czy_pierwszy", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@komunikat", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("Zaloguj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.sesja = cmd.Parameters("@sesja").Value
            wynik.uzytkownik_id = cmd.Parameters("@uzytkownik_id").Value
            wynik.uzytkownik = IIf(IsDBNull(cmd.Parameters("@uzytkownik").Value), "", cmd.Parameters("@uzytkownik").Value)
            wynik.telefon = IIf(IsDBNull(cmd.Parameters("@telefon").Value), "", cmd.Parameters("@telefon").Value)
            wynik.czy_pierwszy = IIf(IsDBNull(cmd.Parameters("@czy_pierwszy").Value), True, cmd.Parameters("@czy_pierwszy").Value)
            wynik.komunikat_dla_uzytkownika = IIf(IsDBNull(cmd.Parameters("@komunikat").Value), "", cmd.Parameters("@komunikat").Value)
            wynik.database = cnn.Database
        End If
        cnn.Close()
        Return wynik
    End Function


    <WebMethod(Description:="Metoda przesyła upranienia jakie posiada zalogowany użytkownik.")> _
    Public Function SprawdzFunkcje(ByVal sesja As Byte()) As SprawdzFunkcjeWynik
        '
        'ByVal user_id As Integer

        Dim wynik As New SprawdzFunkcjeWynik
        Dim cnn As SqlConnection
		
        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            cnn.Close()
            logger.Error("SprawdzFunkcje:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_SPRAWDZ_FUNKCJE", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@SESJA", sesja)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("SprawdzFunkcje:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.dane = ds
        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function Stan(ByVal sesja As Byte(), ByVal magazyn_id As Integer, ByVal grupy As String, _
                         ByVal marki As String, ByVal branze As String, ByVal kategorie As String, _
                         ByVal sku As String, ByVal nazwa As String, _
                         ByVal strona As Integer, ByVal ilosc_na_stronie As Integer, _
                         ByVal sortowanie As String, ByVal rosnaco As Boolean, _
                         ByVal czy_niezerowe As Boolean, ByVal czy_nowosci As Boolean) As StanWynik
        Dim wynik As New StanWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("Stan:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_STAN_ZDJECIA", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandTimeout = 600
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@grupy", grupy)
        cmd.Parameters.AddWithValue("@marki", marki)
        cmd.Parameters.AddWithValue("@branze", branze)
        cmd.Parameters.AddWithValue("@kategorie", kategorie)
        cmd.Parameters.AddWithValue("@sku", sku)
        cmd.Parameters.AddWithValue("@nazwa", nazwa)
        cmd.Parameters.AddWithValue("@strona", strona)
        cmd.Parameters.AddWithValue("@ilosc_na_stronie", ilosc_na_stronie)
        cmd.Parameters.AddWithValue("@sortowanie", IIf(sortowanie Is Nothing, DBNull.Value, sortowanie))
        cmd.Parameters.AddWithValue("@rosnaco", rosnaco)
        cmd.Parameters.AddWithValue("@czy_niezerowe", czy_niezerowe)
        cmd.Parameters.AddWithValue("@czy_nowosci", czy_nowosci)
        cmd.Parameters.Add("@ILOSC_TOTAL_REKORDOW", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@ilosc_stron", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@magazyn_id", SqlDbType.Int).Direction = ParameterDirection.InputOutput
        cmd.Parameters("@magazyn_id").Value = IIf(magazyn_id < 0, DBNull.Value, magazyn_id)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@opis_rozszerzony", SqlDbType.Int).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("Stan:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            'wynik.iloscStron = cmd.Parameters("@ilosc_stron").Value
            wynik.dane = ds
            wynik.ilosc_stron = IIf(IsDBNull(cmd.Parameters("@ilosc_stron").Value), -1, cmd.Parameters("@ilosc_stron").Value)
            wynik.ilosc_total_rekordow = IIf(IsDBNull(cmd.Parameters("@ILOSC_TOTAL_REKORDOW").Value), -1, cmd.Parameters("@ILOSC_TOTAL_REKORDOW").Value)
            wynik.opis_rozszerzony = cmd.Parameters("@opis_rozszerzony").Value
            wynik.magazyn_id = IIf(IsDBNull(cmd.Parameters("@magazyn_id").Value), -1, cmd.Parameters("@magazyn_id").Value)
        End If
        cnn.Close()
        Return wynik
    End Function
    <WebMethod()> _
    Public Function UserStrona(ByVal sesja As Byte(), ByVal strona As Integer, ByVal ilosc_na_stronie As Integer, _
        ByVal filtr As String, ByVal sortowanie As String, ByVal rosnaco As Boolean, _
       ByVal grupy As DataSet, ByVal bDrzewoAktywne As Boolean) As UserStronaWynik

        Dim wynik As New UserStronaWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("UserStrona:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_USER_STRONA", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@strona", strona)
        cmd.Parameters.AddWithValue("@ilosc_na_stronie", ilosc_na_stronie)
        cmd.Parameters.AddWithValue("@filtr", filtr)
        cmd.Parameters.AddWithValue("@sortowanie", IIf(sortowanie Is Nothing, DBNull.Value, sortowanie))
        cmd.Parameters.AddWithValue("@rosnaco", rosnaco)
        'tworzymy XML ukryte grupy z datasetu

        'tworzymy XML grupy do pokazania
        If grupy.Tables.Count > 0 Then
            Dim strXmlGrupy As New StringBuilder
            For Each wiersz As DataRow In grupy.Tables(0).Rows
                strXmlGrupy.Append("<row grupa_id=""" & wiersz.Item("grupa_id") & """ />")
            Next
            cmd.Parameters.AddWithValue("@xml_grupy", strXmlGrupy.ToString.Replace("&", "&amp;"))
        Else
            cmd.Parameters.AddWithValue("@xml_grupy", DBNull.Value)
        End If
        cmd.Parameters.AddWithValue("@b_drzewo_aktywne", bDrzewoAktywne)
        cmd.Parameters.Add("@ilosc_stron", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("UserStrona:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.iloscStron = cmd.Parameters("@ilosc_stron").Value
            wynik.dane = ds
        End If
        cnn.Close()
        Return wynik
    End Function
    <WebMethod()> _
    Public Function GrupaLista(ByVal sesja As Byte()) As GrupaListaWynik
        Dim wynik As New GrupaListaWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("GrupaLista:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_GRUPA_LISTA", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("GrupaLista:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If
        cnn.Close()
        Return wynik

    End Function

    <WebMethod()> _
    Public Function UserUsun(ByVal sesja As Byte(), ByVal user_id As Integer) As UserUsunWynik
        Dim wynik As New UserUsunWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("UserUsun:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_USER_USUN", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@id_user_usuwany", user_id)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("UserUsun:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value

        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function UserEdytuj(ByVal sesja As Byte(), ByVal user_id As Integer) As UserEdytujWynik
        Dim wynik As New UserEdytujWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("UserEdytuj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_USER_EDYCJA_ROZPOCZNIJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@user_id_blokowany", user_id)
        cmd.Parameters.Add("@imie", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@nazwisko", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@nazwa", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@telkom", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@email", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@login", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@haslo", SqlDbType.Bit).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@blokada_id", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@przelozony_id", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@przelozony_nazwa", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@magazyn_id", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@magazyn_nazwa", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@adresy_ilosc", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@czy_maile", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@rola", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@typ_id", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@wielkosc_id", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@obszar_sprzedazy_id", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@siec_sprzedazy_id", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@region_sprzedazy_id", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@zespol_sprzedazy_id", SqlDbType.Int).Direction = ParameterDirection.Output

        cmd.Parameters.Add("@czy_limit_zamowien", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@max_ilosc_zamowien", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@typ_okres_zamowien_id", SqlDbType.Int).Direction = ParameterDirection.Output

        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("UserEdytuj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.imie = IIf(IsDBNull(cmd.Parameters("@imie").Value), "", cmd.Parameters("@imie").Value)
            wynik.nazwisko = IIf(IsDBNull(cmd.Parameters("@nazwisko").Value), "", cmd.Parameters("@nazwisko").Value)
            wynik.nazwa = IIf(IsDBNull(cmd.Parameters("@nazwa").Value), "", cmd.Parameters("@nazwa").Value)
            wynik.telkom = IIf(IsDBNull(cmd.Parameters("@telkom").Value), "", cmd.Parameters("@telkom").Value)
            wynik.email = IIf(IsDBNull(cmd.Parameters("@email").Value), "", cmd.Parameters("@email").Value)
            wynik.login = IIf(IsDBNull(cmd.Parameters("@login").Value), "", cmd.Parameters("@login").Value)
            wynik.haslo = IIf(IsDBNull(cmd.Parameters("@haslo").Value), True, cmd.Parameters("@haslo").Value)
            wynik.blokada_id = cmd.Parameters("@blokada_id").Value
            wynik.przelozony_id = IIf(IsDBNull(cmd.Parameters("@przelozony_id").Value), -1, cmd.Parameters("@przelozony_id").Value)
            wynik.przelozony_nazwa = IIf(IsDBNull(cmd.Parameters("@przelozony_nazwa").Value), "<brak>", cmd.Parameters("@przelozony_nazwa").Value)
            wynik.magazyn_id = IIf(IsDBNull(cmd.Parameters("@magazyn_id").Value), -1, cmd.Parameters("@magazyn_id").Value)
            wynik.magazyn_nazwa = IIf(IsDBNull(cmd.Parameters("@magazyn_nazwa").Value), "<brak>", cmd.Parameters("@magazyn_nazwa").Value)
            wynik.adresy_ilosc = IIf(IsDBNull(cmd.Parameters("@adresy_ilosc").Value), 0, cmd.Parameters("@adresy_ilosc").Value)
            wynik.grupy_obszary = ds
            wynik.czy_maile = IIf(IsDBNull(cmd.Parameters("@czy_maile").Value), -1, cmd.Parameters("@czy_maile").Value)
            wynik.rola = IIf(IsDBNull(cmd.Parameters("@rola").Value), "<brak>", cmd.Parameters("@rola").Value)
            wynik.typ_id = IIf(IsDBNull(cmd.Parameters("@typ_id").Value), -1, cmd.Parameters("@typ_id").Value)
            wynik.wielkosc_id = IIf(IsDBNull(cmd.Parameters("@wielkosc_id").Value), -1, cmd.Parameters("@wielkosc_id").Value)
            wynik.obszar_sprzedazy_id = IIf(IsDBNull(cmd.Parameters("@obszar_sprzedazy_id").Value), -1, cmd.Parameters("@obszar_sprzedazy_id").Value)
            wynik.siec_sprzedazy_id = IIf(IsDBNull(cmd.Parameters("@siec_sprzedazy_id").Value), -1, cmd.Parameters("@siec_sprzedazy_id").Value)
            wynik.region_sprzedazy_id = IIf(IsDBNull(cmd.Parameters("@region_sprzedazy_id").Value), -1, cmd.Parameters("@region_sprzedazy_id").Value)
            wynik.zespol_sprzedazy_id = IIf(IsDBNull(cmd.Parameters("@zespol_sprzedazy_id").Value), -1, cmd.Parameters("@zespol_sprzedazy_id").Value)
            wynik.czy_limit_zamowien = IIf(IsDBNull(cmd.Parameters("@czy_limit_zamowien").Value), 0, cmd.Parameters("@czy_limit_zamowien").Value)
            wynik.max_ilosc_zamowien = IIf(IsDBNull(cmd.Parameters("@max_ilosc_zamowien").Value), 0, cmd.Parameters("@max_ilosc_zamowien").Value)
            wynik.typ_okres_zamowien_id = IIf(IsDBNull(cmd.Parameters("@typ_okres_zamowien_id").Value), -1, cmd.Parameters("@typ_okres_zamowien_id").Value)
        End If
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function UserEdytujZapisz(ByVal sesja As Byte(), ByVal imie As String, _
        ByVal nazwisko As String, ByVal nazwa As String, ByVal telkom As String, ByVal email As String, _
        ByVal login As String, ByVal haslo As String, ByVal przelozony_id As Integer, ByVal magazynid As Integer, ByVal czy_maile As Integer, _
        ByVal blokada_id As Integer, _
        ByVal pozostaw_blokade As Boolean, ByVal grupy As DataSet, ByVal rola As String, _
        ByVal typ_id As Integer, ByVal wielkosc_id As Integer, _
        ByVal obszar_sprzedazy_id As Integer, ByVal siec_sprzedazy_id As Integer, _
        ByVal region_sprzedazy_id As Integer, ByVal zespol_sprzedazy_id As Integer, _
        ByVal czy_limit_zamowien As Integer, ByVal max_ilosc_zamowien As Integer, ByVal typ_okres_zamowien_id As Integer) As UserEdytujZapiszWynik
        Dim wynik As New UserEdytujZapiszWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("UserEdytujZapisz:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_USER_EDYCJA_ZAPISZ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@imie", imie)
        cmd.Parameters.AddWithValue("@nazwisko", nazwisko)
        cmd.Parameters.AddWithValue("@nazwa", nazwa)
        cmd.Parameters.AddWithValue("@telkom", telkom)
        cmd.Parameters.AddWithValue("@email", email)
        cmd.Parameters.AddWithValue("@login", login)
        cmd.Parameters.AddWithValue("@haslo", haslo)
        cmd.Parameters.AddWithValue("@magazyn_id", magazynid)
        cmd.Parameters.AddWithValue("@czy_maile", czy_maile)
        cmd.Parameters.AddWithValue("@rola", rola)
        cmd.Parameters.AddWithValue("@typ_id", typ_id)
        cmd.Parameters.AddWithValue("@wielkosc_id", wielkosc_id)
        cmd.Parameters.AddWithValue("@obszar_sprzedazy_id", obszar_sprzedazy_id)
        cmd.Parameters.AddWithValue("@siec_sprzedazy_id", siec_sprzedazy_id)
        cmd.Parameters.AddWithValue("@region_sprzedazy_id", region_sprzedazy_id)
        cmd.Parameters.AddWithValue("@zespol_sprzedazy_id", zespol_sprzedazy_id)

        cmd.Parameters.AddWithValue("@czy_limit_zamowien", czy_limit_zamowien)
        cmd.Parameters.AddWithValue("@max_ilosc_zamowien", max_ilosc_zamowien)
        cmd.Parameters.AddWithValue("@typ_okres_zamowien_id", typ_okres_zamowien_id)

        'cmd.Parameters.AddWithValue("@nr_zewn", nr_zewn)
        cmd.Parameters.AddWithValue("@przelozony_id", IIf(przelozony_id < 0, DBNull.Value, przelozony_id))
        cmd.Parameters.Add("@blokada_id", SqlDbType.Int, 8).Direction = ParameterDirection.InputOutput
        cmd.Parameters("@blokada_id").Value = IIf(blokada_id <= 0, DBNull.Value, blokada_id)
        cmd.Parameters.AddWithValue("@pozostaw_blokade", pozostaw_blokade)
        'tworzymy XML grupy z datasetu
        Dim strXmlGrupy As New StringBuilder
        strXmlGrupy.Append("<ROOT>")
        For Each wiersz As DataRow In grupy.Tables(0).Rows
            strXmlGrupy.Append("<GRUPA GRUPA_ID=""" & wiersz.Item("grupa_id") & """ />")
        Next
        strXmlGrupy.Append("</ROOT>")
        cmd.Parameters.AddWithValue("@xml_grupy", strXmlGrupy.ToString.Replace("&", "&amp;"))
        Dim strXmlFunkcji As New StringBuilder
        strXmlFunkcji.Append("<ROOT>")
        For Each wiersz As DataRow In grupy.Tables(1).Rows
            strXmlFunkcji.Append("<FUNKCJA FUNKCJA_ID=""" & wiersz.Item("funkcja_id") & """ WLACZ=""" & wiersz.Item("WLACZ") & """ />")
        Next
        strXmlFunkcji.Append("</ROOT>")
        cmd.Parameters.AddWithValue("@xml_funkcji", strXmlFunkcji.ToString.Replace("&", "&amp;"))
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("UserEdytujZapisz:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = IIf(IsDBNull(cmd.Parameters("@status_opis").Value), "", cmd.Parameters("@status_opis").Value)
        If wynik.status >= 0 Then
            wynik.blokada_id = IIf(IsDBNull(cmd.Parameters("@blokada_id").Value), -1, cmd.Parameters("@blokada_id").Value)
        End If
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function ZmienHasloUzytkownika(ByVal sesja As Byte(), ByVal user_id_nowe_haslo As Integer, ByVal nowe_haslo As String) As ZmienHasloUzytkownikaWynik
        Dim wynik As New ZmienHasloUzytkownikaWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("ZmienHasloUzytkownika:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_ZMIEN_HASLO_UZYTKOWNIKA", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@user_id_nowe_haslo", user_id_nowe_haslo)
        cmd.Parameters.AddWithValue("@nowe_haslo", nowe_haslo)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("ZmienHasloUzytkownika:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function AdresEdytujZapisz(ByVal sesja As Byte(), ByVal user_id As Integer, ByVal nazwa As String, _
        ByVal adres As String, ByVal kod As String, ByVal miasto As String, ByVal blokada_id As Integer, _
        ByVal pozostaw_blokade As Boolean, ByVal domyslny As Integer) As AdresEdytujZapiszWynik
        Dim wynik As New AdresEdytujZapiszWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("AdresEdytujZapisz:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_ADRES_EDYCJA_ZAPISZ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@user_id_adresy", user_id)
        cmd.Parameters.AddWithValue("@nazwa", nazwa)
        cmd.Parameters.AddWithValue("@adres", adres)
        cmd.Parameters.AddWithValue("@kod", kod)
        cmd.Parameters.AddWithValue("@miasto", miasto)
        cmd.Parameters.AddWithValue("@domyslny", domyslny)
        cmd.Parameters.Add("@blokada_id", SqlDbType.Int, 8).Direction = ParameterDirection.InputOutput
        cmd.Parameters("@blokada_id").Value = IIf(blokada_id <= 0, DBNull.Value, blokada_id)
        cmd.Parameters.AddWithValue("@pozostaw_blokade", pozostaw_blokade)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            logger.Error("AdresEdytujZapisz:Błąd komunikacji z bazą: ", ex)
            cnn.Close()
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status >= 0 Then
            wynik.blokada_id = IIf(IsDBNull(cmd.Parameters("@blokada_id").Value), -1, cmd.Parameters("@blokada_id").Value)
        End If
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function AdresEdytuj(ByVal sesja As Byte(), ByVal user_id As Integer) As AdresEdytujWynik
        Dim wynik As New AdresEdytujWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("AdresEdytuj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_ADRES_EDYCJA_ROZPOCZNIJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@adres_id_blokowany", user_id)
        cmd.Parameters.Add("@nazwa", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@adres", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@kod", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@miasto", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@domyslny", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@blokada_id", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("AdresEdytuj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.nazwa = IIf(IsDBNull(cmd.Parameters("@nazwa").Value), "", cmd.Parameters("@nazwa").Value)
            wynik.adres = IIf(IsDBNull(cmd.Parameters("@adres").Value), "", cmd.Parameters("@adres").Value)
            wynik.kod = IIf(IsDBNull(cmd.Parameters("@kod").Value), "", cmd.Parameters("@kod").Value)
            wynik.miasto = IIf(IsDBNull(cmd.Parameters("@miasto").Value), "", cmd.Parameters("@miasto").Value)
            wynik.domyslny = IIf(IsDBNull(cmd.Parameters("@domyslny").Value), 0, cmd.Parameters("@domyslny").Value)
            wynik.blokada_id = cmd.Parameters("@blokada_id").Value
        End If
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function AdresStrona(ByVal sesja As Byte(), ByVal user_id As Integer, ByVal adresy_do_wysylek_grupowych As Boolean, _
        ByVal strona As Integer, ByVal ilosc_na_stronie As Integer, ByVal filtr As String, ByVal sortowanie As String, _
        ByVal rosnaco As Boolean, ByVal ukryte_kolumny As DataSet) As AdresStronaWynik

        Dim wynik As New AdresStronaWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("AdresStrona:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_ADRES_STRONA", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@user_id_adresy", user_id)
        cmd.Parameters.AddWithValue("@strona", strona)
        cmd.Parameters.AddWithValue("@ilosc_na_stronie", ilosc_na_stronie)
        cmd.Parameters.AddWithValue("@filtr", filtr)
        cmd.Parameters.AddWithValue("@sortowanie", IIf(sortowanie Is Nothing, DBNull.Value, sortowanie))
        cmd.Parameters.AddWithValue("@rosnaco", rosnaco)
        'tworzymy XML ukryte grupy z datasetu
        Dim strXml As New StringBuilder
        strXml.Append("<ROOT>")
        For Each wiersz As DataRow In ukryte_kolumny.Tables(0).Rows
            strXml.Append("<KOLUMNA KOLUMNA_NAZWA=""" & wiersz.Item("nazwa") & """ />")
        Next
        strXml.Append("</ROOT>")
        cmd.Parameters.AddWithValue("@xml_ukryj_kolumny", strXml.ToString.Replace("&", "&amp;"))
        cmd.Parameters.AddWithValue("@adresy_do_wysylek_grupowych", adresy_do_wysylek_grupowych)

        cmd.Parameters.Add("@ILOSC_TOTAL_REKORDOW", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@ilosc_stron", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@user_adresy", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("AdresStrona:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.iloscStron = cmd.Parameters("@ilosc_stron").Value
            wynik.ilosc_total_rekordow = IIf(IsDBNull(cmd.Parameters("@ILOSC_TOTAL_REKORDOW").Value), -1, cmd.Parameters("@ILOSC_TOTAL_REKORDOW").Value)
            wynik.userAdresy = cmd.Parameters("@user_adresy").Value
            wynik.dane = ds
        End If
        cnn.Close()
        Return wynik
    End Function

    <WebMethod(Description:="Usuwa adres zdefiniowany użytkownika.")> _
    Public Function AdresUsun(ByVal sesja As Byte(), ByVal adres_id As Integer) As AdresUsunWynik
        Dim wynik As New AdresUsunWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("AdresUsun:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_ADRES_USUN", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@adres_id", adres_id)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("AdresUsun:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function MagazynyOdczytaj(ByVal sesja As Byte()) As MagazynyOdczytajWynik
        Dim wynik As New MagazynyOdczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("MagazynyOdczytaj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_MAGAZYNY_ODCZYTAJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("MagazynyOdczytaj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function FunkcjaLista(ByVal sesja As Byte()) As FunkcjaListaWynik
        Dim wynik As New FunkcjaListaWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("FunkcjaLista:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_FUNKCJA_LISTA", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("FunkcjaLista:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If
        cnn.Close()
        Return wynik

    End Function

    <WebMethod()> _
    Public Sub AdresEdytujAnuluj(ByVal sesja As Byte(), ByVal blokada_id As Integer)
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            Return
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_ADRES_EDYCJA_ODBLOKUJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@blokada_id", blokada_id)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            cnn.Close()
            Return
        End Try
        cnn.Close()
        Return
    End Sub

    <WebMethod()> _
    Public Sub UserEdytujAnuluj(ByVal sesja As Byte(), ByVal blokada_id As Integer)
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            logger.Error("UserEdytujAnuluj:Błąd komunikacji z bazą: ", ex)
            Return
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_USER_EDYCJA_ODBLOKUJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@blokada_id", blokada_id)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            cnn.Close()
            logger.Error("UserEdytujAnuluj: ", ex)
            Return
        End Try
        cnn.Close()
        Return
    End Sub

    <WebMethod()> _
    Public Function ZamowienieStrona(ByVal sesja As Byte(), ByVal strona As Integer, ByVal ilosc_na_stronie As Integer, _
        ByVal data_od As Int64, ByVal data_do As Int64, ByVal filtr As String, ByVal sortowanie As String, _
        ByVal rosnaco As Boolean, ByVal ukryte_kolumny As DataSet, ByVal statusID As Integer) As ZamowienieStronaWynik

        Dim wynik As New ZamowienieStronaWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("ZamowienieStrona:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_ZAMOWIENIE_STRONA", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandTimeout = 240
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@strona", strona)
        cmd.Parameters.AddWithValue("@ilosc_na_stronie", ilosc_na_stronie)
        cmd.Parameters.AddWithValue("@data_od", IIf(data_od < 0, DBNull.Value, DateTime.FromBinary(data_od)))
        cmd.Parameters.AddWithValue("@data_do", IIf(data_do < 0, DBNull.Value, DateTime.FromBinary(data_do)))
        cmd.Parameters.AddWithValue("@filtr", filtr)
        cmd.Parameters.AddWithValue("@sortowanie", IIf(sortowanie Is Nothing, DBNull.Value, sortowanie))
        cmd.Parameters.AddWithValue("@rosnaco", rosnaco)

        cmd.Parameters.AddWithValue("@IN_ZAMOWIENIE_STATUS_ID", statusID)

        'tworzymy XML ukryte grupy z datasetu
        Dim strXml As New StringBuilder
        For Each wiersz As DataRow In ukryte_kolumny.Tables(0).Rows
            strXml.Append("<row kolumna=""" & wiersz.Item("nazwa") & """ />")
        Next
        cmd.Parameters.AddWithValue("@xml_ukryj_kolumny", strXml.ToString.Replace("&", "&amp;"))
        cmd.Parameters.Add("@ilosc_stron", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@ILOSC_TOTAL_REKORDOW", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("ZamowienieStrona:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status >= 0 Then
            wynik.ilosc_stron = IIf(IsDBNull(cmd.Parameters("@ilosc_stron").Value), -1, cmd.Parameters("@ilosc_stron").Value)
            wynik.ilosc_total_rekordow = IIf(IsDBNull(cmd.Parameters("@ILOSC_TOTAL_REKORDOW").Value), -1, cmd.Parameters("@ILOSC_TOTAL_REKORDOW").Value)
            wynik.dane = ds
        End If
        cnn.Close()
        Return wynik
    End Function

    <WebMethod(Description:="Metoda anulowuje wybrane zamówienie.")> _
    Public Function AnulujZamowienie(ByVal sesja As Byte(), ByVal zamowienie_id As Integer) As AnulujZamowienieWynik
        'ByVal sesja As Byte()
        Dim wynik As New AnulujZamowienieWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("AnulujZamowienie:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_ZAMOWIENIE_ANULUJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@zamowienie_id", zamowienie_id)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("AnulujZamowienie:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function AdresyOdczytaj(ByVal sesja As Byte(), ByVal zamowienie_id As Integer) As AdresyOdczytajWynik
        Dim wynik As New AdresyOdczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("AdresyOdczytaj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_ADRES_ODCZYTAJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@zamowienie_id", zamowienie_id)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("AdresyOdczytaj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function StanSku(ByVal sesja As Byte(), ByVal magazyn_id As Integer, ByVal dane As DataSet) As StanSkuWynik
        Dim wynik As New StanSkuWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("StanSku:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_STAN_SKU", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandTimeout = 600
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@magazyn_id", IIf(magazyn_id < 0, DBNull.Value, magazyn_id))
        'tworzymy XML sku
        Dim strXml As New StringBuilder
        For Each wiersz As DataRow In dane.Tables(0).Rows
            strXml.Append("<row sku_id=""" & wiersz.Item("sku_id") & """/>")
        Next
        cmd.Parameters.AddWithValue("@xml_sku", strXml.ToString.Replace("&", "&amp;"))
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("StanSku:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function ZamowienieWczytaj(ByVal sesja As Byte(), ByVal zamowienie_id As Integer, _
        ByVal magazyn_id As Integer, ByVal blokada_id As Integer) As ZamowienieWczytajWynik
        Dim wynik As New ZamowienieWczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("ZamowienieWczytaj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_ZAMOWIENIE_WCZYTAJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.Add("@zamowienie_id", SqlDbType.Int).Direction = ParameterDirection.InputOutput
        cmd.Parameters("@zamowienie_id").Value = IIf(zamowienie_id < 0, DBNull.Value, zamowienie_id)
        cmd.Parameters.Add("@magazyn_id", SqlDbType.Int).Direction = ParameterDirection.InputOutput
        cmd.Parameters("@magazyn_id").Value = IIf(magazyn_id < 0, DBNull.Value, magazyn_id)
        cmd.Parameters.Add("@blokada_id", SqlDbType.Int).Direction = ParameterDirection.InputOutput
        cmd.Parameters("@blokada_id").Value = IIf(blokada_id < 0, DBNull.Value, blokada_id)
        cmd.Parameters.Add("@wlasciciel_nazwa", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@tryb_pracy", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@zamowienie_status", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@zamowienie_status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@typ_zamowienia", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@centrala", SqlDbType.Bit).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@magazyn_docelowy_id", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@oddzial_docelowy_id", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@adres_id", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@nazwa", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@adres", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@kod", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@miasto", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@ilosc_adresow", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@osoba_kontaktowa", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@telefon_kontaktowy", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@uwagi", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@data_realizacji", SqlDbType.DateTime).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@users_ids", SqlDbType.NVarChar, -1).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@grupy", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@typy", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@wielkosci", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim pIn As SqlParameter = cmd.Parameters.Add("@limit", SqlDbType.Decimal)
        pIn.Direction = ParameterDirection.Output
        pIn.Precision = 18
        pIn.Scale = 2
        pIn = cmd.Parameters.Add("@koszt_dostawy", SqlDbType.Decimal)
        pIn.Direction = ParameterDirection.Output
        pIn.Precision = 18
        pIn.Scale = 2

        cmd.Parameters.Add("@maDaneDpd", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@dok_zw", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@prz_zw", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@osoba_pryw", SqlDbType.Int).Direction = ParameterDirection.Output
        pIn = cmd.Parameters.Add("@wartosc", SqlDbType.Decimal)
        pIn.Direction = ParameterDirection.Output
        pIn.Precision = 18
        pIn.Scale = 2
        pIn = cmd.Parameters.Add("@kwota_cod", SqlDbType.Decimal)
        pIn.Direction = ParameterDirection.Output
        pIn.Precision = 18
        pIn.Scale = 2
        cmd.Parameters.Add("@dost_gw_typ", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output

        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("ZamowienieWczytaj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.zamowienie_id = IIf(IsDBNull(cmd.Parameters("@zamowienie_id").Value), -1, cmd.Parameters("@zamowienie_id").Value)
            wynik.magazyn_id = IIf(IsDBNull(cmd.Parameters("@magazyn_id").Value), -1, cmd.Parameters("@magazyn_id").Value)
            wynik.blokada_id = IIf(IsDBNull(cmd.Parameters("@blokada_id").Value), -1, cmd.Parameters("@blokada_id").Value)
            wynik.wlasciciel_nazwa = IIf(IsDBNull(cmd.Parameters("@wlasciciel_nazwa").Value), "", cmd.Parameters("@wlasciciel_nazwa").Value)
            wynik.tryb_pracy = cmd.Parameters("@tryb_pracy").Value
            wynik.zamowienie_status = IIf(IsDBNull(cmd.Parameters("@zamowienie_status").Value), "", cmd.Parameters("@zamowienie_status").Value)
            wynik.zamowienie_status_opis = IIf(IsDBNull(cmd.Parameters("@zamowienie_status_opis").Value), "", cmd.Parameters("@zamowienie_status_opis").Value)
            wynik.typ_zamowienia = cmd.Parameters("@typ_zamowienia").Value
            wynik.centrala = IIf(IsDBNull(cmd.Parameters("@centrala").Value), 0, cmd.Parameters("@centrala").Value)
            wynik.magazyn_docelowy_id = IIf(IsDBNull(cmd.Parameters("@magazyn_docelowy_id").Value), -1, cmd.Parameters("@magazyn_docelowy_id").Value)
            wynik.oddzial_docelowy_id = IIf(IsDBNull(cmd.Parameters("@oddzial_docelowy_id").Value), -1, cmd.Parameters("@oddzial_docelowy_id").Value)
            wynik.adres_id = IIf(IsDBNull(cmd.Parameters("@adres_id").Value), -1, cmd.Parameters("@adres_id").Value)
            wynik.nazwa = IIf(IsDBNull(cmd.Parameters("@nazwa").Value), "", cmd.Parameters("@nazwa").Value)
            wynik.adres = IIf(IsDBNull(cmd.Parameters("@adres").Value), "", cmd.Parameters("@adres").Value)
            wynik.kod = IIf(IsDBNull(cmd.Parameters("@kod").Value), "", cmd.Parameters("@kod").Value)
            wynik.miasto = IIf(IsDBNull(cmd.Parameters("@miasto").Value), "", cmd.Parameters("@miasto").Value)
            wynik.ilosc_adresow = IIf(IsDBNull(cmd.Parameters("@ilosc_adresow").Value), 0, cmd.Parameters("@ilosc_adresow").Value)
            wynik.osoba_kontaktowa = IIf(IsDBNull(cmd.Parameters("@osoba_kontaktowa").Value), "", cmd.Parameters("@osoba_kontaktowa").Value)
            wynik.telefon_kontaktowy = IIf(IsDBNull(cmd.Parameters("@telefon_kontaktowy").Value), "", cmd.Parameters("@telefon_kontaktowy").Value)
            wynik.uwagi = IIf(IsDBNull(cmd.Parameters("@uwagi").Value), "", cmd.Parameters("@uwagi").Value)
            wynik.data_realizacji = IIf(IsDBNull(cmd.Parameters("@data_realizacji").Value), New DateTime(1, 1, 1), cmd.Parameters("@data_realizacji").Value)
            wynik.limit = cmd.Parameters("@limit").Value
            wynik.koszt_dostawy = cmd.Parameters("@koszt_dostawy").Value

            wynik.maDaneDpd = IIf(IsDBNull(cmd.Parameters("@maDaneDpd").Value), 0, cmd.Parameters("@maDaneDpd").Value)

            wynik.DokZw = IIf(IsDBNull(cmd.Parameters("@dok_zw").Value), 0, cmd.Parameters("@dok_zw").Value)
            wynik.PrzZw = IIf(IsDBNull(cmd.Parameters("@prz_zw").Value), 0, cmd.Parameters("@prz_zw").Value)
            wynik.OsPryw = IIf(IsDBNull(cmd.Parameters("@osoba_pryw").Value), 0, cmd.Parameters("@osoba_pryw").Value)
            wynik.DPDWartosc = IIf(IsDBNull(cmd.Parameters("@wartosc").Value), 0, cmd.Parameters("@wartosc").Value)
            wynik.DPDKwotaCOD = IIf(IsDBNull(cmd.Parameters("@kwota_cod").Value), -1, cmd.Parameters("@kwota_cod").Value)
            wynik.DPDTyp = IIf(IsDBNull(cmd.Parameters("@dost_gw_typ").Value), "", cmd.Parameters("@dost_gw_typ").Value)
            wynik.users_ids = IIf(IsDBNull(cmd.Parameters("@users_ids").Value), "", cmd.Parameters("@users_ids").Value)
            wynik.grupy = IIf(IsDBNull(cmd.Parameters("@grupy").Value), "", cmd.Parameters("@grupy").Value)
            wynik.typy = IIf(IsDBNull(cmd.Parameters("@typy").Value), "", cmd.Parameters("@typy").Value)
            wynik.wielkosci = IIf(IsDBNull(cmd.Parameters("@wielkosci").Value), "", cmd.Parameters("@wielkosci").Value)
            wynik.dane = ds
        End If
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function KoszykZapisz(ByVal sesja As Byte(), ByVal blokada_id As Integer, _
        ByVal magazyn_id As Integer, ByVal magazyn_docelowy_id As Integer, ByVal adres_id As Integer, _
        ByVal nazwa As String, ByVal adres As String, ByVal kod As String, ByVal miasto As String, _
        ByVal osoba_kontaktowa As String, ByVal telefon_kontaktowy As String, ByVal uwagi As String, _
        ByVal typ_zamowienia As Integer, ByVal dane As DataSet, ByVal data_realizacji As DateTime, _
        ByVal oddzial_docelowy_id As Integer, _
        ByVal zapisz_dane_dpd As Integer, ByVal dok_zw As Integer, _
        ByVal os_pryw As Integer, ByVal prz_zw As Integer, ByVal cod As Decimal, _
        ByVal dpd_wartosc As Decimal, ByVal dpd_typ As String, _
        ByVal odbiorcy As String, ByVal grupy As String, ByVal typy As String, _
        ByVal wielkosc As String, ByVal warunek As String) As KoszykZapiszWynik
        Dim wynik As New KoszykZapiszWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("KoszykZapisz:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_KOSZYK_GRUPA_ZAPISZ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@blokada_id", IIf(blokada_id < 0, DBNull.Value, blokada_id))
        'tworzymy XML pozycje
        Dim strXmlPozycje As New StringBuilder
        For Each wiersz As DataRow In dane.Tables(0).Rows
            strXmlPozycje.Append("<row sku_id=""" & wiersz.Item("sku_id") & """ grupa=""" & IIf(dane.Tables(0).Columns.Contains("Grupa"), wiersz.Item("grupa"), "BRAK") & """ ilosc=""" & wiersz.Item("ilosc") & """ />")
        Next
        cmd.Parameters.AddWithValue("@xml_sku", strXmlPozycje.ToString.Replace("&", "&amp;"))
        cmd.Parameters.AddWithValue("@magazyn_id", IIf(magazyn_id < 0, DBNull.Value, magazyn_id))
        cmd.Parameters.AddWithValue("@osoba_kontaktowa", osoba_kontaktowa)
        cmd.Parameters.AddWithValue("@telefon_kontaktowy", telefon_kontaktowy)
        cmd.Parameters.AddWithValue("@uwagi", uwagi)
        cmd.Parameters.AddWithValue("@zamowienie_typ_id", typ_zamowienia)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        cmd.Parameters.Add("@magazyn_docelowy_id", SqlDbType.Int).Value = DBNull.Value
        cmd.Parameters.Add("@oddzial_docelowy_id", SqlDbType.Int).Value = DBNull.Value
        cmd.Parameters.Add("@adres_id", SqlDbType.Int).Value = DBNull.Value
        cmd.Parameters.Add("@nazwa", SqlDbType.NVarChar, 4000).Value = DBNull.Value
        cmd.Parameters.Add("@adres", SqlDbType.NVarChar, 4000).Value = DBNull.Value
        cmd.Parameters.Add("@kod", SqlDbType.NVarChar, 4000).Value = DBNull.Value
        cmd.Parameters.Add("@miasto", SqlDbType.NVarChar, 4000).Value = DBNull.Value
        cmd.Parameters.Add("@xml_adresy", SqlDbType.Xml).Value = DBNull.Value
        cmd.Parameters.AddWithValue("@data_realizacji", IIf(data_realizacji = New DateTime(1, 1, 1), DBNull.Value, data_realizacji))
        cmd.Parameters.AddWithValue("@zapisz_dane_dpd", zapisz_dane_dpd)
        cmd.Parameters.AddWithValue("@dok_zw", dok_zw)
        cmd.Parameters.AddWithValue("@os_pryw", os_pryw)
        cmd.Parameters.AddWithValue("@prz_zw", prz_zw)
        cmd.Parameters.AddWithValue("@cod", cod)
        cmd.Parameters.AddWithValue("@dpd_wartosc", dpd_wartosc)
        cmd.Parameters.AddWithValue("@dpd_typ", dpd_typ)
        cmd.Parameters.Add("@xml_users_ids", SqlDbType.Xml).Value = DBNull.Value
        cmd.Parameters("@xml_users_ids").Value = odbiorcy
        cmd.Parameters.Add("@xml_grupy", SqlDbType.Xml).Value = DBNull.Value
        cmd.Parameters("@xml_grupy").Value = grupy
        cmd.Parameters.Add("@xml_typy", SqlDbType.Xml).Value = DBNull.Value
        cmd.Parameters("@xml_typy").Value = typy
        cmd.Parameters.Add("@xml_wielkosc", SqlDbType.Xml).Value = DBNull.Value
        cmd.Parameters("@xml_wielkosc").Value = wielkosc
        cmd.Parameters.AddWithValue("@warunek", warunek)
        Select Case typ_zamowienia
            Case 1 'transfer
                cmd.Parameters("@magazyn_docelowy_id").Value = IIf(magazyn_docelowy_id < 0, DBNull.Value, magazyn_docelowy_id)
            Case 2 'odbiór własny
                cmd.Parameters("@magazyn_docelowy_id").Value = IIf(magazyn_docelowy_id < 0, DBNull.Value, magazyn_docelowy_id)
            Case 3 'dostawa na adres zdefiniowany
                cmd.Parameters("@adres_id").Value = IIf(adres_id < 0, DBNull.Value, adres_id)
            Case 4 'dostawa na adres inny
                cmd.Parameters("@nazwa").Value = nazwa
                cmd.Parameters("@adres").Value = adres
                cmd.Parameters("@kod").Value = kod
                cmd.Parameters("@miasto").Value = miasto
            Case 5 'wydanie specjalne na utylizację
                'nie ma parametrów
                'Case 6 'wydanie specjalne na remont standów
                '    'nie ma parametrów
                'Case 7 'wydanie specjalne na klasyczny co-packing
                '    'nie ma parametrów
            Case 7 'zamówienie grupowe
                ''tworzymy XML adresy
                'Dim strXmlAdresy As New StringBuilder
                'For Each wiersz As DataRow In dane.Tables(1).Rows
                '    strXmlAdresy.Append("<row adres_id=""" & wiersz.Item("adres_id") & """ />")
                'Next
                'cmd.Parameters("@xml_adresy").Value = strXmlAdresy.ToString.Replace("&", "&amp;")
            Case 6 'odbiór własny dpd
                cmd.Parameters("@oddzial_docelowy_id").Value = IIf(oddzial_docelowy_id < 0, DBNull.Value, oddzial_docelowy_id)
            Case Else
                wynik.status = -1
                wynik.status_opis = "Błąd wewnętrzny systemu. Aplikacja podała nieznany typ zamówienia:" & typ_zamowienia
                Return wynik
        End Select

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("KoszykZapisz:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function MinimalnaDataRealizacji(ByVal sesja As Byte()) As MinimalnaDataRealizacjiWynik
        Dim wynik As New MinimalnaDataRealizacjiWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("UserEdytujZapisz:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_MINIMALNA_DATA_REALIZACJI", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.Add("@MINIMALNA_DATA_REALIZACJI", SqlDbType.Date).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("UserEdytujZapisz:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = IIf(IsDBNull(cmd.Parameters("@status_opis").Value), "", cmd.Parameters("@status_opis").Value)
        If wynik.status >= 0 Then
            wynik.data = IIf(IsDBNull(cmd.Parameters("@MINIMALNA_DATA_REALIZACJI").Value), Date.Now, cmd.Parameters("@MINIMALNA_DATA_REALIZACJI").Value)
        End If
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function ZdjecieOdczytaj(ByVal sesja As Byte(), ByVal id As Integer, ByVal table_nazwa As String, ByVal column_nazwa As String, ByVal id_nazwa As String) As ZdjecieOdczytajWynik
        Dim wynik As New ZdjecieOdczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("AdresyOdczytaj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_POKAZ_ZDJECIE", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@id", id)
        cmd.Parameters.AddWithValue("@TABLE_NAZWA", table_nazwa)
        cmd.Parameters.AddWithValue("@COLUMN_NAZWA", column_nazwa)
        cmd.Parameters.AddWithValue("@ID_NAZWA", id_nazwa)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("ZdjęcieOdczytaj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Sub ZamowienieEdytujAnuluj(ByVal sesja As Byte(), ByVal blokada_id As Integer)
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            Return
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_ZAMOWIENIE_ODBLOKUJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@blokada_id", blokada_id)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            cnn.Close()
            Return
        End Try
        cnn.Close()
        Return
    End Sub

    <WebMethod()> _
    Public Function StanSkuGrupa(ByVal sesja As Byte(), ByVal magazyn_id As Integer, ByVal dane As DataSet) As StanSkuGrupaWynik
        Dim wynik As New StanSkuGrupaWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("StanSku:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_STAN_SKU_GRUPA", cnn)
        cmd.CommandTimeout = 600
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@magazyn_id", IIf(magazyn_id < 0, DBNull.Value, magazyn_id))
        'tworzymy XML sku
        Dim strXml As New StringBuilder
        For Each wiersz As DataRow In dane.Tables(0).Rows
            Dim ilosc As Integer = 0
            If dane.Tables(0).Columns.Contains("ilosc") = True Then
                ilosc = wiersz.Item("ilosc")
            End If

            strXml.Append("<row sku_id=""" & wiersz.Item("sku_id") & """ grupa_id=""" & wiersz.Item("grupa_id") & """ ilosc=""" & ilosc & """/>")
        Next
        cmd.Parameters.AddWithValue("@xml_sku", strXml.ToString.Replace("&", "&amp;"))
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("StanSku:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function PodzialGrupaOdczytaj(ByVal sesja As Byte(), ByVal magazyn_id As Integer, ByVal dane As DataSet) As PodzialGrupaOdczytajWynik
        Dim wynik As New PodzialGrupaOdczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("PodzialOdczytaj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_PODZIAL_GRUPA_ODCZYTAJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@magazyn_id", IIf(magazyn_id < 0, DBNull.Value, magazyn_id))
        Dim strXmlSku As New StringBuilder
        For Each wiersz As DataRow In dane.Tables(0).Rows
            strXmlSku.Append("<row sku_id=""" & wiersz.Item("sku_id") & vbCrLf & """ />")

        Next
        cmd.Parameters.AddWithValue("@xml_sku_id", strXmlSku.ToString.Replace("&", "&amp;"))
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("PodzialOdczytaj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function PodzialGrupaZapisz(ByVal sesja As Byte(), ByVal magazyn_id As Integer, _
        ByVal dane As DataSet) As PodzialGrupaZapiszWynik
        Dim wynik As New PodzialGrupaZapiszWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("PodzialZapisz:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_PODZIAL_GRUPA_ZAPISZ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@magazyn_id", IIf(magazyn_id < 0, DBNull.Value, magazyn_id))
        'tworzymy XML zmiany
        Dim strXml As New StringBuilder
        For Each wiersz As DataRow In dane.Tables(0).Rows
            'TODO: escape parametru sku
            strXml.Append("<row grupa_id=""" & wiersz.Item("grupa_id") & """ sku=""" & wiersz.Item("sku").ToString.Substring(0, wiersz.Item("sku").ToString.IndexOf(Chr(10))) & """ przydzial=""" & wiersz.Item("przydzial") & """ />")
        Next
        cmd.Parameters.AddWithValue("@xml_zmiany", strXml.ToString.Replace("&", "&amp;"))
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("PodzialZapisz:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function UserLimityWczytaj(ByVal sesja As Byte(), ByVal strona As Integer, ByVal ilosc_na_stronie As Integer, _
        ByVal filtr As String, ByVal sortowanie As String, ByVal rosnaco As Boolean, _
        ByVal grupy As DataSet) As UserLimityWczytajWynik

        Dim wynik As New UserLimityWczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("UserStrona:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_USER_LIMITY_WCZYTAJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@strona", strona)
        cmd.Parameters.AddWithValue("@ilosc_na_stronie", ilosc_na_stronie)
        cmd.Parameters.AddWithValue("@filtr", filtr)
        cmd.Parameters.AddWithValue("@sortowanie", IIf(sortowanie Is Nothing, DBNull.Value, sortowanie))
        cmd.Parameters.AddWithValue("@rosnaco", rosnaco)

        'tworzymy XML grupy do pokazania
        If grupy.Tables.Count > 0 Then
            Dim strXmlGrupy As New StringBuilder
            For Each wiersz As DataRow In grupy.Tables(0).Rows
                strXmlGrupy.Append("<row grupa_id=""" & wiersz.Item("grupa_id") & """ />")
            Next
            cmd.Parameters.AddWithValue("@xml_grupy", strXmlGrupy.ToString.Replace("&", "&amp;"))
        Else
            cmd.Parameters.AddWithValue("@xml_grupy", DBNull.Value)
        End If

        cmd.Parameters.Add("@ilosc_stron", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("UserStrona:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.iloscStron = cmd.Parameters("@ilosc_stron").Value
            wynik.dane = ds
        End If
        cnn.Close()
        Return wynik
    End Function


    <WebMethod()> _
    Public Function UserLimityZapisz(ByVal sesja As Byte(), ByVal dane As DataSet) As UserLimityZapiszWynik
        Dim wynik As New UserLimityZapiszWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("PodzialZapisz:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_USER_LIMITY_ZAPISZ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        'tworzymy XML zmiany
        Dim strXml As New StringBuilder
        For Each wiersz As DataRow In dane.Tables(0).Rows
            'TODO: escape parametru sku
            strXml.Append("<row user_id=""" & wiersz.Item("user_id") & """ przydzial=""" & wiersz.Item("przydzial").ToString.Replace(",", ".") & """ />")
        Next
        cmd.Parameters.AddWithValue("@xml_zmiany", strXml.ToString.Replace("&", "&amp;"))
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("PodzialZapisz:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function OddzialyOdczytaj(ByVal sesja As Byte()) As OddzialyOdczytajWynik
        Dim wynik As New OddzialyOdczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("OddzialyOdczytaj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_ODDZIALY_ODCZYTAJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("OddzialyOdczytaj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function KoszykZatwierdz(ByVal sesja As Byte(), ByVal blokada_id As Integer) As KoszykZatwierdzWynik
        Dim wynik As New KoszykZatwierdzWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("KoszykZatwierdz:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_KOSZYK_GRUPA_ZATWIERDZ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@blokada_id", IIf(blokada_id < 0, DBNull.Value, blokada_id))
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("KoszykZatwierdz:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function UserLimityZapiszWybranym(ByVal sesja As Byte(), ByVal dane As String, ByVal limit As Decimal, ByVal komentarz As String) As UserLimityZapiszWybranymWynik
        Dim wynik As New UserLimityZapiszWybranymWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("PodzialZapisz:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_USER_LIMITY_ZAPISZ_WYBRANYM", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@KOMENTARZ", komentarz)
        cmd.Parameters.AddWithValue("@LIMIT", limit)
        cmd.Parameters.AddWithValue("@xml_zmiany", dane)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("PodzialZapisz:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function UserLimityWczytajWybranych(ByVal sesja As Byte(), ByVal filtr As String) As UserLimityWczytajWybranychWynik

        Dim wynik As New UserLimityWczytajWybranychWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("UserStrona:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_USER_LIMITY_WCZYTAJ_WYBRANYCH", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@filtr", filtr)

        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("UserStrona:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function SkuStanFiltry(ByVal sesja As Byte()) As SkuStanFiltryWynik
        Dim wynik As New SkuStanFiltryWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("PodzialZapisz:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_STAN_FILTRY", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet
        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("UserStrona:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If
        cnn.Close()
        Return wynik

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function ZmienHaslo(ByVal sesja As Byte(), ByVal obecne_haslo As String, ByVal nowe_haslo As String) As ZmienHasloWynik
        Dim wynik As New ZmienHasloWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("ZmienHaslo:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_ZMIEN_HASLO", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@obecne_haslo", obecne_haslo)
        cmd.Parameters.AddWithValue("@nowe_haslo", nowe_haslo)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("ZmienHaslo:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function SKUEdytuj(ByVal sesja As Byte(), ByVal sku_id As Integer) As SKUEdytujWynik
        Dim wynik As New SKUEdytujWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("UserEdytuj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_SKU_EDYCJA_ROZPOCZNIJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@sku_id", sku_id)
        cmd.Parameters.Add("@sku", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@zdjecie", SqlDbType.VarBinary, -1).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@sku_nazwa", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim pIn As SqlParameter = cmd.Parameters.Add("@cena", SqlDbType.Decimal)
        pIn.Direction = ParameterDirection.Output
        pIn.Precision = 18
        pIn.Scale = 2
        cmd.Parameters.Add("@wysokosc", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@szerokosc", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@glebokosc", SqlDbType.Int).Direction = ParameterDirection.Output
        pIn = cmd.Parameters.Add("@waga", SqlDbType.Decimal)
        pIn.Direction = ParameterDirection.Output
        pIn.Precision = 18
        pIn.Scale = 2
        cmd.Parameters.Add("@marka", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@branza", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@jm", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@max_ilosc", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@czy_mozna_zamawiac", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@czy_limit_wydan", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@blokada_id", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@czy_nowosc", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@kategoria", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@opis_rozszerzony", SqlDbType.NVarChar, 1000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@MAX_ILOSC_ZAMOWIEN", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@Typ_okres_zamowien_id", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@SZT_OPK", SqlDbType.Int).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("UserEdytuj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try
        Dim PusteZdjecie() As Byte = New Byte() {}
        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.sku_nazwa = IIf(IsDBNull(cmd.Parameters("@sku_nazwa").Value), "", cmd.Parameters("@sku_nazwa").Value)
            wynik.sku = IIf(IsDBNull(cmd.Parameters("@sku").Value), "", cmd.Parameters("@sku").Value)
            wynik.zdjecie = IIf(IsDBNull(cmd.Parameters("@zdjecie").Value), PusteZdjecie, cmd.Parameters("@zdjecie").Value)
            wynik.opis = IIf(IsDBNull(cmd.Parameters("@opis").Value), "", cmd.Parameters("@opis").Value)
            wynik.cena = IIf(IsDBNull(cmd.Parameters("@cena").Value), 0, cmd.Parameters("@cena").Value)
            wynik.wysokosc = IIf(IsDBNull(cmd.Parameters("@wysokosc").Value), 0, cmd.Parameters("@wysokosc").Value)
            wynik.szerokosc = IIf(IsDBNull(cmd.Parameters("@szerokosc").Value), 0, cmd.Parameters("@szerokosc").Value)
            wynik.glebokosc = IIf(IsDBNull(cmd.Parameters("@glebokosc").Value), 0, cmd.Parameters("@glebokosc").Value)
            wynik.waga = IIf(IsDBNull(cmd.Parameters("@waga").Value), 0, cmd.Parameters("@waga").Value)
            wynik.marka = IIf(IsDBNull(cmd.Parameters("@marka").Value), "", cmd.Parameters("@marka").Value)
            wynik.branza = IIf(IsDBNull(cmd.Parameters("@branza").Value), "", cmd.Parameters("@branza").Value)
            wynik.jm = IIf(IsDBNull(cmd.Parameters("@jm").Value), "", cmd.Parameters("@jm").Value)
            wynik.max_ilosc = IIf(IsDBNull(cmd.Parameters("@max_ilosc").Value), 0, cmd.Parameters("@max_ilosc").Value)
            wynik.czy_mozna_zamawiac = IIf(IsDBNull(cmd.Parameters("@czy_mozna_zamawiac").Value), 1, cmd.Parameters("@czy_mozna_zamawiac").Value)
            wynik.blokada_id = cmd.Parameters("@blokada_id").Value
            wynik.czy_nowosc = IIf(IsDBNull(cmd.Parameters("@czy_nowosc").Value), 1, cmd.Parameters("@czy_nowosc").Value)
            wynik.czy_limit_wydan = IIf(IsDBNull(cmd.Parameters("@czy_limit_wydan").Value), 0, cmd.Parameters("@czy_limit_wydan").Value)
            wynik.kategoria = IIf(IsDBNull(cmd.Parameters("@kategoria").Value), "", cmd.Parameters("@kategoria").Value)
            wynik.opis_rozszerzony = IIf(IsDBNull(cmd.Parameters("@opis_rozszerzony").Value), "", cmd.Parameters("@opis_rozszerzony").Value)
            wynik.max_ilosc_zamowien = IIf(IsDBNull(cmd.Parameters("@MAX_ILOSC_ZAMOWIEN").Value), 0, cmd.Parameters("@MAX_ILOSC_ZAMOWIEN").Value)
            wynik.typ_okres_zamowien_id = IIf(IsDBNull(cmd.Parameters("@typ_okres_zamowien_id").Value), -1, cmd.Parameters("@typ_okres_zamowien_id").Value)
            wynik.sztuk_w_opakowaniu = IIf(IsDBNull(cmd.Parameters("@SZT_OPK").Value), 1, cmd.Parameters("@SZT_OPK").Value)

        End If
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function SKUWczytaj(ByVal sesja As Byte(), ByVal marka As String, ByVal branza As String, ByVal sku As String, ByVal nazwa As String) As SKUWczytajWynik
        Dim wynik As New SKUWczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("Stan:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_SKU_ODCZYTAJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@marka", marka)
        cmd.Parameters.AddWithValue("@branza", branza)
        cmd.Parameters.AddWithValue("@sku", sku)
        cmd.Parameters.AddWithValue("@nazwa", nazwa)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("Stan:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            'wynik.iloscStron = cmd.Parameters("@ilosc_stron").Value
            wynik.dane = ds
        End If
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function SKUEdytujFiltry(ByVal sesja As Byte()) As SKUEdytujFiltryWynik
        Dim wynik As New SKUEdytujFiltryWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("Stan:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę wypełniającą combobox: Marka, Kategoria i J.M.
        Dim cmd As New SqlClient.SqlCommand("SP_SKU_EDYTUJ_FILTRY", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("Stan:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            'wynik.iloscStron = cmd.Parameters("@ilosc_stron").Value
            wynik.dane = ds
        End If
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function UserEdytujPodstawoweDaneZapisz(ByVal sesja As Byte(), ByVal imie As String, _
        ByVal nazwisko As String, ByVal nazwa As String, ByVal telkom As String, ByVal email As String, _
        ByVal login As String, ByVal haslo As String, ByVal czy_maile As Integer, _
        ByVal blokada_id As Integer, _
        ByVal pozostaw_blokade As Boolean) As UserEdytujPodstawoweDaneZapiszWynik
        Dim wynik As New UserEdytujPodstawoweDaneZapiszWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("UserEdytujZapisz:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_USER_EDYCJA_PODSTAWOWE_DANE_ZAPISZ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@imie", imie)
        cmd.Parameters.AddWithValue("@nazwisko", nazwisko)
        cmd.Parameters.AddWithValue("@nazwa", nazwa)
        cmd.Parameters.AddWithValue("@telkom", telkom)
        cmd.Parameters.AddWithValue("@email", email)
        cmd.Parameters.AddWithValue("@login", login)
        cmd.Parameters.AddWithValue("@haslo", haslo)
        cmd.Parameters.AddWithValue("@czy_maile", czy_maile)
        cmd.Parameters.Add("@blokada_id", SqlDbType.Int, 8).Direction = ParameterDirection.InputOutput
        cmd.Parameters("@blokada_id").Value = IIf(blokada_id <= 0, DBNull.Value, blokada_id)
        cmd.Parameters.AddWithValue("@pozostaw_blokade", pozostaw_blokade)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("UserEdytujZapisz:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status >= 0 Then
            wynik.blokada_id = IIf(IsDBNull(cmd.Parameters("@blokada_id").Value), -1, cmd.Parameters("@blokada_id").Value)
        End If
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function ZapiszAtachment(ByVal plik As Byte(), ByVal nazwa As String) As ZapiszAtachmentWynik
        Dim wynik As New ZapiszAtachmentWynik


        Dim pliki() As String = Directory.GetFiles(SciezkaZalaczniki, "*.*", _
        SearchOption.AllDirectories)
        Dim rozmiar_folderu As Integer = 0
        For i = 0 To pliki.Length - 1
            Dim info As New FileInfo(pliki(i))
            rozmiar_folderu = rozmiar_folderu + info.Length
        Next

        'Dim info = From file As String In pliki Select New FileInfo(file)
        'Dim rozmiar_folderu = info.Aggregate(0, Function(licznik As Long, _
        'file As FileInfo) licznik + file.Length)
        '7340032 ;52428800
        If rozmiar_folderu + plik.Length < 52428800 Then
            Try
               
                Dim fs As FileStream = File.Create(SciezkaZalaczniki + nazwa)
                fs.Write(plik, 0, plik.Length)
                fs.Flush()
                fs.Close()
                fs.Dispose()
            Catch ex As Exception
                wynik.status = -1
                wynik.status_opis = ex.Message
                Return wynik
            End Try
        Else
            wynik.status = -1
            wynik.status_opis = "Brak miejsca w katalogu na serwerze na zapisanie pliku." & vbNewLine & " W celu dodania nowego pliku należy usunąć niepotrzebne pliki."
            Return wynik
        End If

        wynik.status = 0
        wynik.status_opis = "Zapisano plik: " + nazwa
        Return wynik
    End Function

    <WebMethod()> _
    Public Function UsunAtachment(ByVal nazwa As String) As UsunAtachmentWynik
        Dim wynik As New UsunAtachmentWynik
        Try
            System.IO.File.Delete(SciezkaZalaczniki + nazwa)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = ex.Message
            Return wynik
        End Try

        wynik.status = 0
        wynik.status_opis = "Usunięto plik: " + nazwa
        Return wynik
    End Function


    <WebMethod()> _
    Public Function WyslijAtachment(ByVal nazwa As String) As WyslijAtachmentWynik
        Dim wynik As New WyslijAtachmentWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("WyslijAtachment:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_NOTYFIKACJA_MAIL_PLIK_TEST", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@email", "adam.lagowski@cursor.pl")
        cmd.Parameters.AddWithValue("@nazwa", nazwa)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try

            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("WyslijAtachment:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function GrupyPokaz(ByVal sesja As Byte()) As GrupyPokazWynik
        Dim wynik As New GrupyPokazWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("GrupaLista:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_GRUPY_POKAZ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("GrupaLista:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If
        cnn.Close()
        Return wynik

    End Function

    <WebMethod()> _
    Public Function GrupyDodajNowa(ByVal sesja As Byte(), ByVal grupa_id As Integer, ByVal nazwa As String, ByVal ds As DataSet) As GrupyDodajNowaWynik
        Dim wynik As New GrupyDodajNowaWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.grupa_id = -1
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("GrupaDodajNowa:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_GRUPY_HIERARCHIA_DODAJ_GRUPA", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.Add("@grupa_id", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.AddWithValue("@nazwa", nazwa)
        'tworzymy XML ukryte grupy z datasetu
        Dim strXml As New StringBuilder
        strXml.Append("<ROOT>")
        For Each wiersz As DataRow In ds.Tables("Hierarchia").Rows
            strXml.Append("<ROW NADRZEDNA_ID=""" & wiersz.Item("Nadrzedna_Id") & """ PODRZEDNA_ID=""" & wiersz.Item("Podrzedna_Id") & """ DODAJ=""" & wiersz.Item("Dodaj") & """ />")
        Next
        strXml.Append("</ROOT>")
        cmd.Parameters.AddWithValue("@XML_HIERARCHIA", strXml.ToString.Replace("&", "&amp;"))
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim dsWynik As New DataSet

        Try
            da.Fill(dsWynik)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("GrupaDodajNowa:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try
        wynik.grupa_id = cmd.Parameters("@grupa_id").Value
        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value

        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function GrupyEdycjaWybrana(ByVal sesja As Byte(), ByVal grupa As String, ByVal nazwa As String) As GrupyEdycjaWybranaWynik
        Dim wynik As New GrupyEdycjaWybranaWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("GrupaLista:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_GRUPY_HIERARCHIA_EDYTUJ_GRUPA", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@grupa", grupa)
        cmd.Parameters.AddWithValue("@nazwa", nazwa)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("GrupaLista:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value

        cnn.Close()
        Return wynik

    End Function

    <WebMethod()> _
    Public Function GrupaUsun(ByVal sesja As Byte(), ByVal grupa As String) As GrupaUsunWynik
        Dim wynik As New GrupaUsunWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("GrupaUsun:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_GRUPY_HIERARCHIA_EDYCJA_USUN_GRUPA", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@grupa", grupa)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        ''tworzymy XML GRUPA
        'Dim strXmlGrupa As New StringBuilder
        'strXmlGrupa.Append("<ROOT>")
        'For Each wierszGrupa As DataRow In ds.Tables("Grupy").Rows
        '    strXmlGrupa.Append("<ROW GRUPA_ID=""" & wierszGrupa.Item("grupa_id") & """ NAZWA=""" & wierszGrupa.Item("nazwa") & """ DODAJ=""" & wierszGrupa.Item("Dodaj") & """ />")
        'Next
        'strXmlGrupa.Append("</ROOT>")
        'cmd.Parameters.AddWithValue("@XML_GRUPA", strXmlGrupa.ToString.Replace("&", "&amp;"))
        ''tworzymy XML HIERARCHIA
        'Dim strXml As New StringBuilder
        'strXml.Append("<ROOT>")
        'For Each wiersz As DataRow In ds.Tables("Hierarchia").Rows
        '    strXml.Append("<ROW NADRZEDNA_ID=""" & wiersz.Item("Nadrzedna_Id") & """ PODRZEDNA_ID=""" & wiersz.Item("Podrzedna_Id") & """ DODAJ=""" & wiersz.Item("Dodaj") & """ />")
        'Next
        'strXml.Append("</ROOT>")
        'cmd.Parameters.AddWithValue("@XML_HIERARCHIA", strXml.ToString.Replace("&", "&amp;"))

        'Dim da As New SqlDataAdapter(cmd)
        'Dim dsWynik As New DataSet

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("GrupaUsun:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value

        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function NewsleterOdczytaj(ByVal sesja As Byte(), ByVal newsleter_id As Integer) As NewsleterOdczytajWynik
        Dim wynik As New NewsleterOdczytajWynik
        Dim cnn As SqlConnection
        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("NewsleterOdczytaj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_NEWSLETER_ODCZYTAJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@NEWSLETER_ID", newsleter_id)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("NewsleterOdczytaj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function NewsleterWyslij(ByVal sesja As Byte(), ByVal tytul As String, ByVal tresc As String, ByVal dane_grupy As String, ByVal dane_typ As String, ByVal dane_wielkosc As String, ByVal dane_user As String, ByVal plik As String) As NewsleterWyslijWynik
        Dim wynik As New NewsleterWyslijWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("NewsleterWyslij:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_NEWSLETER_WYSLIJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@tytul", tytul)
        cmd.Parameters.AddWithValue("@tresc", tresc)
        cmd.Parameters.Add("@plik", SqlDbType.NVarChar, 50)
        cmd.Parameters("@plik").Value = IIf(plik Is Nothing, DBNull.Value, plik)
        cmd.Parameters.AddWithValue("@xml_grupa", dane_grupy)
        cmd.Parameters.AddWithValue("@xml_typ", dane_typ)
        cmd.Parameters.AddWithValue("@xml_wielkosc", dane_wielkosc)
        cmd.Parameters.AddWithValue("@xml_users", dane_user)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("NewsleterWyslij: Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value

        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function NewsleterLista(ByVal sesja As Byte()) As NewsleterListaWynik
        Dim wynik As New NewsleterListaWynik
        Dim cnn As SqlConnection
        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("NewsleterLista:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_NEWSLETER_LISTA", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("NewsleterLista:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If

        ''tworzymy XML grupy z datasetu
        'Dim strXmlGrupy As New StringBuilder
        'For Each wiersz As DataRow In ds.Tables(0).Rows
        '    'TODO: escape parametru sku
        '    strXmlGrupy.Append("<row grupa_id=""" & wiersz.Item("grupa_id") & """ />")
        'Next

        'cmd.Parameters.AddWithValue("@xml_grupy", strXmlGrupy.ToString.Replace("&", "&amp;"))
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function WiadomosciLista(ByVal sesja As Byte()) As WiadomosciListaWynik
        Dim wynik As New WiadomosciListaWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("WiadomosciLista:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_WIADOMOSCI_LISTA", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("NewsleterLista:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If

        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function WiadomoscDodajNowa(ByVal sesja As Byte(), ByVal tytul As String, ByVal tresc As String) As WiadomoscDodajNowaWynik
        Dim wynik As New WiadomoscDodajNowaWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("WiadomoscDodajNowa:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_WIADOMOSC_DODAJ_NOWA", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@tytul", tytul)
        cmd.Parameters.AddWithValue("@tresc", tresc)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim dsWynik As New DataSet

        Try
            da.Fill(dsWynik)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("WiadomoscDodajNowa:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try
        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value

        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function WiadomoscUsun(ByVal sesja As Byte(), ByVal wiadomosc_id As Integer) As WiadomoscUsunWynik
        Dim wynik As New WiadomoscUsunWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("WiadomoscUsun:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_WIADOMOSC_USUN", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@wiadomosc_id", wiadomosc_id)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("WiadomoscUsun:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value

        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function PlikiDoPobraniaLista(ByVal sesja As Byte()) As PlikiDoPobraniaListaWynik
        Dim wynik As New PlikiDoPobraniaListaWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("PlikiDoPobraniaLista:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_PLIKI_DO_POBRANIA_LISTA", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.Add("@centrala", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@MAX_ROZMIAR_PLIKU", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("PlikiDoPobraniaLista:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        wynik.centrala = cmd.Parameters("@centrala").Value
        wynik.maxRozmiarPliku = cmd.Parameters("@MAX_ROZMIAR_PLIKU").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If

        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function SKUEdytujZapisz(ByVal sesja As Byte(), ByVal blokada_id As Integer, _
                                    ByVal zdjecie As Byte(), ByVal zdjecie_nazwa As String, _
                                    ByVal zdjecie_zmieniono As Integer, ByVal opis As String, _
                                    ByVal marka As String, ByVal branza As String, ByVal cena As Decimal, _
                                    ByVal jm As String, ByVal wysokosc As Integer, ByVal szerokosc As Integer, _
                                    ByVal glebokosc As Integer, ByVal waga As Decimal, ByVal maxZam As Integer, _
                                    ByVal czyLimit As Integer, ByVal moznaZamawiac As Integer, _
                                    ByVal nowosc As Integer, ByVal kategoria As String, _
                                    ByVal opis_rozszerzony As String, ByVal nazwa_sku As String, _
                                    ByVal max_ilosc_zamowien As Integer, ByVal typ_okres_zamowien_id As Integer, ByVal sztuk_w_opakowaniu As Integer) As SKUEdytujZapiszWynik
        Dim wynik As New SKUEdytujZapiszWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("UserEdytuj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        If Not IsNothing(zdjecie) Then
            'czy zdjęcie nie przekracza 500KB (512000B)
            If zdjecie.Length > 512000 Then
                wynik.status = -1
                wynik.status_opis = "Rozmiar zdjęcia przekracza maksymalny dopuszczalny rozmiar 500KB!"
                cnn.Close()
                Return wynik
            End If
        End If

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_SKU_EDYCJA_ZAPISZ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@blokada_id", blokada_id)
        cmd.Parameters.Add("@zdjecie", SqlDbType.VarBinary, -1)
        If IsNothing(zdjecie) Then
            cmd.Parameters("@zdjecie").Value = DBNull.Value
        Else
            cmd.Parameters("@zdjecie").Value = zdjecie
        End If
        cmd.Parameters.AddWithValue("@zdjecie_nazwa", zdjecie_nazwa)
        If IsNothing(zdjecie_nazwa) Then
            cmd.Parameters("@zdjecie_nazwa").Value = DBNull.Value
        Else
            cmd.Parameters("@zdjecie_nazwa").Value = zdjecie_nazwa
        End If
        cmd.Parameters.AddWithValue("@zdjecie_zmieniono", zdjecie_zmieniono)
        cmd.Parameters.AddWithValue("@opis", opis)
        cmd.Parameters.AddWithValue("@marka", marka)
        cmd.Parameters.AddWithValue("@branza", branza)
        cmd.Parameters.AddWithValue("@cena", cena)
        cmd.Parameters.AddWithValue("@jm", jm)
        cmd.Parameters.AddWithValue("@wysokosc", wysokosc)
        cmd.Parameters.AddWithValue("@szerokosc", szerokosc)
        cmd.Parameters.AddWithValue("@glebokosc", glebokosc)
        cmd.Parameters.AddWithValue("@waga", waga)
        cmd.Parameters.AddWithValue("@maxZam", maxZam)
        cmd.Parameters.AddWithValue("@czyLimit", czyLimit)
        cmd.Parameters.AddWithValue("@moznaZamawiac", moznaZamawiac)
        cmd.Parameters.AddWithValue("@nowosc", nowosc)
        cmd.Parameters.AddWithValue("@kategoria", kategoria)
        cmd.Parameters.AddWithValue("@nazwa_sku_nowa", nazwa_sku)

        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.AddWithValue("@opis_rozszerzony", opis_rozszerzony)
        cmd.Parameters.AddWithValue("@max_ilosc_zamowien", max_ilosc_zamowien)
        cmd.Parameters.AddWithValue("@typ_okres_zamowien_id", typ_okres_zamowien_id)
        cmd.Parameters.AddWithValue("@szt_opk", sztuk_w_opakowaniu)

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("UserEdytuj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try
        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        cnn.Close()
        Return wynik
    End Function








    <WebMethod()> _
    Public Function ZwrotyWczytaj(ByVal sesja As Byte()) As ZwrotyWczytajWynik
        Dim wynik As New ZwrotyWczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("ZwrotyWczytaj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_ZWROTY_WCZYTAJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("ZwrotyWczytaj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If

        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function ZwrotyZapisz(ByVal sesja As Byte(), ByVal dsZwroty As DataSet) As ZwrotyZapiszWynik

        Dim wynik As New ZwrotyZapiszWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("ZwrotyZapisz:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_ZWROTY_ZAPISZ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)

        'tworzymy XML zwroty do zapisania
        If dsZwroty.Tables.Count > 0 Then
            Dim strXmlZwroty As New StringBuilder
            For Each wiersz As DataRow In dsZwroty.Tables(0).Rows
                strXmlZwroty.Append("<row sku_id=""" & wiersz.Item("sku_id") & """  nr_zamowienia=""" & wiersz.Item("nr_zamowienia") & """ ilosc_zwrot=""" & wiersz.Item("ilosc_zwrot") & """/>")
            Next
            'cmd.Parameters.AddWithValue("@xml_zwrot", strXmlZwroty.ToString.Replace("&", "&amp;"))
        Else
            cmd.Parameters.AddWithValue("@xml_zwrot", DBNull.Value)
        End If

        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("ZwrotyZapisz:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function ZapiszMiniaturke(ByVal sesja As Byte(), ByVal tytul As String, ByVal nazwa_pliku As String, ByVal miniaturka() As Byte, ByVal wyswietlajNaWWW As Boolean) As ZapiszMiniaturkeWynik
        Dim wynik As New ZapiszMiniaturkeWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("ZapiszMiniaturke:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_ZAPISZ_MINIATURKE", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@tytul", tytul)
        cmd.Parameters.AddWithValue("@nazwa_pliku", nazwa_pliku)
        cmd.Parameters.AddWithValue("@WYSWIETLAJ_NA_WWW", wyswietlajNaWWW)
        cmd.Parameters.Add("@miniaturka", SqlDbType.VarBinary, -1)
        If IsNothing(miniaturka) Then
            cmd.Parameters("@miniaturka").Value = DBNull.Value
        Else
            cmd.Parameters("@miniaturka").Value = miniaturka
        End If
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("ZapiszMiniaturke:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value

        cnn.Close()
        Return wynik

    End Function


    <WebMethod()> _
    Public Function PobierzPlik(ByVal plik_nazwa As String) As PobierzPlikWynik
        Dim wynik As New PobierzPlikWynik
        Try
            Dim plik() As Byte
            plik = File.ReadAllBytes(SciezkaZalaczniki + plik_nazwa)

            wynik.status = 0
            wynik.status_opis = "Pobrano plik"
            wynik.plik = plik
            Return wynik
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = ex.Message
            Return wynik
        End Try
    End Function



    <WebMethod()> _
    Public Function ZwrotyBezZamowienWczytaj(ByVal sesja As Byte()) As ZwrotyBezZamowienWczytajWynik
        Dim wynik As New ZwrotyBezZamowienWczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("ZwrotyBezZamowienWczytaj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_ZWROTY_BEZ_ZAMOWIEN_WCZYTAJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("ZwrotyBezZamowienWczytaj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If

        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function ZwrotyBezZamowienWczytajZamowienia(ByVal sesja As Byte(), ByVal zwrot_id As String) As ZwrotyBezZamowienWczytajZamowieniaWynik
        Dim wynik As New ZwrotyBezZamowienWczytajZamowieniaWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("ZwrotyBezZamowienWczytaj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_ZWROTY_BEZ_ZAMOWIEN_WCZYTAJ_ZAMOWIENIA", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@ZWROT_ID", zwrot_id)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("ZwrotyBezZamowienWczytaj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status = 0 Then
            wynik.dane = ds
        End If

        cnn.Close()
        Return wynik
    End Function


    <WebMethod()> _
    Public Function PlikiDoPobraniaUsun(ByVal sesja As Byte(), ByVal xml_pliki As String) As PlikiDoPobraniaUsunWynik
        Dim wynik As New PlikiDoPobraniaUsunWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("PlikiDoPobraniaUsun:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_PLIKI_DO_POBRANIA_USUN", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@xml_plik_id", xml_pliki)
        cmd.Parameters.Add("@centrala_out", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("PlikiDoPobraniaUsun:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function UserRola(ByVal sesja As Byte()) As UserRolaWynik
        Dim wynik As New UserRolaWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("ZwrotyBezZamowienWczytaj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_USER_ROLE", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("ZwrotyBezZamowienWczytaj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status = 0 Then
            wynik.dane = ds
        End If

        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function ZwrotZamowieniePrzypisz(ByVal sesja As Byte(), ByVal zwrot_id As Integer, ByVal zamowienie_id As Integer) As ZwrotZamowieniePrzypiszWynik
        Dim wynik As New ZwrotZamowieniePrzypiszWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("UserEdytujZapisz:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_ZWROT_ZAMOWIENIE_PRZYPISZ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@zwrot_id", zwrot_id)
        cmd.Parameters.AddWithValue("@zamowienie_id", zamowienie_id)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("UserEdytujZapisz:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        cnn.Close()
        Return wynik
    End Function


    <WebMethod()> _
    Public Function PierwszeLogowanieWczytaj(ByVal sesja As Byte()) As PierwszeLogowanieWczytajWynik
        Dim wynik As New PierwszeLogowanieWczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("PierwszeLogowanieWczytaj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_PIERWSZE_LOGOWANIE_WCZYTAJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandTimeout = 1
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.Add("@email", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@telefon", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("PierwszeLogowanieWczytaj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value

        If wynik.status <> -1 Then
            wynik.email = IIf(IsDBNull(cmd.Parameters("@email").Value), "", cmd.Parameters("@email").Value)
            wynik.telefon = IIf(IsDBNull(cmd.Parameters("@telefon").Value), "", cmd.Parameters("@telefon").Value)
        End If
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function ZmienHasloTelEmail(ByVal sesja As Byte(), ByVal obecne_haslo As String, _
                                       ByVal nowe_haslo As String, ByVal telefon As Integer, _
                                       ByVal email As String) As ZmienHasloTelEmailWynik
        Dim wynik As New ZmienHasloTelEmailWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("ZmienHasloTelEmail:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_ZMIEN_HASLO_TEL_EMAIL", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@obecne_haslo", obecne_haslo)
        cmd.Parameters.AddWithValue("@nowe_haslo", nowe_haslo)
        cmd.Parameters.AddWithValue("@telefon", telefon)
        cmd.Parameters.AddWithValue("@email", email)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("ZmienHasloTelEmail:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function KontaktMailWyslij(ByVal sesja As Byte(), ByVal tytul As String, ByVal tresc As String, ByVal nazwaZalacznika As String, ByVal zrodloMaila As Integer) As KontaktMailWyslijWynik
        Dim wynik As New KontaktMailWyslijWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("KontaktMailWyslij:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_KONTAKT_MAIL_WYSLIJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@tytul", tytul)
        cmd.Parameters.AddWithValue("@tresc", tresc)

        If nazwaZalacznika.Length > 0 Then
            Dim lastChar As Char = Right(SciezkaZalaczniki, 1)

            If lastChar <> "\" Then
                nazwaZalacznika = String.Format("\{0}", nazwaZalacznika) 'Zabezpieczenie przed zla sciezka
            End If
            nazwaZalacznika = String.Format("{0}{1}", SciezkaZalaczniki, nazwaZalacznika)
        End If


        cmd.Parameters.AddWithValue("@ZALACZNIK_NAZWA", nazwaZalacznika)
        cmd.Parameters.AddWithValue("@ZRODLO_MAILA", zrodloMaila)


        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("KontaktMailWyslij: Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value

        cnn.Close()
        Return wynik
    End Function


    <WebMethod()> _
    Public Function ZdjecieDodaj(ByVal sesja As Byte(), ByVal sku_id As Integer, ByVal sku As String, ByVal sciezka As String, ByVal zdjecie_nazwa As String, ByVal zdjecie() As Byte) As ZdjecieDodajWynik
        Dim wynik As New ZdjecieDodajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("ZdjecieDodaj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'czy zdjęcie nie przekracza 500KB (512000B)
        If zdjecie.Length > 512000 Then
            wynik.status = -1
            wynik.status_opis = "Rozmiar zdjęcia przekracza maksymalny dopuszczalny rozmiar 500KB!"
            cnn.Close()
            Return wynik
        End If

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_ZDJECIE_DODAJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@sku_id", sku_id)
        cmd.Parameters.AddWithValue("@sku", sku)
        cmd.Parameters.AddWithValue("@sciezka", sciezka)
        cmd.Parameters.AddWithValue("@zdjecie_nazwa", zdjecie_nazwa)
        cmd.Parameters.Add("@zdjecie", SqlDbType.VarBinary, -1)
        If IsNothing(zdjecie) Then
            cmd.Parameters("@zdjecie").Value = DBNull.Value
        Else
            cmd.Parameters("@zdjecie").Value = zdjecie
        End If
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "W bazie już istnieje zdjęcie o podanej ścieżce. Proszę wybrać inne zdjęcie."
            cnn.Close()
            logger.Error("ZdjecieDodaj:Błąd przy dodawaniu zdjęcia do bazy: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value

        cnn.Close()
        Return wynik

    End Function

    <WebMethod()> _
    Public Function ZdjeciaWczytaj(ByVal sesja As Byte(), ByVal sku_id As Integer) As ZdjeciaWczytajWynik
        Dim wynik As New ZdjeciaWczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("ZdjeciaWczytaj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_ZDJECIA_WCZYTAJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@sku_id", sku_id)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("ZdjeciaWczytaj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status = 0 Then
            wynik.dane = ds
        End If

        cnn.Close()
        Return wynik
    End Function


    <WebMethod()> _
    Public Function ZdjecieUsun(ByVal sesja As Byte(), ByVal id_zdjecia As Integer) As ZdjecieUsunWynik
        Dim wynik As New ZdjecieUsunWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("ZdjecieUsun:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_ZDJECIE_USUN", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@zdjecie_id", id_zdjecia)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output


        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "W bazie już istnieje zdjęcie o podanej ścieżce. Proszę wybrać inne zdjęcie."
            cnn.Close()
            logger.Error("ZdjecieUsun:Błąd przy usuwaniu zdjęcia z bazy: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value

        cnn.Close()
        Return wynik

    End Function



    ''' <summary>
    ''' Tworzenie lub usuwanie miniatury dla wskazanego zdjęcia
    ''' </summary>
    ''' <param name="sesja">identyfikator sesji zalogowanego użytkownika </param>
    ''' <param name="SKU_ID">Index produktu SKU</param>
    ''' <param name="ID">Index zdjęcia </param>
    ''' <param name="Image">Zdjęcie</param>
    ''' <returns></returns>
    ''' <remarks>Przekazanie null w parametrze Image powoduje usunięcie zdjęcia</remarks>
    <WebMethod()> _
    Public Function utworzMiniature(ByVal sesja As Byte(), ByVal SKU_ID As Integer, ByVal ID As Integer, ByVal Image As Byte()) As utworzMiniatureWynik
        Dim wynik As New utworzMiniatureWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("KontaktMailWyslij:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_ZDJECIE_MINIATURA", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@SKU_ID", SKU_ID)
        cmd.Parameters.AddWithValue("@ID", ID)
        cmd.Parameters.AddWithValue("@IMAGE", Image)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("KontaktMailWyslij: Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value

        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function UserWielkoscTyp(ByVal sesja As Byte()) As UserWielkoscTypWynik
        Dim wynik As New UserWielkoscTypWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("UserWielkoscTyp:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_USER_WIELKOSC_TYP", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("UserWielkoscTyp:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status = 0 Then
            wynik.dane = ds
        End If

        cnn.Close()
        Return wynik
    End Function


    <WebMethod()> _
    Public Function KategorieSkuOdczytaj(ByVal sesja As Byte()) As KategorieSkuOdczytajWynik
        Dim wynik As New KategorieSkuOdczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("KategorieSkuOdczytaj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_KATEGORIE_SKU_ODCZYTAJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("KategorieSkuOdczytaj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function CzyIstniejaWszystkieSku(ByVal sesja As Byte(), ByVal xml_sku As String) As CzyIstniejaWszystkieSkuWynik
        Dim wynik As New CzyIstniejaWszystkieSkuWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("CzyIstniejaWszystkieSku:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_CZY_ISTNIEJA_WSZYSTKIE_SKU", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@xml_sku", xml_sku)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("CzyIstniejaWszystkieSku:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try
        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function GrupyZamowieniaExcelOdczytaj(ByVal sesja As Byte(), ByVal magazyn_id As Integer) As GrupyZamowieniaExcelOdczytajWynik
        Dim wynik As New GrupyZamowieniaExcelOdczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("GrupyZamowieniaExcelOdczytaj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_GRUPY_ZAMOWIENIA_EXCEL_ODCZYTAJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@magazyn_id", magazyn_id)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("GrupyZamowieniaExcelOdczytaj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function PodajStanSkuIdZSKU(ByVal sesja As Byte(), ByVal sku_xml As String, _
                                       ByVal grupa_sku_xml As String, ByVal magazyn_id As Integer, _
                                       ByVal czy_wszystkie_zamowienia As Boolean) As PodajSkuIdZSKUWynik

        Dim wynik As New PodajSkuIdZSKUWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("PokazSku:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_PODAJ_STAN_SKU_ID_Z_SKU", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@SESJA", sesja)
        cmd.Parameters.AddWithValue("@SKU_XML", sku_xml)
        cmd.Parameters.AddWithValue("@GRUPA_SKU_XML", grupa_sku_xml)
        cmd.Parameters.AddWithValue("@MAGAZYN_ID", magazyn_id)
        cmd.Parameters.AddWithValue("@CZY_WSZYSTKIE_ZAMOWIENIA", czy_wszystkie_zamowienia)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("PokazSku:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.dane = ds
        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        cnn.Close()
        Return wynik
    End Function
    <WebMethod()> _
    Public Function PlikZamowieniaExcelZapisz(ByVal sesja As Byte(), ByVal nazwa_pliku As String, _
                                              ByVal typ_zlecenia As String, ByVal uwagi As String, _
                                              ByVal plik As Byte(), ByVal numery_zamowien As String) As PlikZamowieniaExcelZapiszWynik
        Dim wynik As New PlikZamowieniaExcelZapiszWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("PlikZamowieniaExcelZapisz:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_PLIK_ZAMOWIENIA_EXCEL_ZAPISZ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@nazwa_pliku", nazwa_pliku)
        cmd.Parameters.AddWithValue("@typ_zlecenia", typ_zlecenia)
        cmd.Parameters.AddWithValue("@uwagi", uwagi)
        cmd.Parameters.AddWithValue("@plik", plik)
        cmd.Parameters.AddWithValue("@numery_zamowien", numery_zamowien)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("PlikZamowieniaExcelZapisz:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value

        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function PlikiExcelaZamowieniaWczytaj(ByVal sesja As Byte(), _
                                                 ByVal data_od As DateTime, _
                                                 ByVal data_do As DateTime, _
                                                 ByVal filtr As String, _
                                                 ByVal sortowanie_kolumna As String,
                                                 ByVal rosnaco As Boolean, _
                                                 ByVal numer_strony As Integer,
                                                 ByVal ilosc_na_stronie As Integer) As PlikiExcelaZamowieniaWczytajWynik
        Dim wynik As New PlikiExcelaZamowieniaWczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("PlikiExcelaZamowieniaWczytaj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_PLIKI_ZAMOWIENIA_EXCEL_WCZYTAJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@data_od", data_od)
        cmd.Parameters.AddWithValue("@data_do", data_do)
        cmd.Parameters.AddWithValue("@filtr", filtr)
        cmd.Parameters.AddWithValue("@sortowanie_kolumna", sortowanie_kolumna)
        cmd.Parameters.AddWithValue("@rosnaco", rosnaco)
        cmd.Parameters.AddWithValue("@numer_strony", numer_strony)
        cmd.Parameters.AddWithValue("@ilosc_na_stronie", ilosc_na_stronie)
        cmd.Parameters.Add("@ilosc_stron", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("PlikiExcelaZamowieniaWczytaj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        wynik.ilosc_stron = IIf(IsDBNull(cmd.Parameters("@ilosc_stron").Value), -1, cmd.Parameters("@ilosc_stron").Value)
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function PlikZamowieniaExcelPobierz(ByVal sesja As Byte(), ByVal plik_id As Integer) As PlikZamowieniaExcelPobierzWynik
        Dim wynik As New PlikZamowieniaExcelPobierzWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try

            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()

        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("PlikZamowieniaExcelPobierz:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_PLIK_ZAMOWIENIA_EXCEL_POBIERZ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@plik_id", plik_id)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet
        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("PlikZamowieniaExcelPobierz:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        wynik.dane = ds
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function AdresyZamowieniaOdczytaj(ByVal sesja As Byte(), ByVal zamowienie_id As Integer, ByVal adres_id As Integer) As AdresyZamowieniaOdczytajWynik
        Dim wynik As New AdresyZamowieniaOdczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("AdresyZamowieniaOdczytajWynik:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_ADRES_ZAMOWIENIE_ODCZYTAJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@ADRES_id", adres_id)
        cmd.Parameters.AddWithValue("@ZAMOWIENIE_ID", zamowienie_id)
        cmd.Parameters.Add("@WYSWIETLAJ_COMBO", SqlDbType.Bit).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("AdresyZamowieniaOdczytajWynik:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try
        wynik.wyswietlaj_combo = cmd.Parameters("@WYSWIETLAJ_COMBO").Value
        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If
        cnn.Close()
        Return wynik
    End Function

    <WebMethod(Description:="Sprawdza czy uzytkownik jest typu ODDZIAŁ.")> _
    Public Function CzyUserOddzial(ByVal sesja As Byte()) As CzyUserOddzialWynik
        Dim wynik As New CzyUserOddzialWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("CzyUserOddzial:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_CZY_USER_ODDZIAL", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@czy_oddzial", SqlDbType.Int).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("CzyUserOddzial:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        wynik.czy_oddzial = cmd.Parameters("@czy_oddzial").Value

        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function ZamowienieOdbiorcyOdczytaj(ByVal sesja As Byte(), ByVal zamowienie_id As Integer) As ZamowienieOdbiorcyOdczytajWynik
        Dim wynik As New ZamowienieOdbiorcyOdczytajWynik
        Dim cnn As SqlConnection
        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("ZamowienieOdbiorcyOdczytaj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_ZAMOWIENIE_ODBIORCY_ODCZYTAJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@ZAMOWIENIE_ID", zamowienie_id)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("ZamowienieOdbiorcyOdczytaj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If
        cnn.Close()
        Return wynik
    End Function


    <WebMethod(Description:="Usuwa zamówienie robocze użytkownika.")> _
    Public Function ZamowienieRoboczeUsun(ByVal sesja As Byte(), ByVal zamowienie_id As Integer) As ZamowienieRoboczeUsunWynik
        Dim wynik As New ZamowienieRoboczeUsunWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("ZamowienieRoboczeUsun:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_ZAMOWIENIE_ROBOCZE_USUN", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@zamowienie_id", zamowienie_id)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("ZamowienieRoboczeUsun:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        cnn.Close()
        Return wynik
    End Function

    <WebMethod(Description:="Sprawdzenie czy osoba zalogowana nalezy do grupy Biuro")> _
    Public Function CzyBiuro(ByVal user_id As Integer) As CzyBiuroWynik

        Dim wynik As New CzyBiuroWynik
        Dim cnn As SqlConnection

        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("CzyBiuro:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_INT_CZY_CENTRALA", cnn)
        cmd.CommandTimeout = 120
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@user_id", user_id)
        cmd.Parameters.Add("@centrala", SqlDbType.Int).Direction = ParameterDirection.Output


        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("CzyBiuro:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę

        wynik.czy_biuro = cmd.Parameters("@centrala").Value

        cnn.Close()
        Return wynik
    End Function





    <WebMethod()> _
    Public Function SlownikZespolySprzedazyOdczytaj(ByVal sesja As Byte()) As SlownikZespolySprzedazyOdczytajWynik
        Dim wynik As New SlownikZespolySprzedazyOdczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("SlownikZespolySprzedazyOdczytaj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_SLOWNIK_ZESPOLY_SPRZEDAZY_ODCZYTAJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("SlownikZespolySprzedazyOdczytaj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If

        cnn.Close()
        Return wynik
    End Function

    <WebMethod(Description:="Usuwa ze słownika zespół sprzedaży.")> _
    Public Function SlownikZespolSprzedazyUsun(ByVal sesja As Byte(), ByVal zespol_sprzedazy_id As Integer) As SlownikZespolSprzedazyUsunWynik
        Dim wynik As New SlownikZespolSprzedazyUsunWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("SlownikZespolSprzedazyUsun:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_SLOWNIK_ZESPOL_SPRZEDAZY_USUN", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@zespol_sprzedazy_id", zespol_sprzedazy_id)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("SlownikZespolSprzedyUsun:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        cnn.Close()
        Return wynik
    End Function

    <WebMethod(Description:="Edytuje w słowniku zespół sprzedaży.")> _
    Public Function SlownikZespolSprzedazyEdytuj(ByVal sesja As Byte(), _
                                                 ByVal zespol_sprzedazy_id As Integer, _
                                                 ByVal nazwa_zespolu As String) As SlownikZespolSprzedazyEdytujWynik
        Dim wynik As New SlownikZespolSprzedazyEdytujWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("SlownikZespolSprzedazyEdytuj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_SLOWNIK_ZESPOL_SPRZEDAZY_EDYTUJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@zespol_sprzedazy_id", zespol_sprzedazy_id)
        cmd.Parameters.AddWithValue("@nazwa_zespolu_sprzedazy", nazwa_zespolu)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("SlownikZespolSprzedazyEdytuj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function SlownikSieciSprzedazyOdczytaj(ByVal sesja As Byte()) As SlownikSieciSprzedazyOdczytajWynik
        Dim wynik As New SlownikSieciSprzedazyOdczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("SlownikSieciSprzedazyOdczytaj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_SLOWNIK_SIECI_SPRZEDAZY_ODCZYTAJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("SlownikSieciSprzedazyOdczytaj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If

        cnn.Close()
        Return wynik
    End Function

    <WebMethod(Description:="Usuwa ze słownika sieć sprzedaży.")> _
    Public Function SlownikSiecSprzedazyUsun(ByVal sesja As Byte(), ByVal siec_sprzedazy_id As Integer) As SlownikSiecSprzedazyUsunWynik
        Dim wynik As New SlownikSiecSprzedazyUsunWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("SlownikSiecSprzedazyUsun:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_SLOWNIK_SIEC_SPRZEDAZY_USUN", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@siec_sprzedazy_id", siec_sprzedazy_id)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("SlownikSiecSprzedazyUsun:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        cnn.Close()
        Return wynik
    End Function

    <WebMethod(Description:="Edytuje w słowniku sieć sprzedaży.")> _
    Public Function SlownikSiecSprzedazyEdytuj(ByVal sesja As Byte(), _
                                                 ByVal siec_sprzedazy_id As Integer, _
                                                 ByVal nazwa_sieci As String) As SlownikSiecSprzedazyEdytujWynik
        Dim wynik As New SlownikSiecSprzedazyEdytujWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("SlownikSiecSprzedazyEdytuj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_SLOWNIK_SIEC_SPRZEDAZY_EDYTUJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@siec_sprzedazy_id", siec_sprzedazy_id)
        cmd.Parameters.AddWithValue("@nazwa_sieci_sprzedazy", nazwa_sieci)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("SlownikSiecSprzedazyEdytuj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function SlownikObszarySprzedazyOdczytaj(ByVal sesja As Byte()) As SlownikObszarySprzedazyOdczytajWynik
        Dim wynik As New SlownikObszarySprzedazyOdczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("SlownikObszarySprzedazyOdczytaj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_SLOWNIK_OBSZARY_SPRZEDAZY_ODCZYTAJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("SlownikObszarySprzedazyOdczytaj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If

        cnn.Close()
        Return wynik
    End Function

    <WebMethod(Description:="Usuwa ze słownika obszar sprzedaży.")> _
    Public Function SlownikObszarSprzedazyUsun(ByVal sesja As Byte(), ByVal obszar_sprzedazy_id As Integer) As SlownikObszarSprzedazyUsunWynik
        Dim wynik As New SlownikObszarSprzedazyUsunWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("SlownikObszarSprzedazyUsun:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_SLOWNIK_OBSZAR_SPRZEDAZY_USUN", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@obszar_sprzedazy_id", obszar_sprzedazy_id)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("SlownikObszarSprzedazyUsun:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        cnn.Close()
        Return wynik
    End Function

    <WebMethod(Description:="Edytuje w słowniku obszar sprzedaży.")> _
    Public Function SlownikObszarSprzedazyEdytuj(ByVal sesja As Byte(), _
                                                 ByVal obszar_sprzedazy_id As Integer, _
                                                 ByVal nazwa_obszaru As String) As SlownikObszarSprzedazyEdytujWynik
        Dim wynik As New SlownikObszarSprzedazyEdytujWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("SlownikObszarSprzedazyEdytuj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_SLOWNIK_OBSZAR_SPRZEDAZY_EDYTUJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@obszar_sprzedazy_id", obszar_sprzedazy_id)
        cmd.Parameters.AddWithValue("@nazwa_obszaru_sprzedazy", nazwa_obszaru)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("SlownikObszarSprzedazyEdytuj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function SlownikWielkoscOdczytaj(ByVal sesja As Byte()) As SlownikWielkoscOdczytajWynik
        Dim wynik As New SlownikWielkoscOdczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("SlownikWielkoscOdczytaj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_SLOWNIK_WIELKOSC_ODCZYTAJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("SlownikWielkoscOdczytaj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If

        cnn.Close()
        Return wynik
    End Function

    <WebMethod(Description:="Usuwa ze słownika wielkość użytkownika.")> _
    Public Function SlownikWielkoscUsun(ByVal sesja As Byte(), ByVal wielkosc_id As Integer) As SlownikWielkoscUsunWynik
        Dim wynik As New SlownikWielkoscUsunWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("SlownikObszarSprzedazyUsun:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_SLOWNIK_WIELKOSC_USUN", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@wielkosc_id", wielkosc_id)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("SlownikObszarSprzedazyUsun:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        cnn.Close()
        Return wynik
    End Function

    <WebMethod(Description:="Edytuje w słowniku wielkosc.")> _
    Public Function SlownikWielkoscEdytuj(ByVal sesja As Byte(), _
                                                 ByVal wielkosc_id As Integer, _
                                                 ByVal nazwa_wielkosci As String) As SlownikWielkoscEdytujWynik
        Dim wynik As New SlownikWielkoscEdytujWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("SlownikWielkoscEdytuj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_SLOWNIK_WIELKOSC_EDYTUJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@wielkosc_id", wielkosc_id)
        cmd.Parameters.AddWithValue("@nazwa_wielkosci", nazwa_wielkosci)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("SlownikWielkoscEdytuj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        cnn.Close()
        Return wynik
    End Function

    <WebMethod(Description:="Edytuje komunikat.")> _
    Public Function KomunikatEdytuj(ByVal sesja As Byte(), _
                                    ByVal komunikat_id As Integer, _
                                    ByVal tresc_komunikatu As String, _
                                    ByVal tresc_komunikatu_RTF As String) As KomunikatEdytujWynik
        Dim wynik As New KomunikatEdytujWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("KomunikatEdytuj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_KOMUNIKAT_EDYTUJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@KOMUNIKAT_ID", komunikat_id)
        cmd.Parameters.AddWithValue("@TRESC_KOMUNIKATU", tresc_komunikatu)
        cmd.Parameters.AddWithValue("@TRESC_KOMUNIKATU_RTF", tresc_komunikatu_RTF)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("KomunikatEdytuj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        cnn.Close()
        Return wynik
    End Function

    <WebMethod(Description:="Usuwa komunikat.")> _
    Public Function KomunikatUsun(ByVal sesja As Byte(), _
                                  ByVal komunikat_id As Integer) As KomunikatUsunWynik
        Dim wynik As New KomunikatUsunWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("KomunikatUsun:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_KOMUNIKAT_USUN", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@KOMUNIKAT_ID", komunikat_id)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("KomunikatUsun:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        cnn.Close()
        Return wynik
    End Function

    <WebMethod(Description:="Usuwa komunikat.")> _
    Public Function KomunikatGrupyPrzypisz(ByVal sesja As Byte(), _
                                           ByVal komunikat_id As Integer, _
                                           ByVal grupy_xml As String) As KomunikatGrupyPrzypiszWynik
        Dim wynik As New KomunikatGrupyPrzypiszWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("KomunikatGrupyPrzypisz:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_KOMUNIKAT_GRUPY_PRZYPISZ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@KOMUNIKAT_ID", komunikat_id)
        cmd.Parameters.AddWithValue("@GRUPY_XML", grupy_xml)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("KomunikatGrupyPrzypisz:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function KomunikatGrupyWczytaj(ByVal sesja As Byte(), _
                                          ByVal komunikat_id As Integer) As KomunikatGrupyWczytajWynik
        Dim wynik As New KomunikatGrupyWczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("KomunikatGrupyWczytaj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_KOMUNIKAT_GRUPY_WCZYTAJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@KOMUNIKAT_ID", komunikat_id)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("KomunikatGrupyWczytaj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If

        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function KomunikatyWczytaj(ByVal sesja As Byte()) As KomunikatyWczytajWynik
        Dim wynik As New KomunikatyWczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("KomunikatyWczytaj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_KOMUNIKATY_ODCZYTAJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("KomunikatyWczytaj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If

        cnn.Close()
        Return wynik
    End Function




    '<WebMethod()> _
    'Public Function ProduktyLimitLogistycznyListaWczytaj(ByVal sesja As Byte()) As ProduktyLimitLogistycznyListaWczytajWynik
    '    Dim wynik As New ProduktyLimitLogistycznyListaWczytajWynik
    '    Dim cnn As SqlConnection

    '    'łączymy do bazy
    '    Try
    '        cnn = New SqlConnection()
    '        cnn.ConnectionString = connectionString
    '        cnn.Open()
    '    Catch ex As Exception
    '        wynik.status = -1
    '        wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
    '        logger.Error("ProduktyLimitLogistycznyListaWczytaj:Błąd połączenia do bazy danych: ", ex)
    '        Return wynik
    '    End Try

    '    'wywołujemy procedurę zaloguj
    '    Dim cmd As New SqlClient.SqlCommand("SP_SKU_LIMITY_LOGISTYCZNE_LISTA", cnn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.CommandTimeout = 600
    '    cmd.Parameters.AddWithValue("@sesja", sesja)
    '    cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
    '    cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
    '    Dim da As New SqlDataAdapter(cmd)
    '    Dim ds As New DataSet

    '    Try
    '        da.Fill(ds)
    '    Catch ex As Exception
    '        wynik.status = -1
    '        wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
    '        cnn.Close()
    '        logger.Error("ProduktyLimitLogistycznyListaWczytaj:Błąd komunikacji z bazą: ", ex)
    '        Return wynik
    '    End Try

    '    wynik.status = cmd.Parameters("@status").Value
    '    wynik.status_opis = cmd.Parameters("@status_opis").Value
    '    If wynik.status <> -1 Then
    '        wynik.dane = ds
    '    End If
    '    cnn.Close()
    '    Return wynik
    'End Function

    '<WebMethod()> _
    'Public Function ProduktyLimitLogistycznyZapisz(ByVal sesja As Byte(), ByVal dane As DataSet) As ProduktyLimitLogistycznyZapiszWynik
    '    Dim wynik As New ProduktyLimitLogistycznyZapiszWynik
    '    Dim cnn As SqlConnection

    '    'łączymy do bazy
    '    Try
    '        cnn = New SqlConnection()
    '        cnn.ConnectionString = connectionString
    '        cnn.Open()
    '    Catch ex As Exception
    '        wynik.status = -1
    '        wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
    '        logger.Error("ProduktyLimitLogistycznyZapisz:Błąd połączenia do bazy danych: ", ex)
    '        Return wynik
    '    End Try

    '    'wywołujemy procedurę
    '    Dim cmd As New SqlClient.SqlCommand("SP_SKU_LIMITY_LOGISTYCZNE_ZAPISZ", cnn)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.AddWithValue("@sesja", sesja)
    '    'tworzymy XML limity
    '    Dim strXml As New StringBuilder
    '    For Each wiersz As DataRow In dane.Tables(0).Rows
    '        strXml.Append("<row sku_id=""" & wiersz.Item("sku_id") & """ limit=""" & wiersz.Item("limit") & """ />")
    '    Next
    '    cmd.Parameters.AddWithValue("@xml_limity", strXml.ToString.Replace("&", "&amp;"))
    '    cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
    '    cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

    '    Try
    '        cmd.ExecuteNonQuery()
    '    Catch ex As Exception
    '        wynik.status = -1
    '        wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
    '        cnn.Close()
    '        logger.Error("ProduktyLimitLogistycznyZapisz:Błąd komunikacji z bazą: ", ex)
    '        Return wynik
    '    End Try

    '    wynik.status = cmd.Parameters("@status").Value
    '    wynik.status_opis = cmd.Parameters("@status_opis").Value
    '    cnn.Close()
    '    Return wynik
    'End Function

    <WebMethod()> _
    Public Function NotyfikacjeOdczytaj(ByVal sesja As Byte(), ByVal filtr_user_id As Integer) As NotyfikacjeOdczytajWynik
        Dim wynik As New NotyfikacjeOdczytajWynik
        Dim cnn As SqlConnection
        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("NotyfikacjeOdczytaj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_NOTYFIKACJE_ODCZYTAJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@FILTR_USER_ID", filtr_user_id)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("NewsleterOdczytaj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function NotyfikacjeZapisz(ByVal sesja As Byte(), ByVal dane As DataTable) As NotyfikacjeZapiszWynik
        Dim wynik As New NotyfikacjeZapiszWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("ProduktyLimitLogistycznyZapisz:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_NOTYFIKACJE_ZAPISZ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        'tworzymy XML limity
        Dim strXml As New StringBuilder
        For Each wiersz As DataRow In dane.Rows
            strXml.Append("<row user_id=""" & wiersz.Item("user_id") & """ notyfikacja=""" & wiersz.Item("notyfikacja") & """ wlacz=""" & wiersz.Item("wlacz") & """ />")
        Next
        cmd.Parameters.AddWithValue("@DANE", strXml.ToString.Replace("&", "&amp;"))
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("ProduktyLimitLogistycznyZapisz:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function NotyfikacjeSystemoweZapisz(ByVal sesja As Byte(), ByVal dane As DataTable) As NotyfikacjeSystemoweZapiszWynik
        Dim wynik As New NotyfikacjeSystemoweZapiszWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("NotyfikacjeSystemoweZapisz:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_NOTYFIKACJE_SYSTEMOWE_ZAPISZ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        'tworzymy XML limity
        Dim strXml As New StringBuilder
        For Each wiersz As DataRow In dane.Rows
            strXml.Append("<row notyfikacja=""" & wiersz.Item("notyfikacja") & """ adres=""" & wiersz.Item("adres") & """ />")
        Next
        cmd.Parameters.AddWithValue("@DANE", strXml.ToString.Replace("&", "&amp;"))
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("ProduktyLimitLogistycznyZapisz:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        cnn.Close()
        Return wynik
    End Function

#Region "Awizacja"
    <WebMethod(Description:="Metoda zwraca listę SKU.")> _
    Public Function SkuListaWczytaj(ByVal sesja As Byte()) As SkuListaWczytajWynik

        Dim wynik As New SkuListaWczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            cnn.Close()
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_LISTA_SKU", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@SESJA", sesja)
        cmd.Parameters.Add("@nazwa_arkusz_dodaj_pozycje_awiza", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try

            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            Return wynik
        End Try

        wynik.dane = ds
        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        wynik.nazwaSzablonuDodajPozycjeAwiza = NZ(cmd.Parameters("@nazwa_arkusz_dodaj_pozycje_awiza").Value, "")
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function DostawcaEdycjaZapisz(ByVal sesja As Byte(), ByVal dostawca_id As Integer, ByVal nazwa As String, _
        ByVal adres As String, ByVal kod As String, ByVal miasto As String, ByVal kraj As String) As DostawcaEdycjaZapiszWynik
        Dim wynik As New DostawcaEdycjaZapiszWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            cnn.Close()
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_DOSTAWCA_ZAPISZ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@dostawca_id", dostawca_id)
        cmd.Parameters.AddWithValue("@nazwa", nazwa)
        cmd.Parameters.AddWithValue("@adres", adres)
        cmd.Parameters.AddWithValue("@kod", kod)
        cmd.Parameters.AddWithValue("@miasto", miasto)
        cmd.Parameters.AddWithValue("@kraj", kraj)
        cmd.Parameters.Add("@dostawca_id_out", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status = 0 Then
            wynik.dostawca_id_out = cmd.Parameters("@dostawca_id_out").Value
        End If
        cnn.Close()
        Return wynik
    End Function

    <WebMethod(Description:="Metoda zwraca listę dostawców.")> _
    Public Function DostawcyWczytaj(ByVal sesja As Byte()) As DostawcyWczytajWynik

        Dim wynik As New DostawcyWczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            cnn.Close()
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_LISTA_DOSTAWCY", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@SESJA", sesja)
        cmd.Parameters.Add("@nazwa_arkusz_dodaj_pozycje_awiza", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try

            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            Return wynik
        End Try

        wynik.dane = ds
        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        wynik.nazwaSzablonuDodajPozycjeAwiza = cmd.Parameters("@nazwa_arkusz_dodaj_pozycje_awiza").Value
        cnn.Close()
        Return wynik
    End Function


    <WebMethod(Description:="Metoda zwraca listę dostawców.")> _
    Public Function DostawcaSzczegolyWczytaj(ByVal sesja As Byte(), ByVal dostawca_id As Integer) As DostawcaSzczegolyWczytajWynik

        Dim wynik As New DostawcaSzczegolyWczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            cnn.Close()
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_DOSTAWCA_SZCZEGOLY_WCZYTAJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@SESJA", sesja)
        cmd.Parameters.AddWithValue("@DOSTAWCA_ID", dostawca_id)
        cmd.Parameters.Add("@adres", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@kod", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@miasto", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@kraj", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@NAZWA_ARKUSZ_DODAJ_POZYCJE_AWIZA", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try

            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        wynik.adres = NZ(cmd.Parameters("@adres").Value, "")
        wynik.kod = NZ(cmd.Parameters("@kod").Value, "")
        wynik.miasto = NZ(cmd.Parameters("@miasto").Value, "")
        wynik.kraj = NZ(cmd.Parameters("@kraj").Value, "")
        wynik.nazwaSzablonuDodajPozycjeAwiza = NZ(cmd.Parameters("@NAZWA_ARKUSZ_DODAJ_POZYCJE_AWIZA").Value, "")

        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function DostawcaUsun(ByVal sesja As Byte(), ByVal dostawca_id As Integer) As DostawcaUsunWynik
        Dim wynik As New DostawcaUsunWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            cnn.Close()
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_DOSTAWCA_USUN", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@dostawca_id", dostawca_id)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        cnn.Close()
        Return wynik
    End Function

    <WebMethod(Description:="Metoda zakłada nowy produkt w QGUARZE.")> _
    Public Function ZalozNowyProduktQGUAR(ByVal sesja As Byte(), ByVal sku As String, ByVal sku_nazwa As String, _
                                          ByVal klasa As String, ByVal kategoria As String, ByVal brand As String, ByVal jm As String, ByVal cena As Decimal, _
                                          ByVal id_grupa_art_Q As Integer) As ZalozNowyProduktQGUARWynik

        Dim wynik As New ZalozNowyProduktQGUARWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            cnn.Close()
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_ZALOZ_PRODUKT", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandTimeout = 600
        cmd.Parameters.AddWithValue("@SESJA", sesja)
        cmd.Parameters.AddWithValue("@SKU", sku)
        cmd.Parameters.AddWithValue("@SKU_NAZWA", sku_nazwa)
        cmd.Parameters.AddWithValue("@KLASA", klasa)
        cmd.Parameters.AddWithValue("@KATEGORIA", kategoria)
        cmd.Parameters.AddWithValue("@JM", jm)
        cmd.Parameters.AddWithValue("@BRAND", brand)
        cmd.Parameters.AddWithValue("@CENA", cena)
        cmd.Parameters.AddWithValue("@ID_GRUPA_ART_QGUAR", id_grupa_art_Q)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try

            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function AwizoZapisz(ByVal sesja As Byte(), ByVal awizo_id As Integer, _
         ByVal dostawca_id As Integer, ByVal numer_PO As String, ByVal planowana_data_dostawy As DateTime, _
        ByVal osoba_kontaktowa As String, ByVal telefon_kontaktowy As String, ByVal uwagi As String, _
        ByVal dane As DataSet, ByVal ilosc_palet As Integer, ByVal ilosc_paczek As Integer, ByVal typ_dostawy_Q As Integer) As AwizoZapiszWynik
        Dim wynik As New AwizoZapiszWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            cnn.Close()
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_AWIZO_ZAPISZ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@awizo_id", awizo_id)
        'tworzymy XML pozycje
        Dim strXmlPozycje As New StringBuilder
        For Each wiersz As DataRow In dane.Tables(0).Rows
            strXmlPozycje.Append("<row sku=""" & wiersz.Item("sku").ToString().Replace("&", "&amp;").Replace("'", "&apos;").Replace("""", "&quot;") & """ sku_nazwa=""" & wiersz.Item("nazwa").ToString().Replace("&", "&amp;").Replace("'", "&apos;").Replace("""", "&quot;") & _
                                 """ ilosc=""" & wiersz.Item("ilosc") & """ cena=""0" & """ grupa_id=""" & wiersz.Item("grupa_id") & """ />")
        Next
        cmd.Parameters.AddWithValue("@xml_dane", strXmlPozycje.ToString)
        cmd.Parameters.AddWithValue("@osoba_kontaktowa", osoba_kontaktowa)
        cmd.Parameters.AddWithValue("@telefon_kontaktowy", telefon_kontaktowy)
        cmd.Parameters.AddWithValue("@planowana_data_dostawy", planowana_data_dostawy)
        cmd.Parameters.AddWithValue("@dostawca_id", dostawca_id)
        cmd.Parameters.AddWithValue("@numer_PO", numer_PO)
        cmd.Parameters.AddWithValue("@uwagi", uwagi)
        cmd.Parameters.AddWithValue("@ilosc_palet", ilosc_palet)
        cmd.Parameters.AddWithValue("@ilosc_paczek", ilosc_paczek)
        cmd.Parameters.AddWithValue("@qguar_delivery_kind_id", typ_dostawy_Q)
        cmd.Parameters.Add("@awizo_id_out", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            Return wynik
        End Try
        wynik.awizo_id_out = cmd.Parameters("@awizo_id_out").Value
        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        cnn.Close()
        Return wynik
    End Function

    <WebMethod(Description:="Metoda zwraca listę awiz.")> _
    Public Function AwizaListaWczytaj(ByVal sesja As Byte(), _
        ByVal data_utworzenia_od As Int64, ByVal data_utworzenia_do As Int64, _
        ByVal data_planowana_dostawy_od As Int64, ByVal data_planowana_dostawy_do As Int64, _
        ByVal nr_awiza As String, ByVal nr_po As String, ByVal dostawca As String, _
        ByVal qguar_za As String, ByVal qguar_dostawa As String, _
        ByVal sortowanie As String, _
        ByVal strona As Integer, ByVal ilosc_na_stronie As Integer,
        ByVal rosnaco As Boolean, ByVal strXmlStatusy As String, ByVal sku As String) As AwizaListaWczytajWynik

        Dim wynik As New AwizaListaWczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            cnn.Close()
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_LISTA_AWIZA", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@SESJA", sesja)
        cmd.Parameters.AddWithValue("@strona", strona)
        cmd.Parameters.AddWithValue("@ilosc_na_stronie", ilosc_na_stronie)
        cmd.Parameters.AddWithValue("@DATA_UTWORZENIA_OD", IIf(data_utworzenia_od < 0, DBNull.Value, DateTime.FromBinary(data_utworzenia_od)))
        cmd.Parameters.AddWithValue("@DATA_UTWORZENIA_DO", IIf(data_utworzenia_do < 0, DBNull.Value, DateTime.FromBinary(data_utworzenia_do)))
        cmd.Parameters.AddWithValue("@DATA_PLANOWANA_DOSTAWA_OD", IIf(data_planowana_dostawy_od < 0, DBNull.Value, DateTime.FromBinary(data_planowana_dostawy_od)))
        cmd.Parameters.AddWithValue("@DATA_PLANOWANA_DOSTAWA_DO", IIf(data_planowana_dostawy_do < 0, DBNull.Value, DateTime.FromBinary(data_planowana_dostawy_do)))
        cmd.Parameters.AddWithValue("@NR_AWIZA", nr_awiza)
        cmd.Parameters.AddWithValue("@NR_PO", nr_po)
        cmd.Parameters.AddWithValue("@DOSTAWCA", dostawca)
        cmd.Parameters.AddWithValue("@sku", sku)
        cmd.Parameters.AddWithValue("@QGUAR_ZA", qguar_za)
        cmd.Parameters.AddWithValue("@QGUAR_DOSTAWA", qguar_dostawa)
        cmd.Parameters.AddWithValue("@sortowanie", IIf(sortowanie Is Nothing, DBNull.Value, sortowanie))
        cmd.Parameters.AddWithValue("@rosnaco", rosnaco)
        cmd.Parameters.AddWithValue("@XML_STATUSY", strXmlStatusy)
        cmd.Parameters.Add("@ILOSC_TOTAL_REKORDOW", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@ilosc_stron", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try

            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            Return wynik
        End Try
        wynik.ilosc_stron = IIf(IsDBNull(cmd.Parameters("@ilosc_stron").Value), -1, cmd.Parameters("@ilosc_stron").Value)
        wynik.ilosc_total_rekordow = IIf(IsDBNull(cmd.Parameters("@ILOSC_TOTAL_REKORDOW").Value), -1, cmd.Parameters("@ILOSC_TOTAL_REKORDOW").Value)
        wynik.dane = ds
        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        cnn.Close()
        Return wynik
    End Function

    <WebMethod(Description:="Metoda zwraca listę typów dostaw Qguar.")> _
    Public Function AwizoTypyDostawQguarWczytaj(ByVal sesja As Byte()) As AwizoTypyDostawQguarWczytajWynik

        Dim wynik As New AwizoTypyDostawQguarWczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            cnn.Close()
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_LISTA_TYPY_DOSTAW_QGUAR", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@SESJA", sesja)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            Return wynik
        End Try

        wynik.dane = ds
        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        cnn.Close()
        Return wynik
    End Function

  <WebMethod(Description:="Metoda zwraca szczegóły dotyczące konkretnego awiza.")> _
    Public Function AwizaSzczegolyWczytaj(ByVal sesja As Byte(), ByVal awizo_id As Integer) As AwizaSzczegolyWczytajWynik

        Dim wynik As New AwizaSzczegolyWczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            cnn.Close()
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_SZCZEGOLY_AWIZA", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@SESJA", sesja)
        cmd.Parameters.AddWithValue("@AWIZO_ID", awizo_id)
        cmd.Parameters.Add("@dostawca_nazwa", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@dostawca_adres", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@dostawca_kod", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@dostawca_miasto", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@dostawca_kraj", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@osoba_kontaktowa", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@telefon", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@uwagi", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@numer_PO", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@planowana_data_dostawy", SqlDbType.DateTime).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@awizo_status", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@qguar_za", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@qguar_dostawa", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@ilosc_palet", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@ilosc_paczek", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@qguar_delivery_kind", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try

            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            Return wynik
        End Try

        wynik.dane = ds
        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        wynik.dostawca_adres = IIf(IsDBNull(cmd.Parameters("@dostawca_adres").Value), "", cmd.Parameters("@dostawca_adres").Value)
        wynik.dostawca_kod = IIf(IsDBNull(cmd.Parameters("@dostawca_kod").Value), "", cmd.Parameters("@dostawca_kod").Value)
        wynik.dostawca_kraj = IIf(IsDBNull(cmd.Parameters("@dostawca_kraj").Value), "", cmd.Parameters("@dostawca_kraj").Value)
        wynik.dostawca_miasto = IIf(IsDBNull(cmd.Parameters("@dostawca_miasto").Value), "", cmd.Parameters("@dostawca_miasto").Value)
        wynik.dostawca_nazwa = IIf(IsDBNull(cmd.Parameters("@dostawca_nazwa").Value), "", cmd.Parameters("@dostawca_nazwa").Value)
        wynik.planowana_data_dostawy = IIf(IsDBNull(cmd.Parameters("@planowana_data_dostawy").Value), "", cmd.Parameters("@planowana_data_dostawy").Value)
        wynik.numer_PO = IIf(IsDBNull(cmd.Parameters("@numer_PO").Value), "", cmd.Parameters("@numer_PO").Value)
        wynik.osoba_kontaktowa = IIf(IsDBNull(cmd.Parameters("@osoba_kontaktowa").Value), "", cmd.Parameters("@osoba_kontaktowa").Value)
        wynik.telefon = IIf(IsDBNull(cmd.Parameters("@telefon").Value), "", cmd.Parameters("@telefon").Value)
        wynik.uwagi = IIf(IsDBNull(cmd.Parameters("@uwagi").Value), "", cmd.Parameters("@uwagi").Value)
        wynik.qguar_dostawa = IIf(IsDBNull(cmd.Parameters("@qguar_dostawa").Value), "", cmd.Parameters("@qguar_dostawa").Value)
        wynik.qguar_za = IIf(IsDBNull(cmd.Parameters("@qguar_za").Value), "", cmd.Parameters("@qguar_za").Value)
        wynik.ilosc_palet = IIf(IsDBNull(cmd.Parameters("@ilosc_palet").Value), "", cmd.Parameters("@ilosc_palet").Value)
        wynik.ilosc_paczek = IIf(IsDBNull(cmd.Parameters("@ilosc_paczek").Value), "", cmd.Parameters("@ilosc_paczek").Value)
        wynik.awizo_status = IIf(IsDBNull(cmd.Parameters("@awizo_status").Value), "", cmd.Parameters("@awizo_status").Value)
        wynik.qguar_delivery_kind = IIf(IsDBNull(cmd.Parameters("@qguar_delivery_kind").Value), "", cmd.Parameters("@qguar_delivery_kind").Value)
        

        cnn.Close()
        Return wynik
    End Function


    <WebMethod(Description:="Zapisuje do bazy plik excela")> _
    Public Function PlikExcelZakladanieSkuZapisz(ByVal sesja As Byte(), ByVal plik As Byte(), _
                                       ByVal nazwa_pliku As String, ByVal numery_sku As String) As PlikExcelZakladanieSkuZapiszWynik

        Dim wynik As New PlikExcelZakladanieSkuZapiszWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("PlikExcelZakladanieSkuZapisz:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_PLIK_EXCEL_ZAKLADANIE_SKU_ZAPISZ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@plik", plik)
        cmd.Parameters.AddWithValue("@nazwa_pliku", nazwa_pliku)
        cmd.Parameters.AddWithValue("@numery_sku", numery_sku)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("PlikExcelZakladanieSkuZapisz:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        cnn.Close()
        Return wynik
    End Function

    <WebMethod(Description:="Metoda wczytuje awiza jeśli było wcześniej zapisane, jeśli nie otwiera nowe.")> _
    Public Function AwizoWczytaj(ByVal sesja As Byte(), ByVal awizo_id As Integer) As AwizoWczytajWynik

        Dim wynik As New AwizoWczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            cnn.Close()
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_AWIZO_WCZYTAJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@SESJA", sesja)
        cmd.Parameters.AddWithValue("@awizo_id", awizo_id)
        cmd.Parameters.Add("@osoba_kontaktowa", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@telefon", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@uwagi", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@numer_po", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@planowana_data_dostawy", SqlDbType.DateTime).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@ilosc_palet", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@ilosc_paczek", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@awizo_status", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@qguar_za", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@qguar_dostawa", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@dostawca_id", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@awizo_id_out", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@qguar_delivery_kind_id", SqlDbType.Int).Direction = ParameterDirection.Output
        '' cmd.Parameters("@awizo_id").Value = IIf(awizo_id < 0, DBNull.Value, awizo_id)
        'cmd.Parameters("@awizo_id").Value = awizo_id 'IIf(awizo_id < 0, DBNull.Value, awizo_id)

        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try

            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            Return wynik
        End Try

        wynik.dane = ds
        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        wynik.awizo_id_out = IIf(IsDBNull(cmd.Parameters("@awizo_id_out").Value), 0, cmd.Parameters("@awizo_id_out").Value)
        wynik.dostawca_id = IIf(IsDBNull(cmd.Parameters("@dostawca_id").Value), -1, cmd.Parameters("@dostawca_id").Value)
        wynik.planowana_data_dostawy = IIf(IsDBNull(cmd.Parameters("@planowana_data_dostawy").Value), "", cmd.Parameters("@planowana_data_dostawy").Value)
        wynik.ilosc_palet = IIf(IsDBNull(cmd.Parameters("@ilosc_palet").Value), "", cmd.Parameters("@ilosc_palet").Value)
        wynik.ilosc_paczek = IIf(IsDBNull(cmd.Parameters("@ilosc_paczek").Value), "", cmd.Parameters("@ilosc_paczek").Value)
        wynik.osoba_kontaktowa = IIf(IsDBNull(cmd.Parameters("@osoba_kontaktowa").Value), "", cmd.Parameters("@osoba_kontaktowa").Value)
        wynik.telefon = IIf(IsDBNull(cmd.Parameters("@telefon").Value), "", cmd.Parameters("@telefon").Value)
        wynik.uwagi = IIf(IsDBNull(cmd.Parameters("@uwagi").Value), "", cmd.Parameters("@uwagi").Value)
        wynik.numer_po = IIf(IsDBNull(cmd.Parameters("@numer_po").Value), "", cmd.Parameters("@numer_po").Value)
        wynik.qguar_dostawa = IIf(IsDBNull(cmd.Parameters("@qguar_dostawa").Value), "", cmd.Parameters("@qguar_dostawa").Value)
        wynik.qguar_za = IIf(IsDBNull(cmd.Parameters("@qguar_za").Value), "", cmd.Parameters("@qguar_za").Value)
        wynik.awizo_status = IIf(IsDBNull(cmd.Parameters("@awizo_status").Value), "", cmd.Parameters("@awizo_status").Value)
        wynik.qguar_delivery_kind_id = IIf(IsDBNull(cmd.Parameters("@qguar_delivery_kind_id").Value), 2, cmd.Parameters("@qguar_delivery_kind_id").Value)
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function AwizoUsun(ByVal sesja As Byte(), ByVal awizo_id As Integer) As AwizoUsunWynik
        Dim wynik As New AwizoUsunWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            cnn.Close()
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_AWIZO_USUN", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@awizo_id", awizo_id)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output


        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function AwizoZatwierdz(ByVal sesja As Byte(), _
        ByVal awizo_id As Integer, ByVal dostawca_id As Integer, ByVal numer_PO As String, ByVal planowana_data_dostawy As DateTime, _
        ByVal osoba_kontaktowa As String, ByVal telefon_kontaktowy As String, ByVal uwagi As String, _
        ByVal dane As DataSet, ByVal ilosc_palet As Integer, ByVal ilosc_paczek As Integer, ByVal id_zamowienie_zwrot As Integer, ByVal typ_dostawy_Q As Integer) As AwizoZatwierdzWynik
        Dim wynik As New AwizoZatwierdzWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            cnn.Close()
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_AWIZO_ZATWIERDZ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@awizo_id", awizo_id)
        'tworzymy XML pozycje
        Dim strXmlPozycje As New StringBuilder
        For Each wiersz As DataRow In dane.Tables(0).Rows
            strXmlPozycje.Append("<row sku=""" & wiersz.Item("sku") & """ sku_nazwa=""" & wiersz.Item("nazwa").ToString().Replace("&", "&amp;").Replace("'", "&apos;").Replace("""", "&quot;") & _
                            """ ilosc=""" & wiersz.Item("ilosc") & """ cena=""0" & """ grupa_id=""" & wiersz.Item("grupa_id") & """ />")
        Next
        cmd.Parameters.AddWithValue("@xml_dane", strXmlPozycje.ToString)
        cmd.Parameters.AddWithValue("@osoba_kontaktowa", osoba_kontaktowa)
        cmd.Parameters.AddWithValue("@telefon_kontaktowy", telefon_kontaktowy)
        cmd.Parameters.AddWithValue("@planowana_data_dostawy", planowana_data_dostawy)
        cmd.Parameters.AddWithValue("@dostawca_id", dostawca_id)
        cmd.Parameters.AddWithValue("@numer_PO", numer_PO)
        cmd.Parameters.AddWithValue("@uwagi", uwagi)
        cmd.Parameters.AddWithValue("@ilosc_palet", ilosc_palet)
        cmd.Parameters.AddWithValue("@ilosc_paczek", ilosc_paczek)
        cmd.Parameters.AddWithValue("@id_zamowienie_zwrot", id_zamowienie_zwrot)
        cmd.Parameters.AddWithValue("@qguar_delivery_kind_id", typ_dostawy_Q)
        cmd.Parameters.Add("@awizo_id_out", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            Return wynik
        End Try

        wynik.awizo_id_out = IIf(IsDBNull(cmd.Parameters("@awizo_id_out").Value), 0, cmd.Parameters("@awizo_id_out").Value)
        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function AwizoAnuluj(ByVal sesja As Byte(), ByVal awizo_id As Integer, _
                                ByVal QguarZA As String, ByVal NrDostawy As String) As AwizoAnulujWynik
        Dim wynik As New AwizoAnulujWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            cnn.Close()
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_AWIZO_ANULUJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@awizo_id", awizo_id)
        cmd.Parameters.AddWithValue("@qguar_za", QguarZA)
        cmd.Parameters.AddWithValue("@numer_dostawy", NrDostawy)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function AwizoPrzygotujZwrot(ByVal sesja As Byte(), ByVal zamowienie_id As Integer) As AwizoPrzygotujZwrotWynik
        Dim wynik As New AwizoPrzygotujZwrotWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            cnn.Close()
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_AWIZO_PRZYGOTUJ_ZWROT", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@zamowienie_id", zamowienie_id)
        cmd.Parameters.Add("@dostawca_id", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@osoba_kontaktowa", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@telefon", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@uwagi", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@numer_po", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@planowana_data_dostawy", SqlDbType.DateTime).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@ilosc_palet", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@ilosc_paczek", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try

            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            Return wynik
        End Try

        wynik.dane = ds
        wynik.dostawca_id = IIf(IsDBNull(cmd.Parameters("@dostawca_id").Value), -1, cmd.Parameters("@dostawca_id").Value)
        wynik.planowana_data_dostawy = IIf(IsDBNull(cmd.Parameters("@planowana_data_dostawy").Value), Today, cmd.Parameters("@planowana_data_dostawy").Value)
        wynik.ilosc_palet = IIf(IsDBNull(cmd.Parameters("@ilosc_palet").Value), "", cmd.Parameters("@ilosc_palet").Value)
        wynik.ilosc_paczek = IIf(IsDBNull(cmd.Parameters("@ilosc_paczek").Value), "", cmd.Parameters("@ilosc_paczek").Value)
        wynik.osoba_kontaktowa = IIf(IsDBNull(cmd.Parameters("@osoba_kontaktowa").Value), "", cmd.Parameters("@osoba_kontaktowa").Value)
        wynik.telefon = IIf(IsDBNull(cmd.Parameters("@telefon").Value), "", cmd.Parameters("@telefon").Value)
        wynik.uwagi = IIf(IsDBNull(cmd.Parameters("@uwagi").Value), "", cmd.Parameters("@uwagi").Value)
        wynik.numer_po = IIf(IsDBNull(cmd.Parameters("@numer_po").Value), "", cmd.Parameters("@numer_po").Value)
        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        cnn.Close()
        Return wynik
    End Function

#End Region

#Region "Dane dostawy dla zamówienia"

    <WebMethod()> _
    Public Function DostawyGwarantowaneTypyWczytaj(ByVal sesja As Byte()) As DostawyGwarantowaneTypyWczytajWynik

        Dim wynik As New DostawyGwarantowaneTypyWczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            cnn.Close()
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_DOSTAWY_GWARANTOWANE_TYP_WCZYTAJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@SESJA", sesja)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try

            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            Return wynik
        End Try

        wynik.dane = ds
        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value

        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function KodyPocztoweDpdWczytaj(ByVal sesja As Byte()) As KodyPocztoweDpdWczytajWynik

        Dim wynik As New KodyPocztoweDpdWczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            cnn.Close()
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_KODY_POCZTOWE_DPD_WCZYTAJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@SESJA", sesja)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try

            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            Return wynik
        End Try

        wynik.dane = ds
        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value

        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function KodyPocztoweOdczytaj(ByVal sesja As Byte(), ByVal kod_pocztowy As String) As KodyPocztoweOdczytajWynik
        Dim wynik As New KodyPocztoweOdczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("AdresyOdczytaj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_KOD_POCZTOWY_ODCZYTAJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@kod_pocztowy", kod_pocztowy)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("KodyPocztoweOdczytaj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function ZamowienieDaneDpdWczytaj(ByVal sesja As Byte(), _
        ByVal zamowienie_id As Integer) As ZamowienieDaneDpdWczytajWynik
        Dim wynik As New ZamowienieDaneDpdWczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            cnn.Close()
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_ZAMOWIENIE_DANE_DPD_WCZYTAJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@zamowienie_id", zamowienie_id)
        cmd.Parameters.Add("@dok_zw", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@prz_zw", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@osoba_pryw", SqlDbType.Int).Direction = ParameterDirection.Output
        Dim pIn As SqlParameter = cmd.Parameters.Add("@wartosc", SqlDbType.Decimal)
        pIn.Direction = ParameterDirection.Output
        pIn.Precision = 18
        pIn.Scale = 2
        pIn = cmd.Parameters.Add("@kwota_cod", SqlDbType.Decimal)
        pIn.Direction = ParameterDirection.Output
        pIn.Precision = 18
        pIn.Scale = 2
        cmd.Parameters.Add("@typ", SqlDbType.NVarChar, 255).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            Return wynik
        End Try


        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status > -1 Then
            wynik.DokZw = cmd.Parameters("@dok_zw").Value
            wynik.PrzZw = cmd.Parameters("@prz_zw").Value
            wynik.OsPryw = cmd.Parameters("@osoba_pryw").Value
            wynik.Wartosc = cmd.Parameters("@wartosc").Value
            wynik.KwotaCOD = cmd.Parameters("@kwota_cod").Value
            wynik.Typ = cmd.Parameters("@typ").Value
        End If
        cnn.Close()
        Return wynik
    End Function



    <WebMethod()> _
    Public Function UserDaneDpdWczytaj(ByVal sesja As Byte(), _
        ByVal user_edytowany_id As Integer) As UserDaneDpdWczytajWynik
        Dim wynik As New UserDaneDpdWczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            cnn.Close()
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_USER_DANE_DPD_WCZYTAJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@user_edytowany_id", user_edytowany_id)
        cmd.Parameters.Add("@Dok_Zwrotne_Visible", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@Dok_Zwrotne_Enable", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@Prz_Zwrotne_Visible", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@Prz_Zwrotne_Enable", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@Osob_Pryw_Visible", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@Osob_Pryw_Enable", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@Wartosc_Visible", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@Wartosc_Enable", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@COD_Visible", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@COD_Enable", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@Dost_Gw_Visible", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@Dost_Gw_Enable", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            Return wynik
        End Try


        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status > -1 Then
            wynik.Dok_Zwrotne_Visible = IIf(IsDBNull(cmd.Parameters("@Dok_Zwrotne_Visible").Value), 1, cmd.Parameters("@Dok_Zwrotne_Visible").Value)
            wynik.Dok_Zwrotne_Enable = IIf(IsDBNull(cmd.Parameters("@Dok_Zwrotne_Enable").Value), 0, cmd.Parameters("@Dok_Zwrotne_Enable").Value)
            wynik.Prz_Zwrotne_Visible = IIf(IsDBNull(cmd.Parameters("@Prz_Zwrotne_Visible").Value), 1, cmd.Parameters("@Prz_Zwrotne_Visible").Value)
            wynik.Prz_Zwrotne_Enable = IIf(IsDBNull(cmd.Parameters("@Prz_Zwrotne_Enable").Value), 0, cmd.Parameters("@Prz_Zwrotne_Enable").Value)
            wynik.Osob_Pryw_Visible = IIf(IsDBNull(cmd.Parameters("@Osob_Pryw_Visible").Value), 0, cmd.Parameters("@Osob_Pryw_Visible").Value)
            wynik.Osob_Pryw_Enable = IIf(IsDBNull(cmd.Parameters("@Osob_Pryw_Enable").Value), 0, cmd.Parameters("@Osob_Pryw_Enable").Value)
            wynik.Wartosc_Visible = IIf(IsDBNull(cmd.Parameters("@Wartosc_Visible").Value), 1, cmd.Parameters("@Wartosc_Visible").Value)
            wynik.Wartosc_Enable = IIf(IsDBNull(cmd.Parameters("@Wartosc_Enable").Value), 0, cmd.Parameters("@Wartosc_Enable").Value)
            wynik.COD_Visible = IIf(IsDBNull(cmd.Parameters("@COD_Visible").Value), 0, cmd.Parameters("@COD_Visible").Value)
            wynik.COD_Enable = IIf(IsDBNull(cmd.Parameters("@COD_Enable").Value), 0, cmd.Parameters("@COD_Enable").Value)
            wynik.Dost_Gw_Visible = IIf(IsDBNull(cmd.Parameters("@Dost_Gw_Visible").Value), 1, cmd.Parameters("@Dost_Gw_Visible").Value)
            wynik.Dost_Gw_Enable = IIf(IsDBNull(cmd.Parameters("@Dost_Gw_Enable").Value), 0, cmd.Parameters("@Dost_Gw_Enable").Value)

        End If
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function UserDaneDpdZapisz(ByVal sesja As Byte(), _
        ByVal user_edytowany_id As Integer, ByVal Dok_Zwrotne_Visible As Integer, ByVal Dok_Zwrotne_Enable As Integer, ByVal Prz_Zwrotne_Visible As Integer, _
        ByVal Prz_Zwrotne_Enable As Integer, ByVal Osob_Pryw_Visible As Integer, ByVal Osob_Pryw_Enable As Integer, ByVal Wartosc_Visible As Integer, _
        ByVal Wartosc_Enable As Integer, ByVal COD_Visible As Integer, ByVal COD_Enable As Integer, ByVal Dost_Gw_Visible As Integer, ByVal Dost_Gw_Enable As Integer) As UserDaneDpdZapiszWynik
        Dim wynik As New UserDaneDpdZapiszWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            cnn.Close()
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_USER_DANE_DPD_ZAPISZ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@user_edytowany_id", user_edytowany_id)
        cmd.Parameters.AddWithValue("@Dok_Zwrotne_Visible", Dok_Zwrotne_Visible)
        cmd.Parameters.AddWithValue("@Dok_Zwrotne_Enable", Dok_Zwrotne_Enable)
        cmd.Parameters.AddWithValue("@Prz_Zwrotne_Visible", Prz_Zwrotne_Visible)
        cmd.Parameters.AddWithValue("@Prz_Zwrotne_Enable", Prz_Zwrotne_Enable)
        cmd.Parameters.AddWithValue("@Osob_Pryw_Visible", Osob_Pryw_Visible)
        cmd.Parameters.AddWithValue("@Osob_Pryw_Enable", Osob_Pryw_Enable)
        cmd.Parameters.AddWithValue("@Wartosc_Visible", Wartosc_Visible)
        cmd.Parameters.AddWithValue("@Wartosc_Enable", Wartosc_Enable)
        cmd.Parameters.AddWithValue("@COD_Visible", COD_Visible)
        cmd.Parameters.AddWithValue("@COD_Enable", COD_Enable)
        cmd.Parameters.AddWithValue("@Dost_Gw_Visible", Dost_Gw_Visible)
        cmd.Parameters.AddWithValue("@Dost_Gw_Enable", Dost_Gw_Enable)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            Return wynik
        End Try


        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        cnn.Close()
        Return wynik
    End Function

#End Region

#Region "Raporty"
    <WebMethod()> _
    Public Function RaportAwiz(ByVal sesja As Byte(), ByVal data_od As Date, ByVal data_do As Date) As RaportAwizWynik
        Dim wynik As New RaportAwizWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString

            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("RaportAwiz:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_RAPORT_AWIZ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandTimeout = 600
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@data_start", data_od)
        cmd.Parameters.AddWithValue("@data_koniec", data_do)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("RaportAwiz:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If

        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function RaportHistoriiMaterialuWczytaj(ByVal sesja As Byte(), ByVal xml_sku As String) As RaportHistoriiMaterialuWczytajWynik
        Dim wynik As New RaportHistoriiMaterialuWczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("RaportHistoriiMaterialuWczytaj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_RAPORT_HISTORII_MATERIALU", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandTimeout = 240000
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@xml_sku", xml_sku)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("RaportHistoriiMaterialuWczytaj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If

        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function RaportLimityWczytaj(ByVal sesja As Byte(), ByVal data_od As Date, ByVal data_do As Date) As RaportLimityWczytajWynik
        Dim wynik As New RaportLimityWczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("RaportLimityWczytaj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę SP_RAPORT_LIMITY_WCZYTAJ
        Dim cmd As New SqlClient.SqlCommand("SP_RAPORT_LIMITY_WCZYTAJ", cnn)
        cmd.CommandTimeout = 600
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@data_od", data_od)
        cmd.Parameters.AddWithValue("@data_do", data_do)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("RaportLimityWczytaj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If

        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function RaportPodzialySkuGrupaObdzielanaGeneruj(ByVal sesja As Byte(), _
                                                            ByVal sku As String, _
                                                            ByVal grupa_obdzielana As String) As RaportPodzialySkuGrupaObdzielanaGenerujWynik
        Dim wynik As New RaportPodzialySkuGrupaObdzielanaGenerujWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("RaportPodzialySkuGrupaObdzielanaGeneruj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_RAPORT_PODZIALY_SKU_GRUPA_OBDZIELANA_GENERUJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@sku", sku)
        cmd.Parameters.AddWithValue("@grupa_obdzielana", grupa_obdzielana)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("RaportPodzialySkuGrupaObdzielanaGeneruj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function RaportPodzialySkuGrupaObdzielanaWczytaj(ByVal sesja As Byte()) As RaportPodzialySkuGrupaObdzielanaWczytajWynik
        Dim wynik As New RaportPodzialySkuGrupaObdzielanaWczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("RaportPodzialySkuGrupaObdzielanaWczytaj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_RAPORT_PODZIALY_SKU_GRUPA_OBDZIELANA_WCZYTAJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("RaportPodzialySkuGrupaObdzielanaWczytaj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function RaportPodzialyWczytaj(ByVal sesja As Byte(), ByVal data_od As Date, ByVal data_do As Date) As RaportPodzialyWczytajWynik
        Dim wynik As New RaportPodzialyWczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("RaportPodzialyWczytaj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_RAPORT_PODZIALY_TOWARU", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandTimeout = 600
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@data_od_in", data_od)
        cmd.Parameters.AddWithValue("@data_do_in", data_do)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("RaportPodzialyWczytaj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If

        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function RaportPrzyjeciaPoDatachWczytaj(ByVal sesja As Byte(), ByVal data_od As String, ByVal data_do As String) As RaportPrzyjeciaPoDatachWczytajWynik

        Dim wynik As New RaportPrzyjeciaPoDatachWczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("RaportPrzyjeciaPoDatachWczytaj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_RAPORT_PRZYJEC", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@data_start", data_od)
        cmd.Parameters.AddWithValue("@data_koniec", data_do)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("RaportPrzyjeciaPoDatachWczytaj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If

        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function RaportRentownosciWczytaj(ByVal sesja As Byte()) As RaportRentownosciWczytajWynik
        Dim wynik As New RaportRentownosciWczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("RaportRentownosciWczytaj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_RAPORT_RENTOWNOSCI", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandTimeout = 600
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("RaportRentownosciWczytaj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If

        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function RaportRozchodyWczytaj(ByVal sesja As Byte(), ByVal data_od As Date, ByVal data_do As Date) As RaportRozchodyWczytajWynik
        Dim wynik As New RaportRozchodyWczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("RaportRozchodyWczytaj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_RAPORT_ROZCHODY_WCZYTAJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandTimeout = 600
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@data_od", data_od)
        cmd.Parameters.AddWithValue("@data_do", data_do)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("RaportRozchodyWczytaj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If

        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function RaportStanWczytaj(ByVal sesja As Byte()) As RaportStanWczytajWynik
        Dim wynik As New RaportStanWczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("RaportStanWczytaj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_RAPORT_STAN_WCZYTAJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandTimeout = 600
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("RaportStanWczytaj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If

        cnn.Close()
        Return wynik
    End Function


    <WebMethod()> _
    Public Function RaportWejsciaWyjscia(ByVal sesja As Byte(), ByVal data_start As String, _
                                         ByVal data_koniec As String) As RaportWejsciaWyjsciaWynik

        Dim wynik As New RaportWejsciaWyjsciaWynik

        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            cnn.Close()
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_RAPORT_WEJSCIA_WYJSCIA", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("DATA_START", data_start)
        cmd.Parameters.AddWithValue("DATA_KONIEC", data_koniec)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function RaportZamowieniaDaneUzytkownika(ByVal sesja As Byte(), ByVal data_od As Date, _
                                                    ByVal data_do As Date) As RaportZamowieniaDaneUzytkownikaWynik
        Dim wynik As New RaportZamowieniaDaneUzytkownikaWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("RaportZamowieniaDaneUzytkownika:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_RAPORT_ZAMOWIENIA_DANE_UZYTKOWNIKA", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandTimeout = 600
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@data_od", data_od)
        cmd.Parameters.AddWithValue("@data_do", data_do)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("RaportZamowieniaDaneUzytkownika:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If

        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function RaportZamowieniaPozycjeWczytaj(ByVal sesja As Byte(), ByVal data_od As Date, ByVal data_do As Date) As RaportZamowieniaPozycjeWczytajWynik
        Dim wynik As New RaportZamowieniaPozycjeWczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("RaportZamowieniaPozycjeWczytaj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_RAPORT_ZAMOWIENIA_POZYCJE_WCZYTAJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandTimeout = 600
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@data_od", data_od)
        cmd.Parameters.AddWithValue("@data_do", data_do)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("RaportZamowieniaPozycjeWczytaj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If

        cnn.Close()
        Return wynik
    End Function


    <WebMethod()> _
    Public Function RaportZamowieniaWczytaj(ByVal sesja As Byte(), ByVal data_od As Date, ByVal data_do As Date) As RaportZamowieniaWczytajWynik
        Dim wynik As New RaportZamowieniaWczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("RaportZamowieniaWczytaj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_RAPORT_ZAMOWIENIA_WCZYTAJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandTimeout = 600
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@data_od", data_od)
        cmd.Parameters.AddWithValue("@data_do", data_do)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("RaportZamowieniaWczytaj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If

        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function RaportPaletodniWczytaj(ByVal sesja As Byte(), ByVal data_od As Date, ByVal data_do As Date) As RaportPaletodniWczytajWynik
        Dim wynik As New RaportPaletodniWczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("RaportPaletodniWczytaj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_RAPORT_PALETODNI_WCZYTAJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandTimeout = 600
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@data_pocz", data_od)
        cmd.Parameters.AddWithValue("@data_konc", data_do)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("RaportPaletodniWczytaj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If

        cnn.Close()
        Return wynik
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


#Region "Metody dodatkowe dla MVC Prospekt"

    <WebMethod()> _
    Public Function StatystykiWczytaj(ByVal sesja As Byte()) As StatystykiWczytajWynik
        Dim wynik As New StatystykiWczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("ProduktyLimitLogistycznyListaWczytaj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("UP_DASHBOARD_STATYSTYKA", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandTimeout = 600
        cmd.Parameters.AddWithValue("@sesja", sesja)

        cmd.Parameters.Add("@ILOSC_SKU", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@ILOSC_KOSZYK", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@ILOSC_ZAMOWIEN", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@ILOSC_ADRESOW", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@KOMUNIKAT", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output


        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("StatystykiWczytajWynik:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.IloscAdresow = 0
        wynik.IloscProduktow = 0
        wynik.IloscWKoszyku = 0
        wynik.IloscZamowien = 0
        wynik.textKomunikat = ""

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.IloscProduktow = cmd.Parameters("@ILOSC_SKU").Value
            wynik.IloscWKoszyku = cmd.Parameters("@ILOSC_KOSZYK").Value
            wynik.IloscZamowien = cmd.Parameters("@ILOSC_ZAMOWIEN").Value
            wynik.IloscAdresow = cmd.Parameters("@ILOSC_ADRESOW").Value
            wynik.textKomunikat = cmd.Parameters("@KOMUNIKAT").Value
            wynik.dane = ds
        End If
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function PobierzIdMagazynDomyslny(ByVal sesja As Byte()) As MagazynDomyslnyWynik
        Dim wynik As New MagazynDomyslnyWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("Stan:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("UP_MAGAZYN_DOMYSLNY_ID_POBIERZ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandTimeout = 600
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.Add("@magazyn_id", SqlDbType.Int).Direction = ParameterDirection.InputOutput
        cmd.Parameters("@magazyn_id").Value = DBNull.Value
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("Stan:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.magazyn_id = IIf(IsDBNull(cmd.Parameters("@magazyn_id").Value), -1, cmd.Parameters("@magazyn_id").Value)
        End If

        If wynik.status_opis = Nothing Then
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z serwerem SQL"
        End If

        cnn.Close()
        Return wynik
    End Function

    <WebMethod(Description:="Pobiera listę możliwych statusów zamówienia")> _
    Public Function ZamowienieStatusyLista(ByVal sesja As Byte()) As ZamowienieStatusyListaWynik

        Dim wynik As New ZamowienieStatusyListaWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("PlikExcelZakladanieSkuZapisz:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("UP_ZAMOWIENIE_STATUSY_LISTA_WCZYTAJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)

        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("ZamowienieStatusyLista:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        wynik.dane = ds

        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function ParametrWartoscPobierz(ByVal sesja As Byte(), ByVal nazwaParametru As String) As ParametrWartoscPobierzWynik
        Dim wynik As New ParametrWartoscPobierzWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("ProduktyLimitLogistycznyListaWczytaj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("UP_PARAMETR_WARTOSC_POBIERZ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandTimeout = 600
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@PARAMETR_NAZWA_IN", nazwaParametru)

        cmd.Parameters.Add("@PARAMETR_TYP_ID_OUT", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@PARAMETR_WARTOSC_OUT", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("StatystykiWczytajWynik:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.wartosc = ""
        wynik.typParametruID = -1

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.wartosc = cmd.Parameters("@PARAMETR_WARTOSC_OUT").Value
            wynik.typParametruID = cmd.Parameters("@PARAMETR_TYP_ID_OUT").Value
            wynik.dane = ds
        End If
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function PobierzDaneUzytkownika(ByVal sesja As Byte(), ByVal login As String) As UzytkownikDaneWynik
        Dim wynik As New UzytkownikDaneWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("Stan:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("UP_UZYTKOWNIK_DANE_POBIERZ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandTimeout = 600
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@USER_LOGIN", login)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("Stan:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value

        If wynik.status <> -1 Then
            wynik.dane = ds
        End If

        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function StanMagazynSkuGrupa(ByVal sesja As Byte(), ByVal magazyn_id As Integer, ByVal dane As DataSet) As StanMagazynSkuGrupaWynik
        Dim wynik As New StanMagazynSkuGrupaWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("StanSku:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("UP_STAN_SKU_MAGAZYN_GRUPA", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandTimeout = 600
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@magazyn_id", IIf(magazyn_id < 0, DBNull.Value, magazyn_id))
        'tworzymy XML sku
        Dim strXml As New StringBuilder
        For Each wiersz As DataRow In dane.Tables(0).Rows
            'strXml.Append(String.Format("<row><sku_id>{0}</sku_id><grupa_id>{1}</grupa_id></row>", wiersz.Item("sku_id"), wiersz.Item("grupa_id")))
            strXml.Append("<row sku_id=""" & wiersz.Item("sku_id") & """ grupa_id=""" & wiersz.Item("grupa_id") & """/>")
        Next
        cmd.Parameters.AddWithValue("@xml_sku", strXml.ToString.Replace("&", "&amp;"))
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("StanSku:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If
        cnn.Close()
        Return wynik
    End Function




  <WebMethod()> _
    Public Function UserResetHaslaGenerujToken(ByVal login As String) As UserResetHaslaGenerujTokenWynik
        Dim wynik As New UserResetHaslaGenerujTokenWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("Stan:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("UP_UZYTKOWNIK_RESET_HASLA_TOKEN", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandTimeout = 600
        'cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@USER_LOGIN", login)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("Stan:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value

        If wynik.status <> -1 Then
            wynik.dane = ds
        End If

        cnn.Close()
        Return wynik
    End Function
    <WebMethod()> _
    Public Function UserResetHaslaUstawNoweHaslo(ByVal token As String, ByVal noweHaslo As String) As UserResetHaslaUstawNoweHasloWynik
        Dim wynik As New UserResetHaslaUstawNoweHasloWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("Stan:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("UP_UZYTKOWNIK_RESET_HASLA_ZAPISZ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandTimeout = 600
        'cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@PASSWORD_RESET_TOKEN", token)
        cmd.Parameters.AddWithValue("@NOWE_HASLO", noweHaslo)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("Stan:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value

        If wynik.status <> -1 Then
            wynik.dane = ds
        End If

        cnn.Close()
        Return wynik
    End Function


#End Region

<WebMethod()> _
    Public Function AdresyKopiujFiltryWczytaj(ByVal sesja As Byte()) As AdresyKopiujFiltryWczytajWynik
        Dim wynik As New AdresyKopiujFiltryWczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("AdresyKopiujFiltryWczytaj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_ADRESY_KOPIUJ_FILTRY_WCZYTAJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@nazwa_szablonu", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("AdresyKopiujFiltryWczytaj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.nazwaSzablonu = cmd.Parameters("@nazwa_szablonu").Value
        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function AdresKopiujUserWczytaj(ByVal sesja As Byte(), ByVal userID As Integer) As AdresKopiujUserWczytajWynik
        Dim wynik As New AdresKopiujUserWczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("AdresKopiujUserWczytaj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_ADRESY_KOPIUJ_USER_WCZYTAJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandTimeout = 240000
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@UZYTKOWNIK_ID", userID)
        cmd.Parameters.Add("@ILOSC_ZDUBLOWANYCH", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("AdresKopiujUserWczytaj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.iloscZdublowanych = cmd.Parameters("@ilosc_zdublowanych").Value
        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If

        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function AdresySkopiowaneZapisz(ByVal sesja As Byte(), _
                                           ByVal ds_dane As DataSet) As AdresySkopiowaneZapiszWynik
        Dim wynik As New AdresySkopiowaneZapiszWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Database Connection Error: " & ex.Message & vbNewLine & kontaktIt
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_ADRESY_SKOPIOWANE_ZAPISZ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.Add("@dane", SqlDbType.Structured)
        cmd.Parameters("@dane").TypeName = "TVP_ADRESY_DO_SKOPIOWANIA"
        cmd.Parameters("@dane").Value = ds_dane.Tables(0)
        cmd.Parameters.Add("@ilosc_zdublowanych", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@ilosc_wierszy", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        cmd.ExecuteNonQuery()

        wynik.iloscDubli = cmd.Parameters("@ilosc_zdublowanych").Value
        wynik.iloscAdresowSkopiowanych = cmd.Parameters("@ilosc_wierszy").Value
        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function RaportAwizaDostawaFiltrWczytaj(ByVal sesja As Byte()) As RaportAwizaDostawaFiltrWczytajWynik
        Dim wynik As New RaportAwizaDostawaFiltrWczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("RaportAwizaDostawaFiltrWczytaj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_RAPORT_AWIZA_DOSTAWA_FILTR_WCZYTAJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandTimeout = 600
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@minimalna_data_dostawy", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("RaportAwizaDostawaFiltrWczytaj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.minimalnaDataAwiza = cmd.Parameters("@minimalna_data_dostawy").Value
        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function RaportAwizDostawaGeneruj(ByVal sesja As Byte(), ByVal filtrAwizo As String, ByVal filtrNumerPO As String, ByVal bStatusAll As Boolean, ByVal xmlStatus As String, _
                                ByVal filtrQguarZa As String, ByVal filtrQguarDostawa As String, ByVal filtrQguarPz As String, ByVal planowana_data_od As Date, ByVal planowana_data_do As Date) As RaportAwizDostawaGenerujWynik
        Dim wynik As New RaportAwizDostawaGenerujWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString

            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("RaportAwizDostawaGeneruj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_RAPORT_AWIZ_DOSTAWA_GENERUJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandTimeout = 600
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@filtr_awizo", filtrAwizo)
        cmd.Parameters.AddWithValue("@FILTR_NR_PO", filtrNumerPO)
        cmd.Parameters.AddWithValue("@b_status_all", bStatusAll)
        cmd.Parameters.AddWithValue("@xml_status", xmlStatus)
        cmd.Parameters.AddWithValue("@filtr_qguar_za", filtrQguarZa)
        cmd.Parameters.AddWithValue("@filtr_qguar_dostawa", filtrQguarDostawa)
        cmd.Parameters.AddWithValue("@filtr_qguar_pz", filtrQguarPz)
        cmd.Parameters.AddWithValue("@planowana_data_od", planowana_data_od)
        cmd.Parameters.AddWithValue("@planowana_data_do", planowana_data_do)
        cmd.Parameters.Add("@ile_rekordow", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("RaportAwizDostawaGeneruj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
            wynik.ileRekordow = cmd.Parameters("@ile_rekordow").Value
        End If

        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function KodyPocztoweWieleSprawdz(ByVal sesja As Byte(), ByVal xmlKody As String) As KodyPocztoweWieleSprawdzWynik
        Dim wynik As New KodyPocztoweWieleSprawdzWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("KodyPocztoweWieleSprawdz:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_KOD_POCZTOWY_SPRAWDZ_KILKA", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@xml_kody", xmlKody)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("KodyPocztoweWieleSprawdz:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function SlownikWartoscWczytaj(ByVal sesja As Byte(), ByVal slownikId As Integer) As SlownikWartoscWczytajWynik
        Dim wynik As New SlownikWartoscWczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("SlownikWartoscWczytaj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_SLOWNIK_WARTOSC_WCZYTAJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@slownik_id", slownikId)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("SlownikWartoscWczytaj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try


        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If

        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function SlownikListaWczytaj(ByVal sesja As Byte()) As SlownikListaWczytajWynik
        Dim wynik As New SlownikListaWczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("SlownikListaWczytaj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_SLOWNIK_LISTA_WCZYTAJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@nazwa_szablonu", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("SlownikListaWczytaj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.nazwaSzablonu = IIf(IsDBNull(cmd.Parameters("@nazwa_szablonu").Value), "", cmd.Parameters("@nazwa_szablonu").Value)
        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If

        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function SlownikWartoscUsun(ByVal sesja As Byte(), ByVal slownikId As Integer, ByVal nazwa As String) As SlownikWartoscUsunWynik
        Dim wynik As New SlownikWartoscUsunWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("SlownikWartoscUsun:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_SLOWNIK_WARTOSCI_USUN", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@slownik_id", slownikId)
        cmd.Parameters.AddWithValue("@nazwa", nazwa)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("SlownikWartoscUsun:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
     
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function SlownikWartoscEdytuj(ByVal sesja As Byte(), ByVal slownikId As Integer, ByVal nowaNazwa As String, ByVal staraNazwa As String) As SlownikWartoscEdytujWynik
        Dim wynik As New SlownikWartoscEdytujWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("SlownikWartoscEdytuj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_SLOWNIK_WARTOSCI_EDYTUJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@slownik_id", slownikId)
        cmd.Parameters.AddWithValue("@nowa_nazwa", nowaNazwa)
        cmd.Parameters.AddWithValue("@stara_nazwa", staraNazwa)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("SlownikWartoscEdytuj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value

        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function SlownikWartoscKilkaDodaj(ByVal sesja As Byte(), ByVal slownikId As Integer, ByVal dane As DataSet) As SlownikWartoscKilkaDodajWynik
        Dim wynik As New SlownikWartoscKilkaDodajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("SlownikWartoscKilkaDodaj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_SLOWNIK_WARTOSCI_KILKA_DODAJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure

        Dim strXmlPozycje As New StringBuilder
        For Each wiersz As DataRow In dane.Tables(0).Rows
            strXmlPozycje.Append("<row wartosc=""" & wiersz.Item("wartosc").ToString().Replace("&", "&amp;").Replace("'", "&apos;").Replace("""", "&quot;") & """ />")
        Next

        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@slownik_id", slownikId)
        cmd.Parameters.AddWithValue("@xml_nazwa", strXmlPozycje.ToString)

        cmd.Parameters.Add("@ILOSC_WARTOSCI_DODANYCH", SqlDbType.Int).Direction = ParameterDirection.Output

        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        
        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("SlownikWartoscKilkaDodaj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.iloscDodana = cmd.Parameters("@ILOSC_WARTOSCI_DODANYCH").Value
        End If

        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function SkuUsunWczytaj(ByVal sesja As Byte(), ByVal sku As String) As SkuUsunWczytajWynik
        Dim wynik As New SkuUsunWczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            cnn.Close()
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_SKU_USUN_WCZYTAJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@sku", sku)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function SkuUsun(ByVal sesja As Byte(), ByVal sku As String) As SkuUsunWynik
        Dim wynik As New SkuUsunWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            cnn.Close()
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_SKU_USUN", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@sku", sku)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        cnn.Close()
        Return wynik
    End Function


#Region "Zamowienia_INV"
    <WebMethod()> _
    Public Function ZamowienieINVWczytaj(ByVal sesja As Byte(), ByVal zamowienie_id As Integer, _
        ByVal magazyn_id As Integer, ByVal blokada_id As Integer) As ZamowienieINVWczytajWynik
        Dim wynik As New ZamowienieINVWczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("ZamowienieINVWczytaj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_ZAMOWIENIE_INV_WCZYTAJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.Add("@zamowienie_id", SqlDbType.Int).Direction = ParameterDirection.InputOutput
        cmd.Parameters("@zamowienie_id").Value = IIf(zamowienie_id < 0, DBNull.Value, zamowienie_id)
        cmd.Parameters.Add("@magazyn_id", SqlDbType.Int).Direction = ParameterDirection.InputOutput
        cmd.Parameters("@magazyn_id").Value = IIf(magazyn_id < 0, DBNull.Value, magazyn_id)
        cmd.Parameters.Add("@blokada_id", SqlDbType.Int).Direction = ParameterDirection.InputOutput
        cmd.Parameters("@blokada_id").Value = IIf(blokada_id < 0, DBNull.Value, blokada_id)
        cmd.Parameters.Add("@wlasciciel_nazwa", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@tryb_pracy", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@zamowienie_status", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@zamowienie_status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@typ_zamowienia", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@uwagi", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("ZamowienieINVWczytaj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.zamowienie_id = IIf(IsDBNull(cmd.Parameters("@zamowienie_id").Value), -1, cmd.Parameters("@zamowienie_id").Value)
            wynik.magazyn_id = IIf(IsDBNull(cmd.Parameters("@magazyn_id").Value), -1, cmd.Parameters("@magazyn_id").Value)
            wynik.blokada_id = IIf(IsDBNull(cmd.Parameters("@blokada_id").Value), -1, cmd.Parameters("@blokada_id").Value)
            wynik.wlasciciel_nazwa = IIf(IsDBNull(cmd.Parameters("@wlasciciel_nazwa").Value), "", cmd.Parameters("@wlasciciel_nazwa").Value)
            wynik.tryb_pracy = cmd.Parameters("@tryb_pracy").Value
            wynik.zamowienie_status = IIf(IsDBNull(cmd.Parameters("@zamowienie_status").Value), "", cmd.Parameters("@zamowienie_status").Value)
            wynik.zamowienie_status_opis = IIf(IsDBNull(cmd.Parameters("@zamowienie_status_opis").Value), "", cmd.Parameters("@zamowienie_status_opis").Value)
            wynik.typ_zamowienia = cmd.Parameters("@typ_zamowienia").Value
            wynik.uwagi = IIf(IsDBNull(cmd.Parameters("@uwagi").Value), "", cmd.Parameters("@uwagi").Value)
            wynik.dane = ds
        End If
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function KoszykINVZapisz(ByVal sesja As Byte(), ByVal blokada_id As Integer, _
        ByVal magazyn_id As Integer, ByVal uwagi As String, _
        ByVal typ_zamowienia As Integer, ByVal dane As DataSet) As KoszykINVZapiszWynik
        Dim wynik As New KoszykINVZapiszWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("KoszykINVZapisz:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_KOSZYK_INV_GRUPA_ZAPISZ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@blokada_id", IIf(blokada_id < 0, DBNull.Value, blokada_id))
        'tworzymy XML pozycje
        Dim strXmlPozycje As New StringBuilder
        For Each wiersz As DataRow In dane.Tables(0).Rows
            strXmlPozycje.Append("<row sku_id=""" & wiersz.Item("sku_id") & """ grupa=""" & IIf(dane.Tables(0).Columns.Contains("grupa"), wiersz.Item("grupa"), "BRAK") & """ ilosc=""" & wiersz.Item("ilosc") & """ />")
        Next
        cmd.Parameters.AddWithValue("@xml_sku", strXmlPozycje.ToString.Replace("&", "&amp;"))
        cmd.Parameters.AddWithValue("@magazyn_id", IIf(magazyn_id < 0, DBNull.Value, magazyn_id))
        cmd.Parameters.AddWithValue("@uwagi", uwagi)
        cmd.Parameters.AddWithValue("@zamowienie_typ_id", typ_zamowienia)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("KoszykINVZapisz:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function KoszykINVZatwierdz(ByVal sesja As Byte(), ByVal blokada_id As Integer) As KoszykINVZatwierdzWynik
        Dim wynik As New KoszykINVZatwierdzWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("KoszykINVZatwierdz:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_KOSZYK_INV_GRUPA_ZATWIERDZ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@blokada_id", IIf(blokada_id < 0, DBNull.Value, blokada_id))
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output

        Try
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("KoszykINVZatwierdz:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function StanSkuGrupaINV(ByVal sesja As Byte(), ByVal magazyn_id As Integer, ByVal xml_sku_wg_id As String, ByVal xml_sku_wg_nazw As String) As StanSkuGrupaINVWynik
        Dim wynik As New StanSkuGrupaINVWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("StanSkuGrupaINV:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_STAN_SKU_GRUPA_INV", cnn)
        cmd.CommandTimeout = 600
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@magazyn_id", IIf(magazyn_id < 0, DBNull.Value, magazyn_id))
        ''tworzymy XML sku
        'Dim strXml As New StringBuilder
        'For Each wiersz As DataRow In dane.Tables(0).Rows
        '    strXml.Append("<row sku_id=""" & wiersz.Item("sku_id") & """ grupa_id=""" & wiersz.Item("grupa_id") & """/>")
        'Next
        cmd.Parameters.AddWithValue("@XML_SKU_WG_ID", xml_sku_wg_id)

        'strXml = New StringBuilder
        'For Each wiersz As DataRow In dane.Tables(0).Rows
        '    strXml.Append("<row sku=""" & wiersz.Item("sku").ToString.Replace("&", "&amp;").Replace("'", "&apos;").Replace("""", "&quot;") & _
        '            """ grupa_nazwa=""" & wiersz.Item("grupa").ToString.Replace("&", "&amp;").Replace("'", "&apos;").Replace("""", "&quot;") & """ ilosc=""" & wiersz.Item("ilosc") & """/>")
        'Next
        cmd.Parameters.AddWithValue("@XML_SKU_WG_NAZW", xml_sku_wg_nazw)

        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("StanSkuGrupaINV:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If
        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function StanINV(ByVal sesja As Byte(), ByVal magazyn_id As Integer, ByVal marka As String, ByVal branza As String, ByVal sku As String, ByVal nazwa As String) As StanWynik
        Dim wynik As New StanWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("StanINV:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        Dim cmd As New SqlClient.SqlCommand("SP_STAN_INV", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandTimeout = 600
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@marka", marka)
        cmd.Parameters.AddWithValue("@branza", branza)
        cmd.Parameters.AddWithValue("@sku", sku)
        cmd.Parameters.AddWithValue("@nazwa", nazwa)
        cmd.Parameters.Add("@magazyn_id", SqlDbType.Int).Direction = ParameterDirection.InputOutput
        cmd.Parameters("@magazyn_id").Value = IIf(magazyn_id < 0, DBNull.Value, magazyn_id)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@opis_rozszerzony", SqlDbType.Int).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("StanINV:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            'wynik.iloscStron = cmd.Parameters("@ilosc_stron").Value
            wynik.dane = ds
            wynik.magazyn_id = IIf(IsDBNull(cmd.Parameters("@magazyn_id").Value), -1, cmd.Parameters("@magazyn_id").Value)
        End If
        cnn.Close()
        Return wynik
    End Function

#End Region



    <WebMethod()> _
    Public Function RaportZamowieniaRozliczenie(ByVal sesja As Byte(), ByVal data_od As Date, ByVal data_do As Date) As RaportZamowieniaRozliczenieWynik

        Dim wynik As New RaportZamowieniaRozliczenieWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("RaportZamowieniaRozliczenie:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę
        Dim cmd As New SqlClient.SqlCommand("SP_RAPORT_ROZLICZENIOWY_ZAMOWIENIA", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandTimeout = 240
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@data_od", data_od)
        cmd.Parameters.AddWithValue("@data_do", data_do)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("RaportZamowieniaRozliczenie:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If

        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function RaportTerminowosciFedExWczytaj(ByVal sesja As Byte(), ByVal data_od As Date, ByVal data_do As Date) As RaportTerminowosciFedExWczytajWynik
        Dim wynik As New RaportTerminowosciFedExWczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("RaportTerminowosciFedExWczytaj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_RAPORT_TERMINOWOSCI_FEDEX", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandTimeout = 240000
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@data_od", data_od)
        cmd.Parameters.AddWithValue("@data_do", data_do)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("RaportTerminowosciFedExWczytaj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If

        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function RaportStanBezGrupWczytaj(ByVal sesja As Byte()) As RaportStanBezGrupWczytajWynik
        Dim wynik As New RaportStanBezGrupWczytajWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("RaportStanWczytaj:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_RAPORT_STAN_BEZ_GRUP_WCZYTAJ", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandTimeout = 600
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("RaportStanWczytaj:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If

        cnn.Close()
        Return wynik
    End Function

    <WebMethod()> _
    Public Function RaportPodzialyZamowieniaPerUser(ByVal sesja As Byte(), ByVal uzytkownik_id As Integer) As RaportPodzialyZamowieniaPerUserWynik
        Dim wynik As New RaportPodzialyZamowieniaPerUserWynik
        Dim cnn As SqlConnection

        'łączymy do bazy
        Try
            cnn = New SqlConnection()
            cnn.ConnectionString = connectionString
            cnn.Open()
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd połączenia do bazy danych: " & ex.Message & vbNewLine & kontaktIt
            logger.Error("RaportPodzialyZamowieniaPerUser:Błąd połączenia do bazy danych: ", ex)
            Return wynik
        End Try

        'wywołujemy procedurę zaloguj
        Dim cmd As New SqlClient.SqlCommand("SP_RAPORT_PODZIALY_ZAMOWIENIA_PER_USER", cnn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandTimeout = 600
        cmd.Parameters.AddWithValue("@sesja", sesja)
        cmd.Parameters.AddWithValue("@uzytkownik_id", uzytkownik_id)
        cmd.Parameters.Add("@status", SqlDbType.Int).Direction = ParameterDirection.Output
        cmd.Parameters.Add("@status_opis", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output
        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet

        Try
            da.Fill(ds)
        Catch ex As Exception
            wynik.status = -1
            wynik.status_opis = "Błąd komunikacji z bazą: " & ex.Message & kontaktIt
            cnn.Close()
            logger.Error("RaportPodzialyZamowieniaPerUser:Błąd komunikacji z bazą: ", ex)
            Return wynik
        End Try

        wynik.status = cmd.Parameters("@status").Value
        wynik.status_opis = cmd.Parameters("@status_opis").Value
        If wynik.status <> -1 Then
            wynik.dane = ds
        End If

        cnn.Close()
        Return wynik
    End Function

End Class




