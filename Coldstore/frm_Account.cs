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
    public partial class frm_Account : Form
    {
        DataTable dtAcc;
        String tblName;
        public bool calledIndirect = false;
        public String AccName;
        public String AccType;
        string Gstr = "";
      
        public frm_Account()
        {
            InitializeComponent();
        }

     

        private void Button1_Click(object sender, EventArgs e)
        {
            
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


        private void frm_NewAcc_FormClosing(object sender, FormClosingEventArgs e)
        {

        }


        private void frm_NewAcc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {

                if (validate() == true)
                {
                    save();
                    if (calledIndirect == true)
                    {
                        this.Dispose();
                    }
                }
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

       
        public void LoadData(String str, String frmCaption)
        {
            Gstr = str;
           // Database.setFocus(textBox1);
            tblName = "tblAccount";
            dtAcc = new DataTable(tblName);
            Database.GetSqlData("select * from " + tblName + " where ac_id=" + int.Parse(str), dtAcc);

            
            this.Text = frmCaption;
            if (dtAcc.Rows.Count == 0)
            {
                dtAcc.Rows.Add(0);
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
            }
            else
            {
              
                textBox1.Text = dtAcc.Rows[0]["acc_name"].ToString();
                textBox2.Text = dtAcc.Rows[0]["address"].ToString();
                textBox3.Text = dtAcc.Rows[0]["address2"].ToString();
                textBox4.Text = dtAcc.Rows[0]["mobile_no"].ToString();
                textBox5.Text = dtAcc.Rows[0]["GST_No"].ToString();
            }
            textBox1.Select();
            
        }


        private void SideFill()
        {
            flowLayoutPanel1.Controls.Clear();
            DataTable dtsidefill = new DataTable();
            dtsidefill.Columns.Add("Name", typeof(string));
            dtsidefill.Columns.Add("DisplayName", typeof(string));
            dtsidefill.Columns.Add("ShortcutKey", typeof(string));
            dtsidefill.Columns.Add("Visible", typeof(bool));

            //save
            dtsidefill.Rows.Add();
            dtsidefill.Rows[dtsidefill.Rows.Count - 1]["Name"] = "save";
            dtsidefill.Rows[dtsidefill.Rows.Count - 1]["DisplayName"] = "Save";
            dtsidefill.Rows[dtsidefill.Rows.Count - 1]["ShortcutKey"] = "^S";
           
                dtsidefill.Rows[dtsidefill.Rows.Count - 1]["Visible"] = true;
            

            //close
            dtsidefill.Rows.Add();
            dtsidefill.Rows[dtsidefill.Rows.Count - 1]["Name"] = "quit";
            dtsidefill.Rows[dtsidefill.Rows.Count - 1]["DisplayName"] = "Quit";
            dtsidefill.Rows[dtsidefill.Rows.Count - 1]["ShortcutKey"] = "Esc";
            dtsidefill.Rows[dtsidefill.Rows.Count - 1]["Visible"] = true;

            for (int i = 0; i < dtsidefill.Rows.Count; i++)
            {
                if (bool.Parse(dtsidefill.Rows[i]["Visible"].ToString()) == true)
                {
                    Button btn = new Button();
                    btn.Size = new Size(150, 30);
                    btn.Name = dtsidefill.Rows[i]["Name"].ToString();
                    btn.Text = "";
                    Bitmap bmp = new Bitmap(btn.ClientRectangle.Width, btn.ClientRectangle.Height);
                    Graphics G = Graphics.FromImage(bmp);
                    G.Clear(btn.BackColor);
                    string line1 = dtsidefill.Rows[i]["ShortcutKey"].ToString();
                    string line2 = dtsidefill.Rows[i]["DisplayName"].ToString();
                    StringFormat SF = new StringFormat();
                    SF.Alignment = StringAlignment.Near;
                    SF.LineAlignment = StringAlignment.Center;
                    Rectangle RC = btn.ClientRectangle;
                    Font font = new Font("Arial", 12);
                    G.DrawString(line1, font, Brushes.Red, RC, SF);
                    G.DrawString("".PadLeft(line1.Length * 2 + 1) + line2, font, Brushes.Black, RC, SF);
                    btn.Image = bmp;
                    btn.Click += new EventHandler(button1_Click_1);
                    flowLayoutPanel1.Controls.Add(btn);
                }
            }
        }

        private void btn_click()
        {

        }


        private void save()
        {
           
            AccName = textBox1.Text;
           
            dtAcc.Rows[0]["acc_name"] = textBox1.Text;
            dtAcc.Rows[0]["Address"] = textBox2.Text;
            dtAcc.Rows[0]["Address2"] = textBox3.Text;
            dtAcc.Rows[0]["mobile_no"] = textBox4.Text;
            dtAcc.Rows[0]["GST_No"] = textBox5.Text;
          
            Database.SaveData(dtAcc);
            MessageBox.Show("Saved successfully");
            if (Gstr == "0")
            {
                LoadData("0", this.Text);
                textBox1.Focus();
            }
            else
            {

                this.Close();
                this.Dispose();
            }
          
            //this.Dispose();
        }

        private bool validate()
        {
            if (textBox1.Text.Trim() == "")
            {
                textBox1.BackColor = Color.Aqua;
                textBox1.Focus();
                return false;
            }

            if (funs11.Select_ac_id(textBox1.Text.Trim()) != 0 && funs11.Select_ac_id(textBox1.Text.Trim()) != int.Parse(Gstr))
            {
               
                MessageBox.Show("Account Name Already Exist");
                textBox1.Focus();
                return false;
            }
          
            return true;
        }


        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Button tbtn = (Button)sender;
            string name = tbtn.Name.ToString();

            if (name == "save")
            {
                if (validate() == true)
                {
                    save();
                    if (calledIndirect == true)
                    {
                        this.Dispose();
                    }
                }
            }
            else if (name == "quit")
            {
                this.Close();
                this.Dispose();
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frm_Account_Load(object sender, EventArgs e)
        {
            SideFill();
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

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
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

        private void textBox2_Enter(object sender, EventArgs e)
        {
            Database.setFocus(textBox2);
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            Database.lostFocus(textBox2);
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            Database.setFocus(textBox3);
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            Database.lostFocus(textBox3);
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            Database.setFocus(textBox4);
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            Database.lostFocus(textBox4);
        }

        private void frm_Account_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {

                if (validate() == true)
                {
                    save();
                    if (calledIndirect == true)
                    {
                        this.Dispose();
                    }
                }
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

        private void textBox5_Leave(object sender, EventArgs e)
        {
            Database.lostFocus(textBox5);
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{tab}");
            }
        }

        private void textBox5_Enter(object sender, EventArgs e)
        {
            Database.setFocus(textBox5);
        }
        
      
       
    }
}
