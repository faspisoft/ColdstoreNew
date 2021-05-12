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
    public partial class fum_inoutreg : Form
    {
        public fum_inoutreg()
        {
            InitializeComponent();
            dateTimePicker1.CustomFormat = Database.dformat;
            dateTimePicker1.Value = Database.stDate;
            dateTimePicker2.CustomFormat = Database.dformat;
            dateTimePicker2.Value = Database.ldate;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Type",typeof(string));
            dt.Rows.Add();
            dt.Rows[0][0]="Inward";
            dt.Rows.Add();
            dt.Rows[1][0] = "Outward";
            dt.Rows.Add();
            dt.Rows[2][0] = "Both";

            textBox1.Text = SelectCombo.ComboDt(this, dt, 0);
            SelectCombo.IsEnter(this, e.KeyCode);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
           
                String strCombo = "";
               
                 strCombo = "select [Acc_name] from tblAccount order by acc_name";
                textBox2.Text = SelectCombo.ComboKeypress(this, e.KeyChar, strCombo, e.KeyChar.ToString(), 0);
            }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                Report gg = new Report();
                gg.DailyReg(dateTimePicker1.Value, dateTimePicker2.Value, textBox1.Text, textBox2.Text, "Daily Register");
                gg.MdiParent = this.MdiParent;
                gg.Show();
            }
            else
            {
                MessageBox.Show("Please Enter Report Type");
                textBox1.Focus();
            }



        }

        private void fum_inoutreg_Load(object sender, EventArgs e)
        {
            textBox1.Text = "Both";
        }

        private void dateTimePicker1_Enter(object sender, EventArgs e)
        {
            Database.setFocus(dateTimePicker1);
        }

        private void dateTimePicker1_Leave(object sender, EventArgs e)
        {
            Database.lostFocus(dateTimePicker1);
        }

        private void dateTimePicker2_Enter(object sender, EventArgs e)
        {
            Database.setFocus(dateTimePicker2);
        }

        private void dateTimePicker2_Leave(object sender, EventArgs e)
        {
            Database.lostFocus(dateTimePicker2);
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            SelectCombo.IsEnter(this, e.KeyCode);
        }

        private void dateTimePicker2_KeyDown(object sender, KeyEventArgs e)
        {
            SelectCombo.IsEnter(this, e.KeyCode);
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            Database.lostFocus(textBox1);
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            Database.lostFocus(textBox2);
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            Database.setFocus(textBox1);
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            Database.setFocus(textBox2);
        }
    }
}
