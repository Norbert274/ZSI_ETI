<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDialogGaleria
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnZamknij = New System.Windows.Forms.Button()
        Me.CtrImgGaleriaDialog = New CursorProfClient.ctrImgGaleria()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.btnZamknij, 0, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(356, 405)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(120, 29)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'btnZamknij
        '
        Me.btnZamknij.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnZamknij.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnZamknij.Location = New System.Drawing.Point(3, 3)
        Me.btnZamknij.Name = "btnZamknij"
        Me.btnZamknij.Size = New System.Drawing.Size(114, 23)
        Me.btnZamknij.TabIndex = 0
        Me.btnZamknij.Text = "Zamknij"
        '
        'CtrImgGaleriaDialog
        '
        Me.CtrImgGaleriaDialog.BackColor = System.Drawing.Color.White
        Me.CtrImgGaleriaDialog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CtrImgGaleriaDialog.Location = New System.Drawing.Point(0, 2)
        Me.CtrImgGaleriaDialog.MaximumSize = New System.Drawing.Size(477, 400)
        Me.CtrImgGaleriaDialog.MinimumSize = New System.Drawing.Size(477, 400)
        Me.CtrImgGaleriaDialog.Name = "CtrImgGaleriaDialog"
        Me.CtrImgGaleriaDialog.Size = New System.Drawing.Size(477, 400)
        Me.CtrImgGaleriaDialog.TabIndex = 0
        '
        'frmDialogGaleria
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnZamknij
        Me.ClientSize = New System.Drawing.Size(477, 437)
        Me.ControlBox = False
        Me.Controls.Add(Me.CtrImgGaleriaDialog)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDialogGaleria"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmDialog"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnZamknij As System.Windows.Forms.Button
    Friend WithEvents CtrImgGaleriaDialog As CursorProfClient.ctrImgGaleria

End Class
