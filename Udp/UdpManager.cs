using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using Share_a_Ton.Utilities;

namespace Share_a_Ton.Udp
{
    public class UdpManager
    {
        private const int ListenPort = Constants.UdpPort;

        public EventHandler<ClientArgs> PeerConnected;
        public EventHandler<ClientArgs> PeerDisconnected;

        public UdpManager(Form form)
        {
            Form = form as MainForm;
            Done = false;

            ListeningThread = new Thread(StartListening) {IsBackground = true};
            ListeningThread.Start();
        }

        public bool Done { get; set; }
        public MainForm Form { get; set; }

        public Thread ListeningThread { get; set; }

        private void StartListening()
        {
            var endPoint = new IPEndPoint(IPAddress.Any, ListenPort);

            using (var listener = new UdpClient(endPoint))
            {
                try
                {
                    while (!Done)
                    {
                        // Listen for incoming UDP transmissions
                        byte[] bytesReceived = listener.Receive(ref endPoint);

                        // Create a Data object from the received bytes
                        var receivedData = new UdpData(bytesReceived);

                        // Created a new ClientInfo instance based on the Data object
                        var client = new ClientInfo
                        {
                            ClientName = receivedData.Name,
                            IpEndPoint = endPoint
                        };

                        // If this is NOT my Ip Address
                        if (!endPoint.Address.Equals(Form.MyAddress))
                        {
                            // In regards to the "Command" from the Data object, act appriopriately
                            switch (receivedData.Command)
                            {
                                case UdpCommand.Add:
                                    if (PeerConnected != null)
                                        PeerConnected(this, new ClientArgs(client));

                                    // Form.AddClientToList(client);
                                    break;
                                case UdpCommand.Remove:
                                    if (PeerDisconnected != null)
                                        PeerDisconnected(this, new ClientArgs(client));

                                    // Form.RemoveClientFromList(client);
                                    break;
                                case UdpCommand.AddRefresh:
                                    if(PeerConnected != null)
                                        PeerConnected(this, new ClientArgs(client));

                                    // Form.AddClientToList(client);
                                    var data = new UdpData(UdpCommand.Add, Options.Username);
                                    Broadcast(data);
                                    break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
            }
        }

        public int Broadcast(UdpData data)
        {
            var bytesSent = 0;
            var broadcastAddress = IPAddress.Parse("192.168.1.255");
            var broadcastEndPoint = new IPEndPoint(broadcastAddress, ListenPort);

            using (var client = new UdpClient())
            {
                try
                {
                    var dataToSend = data.ToBytes();
                    bytesSent = client.Send(dataToSend, dataToSend.Length, broadcastEndPoint);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            return bytesSent;
        }

        public int Send(UdpData data, IPEndPoint endPoint)
        {
            var bytesSent = 0;
            using (var client = new UdpClient())
            {
                try
                {
                    var dataToSend = data.ToBytes();
                    client.Connect(endPoint);
                    bytesSent = client.Send(dataToSend, dataToSend.Length);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
            }

            return bytesSent;
        }
    }
}