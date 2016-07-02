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
        private MagiCardAPI magi_api;
        public ViewPrinterInfo(MagiCardAPI magi_api)
        {
            this.magi_api = magi_api;
            InitializeComponent();
            GetData();
            PrintDataOntoForm();
        }

        private void GetData()
        {
            if (magi_api == null)
                return;
            if (Properties.Settings.Default.PrinterType.Equals("Magicard"))
            {
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
            if (magi_api == null)
                return;
            items.Add(new ListViewItem("Model",new String(_info.sModel)));

            ListViewItem[] arr = items.ToArray();
            lvParamVal.Items.AddRange(arr);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetData();
            PrintDataOntoForm();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
