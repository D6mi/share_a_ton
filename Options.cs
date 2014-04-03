using Share_a_Ton.Properties;

namespace Share_a_Ton
{
    public static class Options
    {
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
        ///     This is the folder in which all of the transferred files will be saved.
        /// </summary>
        public static string DownloadFolderPath = Settings.Default.DownloadFolder;
    }
}