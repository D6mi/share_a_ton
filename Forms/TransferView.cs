﻿using System;
using System.Threading;
using System.Windows.Forms;
using Share_a_Ton.Tcp;

namespace Share_a_Ton.Forms
{
    public partial class TransferView : Form
    {
        private readonly Transfer _transfer;
        private bool _isRunning;
        private Thread runningThread;


        public TransferView(Transfer transfer)
        {
            InitializeComponent();

            _transfer = transfer;

            transferProgress.Minimum = 0;
            transferProgress.Maximum = 1000;

            filenameLabel.Text = _transfer.Filename;
            filelengthLabel.Text = (_transfer.FileLength / 1000000) + " mb";
            senderLabel.Text = _transfer.Sender;

            _transfer.Connected += TransferConnected;
            _transfer.Disconnected += TransferDisconnected;
            
            _transfer.Started += TransferStarted;
            _transfer.Completed += TransferCompleted;

            _transfer.TransferredChunk += TransferredPart;

           
            runningThread = new Thread(_transfer.Start) { IsBackground = true };
            runningThread.Start();
        }

        public int LastValue = 0;

        public Transfer Transfer
        {
            get { return _transfer; }
        }
        
        public void TransferConnected(object sender, EventArgs e)
        {
            SetText("Connected");
            _isRunning = true;
        }

        public void TransferDisconnected(object sender, EventArgs e)
        {
            SetText("TRANSFER DISCONNECT OR REJECT!");

            SetButtonText("Okay");
            _isRunning = false;
        }

        public void TransferStarted(object sender, EventArgs e)
        {
            SetText("TRANSFER STARTED!");
            _isRunning = false;
        }

        public void TransferCompleted(object sender, EventArgs e)
        {
            SetText("TRANSFER COMPLETED!");

            SetButtonText("Close");
            _isRunning = false;
        }

        public void TransferredPart(object sender, EventArgs e)
        {
            TransferArgs args = (TransferArgs) e;
            int value = args.BytesTransfered;
            SetProgress(value);
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            if (_isRunning)
            {
                _transfer.Abort();
                Close();
            }
            else
                Close();
        }

        private void SetText(string text)
        {
            if (statusLabel.InvokeRequired)
            {
                SetTextCallback textCallback = SetText;
                Invoke(textCallback, new object[] { text });
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
                Invoke(progressCallback, new object[] { value });
            }
            else
            {
                transferProgress.Value = value;

                if (Microsoft.WindowsAPICodePack.Taskbar.TaskbarManager.IsPlatformSupported)
                {
                    var taskbarInstance = Microsoft.WindowsAPICodePack.Taskbar.TaskbarManager.Instance;

                    taskbarInstance.SetProgressState(Microsoft.WindowsAPICodePack.Taskbar.TaskbarProgressBarState.Normal);
                    taskbarInstance.SetProgressValue(value, 100);

                    taskbarInstance.SetProgressState(Microsoft.WindowsAPICodePack.Taskbar.TaskbarProgressBarState.NoProgress);
                }
            }
        }

        private void SetButtonText(string text)
        {
            if (actionButton.InvokeRequired)
            {
                SetButtonTextCallback textCallback = SetButtonText;
                Invoke(textCallback, new object[] { text });
            }
            else
            {
                actionButton.Text = text;
            }
        }


        private delegate void SetTextCallback(string text);

        private delegate void SetProgressCallback(int value);

        private delegate void SetButtonTextCallback(string text);
    }
}