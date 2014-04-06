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

        public const long KiloByteTreshold = 1000000;
        public const long MegaByteTreshold = 1000000000;

        public const String OptionsUsernameOrDownloadFolderError = "Username and download folder path are mandatory!";
    }
}