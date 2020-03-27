using DriverInterfaceWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserDriverCanon
{
    static class Program
    {
        [MTAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!DriverInterface.Init())
            {
                MessageBox.Show("Unable to init DriverInterface!");

                return;
            }

            Thread sta = new Thread(new ThreadStart(() =>
            {
                Application.Run(new MainForm());
            }));

            sta.SetApartmentState(ApartmentState.STA);
            sta.Start();
            sta.Join();

            DriverInterface.Free();
        }
    }
}
