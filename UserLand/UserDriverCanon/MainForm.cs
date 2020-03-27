using DriverInterfaceWrapper;
using EOSDigital.API;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserDriverCanon
{
    public partial class MainForm : Form
    {
        private CanonAPI api;
        private Camera activeCamera;

        private bool hasDevice = false;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            api = new CanonAPI();

            btnDeviceRefresh_Click(sender, e);
            btnRefreshCameras_Click(sender, e);
        }

        private void btnDeviceRefresh_Click(object sender, EventArgs e)
        {
            DeviceInfo[] info = DriverInterface.GetDevices();

            cbDevices.Items.Clear();
            cbDevices.Items.AddRange(info);
        }

        private void cbDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            hasDevice = false;

            if (!DriverInterface.SelectDevice(((DeviceInfo)cbDevices.SelectedItem).Path))
            {
                cbDevices.SelectedIndex = -1;
                MessageBox.Show("Failed to select device!");
                return;
            }

            hasDevice = (cbDevices.SelectedIndex > -1);
        }

        private void btnRefreshCameras_Click(object sender, EventArgs e)
        {
            List<Camera> cameraList = api.GetCameraList();
            foreach (Camera c in cameraList)
            {
                cbCameras.Items.Add(new CameraListItem(c));
            }
        }

        private void cbCameras_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (activeCamera != null && activeCamera.SessionOpen)
            {
                activeCamera.CloseSession();
                activeCamera = null;
            }

            if (cbCameras.SelectedIndex > -1)
            {
                activeCamera = ((CameraListItem)cbCameras.SelectedItem).Camera;

                activeCamera.LiveViewUpdated += ActiveCamera_LiveViewUpdated;

                activeCamera.OpenSession();

                //activeCamera.StartLiveView();
                activeCamera.SetSetting(EOSDigital.SDK.PropertyID.Evf_OutputDevice, (int)EOSDigital.SDK.EvfOutputDevice.Filming);
            }
        }

        private void ActiveCamera_LiveViewUpdated(Camera sender, Stream img)
        {
            if (!hasDevice)
                return;

            using (Bitmap rawInput = new Bitmap(img))
            {
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

                BitmapData imageLock = videoBuffer.LockBits(new Rectangle(0, 0, videoBuffer.Width, videoBuffer.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                DriverInterface.SetData(imageLock.Scan0, imageLock.Stride, imageLock.Width, imageLock.Height);
                videoBuffer.UnlockBits(imageLock);

                if (videoBuffer != rawInput)
                {
                    videoBuffer.Dispose();
                }
                rawInput.Dispose();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            api.Dispose();
        }

        private void cameraKeepAlive_Tick(object sender, EventArgs e)
        {
            if (activeCamera != null && activeCamera.SessionOpen)
            {
                activeCamera.SendCommand(EOSDigital.SDK.CameraCommand.ExtendShutDownTimer, 0);
            }
        }
    }
}
