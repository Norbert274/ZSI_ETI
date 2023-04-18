Imports System.Drawing.Text
Imports System.Drawing
Public Class ctrEdytorTekstu
    Dim len As Integer
    Private kolor_zaznaczenia As Color = Color.Gold
    Private kolor_bez_zaznacznia As Color = Color.FromName("Control")
    Private rtf_ As String = ""
    Private bLoaded As Boolean = False


    Private Sub btnBold_Click(sender As Object, e As EventArgs) Handles btnBold.Click
        If btnBold.BackColor = kolor_bez_zaznacznia Then
            btnBold.BackColor = kolor_zaznaczenia
        Else
            btnBold.BackColor = kolor_bez_zaznacznia
        End If
        formatuj_text()
    End Sub

    Private Sub btnUnderline_Click(sender As Object, e As EventArgs) Handles btnUnderline.Click
        If btnUnderline.BackColor = kolor_bez_zaznacznia Then
            btnUnderline.BackColor = kolor_zaznaczenia
        Else
            btnUnderline.BackColor = kolor_bez_zaznacznia
        End If
        formatuj_text()
    End Sub

    Private Sub formatuj_text()

        If Not bLoaded Then
            Exit Sub
        End If

        Dim styl As FontStyle

        If btnBold.BackColor = kolor_zaznaczenia Then
            styl = FontStyle.Bold
        End If

        If btnItalic.BackColor = kolor_zaznaczenia Then
            If styl <> FontStyle.Regular Then
                styl += FontStyle.Italic
            Else
                styl = FontStyle.Italic
            End If
        End If

        If btnUnderline.BackColor = kolor_zaznaczenia Then
            If styl <> FontStyle.Regular Then
                styl += FontStyle.Underline
            Else
                styl = FontStyle.Underline
            End If
        End If

        rtbTekst.SelectionFont = New Font(cmbNazwaCzcionki.Text, CInt(cmbRozmiarCzcionki.Text), styl)

    End Sub


    Private Sub cmbNazwaCzcionki_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNazwaCzcionki.SelectedIndexChanged
        formatuj_text()
    End Sub

    Private Sub btnKolor_Click(sender As Object, e As EventArgs) Handles btnKolorCzcionki.Click
        If ColorDialog1.ShowDialog Then
            picKolor.BackColor = ColorDialog1.Color
            rtbTekst.SelectionColor = ColorDialog1.Color
        End If

    End Sub

    Private Sub ctrEdytorTekstu_Load(sender As Object, e As EventArgs) Handles Me.Load

        Dim ZainstalowaneCzcionki As New InstalledFontCollection

        Dim Czcionki() As FontFamily = ZainstalowaneCzcionki.Families()
        Dim czcionki_out As String = ""

        '' czcionki, które nie obsługują stylu Regular, nie będą pokazywane w liście rozwijanej
        Dim Czcionki_do_usuniecia As List(Of String) = New List(Of String)(New String() {"Aharoni", "Berlin Sans FB Demi", _
                                                                                         "Brush Script MT", "Harlow Solid Italic", _
                                                                                         "Magneto", "Monotype Corsiva", _
                                                                                         "Palace Script MT", "Vivaldi"})
        For Each czcionka As FontFamily In Czcionki
            If Not Czcionki_do_usuniecia.Contains(czcionka.Name) Then
                cmbNazwaCzcionki.Items.Add(czcionka.Name)
            End If
        Next

        Dim i As Integer = 5
        For i = 5 To 100
            cmbRozmiarCzcionki.Items.Add(i.ToString)
        Next

        cmbNazwaCzcionki.Text = "Arial"
        cmbRozmiarCzcionki.Text = "12"
        rtbTekst.Rtf = rtf_

        ToolTip1.SetToolTip(btnBold, "Pogrubienie")
        ToolTip1.SetToolTip(btnItalic, "Kursywa")
        ToolTip1.SetToolTip(btnUnderline, "Podkreślenie")
        ToolTip1.SetToolTip(btnKolorCzcionki, "Kolor czcionki")
        ToolTip1.SetToolTip(cmbRozmiarCzcionki, "Rozmiar czcionki")
        ToolTip1.SetToolTip(cmbNazwaCzcionki, "Nazwa czcionki")

        bLoaded = True

       
    End Sub

    Private Sub btnItalic_Click(sender As Object, e As EventArgs) Handles btnItalic.Click
        If btnItalic.BackColor = kolor_bez_zaznacznia Then
            btnItalic.BackColor = kolor_zaznaczenia
        Else
            btnItalic.BackColor = kolor_bez_zaznacznia
        End If
        formatuj_text()
    End Sub

    Private Sub cmbRozmiarCzcionki_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbRozmiarCzcionki.SelectedIndexChanged
        formatuj_text()
    End Sub

  
End Class
