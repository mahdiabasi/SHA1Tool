using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
//Author:mahdi abasi
//15-March-2017
namespace SHA1Tool
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
            Application.Run(new Form1());
        }
    }
}
