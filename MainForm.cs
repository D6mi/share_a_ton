using System;
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

        private ListViewItem _lastDraggedOverItem;

        public MainForm()
        {
            InitializeComponent();

            #region Initial Setup

            var usernameFromSettings = Settings.Default.Username;

            if (String.IsNullOrWhiteSpace(usernameFromSettings))
            {
                var username = Dns.GetHostName();
                Settings.Default.Username = username;
                Settings.Default.Save();
            }

            var path = Settings.Default.DownloadFolder;

            if (String.IsNullOrWhiteSpace(path))
            {
                MessageBox.Show(Constants.ChooseFileString);

                var fileBrowserDialog = new FolderBrowserDialog();

                DialogResult dr = fileBrowserDialog.ShowDialog();
                if (DialogResult.OK == dr)
                {
                    Settings.Default.DownloadFolder = fileBrowserDialog.SelectedPath;
                    _downloadFolderPath = fileBrowserDialog.SelectedPath;
                    Settings.Default.Save();
                }
            }

            #endregion

            #region Network related

            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ipAddress in host.AddressList)
            {
                if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
                    MyAddress = ipAddress;
            }
            LocalIpEndPoint = new IPEndPoint(MyAddress, Constants.TcpPort);

            _udpManager = new UdpManager(this);
            _udpManager.PeerConnected += AddClientToList;
            _udpManager.PeerDisconnected += RemoveClientFromList;

            _tcpManager = new TcpManager(LocalIpEndPoint, _downloadFolderPath);

            var t = new Thread(_tcpManager.StartListeningForTransfers) {IsBackground = true};
            t.Start();

            #endregion
        }

        public IPAddress MyAddress { get; private set; }
        public IPEndPoint LocalIpEndPoint { get; set; }


        #region Cross-thread Method Invocation

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
                    addNotification.Show(this);
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
                    statusLabel.Text = Constants.MultipleFilesDraggedErrorString;
                }
                else
                {
                    // Get the path of the dropped file.
                    var path = paths[0];
                    var info = new FileInfo(path) { IsReadOnly = false };
                    info.Refresh();

                    // Check if the dropped file's a directory/folder.
                    if ((info.Attributes & FileAttributes.Directory) == FileAttributes.Directory)
                    {
                        statusLabel.ForeColor = Constants.ErrorColor;
                        statusLabel.Text = Constants.DirectoryDraggedErrorString;
                    }
                        
                        // Everything's good, we got a valid file.
                    else
                    {
                        // Get the mouse coordinates relative to the control (ListView).
                        var point = listOfPcs.PointToClient(new Point(e.X, e.Y));

                        // Get the ListViewItem on which the file was dropped.
                        var item = listOfPcs.GetItemAt(point.X, point.Y);

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
                            statusLabel.Text = Constants.DragLocationErrorString;
                        }

                        // Initiate the transfer.
                    }
                }
            }
        }

        private void listOfPcs_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;

            statusLabel.ForeColor = Constants.WarningColor;
            statusLabel.Text =  Constants.DragTipString;
        }


        private void listOfPcs_DragOver(object sender, DragEventArgs e)
        {
            var point = PointToClient(new Point(e.X, e.Y));
            var item = listOfPcs.GetItemAt(point.X, point.Y);

            if (item != null)
            {
                _lastDraggedOverItem = item;
                item.BackColor = Color.LightGreen;
            }

            if (item == null && _lastDraggedOverItem != null)
            {
                _lastDraggedOverItem.BackColor = DefaultBackColor;
            }
        }

        #endregion

        private delegate void AddRemoveCallback(ClientInfo client);

    }
}