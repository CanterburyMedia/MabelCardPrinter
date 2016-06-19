using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Reflection;
using System.Diagnostics;

namespace MabelCardPrinter
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
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

            PrinterManager manager = new PrinterManager(Properties.Settings.Default.PrinterID,
                Properties.Settings.Default.PrinterName,
                Properties.Settings.Default.PrinterLocation);

            manager.Checking += manager_Checking;
            manager.ErrorCard += manager_ErrorCard;
            manager.PrintedCard += manager_PrintedCard;
            manager.PrintingCard += manager_PrintingCard;
            manager.Registered += manager_Registered;
            manager.Unregistered += manager_Unregistered;
            manager.WaitingCard += manager_WaitingCard;
            try
            {
                if (manager.register())
                {
                    while (true)
                    {
                        System.Threading.Thread.Sleep(1000);
                        manager.doWork();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR:  " + e.Message);
            }
            finally
            {
                manager.unregister();
            }
        }

        static void manager_WaitingCard(object sender, PrinterEventArgs e)
        {
            Console.WriteLine("Waiting for card to be finished");
        }

        static void manager_Unregistered(object sender, PrinterEventArgs e)
        {
            Console.WriteLine("Printer Unregistered");
        }

        static void manager_Registered(object sender, PrinterEventArgs e)
        {
            Console.WriteLine("Printer Registered");
            Console.WriteLine("Printer Model: " + new string(e.Info.sModel));
            Console.WriteLine("Printer Serial: " + new string( e.Info.sPrinterSerial));
            Console.WriteLine("Printer Connected: " + e.Info.bPrinterConnected);
        }

        static void manager_PrintingCard(object sender, PrinterEventArgs e)
        {
            Console.WriteLine("Printing Card: " + e.Card.card_id);
            Console.WriteLine("Cards Printed: " + e.Info.iCardsPrinted);
            Console.WriteLine("Cards On Printhead: " + e.Info.iCardsOnPrinthead);
        }

        static void manager_PrintedCard(object sender, PrinterEventArgs e)
        {
            Console.WriteLine("Printed Card: " + e.Card.card_id);
        }

        static void manager_ErrorCard(object sender, PrinterEventArgs e)
        {
            Console.WriteLine("ERROR: " + e.Status);
        }

        static void manager_Checking(object sender, PrinterEventArgs e)
        {
            Console.WriteLine("Checking for new cards");
        }
    }
}
