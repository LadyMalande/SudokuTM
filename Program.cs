using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sudokuTM
{
    /// <summary>
    /// Hlavní třída, ze které se volá spuštění programu.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// Hlavní vstupní bod aplikace. Zavolá Form1 (Menu).
        /// </summary>
        [STAThread]
        
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainMenu());
        }
    }
}
