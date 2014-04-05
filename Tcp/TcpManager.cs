using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using Newtonsoft.Json;
using Share_a_Ton.Forms;
using Share_a_Ton.Utilities;

namespace Share_a_Ton.Tcp
{
    internal class TcpManager
    {
        private readonly TcpListener _listener;
        public Transfer CurrentTransfer;

        private String _downloadFolderPath;
        private bool _fileCopied;

        public TcpManager(IPEndPoint localIpEndPoint, String downloadFolderPath)
        {
            _downloadFolderPath = downloadFolderPath;

            _listener = new TcpListener(localIpEndPoint);
            _listener.Start();
        }

        public void StartListeningForTransfers()
        {
            while (true)
            {
                #region Code

                try
                {
                    // Buffer for reading.
                    var buffer = new byte[Constants.DefaultBufferSize];

                    TcpClient client = _listener.AcceptTcpClient();
                    NetworkStream netStream = client.GetStream();

                    var reader = new StreamReader(netStream);
                    string json = reader.ReadLine();

                    var message = new JSONMessage();
                    message = JsonConvert.DeserializeObject<JSONMessage>(json);

                    var filename = message.Filename;
                    var path = _downloadFolderPath + message.Filename;
                    var fileLength = message.FileLength;

                    // Ask the user whether he/she wants to accept the file.
                    DialogResult dr = MessageBox.Show("Do you want to accept this file : " + message.Filename,
                        "Accept or reject?", MessageBoxButtons.OKCancel);

                    // If the user says yes, send the accept response and start accepting the file data.
                    if (dr == DialogResult.OK)
                    {
                        // The Message class static methods for transforming commands into byte arrays.
                        byte[] responseBytes = Message.ConvertCommandToBytes(Commands.Accept);

                        // Send the accept response.
                        netStream.Write(responseBytes, 0, responseBytes.Length);

                        var transfer = new IncomingFileTransfer(client, "Sender", null, path, filename,
                            fileLength);
                        var tView = new TransferView(transfer);
                        tView.ShowDialog();

                        #region Transfer Code

                        /*
                                // Open or create the file for saving.
                                using (var fileStream = new FileStream(_path, FileMode.Create))
                                {
                                    long remaining = message.FileLength;

                                    while (remaining > 0)
                                    {
                                        int bytesRead = netStream.Read(buffer, 0, Constants.DefaultBufferSize);
                                        fileStream.Write(buffer, 0, bytesRead);
                                        remaining -= bytesRead;

                                        if (IsClientDisconnected(client.Client))
                                        {
                                            var notification = new Notification("Transfer interrupted",
                                                "The client has terminated the transfer");
                                            notification.Show();

                                            _transferError = true;

                                            throw new Exception("The client has terminated the connection!");
                                        }
                                    }
                                }

                                if (File.Exists(_path) && !_transferError)
                                {
                                    _fileCopied = true;
                                    byte[] successBytes = Message.ConvertCommandToBytes(Commands.Success);
                                    netStream.Write(successBytes, 0, successBytes.Length);
                                }
                                else
                                {
                                    byte[] errorBytes = Message.ConvertCommandToBytes(Commands.Success);
                                    netStream.Write(errorBytes, 0, errorBytes.Length);
                                }
                               */

                        #endregion

                        // If the user rejected the transfer, send the Reject response.
                    }
                    else
                    {
                        byte[] responseBytes = Message.ConvertCommandToBytes(Commands.Reject);
                        netStream.Write(responseBytes, 0, responseBytes.Length);
                        _fileCopied = false;
                    }

                    /*
                    // If the file was successfully transfered, send the Success message notifying the client that
                    // the operation ended successfully.
                    if (_fileCopied && !_transferError)
                    {
                        if (Options.AskForDownloadFolder)
                        {
                            DialogResult dirResult =
                                MessageBox.Show("Do you want to open the directory where the file was saved?",
                                    "Confirmation", MessageBoxButtons.OKCancel);
                            if (dirResult == DialogResult.OK)
                                Process.Start("explorer", _downloadFolderPath);
                        }
                        else if (Options.AutoOpenDownloadFolder)
                        {
                            Process.Start("explorer", _downloadFolderPath);
                        }
                    }
                    */

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                #endregion
            }
        }

        public void SendFile(String sender, IPEndPoint ipEndPoint, String path, String filename,
            long fileLength, int bufferSize = short.MaxValue)
        {
            var transfer = new OutgoingFileTransfer(null, sender, ipEndPoint, path, filename, fileLength);
            var tView = new TransferView(transfer);
            tView.Show();
        }

        public void UpdateDownloadPath(string downloadFolderPath)
        {
            _downloadFolderPath = downloadFolderPath + "\\";
        }
    }
}