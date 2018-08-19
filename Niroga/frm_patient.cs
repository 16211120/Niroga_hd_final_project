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
    public partial class frm_patient : Form
    {
        SqlConnection sc = new SqlConnection("Data Source=Mahesha-PC\\SQL2012;Initial Catalog=patient_management_db;Integrated Security=True");
        SqlCommand cmd;
        public frm_patient()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frm_menu fm = new frm_menu();
            fm.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frm_visit fv = new frm_visit();
            fv.Show();
            this.Close();
        }
        private void dobDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void patient_tbBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            if (firstnameTextBox.TextLength == 0 && lastnameTextBox.TextLength == 0)
            {
                errorProvider1.SetError(firstnameTextBox, "You must enter patient name");
                errorProvider1.SetError(lastnameTextBox, "You must enter patient name");
            }
            else
            {
                errorProvider1.Clear();
            }

            if (titleComboBox.Text == "Select One")
            {
                errorProvider2.SetError(titleComboBox, "Select patient title");
            }
            else
            {
                errorProvider2.Clear();
            }

            if (dobDateTimePicker.Value == DateTime.Today)
            {
                errorProvider3.SetError(dobDateTimePicker, "Select Birthday");
            }
            else
            {
                errorProvider3.Clear();
            }

            if (genderComboBox.Text == "Select One")
            {
                errorProvider4.SetError(genderComboBox, "Select patient gender");
            }
            else
            {
                errorProvider4.Clear();
            }
            if (mobile_noTextBox.TextLength == 0 || mobile_noTextBox.TextLength != 10)
            {
                errorProvider5.SetError(mobile_noTextBox, "You must enter valid mobile number");

            }
            else
            {
                errorProvider5.Clear();
            }
            if (addressTextBox.TextLength == 0)
            {
                errorProvider6.SetError(addressTextBox, "You must enter address");

            }
            else
            {
                errorProvider6.Clear();
            }

            if (firstnameTextBox.TextLength != 0 && lastnameTextBox.TextLength != 0 && genderComboBox.Text != "Select One" && dobDateTimePicker.Value != DateTime.Today && titleComboBox.Text != "Select One" && mobile_noTextBox.TextLength != 0 && addressTextBox.TextLength != 0)
            {

                DialogResult dr = MessageBox.Show("Do you want to save your details?", " Warning !!! ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr.ToString() == "Yes")
                {
                    try
                    {
                        string title = Convert.ToString(titleComboBox.SelectedItem);
                        string gender = Convert.ToString(genderComboBox.SelectedItem);
                        string dob = dobDateTimePicker.Value.ToString();
                        sc.Open();
                        cmd = new SqlCommand("insert into patient_tb (patient_id, firstname, lastname, title, dob, age, address, mobile_no, past_history, allergies, gender) values ('" + patient_idTextBox.Text + "','" + firstnameTextBox.Text + "','" + lastnameTextBox.Text + "','" + title + "','" + dob + "','" + ageTextBox.Text + "','" + addressTextBox.Text + "','" + mobile_noTextBox.Text + "','" + past_historyTextBox.Text + "','" + allergiesTextBox.Text + "','" + gender + "') ", sc);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data save successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                dataGridView1.Refresh();
                patient_tbDataGridView1.Refresh();
            }
            sc.Open();
            SqlDataAdapter adapt = new SqlDataAdapter("select * from patient_tb", sc);
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            patient_tbDataGridView1.DataSource = dt;

            SqlDataAdapter adapt1 = new SqlDataAdapter("select * from patient_tb", sc);
            DataTable dt1 = new DataTable();
            adapt1.Fill(dt1);
            dataGridView1.DataSource = dt1;
            sc.Close();

        }

        private void frm_patient_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'patient_DataSet1.patient_tb' table. You can move, or remove it, as needed.
            this.patient_tbTableAdapter.Fill(this.patient_DataSet1.patient_tb);

            //load data to datagridview
            sc.Open();
            SqlDataAdapter adapt = new SqlDataAdapter("select * from patient_tb", sc);
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            patient_tbDataGridView1.DataSource = dt;

            SqlDataAdapter adapt1 = new SqlDataAdapter("select * from patient_tb", sc);
            DataTable dt1 = new DataTable();
            adapt1.Fill(dt1);
            dataGridView1.DataSource = dt1;
           

            sc.Close();


            // TODO: This line of code loads data into the 'patient_management_dbDataSet.patient_tb' table. You can move, or remove it, as needed.
          //  this.patient_tbTableAdapter.Fill(this.patient_management_dbDataSet.patient_tb);
            try
            {

                sc.Open();
                cmd = new SqlCommand("select patient_id from patient_tb", sc);
                cmd.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                sc.Close();
                if (ds.Tables[0].Rows.Count < 1)
                {
                    patient_idTextBox.Text = "1";
                    bindingNavigatorPositionItem.Text = "1";

                }
                else
                {
                    sc.Open();
                    String myquery1 = "select max(patient_id) from patient_tb";
                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.CommandText = myquery1;
                    cmd1.Connection = sc;
                    SqlDataAdapter da1 = new SqlDataAdapter();
                    da1.SelectCommand = cmd1;
                    DataSet ds1 = new DataSet();
                    da1.Fill(ds1);
                    patient_idTextBox.Text = ds1.Tables[0].Rows[0][0].ToString();
                    int a;
                    a = Convert.ToInt16(patient_idTextBox.Text);
                    a = a + 1;
                    patient_idTextBox.Text = a.ToString();
                    bindingNavigatorPositionItem.Text = a.ToString();
                    sc.Close();
                }


            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dobDateTimePicker.Value = DateTime.Today;
            titleComboBox.Text = "Select One";
            genderComboBox.Text = "Select One";
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (firstnameTextBox.TextLength != 0 && lastnameTextBox.TextLength != 0 && titleComboBox.Text != "Select One" && genderComboBox.Text != "Select One")
            {
                try
                {

                    sc.Open();
                    cmd = new SqlCommand("select patient_id from patient_tb", sc);
                    cmd.ExecuteNonQuery();

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    sc.Close();
                    if (ds.Tables[0].Rows.Count < 1)
                    {
                        patient_idTextBox.Text = "1";
                        bindingNavigatorPositionItem.Text = "1";

                    }
                    else
                    {
                        sc.Open();
                        String myquery1 = "select max(patient_id) from patient_tb";
                        SqlCommand cmd1 = new SqlCommand();
                        cmd1.CommandText = myquery1;
                        cmd1.Connection = sc;
                        SqlDataAdapter da1 = new SqlDataAdapter();
                        da1.SelectCommand = cmd1;
                        DataSet ds1 = new DataSet();
                        da1.Fill(ds1);
                        patient_idTextBox.Text = ds1.Tables[0].Rows[0][0].ToString();
                        int a;
                        a = Convert.ToInt16(patient_idTextBox.Text);
                        a = a + 1;
                        patient_idTextBox.Text = a.ToString();
                        bindingNavigatorPositionItem.Text = a.ToString();
                        sc.Close();
                    }


                }


                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                dobDateTimePicker.Value = DateTime.Today;
                titleComboBox.Text = "Select One";
                genderComboBox.Text = "Select One";
                firstnameTextBox.Clear();
                lastnameTextBox.Clear();
                addressTextBox.Clear();
                ageTextBox.Clear();
                allergiesTextBox.Clear();
                mobile_noTextBox.Clear();
                past_historyTextBox.Clear();

            }
            else
            {
                MessageBox.Show("You Not Save Current Patient", "Not Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dobDateTimePicker_ValueChanged_1(object sender, EventArgs e)
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you need to delete this patient?", "Delete Patient", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dr.ToString() == "Yes")
            {
                try
                {
                    
                    sc.Open();
                    cmd = new SqlCommand("delete from patient_tb where patient_id ='" + patient_idTextBox1.Text + "'", sc);

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

                dataGridView1.Refresh();
                patient_tbDataGridView1.Refresh();

                patient_idTextBox1.Clear();
                dobDateTimePicker.Value = DateTime.Today;
                titleComboBox.Text = "Select One";
                genderComboBox.Text = "Select One";
                firstnameTextBox.Clear();
                lastnameTextBox.Clear();
                addressTextBox.Clear();
                ageTextBox.Clear();
                allergiesTextBox.Clear();
                mobile_noTextBox.Clear();
                past_historyTextBox.Clear();
            }
            sc.Open();
            SqlDataAdapter adapt = new SqlDataAdapter("select * from patient_tb", sc);
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            patient_tbDataGridView1.DataSource = dt;

            SqlDataAdapter adapt1 = new SqlDataAdapter("select * from patient_tb", sc);
            DataTable dt1 = new DataTable();
            adapt1.Fill(dt1);
            dataGridView1.DataSource = dt1;
            sc.Close();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (patient_idTextBox1.TextLength == 0 )
            {
                errorProvider11.SetError(patient_idTextBox1, "Id is empty.!!!");
               
            }
            else
            {
                errorProvider11.Clear();
            }
            if (firstnameTextBox1.TextLength == 0 && lastnameTextBox1.TextLength == 0)
            {
                errorProvider1.SetError(firstnameTextBox1, "You must enter patient name");
                errorProvider1.SetError(lastnameTextBox1, "You must enter patient name");
            }
            else
            {
                errorProvider1.Clear();
            }

            if (titleComboBox1.Text == "Select One")
            {
                errorProvider2.SetError(titleComboBox1, "Select patient title");
            }
            else
            {
                errorProvider2.Clear();
            }

            if (dobDateTimePicker1.Value == DateTime.Today)
            {
                errorProvider3.SetError(dobDateTimePicker1, "Select Birthday");
            }
            else
            {
                errorProvider3.Clear();
            }

            if (genderComboBox1.Text == "Select One")
            {
                errorProvider4.SetError(genderComboBox1, "Select patient gender");
            }
            else
            {
                errorProvider4.Clear();
            }
            if (mobile_noTextBox1.TextLength == 0 || mobile_noTextBox1.TextLength > 10)
            {
                errorProvider5.SetError(mobile_noTextBox1, "You must enter valid mobile number");

            }
            else
            {
                errorProvider5.Clear();
            }
            if (addressTextBox1.TextLength == 0)
            {
                errorProvider6.SetError(addressTextBox1, "You must enter address");

            }
            else
            {
                errorProvider6.Clear();
            }
            if (past_historyTextBox1.TextLength == 0)
            {
                errorProvider7.SetError(past_historyTextBox1, "You must enter past history");

            }
            else
            {
                errorProvider7.Clear();
            }
            if (allergiesTextBox1.TextLength == 0)
            {
                errorProvider8.SetError(allergiesTextBox1, "You must enter allergies");

            }
            else
            {
                errorProvider8.Clear();
            }
            if (ageTextBox1.TextLength == 0)
            {
                errorProvider10.SetError(ageTextBox1, "You must enter age");

            }
            else
            {
                errorProvider10.Clear();
            }

            if (patient_idTextBox1.TextLength != 0 && firstnameTextBox1.TextLength != 0 && lastnameTextBox1.TextLength != 0 && genderComboBox1.Text != "Select One" && dobDateTimePicker1.Value != DateTime.Today && titleComboBox1.Text != "Select One" && mobile_noTextBox1.TextLength != 0 && addressTextBox1.TextLength != 0 && ageTextBox1.TextLength != 0)
            {

                DialogResult dr = MessageBox.Show("Do you want to Update your details?", " Warning !!! ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr.ToString() == "Yes")
                {
                    try
                    {
                        string title = Convert.ToString(titleComboBox1.SelectedItem);
                        string gender = Convert.ToString(genderComboBox1.SelectedItem);
                        string dob = dobDateTimePicker1.Value.ToString();
                        sc.Open();
                        cmd = new SqlCommand("update patient_tb set patient_id ='"+patient_idTextBox1.Text+"', firstname = '"+firstnameTextBox1.Text+"',lastname ='"+lastnameTextBox1.Text+"', title = '"+title+"', dob = '"+dob+"', age = '"+ageTextBox1.Text+"', address ='"+ addressTextBox1.Text+ "', mobile_no = '"+ mobile_noTextBox1.Text+ "', past_history ='"+ past_historyTextBox1.Text+ "', allergies = '"+ allergiesTextBox.Text + "', gender = '"+ gender + "' where patient_id ='" + patient_idTextBox1.Text + "' ", sc);
                                                                                                                                                                                                                                                                                                                                                                                                     
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data Update successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

                    dataGridView1.Refresh();
                    patient_tbDataGridView1.Refresh();

                    patient_idTextBox1.Clear();
                    dobDateTimePicker.Value = DateTime.Today;
                    titleComboBox.Text = "Select One";
                    genderComboBox.Text = "Select One";
                    firstnameTextBox.Clear();
                    lastnameTextBox.Clear();
                    addressTextBox.Clear();
                    ageTextBox.Clear();
                    allergiesTextBox.Clear();
                    mobile_noTextBox.Clear();
                    past_historyTextBox.Clear();
                }

                sc.Open();
                SqlDataAdapter adapt = new SqlDataAdapter("select * from drug_tbl", sc);
                DataTable dt = new DataTable();
                adapt.Fill(dt);
                dataGridView1.DataSource = dt;

                SqlDataAdapter adapt1 = new SqlDataAdapter("select * from drug_tbl", sc);
                DataTable dt1 = new DataTable();
                adapt1.Fill(dt1);
                patient_tbDataGridView1.DataSource = dt1;
                sc.Close();

            }

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
        }

        private void dobDateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            int y, m;

            if (DateTime.Today >= dobDateTimePicker1.Value)
            {
                if (DateTime.Today.Year == dobDateTimePicker1.Value.Year)
                {
                    y = 0;

                    m = (Convert.ToInt16(DateTime.Today.Month) - Convert.ToInt16(dobDateTimePicker1.Value.Month));

                    y.ToString();
                    m.ToString();

                    ageTextBox1.Text = (y + "Years " + m + "Months");
                }
                else if (DateTime.Today.Month > dobDateTimePicker1.Value.Month)
                {
                    y = (Convert.ToInt16(DateTime.Today.Year) - Convert.ToInt16(dobDateTimePicker1.Value.Year));

                    m = (Convert.ToInt16(DateTime.Today.Month) - Convert.ToInt16(dobDateTimePicker1.Value.Month));

                    y.ToString();
                    m.ToString();

                    ageTextBox1.Text = (y + "Years " + m + "Months");
                }
                else if (DateTime.Today.Month < dobDateTimePicker1.Value.Month)
                {
                    y = (Convert.ToInt16(DateTime.Today.Year) - Convert.ToInt16(dobDateTimePicker1.Value.Year) - 1);

                    m = 12 - Convert.ToInt16(dobDateTimePicker1.Value.Month);
                    m = m + Convert.ToInt16(DateTime.Today.Month);

                    y.ToString();
                    m.ToString();

                    ageTextBox1.Text = (y + "Years " + m + "Months");

                }

            }
            else
            {
                MessageBox.Show("You selected birthday is invalide", "Invalide Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dobDateTimePicker.Value = DateTime.Today;
            }
        }

        private void patient_tbDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void toolStripTextBox2_Click(object sender, EventArgs e)
        {

        }
        private void toolStripTextBox2_TextChanged(object sender, EventArgs e)
        {
            
            

        }

        private void toolStripTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void bindingNavigator1_RefreshItems(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorPositionItem_Click(object sender, EventArgs e)
        {

        }

        private void bindingNavigator1_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void toolStripTextBox2_KeyUp(object sender, KeyEventArgs e)
        {
            sc.Open();
            SqlDataAdapter adapt = new SqlDataAdapter("select * from patient_tb where firstname like '" + toolStripTextBox2.Text + "%'", sc);
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            patient_tbDataGridView1.DataSource = dt;
            sc.Close();
        }

        private void mobile_noTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void mobile_noTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
           

            if (!Char.IsDigit(e.KeyChar) && (e.KeyChar != (char)(Keys.Back)))
            {
                e.Handled = true;
            }
            else
            {
                // is a digit or backspace - ignore digits if length is alreay 10 - allow backspace
                if (Char.IsDigit(e.KeyChar))
                {
                    if (mobile_noTextBox.Text.Length > 9)
                    {
                        e.Handled = true;
                    }
                }
            }
        }

        private void patient_tbDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                patient_idTextBox1.Text = patient_tbDataGridView1.CurrentRow.Cells["dataGridViewTextBoxColumn12"].Value.ToString();
                firstnameTextBox1.Text = patient_tbDataGridView1.CurrentRow.Cells["dataGridViewTextBoxColumn13"].Value.ToString();
                lastnameTextBox1.Text = patient_tbDataGridView1.CurrentRow.Cells["dataGridViewTextBoxColumn14"].Value.ToString();
                titleComboBox1.Text = patient_tbDataGridView1.CurrentRow.Cells["dataGridViewTextBoxColumn15"].Value.ToString();
                genderComboBox1.Text = patient_tbDataGridView1.CurrentRow.Cells["dataGridViewTextBoxColumn22"].Value.ToString();
                dobDateTimePicker1.Text = patient_tbDataGridView1.CurrentRow.Cells["dataGridViewTextBoxColumn16"].Value.ToString();
                ageTextBox1.Text = patient_tbDataGridView1.CurrentRow.Cells["dataGridViewTextBoxColumn17"].Value.ToString();
                addressTextBox1.Text = patient_tbDataGridView1.CurrentRow.Cells["dataGridViewTextBoxColumn18"].Value.ToString();
                mobile_noTextBox1.Text = patient_tbDataGridView1.CurrentRow.Cells["dataGridViewTextBoxColumn19"].Value.ToString();
                past_historyTextBox1.Text = patient_tbDataGridView1.CurrentRow.Cells["dataGridViewTextBoxColumn20"].Value.ToString();
                allergiesTextBox1.Text = patient_tbDataGridView1.CurrentRow.Cells["dataGridViewTextBoxColumn21"].Value.ToString();


            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void mobile_noTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && (e.KeyChar != (char)(Keys.Back)))
            {
                e.Handled = true;
            }
            else
            {
                // is a digit or backspace - ignore digits if length is alreay 10 - allow backspace
                if (Char.IsDigit(e.KeyChar))
                {
                    if (mobile_noTextBox1.Text.Length > 9)
                    {
                        e.Handled = true;
                    }
                }
            }
        }
    }
}
