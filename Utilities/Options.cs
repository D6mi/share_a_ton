using System;
using Share_a_Ton.Properties;

namespace Share_a_Ton.Utilities
{
    public static class Options
    {
        public static event EventHandler DownloadFolderChanged;

        /// <summary>
        ///     The name by which this PC will be known.
        /// </summary>
        public static String Username = Settings.Default.Username;

        /// <summary>
        ///     This variable affects the transfer process. Typically, the Client needs permission from the
        ///     Server to initiate the file transfer. If the "_confirmationNeeded" is set to "false" then the
        ///     permission is NOT required and the transfer will automatically start upon the Client's request.
        /// </summary>
        public static bool ConfirmationNeeded = Settings.Default.ConfirmationNeeded;

        /// <summary>
        ///     If true, the specified download folder will be opened upon transfer completion automatically, without asking.
        /// </summary>
        public static bool AutoOpenDownloadFolder = Settings.Default.AutomaticallyOpenDownloadFolderOnTransfer;

        /// <summary>
        ///     If true, the user will be asked to choose whether he/she wants to open the download folder upon transfer
        ///     completion.
        /// </summary>
        public static bool AskForDownloadFolder = Settings.Default.AskToOpenDownloadFolderOnTransfer;

        /// <summary>
        ///     Determines whether the Transfer Overview will be automatically closed after 1.5 seconds after
        ///     the transfer completes, allowing the next transfer to start automatically.
        /// </summary>
        public static bool AutoFadeOut = Settings.Default.AutoFadeOut;

        public static bool IsDownloadFolderSet;

        private static String _downloadFolderPath;

        /// <summary>
        ///     This is the folder in which all of the transferred files will be saved.
        /// </summary>
        public static String DownloadFolderPath
        {
            get { return _downloadFolderPath; }
            set
            {
                _downloadFolderPath = value;
                if (DownloadFolderChanged != null)
                {
                    DownloadFolderChanged(null, new FolderArgs(_downloadFolderPath));
                }
            }
        }

        static Options()
        {
            _downloadFolderPath = Settings.Default.DownloadFolder;

            if (!String.IsNullOrWhiteSpace(DownloadFolderPath))
                IsDownloadFolderSet = true;
            else
                IsDownloadFolderSet = false;
        }
    }

    public class FolderArgs : EventArgs
    {
        public String Path { get; private set; }

        public FolderArgs(String path)
        {
            Path = path;
        }

    }
}