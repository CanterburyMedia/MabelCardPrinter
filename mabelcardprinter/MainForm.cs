using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MabelCardPrinter
{
    public partial class MainForm : Form
    {
        private PrinterManager manager;
        public MainForm()
        {
            InitializeComponent();
        }

        private void SetupManager()
        {
            manager = new PrinterManager(Properties.Settings.Default.PrinterID,
                Properties.Settings.Default.PrinterName,
                Properties.Settings.Default.PrinterLocation);

            manager.Checking += manager_Checking;
            manager.ErrorCard += manager_ErrorCard;
            manager.PrintedCard += manager_PrintedCard;
            manager.PrintingCard += manager_PrintingCard;
            manager.Registered += manager_Registered;
            manager.Unregistered += manager_Unregistered;
            manager.WaitingCard += manager_WaitingCard;
        }
        delegate void RunManagerDelegate(PrinterManager manager);

        private void StartManager(PrinterManager manager)
        {
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
            catch (Exception ex)
            {
                UpdateStatusbar("ERROR:  " + ex.Message);
            }
            finally
            {
                manager.unregister();
            }
        }
        delegate void UpdateStatsbarDelegate(String text);

        private void UpdateStatusbar(String text)
        {
            if (tbStatusBar.InvokeRequired == false)
            {
                // on the same thread
                tbStatusBar.Text = text;
            } else { 
                UpdateStatsbarDelegate updateSb = new UpdateStatsbarDelegate(UpdateStatusbar);
                this.BeginInvoke(updateSb, new object[] { text });
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            UpdateStatusbar("Initialising...");
            SetupManager();
            RunManagerDelegate runManager = new RunManagerDelegate(StartManager);
            runManager.BeginInvoke(manager,null,null);
        }

        private void manager_WaitingCard(object sender, PrinterEventArgs e)
        {
            UpdateStatusbar("Waiting for card to be finished");
        }

        private void manager_Unregistered(object sender, PrinterEventArgs e)
        {
            UpdateStatusbar("Printer Unregistered");
        }

        private void manager_Registered(object sender, PrinterEventArgs e)
        {
            UpdateStatusbar("Printer registered");
            /*Console.WriteLine("Printer Model: " + new string(e.Info.sModel));
            Console.WriteLine("Printer Serial: " + new string(e.Info.sPrinterSerial));
            Console.WriteLine("Printer Connected: " + e.Info.bPrinterConnected);*/
        }

        private void manager_PrintingCard(object sender, PrinterEventArgs e)
        {
            UpdateStatusbar("Printing Card: " + e.Card.card_id);
        }

        private void manager_PrintedCard(object sender, PrinterEventArgs e)
        {
            UpdateStatusbar("Printed Card: " + e.Card.card_id);
        }

        private void manager_ErrorCard(object sender, PrinterEventArgs e)
        {
            UpdateStatusbar( "ERROR: " + e.Status);
        }

        private void manager_Checking(object sender, PrinterEventArgs e)
        {
            UpdateStatusbar("Checking for new cards");
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form settings = new SettingsDialog();
            settings.ShowDialog();
        }

        private void viewPrinterStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form printerInfo = new ViewPrinterInfo();
            printerInfo.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


    }
}
