using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Coldstore
{
    public partial class frmbackup : Form
    {
        DataTable dtFirmBackup;
        public String frmMenuTyp;
        public bool ret;

        public frmbackup()
        {
            InitializeComponent();
        }

        private void frmbackup_Load(object sender, EventArgs e)
        {
            
            dtFirmBackup = new DataTable("financialyear");
            Database.GetSqlData("select id,Financial_period as  Financial_period from financialyear order by seq desc", dtFirmBackup);
            ansGridView1.DataSource = dtFirmBackup;
            ansGridView1.Columns["Financial_period"].Width = 340;
            ansGridView1.Columns["id"].Visible = false;

            foreach (DataGridViewColumn column in ansGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            if (frmMenuTyp == "Backup")
            {
                Button1.Text = "Backup";
                groupBox3.Visible = false;
            }
           
         
            else if (frmMenuTyp == "Use")
            {
                Button1.Text = "Ok";
                groupBox3.Visible = true;
            }
           

            dateTimePicker1.CustomFormat = Database.dformat;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            
            DataTable dtDbName= new DataTable("firminfo");
            
            //Database.GetOtherSqlData("select * from financialyear where id=" + dtFirmBackup.Rows[ansGridView1.SelectedCells[0].RowIndex]["id"], dtDbName);
            
            if (frmMenuTyp == "Use")
            {
                if (Validate() == true)
                {


                    Database.databaseName = "ColdStorage";
                   
                

                    Database.F_id = Database.GetScalarInt("Select id from Financialyear where id=" + dtFirmBackup.Rows[ansGridView1.SelectedCells[0].RowIndex]["id"]);
                    DataTable dtdate = new DataTable();

                    Database.GetSqlData("select * from financialyear where id=" + dtFirmBackup.Rows[ansGridView1.SelectedCells[0].RowIndex]["id"], dtdate);
                   
                    Database.stDate = DateTime.Parse(dtdate.Rows[0]["stdate"].ToString());
                    Database.enDate = DateTime.Parse(dtdate.Rows[0]["endate"].ToString());
            
                    //String strCmd = "SELECT FIRMINFO.Firm_name, USERINFO.UName, USERINFO.Utype, FIRMINFO.Firm_database, FIRMINFO.Firm_Period_name,Firm_odate,Firm_edate FROM (USERWCOMPANY INNER JOIN FIRMINFO ON USERWCOMPANY.F_id=FIRMINFO.F_id) INNER JOIN USERINFO ON USERWCOMPANY.U_id=USERINFO.U_id WHERE FIRMINFO.f_id=" + dtFirmBackup.Rows[ansGridView1.SelectedCells[0].RowIndex]["f_id"] + " and USERINFO.UName='" + Database.uname + "'";
                    //DataTable dtInfo = new DataTable();
                    //Database.GetOtherSqlData(strCmd, dtInfo);
                    Database.ldate = dateTimePicker1.Value;
                   // Database.fyear=
                    //Database.setVariable(dtInfo.Rows[0]["Firm_name"].ToString(), dtInfo.Rows[0]["Firm_Period_name"].ToString(), dtInfo.Rows[0]["UName"].ToString(), dtInfo.Rows[0]["Utype"].ToString(), dtInfo.Rows[0]["Firm_database"].ToString(), DateTime.Parse(dtInfo.Rows[0]["Firm_odate"].ToString()), DateTime.Parse(dtInfo.Rows[0]["Firm_edate"].ToString()));
                    ret = true;
                    this.Close();
                }
            }

            
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            ret = false;
            this.Close();
        }

        private void frmbackup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2 )
            {
                Button1_Click(sender, e);
            }
            
        }

        private void frmbackup_FormClosing(object sender, FormClosingEventArgs e)
        {
            //ret = false;
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            SelectCombo.IsEnter(this, e.KeyCode);
        }

        private void ansGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
            
            
        }

        private void ansGridView1_DoubleClick(object sender, EventArgs e)
        {


        }

        private void ansGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Button1_Click(sender, e);
        }

        private void ansGridView1_Enter(object sender, EventArgs e)
        {
            

        }

        private void ansGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataTable dtdate = new DataTable();

            Database.GetSqlData("select * from financialyear where id=" + dtFirmBackup.Rows[e.RowIndex]["id"], dtdate);
            DateTime dtfrom = DateTime.Parse(dtdate.Rows[0]["stdate"].ToString());
            DateTime dtto = DateTime.Parse(dtdate.Rows[0]["endate"].ToString());
            if (Database.ldate >= dtfrom && Database.ldate <= dtto)
            {
                dateTimePicker1.Value = Database.ldate;
            }
            else if(dtto >= DateTime.Today)
            {
                dateTimePicker1.Value = DateTime.Today;
            }
            else
            {
                dateTimePicker1.Value = dtto;
            }
        }


        private bool Validate()
        {
            DataTable dtdate = new DataTable();

            Database.GetSqlData("select * from financialyear where id=" + dtFirmBackup.Rows[ansGridView1.SelectedCells[0].RowIndex]["id"], dtdate);
            DateTime dtfrom = DateTime.Parse(dtdate.Rows[0]["stdate"].ToString());
            DateTime dtto = DateTime.Parse(dtdate.Rows[0]["endate"].ToString());
            if (dateTimePicker1.Value < dtfrom)
            {
                return false;
            }
            if (dateTimePicker1.Value > dtto)
            {
                return false;
            }

            return true;
        }
        
    }
}
