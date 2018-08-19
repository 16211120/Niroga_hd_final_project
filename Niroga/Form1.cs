using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Niroga
{
    public partial class Form1 : Form
    {
        SqlConnection sc = new SqlConnection("Data Source=Mahesha-PC\\SQL2012;Initial Catalog=patient_management_db;Integrated Security=True");
        SqlCommand cmd;                       
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.TextLength == 0)
            {
                errorProvider1.SetError(textBox1, "Enter Username");
            }
            else
            {
                errorProvider1.Clear();
            }

            if (textBox2.TextLength == 0)
            {
                errorProvider2.SetError(textBox2, "Enter Password");
            }
            else
            {
                errorProvider2.Clear();
            }

            if (textBox1.TextLength != 0 && textBox2.TextLength != 0)
            {
                try
                {
                    sc.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("select count(*) from log where u_nsme ='" + textBox1.Text + "'and pass = '" + textBox2.Text + "' ", sc);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        //MessageBox.Show("hari");
                        frm_menu fm = new frm_menu();
                        fm.Show();

                        textBox1.Clear();
                        textBox2.Clear();
                        

                    }
                    else
                    {
                        MessageBox.Show("Please Check Your Username & Password", " Warning !!! ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    sc.Close();
                }


                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
            else if (textBox2.TextLength != 0 && textBox1.TextLength != 0)
            {
                
                MessageBox.Show("Password or username incorrect", " Warning !!! ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
        }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox3.TextLength == 0)
            {
                errorProvider1.SetError(textBox3, "Enter Password");
            }
            else
            {
                errorProvider1.Clear();
            }

            if (textBox4.TextLength == 0)
            {
                errorProvider2.SetError(textBox4, "Enter user name");
            }
            else
            {
                errorProvider2.Clear();
            }

            if (textBox3.TextLength != 0 && textBox4.TextLength != 0)
            {
                DialogResult dr = MessageBox.Show("You need to change your password?", "Change Password", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr.ToString() == "Yes")
                {
                    try
                    {
                        sc.Open();
                        SqlDataAdapter sda = new SqlDataAdapter("select count(*) from log where pass = '" + textBox3.Text + "' ", sc);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);

                        if (dt.Rows[0][0].ToString() == "1")
                        {

                            cmd = new SqlCommand("update log set u_nsme ='" + textBox4.Text + "', pass = '" + textBox3.Text + "' ", sc);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Changed successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            textBox4.Clear();
                            textBox3.Clear();

                        }
                        else
                        {
                            MessageBox.Show("Curent Password is incorrect", "Not Matching", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        sc.Close();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox3.Clear();
            textBox4.Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (txt_cpass.TextLength == 0)
            {
                errorProvider1.SetError(txt_cpass, "Enter Current Password");
            }
            else
            {
                errorProvider1.Clear();
            }

            if (txt_new_pass.TextLength == 0)
            {
                errorProvider2.SetError(txt_new_pass, "Enter New Password");
            }
            else
            {
                errorProvider2.Clear();
            }

            if (txt_re_pass.TextLength == 0)
            {
                errorProvider3.SetError(txt_re_pass, "Enter Re-Enter Password");
            }
            else
            {
                errorProvider3.Clear();
            }
            if (txt_new_pass.Text != txt_re_pass.Text)
            {
                MessageBox.Show("Password does not match. Please re enter password", "warning !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_cpass.Clear();
                txt_new_pass.Clear();
                txt_re_pass.Clear();
            }

            if (txt_cpass.TextLength != 0 && txt_new_pass.TextLength != 0 && txt_re_pass.TextLength != 0)
            {

                DialogResult dr = MessageBox.Show("You need to change your password?", "Change Password", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr.ToString() == "Yes")
                {

                    try
                    {
                        sc.Open();
                        SqlDataAdapter sda = new SqlDataAdapter("select count(*) from log where pass = '" + txt_cpass.Text + "' ", sc);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);

                        if (dt.Rows[0][0].ToString() == "1")
                        {

                            cmd = new SqlCommand("update log set pass = '" + txt_new_pass.Text + "' ", sc);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Changed successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            txt_cpass.Clear();
                            txt_new_pass.Clear();
                            txt_re_pass.Clear();

                        }
                        else
                        {
                            MessageBox.Show("Curent Password is incorrect", "Not Matching", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        sc.Close();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    txt_cpass.Clear();
                    txt_new_pass.Clear();
                    txt_re_pass.Clear();
                }

            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            txt_re_pass.Clear();
            txt_new_pass.Clear();
            txt_cpass.Clear();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void txt_re_pass_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_new_pass_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_cpass_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
