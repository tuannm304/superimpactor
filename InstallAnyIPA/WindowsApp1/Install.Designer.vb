<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Install
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Install))
        Me.cmbDevice = New System.Windows.Forms.ComboBox()
        Me.cmbAppleId = New System.Windows.Forms.ComboBox()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.cmdInstall = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.chkSave = New System.Windows.Forms.CheckBox()
        Me.lblLoading = New System.Windows.Forms.Label()
        Me.picLoading = New System.Windows.Forms.PictureBox()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtAppName = New System.Windows.Forms.TextBox()
        Me.chkCloneApp = New System.Windows.Forms.CheckBox()
        CType(Me.picLoading, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmbDevice
        '
        Me.cmbDevice.FormattingEnabled = True
        Me.cmbDevice.Location = New System.Drawing.Point(62, 12)
        Me.cmbDevice.Name = "cmbDevice"
        Me.cmbDevice.Size = New System.Drawing.Size(339, 21)
        Me.cmbDevice.TabIndex = 0
        '
        'cmbAppleId
        '
        Me.cmbAppleId.FormattingEnabled = True
        Me.cmbAppleId.Location = New System.Drawing.Point(62, 48)
        Me.cmbAppleId.Name = "cmbAppleId"
        Me.cmbAppleId.Size = New System.Drawing.Size(206, 21)
        Me.cmbAppleId.TabIndex = 1
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(62, 84)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.Size = New System.Drawing.Size(206, 20)
        Me.txtPassword.TabIndex = 2
        Me.txtPassword.UseSystemPasswordChar = True
        '
        'cmdInstall
        '
        Me.cmdInstall.Location = New System.Drawing.Point(299, 49)
        Me.cmdInstall.Name = "cmdInstall"
        Me.cmdInstall.Size = New System.Drawing.Size(75, 23)
        Me.cmdInstall.TabIndex = 3
        Me.cmdInstall.Text = "Install"
        Me.cmdInstall.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(5, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Device"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(5, 53)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(46, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Apple Id"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(5, 86)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Password"
        '
        'chkSave
        '
        Me.chkSave.AutoSize = True
        Me.chkSave.Checked = True
        Me.chkSave.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSave.Location = New System.Drawing.Point(62, 267)
        Me.chkSave.Name = "chkSave"
        Me.chkSave.Size = New System.Drawing.Size(123, 17)
        Me.chkSave.TabIndex = 7
        Me.chkSave.Text = "Save Account Local"
        Me.chkSave.UseVisualStyleBackColor = True
        Me.chkSave.Visible = False
        '
        'lblLoading
        '
        Me.lblLoading.Location = New System.Drawing.Point(8, 231)
        Me.lblLoading.Name = "lblLoading"
        Me.lblLoading.Size = New System.Drawing.Size(393, 23)
        Me.lblLoading.TabIndex = 8
        Me.lblLoading.Text = "Loading"
        Me.lblLoading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblLoading.Visible = False
        '
        'picLoading
        '
        Me.picLoading.Image = CType(resources.GetObject("picLoading.Image"), System.Drawing.Image)
        Me.picLoading.Location = New System.Drawing.Point(177, 175)
        Me.picLoading.Name = "picLoading"
        Me.picLoading.Size = New System.Drawing.Size(59, 56)
        Me.picLoading.TabIndex = 9
        Me.picLoading.TabStop = False
        Me.picLoading.Visible = False
        '
        'cmdClose
        '
        Me.cmdClose.Location = New System.Drawing.Point(299, 83)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(75, 23)
        Me.cmdClose.TabIndex = 10
        Me.cmdClose.Text = "Close"
        Me.cmdClose.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(62, 110)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label4.Size = New System.Drawing.Size(261, 13)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "We only send your password to Apple Server"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(4, 132)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(130, 13)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "New App Name (Optional)"
        '
        'txtAppName
        '
        Me.txtAppName.Location = New System.Drawing.Point(134, 131)
        Me.txtAppName.Name = "txtAppName"
        Me.txtAppName.Size = New System.Drawing.Size(134, 20)
        Me.txtAppName.TabIndex = 13
        '
        'chkCloneApp
        '
        Me.chkCloneApp.AutoSize = True
        Me.chkCloneApp.Location = New System.Drawing.Point(301, 134)
        Me.chkCloneApp.Name = "chkCloneApp"
        Me.chkCloneApp.Size = New System.Drawing.Size(75, 17)
        Me.chkCloneApp.TabIndex = 14
        Me.chkCloneApp.Text = "Clone App"
        Me.chkCloneApp.UseVisualStyleBackColor = True
        '
        'Install
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(401, 252)
        Me.ControlBox = False
        Me.Controls.Add(Me.chkCloneApp)
        Me.Controls.Add(Me.txtAppName)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.picLoading)
        Me.Controls.Add(Me.lblLoading)
        Me.Controls.Add(Me.chkSave)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmdInstall)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.cmbAppleId)
        Me.Controls.Add(Me.cmbDevice)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Install"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Install"
        CType(Me.picLoading, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cmbDevice As ComboBox
    Friend WithEvents cmbAppleId As ComboBox
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents cmdInstall As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents chkSave As CheckBox
    Friend WithEvents lblLoading As Label
    Friend WithEvents picLoading As PictureBox
    Friend WithEvents cmdClose As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents txtAppName As TextBox
    Friend WithEvents chkCloneApp As CheckBox
End Class
