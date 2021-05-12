using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OleDb;


namespace Coldstore
{
    public partial class frm_voucher : Form
    {
        public frm_voucher()
        {
            InitializeComponent();
            dateTimePicker1.CustomFormat = Database.dformat;
            dateTimePicker1.Value = Database.ldate;
        }
       
        string Gstr = "";
        public bool calledIndirect = false;
        OleDbCommand cmd;
        OleDbDataAdapter da;
        int vtid = 0, vid = 0, vno = 0,item_id=0,postfix=0;
        public String cmdnm;
        public String girvi;
        string vou_name;
        DataTable dtvou;
        DataTable dtvoudet;
        DataTable dtvoustock;
        string gtype = "";
        DataTable dt;
        int gvidreff = 0;
        int gac_id = 0;



    

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadData(string type, String str, String frmCaption)
        {
            
            gtype = type;
            dateTimePicker1.Select();
            ansGridView1.Columns["roomno"].Visible = false;
            ansGridView1.Columns["slapno"].Visible = false;
            ansGridView1.Columns["section"].Visible = false;
            ansGridView1.Columns["remark"].Visible = false;
            if (type == "Inward")
            {
                dateTimePicker1.Value = Database.ldate;
                dateTimePicker1.MinDate = Database.stDate;
                dateTimePicker1.MaxDate = Database.enDate;
              //  Database.setFocus(dateTimePicker1);
               
                ansGridView1.Columns["stock"].Visible = false;
                
               
            }

            if (type == "OpeningStock")
            {
                dateTimePicker1.Value = Database.stDate.AddDays(-1);
               
                dateTimePicker1.Enabled=false;
                ansGridView1.Columns["stock"].Visible = false;  
                textBox3.Select();
            }
            if (type == "Outward")
            {
                ansGridView1.Columns["stock"].Visible = false;
                dateTimePicker1.Value = Database.ldate;
                dateTimePicker1.MinDate = Database.stDate;
                dateTimePicker1.MaxDate = Database.enDate;
                checkBox1.Visible = false;


                ansGridView1.Columns["Sno"].DisplayIndex = 0;
                ansGridView1.Columns["Itemname"].DisplayIndex = 1;
                ansGridView1.Columns["Marka"].DisplayIndex = 2;
                ansGridView1.Columns["roomno"].DisplayIndex = 3;
                ansGridView1.Columns["slapno"].DisplayIndex = 4;
                ansGridView1.Columns["section"].DisplayIndex = 5;
                ansGridView1.Columns["remark"].DisplayIndex = 6;
                ansGridView1.Columns["Quantity"].DisplayIndex = 7;
                ansGridView1.Columns["Stock"].DisplayIndex = 8;
                ansGridView1.Columns["section"].Visible = true;
                //ansGridView1.Columns["Stock"].Visible = true;
                ansGridView1.Columns["Marka"].ReadOnly = true;
                ansGridView1.Columns["roomno"].ReadOnly = false;
                ansGridView1.Columns["slapno"].ReadOnly = false;
                ansGridView1.Columns["section"].ReadOnly = false;
                ansGridView1.Columns["remark"].ReadOnly = false;
            }
            
         
            Gstr = str;
            int v_id = int.Parse(str);

            //vou_name =   type;
            //vtid = funs11.Select_vt_id(vou_name);
            dtvou = new DataTable("tblvoucherinfo");
           
            Database.GetSqlData("select * from tblvoucherinfo where vi_id="+ str , dtvou);
            dtvoudet = new DataTable("tblvoucherDet");
           
            Database.GetSqlData("select * from tblvoucherDet where vi_id=" + str, dtvoudet);
            dtvoustock = new DataTable("tblStock");

            Database.GetSqlData("select * from tblStock where vid=" + str, dtvoustock);
            vno = 0;
            DisplaySetting();
            vid = int.Parse(str);

            DisplayData(vid);

            
          
            
            SetVno();
            this.Text = frmCaption;

            if (frmCaption == "OpeningStock")
            {
                Database.setFocus(textBox3);
            }
            else
            {
                Database.setFocus(dateTimePicker1);
                dateTimePicker1.Select();
            }
          
            

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
        

        private void DisplayData(int vi_id)
        {
            dtvou = new DataTable("tblvoucherinfo");
            Database.GetSqlData("select * from tblvoucherinfo where vi_id=" + vi_id, dtvou);
           
            //cmd = new OleDbCommand("select * from voucherinfo where vi_id=" + vid, Database.AccessConn);

            //if (dr.Read())
            //{
            if (dtvou.Rows.Count == 1)
            {
                textBox3.Text = funs11.Select_ac_nm(int.Parse(dtvou.Rows[0]["Ac_id"].ToString()));
                postfix = int.Parse(dtvou.Rows[0]["postfix"].ToString());
                vno = int.Parse(dtvou.Rows[0]["vnumber"].ToString());
                vtid = int.Parse(dtvou.Rows[0]["vt_id"].ToString());
                label2.Text = dtvou.Rows[0]["vnumber"].ToString();
                float total = int.Parse(dtvou.Rows[0]["totqty"].ToString());
                label1.Text = total.ToString();
                dateTimePicker1.Value = DateTime.Parse(dtvou.Rows[0]["Vdate"].ToString());
                if (bool.Parse(dtvou.Rows[0]["IsBilled"].ToString()) == true)
                {
                    checkBox1.Checked = true;
                }
                else
                {
                    checkBox1.Checked = false;
                }
            }
            else
            {
                clear();
            }
            ansGridView1.Rows.Clear();
            dtvoudet = new DataTable("tblVoucherdet");
            Database.GetSqlData("select * from tblvoucherdet where vi_id=" + vi_id + " order by Itemsr", dtvoudet);


            for (int i = 0; i < dtvoudet.Rows.Count; i++)
            {
                ansGridView1.Rows.Add();
              
                ansGridView1.Rows[i].Cells["sno"].Value = dtvoudet.Rows[i]["Itemsr"];
                ansGridView1.Rows[i].Cells["Itemname"].Value = funs11.Select_Item_nm(int.Parse(dtvoudet.Rows[i]["item_id"].ToString()));
                ansGridView1.Rows[i].Cells["Marka"].Value = dtvoudet.Rows[i]["marka"];
                ansGridView1.Rows[i].Cells["Quantity"].Value = dtvoudet.Rows[i]["Quantity"];  
                ansGridView1.Rows[i].Cells["roomno"].Value = dtvoudet.Rows[i]["roomno"];
                ansGridView1.Rows[i].Cells["slapno"].Value = dtvoudet.Rows[i]["slapno"];
                ansGridView1.Rows[i].Cells["section"].Value = dtvoudet.Rows[i]["section"];
                ansGridView1.Rows[i].Cells["remark"].Value = dtvoudet.Rows[i]["remark"];
            }
            // dr.Close();
          //  Database.CloseConnection();
            
        }



        private void save()
        {
            
            //if (vid == 0)
            //{
            //    vno = funs1.GenerateVno(vtid, dateTimePicker1.Value.Date.ToString());
            //}
           
        

            if (dtvou.Rows.Count == 0)
            {

                dtvou.Rows.Add();
            }

            dtvou.Rows[0]["Vt_id"] = vtid;
            dtvou.Rows[0]["postfix"] = postfix;
            //dtvou.Rows[0]["gatepassno"] = textBox4.Text;
            dtvou.Rows[0]["Vnumber"] = label2.Text;
            dtvou.Rows[0]["ac_id"] = funs11.Select_ac_id(textBox3.Text);
            dtvou.Rows[0]["Vdate"] = dateTimePicker1.Value.Date.ToString(Database.dformat);
            dtvou.Rows[0]["Totqty"] = funs11.DecimalPoint(label1.Text);
            dtvou.Rows[0]["Vdate"] = dateTimePicker1.Value.Date;
            dtvou.Rows[0]["Isbilled"] = checkBox1.Checked;
            dtvou.Rows[0]["F_id"] = Database.F_id;
            Database.SaveData(dtvou);
           
           
            if (vid == 0)
            {
                //DataTable dtVoucherId = new DataTable();
                //dtVoucherId.Clear();
                //da = new OleDbDataAdapter("select max(Vi_id) from tblvoucherinfo where vt_id=" + vtid + " and vdate=#" + dateTimePicker1.Value.Date + "#", Database.AccessConn);
                //da.Fill(dtVoucherId);
                vid = Database.GetScalarInt("select max(Vi_id) from tblvoucherinfo ");
            }

            DataTable dttemp;

            dttemp = new DataTable("tblVoucherdet");
            Database.GetSqlData("Select * from tblVoucherdet where vi_id=" + vid, dttemp);
            for (int i = 0; i < dttemp.Rows.Count; i++)
            {
                dttemp.Rows[i].Delete();
            }
            Database.SaveData(dttemp);
            dtvoudet = new DataTable("tblVoucherdet");
            Database.GetSqlData("Select * from tblVoucherdet where vi_id=" + vid, dtvoudet);
            for (int i = 0; i < ansGridView1.Rows.Count-1 ; i++)
            {

                if (int.Parse(ansGridView1.Rows[i].Cells["Quantity"].Value.ToString()) > 0)
                {



                    dtvoudet.Rows.Add();
                    dtvoudet.Rows[dtvoudet.Rows.Count-1]["Vi_id"] = vid;
                    dtvoudet.Rows[dtvoudet.Rows.Count - 1]["Itemsr"] = ansGridView1.Rows[i].Cells["sno"].Value;
                    if (gtype == "Inward")
                    {
                        dtvoudet.Rows[dtvoudet.Rows.Count - 1]["Item_id"] = funs11.Select_Item_id(ansGridView1.Rows[i].Cells["Itemname"].Value.ToString());
                        dtvoudet.Rows[dtvoudet.Rows.Count - 1]["section"] = label2.Text;
                    }
                    else
                    {
                        dtvoudet.Rows[dtvoudet.Rows.Count - 1]["Item_id"] = funs11.Select_Item_id(ansGridView1.Rows[i].Cells["Itemname"].Value.ToString());
                        dtvoudet.Rows[dtvoudet.Rows.Count - 1]["section"] = ansGridView1.Rows[i].Cells["section"].Value;
                       
                    }
                    dtvoudet.Rows[dtvoudet.Rows.Count - 1]["roomno"] = ansGridView1.Rows[i].Cells["roomno"].Value;
                    dtvoudet.Rows[dtvoudet.Rows.Count - 1]["slapno"] = ansGridView1.Rows[i].Cells["slapno"].Value;
                   
                    dtvoudet.Rows[dtvoudet.Rows.Count - 1]["remark"] = ansGridView1.Rows[i].Cells["remark"].Value;
                    dtvoudet.Rows[dtvoudet.Rows.Count - 1]["marka"] = ansGridView1.Rows[i].Cells["marka"].Value;
                 
                    
                    
                    

                    dtvoudet.Rows[dtvoudet.Rows.Count - 1]["Quantity"] = ansGridView1.Rows[i].Cells["Quantity"].Value;
                }
            }
            Database.SaveData(dtvoudet);



            dttemp = new DataTable("tblStock");
            Database.GetSqlData("Select * from tblStock where vid=" + vid, dttemp);
            for (int i = 0; i < dttemp.Rows.Count; i++)
            {
                dttemp.Rows[i].Delete();
            }
            Database.SaveData(dttemp);
            dtvoustock = new DataTable("tblStock");
            Database.GetSqlData("Select * from tblStock where vid=" + vid, dtvoustock);
            for (int i = 0; i < ansGridView1.Rows.Count-1; i++)
            {
                if (int.Parse(ansGridView1.Rows[i].Cells["Quantity"].Value.ToString()) > 0)
                {

                    dtvoustock.Rows.Add();
                    dtvoustock.Rows[dtvoustock.Rows.Count-1]["Vid"] = vid;
                    dtvoustock.Rows[dtvoustock.Rows.Count - 1]["Itemsr"] = ansGridView1.Rows[i].Cells["sno"].Value;
                    if (gtype == "Inward" || gtype == "OpeningStock")
                    {
                        dtvoustock.Rows[dtvoustock.Rows.Count - 1]["Item_id"] = funs11.Select_Item_id(ansGridView1.Rows[i].Cells["Itemname"].Value.ToString());
                        dtvoustock.Rows[dtvoustock.Rows.Count - 1]["ssection"] = label2.Text;
                        dtvoustock.Rows[dtvoustock.Rows.Count - 1]["Quantity"] = ansGridView1.Rows[i].Cells["Quantity"].Value;
                       
                    }
                    else
                    {
                        dtvoustock.Rows[dtvoustock.Rows.Count - 1]["Quantity"] = int.Parse(ansGridView1.Rows[i].Cells["Quantity"].Value.ToString()) * -1;
                        dtvoustock.Rows[dtvoustock.Rows.Count - 1]["ssection"] = ansGridView1.Rows[i].Cells["section"].Value;
                        dtvoustock.Rows[dtvoustock.Rows.Count - 1]["Item_id"] = funs11.Select_Item_id(ansGridView1.Rows[i].Cells["Itemname"].Value.ToString());
                    }
                    
                    
                    
                    dtvoustock.Rows[dtvoustock.Rows.Count - 1]["Marka"] = ansGridView1.Rows[i].Cells["Marka"].Value;
                    dtvoustock.Rows[dtvoustock.Rows.Count - 1]["Ac_id"] = funs11.Select_ac_id(textBox3.Text);

                    dtvoustock.Rows[dtvoustock.Rows.Count - 1]["sroomno"] = ansGridView1.Rows[i].Cells["roomno"].Value;
                    dtvoustock.Rows[dtvoustock.Rows.Count - 1]["sslapno"] = ansGridView1.Rows[i].Cells["slapno"].Value;
                  
                }
            }

            Database.SaveData(dtvoustock);
            

            MessageBox.Show("saved successfully");
            if (Gstr == "0")
            {
             
                LoadData(gtype,"0", this.Text);
           
            }
            else
            {
                this.Close();
                this.Dispose();
            }
           
        }

     
   

        private bool validate()
        {
            ansGridView1.EndEdit();
            if (label2.Text == "")
            {
                MessageBox.Show("Voucher Number can not be Zero.");
                return false;
            }
            if (textBox3.Text == "")
            {
                textBox3.BackColor = Color.Aqua;
                textBox3.Focus();
                return false;
            }

            if (int.Parse(label1.Text) == 0)
            {
                MessageBox.Show("Please Enter Quantity");
                return false;
            }

            for (int i = 0; i < ansGridView1.Rows.Count - 1; i++)
            {

                if (ansGridView1.Rows[i].Cells["Itemname"].Value.ToString().Trim() == "" || ansGridView1.Rows[i].Cells["Itemname"].Value == null)
                {
                    MessageBox.Show("Please Enter Itemname");
                    return false;
                   
                }
                if (ansGridView1.Rows[i].Cells["Quantity"].Value.ToString().Trim() == "0")
                {
                    MessageBox.Show("Please Enter Quantity");
                    return false;

                }
                if (ansGridView1.Rows[i].Cells["Marka"].Value.ToString().Trim() == "" || ansGridView1.Rows[i].Cells["Marka"].Value == null)
                {
                    MessageBox.Show("Please Enter Marka");
                    return false;

                }
               

               

                if (gtype == "Outward")
                {
                        int stk = 0;
                        if (ansGridView1.Rows[i].Cells["slapno"].Value == null)
                        {
                            ansGridView1.Rows[i].Cells["slapno"].Value = 0;
                        }
                        if (ansGridView1.Rows[i].Cells["remark"].Value == null)
                        {
                            ansGridView1.Rows[i].Cells["remark"].Value = 0;
                        }
                        if (ansGridView1.Rows[i].Cells["roomno"].Value == null)
                        {
                            ansGridView1.Rows[i].Cells["roomno"].Value = 0;
                        }
                        stk = Database.GetScalarInt("SELECT Sum(tblstock.Quantity)  as  Stock FROM (((tblstock LEFT JOIN tblVoucherinfo ON tblstock.Vid = tblVoucherinfo.Vi_id) LEFT JOIN tblItemInfo ON tblstock.Item_id = tblItemInfo.Item_id) LEFT JOIN tblAccount ON tblVoucherinfo.Ac_id = tblAccount.Ac_id) LEFT JOIN tblVoucherDet ON (tblstock.Itemsr = tblVoucherDet.Itemsr) AND (tblstock.Vid = tblVoucherDet.Vi_id) WHERE (((tblItemInfo.Item_name)='" + ansGridView1.Rows[i].Cells["Itemname"].Value.ToString().Trim() + "') AND ((tblstock.Marka)='" + ansGridView1.Rows[i].Cells["marka"].Value.ToString().Trim() + "')  AND ((tblstock.ssection)='" + ansGridView1.Rows[i].Cells["section"].Value.ToString().Trim() + "')  AND ((tblAccount.Acc_name)='" + textBox3.Text + "')) and  tblstock.vid<>"+vid+" HAVING (((Sum(tblstock.Quantity))>0))");
                        int qty = int.Parse(ansGridView1.Rows[i].Cells["Quantity"].Value.ToString());
                        if (stk < qty)
                        {
                            MessageBox.Show("You can't Outward more than Inward quantity...");
                            return false;
                        }
                }

                if (gtype == "Outward")
                {
                    DataTable dt = new DataTable();
                    Database.GetSqlData("SELECT tblItemInfo.Item_name, res.Loan, Sum(tblstock.Quantity)-res.Loan AS WithoutLoan FROM (((SELECT tblVoucherinfo.Item_id, tblVoucherinfo.Ac_id, Sum(tblVoucherinfo.Totqty) AS Loan, tblVoucherinfo.bankname FROM tblVoucherinfo LEFT JOIN TblVoucherType ON tblVoucherinfo.Vt_id = TblVoucherType.Vt_id WHERE (((TblVoucherType.Vname)='LoanMemo' Or (TblVoucherType.Vname)='LoanSettlement')) GROUP BY tblVoucherinfo.Item_id, tblVoucherinfo.Ac_id, tblVoucherinfo.bankname )  AS res LEFT JOIN tblstock ON (res.Ac_id = tblstock.Ac_id) AND (res.Item_id = tblstock.Item_id)) LEFT JOIN tblItemInfo ON res.Item_id = tblItemInfo.Item_id) LEFT JOIN tblAccount ON res.Ac_id = tblAccount.Ac_id  WHERE (((tblAccount.Acc_name)='" + textBox3.Text + "'))  GROUP BY  tblItemInfo.Item_name, res.Loan; ", dt);

                    if (dt.Rows.Count != 0)
                    {
                        double withoutloan = 0;
                        if (dt.Select("Item_Name='" + ansGridView1.Rows[i].Cells["Itemname"].Value.ToString().Trim() + "'").Length != 0)
                        {
                            withoutloan = double.Parse(dt.Compute("Sum(WithoutLoan)", "Item_Name='" + ansGridView1.Rows[i].Cells["Itemname"].Value.ToString().Trim() + "'").ToString());
                            double qty = 0;
                            qty = double.Parse(ansGridView1.Rows[i].Cells["Quantity"].Value.ToString());
                            if (withoutloan <= qty)
                            {

                                MessageBox.Show("You can't dispatched so much quantity...");

                                return false;
                            }
                        }

                       
                       
                    }


                }

            }


       

            return true;
        }
        public static bool IsDouble(string text)
        {
            Double num = 0;
            bool isDouble = false;

            // Check for empty string.
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            isDouble = Double.TryParse(text, out num);
            //Console.WriteLine("Qunatity is not ");
            return isDouble;
        }
        public static bool chknumeric(string mobile)
        {
            int result;

            if (int.TryParse(mobile, out result))
            {

                return true;
            }
            else
            {
                Console.WriteLine("String is non numeric");
                return false;
            }
        }
        private void clear()
        {
           // vid = 0; vno = 0; item_id = 0;

         
            label1.Text = "";
            textBox3.Text = "";
          
            dtvou.Rows.Clear();
            dtvoudet.Rows.Clear();
            ansGridView1.Rows.Clear();
            dateTimePicker1.Focus();
        }

     

        private void button3_Click(object sender, EventArgs e)
        {
           // this.Dispose();
        }

        private void button5_Click(object sender, EventArgs e)
        {

           // LoadData(funs11.Select_vt_nm(vtid),"0", "Voucher");

            //dateTimePicker1.Focus();
            
        }



        private void button4_Click(object sender, EventArgs e)
        {

            //textBox1.BackColor = Color.White;
            

            //textBox1.Text = "";
            //label1.Text = "";
            //textBox3.Text = "";

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


            if (gtype != "OpeningStock" && Gstr == "0")
            {
                dtsidefill.Rows.Add();
                dtsidefill.Rows[dtsidefill.Rows.Count - 1]["Name"] = "changevno";
                dtsidefill.Rows[dtsidefill.Rows.Count - 1]["DisplayName"] = "Change Vno";
                dtsidefill.Rows[dtsidefill.Rows.Count - 1]["ShortcutKey"] = "^F12";
                dtsidefill.Rows[dtsidefill.Rows.Count - 1]["Visible"] = true;
            }
            //Delete
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

            else if (name == "changevno")
            {
                InputBox box = new InputBox("Enter Administrative password", "", true);
                box.ShowDialog(this);
                String pass = box.outStr;
                if (pass.ToLower() == "admin")
                {
                    box = new InputBox("Enter Voucher Number", "", false);
                    box.ShowDialog();
                    if (box.outStr == "")
                    {
                        vno = int.Parse(label2.Text);
                    }
                    else
                    {
                        vno = int.Parse(box.outStr);
                    }
                    label2.Text = vno.ToString();
                    //int numtype = funs11.chkNumType(vtid);
                    //if (numtype != 1)
                    //{
                    //    vid = Database.GetScalarInt("Select Vi_id from voucherinfo where Vt_id=" + vtid + " and Vnumber=" + vno + " and Vdate=" + access_sql.Hash + dateTimePicker1.Value.Date.ToString(Database.dformat) + access_sql.Hash);
                    //}
                    //else
                    //{
                        int tempvid = 0;
                        tempvid = Database.GetScalarInt("Select Vi_id from tblvoucherinfo where Vt_id=" + vtid + " and Vnumber=" + vno+ " and F_id="+Database.F_id);
                        if (tempvid != 0)
                        {
                            MessageBox.Show("Voucher can't be created on this No.");
                            vno = 0;
                            label2.Text = vno.ToString();
                            return;
                        }

                        


                   // }
                    
                }
                else
                {
                    MessageBox.Show("Invalid password");
                }
            }



        }


     
        public void Delete()
        {
            DialogResult res = MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.OKCancel);
            if (res == DialogResult.OK)
            {

                
                      //try
                      //  {
                      //      Database.BeginTran();

                            DataTable dtDelete = new DataTable("tblvoucherinfo");
                            Database.GetSqlData("select * from tblvoucherinfo where vi_id=" + Gstr, dtDelete);
                            if (dtDelete.Rows.Count > 0)
                            {

                                dtDelete.Rows[0].Delete();
                                Database.SaveData(dtDelete);

                            }
                            dtDelete.Rows.Clear();
                            DataTable dtDelete1 = new DataTable("tblvoucherDet");
                            Database.GetSqlData("select * from tblvoucherdet where vi_id=" + Gstr, dtDelete1);
                            for (int i = 0; i < dtDelete1.Rows.Count; i++)
                            {

                                dtDelete1.Rows[i].Delete();

                            }
                            Database.SaveData(dtDelete1);
                            dtDelete1 = new DataTable("tblstock");
                            Database.GetSqlData("select * from tblstock where vid=" + Gstr, dtDelete1);
                            for (int i = 0; i < dtDelete1.Rows.Count; i++)
                            {

                                dtDelete1.Rows[i].Delete();
                            }

                            Database.SaveData(dtDelete1);

                        //    Database.CommitTran();

                        //}
                        //catch (Exception ex)
                        //{
                            
                        //    Database.RollbackTran();
                        //}
                        this.Close();
                        this.Dispose();
                    
               
               
                }


            }

        
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {

        }




