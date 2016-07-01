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
        private bool managerReady = false;
        private Image blankCard;

        public MainForm()
        {
            InitializeComponent();
            blankCard = Properties.Resources.CM_Cardblank;
        }

        private void Reinitialise()
        {
            managerReady = false;
            UpdateStatusbar("Initialising...");
            ResetImages();
            ResetProgress();
            SetupManager();
            if (Properties.Settings.Default.Autostart)
            {
                UpdateStatusbar("Automatically requesting registration (autostart enabled)");
                manager.RequestRegister();
            }
            // work  out what the "step" should be on the progress bar
            int progressBarMaximum = 15;
            if (Properties.Settings.Default.RFIDEnabled)
            {
                // two steps for reading and confirming RFID
                progressBarMaximum += 10;
            }
            if (Properties.Settings.Default.MagstripeEnabled)
            {
                // two steps for encoding and confirming RFID
                progressBarMaximum += 10;
            }
            if (Properties.Settings.Default.PrinterType.Equals("Enduro"))
            {
                // 6 steps for feeding, confirmed feed, ejecting and confirming eject.
                progressBarMaximum += 10;
            }
            progbarPrinting.Maximum = progressBarMaximum;
            progbarPrinting.Step = 5;
        }

        private void SetupManager()
        {
            UpdateStatusbar("Setting up manager");
            manager = new PrinterManager(Properties.Settings.Default.PrinterID,
                Properties.Settings.Default.LocalPrinter,
                Properties.Settings.Default.PrinterLocation);

            manager.Registered += manager_Registered;
            manager.Unregistered += manager_Unregistered;
            manager.CardRequest += manager_CardRequest;
            manager.CardReady += manager_CardReady;
            manager.CardLoad += manager_CardLoad;
            manager.CardLoadSuccess += manager_CardLoadSuccess;
            manager.CardLoadFailed += manager_CardLoadFailed;
            manager.MagEncode += manager_MagEncode;
            manager.MagEncodeFailed += manager_MagEncodeFailed;
            manager.MagEncodeSuccess += manager_MagEncodeSuccess;
            manager.Print += manager_Print;
            manager.PrintFail += manager_PrintFail;
            manager.PrintSuccess += manager_PrintSuccess;
            manager.RFIDRead += manager_RFIDRead;
            manager.RFIDReadTimeout += manager_RFIDReadTimeout;
            manager.RFIDReadSuccess += manager_RFIDReadSuccess;
            manager.RFIDRemoved += manager_RFIDRemoved;
            
            lblProgressText.Text = "";
            managerRunning = false;
            managerReady = true;
            UpdateStatusbar("Manager set up");
        }

        private void manager_CardReady(object sender, PrinterEventArgs e)
        {
            UpdateCardDetails(e.Card);
            UpdateStatusbar("New card ready to print: " + e.Card.member_id);
            UpdateProgress("Ready to print");

            if (cbUnattended.Checked)
            {
                UpdateStatusbar("Printing automatically (unattended mode)");
                manager.RequestPrint();
            } else { 
                PrintButtonEnable(true);
            }
        }

        private void manager_CardLoad(object sender, PrinterEventArgs e)
        {
            UpdateStatusbar("Loading card into mechanism");
            UpdateProgress("Loading card...");
            PrintButtonEnable(false);
        }

        private void manager_CardLoadSuccess(object sender, PrinterEventArgs e)
        {
            UpdateStatusbar("Successfully loaded card");
            UpdateProgress("Card loaded");
        }

        private void manager_CardLoadFailed(object sender, PrinterEventArgs e)
        {
            UpdateStatusbar("Loading card FAILED - " + e.Status);
        }

        private void manager_CardRequest(object sender, PrinterEventArgs e)
        {
            ResetImages();
            ResetProgress();
            UpdateStatusbar("Requesting new cards...");
        }

        private void manager_MagEncode(object sender, PrinterEventArgs e)
        {
            UpdateStatusbar("Encoding card magnetic strip");
        }

        private void manager_MagEncodeFailed(object sender, PrinterEventArgs e)
        {
            UpdateStatusbar("Magnetic strip encoding FAILED " + e.Status);
        }

        private void manager_MagEncodeSuccess(object sender, PrinterEventArgs e)
        {
            UpdateStatusbar("Magnetic strip encoding completed successfully");
            UpdateProgress("Encoded mag strip");
        }

        private void manager_Print(object sender, PrinterEventArgs e)
        {
            UpdateProgress("Printing...");
            UpdateStatusbar("Printing card");
        }

        private void manager_PrintFail(object sender, PrinterEventArgs e)
        {
            UpdateStatusbar("Printing card FAILED " + e.Status);
        }

        private void manager_PrintSuccess(object sender, PrinterEventArgs e)
        {
            UpdateStatusbar("Printing card completed successfully");
            UpdateProgress("Printed OK");
        }

        private void manager_RFIDRead(object sender, PrinterEventArgs e)
        {
            UpdateProgress("Waiting for RFID");
            UpdateStatusbar("Reading RFID");
        }

        private void manager_RFIDReadTimeout(object sender, PrinterEventArgs e)
        {
            UpdateStatusbar("RFID Reading Error (card must be presented within 30 seconds)");
        }

        private void manager_RFIDReadSuccess(object sender, PrinterEventArgs e)
        {
            UpdateProgress("RFID Read");
            UpdateStatusbar("RFID Read successfully: " + e.Status + " (please remove card from the reader)");
        }

        private void manager_RFIDRemoved(object sender, PrinterEventArgs e)
        {
            UpdateProgress("RFID Removed");
            UpdateStatusbar("Card removed from RFID Reader");
        }

        private void manager_Debug(object sender, DebugEventArgs e)
        {
            if (Properties.Settings.Default.Debug)
            {
                UpdateStatusbar(string.Join("", e.message.Take(255)));
                //tbMabelStatus.Text = e.url;
            }
        }

        private void manager_UpdateInfo(object sender, PrinterEventArgs e)
        {
            PrinterInfo info = e.Info;
            // update all the fields
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
        delegate void UpdateStatsbarDelegate(String text);

        private void UpdateStatusbar(String text)
        {
            if (tbStatusBar.InvokeRequired == false)
            {
                // on the same thread
                tbStatusBar.Text = text;
                lvLog.Items.Add(new ListViewItem(new string[] { DateTime.Now.ToString("dd/MM/yyyy H:mm:ss"), text }));
                lvLog.Items[lvLog.Items.Count - 1].EnsureVisible();
                //lvLog.Items.Add(  , text);
            } else { 
                UpdateStatsbarDelegate updateSb = new UpdateStatsbarDelegate(UpdateStatusbar);
                this.BeginInvoke(updateSb, new object[] { text });
            }
        }

        delegate void UpdateProgressDelegate(String text);

        private void UpdateProgress(String text)
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
                this.BeginInvoke(updateprog, new object[] { text });
            }
        }

        delegate void ResetProgressDelegate();

        private void ResetProgress()
        {
            if (tbStatusBar.InvokeRequired == false)
            {
                progbarPrinting.Value = 0;
                lblProgressText.Text = "";
            }
            else
            {
                ResetProgressDelegate updateprog = new ResetProgressDelegate(ResetProgress);
                this.BeginInvoke(updateprog, new object[] {  });
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

        private void MainForm_Load(object sender, EventArgs e)
        {
            Reinitialise();
        }

        private void ResetImages()
        {
            Image frontImage = (Image) blankCard.Clone();
            Image backImage = (Image) blankCard.Clone();

            if (Properties.Settings.Default.Orientation.Equals("Portrait"))
            {
                frontImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                backImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
            }
            pbCardBack.Image = frontImage;
            pbCardFront.Image = backImage;
        }

        delegate void RunManagerDelegate(PrinterManager manager);

        private void StartManager(PrinterManager manager)
        {
            manager.DoWork();
            //UpdateStatusbar("ERROR:  " + ex.Message);
        }

        private void RunManager()
        {
            RunManagerDelegate runManager = new RunManagerDelegate(StartManager);
            runManager.BeginInvoke(manager, null, null);
        }
        
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form settings = new SettingsDialog();
            settings.ShowDialog();
            Reinitialise();
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
            if (!managerReady)
                return;
            if (managerRunning)
                return;
            if (cbUnattended.Checked)
            {
                if (!managerRegistered)
                {
                    UpdateStatusbar("Automatically requesting registration");
                    manager.RequestRegister();
                }
            }
            RunManager();
        }

        private void connectToMABELToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!managerRegistered)
            {
                UpdateStatusbar("Requesting registration");
                manager.RequestRegister();
            }
        }

        private void disconnectFromMABELToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (managerRegistered)
            {
                manager.Unregister();
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
            manager.RequestPrint();
        }
    }
}
