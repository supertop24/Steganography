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
    public class ImageHandling
    {
        FileDialog service = new FileDialog();
        public Bitmap EncodeText(Bitmap image, string text)
        {
            text += '\0';
            byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(text);
            int byteIndex = 0, bitIndex = 0;
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    if (byteIndex >= textBytes.Length)
                        return image;
                    Color pixel = image.GetPixel(x, y);
                    int[] rgb = { pixel.R, pixel.G, pixel.B };
                    for (int color = 0; color < 3; color++)
                    {
                        if (byteIndex < textBytes.Length)
                        {
                            int bit = (textBytes[byteIndex] >> bitIndex) & 1;
                            rgb[color] = (rgb[color] & ~1) | bit;
                            bitIndex++;
                            if (bitIndex == 8)
                            {
                                bitIndex = 0;
                                byteIndex++;
                            }
                        }
                    }
                    image.SetPixel(x, y, Color.FromArgb(rgb[0], rgb[1], rgb[2]));
                }
            }
            return image;
        }
        public string DecodeText(Bitmap image)
        {
            List<byte> textBytes = new List<byte>();
            byte currentByte = 0;
            int bitIndex = 0;
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color pixel = image.GetPixel(x, y);
                    int[] rgb = { pixel.R, pixel.G, pixel.B };
                    for (int color = 0; color < 3; color++)
                    {
                        int bit = rgb[color] & 1;
                        currentByte |= (byte)(bit << bitIndex);
                        bitIndex++;
                        if (bitIndex == 8)
                        {
                            if (currentByte == 0)
                                return System.Text.Encoding.UTF8.GetString(textBytes.ToArray());
                            textBytes.Add(currentByte);
                            currentByte = 0;
                            bitIndex = 0;
                        }
                    }
                }
            }
            return System.Text.Encoding.UTF8.GetString(textBytes.ToArray());
        }
    }
}
