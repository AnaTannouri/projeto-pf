using System;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace ProjetoPF
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // Cultura (mantido)
            CultureInfo ci = new CultureInfo("pt-BR");
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            // Aponta |DataDirectory| para a pasta /Dados ao lado do .exe
            var dataDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Dados");
            Directory.CreateDirectory(dataDir);
            AppDomain.CurrentDomain.SetData("DataDirectory", dataDir);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmPrincipal());
        }
    }
}