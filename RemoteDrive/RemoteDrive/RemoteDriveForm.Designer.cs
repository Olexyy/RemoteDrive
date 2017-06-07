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
            this.listViewLocal = new System.Windows.Forms.ListView();
            this.columnFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuLocal = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItemOpenLocal = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemUploadLocal = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDeleteLocal = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemNewFolderLocal = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemFolderName = new System.Windows.Forms.ToolStripTextBox();
            this.createFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemFileName = new System.Windows.Forms.ToolStripTextBox();
            this.menuItemCreateFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemDownload = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDeleteRemote = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemRefreshLocal = new System.Windows.Forms.ToolStripMenuItem();
            this.textBoxCwdLocal = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.buttonPull = new System.Windows.Forms.Button();
            this.buttonRefreshLocal = new System.Windows.Forms.Button();
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
            this.menuItemAutoStart = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBoxRepoBrowser = new System.Windows.Forms.GroupBox();
            this.buttonMergeRemote = new System.Windows.Forms.Button();
            this.buttonPush = new System.Windows.Forms.Button();
            this.buttonWatch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBoxActions = new System.Windows.Forms.GroupBox();
            this.groupBoxnfo = new System.Windows.Forms.GroupBox();
            this.buttonMergeLocal = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuLocal.SuspendLayout();
            this.contextMenuMain.SuspendLayout();
            this.groupBoxRepoBrowser.SuspendLayout();
            this.groupBoxActions.SuspendLayout();
            this.groupBoxnfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewLocal
            // 
            this.listViewLocal.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnFileName});
            this.listViewLocal.ContextMenuStrip = this.contextMenuLocal;
            this.listViewLocal.Location = new System.Drawing.Point(15, 19);
            this.listViewLocal.Name = "listViewLocal";
            this.listViewLocal.Size = new System.Drawing.Size(373, 310);
            this.listViewLocal.TabIndex = 5;
            this.listViewLocal.UseCompatibleStateImageBehavior = false;
            this.listViewLocal.View = System.Windows.Forms.View.Details;
            this.listViewLocal.DoubleClick += new System.EventHandler(this.listViewLocal_DoubleClick);
            // 
            // columnFileName
            // 
            this.columnFileName.Text = "Folder name / File name";
            this.columnFileName.Width = 345;
            // 
            // contextMenuLocal
            // 
            this.contextMenuLocal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemOpenLocal,
            this.toolStripSeparator4,
            this.menuItemOpen,
            this.menuItemUploadLocal,
            this.menuItemDeleteLocal,
            this.toolStripSeparator7,
            this.menuItemNewFolderLocal,
            this.newFileToolStripMenuItem,
            this.toolStripSeparator9,
            this.menuItemDownload,
            this.menuItemDeleteRemote,
            this.toolStripSeparator3,
            this.menuItemRefreshLocal});
            this.contextMenuLocal.Name = "contextMenuLocal";
            this.contextMenuLocal.Size = new System.Drawing.Size(171, 226);
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
            // menuItemOpen
            // 
            this.menuItemOpen.Name = "menuItemOpen";
            this.menuItemOpen.Size = new System.Drawing.Size(170, 22);
            this.menuItemOpen.Text = "Open";
            this.menuItemOpen.Click += new System.EventHandler(this.menuItemOpen_Click);
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
            this.menuItemNewFolderLocal.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemFolderName,
            this.createFolderToolStripMenuItem});
            this.menuItemNewFolderLocal.Name = "menuItemNewFolderLocal";
            this.menuItemNewFolderLocal.Size = new System.Drawing.Size(170, 22);
            this.menuItemNewFolderLocal.Text = "New folder";
            // 
            // menuItemFolderName
            // 
            this.menuItemFolderName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.menuItemFolderName.Name = "menuItemFolderName";
            this.menuItemFolderName.Size = new System.Drawing.Size(100, 23);
            this.menuItemFolderName.Text = "Name...";
            // 
            // createFolderToolStripMenuItem
            // 
            this.createFolderToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.createFolderToolStripMenuItem.Name = "createFolderToolStripMenuItem";
            this.createFolderToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.createFolderToolStripMenuItem.Text = "Create Folder";
            this.createFolderToolStripMenuItem.Click += new System.EventHandler(this.createFolderToolStripMenuItem_Click);
            // 
            // newFileToolStripMenuItem
            // 
            this.newFileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemFileName,
            this.menuItemCreateFile});
            this.newFileToolStripMenuItem.Name = "newFileToolStripMenuItem";
            this.newFileToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.newFileToolStripMenuItem.Text = "New file";
            // 
            // menuItemFileName
            // 
            this.menuItemFileName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.menuItemFileName.Name = "menuItemFileName";
            this.menuItemFileName.Size = new System.Drawing.Size(100, 23);
            this.menuItemFileName.Text = "Name...";
            // 
            // menuItemCreateFile
            // 
            this.menuItemCreateFile.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.menuItemCreateFile.Name = "menuItemCreateFile";
            this.menuItemCreateFile.Size = new System.Drawing.Size(160, 22);
            this.menuItemCreateFile.Text = "Create File";
            this.menuItemCreateFile.Click += new System.EventHandler(this.menuItemCreateFile_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(167, 6);
            // 
            // menuItemDownload
            // 
            this.menuItemDownload.Name = "menuItemDownload";
            this.menuItemDownload.Size = new System.Drawing.Size(170, 22);
            this.menuItemDownload.Text = "Download";
            this.menuItemDownload.Click += new System.EventHandler(this.menuItemDownload_Click);
            // 
            // menuItemDeleteRemote
            // 
            this.menuItemDeleteRemote.Name = "menuItemDeleteRemote";
            this.menuItemDeleteRemote.Size = new System.Drawing.Size(170, 22);
            this.menuItemDeleteRemote.Text = "Delete remote";
            this.menuItemDeleteRemote.Click += new System.EventHandler(this.menuItemDeleteRemote_Click);
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
            // textBoxCwdLocal
            // 
            this.textBoxCwdLocal.Location = new System.Drawing.Point(73, 337);
            this.textBoxCwdLocal.Name = "textBoxCwdLocal";
            this.textBoxCwdLocal.Size = new System.Drawing.Size(257, 20);
            this.textBoxCwdLocal.TabIndex = 8;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 375);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(551, 22);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip";
            // 
            // buttonPull
            // 
            this.buttonPull.Location = new System.Drawing.Point(10, 137);
            this.buttonPull.Name = "buttonPull";
            this.buttonPull.Size = new System.Drawing.Size(89, 23);
            this.buttonPull.TabIndex = 11;
            this.buttonPull.Text = "Pull";
            this.toolTip.SetToolTip(this.buttonPull, "All non equal and not existant files\r\nfrom remote will be downloaded.\r\nFiles that" +
        " not exist on remote \r\nwill be deleted.");
            this.buttonPull.UseVisualStyleBackColor = true;
            this.buttonPull.Click += new System.EventHandler(this.buttonPull_Click);
            // 
            // buttonRefreshLocal
            // 
            this.buttonRefreshLocal.Location = new System.Drawing.Point(336, 335);
            this.buttonRefreshLocal.Name = "buttonRefreshLocal";
            this.buttonRefreshLocal.Size = new System.Drawing.Size(52, 23);
            this.buttonRefreshLocal.TabIndex = 12;
            this.buttonRefreshLocal.Text = "Refresh";
            this.buttonRefreshLocal.UseVisualStyleBackColor = true;
            this.buttonRefreshLocal.Click += new System.EventHandler(this.buttonRefreshLocal_Click);
            // 
            // buttonBack
            // 
            this.buttonBack.Location = new System.Drawing.Point(15, 335);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(52, 23);
            this.buttonBack.TabIndex = 13;
            this.buttonBack.Text = "Back";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBackLocal_Click);
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
            this.menuItemAutoStart,
            this.toolStripSeparator5,
            this.menuItemExit});
            this.contextMenuMain.Name = "contextMenuMain";
            this.contextMenuMain.Size = new System.Drawing.Size(149, 154);
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
            // menuItemAutoStart
            // 
            this.menuItemAutoStart.Name = "menuItemAutoStart";
            this.menuItemAutoStart.Size = new System.Drawing.Size(148, 22);
            this.menuItemAutoStart.Text = "Autostart";
            this.menuItemAutoStart.Click += new System.EventHandler(this.menuItemAutoStart_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(145, 6);
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
            this.groupBoxRepoBrowser.Controls.Add(this.listViewLocal);
            this.groupBoxRepoBrowser.Controls.Add(this.buttonRefreshLocal);
            this.groupBoxRepoBrowser.Controls.Add(this.buttonBack);
            this.groupBoxRepoBrowser.Controls.Add(this.textBoxCwdLocal);
            this.groupBoxRepoBrowser.Location = new System.Drawing.Point(12, 5);
            this.groupBoxRepoBrowser.Name = "groupBoxRepoBrowser";
            this.groupBoxRepoBrowser.Size = new System.Drawing.Size(404, 366);
            this.groupBoxRepoBrowser.TabIndex = 18;
            this.groupBoxRepoBrowser.TabStop = false;
            this.groupBoxRepoBrowser.Text = "Browser";
            // 
            // buttonMergeRemote
            // 
            this.buttonMergeRemote.Enabled = false;
            this.buttonMergeRemote.Location = new System.Drawing.Point(10, 108);
            this.buttonMergeRemote.Name = "buttonMergeRemote";
            this.buttonMergeRemote.Size = new System.Drawing.Size(89, 23);
            this.buttonMergeRemote.TabIndex = 17;
            this.buttonMergeRemote.Text = "Merge (remote)";
            this.toolTip.SetToolTip(this.buttonMergeRemote, "All non equal and not existant files\r\nfrom remote will be downloaded.\r\nFiles that" +
        " not exist on remote \r\nwill be uploaded.\r\n(Remote priority)");
            this.buttonMergeRemote.UseVisualStyleBackColor = true;
            this.buttonMergeRemote.Click += new System.EventHandler(this.buttonMergeRemote_Click);
            // 
            // buttonPush
            // 
            this.buttonPush.Location = new System.Drawing.Point(10, 50);
            this.buttonPush.Name = "buttonPush";
            this.buttonPush.Size = new System.Drawing.Size(89, 23);
            this.buttonPush.TabIndex = 15;
            this.buttonPush.Text = "Push";
            this.toolTip.SetToolTip(this.buttonPush, "All non equal and not existant files\r\nfrom local will be uploaded.\r\nFiles that no" +
        "t exist on local \r\nwill be deleted.");
            this.buttonPush.UseVisualStyleBackColor = true;
            this.buttonPush.Click += new System.EventHandler(this.buttonPush_Click);
            // 
            // buttonWatch
            // 
            this.buttonWatch.Location = new System.Drawing.Point(10, 19);
            this.buttonWatch.Name = "buttonWatch";
            this.buttonWatch.Size = new System.Drawing.Size(89, 23);
            this.buttonWatch.TabIndex = 14;
            this.buttonWatch.Text = "Watch";
            this.buttonWatch.UseVisualStyleBackColor = true;
            this.buttonWatch.Click += new System.EventHandler(this.buttonWatch_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Green;
            this.label1.Location = new System.Drawing.Point(6, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Local equals remote";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Orange;
            this.label2.Location = new System.Drawing.Point(6, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Differes from remote";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(6, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Not exists on remote";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Brown;
            this.label4.Location = new System.Drawing.Point(6, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Not exists on local";
            // 
            // groupBoxActions
            // 
            this.groupBoxActions.Controls.Add(this.buttonMergeLocal);
            this.groupBoxActions.Controls.Add(this.buttonMergeRemote);
            this.groupBoxActions.Controls.Add(this.buttonPull);
            this.groupBoxActions.Controls.Add(this.buttonWatch);
            this.groupBoxActions.Controls.Add(this.buttonPush);
            this.groupBoxActions.Location = new System.Drawing.Point(428, 203);
            this.groupBoxActions.Name = "groupBoxActions";
            this.groupBoxActions.Size = new System.Drawing.Size(112, 168);
            this.groupBoxActions.TabIndex = 23;
            this.groupBoxActions.TabStop = false;
            this.groupBoxActions.Text = "Actions";
            // 
            // groupBoxnfo
            // 
            this.groupBoxnfo.Controls.Add(this.label1);
            this.groupBoxnfo.Controls.Add(this.label2);
            this.groupBoxnfo.Controls.Add(this.label3);
            this.groupBoxnfo.Controls.Add(this.label4);
            this.groupBoxnfo.Location = new System.Drawing.Point(428, 5);
            this.groupBoxnfo.Name = "groupBoxnfo";
            this.groupBoxnfo.Size = new System.Drawing.Size(112, 146);
            this.groupBoxnfo.TabIndex = 24;
            this.groupBoxnfo.TabStop = false;
            this.groupBoxnfo.Text = "Info";
            // 
            // buttonMergeLocal
            // 
            this.buttonMergeLocal.Enabled = false;
            this.buttonMergeLocal.Location = new System.Drawing.Point(10, 79);
            this.buttonMergeLocal.Name = "buttonMergeLocal";
            this.buttonMergeLocal.Size = new System.Drawing.Size(89, 23);
            this.buttonMergeLocal.TabIndex = 18;
            this.buttonMergeLocal.Text = "Merge (local)";
            this.toolTip.SetToolTip(this.buttonMergeLocal, "All non equal and not existant files\r\nfrom local will be uploaded.\r\nFiles that no" +
        "t exist on local \r\nwill be downloaded.\r\n(Local priority)");
            this.buttonMergeLocal.UseVisualStyleBackColor = true;
            this.buttonMergeLocal.Click += new System.EventHandler(this.buttonMergeLocal_Click);
            // 
            // RemoteDriveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 397);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBoxRepoBrowser);
            this.Controls.Add(this.groupBoxActions);
            this.Controls.Add(this.groupBoxnfo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "RemoteDriveForm";
            this.ShowInTaskbar = false;
            this.Text = "Remote Drive";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RemoteDriveForm_FormClosing);
            this.Resize += new System.EventHandler(this.RemoteDriveForm_Resize);
            this.contextMenuLocal.ResumeLayout(false);
            this.contextMenuMain.ResumeLayout(false);
            this.groupBoxRepoBrowser.ResumeLayout(false);
            this.groupBoxRepoBrowser.PerformLayout();
            this.groupBoxActions.ResumeLayout(false);
            this.groupBoxnfo.ResumeLayout(false);
            this.groupBoxnfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView listViewLocal;
        private System.Windows.Forms.ColumnHeader columnFileName;
        private System.Windows.Forms.TextBox textBoxCwdLocal;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button buttonPull;
        private System.Windows.Forms.Button buttonRefreshLocal;
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
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.Button buttonWatch;
        private System.Windows.Forms.ToolStripMenuItem menuItemAutoStart;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem newFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox menuItemFileName;
        private System.Windows.Forms.ToolStripMenuItem menuItemCreateFile;
        private System.Windows.Forms.ToolStripMenuItem menuItemOpen;
        private System.Windows.Forms.ToolStripTextBox menuItemFolderName;
        private System.Windows.Forms.ToolStripMenuItem createFolderToolStripMenuItem;
        private System.Windows.Forms.Button buttonPush;
        private System.Windows.Forms.Button buttonMergeRemote;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem menuItemDownload;
        private System.Windows.Forms.ToolStripMenuItem menuItemDeleteRemote;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBoxActions;
        private System.Windows.Forms.GroupBox groupBoxnfo;
        private System.Windows.Forms.Button buttonMergeLocal;
        private System.Windows.Forms.ToolTip toolTip;
    }
}

