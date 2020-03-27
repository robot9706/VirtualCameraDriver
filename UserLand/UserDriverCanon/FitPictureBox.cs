using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UserDriverCanon
{
    public class FitPictureBox : Control
    {
        private Image _image;
        public Image Image
        {
            get { return _image; }
            set { _image = value; }
        }

        private bool _simpleBorder = false;
        public bool SimpleBorder 
        {
            get { return _simpleBorder; }
            set
            {
                _simpleBorder = value;
            }
        }

        public object ImageLock = new object();

        public FitPictureBox()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);

            if (_image == null)
                return;

            lock (ImageLock)
            {
                float scaleRatio = 1f;

                if (_image.Width > _image.Height && _image.Width > Width)
                {
                    float a = (float)_image.Width;
                    float b = (float)Width;

                    scaleRatio = Math.Min(a, b) / Math.Max(a, b);
                }
                if (_image.Width < _image.Height && _image.Height > Height)
                {
                    float a = (float)_image.Height;
                    float b = (float)Height;

                    scaleRatio = Math.Min(a, b) / Math.Max(a, b);
                }

                int nw = (int)Math.Floor((float)_image.Width * scaleRatio);
                int nh = (int)Math.Floor((float)_image.Height * scaleRatio);

                e.Graphics.DrawImage(_image, new Rectangle((this.Width / 2) - (nw / 2), (this.Height / 2) - (nh / 2), nw, nh));

                if (SimpleBorder)
                {
                    e.Graphics.DrawRectangle(Pens.Black, new Rectangle(0, 0, this.Width - 1, this.Height - 1));
                }
            }
        }
    }
}
