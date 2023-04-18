Public Class frmUzytkownikGrupy
    Public frmRodzic As frmUzytkownik
    Private bNacisnietoOk As Boolean = False
    Private bStanGuzikaZastosuj As Boolean 'stan guzika zastosuj w oknie rodzica podczas otwierania naszej formy

    Private Sub frmUzytkownikGrupy_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Not bNacisnietoOk Then
            'przywracamy zestaw grup u¿ytkownika z tabeli z kopi¹ (jest w oknie rodzica)
            Dim dt As DataTable = frmRodzic.dtGrupyUzytkownika.Copy
            frmRodzic.dgvGrupy.DataSource = Nothing
            frmRodzic.dgvGrupy.DataSource = dt
            frmRodzic.dgvGrupy.Columns("grupa_id").Visible = False
            'przywracamy stan guzika zastosuj na oknie rodzica
            frmRodzic.btnZastosuj.Enabled = bStanGuzikaZastosuj
        End If
    End Sub

    Private Sub frmUzytkownikGrupy_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Dim wierszBiezace As DataGridViewRow
        Dim wierszDostepne As DataGridViewRow

        'odczytujemy stan formy rodzica
        bStanGuzikaZastosuj = frmRodzic.btnZastosuj.Enabled

        'wyœwietlamy grupy w których u¿ytkownik siê znajduje
        dgvBiezace.DataSource = frmRodzic.dgvGrupy.DataSource
        dgvBiezace.Columns("grupa_id").Visible = False

        'wyœwietlamy wszystkie dostêpne grupy, a nastêpnie ukrywamy grupy w których u¿ytkownik ju¿ siê znajduje
        dgvDostepne.DataSource = frmRodzic.dtGrupyDostepne
        dgvDostepne.Columns("grupa_id").Visible = False
        For Each wierszBiezace In dgvBiezace.Rows
            For Each wierszDostepne In dgvDostepne.Rows
                If wierszBiezace.Cells("grupa_id").Value = wierszDostepne.Cells("grupa_id").Value Then
                    wierszDostepne.Visible = False
                    Exit For
                End If
            Next
        Next
    End Sub

    Private Sub DodajFunkcjeCentrali()
        If Not frmRodzic.dtFunkcjeUzytkownika Is Nothing Then
            Dim newRow As DataRow = frmRodzic.dtFunkcjeUzytkownika.NewRow()
            newRow("FUNKCJA_ID") = 6
            newRow("NAZWA") = "Twórz raporty"
            newRow("WLACZ") = True
            frmRodzic.dtFunkcjeUzytkownika.Rows.Add(newRow)
            newRow = frmRodzic.dtFunkcjeUzytkownika.NewRow()
            newRow("FUNKCJA_ID") = 8
            newRow("NAZWA") = "Awizacje"
            newRow("WLACZ") = True
            frmRodzic.dtFunkcjeUzytkownika.Rows.Add(newRow)
            frmRodzic.dgvFunkcje.DataSource = frmRodzic.dtFunkcjeUzytkownika.Copy()
            frmRodzic.dgvFunkcje.Columns("funkcja_id").Visible = False
            frmRodzic.dgvFunkcje.Columns("WLACZ").ReadOnly = True
            frmRodzic.dgvFunkcje.Columns("WLACZ").ValueType = Type.GetType("System.Boolean")
        End If
    End Sub

    Private Sub UsunFunkcjeCentrala()
        For Each rowUsunac As DataRow In frmRodzic.dtFunkcjeUzytkownika.Select("(funkcja_id = 6) OR (funkcja_id = 8)")
            frmRodzic.dtFunkcjeUzytkownika.Rows.Remove(rowUsunac)
        Next
        frmRodzic.dgvFunkcje.DataSource = frmRodzic.dtFunkcjeUzytkownika.Copy()
        frmRodzic.dgvFunkcje.Columns("funkcja_id").Visible = False
        frmRodzic.dgvFunkcje.Columns("WLACZ").ReadOnly = True
        frmRodzic.dgvFunkcje.Columns("WLACZ").ValueType = Type.GetType("System.Boolean")
    End Sub

    Private Sub btnDodaj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDodaj.Click
        Dim dtWiersze As New DataTable
        dtWiersze.Columns.Add("numer_wiersza")

        If dgvDostepne.CurrentCell Is Nothing Then
            MsgBox("Najpierw wybierz grupê z prawego panelu", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        
        frmRodzic.btnZastosuj.Enabled = True
        frmRodzic.btnOk.Enabled = True
        If dgvBiezace.DataSource.Rows.Count > 0 Then
            MsgBox("U¿ytkownik mo¿e byæ przypisany tylko do jednej grupy", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        For Each komorka As DataGridViewCell In dgvDostepne.SelectedCells
            'dodajemy do lewego gridu, jeœli tej grupy jeszcze tam nie ma
            dgvBiezace.DataSource.DefaultView.RowFilter = "grupa_id=" & dgvDostepne.Rows(komorka.RowIndex).Cells("grupa_id").Value
            If dgvBiezace.DataSource.DefaultView.Count = 0 Then
                dgvBiezace.DataSource.Rows.Add(dgvDostepne.Rows(komorka.RowIndex).Cells("grupa_id").Value, _
                    dgvDostepne.Rows(komorka.RowIndex).Cells("nazwa").Value)
                If dgvDostepne.Rows(komorka.RowIndex).Cells("nazwa").Value = "CENTRALA" Then DodajFunkcjeCentrali()

                dtWiersze.Rows.Add(komorka.RowIndex)
            End If
        Next
        dgvBiezace.DataSource.DefaultView.RowFilter = ""
        dgvDostepne.CurrentCell = Nothing
        For Each wiersz As DataRow In dtWiersze.Rows
            dgvDostepne.Rows(wiersz.Item(0)).Visible = False
        Next
    End Sub
    
    Private Sub btnUsun_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUsun.Click
        Dim dtWiersze As New DataTable
        dtWiersze.Columns.Add("numer")

        If dgvBiezace.CurrentCell Is Nothing Then
            MsgBox("Najpierw wybierz grupy z lewego panelu", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        frmRodzic.btnZastosuj.Enabled = True
        frmRodzic.btnOk.Enabled = True
        For Each komorka As DataGridViewCell In dgvBiezace.SelectedCells
            For Each wierszDostepne As DataGridViewRow In dgvDostepne.Rows
                If wierszDostepne.Cells("grupa_id").Value = _
                    dgvBiezace.Rows(komorka.RowIndex).Cells("grupa_id").Value _
                    AndAlso wierszDostepne.Visible = False Then

                    dtWiersze.Rows.Add(komorka.RowIndex)
                    wierszDostepne.Visible = True
                    Exit For
                End If
            Next
        Next
        dgvBiezace.CurrentCell = Nothing
        For Each wiersz As DataRow In dtWiersze.Rows
            If dgvBiezace.Rows(wiersz.Item(0)).Cells("nazwa").Value = "CENTRALA" Then
                UsunFunkcjeCentrala()
            End If
            dgvBiezace.Rows.RemoveAt(wiersz.Item(0))
        Next
        dgvBiezace.DataSource.AcceptChanges()
    End Sub

    Private Sub btnAnuluj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnuluj.Click
        Me.Close()
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        bNacisnietoOk = True
        Me.Close()
    End Sub

End Class