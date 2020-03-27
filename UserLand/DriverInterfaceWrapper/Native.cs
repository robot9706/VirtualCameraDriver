using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DriverInterfaceWrapper
{
    class Native
    {
        [DllImport("DriverInterface.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Init();

        [DllImport("DriverInterface.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Free();

        [DllImport("DriverInterface.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetNumDevices();

        [DllImport("DriverInterface.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern int GetDevicePath(int index, StringBuilder str, int maxLen);

        [DllImport("DriverInterface.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void DestroyDevice();

        [DllImport("DriverInterface.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern int SetDevice(StringBuilder path, int length);

        [DllImport("DriverInterface.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int SetBuffer(IntPtr data, int stride, int width, int height);

        public static string GetDevicePath(int index)
        {
            StringBuilder buffer = new StringBuilder(256);

            GetDevicePath(index, buffer, buffer.Capacity);

            return buffer.ToString();
        }

        public static bool SetDevice(string path)
        {
            StringBuilder buffer = new StringBuilder(path);

            return (SetDevice(buffer, buffer.Capacity) > 0);
        }
    }
}