        private void frm_voucher_Load(object sender, EventArgs e)
        {
            SideFill();
            //dateTimePicker1.CustomFormat = Database.dformat;
            if (gtype == "OpeningStock")
            {
                textBox3.Select();
            }
        //    ansGridView1.CurrentCell = ansGridView1.Rows[0].Cells[1];

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
           
                DataTable dtAcc = new DataTable();
              
                String strCombo = "";
               
                strCombo = "select [Acc_name] from tblAccount order by acc_name";
               
               
                textBox3.Text = SelectCombo.ComboKeypress(this, e.KeyChar, strCombo, e.KeyChar.ToString(), 0);

                ansGridView1.CurrentCell = ansGridView1["Itemname",0];
             
                //MessageBox.Show("tem selected");
              
        }

        private void ansGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (gtype == "Inward" || gtype == "OpeningStock")
            {
              
                if (char.IsLetter(e.KeyChar) || char.IsNumber(e.KeyChar) || e.KeyChar == ' ' || Convert.ToInt32(e.KeyChar) == 13 || Convert.ToInt32(e.KeyChar) == 9)
                {
                }
                else
                {
                    return;
                }

                if (ansGridView1.CurrentCell.OwningColumn.Name == "Itemname")
                {

                   
                    String strCombo;

                    strCombo = "select distinct Item_name from tblItemInfo order by Item_name";

                    ansGridView1.CurrentCell.Value = SelectCombo.ComboKeypress(this, e.KeyChar, strCombo, e.KeyChar.ToString(), 0);
                    ansGridView1.CurrentRow.Cells["roomno"].Value = "0";
                    ansGridView1.CurrentRow.Cells["slapno"].Value = "0";
                 
                    SendKeys.Send("{Enter}");
                   
                    this.Activate();

                }
            }
            else if (gtype == "Outward")
            {
                if (char.IsLetter(e.KeyChar) || char.IsNumber(e.KeyChar) || e.KeyChar == ' ' || Convert.ToInt32(e.KeyChar) == 13 || Convert.ToInt32(e.KeyChar) == 9)
                {
                }
                else
                {
                    return;
                }

                if (ansGridView1.CurrentCell.OwningColumn.Name == "Itemname")
                {
                    String strCombo;

                    strCombo = "SELECT DISTINCT tblItemInfo.Item_name, tblstock.Marka,  tblstock.ssection , Sum(tblstock.Quantity) & '' AS Stock FROM (((tblstock LEFT JOIN tblVoucherinfo ON tblstock.Vid = tblVoucherinfo.Vi_id) LEFT JOIN tblItemInfo ON tblstock.Item_id = tblItemInfo.Item_id) LEFT JOIN tblAccount ON tblVoucherinfo.Ac_id = tblAccount.Ac_id) LEFT JOIN tblVoucherDet ON (tblstock.Vid = tblVoucherDet.Vi_id) AND (tblstock.Itemsr = tblVoucherDet.Itemsr) WHERE (((tblAccount.Acc_name)='" + textBox3.Text + "') AND tblstock.Vid<>"+vid+") GROUP BY tblItemInfo.Item_name, tblstock.Marka, tblstock.ssection HAVING (((Sum(tblstock.Quantity))>0));";

                    DataTable dt1 = new DataTable();
                    Database.GetSqlData(strCombo, dt1);
                    string value = "";

                    value = SelectCombo.ComboDt1(this, dt1, 4);

                    String[] print_option = value.Split('|');
                    for (int j = 0; j < print_option.Length; j++)
                    {
                        if (print_option[j] != "")
                        {
                            if (j == 0)
                            {
                                ansGridView1.CurrentRow.Cells["Itemname"].Value = print_option[j];
                            }
                            else if (j == 1)
                            {
                                ansGridView1.CurrentRow.Cells["marka"].Value = print_option[j];
                                
                            }
                           
                            else if (j == 2)
                            {
                                ansGridView1.CurrentRow.Cells["section"].Value = print_option[j];
                            }
                           
                            else if (j == 3)
                            {
                                ansGridView1.CurrentRow.Cells["Quantity"].Value =0;
                              
                              
                                ansGridView1.Columns["Quantity"].ReadOnly = false;
                               
                                ansGridView1.CurrentCell = ansGridView1["Quantity", ansGridView1.CurrentCell.RowIndex];
                               // SendKeys.Send("0");
                             ansGridView1.CurrentRow.Cells["Quantity"].Value=0;
                              
                                this.Activate();
                               
                            }
                            else if (j == 4)
                            {
                                ansGridView1.CurrentRow.Cells["stock"].Value = print_option[3];
                            }
                           

                        }



                    }
                }



            }


