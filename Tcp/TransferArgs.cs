using System;

namespace Share_a_Ton.Tcp
{
    public class TransferArgs : EventArgs
    {
        private readonly int _bytesTransfered;

        public TransferArgs()
        {
            _bytesTransfered = 0;
        }

        public TransferArgs(int bytesTransfered)
        {
            _bytesTransfered = bytesTransfered;
        }

        public int BytesTransfered
        {
            get { return _bytesTransfered; }
        }
    }
}