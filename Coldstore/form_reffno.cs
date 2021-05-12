
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
    public partial class form_reffno : Form
    {
        public string reportname = "";
        public form_reffno()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Report gg = new Report();
            if (textBox1.Text.Trim() != "")
            {

                if (reportname == "Referrence No Wise")
                {
                    gg.Reffnowise(Database.stDate, Database.enDate, textBox1.Text.Trim(), "Refference No wise Register");
                }
                else
                {
                    gg.ReffnoSummary(Database.stDate, Database.enDate, textBox1.Text.Trim(), "Refference No Summary");

                }
                gg.MdiParent = this.MdiParent;
                gg.Show();
            }
        }

        private void form_reffno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                this.Dispose();
            }

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            SelectCombo.IsEnter(this, e.KeyCode);
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            Database.setFocus(textBox1);
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            Database.lostFocus(textBox1);
        }


    }
}
