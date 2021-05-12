using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Coldstore
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Dongle.cllogin(false);
           // Database.OpenConnection();
            Application.Run(new Login());
        }
    }
}
