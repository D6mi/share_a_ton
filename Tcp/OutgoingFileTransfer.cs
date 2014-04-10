using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using Newtonsoft.Json;
using Share_a_Ton.Utilities;

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
        ///     Starts sending the associated file to the specified host.
        /// </summary>
        public override void Start()
        {
            var buffer = new byte[Constants.DefaultBufferSize];
            try
            {
                Client = new TcpClient();
                Client.Connect(RemoteEndPoint);

                OnTransferConnected(EventArgs.Empty);

                NetworkStream = Client.GetStream();
                var jsonMessage = new JSONMessage
                {
                    Command = Commands.Send,
                    FileLength = FileLength,
                    Filename = Filename,
                    Sender = Options.Username
                };

                var json = JsonConvert.SerializeObject(jsonMessage);

                var writer = new StreamWriter(NetworkStream) {AutoFlush = true};
                writer.WriteLine(json);

                NetworkStream.Read(buffer, 0, buffer.Length);

                var command = Message.ConvertBytesToCommand(buffer);

                // Determine the appropriate action based on the command contents.
                if (command == Commands.Accept)
                {
                    // Open the chosen file for reading. "_path" holds the user specified path.
                    using (var fileStream = new FileStream(Path, FileMode.Open))
                    {
                        OnTransferStarted(EventArgs.Empty);
                        int bytesRead;
                        while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            var ratio = (decimal) BytesTransferred/FileLength;
                            ratio = ratio*1000;
                            NetworkStream.Write(buffer, 0, bytesRead);
                            BytesTransferred += bytesRead;

                            OnTransferredChunk(new TransferArgs((int) ratio));
                        }
                    }

                    // Wait for the [Success] or [Error] response.
                    NetworkStream.Read(buffer, 0, 4);

                    // Convert the bytes received to a command.
                    command = Message.ConvertBytesToCommand(buffer);

                    // Act appropriately.
                    if (command == Commands.Success)
                    {
                        OnTransferCompleted(EventArgs.Empty);
                    }

                    if (command == Commands.Abort)
                    {
                        OnTransferDisconnected(EventArgs.Empty);
                    }
                }
                else if (command == Commands.Reject)
                {
                    OnTransferRejected(EventArgs.Empty);
                }

            }
            catch (ObjectDisposedException ex)
            {
                Debug.Write("Exception : ");
                Debug.WriteLine(ex.ToString());
            }
            catch (Exception ex)
            {
                Debug.Write("Exception : ");
                Debug.WriteLine(ex.ToString());
            }
            finally
            {
                Client.Close();
                NetworkStream.Close();
            }
        }

        public override void Abort()
        {
            Client.Close();
        }
    }
}