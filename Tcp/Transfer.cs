using System;
using System.Net;
using System.Net.Sockets;

namespace Share_a_Ton.Tcp
{
    public abstract class Transfer
    {
        protected TcpClient Client;
        protected string Path;
        protected IPEndPoint RemoteEndPoint;

        /// <summary>
        ///     Creates a new File transfer object, dedicated to a specific file transfer.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="ipEndPoint">The remote ip end point that the clients connects to and wants to send the file.</param>
        /// <param name="path">The fully qualified path of the file to be sent.</param>
        /// <param name="filename">The name of the file to be sent.</param>
        /// <param name="fileLength">The length of the file to be sent.</param>
        /// <param name="bufferSize">The size of the sending buffer. 32767 by default.</param>
        protected Transfer(TcpClient client, String sender, IPEndPoint ipEndPoint, String path, String filename,
            long fileLength, int bufferSize = short.MaxValue)
        {
            Client = client;
            RemoteEndPoint = ipEndPoint;
            Path = path;
            Filename = filename;
            FileLength = fileLength;
            BufferSize = bufferSize;
            Sender = sender;
        }

        public string Filename { get; set; }
        public long FileLength { get; set; }
        public int BufferSize { get; set; }
        public long BytesTransferred { get; set; }
        public String Sender { get; set; }

        public event EventHandler Started;
        public event EventHandler Connected;
        public event EventHandler Disconnected;
        public event EventHandler Completed;
        public event EventHandler<TransferArgs> TransferredChunk;

        protected virtual void OnTransferStarted(EventArgs e)
        {
            if (Started != null)
            {
                Started(this, e);
            }
        }

        protected virtual void OnTransferCompleted(EventArgs e)
        {
            if (Completed != null)
            {
                Completed(this, e);
                Client.Close();
            }
        }

        protected virtual void OnTransferConnected(EventArgs e)
        {
            if (Connected != null)
            {
                Connected(this, e);
            }
        }

        protected virtual void OnTransferDisconnected(EventArgs e)
        {
            if (Disconnected != null)
            {
                Disconnected(this, e);
            }
        }

        protected virtual void OnTransferredChunk(TransferArgs e)
        {
            if (TransferredChunk != null)
            {
                TransferredChunk(this, e);
            }
        }

        public virtual void Start()
        {
        }

        public virtual void Abort()
        {
        }
    }
}