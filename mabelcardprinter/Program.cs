using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;

namespace MabelCardPrinter
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            /*
            string version = fileVersionInfo.ProductVersion;
            Console.WriteLine("  __  __          ____  ______ _      ");
            Console.WriteLine(" |  \\/  |   /\\   |  _ \\|  ____| |     ");
            Console.WriteLine(" |\\ \\ / |  /  \\  | |_) | |__  | |     ");
            Console.WriteLine(" | |\\/| | / /\\ \\ |  _ <|  __| | |     ");
            Console.WriteLine(" | |  | |/ ____ \\| |_) | |____| |____ ");
            Console.WriteLine(" |_|  |_/_/    \\_\\____/|______|______|");
            Console.WriteLine("                          Card Printer");
            Console.WriteLine("---------- by Chris Robets  <c.roberts@csrfm.com> --");
            Console.WriteLine("----- updated by James Stokell <james@csrfm.com> ---");
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine("---------------- Version: " + version + " ------------------");
            Console.WriteLine("----------------------------------------------------");
            */
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form mainForm = new MainForm();
            
            Application.Run(new MainForm());
        }
    }
}
