using System;

namespace Share_a_Ton.Tcp
{
    public class JSONMessage
    {
        public Commands Command { get; set; }
        public bool ConfirmationNeeded { get; set; }
        public String Filename { get; set; }
        public String Sender { get; set; }
        public long FileLength { get; set; }
    }
}