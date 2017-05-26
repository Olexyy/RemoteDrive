using System;
using System.Windows.Forms;
using FtpClient;
using System.Configuration;
using RemoteDrive.ServiceReference;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using RemoteDrive.Properties;
using System.Runtime.InteropServices;

namespace RemoteDrive
{
    public partial class RemoteDriveForm : Form
    {
        private Ftp Ftp { get; set; }
        private Local Local { get; set; }
        private ServiceClient ServiceClient { get; set; }
        private User User { get; set; }
        private FileWatcher FileWatcher { get; set; }
        private FtpPath FtpPath { get; set; }
        private void InitializeFtp()
        {
            this.notifyIcon.Icon = Resource.media_white;
             string host = ConfigurationManager.AppSettings["ftpHost"];
            string login = ConfigurationManager.AppSettings["ftpLogin"];
            string password = ConfigurationManager.AppSettings["ftpPassword"];
            this.FtpPath = new FtpPath(@"ftp://" + host, host + @"/Repositories", Hashing.GetHashString(User.Email));
            string userRoot = @"ftp://" + host + @"/" + host + @"/Repositories/" + Hashing.GetHashString(User.Email);
            // TODO: change to ftppath handle user root
            this.Ftp = new Ftp(userRoot, login, password, this.FtpEventHandler);
            this.Ftp.InitCwd(userRoot);
        }
        private void InitializeLocal()
        {
            this.Local = new Local(User.Root, this.LocalEventHandler);
        }
        public RemoteDriveForm()
        {
            this.InitializeComponent();
            this.ServiceClient = new ServiceClient();
            this.ManageAutostart();
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

        #region Custom functions

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

        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.listViewFtp.SelectedItems.Count == 1)
            {
                FtpItem item = this.listViewFtp.SelectedItems[0].Tag as FtpItem;
                this.Ftp.GetCwd(item);
            }
        }

        private void buttonPull_Click(object sender, EventArgs e)
        {
            if (this.Local.Cwd.Items.Count > 0)
                MessageBox.Show("Local directory not empty and will be truncated!");
            if (this.FileWatcher != null && this.FileWatcher.Started)
                this.buttonWatch_Click(null, null);
            this.Local.Cwd.DeleteAll();
            foreach (FtpItem ftpItem in this.Ftp.Cwd.Items)
                this.Ftp.Download(ftpItem, this.Local.Cwd);
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
            if (!String.IsNullOrEmpty(this.Local.Cwd.Root))
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
            MessageBox.Show(Application.ProductName + ", " + Application.ProductVersion);
        }
        private void menuItemLogin_Click(object sender, EventArgs e)
        {
            object newIco = Resource.media_black;
            this.notifyIcon.Icon = Resource.media_white;
            AuthorizeForm loginDialog = new AuthorizeForm(AuthorizeForm.FormTypes.Login);
            if (loginDialog.ShowDialog() == DialogResult.OK)
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

        private void menuItemDeleteLocal_Click(object sender, EventArgs e)
        {
            if (this.listViewLocal.SelectedItems.Count == 1)
            {
                LocalItem localItem = this.listViewLocal.SelectedItems[0].Tag as LocalItem;
                this.Local.Cwd.DeleteItem(localItem);
            }
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

        private void buttonWatch_Click(object sender, EventArgs e)
        {
            if(this.FileWatcher == null || !this.FileWatcher.Started)
            {
                if (this.FileWatcher == null)
                    this.FileWatcher = new FileWatcher(User.Root, this.OnFileChanged, this.OnFileCreated,
                        this.OnFileDeleted, this.OnFileRenamed);
                this.FileWatcher.Start();
                this.buttonWatch.Text = "Stop";
            }
            else
            {
                this.FileWatcher.Stop();
                this.buttonWatch.Text = "Watch";
            }
        }

        private void menuItemAutoStart_Click(object sender, EventArgs e)
        {
            this.menuItemAutoStart.Checked = !this.menuItemAutoStart.Checked;
            this.SetAutostart(this.menuItemAutoStart.Checked);
        }

        #endregion

        #region File watcher handlers
        private void OnFileChanged(object source, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Changed)
            {
                this.notifyIcon.Icon = Resource.media_white;
                this.Ftp.Update(e.FullPath, this.FtpPath.Resolve(e.FullPath));
            }
        }

        private void OnFileCreated(object source, FileSystemEventArgs e)
        { // We react on directory events only, no files.
            if (e.ChangeType == WatcherChangeTypes.Created)
            {
                this.notifyIcon.Icon = Resource.media_white;
                if (Directory.Exists(e.FullPath))
                    this.Ftp.CreateDirectory(this.FtpPath.Resolve(e.FullPath));
                else
                    this.Ftp.CreateFileOnly(this.FtpPath.Resolve(e.FullPath));
                this.Local.Cwd.GetCwd();
            }
        }

        private void OnFileDeleted(object source, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Deleted)
            {
                this.notifyIcon.Icon = Resource.media_white;
                if (!String.IsNullOrEmpty(Path.GetExtension(e.Name)))
                    this.Ftp.DeleteFile(this.FtpPath.Resolve(e.FullPath));
                else this.Ftp.DeleteFolder(this.FtpPath.Resolve(e.FullPath));
                this.Local.Cwd.GetCwd();
            }
        }

