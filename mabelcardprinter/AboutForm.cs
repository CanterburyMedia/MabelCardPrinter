using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace MabelCardPrinter
{
    public partial class AboutForm : Form
    {
        SoundPlayer sound;
        public AboutForm()
        {
            InitializeComponent();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            sound = new SoundPlayer(Properties.Resources.Rocket);
            sound.Play();
        }

        private void AboutForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            sound.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
