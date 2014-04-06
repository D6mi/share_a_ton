using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using Share_a_Ton.Utilities;

namespace Share_a_Ton.Tcp
{
    public class IncomingFileTransfer : Transfer
    {
        private byte[] _buffer;
        private readonly NetworkStream _networkStream;
        
        private bool _transferError;
        private bool _transferSuccess;

        public IncomingFileTransfer(TcpClient client, string sender, IPEndPoint ipEndPoint, string path, string filename,
            long fileLength, int bufferSize = Int16.MaxValue)
            : base(client, sender, ipEndPoint, path, filename, fileLength, bufferSize)
        {
            _buffer = new byte[BufferSize];
            _transferSuccess = false;
            _transferError = false;

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
                    var remaining = FileLength;

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
                            OnTransferDisconnected(EventArgs.Empty);
                            _transferError = true;
                            break;
                        }
                    }
                }

                if (File.Exists(Path) && !_transferError)
                {
                    _transferSuccess = true;
                    var successBytes = Message.ConvertCommandToBytes(Commands.Success);
                    _networkStream.Write(successBytes, 0, successBytes.Length);
                    OnTransferCompleted(EventArgs.Empty);
                }
                else
                {
                    var errorBytes = Message.ConvertCommandToBytes(Commands.Error);
                    _networkStream.Write(errorBytes, 0, errorBytes.Length);

                    PerformCleanupOnDisconnect();
                }

                // If the file was successfully transfered, send the Success message notifying the client that
                // the operation ended successfully.
                if (_transferSuccess && !_transferError)
                {
                    if (Options.AskForDownloadFolder)
                    {
                        var dr = MessageBox.Show("Do you want to open the directory where the file was saved?",
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
                Debug.WriteLine(ex.ToString());
            }
            finally
            {
                Client.Close();
                _networkStream.Close();
            }
        }

        public override void Abort()
        {
            Client.Close();
        }

        private void PerformCleanupOnDisconnect()
        {
            if (File.Exists(Path))
            {
                File.Delete(Path);
            }
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