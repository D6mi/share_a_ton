using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using Share_a_Ton.Forms;
using Share_a_Ton.Utilities;

namespace Share_a_Ton.Tcp
{
    public class IncomingFileTransfer : Transfer
    {
        private byte[] _buffer;
        private NetworkStream _networkStream;
        
        private bool _transferError;
        private bool _transferSuccess;

        public IncomingFileTransfer(TcpClient client, string sender, IPEndPoint ipEndPoint, string path, string filename,
            long fileLength, int bufferSize = Int16.MaxValue)
            : base(client, sender, ipEndPoint, path, filename, fileLength, bufferSize)
        {
            _buffer = new byte[BufferSize];

            Client = client;
            _networkStream = client.GetStream();
        }

        public override void Start()
        {
            try
            {
                using (var fileStream = new FileStream(Path, FileMode.Create))
                {
                    OnTransferStarted(EventArgs.Empty);
                    long remaining = FileLength;

                    while (remaining > 0)
                    {
                        var bytesRead = _networkStream.Read(_buffer, 0, Constants.DefaultBufferSize);
                        fileStream.Write(_buffer, 0, bytesRead);
                        remaining -= bytesRead;

                        var ratio = (decimal)(FileLength - remaining) / FileLength;
                        ratio = ratio * 1000;
                        OnTransferredChunk(new TransferArgs((int)ratio));

                        if (IsClientDisconnected(Client.Client))
                        {
                            var notification = new Notification("Transfer interrupted",
                                "The client has terminated the transfer");
                            notification.Show();

                            _transferError = true;

                            throw new Exception("The client has terminated the connection!");
                        }
                    }
                }

                if (File.Exists(Path) && !_transferError)
                {
                    _transferSuccess = true;
                    byte[] successBytes = Message.ConvertCommandToBytes(Commands.Success);
                    _networkStream.Write(successBytes, 0, successBytes.Length);
                    OnTransferCompleted(EventArgs.Empty);
                }
                else
                {
                    byte[] errorBytes = Message.ConvertCommandToBytes(Commands.Success);
                    _networkStream.Write(errorBytes, 0, errorBytes.Length);
                }

                // If the file was successfully transfered, send the Success message notifying the client that
                // the operation ended successfully.
                if (_transferSuccess && !_transferError)
                {
                    if (Options.AskForDownloadFolder)
                    {
                        var dr =
                            MessageBox.Show("Do you want to open the directory where the file was saved?",
                                "Confirmation", MessageBoxButtons.OKCancel);
                        if (dr == DialogResult.OK)
                            Process.Start("explorer", Options.DownloadFolderPath);
                    }
                    else if (Options.AutoOpenDownloadFolder)
                    {
                        Process.Start("explorer", Options.DownloadFolderPath);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Client.Close();
                _networkStream.Close();
            }
        }

        public override void Abort()
        {

        }

        private static bool IsClientDisconnected(Socket clientSocket)
        {
            if (clientSocket.Poll(0, SelectMode.SelectRead))
            {
                var buffer = new byte[1];
                if (clientSocket.Receive(buffer, SocketFlags.Peek) == 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}