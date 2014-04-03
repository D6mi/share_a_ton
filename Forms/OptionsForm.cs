using System;
using System.Windows.Forms;
using Share_a_Ton.Properties;

namespace Share_a_Ton.Forms
{
    public partial class OptionsForm : Form
    {
        /// <summary>
        /// This variable affects the transfer process. Typically, the Client needs permission from the 
        /// Server to initiate the file transfer. If the "_confirmationNeeded" is set to "false" then the 
        /// permission is NOT required and the transfer will automatically start upon the Client's request.
        /// </summary>
        public bool ConfirmationNeeded { get; private set; }

        /// <summary>
        /// If true, the specified download folder will be opened upon transfer completion automatically, without asking.
        /// </summary>
        public bool AutoOpenDownloadFolder { get; private set; }

        /// <summary>
        /// If true, the user will be asked to choose whether he/she wants to open the download folder upon transfer completion.
        /// </summary>
        public bool AskForDownloadFolder { get; private set; }

        /// <summary>
        /// This is the folder in which all of the transferred files will be saved.
        /// </summary>
        public string DownloadFolderPath { get; private set; }

        // Setups the initial state of the controls on the form according to the values in
        // the settings file.
        public OptionsForm()
        {
            InitializeComponent();

            DownloadFolderPath = Settings.Default.DownloadFolder + "\\";
            ConfirmationNeeded = Settings.Default.ConfirmationNeeded;
            AskForDownloadFolder = Settings.Default.AskToOpenDownloadFolderOnTransfer;
            AutoOpenDownloadFolder = Settings.Default.AutomaticallyOpenDownloadFolderOnTransfer;

            downloadFolderTextBox.Text = DownloadFolderPath;
            confirmationCheckBox.Checked = ConfirmationNeeded;
            askForDownloadFolderCheckBox.Checked = AskForDownloadFolder;
            autoOpenDownloadFolderCheckBox.Checked = AutoOpenDownloadFolder;
        }

        // Update the "_confirmationNeeded" variable when the state of the CheckBox changes.
        private void confirmationCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ConfirmationNeeded = confirmationCheckBox.Checked;
        }

        // Allow the user to change the default download folder.
        private void changeButton_Click(object sender, EventArgs e)
        {
            var fileBrowserDialog = new FolderBrowserDialog();
            DialogResult dr = fileBrowserDialog.ShowDialog(this);
            if (DialogResult.OK == dr)
            {
                DownloadFolderPath = fileBrowserDialog.SelectedPath + "\\";
                downloadFolderTextBox.Text = DownloadFolderPath;
            }
        }

        // Save the settings to the Settings file. This method does not check if the values have changed in
        // relation to the already saved Settings, it just overrides the Settings file with the latest values
        // regardless if there has been a change.
        public void ApplySettings()
        {
            Settings.Default.DownloadFolder = DownloadFolderPath;
            Settings.Default.ConfirmationNeeded = ConfirmationNeeded;
            Settings.Default.AskToOpenDownloadFolderOnTransfer = AskForDownloadFolder;
            Settings.Default.AutomaticallyOpenDownloadFolderOnTransfer = AutoOpenDownloadFolder;
            Settings.Default.Save();

            Options.DownloadFolderPath = DownloadFolderPath;
            Options.ConfirmationNeeded = ConfirmationNeeded;
            Options.AutoOpenDownloadFolder = AutoOpenDownloadFolder;
            Options.AskForDownloadFolder = AskForDownloadFolder;
        }

        private void askForDownloadFolderCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            AskForDownloadFolder = askForDownloadFolderCheckBox.Checked;

            if (AskForDownloadFolder)
            {
                AutoOpenDownloadFolder = false;
                autoOpenDownloadFolderCheckBox.Checked = false;
            }
        }

        private void autoOpenDownloadFolderCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            AutoOpenDownloadFolder = autoOpenDownloadFolderCheckBox.Checked;

            if (AutoOpenDownloadFolder)
            {
                AskForDownloadFolder = false;
                askForDownloadFolderCheckBox.Checked = false;
            }
        }
    }
}