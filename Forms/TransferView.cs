using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Taskbar;
using Share_a_Ton.Tcp;
using Share_a_Ton.Utilities;
using Timer = System.Threading.Timer;

namespace Share_a_Ton.Forms
{
    public partial class TransferView : Form
    {
        private readonly Transfer _transfer;
        private Timer _fadeOutTimer;
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
            {
                senderTextLabel.Text = "Sending to : ";
                _transfer.Rejected += TransferRejected;
            }
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
            SetTextWithColor("Queued!", Constants.SuccessColor);
            _isRunning = true;
        }

        public void TransferDisconnected(object sender, EventArgs e)
        {
            SetTextWithColor("Transfer terminated!", Constants.ErrorColor);

            SetButtonText("Okay");
            _isRunning = false;
            
            ScheduleFadeOut();
        }

        public void TransferRejected(object sender, EventArgs e)
        {
            SetTextWithColor("Transfer rejected!", Constants.ErrorColor);

            SetButtonText("Okay");
            _isRunning = false;

            ScheduleFadeOut();
        }

        public void TransferStarted(object sender, EventArgs e)
        {
            SetTextWithColor("Transfer started!", Constants.SuccessColor);
            _isRunning = true;
        }

        public void TransferCompleted(object sender, EventArgs e)
        {
            SetTextWithColor("Transfer completed!", Constants.SuccessColor);

            SetButtonText("Close");
            _isRunning = false;

            ScheduleFadeOut();
        }

        public void TransferredPart(object sender, EventArgs e)
        {
            var args = (TransferArgs) e;
            var value = args.BytesTransfered;

            SetTextWithColor("Transferring data...", Constants.WarningColor);
            SetProgress(value);

            if(!_isRunning)
                statusLabel.Text = "Transfer aborted!";

        }

        #endregion

        private void actionButton_Click(object sender, EventArgs e)
        {
            if (_isRunning)
            {
                _transfer.Abort();
                statusLabel.Text = "Transfer aborted!";
                actionButton.Text = "Close";
                _isRunning = false;
            }
            else
                Close();
        }

        private void TransferView_FormClosing(object sender, FormClosingEventArgs e)
        {
            FadeOut();
        }

        #region Cross-Thread Invocation


        private void SetText(String text) 
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

        private void SetTextWithColor(String text, Color color)
        {
            if (statusLabel.InvokeRequired)
            {
                SetTextWithColorCallback textWithColorback = SetTextWithColor;
                Invoke(textWithColorback, new object[] { text, color });
            }
            else
            {
                statusLabel.ForeColor = color;
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
                    var taskbarInstance = TaskbarManager.Instance;

                    taskbarInstance.SetProgressState(TaskbarProgressBarState.Normal);
                    taskbarInstance.SetProgressValue(value, 100);

                    taskbarInstance.SetProgressState(TaskbarProgressBarState.NoProgress);
                }
            }
        }

        private void SetButtonText(String text)
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

        private void ForceClose(object nevermind)
        {
            if (InvokeRequired)
            {
                CloseCallback closeCallback = ForceClose;
                Invoke(closeCallback, new object[] { " " });
            }
            else
            {
                Close();
            }
        }

        #endregion

        private void FormatFileLengthLabel() 
        {
            decimal fileLength = _transfer.FileLength;

            if (fileLength < Constants.MegaByteTreshold)
            {
                filelengthLabel.Text = (fileLength/Constants.KiloByteTreshold).ToString("F") + " kb";
            }
            else if (fileLength < Constants.GigaByteTreshold)
            {
                filelengthLabel.Text = (fileLength/Constants.MegaByteTreshold).ToString("F") + " mb";
            }
            else if (fileLength > Constants.GigaByteTreshold)
            {
                filelengthLabel.Text = (fileLength/Constants.GigaByteTreshold).ToString("F") + " gb";
            }
        }

        private void ScheduleFadeOut() 
        {
            if (Options.AutoFadeOut)
                _fadeOutTimer = new Timer(ForceClose, null, TimeSpan.FromMilliseconds(2000), TimeSpan.FromMilliseconds(-1));
        }

        private void FadeOut() 
        {
            for (var fadeOut = 1.1; fadeOut > 0; fadeOut -= 0.1)
            {
                Opacity = fadeOut;
                Refresh();
                Thread.Sleep(70);
            }
        }

        private delegate void CloseCallback(object nevermind);

        private delegate void SetButtonTextCallback(String text);

        private delegate void SetProgressCallback(int value);

        private delegate void SetTextCallback(String text);

        private delegate void SetTextWithColorCallback(String text, Color color);
    }
}