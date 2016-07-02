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
        public ViewPrinterInfo()
        {
            InitializeComponent();
            GetData();
            PrintDataOntoForm();
        }

        private void GetData()
        {
            if (Properties.Settings.Default.PrinterType.Equals("Magicard"))
            {
                PrintDocument printDoc = new PrintDocument();
                printDoc.PrinterSettings.PrinterName = Properties.Settings.Default.LocalPrinter;
                MagiCardAPI magi_api = new MagiCardAPI(printDoc.PrinterSettings.CreateMeasurementGraphics().GetHdc());
                magi_api.EnableReporting();
                _info = magi_api.GetPrinterInfoA();
                magi_api.DisableReporting();
            } else
            {
                _info = null;
            }
        }

        private void PrintDataOntoForm()
        {
            lvParamVal.Clear();
            var items = new List<ListViewItem>();
            items.Add(new ListViewItem("Model",new String(_info.sModel)));

            ListViewItem[] arr = items.ToArray();
            lvParamVal.Items.AddRange(arr);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetData();
            PrintDataOntoForm();
        }
    }
}
