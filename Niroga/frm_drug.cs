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
    public partial class frm_drug : Form
    {
        SqlConnection sc = new SqlConnection("Data Source=MAHESHA-PC\\SQL2012;Initial Catalog=patient_management_db;Integrated Security=True");
        SqlCommand cmd;
        public frm_drug()
        {
            InitializeComponent();
        }

        private void frm_drug_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'patient_management_dbDataSet.drug_tbl' table. You can move, or remove it, as needed.
            this.drug_tblTableAdapter.Fill(this.patient_management_dbDataSet.drug_tbl);
            // TODO: This line of code loads data into the 'patient_management_dbDataSet7.drug_tbl' table. You can move, or remove it, as needed.
            // this.drug_tblTableAdapter2.Fill(this.patient_management_dbDataSet7.drug_tbl);
            // TODO: This line of code loads data into the 'patient_management_dbDataSet6.drug_tbl' table. You can move, or remove it, as needed.
            //  this.drug_tblTableAdapter1.Fill(this.patient_management_dbDataSet6.drug_tbl);

            sc.Open();
            SqlDataAdapter adapt = new SqlDataAdapter("select * from drug_tbl", sc);
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;

            SqlDataAdapter adapt1 = new SqlDataAdapter("select * from drug_tbl", sc);
            DataTable dt1 = new DataTable();
            adapt1.Fill(dt1);
            dataGridView2.DataSource = dt1;
            sc.Close();
        
           try
            {

                sc.Open();
                cmd = new SqlCommand("select drug_ID from drug_tbl", sc);
                cmd.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                sc.Close();
                if (ds.Tables[0].Rows.Count < 1)
                {
                    drug_IDTextBox.Text = "1";
                    bindingNavigatorPositionItem.Text = "1";

                }
                else
                {
                    sc.Open();
                    String myquery1 = "select max(drug_ID) from drug_tbl";
                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.CommandText = myquery1;
                    cmd1.Connection = sc;
                    SqlDataAdapter da1 = new SqlDataAdapter();
                    da1.SelectCommand = cmd1;
                    DataSet ds1 = new DataSet();
                    da1.Fill(ds1);
                    drug_IDTextBox.Text = ds1.Tables[0].Rows[0][0].ToString();
                    int a;
                    a = Convert.ToInt16(drug_IDTextBox.Text);
                    a = a + 1;
                    drug_IDTextBox.Text = a.ToString();
                    bindingNavigatorPositionItem.Text = a.ToString();
                    sc.Close();
                }


            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            dateDateTimePicker.Value = DateTime.Today;
            dateTimePicker2.Value = DateTime.Today;

            // TODO: This line of code loads data into the 'patient_management_dbDataSet2.drug_tbl' table. You can move, or remove it, as needed.
            //this.drug_tblTableAdapter.Fill(this.patient_management_dbDataSet2.drug_tbl);
            // TODO: This line of code loads data into the 'patient_management_dbDataSet1.visit_drug_tbl' table. You can move, or remove it, as needed.
           // this.visit_drug_tblTableAdapter.Fill(this.patient_management_dbDataSet1.visit_drug_tbl);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            frm_menu fm = new frm_menu();
            fm.Show();
            this.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
           if (txt_drugname.TextLength == 0 && txt_packSize.TextLength == 0 && txt_noofPack.TextLength == 0 && txt_freePack.TextLength == 0)
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
                        drug_IDTextBox.Text = "1";
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
                        drug_IDTextBox.Text = ds1.Tables[0].Rows[0][0].ToString();
                        int a;
                        a = Convert.ToInt16(drug_IDTextBox.Text);
                        a = a + 1;
                        drug_IDTextBox.Text = a.ToString();
                        bindingNavigatorPositionItem.Text = a.ToString();
                        sc.Close();
                    }


                }


                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


                dateDateTimePicker.Value = DateTime.Today;
                dateTimePicker2.Value = DateTime.Today;
                txt_drugname.Clear();
                txt_packSize.Clear();
                txt_noofPack.Clear();
                txt_freePack.Clear();
                txt_qty.Clear();
                txt_totCost.Clear();
                txt_totRetail.Clear();
                txt_unitCost.Clear();
                txt_uRPrice.Clear();


            }
            else
            {
                MessageBox.Show("You Not Save Current Drugs", "Not Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_calQty_Click(object sender, EventArgs e)
        {
            txt_qty.Text = ((Convert.ToInt32(txt_freePack.Text) + Convert.ToInt32(txt_noofPack.Text)) * Convert.ToInt32(txt_packSize.Text)).ToString();

        }

        private void btn_calUnitCost_Click(object sender, EventArgs e)
        {
            if (txt_qty.TextLength != 0)
            {
                txt_unitCost.Text = (Convert.ToDouble(txt_totCost.Text) / Convert.ToDouble(txt_qty.Text)).ToString();
            }
            else
            {
                MessageBox.Show("Calculate or enter qty", "Calculate Qty", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_calURPrice_Click(object sender, EventArgs e)
        {
            if (txt_qty.TextLength != 0)
            {
                txt_uRPrice.Text = (Convert.ToDouble(txt_totRetail.Text) / Convert.ToDouble(txt_packSize.Text)).ToString();
            }
            else
            {
                MessageBox.Show("Calculate or enter qty", "Calculate Qty", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void drug_tblBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {

           if (dateDateTimePicker.Value > DateTime.Today)
            {
                errorProvider1.SetError(dateDateTimePicker, "Select Valid Date");
            }
            else
            {
                errorProvider1.Clear();
            }
            if (txt_drugname.TextLength == 0)
            {
                errorProvider2.SetError(txt_drugname, "You must enter Drug name");

            }
            else
            {
                errorProvider2.Clear();
            }
            if (txt_packSize.TextLength == 0)
            {
                errorProvider3.SetError(txt_packSize, "You must enter Pack size");

            }
            else
            {
                errorProvider3.Clear();
            }
            if (txt_noofPack.TextLength == 0)
            {
                errorProvider4.SetError(txt_noofPack, "You must enter number of pack");

            }
            else
            {
                errorProvider4.Clear();
            }
            if (txt_freePack.TextLength == 0)
            {
                errorProvider5.SetError(txt_freePack, "You must enter number of free pack");

            }
            else
            {
                errorProvider5.Clear();
            }
            if (txt_qty.TextLength == 0)
            {
                errorProvider6.SetError(txt_qty, "You must calculate quantity");

            }
            else
            {
                errorProvider6.Clear();
            }
            if (txt_totCost.TextLength == 0)
            {
                errorProvider7.SetError(txt_totCost, "You must enter Total cost");

            }
            else
            {
                errorProvider7.Clear();
            }
            if (txt_totRetail.TextLength == 0)
            {
                errorProvider8.SetError(txt_totRetail, "You must enter Total retail");

            }
            else
            {
                errorProvider8.Clear();
            }
            if (txt_unitCost.TextLength == 0)
            {
                errorProvider9.SetError(txt_unitCost, "You must calculate unit cost");

            }
            else
            {
                errorProvider9.Clear();
            }
            if (txt_uRPrice.TextLength == 0)
            {
                errorProvider10.SetError(txt_uRPrice, "You must calculate unit retail price");

            }
            else
            {
                errorProvider10.Clear();
            }
            if (dateTimePicker2.Value == DateTime.Today || dateTimePicker2.Value < DateTime.Today)
            {
                errorProvider11.SetError(dateTimePicker2, "Select valid Day");
            }
            else
            {
                errorProvider11.Clear();
            }

            if (txt_drugname.TextLength != 0 && txt_packSize.TextLength != 0 && txt_noofPack.TextLength != 0 && txt_freePack.TextLength != 0 && txt_qty.TextLength != 0 && txt_totCost.TextLength != 0 && txt_totRetail.TextLength != 0 && txt_unitCost.TextLength != 0 && txt_uRPrice.TextLength != 0 && dateDateTimePicker.Value < DateTime.Today && dateTimePicker2.Value != DateTime.Today || dateTimePicker2.Value > DateTime.Today)
            {

                DialogResult dr = MessageBox.Show("Do you want to save your details?", " Warning !!! ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr.ToString() == "Yes")
                {
                    try
                    {
                        string last = dateDateTimePicker.Value.ToString();
                        string exp = dateTimePicker2.Value.ToString();
                        sc.Open();
                        cmd = new SqlCommand("insert into drug_tbl (drug_ID, last_date, drug_name, pack_size, no_of_packs, no_of_free_pack, qty, unit_cost, total_cost, unit_retail_price, total_retail, exp_date) values ('" + drug_IDTextBox.Text + "','" + last + "','" + txt_drugname.Text + "','" + txt_packSize.Text + "','" + txt_noofPack.Text + "','" + txt_freePack.Text + "','" + txt_qty.Text + "','" + txt_unitCost.Text + "','" + txt_totCost.Text + "','" + txt_uRPrice.Text + "','" + txt_totRetail.Text + "','"+exp+"') ", sc);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data save successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        sc.Close();
                        dataGridView1.Refresh();
                        dataGridView2.Refresh();

                        dateDateTimePicker.Value = DateTime.Today;
                        dateTimePicker2.Value = DateTime.Today;
                        txt_drugname.Clear();
                        txt_packSize.Clear();
                        txt_noofPack.Clear();
                        txt_freePack.Clear();
                        txt_qty.Clear();
                        txt_totCost.Clear();
                        txt_totRetail.Clear();
                        txt_unitCost.Clear();
                        txt_uRPrice.Clear();

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

           


            sc.Open();
            SqlDataAdapter adapt = new SqlDataAdapter("select * from drug_tbl", sc);
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;

            SqlDataAdapter adapt1 = new SqlDataAdapter("select * from drug_tbl", sc);
            DataTable dt1 = new DataTable();
            adapt1.Fill(dt1);
            dataGridView2.DataSource = dt1;
            sc.Close();

        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you need to delete this Drug?", "Delete Drug", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dr.ToString() == "Yes")
            {
                try
                {

                    sc.Open();
                    cmd = new SqlCommand("delete from drug_tbl where drug_ID ='" + drug_IDTextBox.Text + "'", sc);

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

                sc.Open();
                SqlDataAdapter adapt = new SqlDataAdapter("select * from drug_tbl", sc);
                DataTable dt = new DataTable();
                adapt.Fill(dt);
                dataGridView1.DataSource = dt;

                SqlDataAdapter adapt1 = new SqlDataAdapter("select * from drug_tbl", sc);
                DataTable dt1 = new DataTable();
                adapt1.Fill(dt1);
                dataGridView2.DataSource = dt1;
                sc.Close();

                dataGridView1.Refresh();
                dataGridView2.Refresh();

                dateDateTimePicker.Value = DateTime.Today;
                dateTimePicker2.Value = DateTime.Today;
                txt_drugname.Clear();
                txt_packSize.Clear();
                txt_noofPack.Clear();
                txt_freePack.Clear();
                txt_qty.Clear();
                txt_totCost.Clear();
                txt_totRetail.Clear();
                txt_unitCost.Clear();
                txt_uRPrice.Clear();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            
            if (drug_IDTextBox.TextLength == 0)
            {
                errorProvider12.SetError(drug_IDTextBox, "You must have Drug id");
            }
            else
            {
                errorProvider12.Clear();
            }

            if (dateDateTimePicker.Value > DateTime.Today)
            {
                errorProvider1.SetError(dateDateTimePicker, "Select a Date");
            }
            else
            {
                errorProvider1.Clear();
            }
            if (txt_drugname.TextLength == 0)
            {
                errorProvider2.SetError(txt_drugname, "You must enter Drug name");

            }
            else
            {
                errorProvider2.Clear();
            }
            if (txt_packSize.TextLength == 0)
            {
                errorProvider3.SetError(txt_packSize, "You must enter Pack size");

            }
            else
            {
                errorProvider3.Clear();
            }
            if (txt_noofPack.TextLength == 0)
            {
                errorProvider4.SetError(txt_noofPack, "You must enter number of pack");

            }
            else
            {
                errorProvider4.Clear();
            }
            if (txt_freePack.TextLength == 0)
            {
                errorProvider5.SetError(txt_freePack, "You must enter number of free pack");

            }
            else
            {
                errorProvider5.Clear();
            }
            if (txt_qty.TextLength == 0)
            {
                errorProvider6.SetError(txt_qty, "You must calculate quantity");

            }
            else
            {
                errorProvider6.Clear();
            }
            if (txt_totCost.TextLength == 0)
            {
                errorProvider7.SetError(txt_totCost, "You must enter Total cost");

            }
            else
            {
                errorProvider7.Clear();
            }
            if (txt_totRetail.TextLength == 0)
            {
                errorProvider8.SetError(txt_totRetail, "You must enter Total retail");

            }
            else
            {
                errorProvider8.Clear();
            }
            if (txt_unitCost.TextLength == 0)
            {
                errorProvider9.SetError(txt_unitCost, "You must calculate unit cost");

            }
            else
            {
                errorProvider9.Clear();
            }
            if (txt_uRPrice.TextLength == 0)
            {
                errorProvider10.SetError(txt_uRPrice, "You must calculate unit retail price");

            }
            else
            {
                errorProvider10.Clear();
            }
            if (dateTimePicker2.Value == DateTime.Today || dateTimePicker2.Value < DateTime.Today)
            {
                errorProvider11.SetError(dateTimePicker2, "Select valid Day");
            }
            else
            {
                errorProvider11.Clear();
            }

            if (drug_IDTextBox.TextLength != 0 && txt_drugname.TextLength != 0 && txt_packSize.TextLength != 0 && txt_noofPack.TextLength != 0 && txt_freePack.TextLength != 0 && txt_qty.TextLength != 0 && txt_totCost.TextLength != 0 && txt_totRetail.TextLength != 0 && txt_unitCost.TextLength != 0 && txt_uRPrice.TextLength != 0 && dateTimePicker2.Value != DateTime.Today)
            {

                DialogResult dr = MessageBox.Show("Do you want to update your details?", " Warning !!! ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr.ToString() == "Yes")
                {
                    try
                    {
                        string last = dateDateTimePicker.Value.ToString();
                        string exp = dateTimePicker2.Value.ToString();
                        sc.Open();
                        cmd = new SqlCommand("update drug_tbl set drug_ID = '" + drug_IDTextBox.Text + "', last_date = '" + last + "' , drug_name = '" + txt_drugname.Text + "' , pack_size = '" + txt_packSize.Text + "', no_of_packs = '" + txt_noofPack.Text + "', no_of_free_pack = '" + txt_freePack.Text + "', qty = '" + txt_qty.Text + "' , unit_cost = '" + txt_unitCost.Text + "', total_cost = '" + txt_totCost.Text + "', unit_retail_price = '" + txt_uRPrice.Text + "', total_retail = '" + txt_totRetail.Text + "', exp_date = '" + exp + "' where drug_ID = '" + drug_IDTextBox.Text + "' ", sc);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data Update successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        sc.Close();

                        dateDateTimePicker.Value = DateTime.Today;
                        dateTimePicker2.Value = DateTime.Today;
                        txt_drugname.Clear();
                        txt_packSize.Clear();
                        txt_noofPack.Clear();
                        txt_freePack.Clear();
                        txt_qty.Clear();
                        txt_totCost.Clear();
                        txt_totRetail.Clear();
                        txt_unitCost.Clear();
                        txt_uRPrice.Clear();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        sc.Close();
                    }

                    sc.Open();
                    SqlDataAdapter adapt = new SqlDataAdapter("select * from drug_tbl", sc);
                    DataTable dt = new DataTable();
                    adapt.Fill(dt);
                    dataGridView1.DataSource = dt;

                    SqlDataAdapter adapt1 = new SqlDataAdapter("select * from drug_tbl", sc);
                    DataTable dt1 = new DataTable();
                    adapt1.Fill(dt1);
                    dataGridView2.DataSource = dt1;
                    sc.Close();
                }
            }
            

            dataGridView1.Refresh();
            dataGridView2.Refresh();

        }

        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            sc.Open();
            SqlDataAdapter adapt = new SqlDataAdapter("select * from drug_tbl where drug_name like '" + toolStripTextBox1.Text + "%'", sc);
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            sc.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                drug_IDTextBox.Text = dataGridView1.CurrentRow.Cells["drug_ID"].Value.ToString();
                dateDateTimePicker.Text = dataGridView1.CurrentRow.Cells["last_date"].Value.ToString();
                txt_drugname.Text = dataGridView1.CurrentRow.Cells["drug_name"].Value.ToString();
                txt_packSize.Text = dataGridView1.CurrentRow.Cells["pack_size"].Value.ToString();
                txt_noofPack.Text = dataGridView1.CurrentRow.Cells["no_of_packs"].Value.ToString();
                txt_freePack.Text = dataGridView1.CurrentRow.Cells["no_of_free_pack"].Value.ToString();
                txt_qty.Text = dataGridView1.CurrentRow.Cells["qty"].Value.ToString();
                txt_totCost.Text = dataGridView1.CurrentRow.Cells["total_cost"].Value.ToString();
                txt_totRetail.Text = dataGridView1.CurrentRow.Cells["total_retail"].Value.ToString();
                txt_unitCost.Text = dataGridView1.CurrentRow.Cells["unit_cost"].Value.ToString();
                txt_uRPrice.Text = dataGridView1.CurrentRow.Cells["unit_retail_price"].Value.ToString();
                dateTimePicker2.Text = dataGridView1.CurrentRow.Cells["exp_date"].Value.ToString();

            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dateDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
           

            if (DateTime.Today >= dateDateTimePicker.Value)
            {
                

            }
            else
            {
                MessageBox.Show("You selected date is invalide", "Invalide Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dateDateTimePicker.Value = DateTime.Today;
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if(DateTime.Today <= dateTimePicker2.Value)
            {


            }
            else
            {
                MessageBox.Show("You selected date is invalide", "Invalide Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dateTimePicker2.Value = DateTime.Today;
            }
        }

        private void txt_packSize_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void txt_packSize_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txt_noofPack_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_noofPack_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txt_freePack_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txt_totCost_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txt_totRetail_KeyPress(object sender, KeyPressEventArgs e)
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
    }
}
