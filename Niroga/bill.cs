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
    public partial class bill : Form
    {
        public bill()
        {
            InitializeComponent();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void bill_Load(object sender, EventArgs e)
        {
            label3.Text = DateTime.Now.ToString();

            label14.Text = frm_visit.SetValue1;
            label15.Text = frm_visit.SetValue2;
            label16.Text = frm_visit.SetValue3;
            label17.Text = frm_visit.SetValue4;
            label18.Text = frm_visit.SetValue5;
            label19.Text = frm_visit.SetValue6;
            label20.Text = frm_visit.SetValue7;
            label21.Text = frm_visit.SetValue8;


           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            printDialog1.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
