Public Class frmUzytkownikFunkcje
    Public frmRodzic As frmUzytkownik
    Private bNacisnietoOk As Boolean = False
    Private bStanGuzikaZastosuj As Boolean 'stan guzika zastosuj w oknie rodzica podczas otwierania naszej formy

    Private Sub frmUzytkownikFunkcje_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Not bNacisnietoOk Then
            'przywracamy zestaw funkcji u¿ytkownika z tabeli z kopi¹ (jest w oknie rodzica)
            Dim dt As DataTable = frmRodzic.dtFunkcjeUzytkownika.Copy()
            frmRodzic.dgvFunkcje.DataSource = Nothing
            frmRodzic.dgvFunkcje.DataSource = dt
            frmRodzic.dgvFunkcje.Columns("funkcja_id").Visible = False
            frmRodzic.dgvFunkcje.Columns("FORMA_NAZWA").Visible = False
            frmRodzic.dgvFunkcje.Columns("WLACZ").Visible = False
            'przywracamy stan guzika zastosuj na oknie rodzica
            frmRodzic.btnZastosuj.Enabled = bStanGuzikaZastosuj
        End If
    End Sub

    Private Sub frmUzytkownikFunkcje_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Dim wierszBiezace As DataGridViewRow
        Dim wierszDostepne As DataGridViewRow

        'odczytujemy stan formy rodzica
        bStanGuzikaZastosuj = frmRodzic.btnZastosuj.Enabled

        'wyœwietlamy funkcje w których u¿ytkownik siê znajduje
        dgvBiezace.DataSource = frmRodzic.dgvFunkcje.DataSource
        dgvBiezace.Columns("funkcja_id").Visible = False
        dgvBiezace.Columns("NAZWA").ReadOnly = True
        dgvBiezace.Columns("FORMA_NAZWA").Visible = False
        dgvBiezace.Columns("WLACZ").Visible = False
        dgvBiezace.Columns("FORMA_NAZWA").ReadOnly = True
        dgvBiezace.Columns("WLACZ").ReadOnly = False
        dgvBiezace.Columns("WLACZ").ValueType = Type.GetType("System.Boolean")
        CType(dgvBiezace.Columns("WLACZ"), DataGridViewCheckBoxColumn).TrueValue = True
        CType(dgvBiezace.Columns("WLACZ"), DataGridViewCheckBoxColumn).FalseValue = False
        'wyœwietlamy wszystkie dostêpne funkcje, a nastêpnie ukrywamy funkcje w których u¿ytkownik ju¿ siê znajduje
        dgvDostepne.DataSource = frmRodzic.dtFunkcjeDostepne
        dgvDostepne.Columns("funkcja_id").Visible = False
        dgvDostepne.Columns("WLACZ").Visible = False
        dgvDostepne.Columns("FORMA_NAZWA").Visible = False
        For Each wierszBiezace In dgvBiezace.Rows
            For Each wierszDostepne In dgvDostepne.Rows
                If wierszBiezace.Cells("funkcja_id").Value = wierszDostepne.Cells("funkcja_id").Value Then
                    wierszDostepne.Visible = False
                    Exit For
                End If
            Next
        Next
    End Sub

    Private Sub btnDodaj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDodaj.Click
        Dim dtWiersze As New DataTable
        dtWiersze.Columns.Add("numer_wiersza")

        If dgvDostepne.CurrentCell Is Nothing Then
            MsgBox("Najpier wybierz funkcje z prawego panelu", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        frmRodzic.btnZastosuj.Enabled = True
        frmRodzic.btnOk.Enabled = True
        For Each komorka As DataGridViewCell In dgvDostepne.SelectedCells
            'dodajemy do lewego gridu, jeœli tej grupy jeszcze tam nie ma
            dgvBiezace.DataSource.DefaultView.RowFilter = "funkcja_id=" & dgvDostepne.Rows(komorka.RowIndex).Cells("funkcja_id").Value
            If dgvBiezace.DataSource.DefaultView.Count = 0 Then
                dgvBiezace.DataSource.Rows.Add(dgvDostepne.Rows(komorka.RowIndex).Cells("funkcja_id").Value, _
                    dgvDostepne.Rows(komorka.RowIndex).Cells("nazwa").Value _
                    , dgvDostepne.Rows(komorka.RowIndex).Cells("FORMA_NAZWA").Value, dgvDostepne.Rows(komorka.RowIndex).Cells("WLACZ").Value)
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
            MsgBox("Najpier wybierz funkcje z lewego panelu", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        frmRodzic.btnZastosuj.Enabled = True
        frmRodzic.btnOk.Enabled = True
        For Each komorka As DataGridViewCell In dgvBiezace.SelectedCells
            For Each wierszDostepne As DataGridViewRow In dgvDostepne.Rows
                If wierszDostepne.Cells("funkcja_id").Value = _
                    dgvBiezace.Rows(komorka.RowIndex).Cells("funkcja_id").Value _
                    AndAlso wierszDostepne.Visible = False Then

                    dtWiersze.Rows.Add(komorka.RowIndex)
                    wierszDostepne.Visible = True
                    Exit For
                End If
            Next
        Next
        dgvBiezace.CurrentCell = Nothing
        For Each wiersz As DataRow In dtWiersze.Rows
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