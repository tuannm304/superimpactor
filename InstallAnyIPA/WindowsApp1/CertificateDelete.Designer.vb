<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CertificateDelete
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
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.cmbAppleId = New System.Windows.Forms.ComboBox()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdRemove = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lstCydia = New System.Windows.Forms.ListView()
        Me.cmdGetCert = New System.Windows.Forms.Button()
        Me.cmdRemoveAll = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(7, 46)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Password"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(46, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Apple Id"
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(64, 44)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.Size = New System.Drawing.Size(206, 20)
        Me.txtPassword.TabIndex = 8
        Me.txtPassword.UseSystemPasswordChar = True
        '
        'cmbAppleId
        '
        Me.cmbAppleId.FormattingEnabled = True
        Me.cmbAppleId.Location = New System.Drawing.Point(64, 12)
        Me.cmbAppleId.Name = "cmbAppleId"
        Me.cmbAppleId.Size = New System.Drawing.Size(206, 21)
        Me.cmbAppleId.TabIndex = 7
        '
        'cmdClose
        '
        Me.cmdClose.Location = New System.Drawing.Point(285, 293)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(104, 25)
        Me.cmdClose.TabIndex = 15
        Me.cmdClose.Text = "Close"
        Me.cmdClose.UseVisualStyleBackColor = True
        '
        'cmdRemove
        '
        Me.cmdRemove.Location = New System.Drawing.Point(285, 95)
        Me.cmdRemove.Name = "cmdRemove"
        Me.cmdRemove.Size = New System.Drawing.Size(104, 25)
        Me.cmdRemove.TabIndex = 14
        Me.cmdRemove.Text = "Revoke"
        Me.cmdRemove.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 76)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(90, 13)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "List of Certificates"
        '
        'lstCydia
        '
        Me.lstCydia.FullRowSelect = True
        Me.lstCydia.GridLines = True
        Me.lstCydia.HideSelection = False
        Me.lstCydia.Location = New System.Drawing.Point(8, 95)
        Me.lstCydia.MultiSelect = False
        Me.lstCydia.Name = "lstCydia"
        Me.lstCydia.Size = New System.Drawing.Size(262, 224)
        Me.lstCydia.TabIndex = 11
        Me.lstCydia.UseCompatibleStateImageBehavior = False
        '
        'cmdGetCert
        '
        Me.cmdGetCert.Location = New System.Drawing.Point(285, 12)
        Me.cmdGetCert.Name = "cmdGetCert"
        Me.cmdGetCert.Size = New System.Drawing.Size(106, 24)
        Me.cmdGetCert.TabIndex = 16
        Me.cmdGetCert.Text = "Get Certificates"
        Me.cmdGetCert.UseVisualStyleBackColor = True
        '
        'cmdRemoveAll
        '
        Me.cmdRemoveAll.Location = New System.Drawing.Point(285, 128)
        Me.cmdRemoveAll.Name = "cmdRemoveAll"
        Me.cmdRemoveAll.Size = New System.Drawing.Size(104, 25)
        Me.cmdRemoveAll.TabIndex = 17
        Me.cmdRemoveAll.Text = "Revoke All"
        Me.cmdRemoveAll.UseVisualStyleBackColor = True
        '
        'CertificateDelete
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(401, 327)
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
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "CertificateDelete"
        Me.Text = "Revoke Certificates"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents cmbAppleId As ComboBox
    Friend WithEvents cmdClose As Button
    Friend WithEvents cmdRemove As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents lstCydia As ListView
    Friend WithEvents cmdGetCert As Button
    Friend WithEvents cmdRemoveAll As Button
End Class
