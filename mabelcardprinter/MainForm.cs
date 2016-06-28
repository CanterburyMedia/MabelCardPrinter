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
                Properties.Settings.Default.LocalPrinter,
                Properties.Settings.Default.PrinterLocation);

            manager.Checking += manager_Checking;
            manager.ErrorCard += manager_ErrorCard;
            manager.PrintingCard += manager_PrintingCard;
            manager.PrintedCard += manager_PrintedCard;
            manager.ReadyCard += manager_ReadyCard;
            manager.Registered += manager_Registered;
            manager.Unregistered += manager_Unregistered;
            manager.WaitingCard += manager_WaitingCard;
            manager.UpdateInfo += manager_UpdateInfo;
            manager.Debug += manager_Debug;
            manager.NFCRead += manager_NFCRead;
            lblProgressText.Text = "";
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
            if (Properties.Settings.Default.Autostart)
            {
                RegisterManager();
            }
            
            
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

        private void manager_NFCRead(object sender, NFCEventArgs e)
        {
            tbRfidToken.Text = e.rfidToken;
            UpdateStatusbar("RFID Read: " + e.rfidToken);
        }

        private void manager_ReadyCard(object sender, PrinterEventArgs e)
        {
            Image frontImage = e.Card.GetCardFrontImage();
            Image backImage = e.Card.GetCardBackImage();
            if (Properties.Settings.Default.Orientation.Equals("Portrait"))
            {
                frontImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                backImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
            }
            pbCardFront.Image = frontImage;
            pbCardBack.Image = backImage;

            tbMemberId.Text = e.Card.member_id;
            tbMagId.Text = e.Card.mag_token;
            progbarPrinting.Step = 33;
            progbarPrinting.PerformStep();
            lblProgressText.Text = "Card Ready to Print";
        }

        private void manager_Debug(object sender, DebugEventArgs e)
        {
            if (Properties.Settings.Default.Debug)
            {
                UpdateStatusbar(string.Join("", e.message.Take(255)) );
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
            disconnectFromMABELToolStripMenuItem.Enabled = false;
        }

        private void manager_Registered(object sender, PrinterEventArgs e)
        {
            UpdateStatusbar("Printer Registered");
            managerRegistered = true;
            connectToMABELToolStripMenuItem.Enabled = false;
            disconnectFromMABELToolStripMenuItem.Enabled = true;
        }

        private void manager_PrintingCard(object sender, PrinterEventArgs e)
        {
            UpdateStatusbar("Printing Card: " + e.Card.card_id);
            progbarPrinting.PerformStep();
            lblProgressText.Text = "Printing card...";
        }

        private void manager_PrintedCard(object sender, PrinterEventArgs e)
        {
            UpdateStatusbar("Printed Card: " + e.Card.card_id);
            progbarPrinting.PerformStep();
            lblProgressText.Text = "Printing Complete";
        }

        private void manager_ErrorCard(object sender, PrinterEventArgs e)
        {
            UpdateStatusbar( "ERROR: " + e.Status);

            lblProgressText.Text = "ERROR";
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
            if (cbUnattended.Checked)
            {
                if (!managerRegistered)
                {
                    manager.register();
                }
                manager.doWork();
            }
        }

        private void connectToMABELToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!managerRegistered)
            {
                manager.register();

            }
        }

        private void disconnectFromMABELToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (managerRegistered)
            {
                manager.unregister();
                connectToMABELToolStripMenuItem.Enabled = true;
                disconnectFromMABELToolStripMenuItem.Enabled = false;
            }

        }

        private void btnNextCard_Click(object sender, EventArgs e)
        {
            if (managerRegistered)
            { 
                manager.doWork();
            }
        }
    }
}
