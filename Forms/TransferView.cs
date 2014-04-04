using System;
using System.Threading;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Taskbar;
using Share_a_Ton.Tcp;
using Share_a_Ton.Utilities;

namespace Share_a_Ton.Forms
{
    public partial class TransferView : Form
    {
        private readonly Transfer _transfer;
        private bool _isRunning;

        public TransferView(Transfer transfer)
        {
            InitializeComponent();

            _transfer = transfer;

            transferProgress.Minimum = 0;
            transferProgress.Maximum = 1000;

            filenameLabel.Text = _transfer.Filename;
            FormatFileLengthLabel();

            if (_transfer is OutgoingFileTransfer)
                senderTextLabel.Text = "Sending to : ";
            else
                senderTextLabel.Text = "Sender : ";
            senderLabel.Text = _transfer.Sender;

            _transfer.Connected += TransferConnected;
            _transfer.Disconnected += TransferDisconnected;

            _transfer.Started += TransferStarted;
            _transfer.Completed += TransferCompleted;

            _transfer.TransferredChunk += TransferredPart;

            var runningThread = new Thread(_transfer.Start) {IsBackground = true};
            runningThread.Start();
        }

        public Transfer Transfer
        {
            get { return _transfer; }
        }

        #region Event Methods

        public void TransferConnected(object sender, EventArgs e)
        {
            SetText("Connected...");
            _isRunning = true;
        }

        public void TransferDisconnected(object sender, EventArgs e)
        {
            SetText("Transfer rejected...");

            SetButtonText("Okay");
            _isRunning = false;
        }

        public void TransferStarted(object sender, EventArgs e)
        {
            SetText("Transfer started...");
            _isRunning = false;
        }

        public void TransferCompleted(object sender, EventArgs e)
        {
            SetText("Transfer completed!");

            SetButtonText("Close");
            _isRunning = false;
        }

        public void TransferredPart(object sender, EventArgs e)
        {
            var args = (TransferArgs) e;
            int value = args.BytesTransfered;
            SetProgress(value);
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            if (_isRunning)
            {
                _transfer.Abort();
                statusLabel.Text = "Transfer aborted!";
                actionButton.Text = "Close";
            }
            else
                Close();
        }

        private void SetText(string text)
        {
            if (statusLabel.InvokeRequired)
            {
                SetTextCallback textCallback = SetText;
                Invoke(textCallback, new object[] {text});
            }
            else
            {
                statusLabel.Text = text;
            }
        }

        private void SetProgress(int value)
        {
            if (transferProgress.InvokeRequired)
            {
                SetProgressCallback progressCallback = SetProgress;
                Invoke(progressCallback, new object[] {value});
            }
            else
            {
                transferProgress.Value = value;

                if (TaskbarManager.IsPlatformSupported)
                {
                    TaskbarManager taskbarInstance = TaskbarManager.Instance;

                    taskbarInstance.SetProgressState(TaskbarProgressBarState.Normal);
                    taskbarInstance.SetProgressValue(value, 100);

                    taskbarInstance.SetProgressState(TaskbarProgressBarState.NoProgress);
                }
            }
        }

        private void SetButtonText(string text)
        {
            if (actionButton.InvokeRequired)
            {
                SetButtonTextCallback textCallback = SetButtonText;
                Invoke(textCallback, new object[] {text});
            }
            else
            {
                actionButton.Text = text;
            }
        }

        private void FormatFileLengthLabel()
        {
            decimal fileLength = _transfer.FileLength;

            if (fileLength < Constants.KiloByteTreshold)
            {
                filelengthLabel.Text = (fileLength/1000).ToString("F") + " kb";
            }
            else if (fileLength < Constants.MegaByteTreshold)
            {
                filelengthLabel.Text = (fileLength/1000).ToString("F") + " mb";
            }
            else if (fileLength > Constants.MegaByteTreshold)
            {
                filelengthLabel.Text = (fileLength/1000000000).ToString("F") + " gb";
            }
        }


        private delegate void SetButtonTextCallback(string text);

        private delegate void SetProgressCallback(int value);

        private delegate void SetTextCallback(string text);
    }
}