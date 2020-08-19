<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class DownloadProgress
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.lblFileSize = New System.Windows.Forms.Label()
        Me.lblSpeed = New System.Windows.Forms.Label()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblFile = New System.Windows.Forms.Label()
        Me.chkStore = New System.Windows.Forms.CheckBox()
        Me.lblPercent = New System.Windows.Forms.Label()
        Me.lblDownload = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 38)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "File size:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(143, 38)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Speed:"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(45, 62)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(482, 23)
        Me.ProgressBar1.TabIndex = 4
        '
        'lblFileSize
        '
        Me.lblFileSize.AutoSize = True
        Me.lblFileSize.Location = New System.Drawing.Point(66, 38)
        Me.lblFileSize.Name = "lblFileSize"
        Me.lblFileSize.Size = New System.Drawing.Size(0, 13)
        Me.lblFileSize.TabIndex = 5
        '
        'lblSpeed
        '
        Me.lblSpeed.AutoSize = True
        Me.lblSpeed.Location = New System.Drawing.Point(190, 38)
        Me.lblSpeed.Name = "lblSpeed"
        Me.lblSpeed.Size = New System.Drawing.Size(0, 13)
        Me.lblSpeed.TabIndex = 6
        '
        'BackgroundWorker1
        '
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(15, 12)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(26, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "File:"
        '
        'lblFile
        '
        Me.lblFile.AutoSize = True
        Me.lblFile.Location = New System.Drawing.Point(42, 12)
        Me.lblFile.Name = "lblFile"
        Me.lblFile.Size = New System.Drawing.Size(0, 13)
        Me.lblFile.TabIndex = 8
        '
        'chkStore
        '
        Me.chkStore.AutoSize = True
        Me.chkStore.Checked = True
        Me.chkStore.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkStore.Location = New System.Drawing.Point(441, 35)
        Me.chkStore.Name = "chkStore"
        Me.chkStore.Size = New System.Drawing.Size(92, 17)
        Me.chkStore.TabIndex = 9
        Me.chkStore.Text = "Store file local"
        Me.chkStore.UseVisualStyleBackColor = True
        '
        'lblPercent
        '
        Me.lblPercent.AutoSize = True
        Me.lblPercent.BackColor = System.Drawing.Color.Transparent
        Me.lblPercent.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.lblPercent.ForeColor = System.Drawing.Color.Fuchsia
        Me.lblPercent.Location = New System.Drawing.Point(14, 65)
        Me.lblPercent.Name = "lblPercent"
        Me.lblPercent.Size = New System.Drawing.Size(0, 13)
        Me.lblPercent.TabIndex = 10
        '
        'lblDownload
        '
        Me.lblDownload.AutoSize = True
        Me.lblDownload.Location = New System.Drawing.Point(362, 38)
        Me.lblDownload.Name = "lblDownload"
        Me.lblDownload.Size = New System.Drawing.Size(0, 13)
        Me.lblDownload.TabIndex = 12
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(279, 38)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(79, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Download size:"
        '
        'DownloadProgress
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(542, 97)
        Me.Controls.Add(Me.lblDownload)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.lblPercent)
        Me.Controls.Add(Me.chkStore)
        Me.Controls.Add(Me.lblFile)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblSpeed)
        Me.Controls.Add(Me.lblFileSize)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "DownloadProgress"
        Me.Text = "Download IPA"
        Me.TransparencyKey = System.Drawing.Color.Lime
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents lblFileSize As Label
    Friend WithEvents lblSpeed As Label
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents Label3 As Label
    Friend WithEvents lblFile As Label
    Friend WithEvents chkStore As CheckBox
    Friend WithEvents lblPercent As Label
    Friend WithEvents lblDownload As Label
    Friend WithEvents Label5 As Label
End Class
