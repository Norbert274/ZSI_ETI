Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Windows.Forms.VisualStyles
Imports System.Timers


Public Class ToolTipWithImage
    Inherits Control

    Private _img As Image
    Private _dgv As DataGridView
    Private rowIndex As Integer
    Private colmumnIndex As Integer
    Private Shadows scale As Decimal
    Private xsize As Integer
    Private ysize As Integer
    Private KolumnaNazwa As String
    Public Rozmiar As Integer
    Friend WithEvents timer As System.Windows.Forms.Timer
    Private components As System.ComponentModel.IContainer
    Public PoLewej As Boolean
    Public TABELA_NAZWA As String
    Public KOLUMNA_NAZWA As String
    Public ID_NAZWA As String
    Public Sub New(ByVal dgv As DataGridView, ByVal KolNazwa As String)


        _dgv = dgv
        Rozmiar = 400
        PoLewej = False
        timer = New System.Windows.Forms.Timer()
        timer.Interval = 500
        _dgv.Parent.Controls.Add(Me)
        _dgv.Parent.Controls.SetChildIndex(Me, 0)
        KolumnaNazwa = KolNazwa
        TABELA_NAZWA = "SKU_ZDJECIE"
        KOLUMNA_NAZWA = "ZDJECIE"
        ID_NAZWA = "SKU_ID"
        AddHandler _dgv.CellMouseEnter, AddressOf Me.startTimer
        AddHandler _dgv.CellMouseLeave, AddressOf Me.stopTimer
        AddHandler timer.Tick, New EventHandler(AddressOf ShowTipOn)
        Me.Visible = False
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        If Not _img Is Nothing Then
            e.Graphics.DrawImage(_img, 0, 0, xsize, ysize)
        End If

    End Sub

    Public Sub startTimer(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs)
        'spradzam czy jestem w zakresie komurek
        If e.RowIndex >= 0 And e.RowIndex < _dgv.Rows.Count And e.ColumnIndex > 0 And e.ColumnIndex < _dgv.ColumnCount - 1 Then
            'sprawdzam czy jestem w zadanj komurce
            If _dgv.Columns(e.ColumnIndex).HeaderText = KolumnaNazwa Then
                If timer.Enabled = False Then
                    timer.Start()
                    rowIndex = e.RowIndex
                    colmumnIndex = e.ColumnIndex
                Else
                    If rowIndex <> e.RowIndex Or colmumnIndex <> e.ColumnIndex Then
                        timer.Stop()
                        timer.Start()
                        rowIndex = e.RowIndex
                        colmumnIndex = e.ColumnIndex
                    End If
                End If

            End If
        End If
    End Sub

    Public Sub stopTimer(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs)
        'spradzam czy jestem w zakresie komurek
        If e.RowIndex >= 0 And e.RowIndex < _dgv.Rows.Count And e.ColumnIndex > 0 And e.ColumnIndex < _dgv.ColumnCount - 1 Then
            'sprawdzam czy jestem w zadanj komurce
            If _dgv.Columns(e.ColumnIndex).HeaderText = KolumnaNazwa Then
                If timer.Enabled = True Then
                    timer.Stop()
                    rowIndex = -1
                    colmumnIndex = -1
                End If
                If Me.Visible = True Then
                    Me.Visible = False
                End If
            End If
        End If
    End Sub
    Private Sub ShowTipOn(ByVal sender As Object, ByVal e As EventArgs)
        If Not Me.Visible Then
            timer.Stop()
            setScale()
            If Not _img Is Nothing Then

                Me.Size = New Size(xsize, ysize)

                If PoLewej Then
                    Dim lewy As Integer
                    lewy = _dgv.Left + _dgv.GetColumnDisplayRectangle(colmumnIndex, False).Left - xsize
                    If lewy < 0 Then
                        lewy = _dgv.Left + _dgv.GetColumnDisplayRectangle(colmumnIndex, False).Right
                    End If

                    Me.Left = lewy
                    Me.Top = _dgv.Top + _dgv.GetRowDisplayRectangle(rowIndex, False).Top
                Else
                    Me.Left = _dgv.Left + _dgv.GetColumnDisplayRectangle(colmumnIndex, False).Right
                    Me.Top = _dgv.Top + _dgv.GetRowDisplayRectangle(rowIndex, False).Top
                End If
                If Me.Top + ysize > _dgv.Bottom Then
                    Me.Top = _dgv.Bottom - ysize
                End If

                Me.Visible = True
            End If
        End If
    End Sub
    Private Sub setScale()
        If _dgv.Rows(rowIndex).Cells(colmumnIndex).Value = KolumnaNazwa Then
            System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
            System.Net.ServicePointManager.Expect100Continue = False
            ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
            ws.Proxy.Credentials = CredentialCache.DefaultCredentials
            Dim wsWynik As New wsCursorProf.ZdjecieOdczytajWynik
            Dim arrPicture() As Byte
            Try
                Cursor = Cursors.WaitCursor
                Application.DoEvents()
                wsWynik = ws.ZdjecieOdczytaj(frmGlowna.sesja, CType(_dgv.DataSource, DataView).Item(rowIndex).Item(ID_NAZWA), TABELA_NAZWA, KOLUMNA_NAZWA, ID_NAZWA)
                Cursor = Cursors.Default
            Catch ex As Exception
                Cursor = Cursors.Default
                MsgBox("Błąd komunikacji z serwerem. " & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegóły błędu:" & ex.Message, MsgBoxStyle.Critical)
            End Try


            If wsWynik.status < 0 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical)
            ElseIf wsWynik.status > 0 Then
                MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation)
            End If
            If wsWynik.status = 0 Then
                If wsWynik.dane.Tables.Count > 0 Then
                    If wsWynik.dane.Tables(0).Rows.Count > 0 Then
                        arrPicture = CType(wsWynik.dane.Tables(0).Rows(0).Item(0), Byte())
                        Dim ms As New IO.MemoryStream(arrPicture)
                        _img = Image.FromStream(ms)
                    End If
                End If

            End If
            Dim xscale As Decimal = _img.Width / Rozmiar
            Dim yscale As Decimal = _img.Height / Rozmiar
            If xscale > yscale Then
                scale = xscale
            Else
                scale = yscale
            End If
            xsize = CType(_img.Width / scale, Integer)
            ysize = CType(_img.Height / scale, Integer)
            'End If
        Else
            _img = Nothing
        End If
    End Sub

    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.timer = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'timer
        '
        Me.timer.Interval = 50
        Me.ResumeLayout(False)

    End Sub

    Private Sub timer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles timer.Tick

    End Sub
End Class
