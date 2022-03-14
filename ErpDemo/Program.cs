using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ErpDemo
{
    static class Program
    {
        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            ErpDemoLogin loginForm = new ErpDemoLogin();
            Application.Run(loginForm);

            if (loginForm.UserAuthenticated != null)
            {
                Application.Run(new ErpDemo(loginForm.UserAuthenticated));
            }
        }
    }
}
