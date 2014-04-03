using System.Drawing;

namespace Share_a_Ton
{
    public static class Constants
    {
        public static readonly Color SuccessColor = Color.Green;
        public static readonly Color ErrorColor = Color.Red;
        public static readonly Color WarningColor = Color.Yellow;

        public const int TcpPort = 11111;
        public const int UdpPort = 10000;
        public const int DefaultBufferSize = 512;

        public const long KiloByteTreshold = 1000000;
        public const long MegaByteTreshold = 1000000000;
    }
}