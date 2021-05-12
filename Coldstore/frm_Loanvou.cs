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
    public partial class frm_Loanvou : Form
    {
        string Gtype = "";
        string Gstr = "";
        public bool calledIndirect = false;
        int vtid = 0, vid = 0, vno = 0, item_id = 0,postfix=0;
        public String cmdnm;
        public String girvi;
        string vou_name;
        DataTable dtvou;
        DataTable dtvoudet;
        DataTable dtvoustock;
        string frmcap = "";
        DataTable dt;
        int gvidreff = 0;
        int gac_id = 0;


        public frm_Loanvou()
        {
            InitializeComponent();
            dateTimePicker1.CustomFormat = Database.dformat;
            dateTimePicker1.Value = Database.ldate;
            dateTimePicker1.MinDate = Database.stDate;
            dateTimePicker1.MaxDate = Database.enDate;

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
            dtsidefill.Rows[dtsidefill.Rows.Count - 1]["Name"] = "delete";
            dtsidefill.Rows[dtsidefill.Rows.Count - 1]["DisplayName"] = "Delete";
            dtsidefill.Rows[dtsidefill.Rows.Count - 1]["ShortcutKey"] = "^D";
            if (Gstr != "0")
            {
                dtsidefill.Rows[dtsidefill.Rows.Count - 1]["Visible"] = true;
            }
            else
            {
                dtsidefill.Rows[dtsidefill.Rows.Count - 1]["Visible"] = false;
            }
            //close
            dtsidefill.Rows.Add();
            dtsidefill.Rows[dtsidefill.Rows.Count - 1]["Name"] = "quit";
            dtsidefill.Rows[dtsidefill.Rows.Count - 1]["DisplayName"] = "Quit";
            dtsidefill.Rows[dtsidefill.Rows.Count - 1]["ShortcutKey"] = "Esc";
            dtsidefill.Rows[dtsidefill.Rows.Count - 1]["Visible"] = true;

            //Delete
            
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
                    btn.Click += new EventHandler(btn_Click);
                    flowLayoutPanel1.Controls.Add(btn);
                }
            }
        }

        void btn_Click(object sender, EventArgs e)
        {
            Button tbtn = (Button)sender;
            string name = tbtn.Name.ToString();

            if (name == "save")
            {
                if (validate() == true)
                {
                    try
                    {
                        Database.BeginTran();


                        save();
                        
                           
                       
                        Database.CommitTran();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        Database.RollbackTran();
                    }
                }
            }
            else if (name == "quit")
            {
                this.Close();
                this.Dispose();
            }
            else if (name == "delete")
            {
                Delete();

            }


        }

        private bool validate()
        {
            if (textBox3.Text == "")
            {
                textBox3.Select();
                return false;
            }
            if (textBox1.Text == "")
            {
                textBox1.Select();
                return false;
            }
            if (textBox2.Text == "")
            {
                textBox2.Select();
                return false;
            }
            if (textBox6.Text == "")
            {
                textBox6.Select();
                return false;
            }
            if (textBox4.Text == "")
            {
                textBox4.Select();
                return false;
            }
            if (textBox5.Text.Trim() == "")
            {
                textBox5.Text ="0";
                return false;
            }
            return true;
        }

        public void LoadData(string vouchertype, String str, String frmCaption)
        {

            frmcap = vouchertype;
            dateTimePicker1.Select();
            //if (vouchertype == "Inward")
            //{
               

            //}

           

            Gtype = vouchertype;
            Gstr = str;
            int v_id = int.Parse(str);
            vou_name = vouchertype;
            vtid = funs11.Select_vt_id(vou_name);
            dtvou = new DataTable("tblvoucherinfo");

            Database.GetSqlData("select * from tblvoucherinfo where vi_id=" + str, dtvou);
            dtvoudet = new DataTable("tblvoucherDet");

            if (int.Parse(str) != 0)
            {
                DisplayData();

            }
            vid = int.Parse(str);
            SetVno();
            this.Text = frmCaption;

           
            Database.setFocus(dateTimePicker1);
            dateTimePicker1.Select();

        }
        private void SetVno()
        {


            int numtype = 1;

            if (numtype == 1 && vno != 0 && vid != 0)
            {
                DateTime dt1 = dateTimePicker1.Value;
                DateTime dt2 = DateTime.Parse(Database.GetScalarDate("select vdate from tblvoucherinfo where vi_id=" + vid));


                if (dt1 != dt2)
                {
                    vno = funs11.GenerateVno(vtid, dateTimePicker1.Value.ToString("dd-MMM-yyyy"));
                    label2.Text = vno.ToString();
                }
                return;
            }
            if (vtid == 0 || (vno != 0 && vid != 0))
            {
                return;
            }

            vno = funs11.GenerateVno(vtid, dateTimePicker1.Value.ToString("dd-MMM-yyyy"));
            label2.Text = vno.ToString();

        }


        private void DisplayData()
        {
            dtvou = new DataTable("tblvoucherinfo");
            Database.GetSqlData("select * from tblvoucherinfo where vi_id=" + Gstr, dtvou);
            textBox3.Text = funs11.Select_ac_nm(int.Parse(dtvou.Rows[0]["Ac_id"].ToString()));
            vno = int.Parse(dtvou.Rows[0]["vnumber"].ToString());
            postfix = int.Parse(dtvou.Rows[0]["postfix"].ToString());
            label2.Text = dtvou.Rows[0]["vnumber"].ToString();
            int total = int.Parse(dtvou.Rows[0]["totqty"].ToString());
            if (vou_name == "LoanMemo")
            {
                textBox5.Text = total.ToString();
            }
            else
            {
                textBox5.Text = (-1* total).ToString();
            }
            dateTimePicker1.Value = DateTime.Parse(dtvou.Rows[0]["Vdate"].ToString());
            textBox1.Text = dtvou.Rows[0]["bankname"].ToString();
            textBox2.Text = dtvou.Rows[0]["branchname"].ToString();
            textBox4.Text = dtvou.Rows[0]["loanreffno"].ToString();

            textBox6.Text = funs11.Select_Item_nm(int.Parse(dtvou.Rows[0]["item_id"].ToString()));

        }



        private void save()
        {

           

            if (dtvou.Rows.Count == 0)
            {

                dtvou.Rows.Add();
            }

            dtvou.Rows[0]["Vt_id"] = vtid;
            dtvou.Rows[0]["gatepassno"] = "";
            dtvou.Rows[0]["bankname"] = textBox1.Text;
            dtvou.Rows[0]["branchname"] = textBox2.Text;
            dtvou.Rows[0]["loanreffno"] = textBox4.Text;
            dtvou.Rows[0]["postfix"] = postfix;
            dtvou.Rows[0]["Vnumber"] = label2.Text;
            dtvou.Rows[0]["ac_id"] = funs11.Select_ac_id(textBox3.Text);
            dtvou.Rows[0]["Item_id"] = funs11.Select_Item_id(textBox6.Text);
            dtvou.Rows[0]["Vdate"] = dateTimePicker1.Value.Date;
            if (vou_name == "LoanMemo")
            {
                dtvou.Rows[0]["Totqty"] = int.Parse(textBox5.Text);
            }
            else
            {
                dtvou.Rows[0]["Totqty"] = -1* int.Parse(textBox5.Text);
            }

            dtvou.Rows[0]["Isbilled"] = false;

            dtvou.Rows[0]["F_id"] = Database.F_id;
            Database.SaveData(dtvou);


           
            MessageBox.Show("saved successfully");
            if (Gstr == "0")
            {
                clear();
                LoadData(funs11.Select_vt_nm(vtid), "0", this.Text);
                
              
            }
            else
            {
                this.Close();
                this.Dispose();
            }

        }

        private void clear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "0";
            textBox6.Text = "";
            vno = 0;
            
        }
        private void Delete()
        {
            DialogResult res = MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.OKCancel);
            if (res == DialogResult.OK)
            {

                try
                {
                    Database.BeginTran();
                    Database.CommandExecutor("delete  from tblvoucherinfo where vi_id=" + vid);
                    Database.CommitTran();
                    MessageBox.Show("Deleted Successfully");
                    this.Close();
                    this.Dispose();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Database.RollbackTran();
                }
            }
        }
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || char.IsNumber(e.KeyChar) || e.KeyChar == ' ' || Convert.ToInt32(e.KeyChar) == 13)
            {
                DataTable dtAcc = new DataTable();
                dtAcc.Clear();

                String strCombo = "";
                
                strCombo = "select [Acc_name] from tblAccount order by acc_name";
               

                textBox3.Text = SelectCombo.ComboKeypress(this, e.KeyChar, strCombo, e.KeyChar.ToString(), 0);


            }

        }

        private void frm_Loanvou_Load(object sender, EventArgs e)
        {
            SideFill();
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            SelectCombo.IsEnter(this, e.KeyCode);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            SelectCombo.IsEnter(this, e.KeyCode);
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            SelectCombo.IsEnter(this, e.KeyCode);
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            SelectCombo.IsEnter(this, e.KeyCode);
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            SelectCombo.IsEnter(this, e.KeyCode);
        }

        private void dateTimePicker1_Leave(object sender, EventArgs e)
        {
            Database.lostFocus(dateTimePicker1);
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            Database.lostFocus(textBox3);
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            Database.lostFocus(textBox1);
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            Database.lostFocus(textBox2);
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            Database.lostFocus(textBox4);
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            Database.lostFocus(textBox5);
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            Database.lostFocus(textBox5);
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            Database.setFocus(textBox3);
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            Database.setFocus(textBox1);
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            Database.setFocus(textBox2);
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            Database.setFocus(textBox4);
        }

        private void textBox5_Enter(object sender, EventArgs e)
        {
            Database.setFocus(textBox5);
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{tab}");
            }
            if (e.Control && e.KeyCode == Keys.C)
            {
                textBox3.Text = funs11.AddAccount();
            }
            if (e.Control && e.KeyCode == Keys.A)
            {
                textBox3.Text = funs11.EditAccount(textBox3.Text);
            }
        }

        private void frm_Loanvou_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.D)
            {
                if (vid != 0)
                {
                    Delete();
                }
            }


            if (e.KeyCode == Keys.F2)
            {

                String strCombo;
                textBox1.ReadOnly = true;
                strCombo = "select distinct Bankname from tblvoucherinfo Where Bankname <>'' order by Bankname";

                textBox1.Text = SelectCombo.ComboKeypress(this, 'a', strCombo, "", 0);
               
                textBox1.ReadOnly = false;
                
            }



            if (e.Control && e.KeyCode == Keys.S)
            {

                if (validate() == true)
                {
                    if (validate() == true)
                    {
                        try
                        {
                            Database.BeginTran();
                            save();
                            Database.CommitTran();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                            Database.RollbackTran();
                        }
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

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            String strCombo;

            strCombo = "select distinct Item_name from tblItemInfo order by Item_name";

            textBox6.Text = SelectCombo.ComboKeypress(this, e.KeyChar, strCombo, e.KeyChar.ToString(), 0);
        }

        private void textBox6_Enter(object sender, EventArgs e)
        {
            Database.setFocus(textBox6);
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            Database.lostFocus(textBox6);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
    }
}
