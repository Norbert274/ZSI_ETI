Public Class frmSKUNowy
    Public dtKategorie As DataTable
    Public dtBrand As DataTable
    Public dtJM As DataTable
    Public dtGrupyArtykulowQ As DataTable
    Public skuProponowane As String = ""
    Public cena As Decimal
    Private uiSep As String = Globalization.CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator
    Private czy_byl_klik_dodaj As Boolean
    Private Sub btnAnuluj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnuluj.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btnDodaj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDodaj.Click

        czy_byl_klik_dodaj = True
        If Not walidacjaSKU() Then Exit Sub
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub frmSKUNowy_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        'txtNrSKU.Focus()
        txtNrSKU.Text = skuProponowane
        For Each row As DataRow In dtKategorie.Rows
            cmbKategoria.Items.Add(row.Item("nazwa").ToString)
        Next
        cmbKategoria.SelectedIndex = 0

        For Each row As DataRow In dtBrand.Rows
            cmbBrand.Items.Add(row.Item("nazwa").ToString)
        Next
        cmbBrand.SelectedIndex = 0

        For Each row As DataRow In dtJM.Rows
            cmbJM.Items.Add(row.Item("nazwa").ToString)
        Next
        cmbJM.SelectedIndex = 0


        If skuProponowane.Length = 0 Then
            txtNrSKU.Focus()
        Else
            txtNazwaSku.Focus()
        End If

        txtCena.Text = "0,00"
        cmbKlasa.SelectedIndex = 0
        cmbGrupaArtykulow.DataSource = dtGrupyArtykulowQ
        cmbGrupaArtykulow.ValueMember = "PROD_GROUP_ID"
        cmbGrupaArtykulow.DisplayMember = "NAZWA"
        cmbGrupaArtykulow.SelectedIndex = 0

    End Sub

    Private Function walidacjaSKU() As Boolean
        If txtNazwaSku.Text = String.Empty Then
            MsgBox("Nie wypełniono pola - Nazwa.", MsgBoxStyle.Exclamation, "Pole wymagane")
            txtNazwaSku.Focus()
            Return False
        End If
        If txtNrSKU.Text = String.Empty Then
            MsgBox("Nie wypełniono pola - Numer.", MsgBoxStyle.Exclamation, "Pole wymagane")
            txtNrSKU.Focus()
            Return False
        End If
        If cmbKlasa.SelectedItem <> "A" And cmbKlasa.SelectedItem <> "B" And cmbKlasa.SelectedItem <> "C" Then
            MsgBox("Nie wybrano Klasy dla nowego produktu.", MsgBoxStyle.Exclamation, "Pole wymagane")
            cmbKlasa.Focus()
            Return False
        End If
        If cmbKategoria.SelectedIndex < 0 Then
            MsgBox("Nie wybrano kategorii nowego produktu.", MsgBoxStyle.Exclamation, "Pole wymagane")
            cmbKategoria.Focus()
            Return False
        End If

        If cmbBrand.SelectedIndex < 0 Then
            MsgBox("Nie wybrano brandu nowego produktu.", MsgBoxStyle.Exclamation, "Pole wymagane")
            cmbBrand.Focus()
            Return False
        End If

        If cmbJM.SelectedIndex < 0 Then
            MsgBox("Nie wybrano J.M. nowego produktu.", MsgBoxStyle.Exclamation, "Pole wymagane")
            cmbJM.Focus()
            Return False
        End If


        '' sprawdzamy czy wybrano grupę artykułów
        If Not IIf(cmbGrupaArtykulow.SelectedValue Is Nothing, -1, cmbGrupaArtykulow.SelectedValue) > 0 Then
            MsgBox("Nie wybrano grupy artykułów", MsgBoxStyle.Exclamation, "Brak grupy artykułów")
            cmbGrupaArtykulow.Focus()
            Return False
        End If


        If Decimal.TryParse(txtCena.Text.Replace(".", uiSep).Replace(",", uiSep), cena) = False Then
            MsgBox("Cena musi być liczbą.", MsgBoxStyle.Exclamation, Me.Text)
            txtCena.Focus()
            Return False
        End If

        If cena < 0 Then
            MsgBox("Cena musi być liczbą dodatnią.", MsgBoxStyle.Exclamation, Me.Text)
            txtCena.Focus()
            Return False
        End If

       

        Return True
    End Function

    Private Sub txtNazwaSku_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNazwaSku.Validated
        If czy_byl_klik_dodaj Then

            If txtNazwaSku.Text = String.Empty Then
                txtNazwaSku.BackColor = Color.LightCoral
                txtNazwaSku.ForeColor = Color.White
            Else
                txtNazwaSku.BackColor = Color.White
                txtNazwaSku.ForeColor = Color.Black
            End If

        End If
    End Sub

    Private Sub txtNrSKU_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNrSKU.Validated
        If czy_byl_klik_dodaj Then
            If txtNrSKU.Text = String.Empty Then
                txtNrSKU.BackColor = Color.LightCoral
                txtNrSKU.ForeColor = Color.White
            Else
                txtNrSKU.BackColor = Color.White
                txtNrSKU.ForeColor = Color.Black
            End If
        End If
    End Sub

    Private Sub cmbKlasa_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbKlasa.Validated
        If czy_byl_klik_dodaj Then
            If cmbKlasa.SelectedItem = String.Empty Then
                cmbKlasa.BackColor = Color.LightCoral
                cmbKlasa.ForeColor = Color.White
            Else
                cmbKlasa.BackColor = Color.White
                cmbKlasa.ForeColor = Color.Black
            End If
        End If
    End Sub

End Class