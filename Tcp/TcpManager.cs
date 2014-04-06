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

        public TcpManager(IPEndPoint localIpEndPoint, String downloadFolderPath)
        {

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
                    var client = _listener.AcceptTcpClient();
                    var netStream = client.GetStream();

                    var reader = new StreamReader(netStream);
                    var json = reader.ReadLine();

                    var message = new JSONMessage();
                    message = JsonConvert.DeserializeObject<JSONMessage>(json);

                    var filename = message.Filename;
                    var path = Options.DownloadFolderPath + message.Filename;
                    var fileLength = message.FileLength;
                    var sender = message.Sender;

                    var confirmationNeeded = Options.ConfirmationNeeded;

                    if (confirmationNeeded)
                    {
                        // Ask the user whether he/she wants to accept the file.
                        var dr = MessageBox.Show("Do you want to accept this file : " + message.Filename,
                            "Accept or reject?", MessageBoxButtons.OKCancel);

                        // If the user says yes, send the accept response and start accepting the file data.
                        if (dr == DialogResult.OK)
                        {
                            // The Message class static methods for transforming commands into byte arrays.
                            var responseBytes = Message.ConvertCommandToBytes(Commands.Accept);

                            // Send the accept response.
                            netStream.Write(responseBytes, 0, responseBytes.Length);

                            var transfer = new IncomingFileTransfer(client, sender, null, path, filename,
                                fileLength);
                            var tView = new TransferView(transfer);
                            tView.ShowDialog();
                        }
                        else
                        {
                            var responseBytes = Message.ConvertCommandToBytes(Commands.Reject);
                            netStream.Write(responseBytes, 0, responseBytes.Length);
                        }
                    }
                    else
                    {
                        var responseBytes = Message.ConvertCommandToBytes(Commands.Accept);

                        // Send the accept response.
                        netStream.Write(responseBytes, 0, responseBytes.Length);

                        var transfer = new IncomingFileTransfer(client, message.Sender, null, path, filename,
                            fileLength);
                        var tView = new TransferView(transfer);
                        tView.ShowDialog();
                    }
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
    }
}