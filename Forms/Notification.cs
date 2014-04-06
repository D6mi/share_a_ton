using System;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Threading.Timer;

namespace Share_a_Ton.Forms
{
    public partial class Notification : Form
    {
        private readonly Timer _fadeOutTimer;

        public Notification(String title, String text)
        {
            InitializeComponent();
            Text = title;
            textLabel.Text = text;

            _fadeOutTimer = new Timer(ForceClose, null, TimeSpan.FromMilliseconds(2000), TimeSpan.FromMilliseconds(-1));
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            _fadeOutTimer.Dispose();
            Close();
        }

        private void Notification_FormClosing(object sender, FormClosingEventArgs e)
        {
            FadeOut();
        }

        private void ForceClose(object nevermind)
        {
            if (InvokeRequired)
                {
                    CloseCallback closeCallback = ForceClose;
                    Invoke(closeCallback, new object[] {" "});
                }
                else
                {
                    Close();
                }
        }

        public void FadeOut()
        {
            for (double fadeOut = 1.1; fadeOut > 0; fadeOut -= 0.1)
            {
                Opacity = fadeOut;
                Refresh();
                Thread.Sleep(70);
            }
        }

        private delegate void CloseCallback(object nevermind);
    }
}