using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tlpro
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            int i = AppEntry.Login();
            if (i==1)
            {
                Application.Run(AppEntry.MainForm);
            }
            else if(i == 2)
            {
                Application.Run(AppEntry.TainForm);
            }
            else
                Application.Exit();

        }
    }
}
