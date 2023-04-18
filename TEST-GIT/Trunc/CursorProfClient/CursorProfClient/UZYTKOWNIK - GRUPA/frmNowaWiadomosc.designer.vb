<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNowaWiadomosc
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
        Me.txtTytul = New System.Windows.Forms.TextBox()
        Me.rtxtTresc = New System.Windows.Forms.RichTextBox()
        Me.lblTytul = New System.Windows.Forms.Label()
        Me.lblTresc = New System.Windows.Forms.Label()
        Me.btnZapiszWiadomosc = New System.Windows.Forms.Button()
        Me.btnZamknij = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txtTytul
        '
        Me.txtTytul.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTytul.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtTytul.Location = New System.Drawing.Point(4, 20)
        Me.txtTytul.Multiline = True
        Me.txtTytul.Name = "txtTytul"
        Me.txtTytul.Size = New System.Drawing.Size(444, 50)
        Me.txtTytul.TabIndex = 1
        '
        'rtxtTresc
        '
        Me.rtxtTresc.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rtxtTresc.Location = New System.Drawing.Point(4, 86)
        Me.rtxtTresc.Name = "rtxtTresc"
        Me.rtxtTresc.Size = New System.Drawing.Size(444, 150)
        Me.rtxtTresc.TabIndex = 3
        Me.rtxtTresc.Text = ""
        '
        'lblTytul
        '
        Me.lblTytul.AutoSize = True
        Me.lblTytul.ForeColor = System.Drawing.Color.Black
        Me.lblTytul.Location = New System.Drawing.Point(1, 4)
        Me.lblTytul.Name = "lblTytul"
        Me.lblTytul.Size = New System.Drawing.Size(93, 13)
        Me.lblTytul.TabIndex = 0
        Me.lblTytul.Text = "Tytuł wiadomości:"
        '
        'lblTresc
        '
        Me.lblTresc.AutoSize = True
        Me.lblTresc.ForeColor = System.Drawing.Color.Black
        Me.lblTresc.Location = New System.Drawing.Point(0, 70)
        Me.lblTresc.Name = "lblTresc"
        Me.lblTresc.Size = New System.Drawing.Size(37, 13)
        Me.lblTresc.TabIndex = 2
        Me.lblTresc.Text = "Treść:"
        '
        'btnZapiszWiadomosc
        '
        Me.btnZapiszWiadomosc.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnZapiszWiadomosc.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnZapiszWiadomosc.ForeColor = System.Drawing.Color.White
        Me.btnZapiszWiadomosc.Location = New System.Drawing.Point(265, 237)
        Me.btnZapiszWiadomosc.Name = "btnZapiszWiadomosc"
        Me.btnZapiszWiadomosc.Size = New System.Drawing.Size(102, 23)
        Me.btnZapiszWiadomosc.TabIndex = 4
        Me.btnZapiszWiadomosc.Text = "Zapisz wiadomość"
        Me.btnZapiszWiadomosc.UseVisualStyleBackColor = False
        '
        'btnZamknij
        '
        Me.btnZamknij.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnZamknij.BackColor = System.Drawing.Color.DodgerBlue
        Me.btnZamknij.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnZamknij.ForeColor = System.Drawing.Color.White
        Me.btnZamknij.Location = New System.Drawing.Point(373, 237)
        Me.btnZamknij.Name = "btnZamknij"
        Me.btnZamknij.Size = New System.Drawing.Size(75, 23)
        Me.btnZamknij.TabIndex = 5
        Me.btnZamknij.Text = "Zamknij"
        Me.btnZamknij.UseVisualStyleBackColor = False
        '
        'frmNowaWiadomosc
        '
        Me.AcceptButton = Me.btnZapiszWiadomosc
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnZamknij
        Me.ClientSize = New System.Drawing.Size(452, 262)
        Me.Controls.Add(Me.btnZamknij)
        Me.Controls.Add(Me.btnZapiszWiadomosc)
        Me.Controls.Add(Me.lblTresc)
        Me.Controls.Add(Me.lblTytul)
        Me.Controls.Add(Me.rtxtTresc)
        Me.Controls.Add(Me.txtTytul)
        Me.MinimumSize = New System.Drawing.Size(468, 300)
        Me.Name = "frmNowaWiadomosc"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Nowa wiadomość"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtTytul As System.Windows.Forms.TextBox
    Friend WithEvents rtxtTresc As System.Windows.Forms.RichTextBox
    Friend WithEvents lblTytul As System.Windows.Forms.Label
    Friend WithEvents lblTresc As System.Windows.Forms.Label
    Friend WithEvents btnZapiszWiadomosc As System.Windows.Forms.Button
    Friend WithEvents btnZamknij As System.Windows.Forms.Button
End Class
