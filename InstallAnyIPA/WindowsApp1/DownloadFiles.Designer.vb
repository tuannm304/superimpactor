<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DownloadFiles
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
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdRemove = New System.Windows.Forms.Button()
        Me.cmdRemoveAll = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lstCydia = New System.Windows.Forms.ListView()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'cmdClose
        '
        Me.cmdClose.Location = New System.Drawing.Point(432, 361)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(104, 25)
        Me.cmdClose.TabIndex = 9
        Me.cmdClose.Text = "Close"
        Me.cmdClose.UseVisualStyleBackColor = True
        '
        'cmdRemove
        '
        Me.cmdRemove.Location = New System.Drawing.Point(432, 61)
        Me.cmdRemove.Name = "cmdRemove"
        Me.cmdRemove.Size = New System.Drawing.Size(104, 25)
        Me.cmdRemove.TabIndex = 8
        Me.cmdRemove.Text = "Remove"
        Me.cmdRemove.UseVisualStyleBackColor = True
        '
        'cmdRemoveAll
        '
        Me.cmdRemoveAll.Location = New System.Drawing.Point(432, 94)
        Me.cmdRemoveAll.Name = "cmdRemoveAll"
        Me.cmdRemoveAll.Size = New System.Drawing.Size(104, 25)
        Me.cmdRemoveAll.TabIndex = 7
        Me.cmdRemoveAll.Text = "Remove All"
        Me.cmdRemoveAll.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(110, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "List of Download Files"
        '
        'lstCydia
        '
        Me.lstCydia.FullRowSelect = True
        Me.lstCydia.GridLines = True
        Me.lstCydia.HideSelection = False
        Me.lstCydia.Location = New System.Drawing.Point(12, 28)
        Me.lstCydia.Name = "lstCydia"
        Me.lstCydia.Size = New System.Drawing.Size(405, 358)
        Me.lstCydia.TabIndex = 5
        Me.lstCydia.UseCompatibleStateImageBehavior = False
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(432, 28)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(104, 25)
        Me.Button1.TabIndex = 10
        Me.Button1.Text = "Install"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'DownloadFiles
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(548, 398)
        Me.ControlBox = False
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.cmdRemove)
        Me.Controls.Add(Me.cmdRemoveAll)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lstCydia)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "DownloadFiles"
        Me.Text = "DownloadFiles"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cmdClose As Button
    Friend WithEvents cmdRemove As Button
    Friend WithEvents cmdRemoveAll As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents lstCydia As ListView
    Friend WithEvents Button1 As Button
End Class
