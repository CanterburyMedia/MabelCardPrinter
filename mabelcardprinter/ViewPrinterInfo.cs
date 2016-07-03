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
                items.Add(new ListViewItem("Model",new String(_info.sModel)));
                items.Add(new ListViewItem("Connected", (_info.bPrinterConnected) ? "Connected" : "Not connected"));
                items.Add(new ListViewItem("Printer Serial", new String(_info.sPrinterSerial)));
                items.Add(new ListViewItem("Print Head Serial", new String(_info.sPrintheadSerial)));
                items.Add(new ListViewItem("PCB Serial", new String(_info.sPCBSerial)));
                items.Add(new ListViewItem("Hand Feed", _info.iHandFeed.ToString()));
                items.Add(new ListViewItem("Cards Printed", _info.iCardsPrinted.ToString()));
                items.Add(new ListViewItem("Cards on print head", _info.iCardsOnPrinthead.ToString()));
                items.Add(new ListViewItem("Dye panels printed", _info.iDyePanelsPrinted.ToString()));
                items.Add(new ListViewItem("Cleans since shipped", _info.iCleansSinceShipped.ToString()));
                items.Add(new ListViewItem("Dye panels since clean", _info.iDyePanelsSinceClean.ToString()));
                items.Add(new ListViewItem("Cards since last clean",_info.iCardsSinceClean.ToString()));

                ListViewItem[] arr = items.ToArray();
                lvParamVal.Items.AddRange(arr);
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
