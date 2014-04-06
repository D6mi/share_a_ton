using System;
using System.Drawing;

namespace Share_a_Ton.Utilities
{
    public static class Constants
    {
        public static readonly Color SuccessColor = Color.Green;
        public static readonly Color ErrorColor = Color.Red;
        public static readonly Color WarningColor = Color.DarkOrange;

        public const int TcpPort = 11111;
        public const int UdpPort = 10000;
        public const int DefaultBufferSize = short.MaxValue;

        public const long KiloByteTreshold = 1000;
        public const long MegaByteTreshold = 1000000;
        public const long GigaByteTreshold = 1000000000;

        public const String OptionsUsernameOrDownloadFolderErrorString = "Username and download folder path are mandatory!";
        public const String ChooseFileString = "Please choose a folder to which all of you're files will be saved!";
        public const String MultipleFilesDraggedErrorString = "Please drag one file at a time!";
        public const String DirectoryDraggedErrorString = "You've chosen a directory, please only drag and drop files!";
        public const String DragLocationErrorString = "You need to drop the file on one of the computers in the LAN list!";
        public const String DragTipString = "Drop the file on the LAN PC you want to send the file to!";
    }
}