using System;

namespace Share_a_Ton.Udp
{
    public class ClientArgs : EventArgs
    {
        private readonly ClientInfo _client;

        public ClientArgs()
        {
            _client = new ClientInfo()
            {
                ClientName = "Not specified...",
                IpEndPoint = null
            };
        }
        
        public ClientArgs(ClientInfo info)
        {
            _client = info;
        }

        public ClientInfo Client { get { return _client; } }
    }
}