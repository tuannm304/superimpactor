<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class URLInstall
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.optSendSpace = New System.Windows.Forms.RadioButton()
        Me.optDailyUpload = New System.Windows.Forms.RadioButton()
        Me.optFileUp = New System.Windows.Forms.RadioButton()
        Me.txtSendspace = New System.Windows.Forms.TextBox()
        Me.txtDailyUpload = New System.Windows.Forms.TextBox()
        Me.txtFileUp = New System.Windows.Forms.TextBox()
        Me.cmdNext = New System.Windows.Forms.Button()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.txtOpenload = New System.Windows.Forms.TextBox()
        Me.optOpenload = New System.Windows.Forms.RadioButton()
        Me.txtDirect = New System.Windows.Forms.TextBox()
        Me.optDirect = New System.Windows.Forms.RadioButton()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(130, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Enter URL of IPA to install"
        '
        'optSendSpace
        '
        Me.optSendSpace.AutoSize = True
        Me.optSendSpace.Location = New System.Drawing.Point(6, 160)
        Me.optSendSpace.Name = "optSendSpace"
        Me.optSendSpace.Size = New System.Drawing.Size(102, 17)
        Me.optSendSpace.TabIndex = 1
        Me.optSendSpace.Text = "Sendspace.com"
        Me.optSendSpace.UseVisualStyleBackColor = True
        Me.optSendSpace.Visible = False
        '
        'optDailyUpload
        '
        Me.optDailyUpload.AutoSize = True
        Me.optDailyUpload.Checked = True
        Me.optDailyUpload.Location = New System.Drawing.Point(6, 39)
        Me.optDailyUpload.Name = "optDailyUpload"
        Me.optDailyUpload.Size = New System.Drawing.Size(103, 17)
        Me.optDailyUpload.TabIndex = 2
        Me.optDailyUpload.TabStop = True
        Me.optDailyUpload.Text = "Dailyuploads.net"
        Me.optDailyUpload.UseVisualStyleBackColor = True
        '
        'optFileUp
        '
        Me.optFileUp.AutoSize = True
        Me.optFileUp.Location = New System.Drawing.Point(6, 196)
        Me.optFileUp.Name = "optFileUp"
        Me.optFileUp.Size = New System.Drawing.Size(77, 17)
        Me.optFileUp.TabIndex = 3
        Me.optFileUp.Text = "Filepup.net"
        Me.optFileUp.UseVisualStyleBackColor = True
        Me.optFileUp.Visible = False
        '
        'txtSendspace
        '
        Me.txtSendspace.Location = New System.Drawing.Point(115, 160)
        Me.txtSendspace.Name = "txtSendspace"
        Me.txtSendspace.Size = New System.Drawing.Size(258, 20)
        Me.txtSendspace.TabIndex = 4
        Me.txtSendspace.Visible = False
        '
        'txtDailyUpload
        '
        Me.txtDailyUpload.Location = New System.Drawing.Point(115, 39)
        Me.txtDailyUpload.Name = "txtDailyUpload"
        Me.txtDailyUpload.Size = New System.Drawing.Size(258, 20)
        Me.txtDailyUpload.TabIndex = 5
        '
        'txtFileUp
        '
        Me.txtFileUp.Location = New System.Drawing.Point(115, 195)
        Me.txtFileUp.Name = "txtFileUp"
        Me.txtFileUp.Size = New System.Drawing.Size(258, 20)
        Me.txtFileUp.TabIndex = 6
        Me.txtFileUp.Visible = False
        '
        'cmdNext
        '
        Me.cmdNext.Location = New System.Drawing.Point(207, 110)
        Me.cmdNext.Name = "cmdNext"
        Me.cmdNext.Size = New System.Drawing.Size(75, 23)
        Me.cmdNext.TabIndex = 7
        Me.cmdNext.Text = "Next"
        Me.cmdNext.UseVisualStyleBackColor = True
        '
        'cmdClose
        '
        Me.cmdClose.Location = New System.Drawing.Point(114, 110)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(75, 23)
        Me.cmdClose.TabIndex = 8
        Me.cmdClose.Text = "Cancel"
        Me.cmdClose.UseVisualStyleBackColor = True
        '
        'txtOpenload
        '
        Me.txtOpenload.Location = New System.Drawing.Point(115, 231)
        Me.txtOpenload.Name = "txtOpenload"
        Me.txtOpenload.Size = New System.Drawing.Size(258, 20)
        Me.txtOpenload.TabIndex = 10
        Me.txtOpenload.Visible = False
        '
        'optOpenload
        '
        Me.optOpenload.AutoSize = True
        Me.optOpenload.Location = New System.Drawing.Point(6, 232)
        Me.optOpenload.Name = "optOpenload"
        Me.optOpenload.Size = New System.Drawing.Size(86, 17)
        Me.optOpenload.TabIndex = 9
        Me.optOpenload.Text = "Openload.co"
        Me.optOpenload.UseVisualStyleBackColor = True
        Me.optOpenload.Visible = False
        '
        'txtDirect
        '
        Me.txtDirect.Location = New System.Drawing.Point(114, 72)
        Me.txtDirect.Name = "txtDirect"
        Me.txtDirect.Size = New System.Drawing.Size(258, 20)
        Me.txtDirect.TabIndex = 12
        '
        'optDirect
        '
        Me.optDirect.AutoSize = True
        Me.optDirect.Location = New System.Drawing.Point(5, 72)
        Me.optDirect.Name = "optDirect"
        Me.optDirect.Size = New System.Drawing.Size(104, 17)
        Me.optDirect.TabIndex = 11
        Me.optDirect.Text = "Direct Download"
        Me.optDirect.UseVisualStyleBackColor = True
        '
        'URLInstall
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(385, 145)
        Me.Controls.Add(Me.txtDirect)
        Me.Controls.Add(Me.optDirect)
        Me.Controls.Add(Me.txtOpenload)
        Me.Controls.Add(Me.optOpenload)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.cmdNext)
        Me.Controls.Add(Me.txtFileUp)
        Me.Controls.Add(Me.txtDailyUpload)
        Me.Controls.Add(Me.txtSendspace)
        Me.Controls.Add(Me.optFileUp)
        Me.Controls.Add(Me.optDailyUpload)
        Me.Controls.Add(Me.optSendSpace)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "URLInstall"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Install from URL"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents optSendSpace As RadioButton
    Friend WithEvents optDailyUpload As RadioButton
    Friend WithEvents optFileUp As RadioButton
    Friend WithEvents txtSendspace As TextBox
    Friend WithEvents txtDailyUpload As TextBox
    Friend WithEvents txtFileUp As TextBox
    Friend WithEvents cmdNext As Button
    Friend WithEvents cmdClose As Button
    Friend WithEvents txtOpenload As TextBox
    Friend WithEvents optOpenload As RadioButton
    Friend WithEvents txtDirect As TextBox
    Friend WithEvents optDirect As RadioButton
End Class
