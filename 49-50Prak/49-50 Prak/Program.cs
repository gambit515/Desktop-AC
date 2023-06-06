using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _49_50_Prak
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Form form = new Shifr();
            //form.Show();
            Application.Run(new Form1());
            //Application.Run(new Admin("amoskin815@gmail.com"));
        }
    }
}
