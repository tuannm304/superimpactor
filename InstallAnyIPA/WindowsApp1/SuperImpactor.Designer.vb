<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSuperImpactor
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSuperImpactor))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.picHomeBtn = New System.Windows.Forms.PictureBox()
        Me.picDownloadBtn = New System.Windows.Forms.PictureBox()
        Me.picToolBtn = New System.Windows.Forms.PictureBox()
        Me.picDeviceBtn = New System.Windows.Forms.PictureBox()
        Me.picAppBtn = New System.Windows.Forms.PictureBox()
        Me.picLogo = New System.Windows.Forms.PictureBox()
        Me.panelHome = New System.Windows.Forms.Panel()
        Me.homeBrower = New System.Windows.Forms.WebBrowser()
        Me.panelApp = New System.Windows.Forms.Panel()
        Me.cmdClearText = New System.Windows.Forms.Button()
        Me.lstApps = New SuperImpactor.ListViewEx()
        Me.txtAppSearch = New System.Windows.Forms.TextBox()
        Me.cmdUpdate = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.panelDownloads = New System.Windows.Forms.Panel()
        Me.lstDownloads = New SuperImpactor.ListViewEx()
        Me.panelTools = New System.Windows.Forms.Panel()
        Me.childPanelRepo = New System.Windows.Forms.Panel()
        Me.txtCydia = New System.Windows.Forms.TextBox()
        Me.cmdAddCydia = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lstCydia = New SuperImpactor.ListViewEx()
        Me.childPanelRevoke = New System.Windows.Forms.Panel()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmdRefreshRevoke = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbAppleId = New System.Windows.Forms.ComboBox()
        Me.lstRevoke = New SuperImpactor.ListViewEx()
        Me.childPanelAppleIds = New System.Windows.Forms.Panel()
        Me.lstAccount = New SuperImpactor.ListViewEx()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lblDeleteAppId = New System.Windows.Forms.Label()
        Me.lblAccount = New System.Windows.Forms.Label()
        Me.lblRevokeCert = New System.Windows.Forms.Label()
        Me.lblCydiaRepos = New System.Windows.Forms.Label()
        Me.panelDevice = New System.Windows.Forms.Panel()
        Me.cmdCheckForUpdate = New System.Windows.Forms.Button()
        Me.cmdAbout = New System.Windows.Forms.Button()
        Me.cmdFixCrash = New System.Windows.Forms.Button()
        Me.cmdInstallFromFile = New System.Windows.Forms.Button()
        Me.picLoading = New System.Windows.Forms.PictureBox()
        Me.cmdResignExpired = New System.Windows.Forms.Button()
        Me.lblPhone = New System.Windows.Forms.Label()
        Me.lblModelNumber = New System.Windows.Forms.Label()
        Me.lblSerialNumber = New System.Windows.Forms.Label()
        Me.lblProductionVersion = New System.Windows.Forms.Label()
        Me.lblDeviceClass = New System.Windows.Forms.Label()
        Me.lblDeviceName = New System.Windows.Forms.Label()
        Me.cmdResignAll = New System.Windows.Forms.Button()
        Me.picDevice = New System.Windows.Forms.PictureBox()
        Me.lstAppOnDevice = New SuperImpactor.ListViewEx()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbDevice = New System.Windows.Forms.ComboBox()
        Me.cmdSupport = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        CType(Me.picHomeBtn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picDownloadBtn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picToolBtn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picDeviceBtn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picAppBtn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panelHome.SuspendLayout()
        Me.panelApp.SuspendLayout()
        Me.panelDownloads.SuspendLayout()
        Me.panelTools.SuspendLayout()
        Me.childPanelRepo.SuspendLayout()
        Me.childPanelRevoke.SuspendLayout()
        Me.childPanelAppleIds.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.panelDevice.SuspendLayout()
        CType(Me.picLoading, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picDevice, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.BackgroundImage = Global.SuperImpactor.My.Resources.Resources.titlebar
        Me.Panel1.Controls.Add(Me.picHomeBtn)
        Me.Panel1.Controls.Add(Me.picDownloadBtn)
        Me.Panel1.Controls.Add(Me.picToolBtn)
        Me.Panel1.Controls.Add(Me.picDeviceBtn)
        Me.Panel1.Controls.Add(Me.picAppBtn)
        Me.Panel1.Controls.Add(Me.picLogo)
        Me.Panel1.ForeColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.Panel1.Location = New System.Drawing.Point(-1, -1)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(939, 100)
        Me.Panel1.TabIndex = 0
        '
        'picHomeBtn
        '
        Me.picHomeBtn.BackgroundImage = CType(resources.GetObject("picHomeBtn.BackgroundImage"), System.Drawing.Image)
        Me.picHomeBtn.Location = New System.Drawing.Point(322, 0)
        Me.picHomeBtn.Name = "picHomeBtn"
        Me.picHomeBtn.Size = New System.Drawing.Size(89, 99)
        Me.picHomeBtn.TabIndex = 5
        Me.picHomeBtn.TabStop = False
        '
        'picDownloadBtn
        '
        Me.picDownloadBtn.BackgroundImage = Global.SuperImpactor.My.Resources.Resources.download_btn
        Me.picDownloadBtn.Location = New System.Drawing.Point(500, 0)
        Me.picDownloadBtn.Name = "picDownloadBtn"
        Me.picDownloadBtn.Size = New System.Drawing.Size(96, 99)
        Me.picDownloadBtn.TabIndex = 4
        Me.picDownloadBtn.TabStop = False
        '
        'picToolBtn
        '
        Me.picToolBtn.BackgroundImage = Global.SuperImpactor.My.Resources.Resources.tools_btn
        Me.picToolBtn.Location = New System.Drawing.Point(596, 0)
        Me.picToolBtn.Name = "picToolBtn"
        Me.picToolBtn.Size = New System.Drawing.Size(89, 99)
        Me.picToolBtn.TabIndex = 3
        Me.picToolBtn.TabStop = False
        '
        'picDeviceBtn
        '
        Me.picDeviceBtn.BackgroundImage = Global.SuperImpactor.My.Resources.Resources.device_btn
        Me.picDeviceBtn.Location = New System.Drawing.Point(685, 0)
        Me.picDeviceBtn.Name = "picDeviceBtn"
        Me.picDeviceBtn.Size = New System.Drawing.Size(89, 99)
        Me.picDeviceBtn.TabIndex = 2
        Me.picDeviceBtn.TabStop = False
        '
        'picAppBtn
        '
        Me.picAppBtn.BackgroundImage = Global.SuperImpactor.My.Resources.Resources.apps_btn
        Me.picAppBtn.Location = New System.Drawing.Point(411, 0)
        Me.picAppBtn.Name = "picAppBtn"
        Me.picAppBtn.Size = New System.Drawing.Size(89, 99)
        Me.picAppBtn.TabIndex = 1
        Me.picAppBtn.TabStop = False
        '
        'picLogo
        '
        Me.picLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.picLogo.Image = Global.SuperImpactor.My.Resources.Resources.appicon
        Me.picLogo.Location = New System.Drawing.Point(50, 3)
        Me.picLogo.Name = "picLogo"
        Me.picLogo.Size = New System.Drawing.Size(117, 94)
        Me.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picLogo.TabIndex = 0
        Me.picLogo.TabStop = False
        '
        'panelHome
        '
        Me.panelHome.Controls.Add(Me.homeBrower)
        Me.panelHome.Location = New System.Drawing.Point(-1, 100)
        Me.panelHome.Name = "panelHome"
        Me.panelHome.Size = New System.Drawing.Size(939, 493)
        Me.panelHome.TabIndex = 1
        '
        'homeBrower
        '
        Me.homeBrower.Dock = System.Windows.Forms.DockStyle.Fill
        Me.homeBrower.Location = New System.Drawing.Point(0, 0)
        Me.homeBrower.MinimumSize = New System.Drawing.Size(20, 20)
        Me.homeBrower.Name = "homeBrower"
        Me.homeBrower.Size = New System.Drawing.Size(939, 493)
        Me.homeBrower.TabIndex = 0
        '
        'panelApp
        '
        Me.panelApp.Controls.Add(Me.cmdClearText)
        Me.panelApp.Controls.Add(Me.lstApps)
        Me.panelApp.Controls.Add(Me.txtAppSearch)
        Me.panelApp.Controls.Add(Me.cmdUpdate)
        Me.panelApp.Controls.Add(Me.Label1)
        Me.panelApp.Location = New System.Drawing.Point(0, 100)
        Me.panelApp.Name = "panelApp"
        Me.panelApp.Size = New System.Drawing.Size(938, 493)
        Me.panelApp.TabIndex = 2
        '
        'cmdClearText
        '
        Me.cmdClearText.Location = New System.Drawing.Point(869, 2)
        Me.cmdClearText.Name = "cmdClearText"
        Me.cmdClearText.Size = New System.Drawing.Size(60, 23)
        Me.cmdClearText.TabIndex = 13
        Me.cmdClearText.Text = "Clear"
        Me.cmdClearText.UseVisualStyleBackColor = True
        '
        'lstApps
        '
        Me.lstApps.BackColor = System.Drawing.Color.White
        Me.lstApps.FullRowSelect = True
        Me.lstApps.Location = New System.Drawing.Point(-2, 32)
        Me.lstApps.MultiSelect = False
        Me.lstApps.Name = "lstApps"
        Me.lstApps.OwnerDraw = True
        Me.lstApps.Size = New System.Drawing.Size(938, 461)
        Me.lstApps.TabIndex = 12
        Me.lstApps.UseCompatibleStateImageBehavior = False
        Me.lstApps.View = System.Windows.Forms.View.Details
        '
        'txtAppSearch
        '
        Me.txtAppSearch.Location = New System.Drawing.Point(520, 4)
        Me.txtAppSearch.Name = "txtAppSearch"
        Me.txtAppSearch.Size = New System.Drawing.Size(342, 20)
        Me.txtAppSearch.TabIndex = 11
        '
        'cmdUpdate
        '
        Me.cmdUpdate.Location = New System.Drawing.Point(10, 2)
        Me.cmdUpdate.Name = "cmdUpdate"
        Me.cmdUpdate.Size = New System.Drawing.Size(88, 25)
        Me.cmdUpdate.TabIndex = 9
        Me.cmdUpdate.Text = "Refresh Repos"
        Me.cmdUpdate.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(455, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "App Search"
        '
        'panelDownloads
        '
        Me.panelDownloads.Controls.Add(Me.lstDownloads)
        Me.panelDownloads.Location = New System.Drawing.Point(0, 99)
        Me.panelDownloads.Name = "panelDownloads"
        Me.panelDownloads.Size = New System.Drawing.Size(938, 494)
        Me.panelDownloads.TabIndex = 3
        '
        'lstDownloads
        '
        Me.lstDownloads.BackColor = System.Drawing.Color.White
        Me.lstDownloads.FullRowSelect = True
        Me.lstDownloads.Location = New System.Drawing.Point(0, 1)
        Me.lstDownloads.Name = "lstDownloads"
        Me.lstDownloads.OwnerDraw = True
        Me.lstDownloads.Size = New System.Drawing.Size(938, 493)
        Me.lstDownloads.TabIndex = 6
        Me.lstDownloads.UseCompatibleStateImageBehavior = False
        Me.lstDownloads.View = System.Windows.Forms.View.Details
        '
        'panelTools
        '
        Me.panelTools.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.panelTools.Controls.Add(Me.childPanelRepo)
        Me.panelTools.Controls.Add(Me.childPanelRevoke)
        Me.panelTools.Controls.Add(Me.childPanelAppleIds)
        Me.panelTools.Controls.Add(Me.Panel2)
        Me.panelTools.Location = New System.Drawing.Point(-2, 99)
        Me.panelTools.Name = "panelTools"
        Me.panelTools.Size = New System.Drawing.Size(939, 494)
        Me.panelTools.TabIndex = 4
        '
        'childPanelRepo
        '
        Me.childPanelRepo.Controls.Add(Me.txtCydia)
        Me.childPanelRepo.Controls.Add(Me.cmdAddCydia)
        Me.childPanelRepo.Controls.Add(Me.Label4)
        Me.childPanelRepo.Controls.Add(Me.lstCydia)
        Me.childPanelRepo.Location = New System.Drawing.Point(203, 1)
        Me.childPanelRepo.Name = "childPanelRepo"
        Me.childPanelRepo.Size = New System.Drawing.Size(736, 493)
        Me.childPanelRepo.TabIndex = 8
        '
        'txtCydia
        '
        Me.txtCydia.Location = New System.Drawing.Point(76, 6)
        Me.txtCydia.Name = "txtCydia"
        Me.txtCydia.Size = New System.Drawing.Size(549, 20)
        Me.txtCydia.TabIndex = 2
        '
        'cmdAddCydia
        '
        Me.cmdAddCydia.Location = New System.Drawing.Point(631, 6)
        Me.cmdAddCydia.Name = "cmdAddCydia"
        Me.cmdAddCydia.Size = New System.Drawing.Size(99, 22)
        Me.cmdAddCydia.TabIndex = 3
        Me.cmdAddCydia.Text = "Add Repo"
        Me.cmdAddCydia.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(63, 13)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Repos URL"
        '
        'lstCydia
        '
        Me.lstCydia.BackColor = System.Drawing.Color.White
        Me.lstCydia.FullRowSelect = True
        Me.lstCydia.Location = New System.Drawing.Point(-1, 31)
        Me.lstCydia.Name = "lstCydia"
        Me.lstCydia.OwnerDraw = True
        Me.lstCydia.Size = New System.Drawing.Size(737, 462)
        Me.lstCydia.TabIndex = 0
        Me.lstCydia.UseCompatibleStateImageBehavior = False
        Me.lstCydia.View = System.Windows.Forms.View.Details
        '
        'childPanelRevoke
        '
        Me.childPanelRevoke.Controls.Add(Me.txtPassword)
        Me.childPanelRevoke.Controls.Add(Me.Label3)
        Me.childPanelRevoke.Controls.Add(Me.cmdRefreshRevoke)
        Me.childPanelRevoke.Controls.Add(Me.Label2)
        Me.childPanelRevoke.Controls.Add(Me.cmbAppleId)
        Me.childPanelRevoke.Controls.Add(Me.lstRevoke)
        Me.childPanelRevoke.Location = New System.Drawing.Point(203, 0)
        Me.childPanelRevoke.Name = "childPanelRevoke"
        Me.childPanelRevoke.Size = New System.Drawing.Size(736, 493)
        Me.childPanelRevoke.TabIndex = 1
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(370, 6)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(201, 20)
        Me.txtPassword.TabIndex = 10
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(310, 10)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Password"
        '
        'cmdRefreshRevoke
        '
        Me.cmdRefreshRevoke.Location = New System.Drawing.Point(630, 5)
        Me.cmdRefreshRevoke.Name = "cmdRefreshRevoke"
        Me.cmdRefreshRevoke.Size = New System.Drawing.Size(103, 23)
        Me.cmdRefreshRevoke.TabIndex = 8
        Me.cmdRefreshRevoke.Text = "Load Account"
        Me.cmdRefreshRevoke.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Account"
        '
        'cmbAppleId
        '
        Me.cmbAppleId.FormattingEnabled = True
        Me.cmbAppleId.Location = New System.Drawing.Point(61, 5)
        Me.cmbAppleId.Name = "cmbAppleId"
        Me.cmbAppleId.Size = New System.Drawing.Size(227, 21)
        Me.cmbAppleId.TabIndex = 6
        '
        'lstRevoke
        '
        Me.lstRevoke.BackColor = System.Drawing.Color.White
        Me.lstRevoke.FullRowSelect = True
        Me.lstRevoke.Location = New System.Drawing.Point(-1, 33)
        Me.lstRevoke.Name = "lstRevoke"
        Me.lstRevoke.OwnerDraw = True
        Me.lstRevoke.Size = New System.Drawing.Size(735, 457)
        Me.lstRevoke.TabIndex = 5
        Me.lstRevoke.UseCompatibleStateImageBehavior = False
        Me.lstRevoke.View = System.Windows.Forms.View.Details
        '
        'childPanelAppleIds
        '
        Me.childPanelAppleIds.Controls.Add(Me.lstAccount)
        Me.childPanelAppleIds.Location = New System.Drawing.Point(203, 0)
        Me.childPanelAppleIds.Name = "childPanelAppleIds"
        Me.childPanelAppleIds.Size = New System.Drawing.Size(736, 493)
        Me.childPanelAppleIds.TabIndex = 6
        '
        'lstAccount
        '
        Me.lstAccount.BackColor = System.Drawing.Color.White
        Me.lstAccount.FullRowSelect = True
        Me.lstAccount.Location = New System.Drawing.Point(0, -1)
        Me.lstAccount.Name = "lstAccount"
        Me.lstAccount.OwnerDraw = True
        Me.lstAccount.Size = New System.Drawing.Size(734, 491)
        Me.lstAccount.TabIndex = 0
        Me.lstAccount.UseCompatibleStateImageBehavior = False
        Me.lstAccount.View = System.Windows.Forms.View.Details
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel2.Controls.Add(Me.lblDeleteAppId)
        Me.Panel2.Controls.Add(Me.lblAccount)
        Me.Panel2.Controls.Add(Me.lblRevokeCert)
        Me.Panel2.Controls.Add(Me.lblCydiaRepos)
        Me.Panel2.Location = New System.Drawing.Point(2, -1)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(200, 494)
        Me.Panel2.TabIndex = 0
        '
        'lblDeleteAppId
        '
        Me.lblDeleteAppId.BackColor = System.Drawing.Color.WhiteSmoke
        Me.lblDeleteAppId.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.lblDeleteAppId.Location = New System.Drawing.Point(1, 110)
        Me.lblDeleteAppId.Name = "lblDeleteAppId"
        Me.lblDeleteAppId.Size = New System.Drawing.Size(198, 35)
        Me.lblDeleteAppId.TabIndex = 3
        Me.lblDeleteAppId.Text = "     Delete AppId"
        Me.lblDeleteAppId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblAccount
        '
        Me.lblAccount.BackColor = System.Drawing.Color.WhiteSmoke
        Me.lblAccount.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.lblAccount.Location = New System.Drawing.Point(1, 38)
        Me.lblAccount.Name = "lblAccount"
        Me.lblAccount.Size = New System.Drawing.Size(198, 35)
        Me.lblAccount.TabIndex = 2
        Me.lblAccount.Text = "     Account Manager"
        Me.lblAccount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRevokeCert
        '
        Me.lblRevokeCert.BackColor = System.Drawing.Color.WhiteSmoke
        Me.lblRevokeCert.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.lblRevokeCert.Location = New System.Drawing.Point(1, 74)
        Me.lblRevokeCert.Name = "lblRevokeCert"
        Me.lblRevokeCert.Size = New System.Drawing.Size(198, 35)
        Me.lblRevokeCert.TabIndex = 1
        Me.lblRevokeCert.Text = "     Revoke Certificate"
        Me.lblRevokeCert.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCydiaRepos
        '
        Me.lblCydiaRepos.BackColor = System.Drawing.Color.WhiteSmoke
        Me.lblCydiaRepos.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(163, Byte))
        Me.lblCydiaRepos.Location = New System.Drawing.Point(1, 2)
        Me.lblCydiaRepos.Name = "lblCydiaRepos"
        Me.lblCydiaRepos.Size = New System.Drawing.Size(198, 35)
        Me.lblCydiaRepos.TabIndex = 0
        Me.lblCydiaRepos.Text = "     Cydia Repos"
        Me.lblCydiaRepos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'panelDevice
        '
        Me.panelDevice.BackColor = System.Drawing.Color.Transparent
        Me.panelDevice.Controls.Add(Me.cmdSupport)
        Me.panelDevice.Controls.Add(Me.cmdCheckForUpdate)
        Me.panelDevice.Controls.Add(Me.cmdAbout)
        Me.panelDevice.Controls.Add(Me.cmdFixCrash)
        Me.panelDevice.Controls.Add(Me.cmdInstallFromFile)
        Me.panelDevice.Controls.Add(Me.picLoading)
        Me.panelDevice.Controls.Add(Me.cmdResignExpired)
        Me.panelDevice.Controls.Add(Me.lblPhone)
        Me.panelDevice.Controls.Add(Me.lblModelNumber)
        Me.panelDevice.Controls.Add(Me.lblSerialNumber)
        Me.panelDevice.Controls.Add(Me.lblProductionVersion)
        Me.panelDevice.Controls.Add(Me.lblDeviceClass)
        Me.panelDevice.Controls.Add(Me.lblDeviceName)
        Me.panelDevice.Controls.Add(Me.cmdResignAll)
        Me.panelDevice.Controls.Add(Me.picDevice)
        Me.panelDevice.Controls.Add(Me.lstAppOnDevice)
        Me.panelDevice.Controls.Add(Me.Label6)
        Me.panelDevice.Controls.Add(Me.Label5)
        Me.panelDevice.Controls.Add(Me.cmbDevice)
        Me.panelDevice.Location = New System.Drawing.Point(0, 98)
        Me.panelDevice.Name = "panelDevice"
        Me.panelDevice.Size = New System.Drawing.Size(938, 494)
        Me.panelDevice.TabIndex = 5
        '
        'cmdCheckForUpdate
        '
        Me.cmdCheckForUpdate.Location = New System.Drawing.Point(139, 451)
        Me.cmdCheckForUpdate.Name = "cmdCheckForUpdate"
        Me.cmdCheckForUpdate.Size = New System.Drawing.Size(111, 23)
        Me.cmdCheckForUpdate.TabIndex = 18
        Me.cmdCheckForUpdate.Text = "Check for Update"
        Me.cmdCheckForUpdate.UseVisualStyleBackColor = True
        '
        'cmdAbout
        '
        Me.cmdAbout.Location = New System.Drawing.Point(261, 451)
        Me.cmdAbout.Name = "cmdAbout"
        Me.cmdAbout.Size = New System.Drawing.Size(75, 23)
        Me.cmdAbout.TabIndex = 17
        Me.cmdAbout.Text = "About"
        Me.cmdAbout.UseVisualStyleBackColor = True
        '
        'cmdFixCrash
        '
        Me.cmdFixCrash.Location = New System.Drawing.Point(744, 7)
        Me.cmdFixCrash.Name = "cmdFixCrash"
        Me.cmdFixCrash.Size = New System.Drawing.Size(81, 23)
        Me.cmdFixCrash.TabIndex = 16
        Me.cmdFixCrash.Text = "Fix Crash"
        Me.cmdFixCrash.UseVisualStyleBackColor = True
        Me.cmdFixCrash.Visible = False
        '
        'cmdInstallFromFile
        '
        Me.cmdInstallFromFile.Location = New System.Drawing.Point(832, 7)
        Me.cmdInstallFromFile.Name = "cmdInstallFromFile"
        Me.cmdInstallFromFile.Size = New System.Drawing.Size(96, 23)
        Me.cmdInstallFromFile.TabIndex = 15
        Me.cmdInstallFromFile.Text = "Install from IPA"
        Me.cmdInstallFromFile.UseVisualStyleBackColor = True
        Me.cmdInstallFromFile.Visible = False
        '
        'picLoading
        '
        Me.picLoading.Image = CType(resources.GetObject("picLoading.Image"), System.Drawing.Image)
        Me.picLoading.Location = New System.Drawing.Point(155, 110)
        Me.picLoading.Name = "picLoading"
        Me.picLoading.Size = New System.Drawing.Size(59, 56)
        Me.picLoading.TabIndex = 13
        Me.picLoading.TabStop = False
        '
        'cmdResignExpired
        '
        Me.cmdResignExpired.Location = New System.Drawing.Point(466, 7)
        Me.cmdResignExpired.Name = "cmdResignExpired"
        Me.cmdResignExpired.Size = New System.Drawing.Size(103, 23)
        Me.cmdResignExpired.TabIndex = 12
        Me.cmdResignExpired.Text = "Resign Expired"
        Me.cmdResignExpired.UseVisualStyleBackColor = True
        Me.cmdResignExpired.Visible = False
        '
        'lblPhone
        '
        Me.lblPhone.AutoSize = True
        Me.lblPhone.Location = New System.Drawing.Point(50, 304)
        Me.lblPhone.Name = "lblPhone"
        Me.lblPhone.Size = New System.Drawing.Size(0, 13)
        Me.lblPhone.TabIndex = 11
        '
        'lblModelNumber
        '
        Me.lblModelNumber.AutoSize = True
        Me.lblModelNumber.Location = New System.Drawing.Point(50, 416)
        Me.lblModelNumber.Name = "lblModelNumber"
        Me.lblModelNumber.Size = New System.Drawing.Size(0, 13)
        Me.lblModelNumber.TabIndex = 10
        '
        'lblSerialNumber
        '
        Me.lblSerialNumber.AutoSize = True
        Me.lblSerialNumber.Location = New System.Drawing.Point(50, 388)
        Me.lblSerialNumber.Name = "lblSerialNumber"
        Me.lblSerialNumber.Size = New System.Drawing.Size(0, 13)
        Me.lblSerialNumber.TabIndex = 9
        '
        'lblProductionVersion
        '
        Me.lblProductionVersion.AutoSize = True
        Me.lblProductionVersion.Location = New System.Drawing.Point(50, 360)
        Me.lblProductionVersion.Name = "lblProductionVersion"
        Me.lblProductionVersion.Size = New System.Drawing.Size(0, 13)
        Me.lblProductionVersion.TabIndex = 8
        '
        'lblDeviceClass
        '
        Me.lblDeviceClass.AutoSize = True
        Me.lblDeviceClass.Location = New System.Drawing.Point(50, 332)
        Me.lblDeviceClass.Name = "lblDeviceClass"
        Me.lblDeviceClass.Size = New System.Drawing.Size(0, 13)
        Me.lblDeviceClass.TabIndex = 7
        '
        'lblDeviceName
        '
        Me.lblDeviceName.AutoSize = True
        Me.lblDeviceName.Location = New System.Drawing.Point(50, 276)
        Me.lblDeviceName.Name = "lblDeviceName"
        Me.lblDeviceName.Size = New System.Drawing.Size(0, 13)
        Me.lblDeviceName.TabIndex = 6
        '
        'cmdResignAll
        '
        Me.cmdResignAll.Location = New System.Drawing.Point(575, 7)
        Me.cmdResignAll.Name = "cmdResignAll"
        Me.cmdResignAll.Size = New System.Drawing.Size(75, 23)
        Me.cmdResignAll.TabIndex = 5
        Me.cmdResignAll.Text = "Resign All"
        Me.cmdResignAll.UseVisualStyleBackColor = True
        Me.cmdResignAll.Visible = False
        '
        'picDevice
        '
        Me.picDevice.Location = New System.Drawing.Point(89, 41)
        Me.picDevice.Name = "picDevice"
        Me.picDevice.Size = New System.Drawing.Size(196, 195)
        Me.picDevice.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picDevice.TabIndex = 4
        Me.picDevice.TabStop = False
        Me.picDevice.Visible = False
        '
        'lstAppOnDevice
        '
        Me.lstAppOnDevice.BackColor = System.Drawing.Color.White
        Me.lstAppOnDevice.FullRowSelect = True
        Me.lstAppOnDevice.Location = New System.Drawing.Point(382, 37)
        Me.lstAppOnDevice.Name = "lstAppOnDevice"
        Me.lstAppOnDevice.OwnerDraw = True
        Me.lstAppOnDevice.Size = New System.Drawing.Size(556, 457)
        Me.lstAppOnDevice.TabIndex = 3
        Me.lstAppOnDevice.UseCompatibleStateImageBehavior = False
        Me.lstAppOnDevice.View = System.Windows.Forms.View.Details
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(387, 12)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(73, 13)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "Installed Apps"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(5, 11)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(41, 13)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "Device"
        '
        'cmbDevice
        '
        Me.cmbDevice.FormattingEnabled = True
        Me.cmbDevice.Location = New System.Drawing.Point(50, 7)
        Me.cmbDevice.Name = "cmbDevice"
        Me.cmbDevice.Size = New System.Drawing.Size(324, 21)
        Me.cmbDevice.TabIndex = 0
        '
        'cmdSupport
        '
        Me.cmdSupport.Location = New System.Drawing.Point(53, 451)
        Me.cmdSupport.Name = "cmdSupport"
        Me.cmdSupport.Size = New System.Drawing.Size(75, 23)
        Me.cmdSupport.TabIndex = 19
        Me.cmdSupport.Text = "Support"
        Me.cmdSupport.UseVisualStyleBackColor = True
        '
        'frmSuperImpactor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(939, 592)
        Me.Controls.Add(Me.panelDevice)
        Me.Controls.Add(Me.panelTools)
        Me.Controls.Add(Me.panelApp)
        Me.Controls.Add(Me.panelDownloads)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.panelHome)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmSuperImpactor"
        Me.Text = "SuperImpactor"
        Me.Panel1.ResumeLayout(False)
        CType(Me.picHomeBtn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picDownloadBtn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picToolBtn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picDeviceBtn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picAppBtn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panelHome.ResumeLayout(False)
        Me.panelApp.ResumeLayout(False)
        Me.panelApp.PerformLayout()
        Me.panelDownloads.ResumeLayout(False)
        Me.panelTools.ResumeLayout(False)
        Me.childPanelRepo.ResumeLayout(False)
        Me.childPanelRepo.PerformLayout()
        Me.childPanelRevoke.ResumeLayout(False)
        Me.childPanelRevoke.PerformLayout()
        Me.childPanelAppleIds.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.panelDevice.ResumeLayout(False)
        Me.panelDevice.PerformLayout()
        CType(Me.picLoading, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picDevice, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents picAppBtn As PictureBox
    Friend WithEvents picLogo As PictureBox
    Friend WithEvents picHomeBtn As PictureBox
    Friend WithEvents picDownloadBtn As PictureBox
    Friend WithEvents picToolBtn As PictureBox
    Friend WithEvents picDeviceBtn As PictureBox
    Friend WithEvents panelHome As Panel
    Friend WithEvents homeBrower As WebBrowser
    Friend WithEvents panelApp As Panel
    Friend WithEvents cmdUpdate As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents txtAppSearch As TextBox
    Friend WithEvents lstApps As ListViewEx
    Friend WithEvents panelDownloads As Panel
    Friend WithEvents lstDownloads As ListViewEx
    Friend WithEvents panelTools As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents lblCydiaRepos As Label
    Friend WithEvents lblDeleteAppId As Label
    Friend WithEvents lblAccount As Label
    Friend WithEvents lblRevokeCert As Label
    Friend WithEvents childPanelRevoke As Panel
    Friend WithEvents cmdRefreshRevoke As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents cmbAppleId As ComboBox
    Friend WithEvents lstRevoke As ListViewEx
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents childPanelAppleIds As Panel
    Friend WithEvents lstAccount As ListViewEx
    Friend WithEvents childPanelRepo As Panel
    Friend WithEvents lstCydia As ListViewEx
    Friend WithEvents cmdAddCydia As Button
    Friend WithEvents txtCydia As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents panelDevice As Panel
    Friend WithEvents Label5 As Label
    Friend WithEvents cmbDevice As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents lstAppOnDevice As ListViewEx
    Friend WithEvents picDevice As PictureBox
    Friend WithEvents cmdResignAll As Button
    Friend WithEvents lblDeviceClass As Label
    Friend WithEvents lblDeviceName As Label
    Friend WithEvents lblSerialNumber As Label
    Friend WithEvents lblProductionVersion As Label
    Friend WithEvents lblPhone As Label
    Friend WithEvents lblModelNumber As Label
    Friend WithEvents cmdResignExpired As Button
    Friend WithEvents picLoading As PictureBox
    Friend WithEvents cmdFixCrash As Button
    Friend WithEvents cmdInstallFromFile As Button
    Friend WithEvents cmdAbout As Button
    Friend WithEvents cmdCheckForUpdate As Button
    Friend WithEvents cmdClearText As Button
    Friend WithEvents cmdSupport As Button
End Class
