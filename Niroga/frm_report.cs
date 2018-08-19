using Microsoft.Reporting.WinForms;
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
    public partial class frm_report : Form
    {
        Niroga.DataSet1.incomeDataTable inc = new DataSet1.incomeDataTable();
        Niroga.DataSet1.patientDataTable pat = new DataSet1.patientDataTable();
        Niroga.DataSet1.drugDataTable dr = new DataSet1.drugDataTable();
        Niroga.DataSet1.visit_drugDataTable vdr = new DataSet1.visit_drugDataTable();
        Niroga.DataSet1.visit_patient_tblDataTable vpt = new DataSet1.visit_patient_tblDataTable();


        SqlConnection sc = new SqlConnection("Data Source=Mahesha-PC\\SQL2012;Initial Catalog=patient_management_db;Integrated Security=True");
        SqlCommand cmd;
        public frm_report()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frm_menu fm = new frm_menu();
            fm.Show();
            this.Close();
           
        }

        private void frm_report_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DataSet1.visit_patient_tbl' table. You can move, or remove it, as needed.
            this.visit_patient_tblTableAdapter.Fill(this.DataSet1.visit_patient_tbl);
            // TODO: This line of code loads data into the 'patient_DataSet1.patient_tb' table. You can move, or remove it, as needed.
            this.patient_tbTableAdapter.Fill(this.patient_DataSet1.patient_tb);
            getData();
            getDataPatient();
            getDataDrug();
            getVisitDataDrug();
            getVisitDataPatient();


            incomeBindingSource.DataSource = inc;
            patient_tbBindingSource.DataSource = pat;
            drugBindingSource.DataSource = dr;
            visit_drugBindingSource.DataSource = vdr;
            visit_patient_tblBindingSource.DataSource = vpt;

            this.reportViewer1.RefreshReport();
            this.reportViewer2.RefreshReport();
            this.reportViewer3.RefreshReport();
            this.reportViewer4.RefreshReport();
            this.reportViewer5.RefreshReport();
           
        }
        private void getData()
        {
            sc.Open();
            SqlDataAdapter sda = new SqlDataAdapter("  select * from visit_income_tbl ", sc);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow r in dt.Rows)
            {
                inc.AddincomeRow(r.Field<int>("visit_id"), r.Field<decimal>("chanel_fee"), r.Field<decimal>("drug_cost"), r.Field<decimal>("total"), r.Field<string>("income_date"));
            }
            sc.Close();
        }
        private void getDataPatient()
        {
            sc.Open();
            SqlDataAdapter sda = new SqlDataAdapter("  select * from patient_tb ", sc);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow r in dt.Rows)
            {
                pat.AddpatientRow(r.Field<int>("patient_id"), r.Field<string>("firstname"), r.Field<string>("lastname"), r.Field<string>("title"), r.Field<string>("dob"), r.Field<string>("age"), r.Field<string>("address"), r.Field<int>("mobile_no"), r.Field<string>("past_history"), r.Field<string>("allergies"), r.Field<string>("gender"));

            }
            sc.Close();
        }
        private void getDataDrug()
        {
            sc.Open();
            SqlDataAdapter sda = new SqlDataAdapter("  select * from drug_tbl ", sc);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow r in dt.Rows)
            {
                dr.AdddrugRow(r.Field<int>("drug_ID"), r.Field<string>("last_date"), r.Field<string>("drug_name"), r.Field<int>("pack_size"), r.Field<int>("no_of_packs"), r.Field<int>("no_of_free_pack"), r.Field<int>("qty"), r.Field<decimal>("unit_cost"), r.Field<decimal>("total_cost"), r.Field<decimal>("unit_retail_price"), r.Field<decimal>("total_retail"), r.Field<string>("exp_date"));
            }
            sc.Close();
        }
        private void getVisitDataDrug()
        {
             sc.Open();
             SqlDataAdapter sda = new SqlDataAdapter("  select * from visit_drug_tbl ", sc);
             DataTable dt = new DataTable();
             sda.Fill(dt);
             foreach (DataRow r in dt.Rows)
             {
                 vdr.Addvisit_drugRow(r.Field<int>("visit_id"), r.Field<string>("drug_name"), r.Field<string>("dose"), r.Field<string>("freq"), r.Field<string>("duration"), r.Field<int>("qty"), r.Field<string>("visitdrug_date"));
             }
             sc.Close();

        }
        private void getVisitDataPatient()
        {
            sc.Open();
            SqlDataAdapter sda = new SqlDataAdapter("  select * from visit_patient_tbl ", sc);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow r in dt.Rows)
            {
                vpt.Addvisit_patient_tblRow(r.Field<int>("id"), r.Field<string>("patient_firstname"), r.Field<string>("age"), r.Field<string>("sys_date"), r.Field<string>("sys_time"), r.Field<string>("complain"));
            }
            sc.Close();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void reportViewer2_Load(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void reportViewer3_Load(object sender, EventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void reportViewer4_Load(object sender, EventArgs e)
        {

        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void reportViewer5_Load(object sender, EventArgs e)
        {

        }

        private void incomeBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void patient_tbBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        
        private void button2_Click(object sender, EventArgs e)
        {
            inc.Clear();
            sc.Open();
            DateTime i;
            for (i = dateTimePicker1.Value.Date; i <= dateTimePicker2.Value.Date; i=i.AddDays(1))
            {
                SqlDataAdapter sda = new SqlDataAdapter("  select * from visit_income_tbl where income_date ='"+ i+"' ", sc);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                foreach (DataRow r in dt.Rows)
                {
                    inc.AddincomeRow(r.Field<int>("visit_id"), r.Field<decimal>("chanel_fee"), r.Field<decimal>("drug_cost"), r.Field<decimal>("total"), r.Field<string>("income_date"));
                }

            }
            sc.Close();
            reportViewer5.RefreshReport();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            vdr.Clear();
            sc.Open();
            DateTime i;
            for (i = dateTimePicker4.Value.Date; i <= dateTimePicker3.Value.Date; i = i.AddDays(1))
            {
                SqlDataAdapter sda = new SqlDataAdapter("  select * from visit_drug_tbl where visitdrug_date ='" + i + "' ", sc);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                foreach (DataRow r in dt.Rows)
                {
                    vdr.Addvisit_drugRow(r.Field<int>("visit_id"), r.Field<string>("drug_name"), r.Field<string>("dose"), r.Field<string>("freq"), r.Field<string>("duration"), r.Field<int>("qty"), r.Field<string>("visitdrug_date"));
                }

            }
            sc.Close();
            reportViewer4.RefreshReport();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            vpt.Clear();
            sc.Open();
            DateTime i;
            for (i = dateTimePicker6.Value.Date; i <= dateTimePicker5.Value.Date; i = i.AddDays(1))
            {
                SqlDataAdapter sda = new SqlDataAdapter("  select * from visit_patient_tbl where sys_date ='" + i + "' ", sc);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                foreach (DataRow r in dt.Rows)
                {
                    vpt.Addvisit_patient_tblRow(r.Field<int>("id"), r.Field<string>("patient_firstname"), r.Field<string>("age"), r.Field<string>("sys_date"), r.Field<string>("sys_time"), r.Field<string>("complain"));
                }

            }
            sc.Close();
            reportViewer3.RefreshReport();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dr.Clear();
            sc.Open();
            DateTime i;
            for (i = dateTimePicker8.Value.Date; i <= dateTimePicker7.Value.Date; i = i.AddDays(1))
            {
                SqlDataAdapter sda = new SqlDataAdapter("  select * from drug_tbl where exp_date ='" + i + "' ", sc);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                foreach (DataRow r in dt.Rows)
                {
                    dr.AdddrugRow(r.Field<int>("drug_ID"), r.Field<string>("last_date"), r.Field<string>("drug_name"), r.Field<int>("pack_size"), r.Field<int>("no_of_packs"), r.Field<int>("no_of_free_pack"), r.Field<int>("qty"), r.Field<decimal>("unit_cost"), r.Field<decimal>("total_cost"), r.Field<decimal>("unit_retail_price"), r.Field<decimal>("total_retail"), r.Field<string>("exp_date"));
                }

            }
            sc.Close();
            reportViewer2.RefreshReport();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            pat.Clear();
            sc.Open();
            DateTime i;
            for (i = dateTimePicker10.Value.Date; i <= dateTimePicker9.Value.Date; i = i.AddDays(1))
            {
                SqlDataAdapter sda = new SqlDataAdapter("  select * from patient_tb where dob ='" + i + "' ", sc);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                foreach (DataRow r in dt.Rows)
                {
                    pat.AddpatientRow(r.Field<int>("patient_id"), r.Field<string>("firstname"), r.Field<string>("lastname"), r.Field<string>("title"), r.Field<string>("dob"), r.Field<string>("age"), r.Field<string>("address"), r.Field<int>("mobile_no"), r.Field<string>("past_history"), r.Field<string>("allergies"), r.Field<string>("gender"));
                }

            }
            sc.Close();
            reportViewer1.RefreshReport();
        }
    }
}
