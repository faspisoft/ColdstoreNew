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
    public partial class frm_Item : Form
    {
        public frm_Item()
        {
            InitializeComponent();
        }
        DataTable dtAcc;
        String tblName;
        public bool calledIndirect = false;
        public String itemName;
        public String AccType;
        string Gstr = "";

      

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


      

       
        public void LoadData(String str, String frmCaption)
        {
            Gstr = str;
            tblName = "tblItemInfo";
            dtAcc = new DataTable(tblName);
            Database.GetSqlData("select * from "+ tblName +"  where item_id=" + int.Parse(str), dtAcc);

            
            this.Text = frmCaption;
            if (dtAcc.Rows.Count == 0)
            {
                dtAcc.Rows.Add(0);
                textBox1.Text = "";
               
               
            }
            else
            {
                //funs1 fObj = new funs1();
                textBox1.Text = dtAcc.Rows[0]["Item_name"].ToString();
               
            }
            
        }


       
        

       


        private void save()
        {
           
            itemName = textBox1.Text;
           
            dtAcc.Rows[0]["item_name"] = textBox1.Text;
            
          
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
            if (textBox1.Text == "")
            {
                textBox1.BackColor = Color.Aqua;
                textBox1.Focus();
                return false;
            }
            if (funs11.Select_Item_id(textBox1.Text) != 0 && funs11.Select_Item_id(textBox1.Text) != int.Parse(Gstr))
            {

                MessageBox.Show("Item Name Already Exist");
                textBox1.Focus();
                return false;
            }
            
            return true;
        }

        private int funs1(string p)
        {
            throw new NotImplementedException();
        }

        


        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{tab}");
            }
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
                    btn.Click += new EventHandler(btnsave_Click);
                    flowLayoutPanel1.Controls.Add(btn);
                }
            }
        }
       


       
        private void frm_Item_Load(object sender, EventArgs e)
        {
            SideFill();
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void btnsave_Click(object sender, EventArgs e)
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

        private void frm_Item_KeyDown(object sender, KeyEventArgs e)
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

        private void textBox1_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{tab}");
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            Database.lostFocus(textBox1);
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            Database.setFocus(textBox1);
        }

        }

        
      
    }

