using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lab04_Application.IO;

namespace Lab04_Application
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
            IDataSource dataSource = new TextDataSource();
            Application.Run(new Form1(dataSource, "Data\\DSSV.txt"));
        }
    }
}
