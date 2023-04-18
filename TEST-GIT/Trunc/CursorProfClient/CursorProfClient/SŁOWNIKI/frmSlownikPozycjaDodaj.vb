Imports System.IO
Imports OfficeOpenXml

Public Class frmSlownikPozycjaDodaj
    Public slownikId As Integer
    Public slownikNazwa As String = ""
    Public bEdycja As Boolean = False
    Public wartoscNazwa As String

    Private Sub btnAnuluj_Click(sender As Object, e As System.EventArgs) Handles btnZamknij.Click
        If bylyZmiany() Then
            Dim odp As MsgBoxResult = MsgBox("Uwaga! Wprowadzono zmiany w tym słowniku. Czy chcesz je zapisać?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1, "Zapisanie zmian")
            If odp = MsgBoxResult.Yes Then
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Exit Sub
            End If
        End If
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmSlownikPozycjaDodaj_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        txtWartosc.Focus()
        Me.Text = slownikNazwa
        If bEdycja = True Then
            txtWartosc.Text = wartoscNazwa
        End If
    End Sub

    Private Sub btnZapisz_Click(sender As Object, e As System.EventArgs) Handles btnZapisz.Click
        If txtWartosc.Text.Length < 1 Then
            MsgBox("Proszę podać wartość do zapisu tego słownika", MsgBoxStyle.Exclamation, "Brak wartości")
            Exit Sub
        End If

        If bylyZmiany() = False Then
            MsgBox("Nie zmieniono nazwy. Zapis do bazy nie był potrzebny.", MsgBoxStyle.Information, Me.Text)
            Exit Sub
        End If

        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Function bylyZmiany() As Boolean
        If txtWartosc.Text <> wartoscNazwa Then
            Return True
        End If

        Return False
    End Function
End Class