            calc();
        }

        private void calc()
        {
            int total = 0;
            

          
           for (int i = 0; i < ansGridView1.Rows.Count - 1; i++)
           {
               if (ansGridView1.Rows[i].Cells["Quantity"].Value == null || ansGridView1.Rows[i].Cells["Quantity"].Value.ToString() == "")
               {
                   ansGridView1.Rows[i].Cells["Quantity"].Value = 0;
               }

               total += int.Parse(ansGridView1.Rows[i].Cells["Quantity"].Value.ToString());
           }
            label1.Text = total.ToString();
        }

        private void ansGridView1_CellEnter_1(object sender, DataGridViewCellEventArgs e)
        {
            ansGridView1.Rows[e.RowIndex].Cells["sno"].Value = e.RowIndex + 1;
            if (ansGridView1.CurrentCell.OwningColumn.Name == "sno" && e.RowIndex!=0)
            {
                SendKeys.Send("{right}");
             
                this.Activate();
               
            }
            if (gtype == "Inward")
            {
                if (ansGridView1.CurrentCell.OwningColumn.Name == "Itemname" && e.RowIndex != 0)
                {
                    ansGridView1.CurrentCell.Value = ansGridView1.Rows[0].Cells["Itemname"].Value.ToString();
                    SendKeys.Send("{right}");

                    this.Activate();
                }
            }


          //  ansGridView1.CurrentCell = ansGridView1["Itemname", ansGridView1.CurrentCell.RowIndex];
        }

