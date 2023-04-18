Imports System.Windows.Forms
Public Class frmBase
    Inherits System.Windows.Forms.Form
    Protected Sub Wlacz(ByVal sesja As Byte())
        Dim table As DataTable = SprawdzUprawnienia(sesja)
        If Not table Is Nothing Then
            For Each ctr As Control In Me.Controls
                If (Not TypeOf ctr Is StatusStrip) And (TypeOf ctr Is MenuStrip Or TypeOf ctr Is ToolStrip) Then
                    If TypeOf ctr Is MenuStrip Then
                        For Each item As ToolStripItem In CType(ctr, MenuStrip).Items
                            Dim rows As DataRow() = table.Select(String.Format("BUTTON_NAZWA = '{0}' AND FORMA_NAZWA IS NULL", item.Name))
                            If rows.Length > 0 Then
                                item.Visible = CBool(rows(0).Item("WLACZ").ToString)
                            End If
                        Next
                    Else
                        For Each item As ToolStripItem In CType(ctr, ToolStrip).Items
                            Dim rows As DataRow() = table.Select(String.Format("BUTTON_NAZWA = '{0}' AND FORMA_NAZWA IS NULL", item.Name))
                            If rows.Length > 0 Then
                                item.Visible = CBool(rows(0).Item("WLACZ").ToString)
                            End If
                        Next
                    End If
                End If

            Next
            Dim ctrRow As Control
            For Each row As DataRow In table.Rows
                ctrRow = WezControl(row.Item("BUTTON_NAZWA"), Me)
                If Not ctrRow Is Nothing Then
                    ctrRow.Visible = CBool(row.Item("WLACZ").ToString)
                End If
            Next
            Dim rowsForm As DataRow() = table.Select(String.Format("FORMA_NAZWA = '{0}'", Me.Name))
            If rowsForm.Length > 0 Then
                For Each ctr As Control In Me.Controls
                    'If (TypeOf ctr Is ctrStan) Then
                    '    WylaczStrip(ctr.Controls("ts"), table)
                    'Else
                    '    WylaczStrip(ctr, table)
                    'End If
                    WylaczStrip(ctr, table)
                Next
                For Each row As DataRow In rowsForm
                    ctrRow = WezControl(row.Item("BUTTON_NAZWA"), Me)
                    If Not ctrRow Is Nothing Then
                        ctrRow.Visible = CBool(row.Item("WLACZ").ToString)
                    End If
                Next
            End If
            If rowsForm.Length > 0 Then
                
                For Each row As DataRow In rowsForm
                    ctrRow = WezControl(row.Item("BUTTON_NAZWA"), Me)
                    If Not ctrRow Is Nothing Then
                        ctrRow.Enabled = CBool(row.Item("WLACZ").ToString)
                    End If
                Next
            End If
        End If
    End Sub
    Private Sub WylaczStrip(ByRef ctr As Control, ByRef table As DataTable)
        If (Not TypeOf ctr Is StatusStrip) And (TypeOf ctr Is MenuStrip Or TypeOf ctr Is ToolStrip) Then
            If TypeOf ctr Is MenuStrip Then
                For Each item As ToolStripItem In CType(ctr, MenuStrip).Items
                    Dim rowsFormBtn As DataRow() = table.Select(String.Format("BUTTON_NAZWA = '{0}' AND FORMA_NAZWA = '{1}'", item.Name, Me.Name))
                    If rowsFormBtn.Length > 0 Then
                        item.Visible = CBool(rowsFormBtn(0).Item("WLACZ").ToString)
                    End If
                Next
            Else
                For Each item As ToolStripItem In CType(ctr, ToolStrip).Items
                    Dim rowsFormBtn As DataRow() = table.Select(String.Format("BUTTON_NAZWA = '{0}' AND FORMA_NAZWA = '{1}'", item.Name, Me.Name))
                    If rowsFormBtn.Length > 0 Then
                        item.Visible = CBool(rowsFormBtn(0).Item("WLACZ").ToString)
                    End If
                Next
            End If
        End If
    End Sub

    Private Function WezControl(ByVal nazwa As String, ByVal ctr As Control) As Control
        Dim ctrWynik As Control = Nothing
        If Not ctr.Controls Is Nothing Then
            If Not ctr.Controls(nazwa) Is Nothing Then
                Return ctr.Controls(nazwa)
            Else
                For Each ctrPodrzedna As Control In ctr.Controls()
                    ctrWynik = WezControl(nazwa, ctrPodrzedna)
                    If Not ctrWynik Is Nothing Then
                        Return ctrWynik
                    End If
                Next
            End If
        End If
        Return ctrWynik
    End Function

    Private Function SprawdzUprawnienia(ByVal sesja As Byte()) As DataTable
        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
Dim ws As New wsCursorProf.CursorService
        System.Net.ServicePointManager.Expect100Continue = False
        ws.Proxy = System.Net.HttpWebRequest.GetSystemWebProxy()
        ws.Proxy.Credentials = CredentialCache.DefaultCredentials
        Dim wsWynik = New wsCursorProf.SprawdzFunkcjeWynik
        Try
            Application.DoEvents()
            wsWynik = ws.SprawdzFunkcje(frmGlowna.sesja)
        Catch ex As Exception
            MsgBox("B³¹d komunikacji z serwerem. " & frmGlowna.kontaktIt & vbNewLine & vbNewLine & "Szczegó³y b³êdu:" & ex.Message, MsgBoxStyle.Critical)
            Return Nothing
            Exit Function
        End Try
        If wsWynik.status < 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Critical)
            Return Nothing
            Exit Function
        ElseIf wsWynik.status > 0 Then
            MsgBox(wsWynik.status_opis, MsgBoxStyle.Exclamation)
        End If
        Return wsWynik.dane.Tables(0)

    End Function
End Class
