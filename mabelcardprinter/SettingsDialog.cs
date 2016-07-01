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
            SaveSettings();
            this.Close();
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
                rbGenericType.Checked = true;
            if (Properties.Settings.Default.PrinterType.Equals("Magicard"))
                rbMagicardType.Checked = true;
                
            if (Properties.Settings.Default.Orientation.Equals("Landscape"))
                rbLandscape.Checked = true;
            if (Properties.Settings.Default.Orientation.Equals("Portrait"))
                rbPortrait.Checked  = true;
            cbDontPrint.Checked = Properties.Settings.Default.DontPrint;
            cbAutoStart.Checked = Properties.Settings.Default.Autostart;
        }

        private void SaveSettings()
        {
            if (testWorks)
            { 
               Properties.Settings.Default.apiBaseUrl = tbMabelUrl.Text;
               Properties.Settings.Default.APIKey = tbApiKey.Text;
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
            Properties.Settings.Default.Save();

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
    }
}
