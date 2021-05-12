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
    public partial class frmMaster : Form
    {
        string gstr = "";
        BindingSource bs = new BindingSource();
        DataTable dtitem = new DataTable();
        public frmMaster()
        {
            InitializeComponent();
        }

        private void filter()
        {
            String strTemp = textBox1.Text;
            strTemp = strTemp.Replace("%", "?");
            strTemp = strTemp.Replace("[", string.Empty);
            strTemp = strTemp.Replace("]", string.Empty);
            string strfilter = "";

            int a = 0;
            a = dtitem.Columns.Count;
            if (gstr == "Inward" || gstr == "Outward")
            {
                for (int i = 1; i < dtitem.Columns.Count - 1; i++)
                {
                    if (strfilter != "")
                    {
                        strfilter += " or ";
                    }
                    strfilter += "(" + dtitem.Columns[i].ColumnName + " like '*" + strTemp + "*' " + ")";
                }
            }
            else if (gstr == "LoanS" || gstr == "LoanM")
            {
                for (int i = 1; i < dtitem.Columns.Count - 1; i++)
                {
                    if (strfilter != "")
                    {
                        strfilter += " or ";
                    }
                    strfilter += "(" + dtitem.Columns[i].ColumnName + " like '*" + strTemp + "*' " + ")";
                }
            }
            else
            {
                for (int i = 1; i < dtitem.Columns.Count; i++)
                {
                    if (strfilter != "")
                    {
                        strfilter += " or ";
                    }
                    strfilter += "(" + dtitem.Columns[i].ColumnName + " like '*" + strTemp + "*' " + ")";
                }
            }
            bs.Filter = null;
            bs.DataSource = dtitem;
            bs.Filter = strfilter;
        }

        public void LoadData(string str, string frmCaption)
        {
            gstr = str;
            string sql = "";
           // dtitem.Clear();
            this.Text = frmCaption;
            
            if (str == "Account")
            {

               //   string X = " where tblAccount.acc_name Like ('%" + textBox1.Text + "%')";   
           
                sql = "select  Ac_id, acc_name as AccountName,Address,GST_No As GSTNumber,mobile_No as MobileNo from tblAccount order by acc_name ";

                 Database.GetSqlData(sql, dtitem);

                ansGridView5.DataSource = dtitem;
                ansGridView5.Columns["Ac_id"].Visible = false;
               label2.Text = "List of Accounts";
            }

            if (str == "Item")
            {
              //  string Y = " where tblIteminfo.Item_name Like('%" + textBox1.Text + "%')";

                sql = "select Item_id,Item_name as ItemName from tblIteminfo order by Item_name";
                Database.GetSqlData(sql, dtitem);

                ansGridView5.DataSource = dtitem;
                ansGridView5.Columns["Item_id"].Visible = false;
                label2.Text = "List of Item";
            }

            if (str == "Inward")
            {
              
                //sql = "SELECT tblVoucherinfo.Vi_id, Format$([tblVoucherinfo].[Vdate],'dd-mmm-yyyy') AS VoucherDate, tblAccount.Acc_name AS AccountName, tblVoucherinfo.vnumber & '' AS VoucherNumber, tblVoucherinfo.Totqty AS TotalQuantity  FROM (((tblVoucherinfo INNER JOIN TblVoucherType ON tblVoucherinfo.Vt_id = TblVoucherType.Vt_id) INNER JOIN tblAccount ON tblVoucherinfo.Ac_id = tblAccount.Ac_id) LEFT JOIN tblVoucherDet ON tblVoucherinfo.Vi_id = tblVoucherDet.Vi_id) LEFT JOIN tblItemInfo ON tblVoucherDet.Item_id = tblItemInfo.Item_id GROUP BY tblVoucherinfo.Vi_id, Format$([tblVoucherinfo].[Vdate],'dd-mmm-yyyy'), tblAccount.Acc_name, tblVoucherinfo.vnumber & '',  tblVoucherinfo.vdate, tblVoucherinfo.vnumber,  tblVoucherinfo.Totqty ,tblVoucherinfo.vt_id HAVING (((tblVoucherinfo.vt_id)=1)) ORDER BY tblVoucherinfo.vdate desc, tblVoucherinfo.vnumber desc;";
                sql = "SELECT   tblVoucherinfo.Vi_id, TblVoucherType.Vname,Format$([tblVoucherinfo].[Vdate],'dd-mmm-yyyy') AS VoucherDate, tblAccount.Acc_name AS AccountName, tblVoucherinfo.vnumber & '' AS VoucherNumber, tblVoucherinfo.Totqty AS TotalQuantity FROM (((tblVoucherinfo INNER JOIN TblVoucherType ON tblVoucherinfo.Vt_id = TblVoucherType.Vt_id) INNER JOIN tblAccount ON tblVoucherinfo.Ac_id = tblAccount.Ac_id) LEFT JOIN tblVoucherDet ON tblVoucherinfo.Vi_id = tblVoucherDet.Vi_id) LEFT JOIN tblItemInfo ON tblVoucherDet.Item_id = tblItemInfo.Item_id WHERE TblVoucherType.Type='Inward' and (tblVoucherinfo.vdate>=#" + Database.stDate.ToString(Database.dformat) + "# And tblVoucherinfo.Vdate<=#" + Database.enDate.ToString(Database.dformat) + "#) GROUP BY TblVoucherType.Vname, tblVoucherinfo.Vi_id, Format$([tblVoucherinfo].[Vdate],'dd-mmm-yyyy'), tblAccount.Acc_name, tblVoucherinfo.vnumber & '', tblVoucherinfo.Totqty, tblVoucherinfo.vdate, tblVoucherinfo.vnumber ORDER BY tblVoucherinfo.vdate DESC , tblVoucherinfo.vnumber DESC;";
                Database.GetSqlData(sql, dtitem);
                label4.Text = "0";


                if (dtitem.Rows.Count > 0)
                {
                    label4.Text = "Total Quantity:- " + int.Parse(dtitem.Compute("Sum(TotalQuantity)", "").ToString());
                }
                ansGridView5.DataSource = dtitem;
                ansGridView5.Columns["Vi_id"].Visible = false;
                
                label2.Text = "List of Inward";
            }
            if (str == "Outward")
            {
               // sql = "SELECT tblVoucherinfo.Vi_id, Format$([tblVoucherinfo].[Vdate],'dd-mmm-yyyy') AS VoucherDate, tblAccount.Acc_name AS AccountName, tblVoucherinfo.vnumber & '' AS VoucherNumber, tblVoucherinfo.Totqty AS TotalQuantity  FROM (((tblVoucherinfo INNER JOIN TblVoucherType ON tblVoucherinfo.Vt_id = TblVoucherType.Vt_id) INNER JOIN tblAccount ON tblVoucherinfo.Ac_id = tblAccount.Ac_id) LEFT JOIN tblVoucherDet ON tblVoucherinfo.Vi_id = tblVoucherDet.Vi_id) LEFT JOIN tblItemInfo ON tblVoucherDet.Item_id = tblItemInfo.Item_id GROUP BY tblVoucherinfo.Vi_id, Format$([tblVoucherinfo].[Vdate],'dd-mmm-yyyy'), tblAccount.Acc_name, tblVoucherinfo.vnumber & '',  tblVoucherinfo.vdate, tblVoucherinfo.vnumber,  tblVoucherinfo.Totqty ,tblVoucherinfo.vt_id HAVING (((tblVoucherinfo.vt_id)=2)) ORDER BY tblVoucherinfo.vdate desc, tblVoucherinfo.vnumber desc;";
                sql = "SELECT   tblVoucherinfo.Vi_id, TblVoucherType.Vname,Format$([tblVoucherinfo].[Vdate],'dd-mmm-yyyy') AS VoucherDate, tblAccount.Acc_name AS AccountName, tblVoucherinfo.vnumber & '' AS VoucherNumber, tblVoucherinfo.Totqty AS TotalQuantity FROM (((tblVoucherinfo INNER JOIN TblVoucherType ON tblVoucherinfo.Vt_id = TblVoucherType.Vt_id) INNER JOIN tblAccount ON tblVoucherinfo.Ac_id = tblAccount.Ac_id) LEFT JOIN tblVoucherDet ON tblVoucherinfo.Vi_id = tblVoucherDet.Vi_id) LEFT JOIN tblItemInfo ON tblVoucherDet.Item_id = tblItemInfo.Item_id WHERE (((TblVoucherType.Type)='Outward'))  and (tblVoucherinfo.vdate>=#" + Database.stDate.ToString(Database.dformat) + "# And tblVoucherinfo.Vdate<=#" + Database.enDate.ToString(Database.dformat) + "#)  GROUP BY TblVoucherType.Vname, tblVoucherinfo.Vi_id, Format$([tblVoucherinfo].[Vdate],'dd-mmm-yyyy'), tblAccount.Acc_name, tblVoucherinfo.vnumber & '', tblVoucherinfo.Totqty, tblVoucherinfo.vdate, tblVoucherinfo.vnumber ORDER BY tblVoucherinfo.vdate DESC , tblVoucherinfo.vnumber DESC;";
                Database.GetSqlData(sql, dtitem);
              
               
                label4.Text = "0";
                 if(dtitem.Rows.Count>0)
                {

                    label4.Text = "Total Quantity:- " +int.Parse(dtitem.Compute("Sum(TotalQuantity)", "").ToString());
                }
                ansGridView5.DataSource = dtitem;
                ansGridView5.Columns["Vi_id"].Visible = false;
                label2.Text = "List of Outward";
            }
           
            if (str == "OpeningStock")
            {
                sql = "SELECT tblVoucherinfo.Vi_id, Format$([tblVoucherinfo].[Vdate],'dd-mmm-yyyy') AS VoucherDate, tblAccount.Acc_name AS AccountName, tblVoucherinfo.vnumber & '' AS VoucherNumber, tblVoucherinfo.Totqty AS TotalQuantity  FROM (((tblVoucherinfo INNER JOIN TblVoucherType ON tblVoucherinfo.Vt_id = TblVoucherType.Vt_id) INNER JOIN tblAccount ON tblVoucherinfo.Ac_id = tblAccount.Ac_id) LEFT JOIN tblVoucherDet ON tblVoucherinfo.Vi_id = tblVoucherDet.Vi_id) LEFT JOIN tblItemInfo ON tblVoucherDet.Item_id = tblItemInfo.Item_id GROUP BY tblVoucherinfo.Vi_id, Format$([tblVoucherinfo].[Vdate],'dd-mmm-yyyy'), tblAccount.Acc_name, tblVoucherinfo.vnumber & '',  tblVoucherinfo.vdate, tblVoucherinfo.vnumber,  tblVoucherinfo.Totqty ,tblVoucherinfo.vt_id HAVING (((tblVoucherinfo.vt_id)=3))  and tblVoucherinfo.vdate>=#" + Database.stDate.AddDays(-1).ToString(Database.dformat) + "#   ORDER BY tblVoucherinfo.vdate desc, tblVoucherinfo.vnumber desc;";
                Database.GetSqlData(sql, dtitem);
                label4.Text = "0";


                if (dtitem.Rows.Count > 0)
                {
                    label4.Text = "Total Quantity:- " + int.Parse(dtitem.Compute("Sum(TotalQuantity)", "").ToString());
                }
                ansGridView5.DataSource = dtitem;
                ansGridView5.Columns["Vi_id"].Visible = false;

              
                label2.Text = "List of Opening Stock";
            }
            if (str == "LoanM")
            {
                sql = "SELECT tblVoucherinfo.Vi_id, Format$([tblVoucherinfo].[Vdate],'dd-mmm-yyyy') AS VoucherDate, tblAccount.Acc_name AS AccountName, tblVoucherinfo.vnumber & '' AS VoucherNumber, tblVoucherinfo.Totqty AS TotalQuantity FROM (tblVoucherinfo INNER JOIN TblVoucherType ON tblVoucherinfo.Vt_id = TblVoucherType.Vt_id) INNER JOIN tblAccount ON tblVoucherinfo.Ac_id = tblAccount.Ac_id WHERE (((TblVoucherType.Vname)='LoanMemo')) GROUP BY tblVoucherinfo.Vi_id, Format$([tblVoucherinfo].[Vdate],'dd-mmm-yyyy'), tblAccount.Acc_name, tblVoucherinfo.vnumber & '', tblVoucherinfo.Totqty, tblVoucherinfo.vdate, tblVoucherinfo.vnumber ORDER BY tblVoucherinfo.vdate desc, tblVoucherinfo.vnumber desc";
                Database.GetSqlData(sql, dtitem);
                label4.Text = "0";

                if (dtitem.Rows.Count > 0)
                {
                    label4.Text = "Total Quantity:- " + int.Parse(dtitem.Compute("Sum(TotalQuantity)", "").ToString());
                }




                ansGridView5.DataSource = dtitem;
                ansGridView5.Columns["Vi_id"].Visible = false;
                label2.Text = "List of Loan Memo";
            }
            if (str == "LoanS")
            {
                sql = "SELECT tblVoucherinfo.Vi_id, Format$([tblVoucherinfo].[Vdate],'dd-mmm-yyyy') AS VoucherDate, tblAccount.Acc_name AS AccountName, tblVoucherinfo.vnumber & '' AS VoucherNumber, -1*tblVoucherinfo.Totqty AS TotalQuantity FROM (tblVoucherinfo INNER JOIN TblVoucherType ON tblVoucherinfo.Vt_id = TblVoucherType.Vt_id) INNER JOIN tblAccount ON tblVoucherinfo.Ac_id = tblAccount.Ac_id WHERE (((TblVoucherType.Vname)='LoanSettlement')) GROUP BY tblVoucherinfo.Vi_id, Format$([tblVoucherinfo].[Vdate],'dd-mmm-yyyy'), tblAccount.Acc_name, tblVoucherinfo.vnumber & '', tblVoucherinfo.Totqty, tblVoucherinfo.vdate, tblVoucherinfo.vnumber ORDER BY tblVoucherinfo.vdate desc, tblVoucherinfo.vnumber desc";
                Database.GetSqlData(sql, dtitem);
                label4.Text = "0";

                if (dtitem.Rows.Count > 0)
                {
                    label4.Text = "Total Quantity:- " + int.Parse(dtitem.Compute("Sum(TotalQuantity)", "").ToString());
                }




                ansGridView5.DataSource = dtitem;
                ansGridView5.Columns["Vi_id"].Visible = false;
                label2.Text = "List of Loan Settlement";
            }
            textBox1.Focus();
            ansGridView5.Columns["Edit"].DataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ansGridView5.Columns["Edit"].DisplayIndex = ansGridView5.Columns.Count - 2 + 1;
            ansGridView5.Columns["Delete"].DisplayIndex = ansGridView5.Columns.Count - 2 + 1;
        }

    

        private bool validate(string name)
        {

            if (gstr == "Inward")
            {
                DataTable dt = new DataTable("tblstock");
                Database.GetSqlData("select * from tblstock where vid=" + int.Parse(name) + "", dt);
                int reffno = 0;
                reffno = Database.GetScalarInt("Select reffno from tblstock where vid=" + int.Parse(name));

                if (reffno != 0)
                {
                    DataTable dt1 = new DataTable();
                    Database.GetSqlData("Select * from tblvoucherinfo where vt_id=1 and vnumber="+reffno,dt1);

                    if (dt1.Rows.Count != 0)
                    {
                        MessageBox.Show("Please Delete Outward..");
                        return false;
                    }


                }


                
                return true;

            }


            if (gstr == "Account")
            {
                DataTable dt= new DataTable("tblvoucherinfo");
                Database.GetSqlData("select * from tblvoucherinfo where ac_id="+ name+"",dt);
                if (dt.Rows.Count > 0)
                {
                    return false;
                }
                return true;
            }
            if (gstr == "Item")
            {
                DataTable dt = new DataTable("tblvoucherdet");
                Database.GetSqlData("select * from tblvoucherdet where Item_id=" + name + "", dt);
                if (dt.Rows.Count > 0)
                {
                    return false;
                }

                dt = new DataTable("tblVoucherinfo");
                Database.GetSqlData("select * from tblVoucherinfo where Item_id=" + name + "", dt);
                if (dt.Rows.Count > 0)
                {
                    return false;
                }


                return true;
            }
            


            return true;
        }

        private void SideFill()
        {
            flowLayoutPanel1.Controls.Clear();
            DataTable dtsidefill = new DataTable();
            dtsidefill.Columns.Add("Name", typeof(string));
            dtsidefill.Columns.Add("DisplayName", typeof(string));
            dtsidefill.Columns.Add("ShortcutKey", typeof(string));
            dtsidefill.Columns.Add("Visible", typeof(bool));
            //createnew
            dtsidefill.Rows.Add();
            dtsidefill.Rows[dtsidefill.Rows.Count - 1]["Name"] = "add";
            dtsidefill.Rows[dtsidefill.Rows.Count - 1]["DisplayName"] = "Create New";
            dtsidefill.Rows[dtsidefill.Rows.Count - 1]["ShortcutKey"] = "^C";
            dtsidefill.Rows[dtsidefill.Rows.Count - 1]["Visible"] = true;

            //refresh
            dtsidefill.Rows.Add();
            dtsidefill.Rows[dtsidefill.Rows.Count - 1]["Name"] = "refresh";
            dtsidefill.Rows[dtsidefill.Rows.Count - 1]["DisplayName"] = "Refresh";
            dtsidefill.Rows[dtsidefill.Rows.Count - 1]["ShortcutKey"] = "^R";
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
                    System.Drawing.Rectangle RC = btn.ClientRectangle;
                    System.Drawing.Font font = new System.Drawing.Font("Arial", 12);
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

            if (name == "add")
            {
               
                if (gstr == "Account")
                {
                    frm_Account frm = new frm_Account();
                    frm.LoadData("0", "frm_account");
                    frm.MdiParent = this.MdiParent; 
                    frm.Show();
                   
                }
                else  if (gstr == "Item")
                {

                    frm_Item frm = new frm_Item();
                    frm.LoadData("0", "Product Group");
                    frm.MdiParent = this.MdiParent; 
                    frm.Show();
                    
                }
                else if (gstr == "Inward")
                {
                    frm_voucher frm = new frm_voucher();
                    frm.LoadData("Inward","0", "Inward");
                    frm.MdiParent = this.MdiParent; ;
                    frm.Show();

                }
                else if (gstr == "Outward")
                {
                    frm_voucher frm = new frm_voucher();
                    frm.LoadData("Outward", "0", "Outward");
                    frm.MdiParent = this.MdiParent; ;
                    frm.Show();

                }
                else if (gstr == "OpeningStock")
                {
                    frm_voucher frm = new frm_voucher();
                    frm.LoadData("OpeningStock", "0", "Opening Stock");
                    frm.MdiParent = this.MdiParent; ;
                    frm.Show();

                }
                else if (gstr == "LoanM")
                {
                    frm_Loanvou frm = new frm_Loanvou();
                    frm.LoadData("LoanMemo", "0", "LoanMemo");
                    frm.MdiParent = this.MdiParent; ;
                    frm.Show();

                }
                else if (gstr == "LoanS")
                {
                    frm_Loanvou frm = new frm_Loanvou();
                    frm.LoadData("LoanSettlement", "0", "LoanSettlement");
                    frm.MdiParent = this.MdiParent; ;
                    frm.Show();

                }
            }


            else if (name == "refresh")
            {
                LoadData(gstr, gstr);
            }


            else if (name == "quit")
            {
                this.Close();
                this.Dispose();
            }
           




        }

        private void frmMaster_Load(object sender, EventArgs e)
        {
            SideFill();
        }

        private void frmMaster_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                this.Dispose();
            }
            else if(e.Control && e.KeyCode==Keys.R)
            {
                LoadData(gstr, gstr);
            }
            else if (e.Control && e.KeyCode == Keys.C)
            {
                if (gstr == "Account")
                {
                    frm_Account frm = new frm_Account();
                    frm.LoadData("0", "Account");
                    frm.MdiParent = this.MdiParent;
                    frm.Show();


                }
                else if (gstr == "Item")
                {
                    frm_Item frm = new frm_Item();
                    frm.LoadData("0", "Item");
                    frm.MdiParent = this.MdiParent;
                    frm.Show();
                }
                else if (gstr == "Inward")
                {
                    frm_voucher frm = new frm_voucher();
                    frm.LoadData("Inward", "0", "Inward");
                    frm.MdiParent = this.MdiParent; ;
                    frm.Show();
                }
                else if (gstr == "LoanM")
                {
                    frm_Loanvou frm = new frm_Loanvou();
                    frm.LoadData("LoanMemo", "0", "LoanMemo");
                    frm.MdiParent = this.MdiParent; ;
                    frm.Show();
                }
                else if (gstr == "LoanS")
                {
                    frm_Loanvou frm = new frm_Loanvou();
                    frm.LoadData("LoanSettlement", "0", "LoanSettlement");
                    frm.MdiParent = this.MdiParent; ;
                    frm.Show();
                }
                else if (gstr == "Outward")
                {
                    frm_voucher frm = new frm_voucher();
                    frm.LoadData("Outward", "0", "Outward");
                    frm.MdiParent = this.MdiParent; ;
                    frm.Show();
                }
                else if (gstr == "OpeningStock")
                { 
               
                    frm_voucher frm = new frm_voucher();
                    frm.LoadData("OpeningStock", "0", "Opening Stock");
                    frm.MdiParent = this.MdiParent; ;
                    frm.Show();
               
                }
           }

            
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            filter();
           // LoadData(gstr,"Account");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ansGridView5_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void ansGridView5_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gstr == "Account")
            {

                if (ansGridView5.CurrentCell.OwningColumn.Name == "Edit")
                {
                    if (ansGridView5.Rows[ansGridView5.CurrentRow.Index].Cells["Ac_id"].Value.ToString() == "0")
                    {
                        return;
                    }
                    frm_Account frm = new frm_Account();
                    frm.LoadData(ansGridView5.Rows[ansGridView5.CurrentRow.Index].Cells["Ac_id"].Value.ToString(), "Edit Account");
                    frm.MdiParent = this.MdiParent;
                    frm.Show();

                }
                else if (ansGridView5.CurrentCell.OwningColumn.Name == "Delete")
                {
                    if (validate(ansGridView5.Rows[ansGridView5.CurrentRow.Index].Cells["ac_id"].Value.ToString()) == true)
                    {
                        DialogResult res = MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.OKCancel);
                        if (res == DialogResult.OK)
                        {


                            DataTable dtDelete = new DataTable("tblAccount");
                            Database.GetSqlData("select * from tblAccount where Ac_id=" + ansGridView5.Rows[ansGridView5.SelectedCells[0].RowIndex].Cells["Ac_id"].Value, dtDelete);
                            dtDelete.Rows[0].Delete();
                            Database.SaveData(dtDelete);
                            MessageBox.Show("Deleted successfully");
                            LoadData(gstr, "Inward");
                        }
                    }
                    else { MessageBox.Show("Account can not be Deleted,Used in Voucher"); }
                    //LoadData(gstr, "Account");
                }
            }



            if (gstr == "Item")
            {

                if (ansGridView5.CurrentCell.OwningColumn.Name == "Edit")
                {
                    if (ansGridView5.Rows[ansGridView5.CurrentRow.Index].Cells["item_id"].Value.ToString() == "0")
                    {
                        return;
                    }
                    int grp_id = int.Parse(ansGridView5.Rows[ansGridView5.SelectedCells[0].RowIndex].Cells["Item_id"].Value.ToString());
                    if (ansGridView5.CurrentCell.OwningColumn.Name == "Edit")
                    {
                        frm_Item frm = new frm_Item();
                        frm.LoadData(grp_id.ToString(), "Edit Product Group");
                        frm.MdiParent = this.MdiParent;
                        frm.Show();
                    }

                }
                else if (ansGridView5.CurrentCell.OwningColumn.Name == "Delete")
                {
                    if (validate(ansGridView5.Rows[ansGridView5.CurrentRow.Index].Cells["Item_id"].Value.ToString()) == true)
                    {
                        DialogResult res = MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.OKCancel);
                        if (res == DialogResult.OK)
                        {

                            DataTable dtDelete = new DataTable("tblIteminfo");
                            Database.GetSqlData("select * from tblIteminfo where item_id=" + ansGridView5.Rows[ansGridView5.SelectedCells[0].RowIndex].Cells["item_id"].Value, dtDelete);
                            dtDelete.Rows[0].Delete();
                            Database.SaveData(dtDelete);
                            MessageBox.Show("Deleted successfully");
                            LoadData(gstr, "Product Group");
                        }
                    }
                    else { MessageBox.Show("Item can not be Deleted,Item Used in Voucher"); }
                    //LoadData(gstr, "Product Group");
                }
            }
            if (gstr == "Inward")
            {

                if (ansGridView5.CurrentCell.OwningColumn.Name == "Edit")
                {
                    if (ansGridView5.Rows[ansGridView5.CurrentRow.Index].Cells["vi_id"].Value.ToString() == "0")
                    {
                        return;
                    }
                    int vi_id = int.Parse(ansGridView5.Rows[ansGridView5.SelectedCells[0].RowIndex].Cells["vi_id"].Value.ToString());
                    if (ansGridView5.CurrentCell.OwningColumn.Name == "Edit")
                    {
                        frm_voucher frm = new frm_voucher();
                        frm.LoadData("Inward", vi_id.ToString(), "Edit Inward");
                        frm.MdiParent = this.MdiParent;
                        frm.Show();
                    }

                }
                else if (ansGridView5.CurrentCell.OwningColumn.Name == "Delete")
                {

                    DialogResult res = MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.OKCancel);
                    if (res == DialogResult.OK)
                    {
                        int vi_id = int.Parse(ansGridView5.Rows[ansGridView5.SelectedCells[0].RowIndex].Cells["vi_id"].Value.ToString());


                        if (validate(vi_id.ToString()) == true)
                        {
                            try
                            {
                                Database.BeginTran();


                                DataTable dtDelete = new DataTable("tblvoucherinfo");
                                Database.GetSqlData("select * from tblvoucherinfo where vi_id=" + ansGridView5.Rows[ansGridView5.SelectedCells[0].RowIndex].Cells["vi_id"].Value, dtDelete);
                                if (dtDelete.Rows.Count > 0)
                                {

                                    dtDelete.Rows[0].Delete();
                                    Database.SaveData(dtDelete);

                                }

                                DataTable dtDelete1 = new DataTable("tblvoucherDet");
                                Database.GetSqlData("select * from tblvoucherdet where vi_id=" + ansGridView5.Rows[ansGridView5.SelectedCells[0].RowIndex].Cells["vi_id"].Value, dtDelete1);
                                for (int i = 0; i < dtDelete1.Rows.Count; i++)
                                {

                                    dtDelete1.Rows[i].Delete();

                                }
                                Database.SaveData(dtDelete1);
                                dtDelete1 = new DataTable("tblstock");
                                Database.GetSqlData("select * from tblstock where vid=" + ansGridView5.Rows[ansGridView5.SelectedCells[0].RowIndex].Cells["vi_id"].Value, dtDelete1);
                                for (int i = 0; i < dtDelete1.Rows.Count; i++)
                                {

                                    dtDelete1.Rows[i].Delete();
                                }

                                Database.SaveData(dtDelete1);

                                MessageBox.Show("Deleted successfully");
                                Database.CommitTran();
                            }

                            catch (Exception ex)
                            {
                                Database.RollbackTran();
                            }
                            LoadData(gstr, "Inward");
                        }
                    }


                }
            }
            if (gstr == "Outward")
            {

                if (ansGridView5.CurrentCell.OwningColumn.Name == "Edit")
                {
                    if (ansGridView5.Rows[ansGridView5.CurrentRow.Index].Cells["vi_id"].Value.ToString() == "0")
                    {
                        return;
                    }
                    int vi_id = int.Parse(ansGridView5.Rows[ansGridView5.SelectedCells[0].RowIndex].Cells["vi_id"].Value.ToString());
                    if (ansGridView5.CurrentCell.OwningColumn.Name == "Edit")
                    {
                        frm_voucher frm = new frm_voucher();
                        frm.LoadData("Outward", vi_id.ToString(), "Edit Outward");
                        frm.MdiParent = this.MdiParent;
                        frm.Show();
                    }

                }
                else if (ansGridView5.CurrentCell.OwningColumn.Name == "Delete")
                {

                    DialogResult res = MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.OKCancel);
                    if (res == DialogResult.OK)
                    {


                        try
                        {
                            Database.BeginTran();

                            DataTable dtDelete = new DataTable("tblvoucherinfo");
                            Database.GetSqlData("select * from tblvoucherinfo where vi_id=" + ansGridView5.Rows[ansGridView5.SelectedCells[0].RowIndex].Cells["vi_id"].Value, dtDelete);

                            if (dtDelete.Rows.Count > 0)
                            {

                                dtDelete.Rows[0].Delete();
                                Database.SaveData(dtDelete);

                            }

                            dtDelete = new DataTable("tblvoucherDet");
                            Database.GetSqlData("select * from tblvoucherdet where vi_id=" + ansGridView5.Rows[ansGridView5.SelectedCells[0].RowIndex].Cells["vi_id"].Value, dtDelete);
                            for (int i = 0; i < dtDelete.Rows.Count; i++)
                            {
                                dtDelete.Rows[i].Delete();
                            }
                            Database.SaveData(dtDelete);

                            dtDelete = new DataTable("tblstock");
                            Database.GetSqlData("select * from tblstock where vid=" + ansGridView5.Rows[ansGridView5.SelectedCells[0].RowIndex].Cells["vi_id"].Value, dtDelete);
                            for (int i = 0; i < dtDelete.Rows.Count; i++)
                            {
                                dtDelete.Rows[i].Delete();
                            }
                            Database.SaveData(dtDelete);

                            MessageBox.Show("Deleted successfully");
                            Database.CommitTran();
                        }
                        catch (Exception ex)
                        {
                            Database.RollbackTran();
                        }
                            LoadData(gstr, "Outward");
                        
                    }


                }
            }
            else if (gstr == "LoanM")
            {

                if (ansGridView5.CurrentCell.OwningColumn.Name == "Edit")
                {
                    if (ansGridView5.Rows[ansGridView5.CurrentRow.Index].Cells["vi_id"].Value.ToString() == "0")
                    {
                        return;
                    }
                    int vi_id = int.Parse(ansGridView5.Rows[ansGridView5.SelectedCells[0].RowIndex].Cells["vi_id"].Value.ToString());
                    if (ansGridView5.CurrentCell.OwningColumn.Name == "Edit")
                    {
                        frm_Loanvou frm = new frm_Loanvou();
                        frm.LoadData("LoanMemo", vi_id.ToString(), "Edit LoanMemo");
                        frm.MdiParent = this.MdiParent;
                        frm.Show();
                    }

                }
                else if (ansGridView5.CurrentCell.OwningColumn.Name == "Delete")
                {

                    DialogResult res = MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.OKCancel);
                    if (res == DialogResult.OK)
                    {
                        int vi_id = int.Parse(ansGridView5.Rows[ansGridView5.SelectedCells[0].RowIndex].Cells["vi_id"].Value.ToString());


                        if (validate(vi_id.ToString()) == true)
                        {
                            try
                            {
                                Database.BeginTran();


                                DataTable dtDelete = new DataTable("tblvoucherinfo");
                                Database.GetSqlData("select * from tblvoucherinfo where vi_id=" + ansGridView5.Rows[ansGridView5.SelectedCells[0].RowIndex].Cells["vi_id"].Value, dtDelete);
                                if (dtDelete.Rows.Count > 0)
                                {

                                    dtDelete.Rows[0].Delete();
                                    Database.SaveData(dtDelete);

                                }

                                

                                MessageBox.Show("Deleted successfully");
                                Database.CommitTran();
                            }

                            catch (Exception ex)
                            {
                                Database.RollbackTran();
                            }
                            LoadData(gstr, "Loan Advisor");
                        }
                    }


                }
            }
            else if (gstr == "LoanS")
            {

                if (ansGridView5.CurrentCell.OwningColumn.Name == "Edit")
                {
                    if (ansGridView5.Rows[ansGridView5.CurrentRow.Index].Cells["vi_id"].Value.ToString() == "0")
                    {
                        return;
                    }
                    int vi_id = int.Parse(ansGridView5.Rows[ansGridView5.SelectedCells[0].RowIndex].Cells["vi_id"].Value.ToString());
                    if (ansGridView5.CurrentCell.OwningColumn.Name == "Edit")
                    {
                        frm_Loanvou frm = new frm_Loanvou();
                        frm.LoadData("LoanSettlement", vi_id.ToString(), "Edit LoanSettlement");
                        frm.MdiParent = this.MdiParent;
                        frm.Show();
                    }

                }
                else if (ansGridView5.CurrentCell.OwningColumn.Name == "Delete")
                {

                    DialogResult res = MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.OKCancel);
                    if (res == DialogResult.OK)
                    {
                        int vi_id = int.Parse(ansGridView5.Rows[ansGridView5.SelectedCells[0].RowIndex].Cells["vi_id"].Value.ToString());


                        if (validate(vi_id.ToString()) == true)
                        {
                            try
                            {
                                Database.BeginTran();


                                DataTable dtDelete = new DataTable("tblvoucherinfo");
                                Database.GetSqlData("select * from tblvoucherinfo where vi_id=" + ansGridView5.Rows[ansGridView5.SelectedCells[0].RowIndex].Cells["vi_id"].Value, dtDelete);
                                if (dtDelete.Rows.Count > 0)
                                {

                                    dtDelete.Rows[0].Delete();
                                    Database.SaveData(dtDelete);

                                }



                                MessageBox.Show("Deleted successfully");
                                Database.CommitTran();
                            }

                            catch (Exception ex)
                            {
                                Database.RollbackTran();
                            }
                            LoadData(gstr, "LoanSettlement");
                        }
                    }


                }
            }
            else if (gstr == "OpeningStock")
            {

                if (ansGridView5.CurrentCell.OwningColumn.Name == "Edit")
                {
                    if (ansGridView5.Rows[ansGridView5.CurrentRow.Index].Cells["vi_id"].Value.ToString() == "0")
                    {
                        return;
                    }
                    int vi_id = int.Parse(ansGridView5.Rows[ansGridView5.SelectedCells[0].RowIndex].Cells["vi_id"].Value.ToString());
                    if (ansGridView5.CurrentCell.OwningColumn.Name == "Edit")
                    {
                        frm_voucher frm = new frm_voucher();
                        frm.LoadData("OpeningStock", vi_id.ToString(), "Edit Opening Stock");
                        frm.MdiParent = this.MdiParent;
                        frm.Show();
                    }

                }
                else if (ansGridView5.CurrentCell.OwningColumn.Name == "Delete")
                {

                    DialogResult res = MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.OKCancel);
                    if (res == DialogResult.OK)
                    {

                        DataTable dtDelete = new DataTable("tblvoucherinfo");
                        Database.GetSqlData("select * from tblvoucherinfo where vi_id=" + ansGridView5.Rows[ansGridView5.SelectedCells[0].RowIndex].Cells["vi_id"].Value, dtDelete);
                        if (dtDelete.Rows.Count > 0)
                        {

                            dtDelete.Rows[0].Delete();
                            Database.SaveData(dtDelete);

                        }

                        dtDelete = new DataTable("tblvoucherDet");
                        Database.GetSqlData("select * from tblvoucherdet where vi_id=" + ansGridView5.Rows[ansGridView5.SelectedCells[0].RowIndex].Cells["vi_id"].Value, dtDelete);
                        for (int i = 0; i < dtDelete.Rows.Count; i++)
                        {
                            dtDelete.Rows[i].Delete();
                        }
                        Database.SaveData(dtDelete);

                        dtDelete = new DataTable("tblstock");
                        Database.GetSqlData("select * from tblstock where vid=" + ansGridView5.Rows[ansGridView5.SelectedCells[0].RowIndex].Cells["vi_id"].Value, dtDelete);
                        for (int i = 0; i < dtDelete.Rows.Count; i++)
                        {
                            dtDelete.Rows[i].Delete();
                        }
                        Database.SaveData(dtDelete);

                        MessageBox.Show("Deleted successfully");
                        LoadData(gstr, "Opening Stock");
                    }


                }
            }

        }




        
    }
}
