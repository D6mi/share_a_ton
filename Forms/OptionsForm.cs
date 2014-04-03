using System;
using System.Windows.Forms;
using Share_a_Ton.Properties;

namespace Share_a_Ton.Forms
{
    public partial class OptionsForm : Form
    {
        // This variable affects the transfer process. Typically, the Client needs permission from the 
        // Server to initiate the file transfer. If the "_confirmationNeeded" is set to "false" then the 
        // permission is NOT required and the transfer will automatically start upon the Client's request.
        private bool _confirmationNeeded;

        // This is the folder in which all of the transferred files will be saved.
        private String _downloadFolderPath;

        public bool ConfirmationNeeded { get { return _confirmationNeeded; } }
        public String DownloadFolderPath { get { return _downloadFolderPath; } }


        // Setups the initial state of the controls on the form according to the values in
        // the settings file.
        public OptionsForm()
        {
            InitializeComponent();

            _downloadFolderPath = Settings.Default.DownloadFolder;
            _confirmationNeeded = Settings.Default.ConfirmationNeeded;

            downloadFolderTextBox.Text = _downloadFolderPath;
            confirmationCheckBox.Checked = _confirmationNeeded;
        }

        // Update the "_confirmationNeeded" variable when the state of the CheckBox changes.
        private void confirmationCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            _confirmationNeeded = confirmationCheckBox.Checked;
        }

        // Allow the user to change the default download folder.
        private void changeButton_Click(object sender, EventArgs e)
        {
            var fileBrowserDialog = new FolderBrowserDialog();
            DialogResult dr = fileBrowserDialog.ShowDialog(this);
            if (DialogResult.OK == dr)
            {
                _downloadFolderPath = fileBrowserDialog.SelectedPath;
                downloadFolderTextBox.Text = _downloadFolderPath;
            }
        }

        // Save the settings to the Settings file. This method does not check if the values have changed in
        // relation to the already saved Settings, it just overrides the Settings file with the latest values
        // regardless if there has been a change.
        public void ApplySettings()
        {
            Settings.Default.DownloadFolder = _downloadFolderPath;
            Settings.Default.ConfirmationNeeded = _confirmationNeeded;
            Settings.Default.Save();
        }
    }
}