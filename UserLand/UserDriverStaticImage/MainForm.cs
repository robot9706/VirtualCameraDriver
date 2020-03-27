using DriverInterfaceWrapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserDriverStaticImage
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            btnDeviceRefresh_Click(sender, e);
        }

        private void btnDeviceRefresh_Click(object sender, EventArgs e)
        {
            DeviceInfo[] info = DriverInterface.GetDevices();

            cbDevices.Items.Clear();
            cbDevices.Items.AddRange(info);
        }

        private void cbDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupPicture.Enabled = (cbDevices.SelectedIndex != -1);

            if (groupPicture.Enabled && !DriverInterface.SelectDevice(((DeviceInfo)cbDevices.SelectedItem).Path))
            {
                cbDevices.SelectedIndex = -1;
                MessageBox.Show("Failed to select device!");
                return;
            }
        }

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Image files|*.bmp;*.jpg;*.png";
            if (opf.ShowDialog(this) == DialogResult.OK)
            {
                Bitmap rawInput = new Bitmap(opf.FileName);
                Bitmap videoBuffer = null;

                if (rawInput.Width != DriverInterface.Width || rawInput.Height != DriverInterface.Height)
                {
                    videoBuffer = new Bitmap(DriverInterface.Width, DriverInterface.Height);
                    Graphics gfx = Graphics.FromImage(videoBuffer);

                    float scaleX = 1.0f;
                    if (rawInput.Width != DriverInterface.Width)
                    {
                        scaleX = DriverInterface.Width / (float)rawInput.Width;
                    }

                    float scaleY = 1.0f;
                    if (rawInput.Height != DriverInterface.Height)
                    {
                        scaleY = DriverInterface.Height / (float)rawInput.Height;
                    }

                    float scale = Math.Min(scaleX, scaleY);

                    int newWidth = (int)Math.Floor(scale * rawInput.Width);
                    int newHeight = (int)Math.Floor(scale * rawInput.Height);

                    gfx.Clear(Color.Black);
                    gfx.DrawImage(rawInput, new Rectangle((videoBuffer.Width / 2) - (newWidth / 2), (videoBuffer.Height / 2) - (newHeight / 2), newWidth, newHeight));

                    gfx.Dispose();
                }
                else
                {
                    videoBuffer = rawInput;
                }

                if (picView.Image != null)
                {
                    picView.Image.Dispose();
                }
                picView.Image = (Bitmap)videoBuffer.Clone();

                Stopwatch sw = new Stopwatch();
                BitmapData imageLock = videoBuffer.LockBits(new Rectangle(0, 0, videoBuffer.Width, videoBuffer.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                sw.Start();
                DriverInterface.SetData(imageLock.Scan0, imageLock.Stride, imageLock.Width, imageLock.Height);
                sw.Stop();
                videoBuffer.UnlockBits(imageLock);

                Console.WriteLine(sw.Elapsed.ToString());
                
                if (videoBuffer != rawInput)
                {
                    videoBuffer.Dispose();
                }
                rawInput.Dispose();
            }
        }
    }
}
