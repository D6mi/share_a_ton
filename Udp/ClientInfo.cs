using System;
using System.Net;

namespace Share_a_Ton.Udp
{
    public class ClientInfo
    {
        public String ClientName { get; set; }
        public IPEndPoint IpEndPoint { get; set; }

        public override string ToString()
        {
            return ClientName;
        }

        /// <summary>
        /// Compares the two clients and returns whether they're the same.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var client = (ClientInfo)obj;

            if (ClientName == client.ClientName &&
                IpEndPoint.Address.ToString() == client.IpEndPoint.Address.ToString())
                return true;

            return false;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}