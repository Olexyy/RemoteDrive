using System;
using System.Windows.Forms;
using System.Configuration;
using RemoteDrive.ServiceReference;
using System.IO;
using System.Diagnostics;
using System.Drawing;
using RemoteDrive.Properties;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;

namespace RemoteDrive
{
    public partial class RemoteDriveForm : Form
    {
        private User User { get; set; }
        private ServiceClient ServiceClient { get; set; }
        private FileWatcher FileWatcher { get; set; }
        private RemoteDrivePath PathResolver { get; set; }
        private RemoteDriveBase RemoteDrive { get; set; }
        private bool CompareTrigger { get; set; }
        public RemoteDriveForm()
        {
            this.InitializeComponent();
            this.ServiceClient = new ServiceClient();
            this.ManageAutostart();
        }
        private bool CompareNeeded()
        {
            if (this.CompareTrigger)
                this.CompareTrigger = false;
            else
                this.CompareTrigger = true;
            return !this.CompareTrigger;
        }
        private void Compare()
        {
            List<RemoteDriveItem> remoteDriveItems = this.RemoteDrive.DirectoryRemote.Children().ToList();
            foreach (ListViewItem listViewItem in this.listViewLocal.Items)
            {
                RemoteDriveItem item = listViewItem.Tag as RemoteDriveItem;
                bool trigger = false;
                foreach (RemoteDriveItem remoteItem in remoteDriveItems)
                {
                    if (item.IsSimilarTo(remoteItem))
                    {
                        if(item.NameAndSizeMatches())
                            listViewItem.ForeColor = Color.Green;
                        else if(item.NameMatches())
                            listViewItem.ForeColor = Color.Orange;
                        trigger = true;
                    }
                }
                if (!trigger)
                    listViewItem.ForeColor = Color.Red;
            }
            foreach(RemoteDriveItem remoteItem in remoteDriveItems)
                if(remoteItem.NotMatched())
                {
                    ListViewItem listViewItem = new ListViewItem(remoteItem.Name);
                    listViewItem.Tag = remoteItem;
                    listViewItem.ForeColor = Color.Brown;
                    this.listViewLocal.Items.Add(listViewItem);
                }
        }
        private void RemoteDriveEventHandler(object sender, RemoteDriveEventArgs args)
        {
            if (args.Type == RemoteDriveEventType.ReadRemoteOk)
            {
                this.Invoke(new Action(() => {
                    if (this.CompareNeeded())
                        this.Compare();
                    this.notifyIcon.Icon = Resource.media_black;
                }));
            }
            if (args.Type == RemoteDriveEventType.ReadLocalOk)
            {
                this.Invoke(new Action(() => {
                    this.listViewLocal.Items.Clear();
                    this.textBoxCwdLocal.Text = this.PathResolver.FilterName(args.Item.Name);
                    args.Item.Children().ToList().ForEach(i => { this.AddRemoteDriveItemLocal(i); });
                    if (this.CompareNeeded())
                        this.Compare();
                    this.notifyIcon.Icon = Resource.media_black;
                }));
            }
            if (args.Type == RemoteDriveEventType.DownloadOk || args.Type == RemoteDriveEventType.DeleteLocalOk ||
                args.Type == RemoteDriveEventType.CreateLocalOk || args.Type == RemoteDriveEventType.CreateRemoteOk || 
                args.Type == RemoteDriveEventType.DeleteRemoteOk)
            {
                this.Invoke(new Action(() => { this.RemoteDrive.Navigate(this.RemoteDrive.DirectoryLocal.FullPath); }));
            }
            if (args.Type == RemoteDriveEventType.CreateLocalBegin || args.Type == RemoteDriveEventType.CreateRemoteBegin ||
                args.Type == RemoteDriveEventType.DeleteRemoteBegin || args.Type == RemoteDriveEventType.DownloadBegin ||
                args.Type == RemoteDriveEventType.DeleteLocalBegin)
            {
                this.Invoke(new Action(() => { this.notifyIcon.Icon = Resource.media_white; }));
            }
        }

