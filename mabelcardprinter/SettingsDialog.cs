using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace MabelCardPrinter
{
    public partial class SettingsDialog : Form
    {
        private bool testWorks = false;
        public SettingsDialog()
        {
            InitializeComponent();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (SaveSettings())
            {
                this.Close();
            }
            
        }

        private void SettingsDialog_Load(object sender, EventArgs e)
        {
            this.PopulateInstalledPrintersCombo();
            tbMabelUrl.Text = (String) Properties.Settings.Default.apiBaseUrl;
            tbApiKey.Text = (String) Properties.Settings.Default.APIKey;
            tbPrinterLocation.Text = Properties.Settings.Default.PrinterLocation;
            tbPrinterId.Text = Properties.Settings.Default.PrinterID.ToString();

            chbDebug.Checked = Properties.Settings.Default.Debug;
            chbMagstripe.Checked = Properties.Settings.Default.MagstripeEnabled;
            chbRFID.Checked = Properties.Settings.Default.RFIDEnabled;

            if (Properties.Settings.Default.PrinterType.Equals("Generic"))
            {
                rbGenericType.Checked = true;
                EnableMagicardFunctionality(false);
            }

            if (Properties.Settings.Default.PrinterType.Equals("Magicard"))
            {
                rbMagicardType.Checked = true;
                EnableMagicardFunctionality(true);
            }

            if (Properties.Settings.Default.Orientation.Equals("Landscape"))
                rbLandscape.Checked = true;
            if (Properties.Settings.Default.Orientation.Equals("Portrait"))
                rbPortrait.Checked  = true;
            cbDontPrint.Checked = Properties.Settings.Default.DontPrint;
            cbAutoStart.Checked = Properties.Settings.Default.Autostart;
            cbAutoUnattended.Checked = Properties.Settings.Default.AutoUnattended;
            for(int i=0;i<comboRfidAutoRemove.Items.Count;i++)
            {
                if (comboRfidAutoRemove.Items[i].Equals(Properties.Settings.Default.RFIDAutoremove.ToString()))
                {
                    comboRfidAutoRemove.SelectedIndex = i;
                }
            }
            for (int i = 0; i < comboRfidTimeout.Items.Count; i++)
            {
                if (comboRfidTimeout.Items[i].Equals(Properties.Settings.Default.RFIDTimeout.ToString()))
                {
                    comboRfidTimeout.SelectedIndex = i;
                }
            }
            this.ValidateChildren();
        }

        private bool SaveSettings()
        {
            if (testWorks)
            { 
               Properties.Settings.Default.apiBaseUrl = tbMabelUrl.Text;
               Properties.Settings.Default.APIKey = tbApiKey.Text;
            } else
            {
                settingsErrorProvider.SetError(tbTestResponse, "Please test connection before saving");
                return false;
            }
            Properties.Settings.Default.Debug = chbDebug.Checked;
            Properties.Settings.Default.MagstripeEnabled = chbMagstripe.Checked;
            Properties.Settings.Default.RFIDEnabled = chbRFID.Checked;
            
            Properties.Settings.Default.PrinterLocation = tbPrinterLocation.Text;
            Properties.Settings.Default.PrinterID = Int32.Parse(tbPrinterId.Text);

            if (rbGenericType.Checked)
                Properties.Settings.Default.PrinterType = "Generic";
            if (rbMagicardType.Checked)
                Properties.Settings.Default.PrinterType = "Magicard";
            if (rbLandscape.Checked)
                Properties.Settings.Default.Orientation = "Landscape";
            if (rbPortrait.Checked)
                Properties.Settings.Default.Orientation = "Portrait";
            Properties.Settings.Default.DontPrint = cbDontPrint.Checked;
            Properties.Settings.Default.Autostart = cbAutoStart.Checked;
            Properties.Settings.Default.AutoUnattended = cbAutoUnattended.Checked;
            Properties.Settings.Default.RFIDTimeout = Int32.Parse((string) comboRfidTimeout.SelectedItem);
            Properties.Settings.Default.RFIDAutoremove = Int32.Parse((string) comboRfidAutoRemove.SelectedItem);
            Properties.Settings.Default.Save();
            return true;

        }

        private void PopulateInstalledPrintersCombo()
        {
            // Add list of installed printers found to the combo box.
            // The pkInstalledPrinters string will be used to provide the display string.
            String pkInstalledPrinters;
            int indexSelected = -1;
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                pkInstalledPrinters = PrinterSettings.InstalledPrinters[i];
                if (pkInstalledPrinters.Equals(Properties.Settings.Default.LocalPrinter))
                {
                    // have this item selected
                    indexSelected = i;
                }
                cbPrinters.Items.Add(pkInstalledPrinters);
            }
            cbPrinters.SelectedIndex = indexSelected;
        }

        private void cbPrinters_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPrinters.SelectedIndex != -1)
            {
                // The combo box's Text property returns the selected item's text, which is the printer name.
                Properties.Settings.Default.LocalPrinter = cbPrinters.Text;
            }
        }
        
        private void btnTestMabelConn_Click(object sender, EventArgs e)
        {
            MabelAPI mabel_api = new MabelAPI();
            mabel_api.setBaseUrl(tbMabelUrl.Text);
            try
            {
                MabelResponse resp = mabel_api.MabelSays();
                tbTestResponse.Text = "MABEL Response: " + resp.message;
                if (!resp.isError)
                {
                    testWorks = true;
                    tbTestResponse.BackColor = Color.LightGreen;
                    settingsErrorProvider.SetError(tbTestResponse, "");
                }
                else
                {
                    if (resp.results != null)
                    {
                        tbTestResponse.Text += resp.results;
                    }
                    testWorks = false;
                    tbTestResponse.BackColor = Color.LightPink;
                    settingsErrorProvider.SetError(tbTestResponse, "Error connecting to MABEL");
                }
            } catch (Exception ex)
            {
                testWorks = false;
                tbTestResponse.Text = ex.Message;
                tbTestResponse.BackColor = Color.LightPink;
                settingsErrorProvider.SetError(tbTestResponse, "Error connecting to MABEL");
            }
        }

        private void btnGetSettings_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gbPrinterType_Enter(object sender, EventArgs e)
        {

        }

        private void tbMabelUrl_Validating(object sender, CancelEventArgs e)
        {
            Uri uriResult;
            testWorks = false;
            tbTestResponse.BackColor = Color.White;
            tbTestResponse.Text = "";
            bool result = Uri.TryCreate(tbMabelUrl.Text, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            if (result)
            {
                tbMabelUrl.BackColor = Color.LightGreen;
                settingsErrorProvider.SetError(tbMabelUrl, "");
            }
            else
            {
                tbMabelUrl.BackColor = Color.LightPink;
                this.settingsErrorProvider.SetError(tbMabelUrl, "Must be a valid URL");

            }
        }

        private void chbRFID_CheckedChanged(object sender, EventArgs e)
        {
            if (chbRFID.Checked)
            {
                comboRfidAutoRemove.Enabled = true;
                comboRfidTimeout.Enabled = true;
            } else
            {
                comboRfidAutoRemove.Enabled = false;
                comboRfidTimeout.Enabled = false;
            }
        }

        private void rbMagicardType_CheckedChanged(object sender, EventArgs e)
        {
            EnableMagicardFunctionality(rbMagicardType.Checked);
        }
        private void EnableMagicardFunctionality(bool onoff)
        {
            if (onoff)
            {
                chbMagstripe.Enabled = true;
            }
            else
            {
                chbMagstripe.Enabled = false;
                chbMagstripe.Checked = false;
            }
        }
    }
}
