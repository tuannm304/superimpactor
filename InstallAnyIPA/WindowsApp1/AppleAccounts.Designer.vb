<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AppleAccounts
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
        Me.SuspendLayout()
        '
        'cmdClose
        '
        Me.cmdClose.Location = New System.Drawing.Point(262, 357)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(104, 25)
        Me.cmdClose.TabIndex = 15
        Me.cmdClose.Text = "Close"
        Me.cmdClose.UseVisualStyleBackColor = True
        '
        'cmdRemove
        '
        Me.cmdRemove.Location = New System.Drawing.Point(262, 24)
        Me.cmdRemove.Name = "cmdRemove"
        Me.cmdRemove.Size = New System.Drawing.Size(104, 25)
        Me.cmdRemove.TabIndex = 14
        Me.cmdRemove.Text = "Remove"
        Me.cmdRemove.UseVisualStyleBackColor = True
        '
        'cmdRemoveAll
        '
        Me.cmdRemoveAll.Location = New System.Drawing.Point(262, 57)
        Me.cmdRemoveAll.Name = "cmdRemoveAll"
        Me.cmdRemoveAll.Size = New System.Drawing.Size(104, 25)
        Me.cmdRemoveAll.TabIndex = 13
        Me.cmdRemoveAll.Text = "Remove All"
        Me.cmdRemoveAll.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(117, 13)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "List of Stored Accounts"
        '
        'lstCydia
        '
        Me.lstCydia.FullRowSelect = True
        Me.lstCydia.GridLines = True
        Me.lstCydia.HideSelection = False
        Me.lstCydia.Location = New System.Drawing.Point(12, 24)
        Me.lstCydia.Name = "lstCydia"
        Me.lstCydia.Size = New System.Drawing.Size(239, 358)
        Me.lstCydia.TabIndex = 11
        Me.lstCydia.UseCompatibleStateImageBehavior = False
        '
        'AppleAccounts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(377, 393)
        Me.ControlBox = False
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.cmdRemove)
        Me.Controls.Add(Me.cmdRemoveAll)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lstCydia)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AppleAccounts"
        Me.Text = "Stored Apple Accounts"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cmdClose As Button
    Friend WithEvents cmdRemove As Button
    Friend WithEvents cmdRemoveAll As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents lstCydia As ListView
End Class