        #region Custom functions

        private void AddRemoteDriveItemLocal(RemoteDriveItem item)
        {
            ListViewItem listViewItem = new ListViewItem(item.Name);
            listViewItem.Tag = item;
            this.listViewLocal.Items.Add(listViewItem);
        }
        private void SetLoggedInState(bool enabled = true)
        {
            if (enabled)
            {
                this.menuItemMainWindow.Enabled = true;
                this.menuItemAuthorize.Visible = false;
                this.menuItemLogOut.Visible = true;
                this.PathResolver = new RemoteDrivePath(this.User.Root);
                this.RemoteDrive = new RemoteDriveBase(this.ServiceClient, this.PathResolver, this.RemoteDriveEventHandler);
                this.RemoteDrive.ReadDirectoryRemote(this.PathResolver.UserRoot);
                this.RemoteDrive.ReadDirectoryLocal(this.PathResolver.UserPath);
            }
            else
            {
                this.menuItemMainWindow.Enabled = false;
                this.menuItemAuthorize.Visible = true;
                this.menuItemLogOut.Visible = false;
                this.PathResolver = null;
                this.RemoteDrive.ClearDirectories();
                this.RemoteDrive = null;
                this.User = null;
            }
        }
        private bool IsLoggedIn()
        {
            return this.User != null;
        }
        #endregion

        #region Context menus and buttons handlers


        private void buttonPull_Click(object sender, EventArgs e)
        {
            if (this.RemoteDrive.DirectoryLocal.Children().ToList().Count > 0)
                MessageBox.Show("Local directory not empty and will be truncated!");
            if (this.FileWatcher != null && this.FileWatcher.Started)
                this.buttonWatch_Click(null, null);
            this.RemoteDrive.DirectoryLocal.Children().ToList().ForEach(i => i.Delete());
            this.RemoteDrive.Download(this.RemoteDrive.DirectoryRemote.FullPath);
        }

        private void listViewLocal_DoubleClick(object sender, EventArgs e)
        {
            if (this.listViewLocal.SelectedItems.Count == 1)
            {
                RemoteDriveItem item = this.listViewLocal.SelectedItems[0].Tag as RemoteDriveItem;
                if(item.IsDirectory())
                    this.RemoteDrive.Navigate(item.FullPath);
            }
        }
        private void buttonBackLocal_Click(object sender, EventArgs e)
        {
            if (!this.PathResolver.IsHostPath(this.RemoteDrive.DirectoryLocal.Parent))
                this.RemoteDrive.Navigate(this.RemoteDrive.DirectoryLocal.Parent);
        }