        private void OnFileRenamed(object source, RenamedEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Renamed)
                throw new NotImplementedException();
        }
        #endregion

        #region Autostart

        private bool AutostartExist()
        {
            var shortcutName = Application.ProductName + ".lnk";
            var startupPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), shortcutName);
            return File.Exists(startupPath);
        }

        private void SetAutostart(bool state)
        {
            this.CreateShortcut();
            var shortcutName = Application.ProductName + ".lnk";
            FileInfo shortcutInfo = new FileInfo(shortcutName);
            var startupPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), shortcutName);
            if (state)
            {
                if(!this.AutostartExist())
                    File.Copy(shortcutInfo.FullName, startupPath);
            }
            else
            {
                if (this.AutostartExist())
                    File.Delete(startupPath);
            } 
        }

        private void ManageAutostart()
        {
            try
            {
                if (this.AutostartExist())
                    this.menuItemAutoStart.Checked = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                this.menuItemAutoStart.Enabled = false;
            }
        }

        private void CreateShortcut()
        {
            var shortcutName = Application.ProductName + ".lnk";
            FileInfo shortcutInfo = new FileInfo(shortcutName);
            if (!shortcutInfo.Exists)
            {
                Type t = Type.GetTypeFromCLSID(new Guid("72C24DD5-D70A-438B-8A42-98424B88AFB8")); //Windows Script Host Shell Object
                dynamic shell = Activator.CreateInstance(t);
                try
                {
                    var lnk = shell.CreateShortcut(Application.ProductName + ".lnk");
                    try
                    {
                        lnk.TargetPath = Path.Combine(Directory.GetCurrentDirectory(), Application.ExecutablePath);
                        lnk.IconLocation = Application.ExecutablePath;
                        lnk.Save();
                    }
                    finally
                    {
                        Marshal.FinalReleaseComObject(lnk);
                    }
                }
                finally
                {
                    Marshal.FinalReleaseComObject(shell);
                }
            }
        }

        #endregion

        private void menuItemCreateFile_Click(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(this.menuItemFileName.Text.Trim()) &&
                this.menuItemFileName.Text.Trim() != "Name...")
                this.Local.Cwd.AddFile(this.menuItemFileName.Text.Trim());
            this.menuItemFileName.Text = "Name...";
        }

        private void menuItemOpen_Click(object sender, EventArgs e)
        {
            if (this.listViewLocal.SelectedItems.Count == 1)
            {
                LocalItem localItem = this.listViewLocal.SelectedItems[0].Tag as LocalItem;
                if (localItem.Type == LocalItemType.File)
                    Process.Start(localItem.FullPath);
            }
        }

        private void createFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.menuItemFolderName.Text.Trim()) &&
                this.menuItemFolderName.Text.Trim() != "Name...")
                this.Local.Cwd.AddFolder(this.menuItemFolderName.Text.Trim());
            this.menuItemFolderName.Text = "Name...";
        }
    }
}
