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
        private bool managerRegistered;
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
            manager.UpdateInfo += manager_UpdateInfo;
            manager.Debug += manager_Debug;
        }
        delegate void RunManagerDelegate(PrinterManager manager);

        private void StartManager(PrinterManager manager)
        {
            try
            {
                manager.doWork();
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
                lbLog.Items.Add("[" + DateTime.Now.ToString("dd/MM/yyyy H:mm:ss") + "] : " + text);
            } else { 
                UpdateStatsbarDelegate updateSb = new UpdateStatsbarDelegate(UpdateStatusbar);
                this.BeginInvoke(updateSb, new object[] { text });
            }
        }

        private void RegisterManager()
        {
            manager.register();
        }

        private void UnregisterManager()
        {
            manager.unregister();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            UpdateStatusbar("Initialising...");
            SetupManager();
            RegisterManager();
        }

        private void RunManager()
        {
            RunManagerDelegate runManager = new RunManagerDelegate(StartManager);
            runManager.BeginInvoke(manager, null, null);
        }

        private void manager_WaitingCard(object sender, PrinterEventArgs e)
        {
            UpdateStatusbar("Waiting for card to be finished");
        }

        private void manager_Debug(object sender, DebugEventArgs e)
        {
            if (Properties.Settings.Default.Debug)
            {
                //UpdateStatusbar(e.url);
                UpdateStatusbar(e.message);
                tbMabelStatus.Text = e.url;
            }
        }

        private void manager_UpdateInfo(object sender, PrinterEventArgs e)
        {
            PrinterInfo info = e.Info;
            
        }

        private void manager_Unregistered(object sender, PrinterEventArgs e)
        {
            UpdateStatusbar("Printer Unregistered");
            managerRegistered = false;
            connectToMABELToolStripMenuItem.Enabled = true;
        }

        private void manager_Registered(object sender, PrinterEventArgs e)
        {
            UpdateStatusbar("Printer registered");
            managerRegistered = true;
            tbPrinterStatus.Text = new string(e.Info.sModel);
            connectToMABELToolStripMenuItem.Enabled = false;
            //Console.WriteLine("Printer Serial: " + new string(e.Info.sPrinterSerial));
            //Console.WriteLine("Printer Connected: " + e.Info.bPrinterConnected);*/
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

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void managerPollTimer_Tick(object sender, EventArgs e)
        {
            if (managerRegistered)
            {
                manager.doWork();
            }
            if (cbUnattended.Checked)
            {
                if (!managerRegistered)
                {
                    manager.register();
                }
            }
        }

        private void connectToMABELToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!managerRegistered)
            {
                manager.register();
                
            }
        }

    }
}
