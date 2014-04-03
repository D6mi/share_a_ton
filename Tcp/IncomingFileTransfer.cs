using System;
using System.Net;
using System.Net.Sockets;

namespace Share_a_Ton.Tcp
{
    public class IncomingFileTransfer : Transfer
    {
        public IncomingFileTransfer(TcpClient client, string sender, IPEndPoint ipEndPoint, string path, string filename,
            long fileLength, int bufferSize = Int16.MaxValue)
            : base(client, sender, ipEndPoint, path, filename, fileLength, bufferSize)
        {

        }
    }
}