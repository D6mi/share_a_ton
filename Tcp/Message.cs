using System;
using System.Collections.Generic;
using System.Text;

namespace Share_a_Ton.Tcp
{
    public enum Commands
    {
        Send,
        Acknowledge,
        Accept,
        Reject,
        Success,
        Error,
        Abort
    }

    internal class Message
    {
        private readonly Commands _command;
        private readonly long _fileLength;
        private readonly String _filename;

        public Message(string filename, long fileLength)
        {
            _command = Commands.Send;
            _filename = filename;
            _fileLength = fileLength;
        }

        // Create a message from the passed byte array.
        public Message(byte[] bytes)
        {
            // The first four bytes is the command.
            
            _command = (Commands) BitConverter.ToInt32(bytes, 0);

            _fileLength = BitConverter.ToInt32(bytes, 4);

            // The seconds four bytes is the filename length.
            int nameLength = BitConverter.ToInt32(bytes, 12);

            // If there is a filename specified, "nameLength" WILL always be larger than zero.
            if (nameLength > 0)
            {
                // Get the filename from the received byte array.
                _filename = Encoding.UTF8.GetString(bytes, 16, nameLength);
            }
        }

        public Commands Command
        {
            get { return _command; }
        }

        public String Filename
        {
            get { return _filename; }
        }

        public long FileLength
        {
            get { return _fileLength; }
        }

        // Convert the message to a byte array.
        public byte[] ToBytes()
        {
            var result = new List<byte>();

            // Add the four Command (as int) bytes to the List.
            result.AddRange(BitConverter.GetBytes((int) _command));

            // Add the file length
            result.AddRange(BitConverter.GetBytes(_fileLength));

            // Get the filename length.
            int nameLength = _filename.Length;

            // Store the length into the List. If it's zero, store the zero.
            if (nameLength > 0)
                result.AddRange(BitConverter.GetBytes(nameLength));
            else
                result.AddRange(BitConverter.GetBytes(0));

            // Store the filename into the List.
            result.AddRange(Encoding.UTF8.GetBytes(_filename));

            // Transform the List into an array and return it.
            return result.ToArray();
        }

        public override string ToString()
        {
            return String.Format("Command : {0} | File Length : {1} | Filename : {2}\r\n",
                _command, _fileLength, _filename);
        }

        public static byte[] ConvertCommandToBytes(Commands command)
        {
            return BitConverter.GetBytes((int) command);
        }

        public static Commands ConvertBytesToCommand(byte[] data)
        {
            var command = (Commands) BitConverter.ToInt32(data, 0);
            return command;
        }
    }
}