        private void DisplaySetting()
        {
               DataTable dtvt = new DataTable();
           string cmbVouTyp = "select [vname] from tblvouchertype where  type='" + gtype + "' order by vname ";
         
            Database.GetSqlData(cmbVouTyp, dtvt);

            if (dtvt.Rows.Count == 1)
            {
                textBox1.Text = dtvt.Rows[0]["vname"].ToString();
                vtid = funs11.Select_vt_id(textBox1.Text);
                textBox1.Enabled = false;
                SetVno();
            }
           

            if (textBox1.Text == "")
            {
                return;
            }
            vtid = funs11.Select_vt_id(textBox1.Text);
            
        }

        private void frm_voucher_KeyDown(object sender, KeyEventArgs e)
        {


            if (e.KeyCode == Keys.F2)
            {
                if (ansGridView1.CurrentCell.OwningColumn.Name == "Marka")
                {
                    if (gtype == "Inward" || gtype == "OpeningStock")
                    {
                        String strCombo;
                        ansGridView1.Columns["Marka"].ReadOnly = true;
                        strCombo = "select distinct Marka from tblstock Where marka <>'' order by Marka";

                        ansGridView1.CurrentCell.Value = SelectCombo.ComboKeypress(this, 'a', strCombo, "", 0);
                        SendKeys.Send("{Enter}");
                        ansGridView1.Columns["Marka"].ReadOnly = false;
                        this.Activate();
                    }

                }
            }

            if (e.Control && e.KeyCode == Keys.S)
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
            else if (e.Control && e.KeyCode == Keys.F12)
            {
                e.Handled = true;
                 InputBox box = new InputBox("Enter Administrative password", "", true);
                box.ShowDialog(this);
                String pass = box.outStr;
                if (pass.ToLower() == "admin")
                {
                    box = new InputBox("Enter Voucher Number", "", false);
                    box.ShowDialog();
                    if (box.outStr == "")
                    {
                        vno = int.Parse(label2.Text);
                    }
                    else
                    {
                        vno = int.Parse(box.outStr);
                    }
                    label2.Text = vno.ToString();
                    //int numtype = funs11.chkNumType(vtid);
                    //if (numtype != 1)
                    //{
                    //    vid = Database.GetScalarInt("Select Vi_id from voucherinfo where Vt_id=" + vtid + " and Vnumber=" + vno + " and Vdate=" + access_sql.Hash + dateTimePicker1.Value.Date.ToString(Database.dformat) + access_sql.Hash);
                    //}
                    //else
                    //{
                        int tempvid = 0;
                        tempvid = Database.GetScalarInt("Select Vi_id from tblvoucherinfo where Vt_id=" + vtid + " and Vnumber=" + vno+" and F_id="+Database.F_id);
                        if (tempvid != 0)
                        {
                            MessageBox.Show("Voucher can't be created on this No.");
                            vno = 0;
                            label2.Text = vno.ToString();
                            return;
                        }

                       

                   // }
                    
                }
                else
                {
                    MessageBox.Show("Invalid password");
                }
            


               
            } 
            if (e.KeyCode == Keys.F4)
            {
                if (ansGridView1.CurrentCell.OwningColumn.Name == "Marka")
                {
                    String strCombo;
                 
                    strCombo = "select distinct Marka from tblstock where Marka <>'' order by marka";
                    string str="";
                    ansGridView1.CurrentCell.Value = SelectCombo.ComboKeydown(this,e.KeyCode, strCombo, str, 0);
                    
                        ansGridView1.CurrentCell = ansGridView1["Quantity", ansGridView1.CurrentCell.RowIndex];
                  
                }
            }
            if (e.Control && e.KeyCode == Keys.D)
            {
                if (Gstr != "0")
                {
                    Delete();
                }
            }
        }

