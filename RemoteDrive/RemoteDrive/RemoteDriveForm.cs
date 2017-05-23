using System;
using System.Windows.Forms;
using FtpClient;
using System.Configuration;
using RemoteDrive.ServiceReference;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using RemoteDrive.Properties;

namespace RemoteDrive
{
    public partial class RemoteDriveForm : Form
    {
        private Ftp Ftp { get; set; }
        private Local Local { get; set; }
        private ServiceClient ServiceClient { get; set; }
        private User User { get; set; }
        private void InitializeFtp()
        {
            this.notifyIcon.Icon = Resource.media_white;
            string host = ConfigurationManager.AppSettings["ftpHost"];
            string login = ConfigurationManager.AppSettings["ftpLogin"];
            string password = ConfigurationManager.AppSettings["ftpPassword"];
            string serverRoot = @"ftp://" + host + @"/" + host + @"/Repositories/" + Hashing.GetHashString(User.Email);
            this.Ftp = new Ftp(serverRoot, login, password, this.FtpEventHandler);
            this.Ftp.InitCwd(serverRoot);
        }
        private void InitializeLocal()
        {
            this.Local = new Local(User.Root, this.LocalEventHandler);
        }
        public RemoteDriveForm()
        {
            this.InitializeComponent();
            this.ServiceClient = new ServiceClient();
        }
        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.listViewFtp.SelectedItems.Count == 1)
            {
                FtpItem item = this.listViewFtp.SelectedItems[0].Tag as FtpItem;
                this.Ftp.GetCwd(item);
            }
        }
        private void LocalEventHandler(object sender, LocalEventArgs args)
        {
            if (args.Type == LocalEventType.ListDirectory || args.Type == LocalEventType.DeleteFileOk ||
                args.Type == LocalEventType.DeleteFolderOk || args.Type == LocalEventType.MakeDirectoryOk)
            {
                this.Invoke(new Action(() => {
                    this.listViewLocal.Items.Clear();
                    this.textBoxCwdLocal.Text = args.Cwd.FullPath;
                    args.Cwd.Items.ForEach(i => { this.AddItemLocal(i); });
                }));
            }
        }
        private void FtpEventHandler(object sender, FtpEventArgs args)
        {
            if(args.Type == FtpEventType.ListDirectory)
            {
                this.Invoke(new Action(() => {
                    this.listViewFtp.Items.Clear();
                    this.textBoxCwdRemote.Text = args.Cwd.FullPath;
                    args.Cwd.Items.ForEach(i => { this.AddItem(i); });
                    this.notifyIcon.Icon = Resource.media_black;
                }));
            }
            if( args.Type == FtpEventType.UploadOk ||
                args.Type == FtpEventType.DeleteFileOk || args.Type == FtpEventType.MakeDirectoryOk ||
                args.Type == FtpEventType.DeleteFolderOk)
            {
                this.Invoke(new Action(() => {
                    this.Ftp.GetCwd();
                    this.notifyIcon.Icon = Resource.media_white;
                }));
            }
            if(args.Type == FtpEventType.DownloadOk)
            {
                this.Invoke(new Action(() => {
                    this.Local.Cwd.GetCwd(this.Local.Cwd.FullPath);
                }));
            }
        }
        private void AddItemLocal(LocalItem item)
        {
            ListViewItem listViewItem = new ListViewItem(item.Name);
            listViewItem.Tag = item;
            this.listViewLocal.Items.Add(listViewItem);
        }
        private void AddItem(FtpItem item)
        {
            ListViewItem listViewItem = new ListViewItem(item.Name);
            listViewItem.Tag = item;
            this.listViewFtp.Items.Add(listViewItem);
        }
        private void buttonCooseCwd_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.textBoxCwdLocal.Text = dialog.SelectedPath;
                this.Local.Cwd.GetCwd(dialog.SelectedPath);
            }
        }
        private void listViewLocal_DoubleClick(object sender, EventArgs e)
        {
            if (this.listViewLocal.SelectedItems.Count == 1)
            {
                LocalItem localItem = this.listViewLocal.SelectedItems[0].Tag as LocalItem;
                if (localItem.Type == LocalItemType.Folder)
                    this.Local.Cwd.GetCwd(localItem.FullPath);
            }
        }
        private void buttonBack_Click(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(this.Local.Cwd.Root))
                this.Local.Cwd.GetCwd(this.Local.Cwd.Root);
        }
        private void buttonGo_Click(object sender, EventArgs e)
        {
            this.Local.Cwd.GetCwd(this.Local.Cwd.FullPath);
        }
        private void RemoteDriveForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.notifyIcon.Visible = true;
                //this.notifyIcon.ShowBalloonTip(500);
                this.Hide();
            }
            else if (this.WindowState == FormWindowState.Normal)
                if (this.IsLoggedIn())
                {
                    if (this.Ftp == null)
                        InitializeFtp();
                    if (this.Local == null)
                        InitializeLocal();
                }
        }
        private void menuItemMainWindow_Click(object sender, EventArgs e)
        {
            if (this.IsLoggedIn())
            {
                if (!this.Visible)
                    this.Show();
                this.WindowState = FormWindowState.Normal;
            }
            else
                MessageBox.Show("Please Sign up or login.");
        }
        private void menuItemAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Application.ProductName + ", " + Application.ProductVersion );
        }
        private void menuItemLogin_Click(object sender, EventArgs e)
        {
            object newIco = Resource.media_black;
            this.notifyIcon.Icon = Resource.media_white;
            AuthorizeForm loginDialog = new AuthorizeForm(AuthorizeForm.FormTypes.Login);
            if(loginDialog.ShowDialog() == DialogResult.OK)
            {
                this.User = this.ServiceClient.Login(loginDialog.Login, loginDialog.Password);
                if (this.IsLoggedIn())
                {
                    this.SetLoggedInState(true);
                    MessageBox.Show("Logged in!");
                }
                else
                    MessageBox.Show("Invalid credentials.");
            }
            this.notifyIcon.Icon = Resource.media_black;
        }
        private void menuItemSignUp_Click(object sender, EventArgs e)
        {
            this.notifyIcon.Icon = Resource.media_white;
            AuthorizeForm signUpDialog = new AuthorizeForm(AuthorizeForm.FormTypes.SignUp);
            if (signUpDialog.ShowDialog() == DialogResult.OK)
            {
                if (this.ServiceClient.CreateUser(signUpDialog.Login, signUpDialog.Password, Directory.GetCurrentDirectory()))
                    MessageBox.Show("Registered, please login.");
                else
                    MessageBox.Show("Not success, try again.");
            }
            this.notifyIcon.Icon = Resource.media_black;
        }
        private void menuItemLogOut_Click(object sender, EventArgs e)
        {
            this.User = null;
            this.Ftp = null;
            this.SetLoggedInState(false);
            this.WindowState = FormWindowState.Minimized;
            MessageBox.Show("Logged out.");
        }
        private void menuItemExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
            Application.Exit();
        }
        private void RemoteDriveForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.WindowState = FormWindowState.Minimized;
        }
        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            this.menuItemMainWindow_Click(sender, e);
        }
        #region Custom functions
        private void SetLoggedInState(bool enabled = true)
        {
            if (enabled)
            {
                this.menuItemMainWindow.Enabled = true;
                this.menuItemAuthorize.Visible = false;
                this.menuItemLogOut.Visible = true;
            }
            else
            {
                this.menuItemMainWindow.Enabled = false;
                this.menuItemAuthorize.Visible = true;
                this.menuItemLogOut.Visible = false;
            }
        }
        private bool IsLoggedIn()
        {
            return this.User != null;
        }
        #endregion

        #region Context menus and buttons handlers

        private void menuItemDeleteLocal_Click(object sender, EventArgs e)
        {
            if (this.listViewLocal.SelectedItems.Count == 1)
            {
                LocalItem localItem = this.listViewLocal.SelectedItems[0].Tag as LocalItem;
                this.Local.Cwd.DeleteItem(localItem);
            }
        }

        private void menuItemNewFolderLocal_Click(object sender, EventArgs e)
        {
            this.Local.Cwd.AddFolder();
        }

        private void menuItemUploadLocal_Click(object sender, EventArgs e)
        {
            if (this.listViewLocal.SelectedItems.Count == 1)
            {
                LocalItem localItem = this.listViewLocal.SelectedItems[0].Tag as LocalItem;
                this.Ftp.Upload(localItem);
            }
        }
        private void menuItemOpenLocal_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", this.Local.Cwd.FullPath);
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            this.notifyIcon.Icon = Resource.media_white;
            this.Ftp.GetCwd();
        }

        private void menuItemDeleteRemote_Click(object sender, EventArgs e)
        {
            if (this.listViewFtp.SelectedItems.Count == 1)
            {
                this.notifyIcon.Icon = Resource.media_white;
                FtpItem item = this.listViewFtp.SelectedItems[0].Tag as FtpItem;
                this.Ftp.Delete(item);
            }
        }

        private void menuItemDownloadRemote_Click(object sender, EventArgs e)
        {
            if (this.listViewFtp.SelectedItems.Count == 1)
            {
                this.notifyIcon.Icon = Resource.media_white;
                FtpItem ftpItem = this.listViewFtp.SelectedItems[0].Tag as FtpItem;
                this.Ftp.Download(ftpItem, this.Local.Cwd, null);
            }
        }

        private void menuItemNewFolderRemote_Click(object sender, EventArgs e)
        {
            this.notifyIcon.Icon = Resource.media_white;
            this.Ftp.NewFolder("New_folder");
        }

        #endregion
    }
}
