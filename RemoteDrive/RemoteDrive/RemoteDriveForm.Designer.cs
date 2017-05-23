namespace RemoteDrive
{
    partial class RemoteDriveForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RemoteDriveForm));
            this.listViewFtp = new System.Windows.Forms.ListView();
            this.columnFtpFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuRemote = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItemDownloadRemote = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDeleteRemote = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemNewFolderRemote = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemRefreshRemote = new System.Windows.Forms.ToolStripMenuItem();
            this.listViewLocal = new System.Windows.Forms.ListView();
            this.columnFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuLocal = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItemOpenLocal = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemUploadLocal = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDeleteLocal = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemNewFolderLocal = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemRefreshLocal = new System.Windows.Forms.ToolStripMenuItem();
            this.textBoxCwdRemote = new System.Windows.Forms.TextBox();
            this.textBoxCwdLocal = new System.Windows.Forms.TextBox();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.buttonGo = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItemMainWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemAuthorize = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemLogin = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSignUp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemLogOut = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBoxRepoBrowser = new System.Windows.Forms.GroupBox();
            this.contextMenuRemote.SuspendLayout();
            this.contextMenuLocal.SuspendLayout();
            this.contextMenuMain.SuspendLayout();
            this.groupBoxRepoBrowser.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewFtp
            // 
            this.listViewFtp.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnFtpFileName});
            this.listViewFtp.ContextMenuStrip = this.contextMenuRemote;
            this.listViewFtp.Location = new System.Drawing.Point(12, 16);
            this.listViewFtp.Name = "listViewFtp";
            this.listViewFtp.Size = new System.Drawing.Size(370, 293);
            this.listViewFtp.TabIndex = 0;
            this.listViewFtp.UseCompatibleStateImageBehavior = false;
            this.listViewFtp.View = System.Windows.Forms.View.Details;
            this.listViewFtp.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView_MouseDoubleClick);
            // 
            // columnFtpFileName
            // 
            this.columnFtpFileName.Text = "Remote:";
            this.columnFtpFileName.Width = 346;
            // 
            // contextMenuRemote
            // 
            this.contextMenuRemote.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemDownloadRemote,
            this.menuItemDeleteRemote,
            this.toolStripSeparator8,
            this.menuItemNewFolderRemote,
            this.toolStripSeparator6,
            this.menuItemRefreshRemote});
            this.contextMenuRemote.Name = "contextMenuLocal";
            this.contextMenuRemote.Size = new System.Drawing.Size(133, 104);
            // 
            // menuItemDownloadRemote
            // 
            this.menuItemDownloadRemote.Name = "menuItemDownloadRemote";
            this.menuItemDownloadRemote.Size = new System.Drawing.Size(152, 22);
            this.menuItemDownloadRemote.Text = "Download";
            this.menuItemDownloadRemote.Click += new System.EventHandler(this.menuItemDownloadRemote_Click);
            // 
            // menuItemDeleteRemote
            // 
            this.menuItemDeleteRemote.Name = "menuItemDeleteRemote";
            this.menuItemDeleteRemote.Size = new System.Drawing.Size(152, 22);
            this.menuItemDeleteRemote.Text = "Delete";
            this.menuItemDeleteRemote.Click += new System.EventHandler(this.menuItemDeleteRemote_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(149, 6);
            // 
            // menuItemNewFolderRemote
            // 
            this.menuItemNewFolderRemote.Name = "menuItemNewFolderRemote";
            this.menuItemNewFolderRemote.Size = new System.Drawing.Size(152, 22);
            this.menuItemNewFolderRemote.Text = "New folder";
            this.menuItemNewFolderRemote.Click += new System.EventHandler(this.menuItemNewFolderRemote_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(149, 6);
            // 
            // menuItemRefreshRemote
            // 
            this.menuItemRefreshRemote.Name = "menuItemRefreshRemote";
            this.menuItemRefreshRemote.Size = new System.Drawing.Size(152, 22);
            this.menuItemRefreshRemote.Text = "Refresh";
            // 
            // listViewLocal
            // 
            this.listViewLocal.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnFileName});
            this.listViewLocal.ContextMenuStrip = this.contextMenuLocal;
            this.listViewLocal.Location = new System.Drawing.Point(388, 16);
            this.listViewLocal.Name = "listViewLocal";
            this.listViewLocal.Size = new System.Drawing.Size(373, 293);
            this.listViewLocal.TabIndex = 5;
            this.listViewLocal.UseCompatibleStateImageBehavior = false;
            this.listViewLocal.View = System.Windows.Forms.View.Details;
            this.listViewLocal.DoubleClick += new System.EventHandler(this.listViewLocal_DoubleClick);
            // 
            // columnFileName
            // 
            this.columnFileName.Text = "Local:";
            this.columnFileName.Width = 345;
            // 
            // contextMenuLocal
            // 
            this.contextMenuLocal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemOpenLocal,
            this.toolStripSeparator4,
            this.menuItemUploadLocal,
            this.menuItemDeleteLocal,
            this.toolStripSeparator7,
            this.menuItemNewFolderLocal,
            this.toolStripSeparator3,
            this.menuItemRefreshLocal});
            this.contextMenuLocal.Name = "contextMenuLocal";
            this.contextMenuLocal.Size = new System.Drawing.Size(171, 132);
            // 
            // menuItemOpenLocal
            // 
            this.menuItemOpenLocal.Name = "menuItemOpenLocal";
            this.menuItemOpenLocal.Size = new System.Drawing.Size(170, 22);
            this.menuItemOpenLocal.Text = "Browse in Explorer";
            this.menuItemOpenLocal.Click += new System.EventHandler(this.menuItemOpenLocal_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(167, 6);
            // 
            // menuItemUploadLocal
            // 
            this.menuItemUploadLocal.Name = "menuItemUploadLocal";
            this.menuItemUploadLocal.Size = new System.Drawing.Size(170, 22);
            this.menuItemUploadLocal.Text = "Upload";
            this.menuItemUploadLocal.Click += new System.EventHandler(this.menuItemUploadLocal_Click);
            // 
            // menuItemDeleteLocal
            // 
            this.menuItemDeleteLocal.Name = "menuItemDeleteLocal";
            this.menuItemDeleteLocal.Size = new System.Drawing.Size(170, 22);
            this.menuItemDeleteLocal.Text = "Delete";
            this.menuItemDeleteLocal.Click += new System.EventHandler(this.menuItemDeleteLocal_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(167, 6);
            // 
            // menuItemNewFolderLocal
            // 
            this.menuItemNewFolderLocal.Name = "menuItemNewFolderLocal";
            this.menuItemNewFolderLocal.Size = new System.Drawing.Size(170, 22);
            this.menuItemNewFolderLocal.Text = "New folder";
            this.menuItemNewFolderLocal.Click += new System.EventHandler(this.menuItemNewFolderLocal_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(167, 6);
            // 
            // menuItemRefreshLocal
            // 
            this.menuItemRefreshLocal.Name = "menuItemRefreshLocal";
            this.menuItemRefreshLocal.Size = new System.Drawing.Size(170, 22);
            this.menuItemRefreshLocal.Text = "Refresh";
            // 
            // textBoxCwdRemote
            // 
            this.textBoxCwdRemote.Location = new System.Drawing.Point(12, 317);
            this.textBoxCwdRemote.Name = "textBoxCwdRemote";
            this.textBoxCwdRemote.Size = new System.Drawing.Size(267, 20);
            this.textBoxCwdRemote.TabIndex = 7;
            // 
            // textBoxCwdLocal
            // 
            this.textBoxCwdLocal.Location = new System.Drawing.Point(434, 316);
            this.textBoxCwdLocal.Name = "textBoxCwdLocal";
            this.textBoxCwdLocal.Size = new System.Drawing.Size(219, 20);
            this.textBoxCwdLocal.TabIndex = 8;
            this.textBoxCwdLocal.Text = "C:\\";
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Location = new System.Drawing.Point(285, 315);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(97, 23);
            this.buttonRefresh.TabIndex = 6;
            this.buttonRefresh.Text = "Refresh";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 351);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(796, 22);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip";
            // 
            // buttonOpen
            // 
            this.buttonOpen.Location = new System.Drawing.Point(693, 314);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(68, 23);
            this.buttonOpen.TabIndex = 11;
            this.buttonOpen.Text = "Set Local";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonCooseCwd_Click);
            // 
            // buttonGo
            // 
            this.buttonGo.Location = new System.Drawing.Point(659, 314);
            this.buttonGo.Name = "buttonGo";
            this.buttonGo.Size = new System.Drawing.Size(29, 23);
            this.buttonGo.TabIndex = 12;
            this.buttonGo.Text = "Go";
            this.buttonGo.UseVisualStyleBackColor = true;
            this.buttonGo.Click += new System.EventHandler(this.buttonGo_Click);
            // 
            // buttonBack
            // 
            this.buttonBack.Location = new System.Drawing.Point(388, 315);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(40, 23);
            this.buttonBack.TabIndex = 13;
            this.buttonBack.Text = "Back";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuMain;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Web drive";
            this.notifyIcon.Visible = true;
            this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
            // 
            // contextMenuMain
            // 
            this.contextMenuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemMainWindow,
            this.toolStripSeparator1,
            this.menuItemAuthorize,
            this.menuItemLogOut,
            this.menuItemAbout,
            this.toolStripSeparator2,
            this.menuItemExit});
            this.contextMenuMain.Name = "contextMenuMain";
            this.contextMenuMain.Size = new System.Drawing.Size(149, 126);
            // 
            // menuItemMainWindow
            // 
            this.menuItemMainWindow.Enabled = false;
            this.menuItemMainWindow.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.menuItemMainWindow.Name = "menuItemMainWindow";
            this.menuItemMainWindow.Size = new System.Drawing.Size(148, 22);
            this.menuItemMainWindow.Text = "Main window";
            this.menuItemMainWindow.Click += new System.EventHandler(this.menuItemMainWindow_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(145, 6);
            // 
            // menuItemAuthorize
            // 
            this.menuItemAuthorize.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemLogin,
            this.menuItemSignUp});
            this.menuItemAuthorize.Name = "menuItemAuthorize";
            this.menuItemAuthorize.Size = new System.Drawing.Size(148, 22);
            this.menuItemAuthorize.Text = "Authorize...";
            // 
            // menuItemLogin
            // 
            this.menuItemLogin.Name = "menuItemLogin";
            this.menuItemLogin.Size = new System.Drawing.Size(114, 22);
            this.menuItemLogin.Text = "Login";
            this.menuItemLogin.Click += new System.EventHandler(this.menuItemLogin_Click);
            // 
            // menuItemSignUp
            // 
            this.menuItemSignUp.Name = "menuItemSignUp";
            this.menuItemSignUp.Size = new System.Drawing.Size(114, 22);
            this.menuItemSignUp.Text = "Sign up";
            this.menuItemSignUp.Click += new System.EventHandler(this.menuItemSignUp_Click);
            // 
            // menuItemLogOut
            // 
            this.menuItemLogOut.Name = "menuItemLogOut";
            this.menuItemLogOut.Size = new System.Drawing.Size(148, 22);
            this.menuItemLogOut.Text = "Log out";
            this.menuItemLogOut.Visible = false;
            this.menuItemLogOut.Click += new System.EventHandler(this.menuItemLogOut_Click);
            // 
            // menuItemAbout
            // 
            this.menuItemAbout.Name = "menuItemAbout";
            this.menuItemAbout.Size = new System.Drawing.Size(148, 22);
            this.menuItemAbout.Text = "About";
            this.menuItemAbout.Click += new System.EventHandler(this.menuItemAbout_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(145, 6);
            // 
            // menuItemExit
            // 
            this.menuItemExit.Name = "menuItemExit";
            this.menuItemExit.Size = new System.Drawing.Size(148, 22);
            this.menuItemExit.Text = "Exit";
            this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
            // 
            // groupBoxRepoBrowser
            // 
            this.groupBoxRepoBrowser.Controls.Add(this.buttonRefresh);
            this.groupBoxRepoBrowser.Controls.Add(this.buttonOpen);
            this.groupBoxRepoBrowser.Controls.Add(this.listViewLocal);
            this.groupBoxRepoBrowser.Controls.Add(this.buttonGo);
            this.groupBoxRepoBrowser.Controls.Add(this.listViewFtp);
            this.groupBoxRepoBrowser.Controls.Add(this.buttonBack);
            this.groupBoxRepoBrowser.Controls.Add(this.textBoxCwdRemote);
            this.groupBoxRepoBrowser.Controls.Add(this.textBoxCwdLocal);
            this.groupBoxRepoBrowser.Location = new System.Drawing.Point(12, 0);
            this.groupBoxRepoBrowser.Name = "groupBoxRepoBrowser";
            this.groupBoxRepoBrowser.Size = new System.Drawing.Size(772, 344);
            this.groupBoxRepoBrowser.TabIndex = 18;
            this.groupBoxRepoBrowser.TabStop = false;
            // 
            // RemoteDriveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(796, 373);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBoxRepoBrowser);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "RemoteDriveForm";
            this.ShowInTaskbar = false;
            this.Text = "Remote Drive";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RemoteDriveForm_FormClosing);
            this.Resize += new System.EventHandler(this.RemoteDriveForm_Resize);
            this.contextMenuRemote.ResumeLayout(false);
            this.contextMenuLocal.ResumeLayout(false);
            this.contextMenuMain.ResumeLayout(false);
            this.groupBoxRepoBrowser.ResumeLayout(false);
            this.groupBoxRepoBrowser.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewFtp;
        private System.Windows.Forms.ColumnHeader columnFtpFileName;
        private System.Windows.Forms.ListView listViewLocal;
        private System.Windows.Forms.ColumnHeader columnFileName;
        private System.Windows.Forms.TextBox textBoxCwdRemote;
        private System.Windows.Forms.TextBox textBoxCwdLocal;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.Button buttonGo;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.GroupBox groupBoxRepoBrowser;
        private System.Windows.Forms.ContextMenuStrip contextMenuMain;
        private System.Windows.Forms.ToolStripMenuItem menuItemMainWindow;
        private System.Windows.Forms.ToolStripMenuItem menuItemAuthorize;
        private System.Windows.Forms.ToolStripMenuItem menuItemLogin;
        private System.Windows.Forms.ToolStripMenuItem menuItemSignUp;
        private System.Windows.Forms.ToolStripMenuItem menuItemAbout;
        private System.Windows.Forms.ToolStripMenuItem menuItemExit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem menuItemLogOut;
        private System.Windows.Forms.ContextMenuStrip contextMenuLocal;
        private System.Windows.Forms.ToolStripMenuItem menuItemNewFolderLocal;
        private System.Windows.Forms.ToolStripMenuItem menuItemDeleteLocal;
        private System.Windows.Forms.ToolStripMenuItem menuItemUploadLocal;
        private System.Windows.Forms.ToolStripMenuItem menuItemRefreshLocal;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem menuItemOpenLocal;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ContextMenuStrip contextMenuRemote;
        private System.Windows.Forms.ToolStripMenuItem menuItemDownloadRemote;
        private System.Windows.Forms.ToolStripMenuItem menuItemNewFolderRemote;
        private System.Windows.Forms.ToolStripMenuItem menuItemDeleteRemote;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem menuItemRefreshRemote;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
    }
}

