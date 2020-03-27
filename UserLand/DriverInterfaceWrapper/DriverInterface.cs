using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverInterfaceWrapper
{
    public class DriverInterface
    {
        public const int Width = 1280;
        public const int Height = 720;

        public static bool Init()
        {
            return (Native.Init() != 0);
        }

        public static void Free()
        {
            Native.Free();
        }

        public static DeviceInfo[] GetDevices()
        {
            int count = Native.GetNumDevices();
            DeviceInfo[] array = new DeviceInfo[count];

            for (int x = 0; x < count; x++)
            {
                array[x] = new DeviceInfo(Native.GetDevicePath(x));
            }

            return array;
        }

        public static void DestroyDevice()
        {
            Native.DestroyDevice();
        }

        public static bool SelectDevice(string path)
        {
            return Native.SetDevice(path);
        }

        public static bool SetData(IntPtr data, int stride, int width, int height)
        {
            return (Native.SetBuffer(data, stride, width, height) > 0); 
        }
    }
}
