<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AppIdDelete
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
        Me.cmdRemoveAll = New System.Windows.Forms.Button()
        Me.cmdGetCert = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdRemove = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lstCydia = New System.Windows.Forms.ListView()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.cmbAppleId = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'cmdRemoveAll
        '
        Me.cmdRemoveAll.Location = New System.Drawing.Point(289, 122)
        Me.cmdRemoveAll.Name = "cmdRemoveAll"
        Me.cmdRemoveAll.Size = New System.Drawing.Size(104, 25)
        Me.cmdRemoveAll.TabIndex = 27
        Me.cmdRemoveAll.Text = "Delete All"
        Me.cmdRemoveAll.UseVisualStyleBackColor = True
        '
        'cmdGetCert
        '
        Me.cmdGetCert.Location = New System.Drawing.Point(289, 6)
        Me.cmdGetCert.Name = "cmdGetCert"
        Me.cmdGetCert.Size = New System.Drawing.Size(106, 24)
        Me.cmdGetCert.TabIndex = 26
        Me.cmdGetCert.Text = "Get AppIds"
        Me.cmdGetCert.UseVisualStyleBackColor = True
        '
        'cmdClose
        '
        Me.cmdClose.Location = New System.Drawing.Point(289, 287)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(104, 25)
        Me.cmdClose.TabIndex = 25
        Me.cmdClose.Text = "Close"
        Me.cmdClose.UseVisualStyleBackColor = True
        '
        'cmdRemove
        '
        Me.cmdRemove.Location = New System.Drawing.Point(289, 89)
        Me.cmdRemove.Name = "cmdRemove"
        Me.cmdRemove.Size = New System.Drawing.Size(104, 25)
        Me.cmdRemove.TabIndex = 24
        Me.cmdRemove.Text = "Delete"
        Me.cmdRemove.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 70)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 13)
        Me.Label1.TabIndex = 23
        Me.Label1.Text = "List of AppIds"
        '
        'lstCydia
        '
        Me.lstCydia.FullRowSelect = True
        Me.lstCydia.GridLines = True
        Me.lstCydia.HideSelection = False
        Me.lstCydia.Location = New System.Drawing.Point(12, 89)
        Me.lstCydia.MultiSelect = False
        Me.lstCydia.Name = "lstCydia"
        Me.lstCydia.Size = New System.Drawing.Size(262, 224)
        Me.lstCydia.TabIndex = 22
        Me.lstCydia.UseCompatibleStateImageBehavior = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(11, 40)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 13)
        Me.Label3.TabIndex = 21
        Me.Label3.Text = "Password"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(11, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(46, 13)
        Me.Label2.TabIndex = 20
        Me.Label2.Text = "Apple Id"
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(68, 38)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.Size = New System.Drawing.Size(206, 20)
        Me.txtPassword.TabIndex = 19
        Me.txtPassword.UseSystemPasswordChar = True
        '
        'cmbAppleId
        '
        Me.cmbAppleId.FormattingEnabled = True
        Me.cmbAppleId.Location = New System.Drawing.Point(68, 6)
        Me.cmbAppleId.Name = "cmbAppleId"
        Me.cmbAppleId.Size = New System.Drawing.Size(206, 21)
        Me.cmbAppleId.TabIndex = 18
        '
        'AppIdDelete
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(403, 323)
        Me.ControlBox = False
        Me.Controls.Add(Me.cmdRemoveAll)
        Me.Controls.Add(Me.cmdGetCert)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.cmdRemove)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lstCydia)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.cmbAppleId)
        Me.Name = "AppIdDelete"
        Me.Text = "Delete AppId"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cmdRemoveAll As Button
    Friend WithEvents cmdGetCert As Button
    Friend WithEvents cmdClose As Button
    Friend WithEvents cmdRemove As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents lstCydia As ListView
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents cmbAppleId As ComboBox
End Class
