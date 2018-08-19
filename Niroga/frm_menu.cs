using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Niroga
{
    public partial class frm_menu : Form
    {
        public frm_menu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frm_patient fp = new frm_patient();
            fp.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frm_visit fv = new frm_visit();
            fv.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frm_drug fd = new frm_drug();
            fd.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            frm_report fr = new frm_report();
            fr.Show();
            this.Close();

        }
    }
}
