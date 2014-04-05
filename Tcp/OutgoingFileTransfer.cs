﻿using System;
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
        /// Starts sending the associated file to the specified host.
        /// </summary>
        public override void Start()
        {
            var buffer = new byte[Constants.DefaultBufferSize];
            try
            {
                Client = new TcpClient();
                Client.Connect(RemoteEndPoint);

                OnTransferConnected(EventArgs.Empty);

                using (NetworkStream networkStream = Client.GetStream())
                {
                    var jsonMessage = new JSONMessage()
                    {
                        Command = Commands.Send,
                        FileLength = FileLength,
                        Filename = Filename,
                        Sender = "D6mi-PC"
                    };

                    string json = JsonConvert.SerializeObject(jsonMessage);

                    var writer = new StreamWriter(networkStream) { AutoFlush = true };
                    writer.WriteLine(json);

                    networkStream.Read(buffer, 0, buffer.Length);

                    Commands command = Message.ConvertBytesToCommand(buffer);

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
                        }
                    }
                    else if (command == Commands.Reject)
                    {
                        OnTransferDisconnected(EventArgs.Empty);
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