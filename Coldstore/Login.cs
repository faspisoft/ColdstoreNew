using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Coldstore
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            dateTimePicker1.CustomFormat = Database.dformat;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Database.databaseName = "ColdStorage";
            Database.ldate = dateTimePicker1.Value.Date;

            if (textBox1.Text == "")
            {
                MessageBox.Show("Enter Username");
                textBox1.Focus();
                return;
            }
            if (textBox2.Text == "")
            {
                MessageBox.Show("Enter Password");
                textBox2.Focus();
                return;
            }

            if (textBox3.Text == "")
            {
                MessageBox.Show("Enter Financial Period");
                textBox3.Focus();
                return;
            }
            string str = "select * from tbllogininfo where user_name='" + textBox1.Text + "' and password='" + textBox2.Text + "' ";
            DataTable dtlogin = new System.Data.DataTable();
            Database.GetSqlData(str,dtlogin);
            if (Database.ldate < Database.stDate)
            {
                MessageBox.Show("Login Date Can't be less then Start Date");
                dateTimePicker1.Focus();
                return;
            }
            else if (Database.ldate > Database.enDate)
            {
                MessageBox.Show("Login Date Can't be greater then End Date");
                dateTimePicker1.Focus();
                return;
            }


            if (dtlogin.Rows.Count == 1)
            {
                str_main m = new str_main();
                Database.ldate = dateTimePicker1.Value;
                m.Show();
                Update();
                Postfix();
                this.Hide();
                Database.F_id = Database.GetScalarInt("Select id from Financialyear where Financial_period='"+textBox3.Text+"'");

            }
            else
            {
                MessageBox.Show("Name or Password Mismatch");
                Database.setFocus(textBox1);

            }
        }

        private void Update()
        {
            try
            {
                Database.BeginTran();
                if (Database.CommandExecutor("Alter table tblvoucherinfo Add Column bankname text(100)") == true)
                {
                    Database.CommandExecutor("Alter table tblvoucherinfo Add Column branchname text(100)");
                    Database.CommandExecutor("Alter table tblvoucherinfo Add Column loanreffno text(100)");
                    Database.CommandExecutor("insert into TblVouchertype (vname,startvno) values('LoanMemo',1)");
                    Database.CommandExecutor("insert into TblVouchertype (vname,startvno) values('LoanSettlement',1)");
                    
                    
                }
                 Database.CommitTran();
            }
            catch (Exception ex)
            {
                Database.RollbackTran();
            }
        }
        private void Postfix()
        {
            try
            {
                Database.BeginTran();
                if (Database.CommandExecutor("Alter table tblvoucherinfo Add Column Postfix number") == true)
                {
                    Database.CommandExecutor("Update tblvoucherinfo set Postfix=0");


                }
                Database.CommitTran();
            }
            catch (Exception ex)
            {
                Database.RollbackTran();
            }
        }
        private void Login_Load(object sender, EventArgs e)
        {
            textBox1.Select();
            dateTimePicker1.CustomFormat = Database.dformat;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || char.IsNumber(e.KeyChar) || e.KeyChar == ' ' || Convert.ToInt32(e.KeyChar) == 13)
            {
                DataTable dtAcc = new DataTable();
              //  Database.OpenConnection();
                
                String strCombo;

                strCombo = "select [Financial_Period] from Financialyear order by seq desc";

                textBox3.Text = SelectCombo.ComboKeypress(this, e.KeyChar, strCombo, e.KeyChar.ToString(), 0);
                
                if(textBox3.Text!="")
                {
                    DataTable dtlogin = new System.Data.DataTable();
                    Database.GetSqlData("Select * from Financialyear where Financial_Period='" + textBox3.Text + "'", dtlogin);
                    if (dtlogin.Rows.Count > 0)
                    {
                        Database.databaseName = "ColdStorage";
                        Database.stDate = DateTime.Parse(dtlogin.Rows[0]["stdate"].ToString());
                        Database.enDate = DateTime.Parse(dtlogin.Rows[0]["endate"].ToString());
                        Database.ldate = dateTimePicker1.Value.Date;
                    }

                 
                    Database.activated = Database.GetScalarBool("Select active from feature where features='activated'");

                }


              //  Database.CloseConnection();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{tab}");
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{tab}");
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{tab}");
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            Database.setFocus(textBox1);
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            Database.lostFocus(textBox1);
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            Database.lostFocus(textBox2);
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            Database.setFocus(textBox2);
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            Database.setFocus(textBox3);
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            Database.lostFocus(textBox3);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            funs11.IsEnter(this, e.KeyCode);
        }

        private void dateTimePicker1_Leave(object sender, EventArgs e)
        {
            Database.lostFocus(dateTimePicker1);
        }

        private void dateTimePicker1_Enter(object sender, EventArgs e)
        {
            Database.setFocus(dateTimePicker1);
        }
    }
}
