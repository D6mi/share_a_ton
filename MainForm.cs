using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using Share_a_Ton.Forms;
using Share_a_Ton.Properties;
using Share_a_Ton.Tcp;
using Share_a_Ton.Udp;
using Share_a_Ton.Utilities;

namespace Share_a_Ton
{
    public partial class MainForm : Form
    {
        private readonly String _downloadFolderPath;
        private readonly TcpManager _tcpManager;
        private readonly UdpManager _udpManager;
        private readonly FileSystemWatcher _watcher;

        public MainForm()
        {
            InitializeComponent();

            #region Initial Setup

            string usernameFromSettings = Settings.Default.Username;

            if (String.IsNullOrWhiteSpace(usernameFromSettings))
            {
                string username = Dns.GetHostName();
                Settings.Default.Username = username;
                Settings.Default.Save();
            }

            string path = Settings.Default.DownloadFolder;

            if (String.IsNullOrWhiteSpace(path))
            {
                MessageBox.Show(Strings.ChooseFileString);

                var fileBrowserDialog = new FolderBrowserDialog();

                DialogResult dr = fileBrowserDialog.ShowDialog();

                if (DialogResult.OK == dr)
                {
                    Settings.Default.DownloadFolder = fileBrowserDialog.SelectedPath;
                    Options.DownloadFolderPath = fileBrowserDialog.SelectedPath;
                    Settings.Default.Save();
                }
            }

            #endregion

            string directory = Directory.GetParent(Options.DownloadFolderPath).FullName;
            Debug.WriteLine("DIRECTORY : " + directory);
            _watcher = new FileSystemWatcher(directory) {NotifyFilter = NotifyFilters.DirectoryName};
            _watcher.Deleted += OnFileDeleted;
            _watcher.EnableRaisingEvents = true;

            Options.DownloadFolderChanged += OnDownloadFolderChanged;

            #region Network related

            var host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress ipAddress in host.AddressList)
            {
                if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
                    MyAddress = ipAddress;
            }
            LocalIpEndPoint = new IPEndPoint(MyAddress, Constants.TcpPort);

            _udpManager = new UdpManager(this);
            _udpManager.PeerConnected += AddClientToList;
            _udpManager.PeerDisconnected += RemoveClientFromList;

            _tcpManager = new TcpManager(LocalIpEndPoint, Options.DownloadFolderPath);

            var t = new Thread(_tcpManager.StartListeningForTransfers) {IsBackground = true};
            t.Start();

            #endregion
        }

        public IPAddress MyAddress { get; private set; }
        public IPEndPoint LocalIpEndPoint { get; set; }

        #region Cross-thread Method Invocation

        private void SetTextWithColor(String text, Color color)
        {
            if (statusLabel.InvokeRequired)
            {
                SetTextWithColorCallback textWithColorback = SetTextWithColor;
                Invoke(textWithColorback, new object[] { text, color });
            }
            else
            {
                statusLabel.ForeColor = color;
                statusLabel.Text = text;
            }
        }

        public void AddClientToList(object sender, ClientArgs args)
        {
            AddClientToList(args.Client);
        }

        public void RemoveClientFromList(object sender, ClientArgs args)
        {
            RemoveClientFromList(args.Client);
        }

        public void AddClientToList(ClientInfo client)
        {
            if (listOfPcs.InvokeRequired)
            {
                AddRemoveCallback arc = AddClientToList;
                Invoke(arc, new object[] {client});
            }
            else
            {
                ListViewItem clientItem = Helpers.CreateListViewItem(listOfPcs, client);
                if (!Helpers.DoesListContainClient(client, listOfPcs))
                {
                    listOfPcs.Items.Add(clientItem);

                    var addNotification = new Notification(DateTime.Now.ToShortTimeString() + " : Client joined",
                        client + " has joined!");
                    new Thread(() => addNotification.ShowDialog()).Start();
                }
            }
        }

        public void RemoveClientFromList(ClientInfo client)
        {
            if (listOfPcs.InvokeRequired)
            {
                AddRemoveCallback arc = RemoveClientFromList;
                Invoke(arc, new object[] {client});
            }
            else
            {
                if (Helpers.DoesListContainClient(client, listOfPcs))
                {
                    foreach (ListViewItem item in listOfPcs.Items)
                    {
                        if (item.Text == client.ClientName)
                            item.Remove();
                    }

                    var disconnectNotification =
                        new Notification(DateTime.Now.ToShortTimeString() + " : Client disconnected",
                            client.ClientName + " has disconnected!");
                    disconnectNotification.Show(this);
                }
            }
        }

