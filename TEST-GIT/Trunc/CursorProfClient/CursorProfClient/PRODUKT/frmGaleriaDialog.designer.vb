<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGaleriaDialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGaleriaDialog))
        Me.btnZamknij = New System.Windows.Forms.Button()
        Me.CtrImgGaleriaDialog = New CursorProfClient.ctrImgGaleria()
        Me.SuspendLayout()
        '
        'btnZamknij
        '
        Me.btnZamknij.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnZamknij.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnZamknij.ForeColor = System.Drawing.Color.White
        resources.ApplyResources(Me.btnZamknij, "btnZamknij")
        Me.btnZamknij.Name = "btnZamknij"
        Me.btnZamknij.UseVisualStyleBackColor = False
        '
        'CtrImgGaleriaDialog
        '
        Me.CtrImgGaleriaDialog.BackColor = System.Drawing.Color.White
        Me.CtrImgGaleriaDialog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.CtrImgGaleriaDialog, "CtrImgGaleriaDialog")
        Me.CtrImgGaleriaDialog.Name = "CtrImgGaleriaDialog"
        '
        'frmGaleriaDialog
        '
        Me.AcceptButton = Me.btnZamknij
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnZamknij
        Me.ControlBox = False
        Me.Controls.Add(Me.btnZamknij)
        Me.Controls.Add(Me.CtrImgGaleriaDialog)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmGaleriaDialog"
        Me.ShowIcon = False
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CtrImgGaleriaDialog As CursorProfClient.ctrImgGaleria
    Friend WithEvents btnZamknij As System.Windows.Forms.Button
End Class
