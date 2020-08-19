<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AppDetail
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
        Me.imgApp = New System.Windows.Forms.PictureBox()
        Me.lblAppName = New System.Windows.Forms.Label()
        Me.cmdInstall = New System.Windows.Forms.Button()
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.lblSize = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblCategory = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblSupport = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtDesc = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblAuthor = New System.Windows.Forms.Label()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pic1 = New System.Windows.Forms.PictureBox()
        Me.pic2 = New System.Windows.Forms.PictureBox()
        Me.pic3 = New System.Windows.Forms.PictureBox()
        Me.pic4 = New System.Windows.Forms.PictureBox()
        CType(Me.imgApp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pic4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'imgApp
        '
        Me.imgApp.Location = New System.Drawing.Point(13, 13)
        Me.imgApp.Name = "imgApp"
        Me.imgApp.Size = New System.Drawing.Size(63, 58)
        Me.imgApp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.imgApp.TabIndex = 0
        Me.imgApp.TabStop = False
        '
        'lblAppName
        '
        Me.lblAppName.AutoSize = True
        Me.lblAppName.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAppName.Location = New System.Drawing.Point(84, 19)
        Me.lblAppName.Name = "lblAppName"
        Me.lblAppName.Size = New System.Drawing.Size(91, 18)
        Me.lblAppName.TabIndex = 1
        Me.lblAppName.Text = "APP HERE"
        '
        'cmdInstall
        '
        Me.cmdInstall.Location = New System.Drawing.Point(423, 19)
        Me.cmdInstall.Name = "cmdInstall"
        Me.cmdInstall.Size = New System.Drawing.Size(75, 23)
        Me.cmdInstall.TabIndex = 2
        Me.cmdInstall.Text = "Download"
        Me.cmdInstall.UseVisualStyleBackColor = True
        '
        'lblVersion
        '
        Me.lblVersion.AutoSize = True
        Me.lblVersion.Location = New System.Drawing.Point(83, 52)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(22, 13)
        Me.lblVersion.TabIndex = 3
        Me.lblVersion.Text = "0.0"
        '
        'lblSize
        '
        Me.lblSize.AutoSize = True
        Me.lblSize.Location = New System.Drawing.Point(181, 52)
        Me.lblSize.Name = "lblSize"
        Me.lblSize.Size = New System.Drawing.Size(29, 13)
        Me.lblSize.TabIndex = 4
        Me.lblSize.Text = "0 kB"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 98)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(49, 13)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Category"
        '
        'lblCategory
        '
        Me.lblCategory.AutoSize = True
        Me.lblCategory.Location = New System.Drawing.Point(84, 98)
        Me.lblCategory.Name = "lblCategory"
        Me.lblCategory.Size = New System.Drawing.Size(39, 13)
        Me.lblCategory.TabIndex = 6
        Me.lblCategory.Text = "Label5"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 124)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(68, 13)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "Minimum iOS"
        '
        'lblSupport
        '
        Me.lblSupport.AutoSize = True
        Me.lblSupport.Location = New System.Drawing.Point(84, 124)
        Me.lblSupport.Name = "lblSupport"
        Me.lblSupport.Size = New System.Drawing.Size(39, 13)
        Me.lblSupport.TabIndex = 8
        Me.lblSupport.Text = "Label7"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(12, 176)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(60, 13)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "Description"
        '
        'txtDesc
        '
        Me.txtDesc.Location = New System.Drawing.Point(84, 176)
        Me.txtDesc.Multiline = True
        Me.txtDesc.Name = "txtDesc"
        Me.txtDesc.ReadOnly = True
        Me.txtDesc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDesc.Size = New System.Drawing.Size(418, 215)
        Me.txtDesc.TabIndex = 10
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(12, 150)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(38, 13)
        Me.Label9.TabIndex = 11
        Me.Label9.Text = "Author"
        '
        'lblAuthor
        '
        Me.lblAuthor.AutoSize = True
        Me.lblAuthor.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAuthor.Location = New System.Drawing.Point(84, 150)
        Me.lblAuthor.Name = "lblAuthor"
        Me.lblAuthor.Size = New System.Drawing.Size(52, 13)
        Me.lblAuthor.TabIndex = 12
        Me.lblAuthor.Text = "Label10"
        '
        'cmdClose
        '
        Me.cmdClose.Location = New System.Drawing.Point(423, 50)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(75, 23)
        Me.cmdClose.TabIndex = 13
        Me.cmdClose.Text = "Close"
        Me.cmdClose.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 415)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 13)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "ScreenShots"
        '
        'pic1
        '
        Me.pic1.Location = New System.Drawing.Point(84, 415)
        Me.pic1.Name = "pic1"
        Me.pic1.Size = New System.Drawing.Size(100, 174)
        Me.pic1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pic1.TabIndex = 15
        Me.pic1.TabStop = False
        '
        'pic2
        '
        Me.pic2.Location = New System.Drawing.Point(190, 415)
        Me.pic2.Name = "pic2"
        Me.pic2.Size = New System.Drawing.Size(100, 174)
        Me.pic2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pic2.TabIndex = 16
        Me.pic2.TabStop = False
        '
        'pic3
        '
        Me.pic3.Location = New System.Drawing.Point(296, 415)
        Me.pic3.Name = "pic3"
        Me.pic3.Size = New System.Drawing.Size(100, 174)
        Me.pic3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pic3.TabIndex = 17
        Me.pic3.TabStop = False
        '
        'pic4
        '
        Me.pic4.Location = New System.Drawing.Point(402, 415)
        Me.pic4.Name = "pic4"
        Me.pic4.Size = New System.Drawing.Size(100, 174)
        Me.pic4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pic4.TabIndex = 18
        Me.pic4.TabStop = False
        '
        'AppDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(503, 589)
        Me.ControlBox = False
        Me.Controls.Add(Me.pic4)
        Me.Controls.Add(Me.pic3)
        Me.Controls.Add(Me.pic2)
        Me.Controls.Add(Me.pic1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.lblAuthor)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtDesc)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.lblSupport)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.lblCategory)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lblSize)
        Me.Controls.Add(Me.lblVersion)
        Me.Controls.Add(Me.cmdInstall)
        Me.Controls.Add(Me.lblAppName)
        Me.Controls.Add(Me.imgApp)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AppDetail"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "App Detail"
        CType(Me.imgApp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pic4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents imgApp As PictureBox
    Friend WithEvents lblAppName As Label
    Friend WithEvents cmdInstall As Button
    Friend WithEvents lblVersion As Label
    Friend WithEvents lblSize As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents lblCategory As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents lblSupport As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents txtDesc As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents lblAuthor As Label
    Friend WithEvents cmdClose As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents pic1 As PictureBox
    Friend WithEvents pic2 As PictureBox
    Friend WithEvents pic3 As PictureBox
    Friend WithEvents pic4 As PictureBox
End Class
