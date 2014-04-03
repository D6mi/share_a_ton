using System;
using System.Windows.Forms;

namespace Share_a_Ton.Forms
{
    public partial class Notification : Form
    {
        public Notification(String title, String text)
        {
            InitializeComponent();
            Text = title;
            textLabel.Text = text;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}