        private void RemoteDriveForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                this.Hide();
            }
            else if (this.WindowState == FormWindowState.Normal)
                this.ShowInTaskbar = true;
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
            this.notifyIcon.Icon = Resource.media_white;
            AuthorizeForm loginDialog = new AuthorizeForm(AuthorizeForm.FormTypes.Login);
            if (loginDialog.ShowDialog() == DialogResult.OK)
            {
                try
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
                catch
                {
                    MessageBox.Show("Connection failed.");
                }
            }
            this.notifyIcon.Icon = Resource.media_black;
        }
        private void menuItemSignUp_Click(object sender, EventArgs e)
        {
            this.notifyIcon.Icon = Resource.media_white;
            AuthorizeForm signUpDialog = new AuthorizeForm(AuthorizeForm.FormTypes.SignUp);
            if (signUpDialog.ShowDialog() == DialogResult.OK)
            {
                if (this.ServiceClient.CreateUser(signUpDialog.Login, signUpDialog.Password))
                    MessageBox.Show("Registered, please login.");
                else
                    MessageBox.Show("Not success, try again.");
            }
            this.notifyIcon.Icon = Resource.media_black;
        }
        private void menuItemLogOut_Click(object sender, EventArgs e)
        {//TODO SERVICE LOGOUT
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

        private void buttonRefreshLocal_Click(object sender, EventArgs e)
        {
            this.RemoteDrive.Navigate(this.RemoteDrive.DirectoryLocal.FullPath);
        }

        private void menuItemDeleteLocal_Click(object sender, EventArgs e)
        {
            if (this.listViewLocal.SelectedItems.Count == 1)
            {
                RemoteDriveItem item = this.listViewLocal.SelectedItems[0].Tag as RemoteDriveItem;
                this.RemoteDrive.DeleteLocal(item);
            }
        }

        private void menuItemUploadLocal_Click(object sender, EventArgs e)
        {
            if (this.listViewLocal.SelectedItems.Count == 1)
            {
                RemoteDriveItem item = new RemoteDriveItem((this.listViewLocal.SelectedItems[0].Tag as RemoteDriveItem).FullPath);
                this.RemoteDrive.CreateRemote(item);
            }
        }
        private void menuItemOpenLocal_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", this.RemoteDrive.DirectoryLocal.FullPath);
        }

        private void menuItemDownload_Click(object sender, EventArgs e)
        {
            if (this.listViewLocal.SelectedItems.Count == 1)
            {
                RemoteDriveItem item = this.listViewLocal.SelectedItems[0].Tag as RemoteDriveItem;
                this.RemoteDrive.Download(item.FullPath);
            }
        }

        private void menuItemDeleteRemote_Click(object sender, EventArgs e)
        {
            if (this.listViewLocal.SelectedItems.Count == 1)
            {
                RemoteDriveItem item = this.listViewLocal.SelectedItems[0].Tag as RemoteDriveItem;
                if (item.NameMatches() || item.NameAndSizeMatches())
                    this.RemoteDrive.DeleteRemote(item);
            }
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
        {/* TODO
            if (e.ChangeType == WatcherChangeTypes.Changed)
            {
                this.notifyIcon.Icon = Resource.media_white;
                this.Ftp.Update(e.FullPath, this.FtpPath.Resolve(e.FullPath));
            }*/
        }

        private void OnFileCreated(object source, FileSystemEventArgs e)
        { // We react on directory events only, no files.
            /*if (e.ChangeType == WatcherChangeTypes.Created)
            {
                this.notifyIcon.Icon = Resource.media_white;
                if (Directory.Exists(e.FullPath))
                    this.Ftp.CreateDirectory(this.FtpPath.Resolve(e.FullPath));
                else
                    this.Ftp.CreateFileOnly(this.FtpPath.Resolve(e.FullPath));
                this.Local.Cwd.GetCwd();
            }*/
        }

        private void OnFileDeleted(object source, FileSystemEventArgs e)
        {
            /*if (e.ChangeType == WatcherChangeTypes.Deleted)
            {
                this.notifyIcon.Icon = Resource.media_white;
                if (!String.IsNullOrEmpty(Path.GetExtension(e.Name)))
                    this.Ftp.DeleteFile(this.FtpPath.Resolve(e.FullPath));
                else this.Ftp.DeleteFolder(this.FtpPath.Resolve(e.FullPath));
                this.Local.Cwd.GetCwd();
            }*/
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
                //TODO!!!//this.Local.Cwd.AddFile(this.menuItemFileName.Text.Trim());
            this.menuItemFileName.Text = "Name...";
        }

        private void menuItemOpen_Click(object sender, EventArgs e)
        {
            if (this.listViewLocal.SelectedItems.Count == 1)
            {
                RemoteDriveItem item = this.listViewLocal.SelectedItems[0].Tag as RemoteDriveItem;
                Process.Start(item.FullPath);
            }
        }

        private void createFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.menuItemFolderName.Text.Trim()) &&
                this.menuItemFolderName.Text.Trim() != "Name...")
                //TODO//this.Local.Cwd.AddFolder(this.menuItemFolderName.Text.Trim());
            this.menuItemFolderName.Text = "Name...";
        }


        private void buttonPush_Click(object sender, EventArgs e)
        {
            foreach(RemoteDriveItem child in this.RemoteDrive.DirectoryLocal.Children())
                this.RemoteDrive.CreateRemote(child);
        }
    }
}
