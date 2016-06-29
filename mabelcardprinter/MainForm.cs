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
        private bool managerRunning;
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
            manager.FinishedCard += manager_FinishedCard;
            lblProgressText.Text = "";
            managerRunning = false;
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
                managerRunning = false;
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

        delegate void UpdateProgressDelegate(String text, int percent);

        private void UpdateProgress(String text, int percent)
        {
            if (tbStatusBar.InvokeRequired == false)
            {
                // on the same thread
                lblProgressText.Text = text;
                progbarPrinting.PerformStep();
            }
            else
            {
                UpdateProgressDelegate updateprog = new UpdateProgressDelegate(UpdateProgress);
                this.BeginInvoke(updateprog, new object[] { text, percent });
            }
        }

        delegate void ToggleConnectedDelegate(bool connected);

        private void ToggleConnected(bool connected)
        {
            if (tbStatusBar.InvokeRequired == false)
            {
                // on the same thread
               if (connected)
                {
                    connectToMABELToolStripMenuItem.Enabled = false;
                    disconnectFromMABELToolStripMenuItem.Enabled = true;
                } else
                {
                    connectToMABELToolStripMenuItem.Enabled = true;
                    disconnectFromMABELToolStripMenuItem.Enabled = false;
                }
            }
            else
            {
                ToggleConnectedDelegate toggleConnected = new ToggleConnectedDelegate(ToggleConnected);
                this.BeginInvoke(toggleConnected, new object[] { connected });
            }
        }

        delegate void UpdateCardDetailsDelegate(MabelCard card);

        private void UpdateCardDetails(MabelCard card)
        {
            if (tbStatusBar.InvokeRequired == false)
            {
                Image frontImage = card.GetCardFrontImage();
                Image backImage = card.GetCardBackImage();
                if (Properties.Settings.Default.Orientation.Equals("Portrait"))
                {
                    frontImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    backImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
                }
                pbCardFront.Image = frontImage;
                pbCardBack.Image = backImage;

                tbMemberId.Text = card.member_id;
                tbMagId.Text = card.mag_token;
            }
            else
            {
                UpdateCardDetailsDelegate updateCard = new UpdateCardDetailsDelegate(UpdateCardDetails);
                this.BeginInvoke(updateCard, new object[] { card });
            }
        }

        delegate void PrintButtonEnableDelegate(bool onoff);

        private void PrintButtonEnable(bool onoff)
        {
            if (btnPrint.InvokeRequired == false)
            {
                btnPrint.Enabled = onoff;
            } else
            {
                PrintButtonEnableDelegate printButtonEnable = new PrintButtonEnableDelegate(PrintButtonEnable);
                this.BeginInvoke(printButtonEnable, new object[] { onoff });
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
            if (!managerRunning)
            {
                runManager.BeginInvoke(manager, null, null);
            }
            managerRunning = true;
        }

        private void manager_WaitingCard(object sender, PrinterEventArgs e)
        {
            UpdateStatusbar(e.Status);
        }

        private void manager_NFCRead(object sender, NFCEventArgs e)
        {
            tbRfidToken.Text = e.rfidToken;
            UpdateStatusbar("RFID Read: " + e.rfidToken);
        }

        private void manager_ReadyCard(object sender, PrinterEventArgs e)
        {
            UpdateCardDetails(e.Card);
            UpdateProgress("Ready to print", 10);
            PrintButtonEnable(true);
        }

        private void manager_Debug(object sender, DebugEventArgs e)
        {
            if (Properties.Settings.Default.Debug)
            {
                UpdateStatusbar(string.Join("", e.message.Take(255)) );
                //tbMabelStatus.Text = e.url;
            }
        }

        private void manager_UpdateInfo(object sender, PrinterEventArgs e)
        {
            PrinterInfo info = e.Info;
            
        }

        public void manager_FinishedCard(object sender, PrinterEventArgs e)
        {
            UpdateProgress("Finished!", 100);
            UpdateStatusbar("Finished printing card " + e.Card.card_id);
            managerRunning = false;
        }

        private void manager_Unregistered(object sender, PrinterEventArgs e)
        {
            UpdateStatusbar("Printer Unregistered");
            managerRegistered = false;
            ToggleConnected(false);
        }

        private void manager_Registered(object sender, PrinterEventArgs e)
        {
            UpdateStatusbar("Printer Registered");
            managerRegistered = true;
            ToggleConnected(true);
        }

        private void manager_PrintingCard(object sender, PrinterEventArgs e)
        {
            UpdateStatusbar("Printing Card: " + e.Card.card_id);
            UpdateProgress("printing..", 10);
            PrintButtonEnable(false);
        }

        private void manager_PrintedCard(object sender, PrinterEventArgs e)
        {
            UpdateStatusbar("Printed Card: " + e.Card.card_id);
            UpdateProgress("printing complete!", 30);
        }

        private void manager_ErrorCard(object sender, PrinterEventArgs e)
        {
            UpdateStatusbar( "ERROR: " + e.Status);
            UpdateProgress("ERROR", 0); ;
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
                RunManager();
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
                RunManager();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            btnPrint.Enabled = false;
            manager.printNextCard();
        }
    }
}
