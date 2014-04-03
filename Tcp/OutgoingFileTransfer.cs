using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Share_a_Ton.Tcp
{
    public class OutgoingFileTransfer : Transfer
    {
        public OutgoingFileTransfer(TcpClient client, String sender, IPEndPoint ipEndPoint, String path, String filename,
            long fileLength, int bufferSize = short.MaxValue)
            : base(client, sender, ipEndPoint, path, filename, fileLength, bufferSize)
        {
        }

        /// <summary>
        /// Starts sending the associated file to the specified host.
        /// </summary>
        public override void Start()
        {
            var buffer = new byte[BufferSize];
            try
            {
                Client = new TcpClient();
                Client.Connect(RemoteEndPoint);

                OnTransferConnected(EventArgs.Empty);

                using (NetworkStream networkStream = Client.GetStream())
                {
                    var message = new Message(Filename, FileLength);
                    byte[] messageBytes = message.ToBytes();

                    networkStream.Write(messageBytes, 0, messageBytes.Length);

                    networkStream.Read(buffer, 0, buffer.Length);

                    Commands command = Message.ConvertBytesToCommand(buffer);

                    // Determine the appropriate action based on the command contents.
                    if (command == Commands.Accept)
                    {
                        // Open the chosen file for reading. "_path" holds the user specified path.
                        using (var fileStream = new FileStream(Path, FileMode.Open))
                        {
                            OnTransferStarted(EventArgs.Empty);
                            int bytesRead = 0;
                            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                decimal ratio = (decimal) BytesTransferred/FileLength;
                                ratio = ratio*1000;
                                networkStream.Write(buffer, 0, bytesRead);
                                BytesTransferred += bytesRead;

                                OnTransferredChunk(new TransferArgs((int) ratio));
                            }
                        }

                        // Wait for the [Success] or [Error] response.
                        networkStream.Read(buffer, 0, 4);

                        // Convert the bytes received to a command.
                        command = Message.ConvertBytesToCommand(buffer);

                        // Act appropriately.
                        if (command == Commands.Success)
                        {
                            OnTransferCompleted(EventArgs.Empty);
                            MessageBox.Show("The host successfully received the file!");
                        }
                        else
                            MessageBox.Show("The transfer was unsuccessful!");
                    }
                    else if (command == Commands.Reject)
                    {
                        OnTransferDisconnected(EventArgs.Empty);
                        MessageBox.Show("The host rejected the transfer!");
                    }
                }
            }
            catch (SocketException sEx)
            {
                if (sEx.SocketErrorCode == SocketError.ConnectionAborted)
                {
                    OnTransferDisconnected(EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                Client.Close();
            }
        }

        public override void Abort()
        {
            Client.Close();
        }
    }
}