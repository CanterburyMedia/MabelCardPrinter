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

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void SettingsDialog_Load(object sender, EventArgs e)
        {
            this.PopulateInstalledPrintersCombo();
        }

        private void PopulateInstalledPrintersCombo()
        {
            // Add list of installed printers found to the combo box.
            // The pkInstalledPrinters string will be used to provide the display string.
            String pkInstalledPrinters;
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                pkInstalledPrinters = PrinterSettings.InstalledPrinters[i];
                cbPrinters.Items.Add(pkInstalledPrinters);
            }
        }

        private void cbPrinters_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Set the printer to a printer in the combo box when the selection changes.
            PrintDocument printDoc = new PrintDocument();
            if (cbPrinters.SelectedIndex != -1)
            {
                // The combo box's Text property returns the selected item's text, which is the printer name.
                printDoc.PrinterSettings.PrinterName = cbPrinters.Text;
            }
        }
    }
}
