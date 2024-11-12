using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using Steganography.Data;
using System.Windows.Forms;

namespace Steganography.Services
{
    public class Embedding
    {
        FileDialog fileService = new FileDialog();

        public void EmbedMessage(string message)
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            byte[] messageWithNullTerminator = messageBytes.Concat(new byte[] { 0x00 }).ToArray();
            byte[] imageBytes = fileService.OpenImage();
            if (messageWithNullTerminator.Length * 8 > imageBytes.Length)
            {
                throw new Exception("Message is too large to fit in the image.");
            }
            int byteIndex = 0;
            foreach (byte messageByte in messageWithNullTerminator)
            {
                for (int bitIndex = 7; bitIndex >= 0; bitIndex--)
                {
                    bool bit = (messageByte & (1 << bitIndex)) != 0;
                    imageBytes[byteIndex] = SetLeastSignificantBit(imageBytes[byteIndex], bit);
                    byteIndex++;
                }
            }
            fileService.SaveImage(imageBytes);
        }
        private static byte SetLeastSignificantBit(byte imageByte, bool bit)
        {
            if (bit)
            {
                return (byte)(imageByte | 1);
            }
            else
            {
                return (byte)(imageByte & 0xFE);
            }
        }

        public string ExtractMessage()
        {
            byte[] imageBytes = fileService.OpenImage();
            StringBuilder messageBinary = new StringBuilder();
            for (int i = 0; i < imageBytes.Length; i++)
            {
                byte imageByte = imageBytes[i];
                messageBinary.Append((imageByte & 1) == 1 ? '1' : '0');
            }
            string message = BinaryToString(messageBinary.ToString());
            return message.Split('\0')[0];
        }
        private static string BinaryToString(string binary)
        {
            StringBuilder message = new StringBuilder();
            for (int i = 0; i < binary.Length; i += 8)
            {
                string byteValue = binary.Substring(i, 8);
                byte b = Convert.ToByte(byteValue, 2);
                message.Append((char)b);
            }
            return message.ToString();
        }
    }
}
