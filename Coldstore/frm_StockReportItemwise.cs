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
    public partial class frm_StockReportItemwise : Form
    {
        public frm_StockReportItemwise()
        {
            InitializeComponent();
            dateTimePicker1.CustomFormat = Database.dformat;
            dateTimePicker2.CustomFormat = Database.dformat;
            dateTimePicker1.Value = Database.stDate;
           
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

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || char.IsNumber(e.KeyChar) || e.KeyChar == ' ' || Convert.ToInt32(e.KeyChar) == 13)
            {
                DataTable dtAcc = new DataTable();
                dtAcc.Clear();
               
                String strCombo;

                strCombo = "select distinct [Acc_name] from tblAccount order by acc_name";

                textBox1.Text = SelectCombo.ComboKeypress(this, e.KeyChar, strCombo, e.KeyChar.ToString(), 0);


            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || char.IsNumber(e.KeyChar) || e.KeyChar == ' ' || Convert.ToInt32(e.KeyChar) == 13)
            {
                DataTable dtAcc = new DataTable();
                dtAcc.Clear();
               
                String strCombo;

                strCombo = "select distinct [Item_name] from tblItemInfo where Item_name<>'' order by item_name";

                textBox2.Text = SelectCombo.ComboKeypress(this, e.KeyChar, strCombo, e.KeyChar.ToString(), 0);


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Report gg = new Report();

            gg.MdiParent = this.MdiParent;

             gg.StockReportItemWise(dateTimePicker1.Value, dateTimePicker2.Value, textBox1.Text, textBox2.Text,"StockItemWise");
           
                gg.Show();
           
    
        }

        private void frm_StockReport_Load(object sender, EventArgs e)
        {
            dateTimePicker1.CustomFormat = Database.dformat;
            dateTimePicker2.CustomFormat = Database.dformat;
            dateTimePicker1.Value = Database.stDate;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();

        }

        private void frm_StockReport_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                Report gg = new Report();

                gg.MdiParent = this.MdiParent;

                gg.StockReportMarkaWise(dateTimePicker1.Value, dateTimePicker2.Value, textBox1.Text, textBox2.Text, "StockItemWise");
                
               gg.Show();
                
                
            }
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult chk = MessageBox.Show("Are u sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (chk == DialogResult.No)
                {
                    e.Handled = false;
                }
                else
                {
                    this.Dispose();
                }
            }
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            funs11.IsEnter(this, e.KeyCode);
        }

        private void dateTimePicker1_Enter(object sender, EventArgs e)
        {
           
            Database.setFocus(dateTimePicker1);
        }

        private void dateTimePicker1_Leave(object sender, EventArgs e)
        {
            Database.lostFocus(dateTimePicker1);
        }


        private void dateTimePicker2_KeyDown(object sender, KeyEventArgs e)
        {
            funs11.IsEnter(this, e.KeyCode);
        }

        private void dateTimePicker2_Enter(object sender, EventArgs e)
        {
            Database.setFocus(dateTimePicker2);
        }

        private void dateTimePicker2_Leave(object sender, EventArgs e)
        {
            Database.lostFocus(dateTimePicker2);

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            Database.lostFocus(textBox1);
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            Database.setFocus(textBox1);
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            Database.lostFocus(textBox2);
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            Database.setFocus(textBox2);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void frm_StockReportItemwise_Load(object sender, EventArgs e)
        {
            dateTimePicker1.CustomFormat = Database.dformat;
            dateTimePicker2.CustomFormat = Database.dformat;
            dateTimePicker1.Value = Database.stDate;
        }
    }
}
