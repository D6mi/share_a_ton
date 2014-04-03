using System;
using System.Collections.Generic;
using System.Text;

namespace Share_a_Ton.Udp
{
    public enum UdpCommand
    {
        Add,
        Remove,
        AddRefresh,
        Null
    }

    public class UdpData
    {
        public UdpData()
        {
            Command = UdpCommand.Null;
            Name = null;
        }

        public UdpData(UdpCommand command, String name)
        {
            Command = command;
            Name = name;
        }

        public UdpData(byte[] data)
        {
            Command = (UdpCommand) BitConverter.ToInt32(data, 0);

            int nameLength = data.Length - 4;

            if (nameLength > 0)
            {
                Name = Encoding.ASCII.GetString(data, 4, nameLength);
            }
            else
            {
                Name = null;
            }
        }

        public UdpCommand Command { get; set; }
        public String Name { get; set; }

        public byte[] ToBytes()
        {
            var result = new List<byte>();

            result.AddRange(BitConverter.GetBytes((int) Command));

            result.AddRange(Encoding.ASCII.GetBytes(Name));

            return result.ToArray();
        }

        public override string ToString()
        {
            return Command + " " + Name;
        }
    }
}