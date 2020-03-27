using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverInterfaceWrapper
{
    public class DeviceInfo
    {
        public string Path
        {
            get;
            private set;
        }

        internal DeviceInfo(string path)
        {
            Path = path;
        }

        public override string ToString()
        {
            return Path;
        }
    }
}