        #endregion

        #region Form Events

        private void mainForm_Load(object sender, EventArgs e)
        {
            var data = new UdpData(UdpCommand.AddRefresh, Options.Username);
            _udpManager.Broadcast(data);
        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var shutdownData = new UdpData(UdpCommand.Remove, Options.Username);
            _udpManager.Broadcast(shutdownData);
        }

        private void optionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var options = new OptionsForm();
            options.ShowDialog(this);
        }

        private void listOfPcs_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var client = (ClientInfo) listOfPcs.SelectedItems[0].Tag;
                clientNameLabel.Text = client.ClientName;
                ipAddressLabel.Text = client.IpEndPoint.Address.ToString();
            }
            catch (Exception)
            {
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region Drag & Drop

        private void listOfPcs_DragDrop(object sender, DragEventArgs e)
        {
            // If the data dropped on the Listbox is a "File Drop".
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Check if there's more than one file dropped on the Listbox.
                var paths = (String[]) e.Data.GetData(DataFormats.FileDrop);

                // If there's more than one file dropped on the Listbox, notify the user.
                if (paths.Length > 1)
                {
                    statusLabel.ForeColor = Constants.ErrorColor;
                    statusLabel.Text = Strings.MultipleFilesDraggedErrorString;
                }
                else
                {
                    // Get the path of the dropped file.
                    string path = paths[0];
                    var info = new FileInfo(path) {IsReadOnly = false};
                    info.Refresh();

                    // Check if the dropped file's a directory/folder.
                    if ((info.Attributes & FileAttributes.Directory) == FileAttributes.Directory)
                    {
                        statusLabel.ForeColor = Constants.ErrorColor;
                        statusLabel.Text = Strings.DirectoryDraggedErrorString;
                    }
                        
                        // Everything's good, we got a valid file.
                    else
                    {
                        // Get the mouse coordinates relative to the control (ListView).
                        Point point = listOfPcs.PointToClient(new Point(e.X, e.Y));

                        // Get the ListViewItem on which the file was dropped.
                        ListViewItem item = listOfPcs.GetItemAt(point.X, point.Y);

                        if (item != null)
                        {
                            // Get the ClientInfo from the Tag property of the ListViewItem.
                            var client = (ClientInfo) item.Tag;

                            // Create a IpEndPoint with the Client's Ip Address and the default Tcp Port used for
                            // file transfers.
                            var remoteIpEndPoint = new IPEndPoint(client.IpEndPoint.Address, Constants.TcpPort);

                            // Initiate a new Outgoing File Transfer with the Full File Path that was acquired with
                            // the FileDrop and use the FileInfo.Name as the file name, without the whole path.
                            _tcpManager.SendFile(client.ClientName, remoteIpEndPoint,
                                path, info.Name, info.Length);
                        }
                        else
                        {
                            statusLabel.ForeColor = Constants.ErrorColor;
                            statusLabel.Text = Strings.DragLocationErrorString;
                        }

                        statusLabel.Text = "";
                    }
                }
            }
        }

        private void listOfPcs_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;

            statusLabel.ForeColor = Constants.WarningColor;
            statusLabel.Text = Strings.DragTipString;
        }

        private void listOfPcs_DragLeave(object sender, EventArgs e)
        {
            statusLabel.Text = String.Empty;
        }

        #endregion

        #region File Watcher

        private void OnFileDeleted(object sender, EventArgs e)
        {
            Options.IsDownloadFolderSet = false;
            Options.DownloadFolderPath = "";
            Settings.Default.DownloadFolder = "";

            Debug.WriteLine("FILE WATCHER : The directory was deleted!");

            SetTextWithColor(Strings.FileDeletedWarning, Constants.WarningColor);
        }

        private void OnDownloadFolderChanged(object sender, EventArgs e)
        {
            var args = (FolderArgs) e;

            if (!String.IsNullOrWhiteSpace(args.Path))
            {
                _watcher.Path = Directory.GetParent(args.Path).FullName;
                Debug.WriteLine("FILE WATCHER : Now watching @" + _watcher.Path);
            }
        }
        
        #endregion

        private delegate void SetTextWithColorCallback(string text, Color color);
        private delegate void AddRemoveCallback(ClientInfo client);
    }
}