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
    public partial class ViewPrinterInfo : Form
    {
        private PrinterInfo _info;

        public ViewPrinterInfo(PrinterInfo info)
        {
            InitializeComponent();
            _info = info;
        }

        public void UpdateInfo(PrinterInfo info)
        {
            this._info = info;
        }

        delegate void PrintDataOntoFormDelegate();

        private void PrintDataOntoForm()
        {
            if (lvParamVal.InvokeRequired == false)
            { 
                //lvParamVal.Clear();
                var items = new List<ListViewItem>();
                lvParamVal.Items.Add("Model",new String(_info.sModel));
                lvParamVal.Items.Add("Connected", (_info.bPrinterConnected) ? "Connected" : "Not connected");
                lvParamVal.Items.Add("Printer Serial", new String(_info.sPrinterSerial));
                lvParamVal.Items.Add("Print Head Serial", new String(_info.sPrintheadSerial));
                lvParamVal.Items.Add("PCB Serial", new String(_info.sPCBSerial));
                lvParamVal.Items.Add("Hand Feed", _info.iHandFeed.ToString());
                lvParamVal.Items.Add("Cards Printed", _info.iCardsPrinted.ToString());
                lvParamVal.Items.Add("Cards on print head", _info.iCardsOnPrinthead.ToString());
                lvParamVal.Items.Add("Dye panels printed", _info.iDyePanelsPrinted.ToString());
                lvParamVal.Items.Add("Cleans since shipped", _info.iCleansSinceShipped.ToString());
                lvParamVal.Items.Add("Dye panels since clean", _info.iDyePanelsSinceClean.ToString());
                lvParamVal.Items.Add("Cards since last clean",_info.iCardsSinceClean.ToString());
                
            } else
            {
                PrintDataOntoFormDelegate printData = new PrintDataOntoFormDelegate(PrintDataOntoForm);
                this.Invoke(printData);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PrintDataOntoForm();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ViewPrinterInfo_Load(object sender, EventArgs e)
        {
            PrintDataOntoForm();
        }
    }
}
