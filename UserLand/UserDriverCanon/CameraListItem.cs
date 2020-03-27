using EOSDigital.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDriverCanon
{
    class CameraListItem
    {
        public Camera Camera;

        public CameraListItem(Camera c)
        {
            Camera = c;
        }

        public override string ToString()
        {
            return Camera.DeviceName;
        }
    }
}
