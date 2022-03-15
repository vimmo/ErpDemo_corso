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
            
            Login login = new Login();

            Application.Run(login);

            if(login.UTENTE_LOGGATO != null)
            {
                Application.Run(new ErpDemo(login.UTENTE_LOGGATO));
            }
        }
    }
}
