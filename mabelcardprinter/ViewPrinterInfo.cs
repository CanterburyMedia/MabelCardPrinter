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
        private PrinterManager manager;

        public ViewPrinterInfo(PrinterManager manager)
        {
            InitializeComponent();
            this.manager = manager;
            manager.PrinterUpdate += GetData;
        }

        private void GetData(Object sender, PrinterEventArgs e)
        {
        }

        delegate void PrintDataOntoFormDelegate();

        private void PrintDataOntoForm()
        {
            if (lvParamVal.InvokeRequired == false)
            { 
                lvParamVal.Clear();
                var items = new List<ListViewItem>();
                _info = manager.GetPrinterInfo();
                items.Add(new ListViewItem("Model",new String(_info.sModel)));

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
    }
}