        private void ansGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (ansGridView1.CurrentCell.OwningColumn.Name == "Quantity" && ansGridView1.Rows[e.RowIndex].Cells["Quantity"].Value == null)
            {
                ansGridView1.Rows[e.RowIndex].Cells["Quantity"].Value = 0;
            }
            if (ansGridView1.CurrentCell.OwningColumn.Name == "Quantity" && ansGridView1.Rows[e.RowIndex].Cells["Quantity"].Value.ToString() != "")
            {
                ansGridView1.Rows[e.RowIndex].Cells["Quantity"].Value = ansGridView1.Rows[e.RowIndex].Cells["Quantity"].Value;
               
            }
           
            calc();
        }

        private void dateTimePicker1_KeyDown_1(object sender, KeyEventArgs e)
        {
            funs11.IsEnter(this, e.KeyCode);
        }

        private void ansGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (ansGridView1.CurrentRow.Index == ansGridView1.Rows.Count - 1)
                {
                    for (int i = 0; i < ansGridView1.Columns.Count; i++)
                    {
                        ansGridView1.Rows[ansGridView1.CurrentRow.Index].Cells[i].Value = null;
                        calc();
                    }
                }
                else
                {
                    int rindex = ansGridView1.CurrentRow.Index;
                    ansGridView1.Rows.RemoveAt(rindex);
                    for (int i = 0; i < ansGridView1.Rows.Count; i++)
                    {
                        ansGridView1.Rows[i].Cells["sno"].Value = (i+1);

                     }
                    calc();

                }
                
            }
                 if (e.Control && e.KeyCode == Keys.C)
                 {
                     if (ansGridView1.CurrentCell.OwningColumn.Name == "Itemname")
                      {
                
                        ansGridView1.CurrentCell.Value = funs11.AddItem();
                       }
                 }
                 if (e.Control && e.KeyCode == Keys.A)
                 {
                     if (ansGridView1.CurrentCell.OwningColumn.Name == "Itemname")
                     {
                         ansGridView1.CurrentCell.Value = funs11.EditItem(ansGridView1.CurrentCell.Value.ToString());
                     }
                 }

            
        }

        private void dateTimePicker1_Leave(object sender, EventArgs e)
        
        {
            
            Database.lostFocus(dateTimePicker1);
        }

        private void dateTimePicker1_Enter(object sender, EventArgs e)
        {
            Database.setFocus(dateTimePicker1);
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            Database.lostFocus(textBox3);
           
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            Database.setFocus(textBox3);
        }

        private void ansGridView1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
          //  Database.lostFocus(textBox1);
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
           // Database.setFocus(textBox1);
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {

           
            if (e.Control && e.KeyCode == Keys.C)
            
            {
                textBox3.Text = funs11.AddAccount();
            }
            if (e.Control && e.KeyCode == Keys.A)
            {
                textBox3.Text = funs11.EditAccount(textBox3.Text);
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            SelectCombo.IsEnter(this, e.KeyCode);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (gtype == "Outward")
            {
                ansGridView1.Rows.Clear();
            }

           //if(textBox3.Text != "")
           // {
           //     item_id = funs11.Select_ac_id(textBox3.Text);
           //     ansGridView1.Enabled = true;
           //     ansGridView1.Select();
           // }
        }

        private void ansGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            ansGridView1.Rows[e.RowIndex].Cells["Quantity"].Value = 0;
           // ansGridView1.CurrentCell = ansGridView1["Itemname", ansGridView1.CurrentCell.RowIndex];
        }

      
       

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            SelectCombo.IsEnter(this, e.KeyCode);
        }

        

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            string cmbVouTyp = "select [vname] from tblvouchertype where  type='" + gtype + "' order by vname ";
         

            textBox1.Text = SelectCombo.ComboKeypress(this, e.KeyChar, cmbVouTyp, e.KeyChar.ToString(), 0);
            if (textBox1.Text == "")
            {
                return;
            }
            vtid = funs11.Select_vt_id(textBox1.Text);
            SetVno();
        }

      
        
    }
}
