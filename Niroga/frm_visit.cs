using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Niroga
{
    public partial class frm_visit : Form
    {
        SqlConnection sc = new SqlConnection("Data Source=MAHESHA-PC\\SQL2012;Initial Catalog=patient_management_db;Integrated Security=True");
        SqlCommand cmd,cmd1;

        public static string SetValue1 = "";
        public static string SetValue2 = "";
        public static string SetValue3 = "";
        public static string SetValue4 = "";
        public static string SetValue5 = "";
        public static string SetValue6 = "";
        public static string SetValue7 = "";
        public static string SetValue8 = "";
        public frm_visit()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void frm_visit_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'patient_management_dbDataSet5.visit_income_tbl' table. You can move, or remove it, as needed.
            this.visit_income_tblTableAdapter.Fill(this.patient_management_dbDataSet5.visit_income_tbl);
            // TODO: This line of code loads data into the 'patient_management_dbDataSet4.visit_patient_tbl' table. You can move, or remove it, as needed.
            this.visit_patient_tblTableAdapter.Fill(this.patient_management_dbDataSet4.visit_patient_tbl);
            // TODO: This line of code loads data into the 'patient_management_dbDataSet3.visit_drug_tbl' table. You can move, or remove it, as needed.
            this.visit_drug_tblTableAdapter.Fill(this.patient_management_dbDataSet3.visit_drug_tbl);
            // TODO: This line of code loads data into the 'patient_management_dbDataSet2.drug_tbl' table. You can move, or remove it, as needed.
            this.drug_tblTableAdapter.Fill(this.patient_management_dbDataSet2.drug_tbl);
            label8.Text = DateTime.Now.ToShortTimeString();
            sys_dateDateTimePicker.Value =DateTime.Now;
            // TODO: This line of code loads data into the 'patient_management_dbDataSet1.patient_tb' table. You can move, or remove it, as needed.
            this.patient_tbTableAdapter.Fill(this.patient_management_dbDataSet1.patient_tb);

            sc.Open();
            SqlDataAdapter adapt = new SqlDataAdapter("select * from patient_tb", sc);
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            
             SqlDataAdapter adapt1 = new SqlDataAdapter("select * from drug_tbl", sc);
             DataTable dt1 = new DataTable();
             adapt1.Fill(dt1);
             dataGridView2.DataSource = dt1;

            SqlDataAdapter adapt2 = new SqlDataAdapter("select * from visit_drug_tbl", sc);
            DataTable dt2 = new DataTable();
            adapt2.Fill(dt2);
            dataGridView3.DataSource = dt2;

            sc.Close();

            try
            {

                sc.Open();
                cmd = new SqlCommand("select id from visit_patient_tbl", sc);
                cmd.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                sc.Close();
                if (ds.Tables[0].Rows.Count < 1)
                {
                    idTextBox.Text = "1";
                   
                }
                else
                {
                    sc.Open();
                    String myquery1 = "select max(id) from visit_patient_tbl";
                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.CommandText = myquery1;
                    cmd1.Connection = sc;
                    SqlDataAdapter da1 = new SqlDataAdapter();
                    da1.SelectCommand = cmd1;
                    DataSet ds1 = new DataSet();
                    da1.Fill(ds1);
                    idTextBox.Text = ds1.Tables[0].Rows[0][0].ToString();
                    int a;
                    a = Convert.ToInt16(idTextBox.Text);
                    a = a + 1;
                    idTextBox.Text = a.ToString();
                    
                    sc.Close();
                }


            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frm_menu fm = new frm_menu();
            fm.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frm_patient fp = new frm_patient();
            fp.Show();
            this.Close();
        }

        private void firstnameToolStripTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            sc.Open();
            SqlDataAdapter adapt = new SqlDataAdapter("select * from patient_tb where firstname like '" + firstnameToolStripTextBox.Text + "%'", sc);
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;

             sc.Close();
        }

        private void firstnameToolStripTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(patient_firstnameTextBox.Text == "")
            {
                MessageBox.Show("Please enter Patient Name", "ERROR !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (ageTextBox.Text == "")
            {
                MessageBox.Show("Please enter Patient Age", "ERROR !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (complainTextBox.Text == "")
            {
                MessageBox.Show("Please enter Patient Complain", "ERROR !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (complainTextBox.Text != "")
            {
                try
                {
                    //DateTime bd = dobDateTimePicker.Value.Date;
                     string sysdate = sys_dateDateTimePicker.Value.Date.ToString();
                    sc.Open();
                    cmd = new SqlCommand("insert into visit_patient_tbl (id, patient_firstname, age, sys_date, sys_time, complain) values ('" + idTextBox.Text + "','" + patient_firstnameTextBox.Text + "','" + ageTextBox.Text + "','" + sysdate + "','" + label8.Text + "','" + complainTextBox.Text + "') ", sc);
                    cmd.ExecuteNonQuery();
                   // MessageBox.Show("Data save successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    sc.Close();
                  /*  dataGridView1.Refresh();
                    dataGridView2.Refresh();*/

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    sc.Close();
                }


                label11.Visible = true;
                label12.Visible = true;
                label13.Visible = true;
                label14.Visible = true;
                label15.Visible = true;
                label16.Visible = true;
                label17.Visible = true;
                label18.Visible = true;
                label19.Visible = true;
                label20.Visible = true;
                label21.Visible = true;

                drug_nameTextBox.Visible = true;
                drugnameTextBox.Visible = true;
                doseTextBox.Visible = true;
                avaqtyTextBox.Visible = true;
                qtyTextBox.Visible = true;
                freqComboBox.Visible = true;
                durationTextBox.Visible = true;
                txt_chanelfee.Visible = true;
                txt_totfee.Visible = true;
                txt_drugfee.Visible = true;

                btn_search.Visible = true;
                btn_adddrug.Visible = true;
                btn_deletedrug.Visible = true;
                btn_caltot.Visible = true;
                btn_save.Visible = true;
                button4.Visible = true;

                dataGridView2.Visible = true;
                dataGridView3.Visible = true;


            }

        }

        private void dobDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            int y, m;

            if (DateTime.Today >= dobDateTimePicker.Value)
            {
                if (DateTime.Today.Year == dobDateTimePicker.Value.Year)
                {
                    y = 0;

                    m = (Convert.ToInt16(DateTime.Today.Month) - Convert.ToInt16(dobDateTimePicker.Value.Month));

                    y.ToString();
                    m.ToString();

                    ageTextBox.Text = (y + "Years " + m + "Months");
                }
                else if (DateTime.Today.Month > dobDateTimePicker.Value.Month)
                {
                    y = (Convert.ToInt16(DateTime.Today.Year) - Convert.ToInt16(dobDateTimePicker.Value.Year));

                    m = (Convert.ToInt16(DateTime.Today.Month) - Convert.ToInt16(dobDateTimePicker.Value.Month));

                    y.ToString();
                    m.ToString();

                    ageTextBox.Text = (y + "Years " + m + "Months");
                }
                else if (DateTime.Today.Month < dobDateTimePicker.Value.Month)
                {
                    y = (Convert.ToInt16(DateTime.Today.Year) - Convert.ToInt16(dobDateTimePicker.Value.Year) - 1);

                    m = 12 - Convert.ToInt16(dobDateTimePicker.Value.Month);
                    m = m + Convert.ToInt16(DateTime.Today.Month);

                    y.ToString();
                    m.ToString();

                    ageTextBox.Text = (y + "Years " + m + "Months");

                }

            }
            else
            {
                MessageBox.Show("You selected birthday is invalide", "Invalide Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dobDateTimePicker.Value = DateTime.Today;
            }
        }
        string price;
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            
        }

        private void drug_nameTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            sc.Open();
            SqlDataAdapter adapt = new SqlDataAdapter("select * from drug_tbl where drug_name like '" + drug_nameTextBox.Text + "%'", sc);
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            dataGridView2.DataSource = dt;
            sc.Close();
        }

        private void qtyTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (qtyTextBox.Text != "")
            {
                try
                {
                    int q = Convert.ToInt32(qtyTextBox.Text);
                    double p = Convert.ToDouble(price);
                    txt_drugfee.Text = (p * q).ToString();
                }
                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please enter quantity", "ERROR !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btn_caltot_Click(object sender, EventArgs e)
        {
            if (txt_chanelfee.Text != "" || txt_drugfee.Text != "")
            {
                try
                {
                    double cf = Convert.ToDouble(txt_chanelfee.Text);
                    double df = Convert.ToDouble(txt_drugfee.Text);
                    txt_totfee.Text = (cf + df).ToString();
                }
                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please Fill the Channel Fee & Drug Fee", "ERROR !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (txt_chanelfee.Text == "0.00")
            {
                MessageBox.Show("Please enter Channel Fee", "ERROR !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (txt_drugfee.Text == "0.00")
            {
                MessageBox.Show("Drug Fee is empty", "ERROR !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (txt_totfee.Text == "0.00")
            {
                MessageBox.Show("Please Calculate the Total Fee", "ERROR !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            if (txt_chanelfee.Text != "0.00" && txt_drugfee.Text != "0.00" && txt_totfee.Text != "0.00")
            {
                try
                {
                    string sysdate = sys_dateDateTimePicker.Value.Date.ToString();
                    sc.Open();
                    cmd = new SqlCommand("insert into visit_income_tbl (visit_id, chanel_fee, drug_cost, total, income_date) values ('" + idTextBox.Text + "','" + txt_chanelfee.Text + "','" + txt_drugfee.Text + "','" + txt_totfee.Text + "','" + sysdate + "') ", sc);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data save successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    sc.Close();

                    dataGridView1.Refresh();
                    dataGridView2.Refresh();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    sc.Close();
                }
            }
            
        }

        private void btn_deletedrug_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you need to delete this Drug?", "Delete Drug", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dr.ToString() == "Yes")
            {
                try
                {

                    sc.Open();
                    cmd = new SqlCommand("delete from drug_tbl where drug_name ='" + drugnameTextBox.Text + "'", sc);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Delete successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    sc.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    sc.Close();
                }
            }

            sc.Open();
           
            SqlDataAdapter adapt1 = new SqlDataAdapter("select * from drug_tbl", sc);
            DataTable dt1 = new DataTable();
            adapt1.Fill(dt1);
            dataGridView2.DataSource = dt1;
            sc.Close();

            btn_deletedrug.Enabled = false;
         }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                                                                       
                drugnameTextBox.Text = dataGridView2.CurrentRow.Cells["drugnameDataGridViewTextBoxColumn"].Value.ToString();
                avaqtyTextBox.Text = dataGridView2.CurrentRow.Cells["qtyDataGridViewTextBoxColumn"].Value.ToString();
                price = dataGridView2.CurrentRow.Cells["unitretailpriceDataGridViewTextBoxColumn"].Value.ToString();

            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        string gender;string add;string cno;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                patient_firstnameTextBox.Text = dataGridView1.CurrentRow.Cells["firstnameDataGridViewTextBoxColumn"].Value.ToString();
                ageTextBox.Text = dataGridView1.CurrentRow.Cells["ageDataGridViewTextBoxColumn"].Value.ToString();
                dobDateTimePicker.Text = dataGridView1.CurrentRow.Cells["dobDataGridViewTextBoxColumn"].Value.ToString();
                past_historyTextBox.Text = dataGridView1.CurrentRow.Cells["pasthistoryDataGridViewTextBoxColumn"].Value.ToString();
                allergiesTextBox.Text = dataGridView1.CurrentRow.Cells["allergiesDataGridViewTextBoxColumn"].Value.ToString();
                gender = dataGridView1.CurrentRow.Cells["genderDataGridViewTextBoxColumn"].Value.ToString();
                add = dataGridView1.CurrentRow.Cells["addressDataGridViewTextBoxColumn"].Value.ToString();
                cno = dataGridView1.CurrentRow.Cells["mobilenoDataGridViewTextBoxColumn"].Value.ToString();

            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void qtyTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void durationTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txt_chanelfee_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void frm_visit_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void tabControl1_MouseClick(object sender, MouseEventArgs e)
        {
            sc.Open();
            SqlDataAdapter adapt = new SqlDataAdapter("select * from visit_patient_tbl", sc);
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            dataGridView4.DataSource = dt;

            SqlDataAdapter adapt1 = new SqlDataAdapter("select * from visit_drug_tbl", sc);
            DataTable dt1 = new DataTable();
            adapt1.Fill(dt1);
            dataGridView5.DataSource = dt1;

            SqlDataAdapter adapt2 = new SqlDataAdapter("select * from visit_income_tbl", sc);
            DataTable dt2 = new DataTable();
            adapt2.Fill(dt2);
            dataGridView6.DataSource = dt2;

            sc.Close();
        }

        private void patient_firstnameToolStripTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                sc.Open();
            SqlDataAdapter adapt = new SqlDataAdapter("select * from visit_patient_tbl where patient_firstname like '" + patient_firstnameToolStripTextBox.Text + "%'", sc);
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            dataGridView4.DataSource = dt;
            sc.Close();
            
            }
            catch (SqlException ex) {

                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }


        }
        string id;
        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                id = dataGridView4.CurrentRow.Cells["idDataGridViewTextBoxColumn"].Value.ToString();

                try
                {
                    sc.Open();
                    SqlDataAdapter adapt = new SqlDataAdapter("select * from visit_drug_tbl where visit_id = '" + id + "'", sc);
                    DataTable dt = new DataTable();
                    adapt.Fill(dt);
                    dataGridView5.DataSource = dt;

                    SqlDataAdapter adapt1 = new SqlDataAdapter("select * from visit_income_tbl where visit_id = '" + id + "'", sc);
                    DataTable dt1 = new DataTable();
                    adapt1.Fill(dt1);
                    dataGridView6.DataSource = dt1;
                    
                    sc.Close();

                }
                catch (SqlException ex)
                {

                    MessageBox.Show(ex.Message);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }


            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
           

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            SetValue1 = patient_firstnameTextBox.Text;
            SetValue2 = ageTextBox.Text;
            SetValue3 = gender;
            SetValue4 = add;
            SetValue5 = cno;
            SetValue6 = txt_chanelfee.Text;
            SetValue7 = txt_drugfee.Text;
            SetValue8 = txt_totfee.Text;

           bill bp = new bill();
            bp.Show();
        }

        private void btn_adddrug_Click(object sender, EventArgs e)
        {
            if (drugnameTextBox.Text == "")
            {
                MessageBox.Show("Please enter Drug Name", "ERROR !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (doseTextBox.Text == "")
            {
                MessageBox.Show("Please enter Dose", "ERROR !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (qtyTextBox.Text == "")
            {
                MessageBox.Show("Please enter Quantity", "ERROR !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (freqComboBox.Text == "Select one")
            {
                MessageBox.Show("Please Select Frequency", "ERROR !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (durationTextBox.Text == "")
            {
                MessageBox.Show("Please enter Duration", "ERROR !!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (drugnameTextBox.Text != "" && doseTextBox.Text != "" && qtyTextBox.Text != "" && freqComboBox.Text != "Select one" && durationTextBox.Text != "")
            {
                try
                {
                    int reduct = Convert.ToInt32(avaqtyTextBox.Text) - Convert.ToInt32(qtyTextBox.Text);
                    string sysdate = sys_dateDateTimePicker.Value.Date.ToString();
                    string freq = Convert.ToString(freqComboBox.SelectedItem);
                    sc.Open();
                    cmd = new SqlCommand("insert into visit_drug_tbl (visit_id, drug_name, dose, freq, duration, qty, visitdrug_date) values ('" + idTextBox.Text + "','" + drugnameTextBox.Text + "','" + doseTextBox.Text + "','" + freq + "','" + durationTextBox.Text + "','" + qtyTextBox.Text + "','"+ sysdate + "') ", sc);
                    cmd.ExecuteNonQuery();
                    //MessageBox.Show("Data save successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmd1 = new SqlCommand("update drug_tbl set qty ='"+ reduct + "' where drug_name = '"+ drugnameTextBox.Text + "' ", sc);
                    cmd1.ExecuteNonQuery();
                   // MessageBox.Show("Data Update successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    sc.Close();
                    
                    dataGridView1.Refresh();
                    dataGridView2.Refresh();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    sc.Close();
                }
            }
            sc.Open();
            SqlDataAdapter adapt2 = new SqlDataAdapter("select * from visit_drug_tbl", sc);
            DataTable dt2 = new DataTable();
            adapt2.Fill(dt2);
            dataGridView3.DataSource = dt2;

            sc.Close();

            btn_adddrug.Enabled = false;

        }
    }
}
