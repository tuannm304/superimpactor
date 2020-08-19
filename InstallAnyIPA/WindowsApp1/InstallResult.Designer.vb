<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class InstallResult
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
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.cmdClose = New System.Windows.Forms.Button()
        Me.cmdSupport = New System.Windows.Forms.Button()
        Me.cmdDetail = New System.Windows.Forms.Button()
        Me.txtDetail = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'lblMessage
        '
        Me.lblMessage.AutoSize = True
        Me.lblMessage.Location = New System.Drawing.Point(13, 24)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(0, 13)
        Me.lblMessage.TabIndex = 0
        '
        'cmdClose
        '
        Me.cmdClose.Location = New System.Drawing.Point(399, 15)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(75, 23)
        Me.cmdClose.TabIndex = 1
        Me.cmdClose.Text = "Close"
        Me.cmdClose.UseVisualStyleBackColor = True
        '
        'cmdSupport
        '
        Me.cmdSupport.Location = New System.Drawing.Point(399, 54)
        Me.cmdSupport.Name = "cmdSupport"
        Me.cmdSupport.Size = New System.Drawing.Size(75, 23)
        Me.cmdSupport.TabIndex = 2
        Me.cmdSupport.Text = "Support"
        Me.cmdSupport.UseVisualStyleBackColor = True
        '
        'cmdDetail
        '
        Me.cmdDetail.Location = New System.Drawing.Point(12, 54)
        Me.cmdDetail.Name = "cmdDetail"
        Me.cmdDetail.Size = New System.Drawing.Size(75, 23)
        Me.cmdDetail.TabIndex = 3
        Me.cmdDetail.Text = "Detail"
        Me.cmdDetail.UseVisualStyleBackColor = True
        '
        'txtDetail
        '
        Me.txtDetail.Location = New System.Drawing.Point(12, 99)
        Me.txtDetail.Multiline = True
        Me.txtDetail.Name = "txtDetail"
        Me.txtDetail.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDetail.Size = New System.Drawing.Size(461, 81)
        Me.txtDetail.TabIndex = 4
        '
        'InstallResult
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(485, 90)
        Me.ControlBox = False
        Me.Controls.Add(Me.txtDetail)
        Me.Controls.Add(Me.cmdDetail)
        Me.Controls.Add(Me.cmdSupport)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.lblMessage)
        Me.Name = "InstallResult"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Install Result"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblMessage As Label
    Friend WithEvents cmdClose As Button
    Friend WithEvents cmdSupport As Button
    Friend WithEvents cmdDetail As Button
    Friend WithEvents txtDetail As TextBox
End Class
