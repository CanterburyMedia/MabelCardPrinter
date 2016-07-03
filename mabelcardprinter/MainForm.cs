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
        private ViewPrinterInfo printerInfoDialogue;

        public MainForm()
        {
            InitializeComponent();
            this.printerInfoDialogue = new ViewPrinterInfo();
            blankCard = Properties.Resources.CM_Cardblank;
        }

        private void Reinitialise()
        {
            managerReady = false;
            UpdateStatusbar("Initialising...");
            ResetCardForm();
            ResetProgress();
            PrintButtonEnable(false);
            SetupManager();

            if (Properties.Settings.Default.Autostart)
            {
                UpdateStatusbar("Automatically requesting registration (autostart enabled)");
                manager.RequestRegister();
            }
            if (Properties.Settings.Default.AutoUnattended)
            {
                UpdateStatusbar("Automatically enabling unattended mode");
                cbUnattended.Checked = true;
            }
            // work  out what the "step" should be on the progress bar
            int progressBarMaximum = 15;
            if (Properties.Settings.Default.RFIDEnabled)
            {
                // two steps for reading and confirming RFID
                progressBarMaximum += 15;
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
            manager.PrinterUpdate += manager_UpdateInfo;
            manager.Debug += manager_Debug; 

            lblProgressText.Text = "";
            managerRunning = false;
            manager.StartUp();
            managerReady = true;
            UpdateStatusbar("Manager set up");
            EnableAbortRetry(false);
            PrintButtonEnable(false);
        }

        private void manager_CardReady(object sender, PrinterEventArgs e)
        {
            UpdateCardDetails(e.Card);
            UpdateStatusbar("New card ready to print: " + e.Card.member_id);
            EnableAbortRetry(true); // abort skips to the next card
            UpdateProgress("Ready to print");

            if (cbUnattended.Checked)
            {
                UpdateStatusbar("Printing automatically (unattended mode)");
                PrintButtonEnable(false);
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
            EnableAbortRetry(true);
        }

        private void manager_CardLoadSuccess(object sender, PrinterEventArgs e)
        {
            UpdateStatusbar("Successfully loaded card");
            UpdateProgress("Card loaded");
            EnableAbortRetry(true);
            PrintButtonEnable(false);
        }

        private void manager_CardLoadFailed(object sender, PrinterEventArgs e)
        {
            UpdateStatusbar("Loading card FAILED - " + e.Status);
            PrintButtonEnable(false);
            EnableAbortRetry(true);
        }

        private void manager_CardRequest(object sender, PrinterEventArgs e)
        {
            ResetCardForm();
            ResetProgress();
            EnableAbortRetry(false);
            PrintButtonEnable(false);
            //UpdateStatusbar("Requesting new cards...");
        }

        private void manager_MagEncode(object sender, PrinterEventArgs e)
        {
            EnableAbortRetry(true);
            PrintButtonEnable(false);
            UpdateStatusbar("Encoding card magnetic strip");
        }

        private void manager_MagEncodeFailed(object sender, PrinterEventArgs e)
        {
            EnableAbortRetry(true);
            PrintButtonEnable(false);
            UpdateStatusbar("Magnetic strip encoding FAILED " + e.Status);
        }

        private void manager_MagEncodeSuccess(object sender, PrinterEventArgs e)
        {
            EnableAbortRetry(true);
            PrintButtonEnable(false);
            UpdateStatusbar("Magnetic strip encoding completed successfully");
            UpdateProgress("Encoded mag strip");
        }

        private void manager_Print(object sender, PrinterEventArgs e)
        {
            UpdateProgress("Printing...");
            UpdateStatusbar("Printing card");
            EnableAbortRetry(true);
            PrintButtonEnable(false);
        }

        private void manager_PrintFail(object sender, PrinterEventArgs e)
        {
            UpdateStatusbar("Printing card FAILED " + e.Status);
            EnableAbortRetry(true);
            PrintButtonEnable(false);
        }

        private void manager_PrintSuccess(object sender, PrinterEventArgs e)
        {
            EnableAbortRetry(true);
            PrintButtonEnable(false);
            UpdateStatusbar("Printing card completed successfully");
            UpdateProgress("Printed OK");
        }

        private void manager_RFIDRead(object sender, PrinterEventArgs e)
        {
            UpdateProgress("Place card on RFID Reader");
            EnableAbortRetry(true);
            PrintButtonEnable(false);
            UpdateStatusbar("Reading RFID");
        }

        private void manager_RFIDReadTimeout(object sender, PrinterEventArgs e)
        {
            UpdateStatusbar("RFID Reading Error (card must be presented within " + Properties.Settings.Default.RFIDTimeout + " seconds)");
            PrintButtonEnable(false);
            EnableAbortRetry(true);
        }

        private void manager_RFIDReadSuccess(object sender, PrinterEventArgs e)
        {
            EnableAbortRetry(false);
            PrintButtonEnable(false);
            UpdateProgress("RFID Read OK (please remove)");
            UpdateStatusbar("RFID Read successfully: " + e.Status + " (please remove card from the reader)");
        }

        private void manager_RFIDRemoved(object sender, PrinterEventArgs e)
        {
            EnableAbortRetry(false);
            UpdateProgress("RFID Removed");
            UpdateStatusbar("Card removed from RFID Reader");
        }

        private void manager_Debug(object sender, DebugEventArgs e)
        {
            if (Properties.Settings.Default.Debug)
            {
                UpdateStatusbar(string.Join("", e.message.Take(255)));
            }
        }

        private void manager_UpdateInfo(object sender, PrinterEventArgs e)
        {
            PrinterInfo info = e.Info;
            printerInfoDialogue.UpdateInfo(info);
            UpdateInfo(info);
        }
        delegate void UpdateInfoDelegate(PrinterInfo p);

        private void UpdateInfo(PrinterInfo p)
        {
            if (tbPrinterStatus.InvokeRequired == false)
            {
                tbPrinterStatus.Text = (p.bPrinterConnected) ? "Connected" : "Not Connected";
                tbPrinterName.Text = new String(p.sModel);
            } else
            {
                UpdateInfoDelegate updateInfo = new UpdateInfoDelegate(UpdateInfo);
                this.BeginInvoke(updateInfo, new object[] { p });
            }

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

        delegate void EnableAbortRetryDelegate(bool onoff);

        private void EnableAbortRetry(bool onoff)
        {
            if (tbStatusBar.InvokeRequired == false)
            {
                // on the same thread
                btnAbort.Enabled = onoff;
                btnRetry.Enabled = onoff;
             }
            else
            {
                EnableAbortRetryDelegate enableAbortRetry = new EnableAbortRetryDelegate(EnableAbortRetry);
                this.BeginInvoke(enableAbortRetry, new object[] { onoff });
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
                if (card != null)
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
                    tbCardId.Text = card.card_id.ToString();
                } 
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
        delegate void ResetCardFormDelegate();

        private void ResetCardForm()
        {
            if (tbMemberId.InvokeRequired == false)
            {
                Image frontImage = (Image)blankCard.Clone();
                Image backImage = (Image)blankCard.Clone();

                if (Properties.Settings.Default.Orientation.Equals("Portrait"))
                {
                    frontImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    backImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
                }
                pbCardBack.Image = frontImage;
                pbCardFront.Image = backImage;
                tbMemberId.Text = "";
                tbCardId.Text = "";
                tbMagId.Text = "";
            } else
            {
                ResetCardFormDelegate reset = new ResetCardFormDelegate(ResetCardForm);
                this.Invoke(reset);
            }
            
        }

        delegate void RunManagerDelegate(PrinterManager manager);

        private void StartManager(PrinterManager manager)
        {
            manager.DoWork();
        }

        private void RunManager()
        {
            RunManagerDelegate runManager = new RunManagerDelegate(StartManager);
            runManager.BeginInvoke(manager, null, null);
        }
        
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form settings = new SettingsDialog();
            settings.Size = new Size(600, 100);
            settings.ShowDialog();
            Reinitialise();
        }

        private void viewPrinterStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            printerInfoDialogue.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            manager.Unregister();
            Application.Exit();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void UpdateMabelStatus()
        {
            MabelAPI mabel_api = new MabelAPI();
            MabelResponse resp = mabel_api.MabelSays();
            if (manager == null)
                return;
            if (!resp.isError)
            {
                tbMabelStatus.Text = resp.message;
                tbMabelStatus.BackColor = Color.LightGreen;
                // get the number of pending prints
                MabelResponse pendingResp = mabel_api.GetPendingPrints(manager.GetPrinterId());
                if (!pendingResp.isError)
                {
                    tbQueueSize.Text = pendingResp.count.ToString();
                }
            } else
            {
                tbMabelStatus.Text = resp.message;
                tbMabelStatus.BackColor = Color.LightPink;
            }
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
                ResetCardForm();
                ResetProgress();
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

        private void btnRetry_Click(object sender, EventArgs e)
        {
            manager.Retry();
        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            manager.Abort();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

        private void bgManagerWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (manager != null)
            {
         
                manager.DoWork();
      
            }
                
        }

        private void managerPollTimer_Tick_1(object sender, EventArgs e)
        {
            UpdateMabelStatus();
            if (cbUnattended.Checked)
            {
                if (!managerRegistered)
                {
                    UpdateStatusbar("Automatically requesting registration");
                    manager.RequestRegister();
                }
            }

            if (!bgManagerWorker.IsBusy)
                bgManagerWorker.RunWorkerAsync();
        }
    }
}
