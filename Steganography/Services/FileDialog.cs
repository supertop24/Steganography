using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Steganography.Services
{
    public class FileDialog
    {
        public bool SaveImage(byte[] image)
        {
            bool check = false;
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = "c:\\";
                saveFileDialog.Filter = "Image files (*.png)|*.png";
                saveFileDialog.Title = "Save Image File";
                saveFileDialog.DefaultExt = "png";
                saveFileDialog.FileName = "Test";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string saveFilePath = saveFileDialog.FileName;
                    File.WriteAllBytes(saveFilePath, image);
                    MessageBox.Show($"Image saved successfully at {saveFilePath}");
                    check = true;
                }
                else
                {
                    MessageBox.Show("Save operation was canceled.");
                }
            }
            return check;
        }
        public byte[] OpenImage()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Image files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp";
                openFileDialog.Title = "Select an Image File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    string fileName = Path.GetFileName(filePath);
                    byte[] imageData = File.ReadAllBytes(filePath);
                    return imageData;
                }
                else
                {
                    MessageBox.Show("Open operation was canceled.");
                }
            }
            return null;
        }
        public string OpenImagePath()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Image files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp";
                openFileDialog.Title = "Select an Image File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    string fileName = Path.GetFileName(filePath);
                    byte[] imageData = File.ReadAllBytes(filePath);
                    return filePath;
                }
                else
                {
                    MessageBox.Show("Open operation was canceled.");
                }
            }
            return null;
        }
        public string SaveImagePath()
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = "c:\\";
                saveFileDialog.Filter = "Image files (*.png)|*.png";
                saveFileDialog.Title = "Save Image File";
                saveFileDialog.DefaultExt = "png";
                saveFileDialog.FileName = "Test";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string saveFilePath = saveFileDialog.FileName;
                    MessageBox.Show($"Image saved successfully at {saveFilePath}");
                    return saveFilePath;
                }
                else
                {
                    MessageBox.Show("Save operation was canceled.");
                }
            }
            return null;
        }
    }
}
