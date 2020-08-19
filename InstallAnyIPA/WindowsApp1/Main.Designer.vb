<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InstallFromURLToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InstallFromReposToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InstallFromFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FromDownloadedFilesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CydiaReposManagerMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AppleAccountsManagerMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DownloadedFilesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RevokeCertificatesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteAppIDToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmdHot = New System.Windows.Forms.Button()
        Me.txtAppSearch = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmdUpdate = New System.Windows.Forms.Button()
        Me.WebBrowser1 = New System.Windows.Forms.WebBrowser()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.lblStatusBar = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lstApps = New SuperImpactor.ListViewEx()
        Me.MenuStrip1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.EditToolStripMenuItem, Me.ToolsToolStripMenuItem, Me.AboutToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(474, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.InstallFromURLToolStripMenuItem, Me.InstallFromReposToolStripMenuItem, Me.InstallFromFileToolStripMenuItem, Me.FromDownloadedFilesToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(68, 20)
        Me.FileToolStripMenuItem.Text = "Install IPA"
        '
        'InstallFromURLToolStripMenuItem
        '
        Me.InstallFromURLToolStripMenuItem.Name = "InstallFromURLToolStripMenuItem"
        Me.InstallFromURLToolStripMenuItem.Size = New System.Drawing.Size(184, 22)
        Me.InstallFromURLToolStripMenuItem.Text = "From URL"
        '
        'InstallFromReposToolStripMenuItem
        '
        Me.InstallFromReposToolStripMenuItem.Name = "InstallFromReposToolStripMenuItem"
        Me.InstallFromReposToolStripMenuItem.Size = New System.Drawing.Size(184, 22)
        Me.InstallFromReposToolStripMenuItem.Text = "From Repos"
        '
        'InstallFromFileToolStripMenuItem
        '
        Me.InstallFromFileToolStripMenuItem.Name = "InstallFromFileToolStripMenuItem"
        Me.InstallFromFileToolStripMenuItem.Size = New System.Drawing.Size(184, 22)
        Me.InstallFromFileToolStripMenuItem.Text = "From File"
        '
        'FromDownloadedFilesToolStripMenuItem
        '
        Me.FromDownloadedFilesToolStripMenuItem.Name = "FromDownloadedFilesToolStripMenuItem"
        Me.FromDownloadedFilesToolStripMenuItem.Size = New System.Drawing.Size(184, 22)
        Me.FromDownloadedFilesToolStripMenuItem.Text = "From Downloaded Files"
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CydiaReposManagerMenuItem, Me.AppleAccountsManagerMenuItem, Me.DownloadedFilesToolStripMenuItem})
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(53, 20)
        Me.EditToolStripMenuItem.Text = "Setting"
        '
        'CydiaReposManagerMenuItem
        '
        Me.CydiaReposManagerMenuItem.Name = "CydiaReposManagerMenuItem"
        Me.CydiaReposManagerMenuItem.Size = New System.Drawing.Size(148, 22)
        Me.CydiaReposManagerMenuItem.Text = "Cydia Repos"
        '
        'AppleAccountsManagerMenuItem
        '
        Me.AppleAccountsManagerMenuItem.Name = "AppleAccountsManagerMenuItem"
        Me.AppleAccountsManagerMenuItem.Size = New System.Drawing.Size(148, 22)
        Me.AppleAccountsManagerMenuItem.Text = "Apple Accounts"
        '
        'DownloadedFilesToolStripMenuItem
        '
        Me.DownloadedFilesToolStripMenuItem.Name = "DownloadedFilesToolStripMenuItem"
        Me.DownloadedFilesToolStripMenuItem.Size = New System.Drawing.Size(148, 22)
        Me.DownloadedFilesToolStripMenuItem.Text = "Download Files"
        '
        'ToolsToolStripMenuItem
        '
        Me.ToolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RevokeCertificatesToolStripMenuItem, Me.DeleteAppIDToolStripMenuItem})
        Me.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem"
        Me.ToolsToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.ToolsToolStripMenuItem.Text = "Tools"
        '
        'RevokeCertificatesToolStripMenuItem
        '
        Me.RevokeCertificatesToolStripMenuItem.Name = "RevokeCertificatesToolStripMenuItem"
        Me.RevokeCertificatesToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.RevokeCertificatesToolStripMenuItem.Text = "Revoke Certificates"
        '
        'DeleteAppIDToolStripMenuItem
        '
        Me.DeleteAppIDToolStripMenuItem.Name = "DeleteAppIDToolStripMenuItem"
        Me.DeleteAppIDToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.DeleteAppIDToolStripMenuItem.Text = "Delete App ID"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HelpToolStripMenuItem, Me.AboutToolStripMenuItem1})
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(40, 20)
        Me.AboutToolStripMenuItem.Text = "Help"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(103, 22)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'AboutToolStripMenuItem1
        '
        Me.AboutToolStripMenuItem1.Name = "AboutToolStripMenuItem1"
        Me.AboutToolStripMenuItem1.Size = New System.Drawing.Size(103, 22)
        Me.AboutToolStripMenuItem1.Text = "About"
        '
        'cmdHot
        '
        Me.cmdHot.Location = New System.Drawing.Point(302, 33)
        Me.cmdHot.Name = "cmdHot"
        Me.cmdHot.Size = New System.Drawing.Size(66, 25)
        Me.cmdHot.TabIndex = 2
        Me.cmdHot.Text = "Hot Apps"
        Me.cmdHot.UseVisualStyleBackColor = True
        '
        'txtAppSearch
        '
        Me.txtAppSearch.Location = New System.Drawing.Point(76, 35)
        Me.txtAppSearch.Name = "txtAppSearch"
        Me.txtAppSearch.Size = New System.Drawing.Size(220, 20)
        Me.txtAppSearch.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 38)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "App Search"
        '
        'cmdUpdate
        '
        Me.cmdUpdate.Enabled = False
        Me.cmdUpdate.Location = New System.Drawing.Point(374, 33)
        Me.cmdUpdate.Name = "cmdUpdate"
        Me.cmdUpdate.Size = New System.Drawing.Size(88, 25)
        Me.cmdUpdate.TabIndex = 6
        Me.cmdUpdate.Text = "Refresh Repos"
        Me.cmdUpdate.UseVisualStyleBackColor = True
        '
        'WebBrowser1
        '
        Me.WebBrowser1.Location = New System.Drawing.Point(12, 240)
        Me.WebBrowser1.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowser1.Name = "WebBrowser1"
        Me.WebBrowser1.Size = New System.Drawing.Size(449, 467)
        Me.WebBrowser1.TabIndex = 7
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatusBar})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 538)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(474, 22)
        Me.StatusStrip1.TabIndex = 8
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'lblStatusBar
        '
        Me.lblStatusBar.Name = "lblStatusBar"
        Me.lblStatusBar.Size = New System.Drawing.Size(111, 17)
        Me.lblStatusBar.Text = "ToolStripStatusLabel1"
        '
        'lstApps
        '
        Me.lstApps.BackColor = System.Drawing.Color.White
        Me.lstApps.FullRowSelect = True
        Me.lstApps.Location = New System.Drawing.Point(13, 68)
        Me.lstApps.Name = "lstApps"
        Me.lstApps.OwnerDraw = True
        Me.lstApps.Size = New System.Drawing.Size(449, 467)
        Me.lstApps.TabIndex = 5
        Me.lstApps.UseCompatibleStateImageBehavior = False
        Me.lstApps.View = System.Windows.Forms.View.Details
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(474, 560)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.WebBrowser1)
        Me.Controls.Add(Me.cmdUpdate)
        Me.Controls.Add(Me.lstApps)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtAppSearch)
        Me.Controls.Add(Me.cmdHot)
        Me.Controls.Add(Me.MenuStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.Name = "Main"
        Me.Text = "Super Impactor"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents EditToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CydiaReposManagerMenuItem As ToolStripMenuItem
    Friend WithEvents cmdHot As Button
    Friend WithEvents txtAppSearch As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents AppleAccountsManagerMenuItem As ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents lstApps As ListViewEx
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents InstallFromURLToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents InstallFromReposToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RevokeCertificatesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DeleteAppIDToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents InstallFromFileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DownloadedFilesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FromDownloadedFilesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents cmdUpdate As Button
    Friend WithEvents WebBrowser1 As WebBrowser
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents lblStatusBar As ToolStripStatusLabel
End Class
