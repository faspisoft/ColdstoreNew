using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using Microsoft.Office.Core;
using Excel = Microsoft.Office.Interop.Excel;


namespace Coldstore
{
    public partial class Report : Form
    {
        string item = "";
        string frmcap = "";
        DateTime interest = new DateTime();
        DateTime stdt = new DateTime();
        DateTime endt = new DateTime();
        DataTable tdt = new DataTable();
        DataTable dt = new DataTable();
        DataTable dtFinal = new DataTable();
        private System.ComponentModel.IContainer components = null;
        string sql, strCombo = "", frmptyp, DecsOfReport, str = "", AccName = "", gGodownName = "", strqyery = "", Sstr = "", Sstr1 = "", Sstr2 = "", gvtid = "",gtype="";      
        public static string Pagesize = "",frmptyp2,DecsOfReport2,str2 = "";
        public string Fld1, Fld2, Fld3, Fld4, Fld5, Fld6, Fld7, Fld8, Fld9, Fld10;  
        public Report()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (frmptyp == "Item wise")
            {
                StockReportItemLedgerDetail(dateTimePicker1.Value, dateTimePicker2.Value, textBox1.Text, "ItemWise");
            }
            else if (frmptyp == "Total Stock List")
            {
                TotalStockReg(dateTimePicker1.Value, dateTimePicker2.Value, "Total Stock List");
            }
            else if (frmptyp == "Inward Total Stock List")
            {
                InTotalStockReg(dateTimePicker1.Value, dateTimePicker2.Value, "Inward Total Stock List");
            }
            else if (frmptyp == "All Stock List")
            {
                StockReg(dateTimePicker1.Value, dateTimePicker2.Value, "All Stock List");
            }
            else if (frmptyp == "Refference No wise Register")
            {
                Reffnowise(dateTimePicker1.Value, dateTimePicker2.Value, textBox1.Text, "Refference No wise Register");
            }
            else if (frmptyp == "Refference No Summary")
            {
                ReffnoSummary(dateTimePicker1.Value, dateTimePicker2.Value, textBox1.Text, "Refference No Summary");
            }
            else if (frmptyp == "Party's Stock")
            {
                PartyStock(dateTimePicker1.Value, dateTimePicker2.Value, textBox1.Text, "Party Log Book");
            }
            else if (frmptyp == "Party's Where Goods")
            {
                PartyWhereGoods(dateTimePicker1.Value, dateTimePicker2.Value, textBox1.Text, "Party's Where Goods");
            }
            else if (frmptyp == "Party's Item Wise Register")
            {
                PartyRegisterItemWise(dateTimePicker1.Value, dateTimePicker2.Value, "Party's Item Wise Register",textBox1.Text);
            }
            else if (frmptyp == "Party's Item Wise Register All")
            {
                PartyRegisterItemWiseAll(dateTimePicker1.Value, dateTimePicker2.Value, "Party's Item Wise Register", textBox1.Text);
            }
            else if (frmptyp == "Daily Register")
            {
                DailyReg(dateTimePicker1.Value, dateTimePicker2.Value, gtype, textBox1.Text, "Daily Register");
            }
            else if (frmptyp == "Stock Register Inward")
            {
                StockRegIn(dateTimePicker1.Value, dateTimePicker2.Value, "Stock Register Inward");
            }
            else if (frmptyp == "Stock Register Outward")
            {
                StockRegOut(dateTimePicker1.Value, dateTimePicker2.Value, "Stock Register Outward");
            }
            if (dataGridView1.Rows.Count > 0)
            {
                button1.Visible = true;
                button2.Visible = true;
                button4.Visible = true;
                button6.Visible = true;
            }
            else
            {
                button1.Visible = false;
                button2.Visible = false;
                button4.Visible = false;
                button6.Visible = false;
            }
        }

        private void button6_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu cm = new ContextMenu();
                cm.MenuItems.Add("Export to Excel", new EventHandler(Item1_Click));
                cm.MenuItems.Add("Export Data Only", new EventHandler(Item2_Click));
                button6.ContextMenu = cm;
            }
        }



        void Item1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                return;
            }

            Object misValue = System.Reflection.Missing.Value;
            Excel.Application apl = new Microsoft.Office.Interop.Excel.Application();
            Excel.Workbook wb = (Excel.Workbook)apl.Workbooks.Add(misValue);
            Excel.Worksheet ws;
            ws = (Excel.Worksheet)wb.Worksheets[1];


            int lno = 1;
            DataTable dtExcel = new DataTable();
            DataTable dtRheader = new DataTable();



            //Database.GetSqlData("select * from company", dtRheader);

            //ws.Cells[lno, 1] = dtRheader.Rows[0]["name"].ToString();
            //ws.get_Range(ws.Cells[lno, 1], ws.Cells[lno, dataGridView1.Columns.Count]).Merge(Type.Missing);
            //ws.get_Range(ws.Cells[lno, 1], ws.Cells[lno, dataGridView1.Columns.Count]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //ws.get_Range(ws.Cells[lno, 1], ws.Cells[lno, dataGridView1.Columns.Count]).Font.Bold = true;
            //lno++;

            //ws.Cells[lno, 1] = dtRheader.Rows[0]["Address1"].ToString();
            //ws.get_Range(ws.Cells[lno, 1], ws.Cells[lno, dataGridView1.Columns.Count]).Merge(Type.Missing);
            //ws.get_Range(ws.Cells[lno, 1], ws.Cells[lno, dataGridView1.Columns.Count]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //ws.get_Range(ws.Cells[lno, 1], ws.Cells[lno, dataGridView1.Columns.Count]).Font.Bold = true;
            //lno++;

            //ws.Cells[lno, 1] = dtRheader.Rows[0]["Address2"].ToString();
            //ws.get_Range(ws.Cells[lno, 1], ws.Cells[lno, dataGridView1.Columns.Count]).Merge(Type.Missing);
            //ws.get_Range(ws.Cells[lno, 1], ws.Cells[lno, dataGridView1.Columns.Count]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //ws.get_Range(ws.Cells[lno, 1], ws.Cells[lno, dataGridView1.Columns.Count]).Font.Bold = true;
            //lno++;

            //for (int i = 0; i < dataGridView1.Columns.Count; i++)
            //{
            //    if (dataGridView1.Columns[i].HeaderCell.Style.Alignment == DataGridViewContentAlignment.MiddleRight)
            //    {
            //        ws.get_Range(ws.Cells[5, i + 1], ws.Cells[5, i + 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            //    }
            //    ws.get_Range(ws.Cells[i + 1, i + 1], ws.Cells[i + 1, i + 1]).ColumnWidth = dataGridView1.Columns[i].Width / 11.5;
            //    ws.Cells[5, i + 1] = dataGridView1.Columns[i].HeaderText.ToString();
            //}

            //for (int i = 0; i < dataGridView1.Rows.Count; i++)
            //{
            //    for (int j = 0; j < dataGridView1.Columns.Count; j++)
            //    {
            //        if (dataGridView1.Columns[j].HeaderCell.Style.Alignment == DataGridViewContentAlignment.MiddleRight)
            //        {
            //            ws.get_Range(ws.Cells[i + 6, j + 1], ws.Cells[i + 6, j + 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            //            ws.get_Range(ws.Cells[i + 6, j + 1], ws.Cells[i + 6, j + 1]).NumberFormat = "0,0.00";
            //        }
            //        else
            //        {
            //            ws.get_Range(ws.Cells[i + 6, j + 1], ws.Cells[i + 6, j + 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
            //        }

            //        if (dataGridView1.Columns[j].DefaultCellStyle.Font != null)
            //        {
            //            ws.get_Range(ws.Cells[i + 6, j + 1], ws.Cells[i + 6, j + 1]).Font.Bold = true;
            //        }

            //        if (dataGridView1.Rows[i].Cells[j].Value != null)
            //        {
            //            ws.Cells[i + 6, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString().Replace(",", "");
            //        }
            //    }
            //}


            var data = new object[dataGridView1.Rows.Count, dataGridView1.Columns.Count];
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                    {
                        data[i, j] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }
                }
            }


            var startcell = (Excel.Range)ws.Cells[6, 1];
            var endcell = (Excel.Range)ws.Cells[dataGridView1.Rows.Count + 5, dataGridView1.Columns.Count];
            var writerange = ws.Range[startcell, endcell];
            writerange.Value = data;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {

                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {

                    if (dataGridView1.Columns[j].ToString().IndexOf("date") > -1)
                    {

                        if (dataGridView1.Rows[i].Cells[j].Value != null)
                        {
                            ws.Cells[i + 6, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString().Replace(",", "");
                        }
                    }


                }



            }

            Excel.Range last = ws.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
            ws.get_Range("A1", last).WrapText = true;
            apl.Visible = true;
        }

        void Item2_Click(object sender, EventArgs e)
        {
            if (dtFinal.Rows.Count == 0)
            {
                return;
            }
            Object misValue = System.Reflection.Missing.Value;
            Excel.Application apl = new Microsoft.Office.Interop.Excel.Application();
            Excel.Workbook wb = (Excel.Workbook)apl.Workbooks.Add(misValue);
            Excel.Worksheet ws;
            ws = (Excel.Worksheet)wb.Worksheets[1];
            DataTable dtExcel = new DataTable();

            //for (int i = 0; i < dtFinal.Columns.Count; i++)
            //{
            //    ws.Cells[1, i + 1] = dtFinal.Columns[i].ToString();
            //}

            //for (int i = 0; i < dtFinal.Rows.Count; i++)
            //{
            //    for (int j = 0; j < dtFinal.Columns.Count; j++)
            //    {
            //        ws.get_Range(ws.Cells[i + 2, j + 1], ws.Cells[i + 2, j + 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;

            //        if (dtFinal.Columns[j].DataType.Name == "DateTime" && DateTime.Parse(dtFinal.Rows[i][j].ToString()).ToString("yyyy") == "1801")
            //        {
            //            ws.Cells[i + 2, j + 1] = "";
            //        }
            //        else if (dtFinal.Columns[j].DataType.Name == "DateTime")
            //        {
            //            DateTime datet = new DateTime();
            //            datet = DateTime.Parse(dtFinal.Rows[i][j].ToString());
            //            ws.Cells[i + 2, j + 1] = datet.ToString("dd-MMM-yyyy");
            //        }
            //        else if (dtFinal.Columns[j].DataType.Name == "Int32")
            //        {
            //            ws.Cells[i + 2, j + 1] = funs1.IndianCurr(double.Parse(dtFinal.Rows[i][j].ToString()));
            //        }
            //        else if (dtFinal.Columns[j].DataType.Name == "Double")
            //        {
            //            ws.Cells[i + 2, j + 1] = funs1.IndianCurr(double.Parse(dtFinal.Rows[i][j].ToString()));
            //        }
            //        else if (dtFinal.Columns[j].DataType.Name == "Decimal")
            //        {
            //            ws.Cells[i + 2, j + 1] = funs1.IndianCurr(double.Parse(dtFinal.Rows[i][j].ToString()));
            //        }

            //        else
            //        {
            //            ws.Cells[i + 2, j + 1] = dtFinal.Rows[i][j].ToString().Replace(",", "");
            //        }
            //    }
            //}

            int lno = 1;
           
          

            for (int i = 0; i < dtFinal.Columns.Count; i++)
            {

                ws.get_Range(ws.Cells[lno, 1], ws.Cells[lno, dtFinal.Columns.Count]).Font.Bold = true;
                //if (dataGridView1.Columns[i].HeaderCell.Style.Alignment == DataGridViewContentAlignment.MiddleRight)
                //{
                //    ws.get_Range(ws.Cells[lno, i + 1], ws.Cells[lno, i + 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                //}
               // ws.get_Range(ws.Cells[lno,  1], ws.Cells[lno, i+1]).ColumnWidth = dataGridView1.Columns[i].Width / 11.5;
                ws.Cells[lno, i+1] = dtFinal.Columns[i].ToString();

            }


            lno++;

            var data = new object[dtFinal.Rows.Count, dtFinal.Columns.Count];



            for (int i = 0; i < dtFinal.Rows.Count; i++)
            {
                
                for (int j = 0; j < dtFinal.Columns.Count; j++)
                {
                   
                    if (dtFinal.Rows[i][j] != null || dtFinal.Rows[i][j].ToString() != "")
                    {


                        data[i, j] = dtFinal.Rows[i][j].ToString().Replace(",", "");
                          //  data[i, j] = dtFinal.Rows[i][j].ToString();
                       

                    }
                    if (dtFinal.Rows[i][j].ToString().IndexOf("<b>") > -1)
                    {
                        data[i, j] = dtFinal.Rows[i][j].ToString().Replace("<b>", "");
                        
                    }
                }
                 

              
         }

           

            var startcell = (Excel.Range)ws.Cells[2, 1];
            var endcell = (Excel.Range)ws.Cells[dtFinal.Rows.Count + 2, dtFinal.Columns.Count];
            var writerange = ws.Range[startcell, endcell];
            writerange.Value = data;
            for (int i = 0; i < dtFinal.Rows.Count; i++)
            {

                for (int j = 0; j < dtFinal.Columns.Count; j++)
                {
                    if (dtFinal.Rows[i][j] != null || dtFinal.Rows[i][j].ToString() != "")
                    {
                        if (dtFinal.Columns[j].ToString().IndexOf("date") > -1)
                        {

                            ws.Cells[i + 2, j + 1] = DateTime.Parse(dtFinal.Rows[i][j].ToString().Replace(",", "")).ToString("dd-MMM-yyyy");
                           // data[i, j] = DateTime.Parse(dtFinal.Rows[i][j].ToString()).ToString("dd-MMM-yyyy");

                        }

                    }
                }
            }



            Excel.Range last = ws.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
            ws.get_Range("A1", last).WrapText = true;
            apl.Visible = true;
        }

        public bool DailyReg(DateTime DateFrom, DateTime DateTo, string type, string accountname, string frmcaption)
        {
            label3.Text = "Party Name";
            frmcap = frmcaption;
            stdt = DateFrom;
            endt = DateTo;
            gtype = type;
            frmptyp = "Daily Register";
            dateTimePicker1.Value = DateFrom;
            dateTimePicker2.Value = DateTo;
            textBox1.Text = accountname;
           // dateTimePicker1.Enabled = false;
            //dateTimePicker2.Enabled = false;
            this.Text = frmptyp;
            DecsOfReport = "Daily Register, for the period of " + DateFrom.ToString(Database.dformat) + " to " + DateTo.ToString(Database.dformat);
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dtFinal = new DataTable();

            sql = "TRANSFORM Sum(tblVoucherDet.Quantity) AS Quantity SELECT tblVoucherinfo.Vdate, tblVoucherinfo.vnumber & '' as vnumber, TblVoucherType.Vname, tblAccount.Acc_name, tblVoucherDet.Marka, tblVoucherDet.roomno, tblVoucherDet.slapno , tblVoucherDet.section, tblVoucherDet.remark FROM (((tblVoucherinfo LEFT JOIN tblVoucherDet ON tblVoucherinfo.Vi_id = tblVoucherDet.Vi_id) LEFT JOIN TblVoucherType ON tblVoucherinfo.Vt_id = TblVoucherType.Vt_id) LEFT JOIN tblItemInfo ON tblVoucherDet.Item_id = tblItemInfo.Item_id) LEFT JOIN tblAccount ON tblVoucherinfo.Ac_id = tblAccount.Ac_id WHERE (((tblVoucherinfo.Vdate)>=#" + DateFrom.ToString(Database.dformat) + "# And (tblVoucherinfo.Vdate)<=#" + DateTo.ToString(Database.dformat) + "#)) GROUP BY TblVoucherType.Vname, tblVoucherinfo.Vdate, tblVoucherinfo.vnumber, tblAccount.Acc_name, tblVoucherDet.Marka, tblVoucherDet.roomno, tblVoucherDet.slapno, tblVoucherDet.section, tblVoucherDet.remark PIVOT tblItemInfo.Item_name;";
            dt = new DataTable();

            tdt = new DataTable();



            Database.GetSqlData(sql, dt);


            DataRow[] drow;
            drow = dt.Select("Acc_Name is not null and Vname is not null");
            if (accountname != "" && gtype != "Both")
            {
                drow = dt.Select("acc_name ='" + accountname + "' and vname='" + gtype + "'");
            }
            else if (accountname == "" && gtype != "Both")
            {
                drow = dt.Select("acc_name is not null and vname='" + gtype + "'");
            }

            else if (accountname != "" && gtype == "Both")
            {
                drow = dt.Select("acc_name ='" + accountname + "' and vname is not null");
            }
            else if (accountname == "" && gtype == "Both")
            {
                drow = dt.Select("acc_name is not null and vname is not null");
            }
            if (drow.GetLength(0) > 0)
            {
                tdt = drow.CopyToDataTable();
                tdt.DefaultView.Sort = "vdate,vnumber";
                tdt.DefaultView.ToTable();

            }
            if (tdt.Rows.Count == 0)
            {
                return false;

            }


            tdt.Columns.Add("Total", typeof(int));
            for (int i = 0; i < tdt.Rows.Count; i++)
            {

                int total = 0;
             
                for (int j = 9; j < tdt.Columns.Count - 1; j++)
                {
                    if (tdt.Rows[i][j] == null || tdt.Rows[i][j].ToString() == "")
                    {
                        tdt.Rows[i][j] = 0;
                    }
                    total += int.Parse(tdt.Rows[i][j].ToString());
                }
                tdt.Rows[i]["total"] = total;
            }
            string[,] col = new string[1, 3]{
             { "vdate", "1", "1" }
            
           
            };


            string[,] Cwidth = new string[tdt.Columns.Count, 6];
            for (int i = 0; i < tdt.Columns.Count; i++)
            {
                Cwidth[i, 0] = tdt.Columns[i].ColumnName;
                if (i == 0)
                {
                    Cwidth[i, 1] = "100";
                    Cwidth[i, 2] = "0";
                    Cwidth[i, 3] = "";
                    Cwidth[i, 4] = "";
                    Cwidth[i, 5] = "";
                   
                    
                }
                else if (i == 1)
                {
                    Cwidth[i, 0] = "Gate Pass";
                    Cwidth[i, 1] = "100";
                    Cwidth[i, 2] = "0";
                    Cwidth[i, 3] = "";
                    Cwidth[i, 4] = "";
                    Cwidth[i, 5] = "";
                    
                }
                else if (i == 2)
                {
                    Cwidth[i, 1] = "100";
                    Cwidth[i, 2] = "0";
                    Cwidth[i, 3] = "";
                    Cwidth[i, 4] = "";
                    Cwidth[i, 5] = "";
                   
                }
                else if (i == 3)
                {
                    Cwidth[i, 0] = "Party Name";
                    Cwidth[i, 1] = "150";
                    Cwidth[i, 2] = "0";
                    Cwidth[i, 3] = "";
                    Cwidth[i, 4] = "";
                    Cwidth[i, 5] = "";
                    
                }
                else if (i == 4)
                {
                    Cwidth[i, 1] = "100";
                    Cwidth[i, 2] = "0";
                    Cwidth[i, 3] = "";
                    Cwidth[i, 4] = "";
                    Cwidth[i, 5] = "";
                  
                }
                else if (i == 5)
                {
                    Cwidth[i, 0] = "Room No";
                    Cwidth[i, 1] = "100";
                    Cwidth[i, 2] = "0";
                    Cwidth[i, 3] = "";
                    Cwidth[i, 4] = "";
                    Cwidth[i, 5] = "";
                   
                }
                else if (i == 6)
                {
                    Cwidth[i, 0] = "Slab No";
                    Cwidth[i, 1] = "100";
                    Cwidth[i, 2] = "0";
                    Cwidth[i, 3] = "";
                    Cwidth[i, 4] = "";
                    Cwidth[i, 5] = "";
                 
                }
                else if (i == 7)
                {
                    Cwidth[i, 0] = "Section";
                    Cwidth[i, 1] = "100";
                    Cwidth[i, 2] = "0";
                    Cwidth[i, 3] = "Total";
                    Cwidth[i, 4] = "Day Total";
                    Cwidth[i, 5] = "";
                   
                }
                else if (i == 8)
                {
                    Cwidth[i, 0] = "Remark";
                    Cwidth[i, 1] = "100";
                    Cwidth[i, 2] = "0";
                    Cwidth[i, 3] = "";
                    Cwidth[i, 4] = "";
                    Cwidth[i, 5] = "";
                    
                }
                else
                {
                    Cwidth[i, 1] = (850 / (tdt.Columns.Count - 8)).ToString();
                    //if (i == dt.Columns.Count)
                    //{
                    //    Cwidth[i, 3] = "";
                    //    Cwidth[i, 4] = "";
                    //}
                    //else
                    //{
                        Cwidth[i, 3] = "|sum([" + tdt.Columns[i].ColumnName + "])";
                        Cwidth[i, 4] = "|sum([" + tdt.Columns[i].ColumnName + "])";
                    //}
                    Cwidth[i, 5] = "";
                   
                    Cwidth[i, 2] = "1";
                }
            }
            CreateReport(tdt, col, Cwidth);
            return true;

        }


        public bool DailyRegold(DateTime DateFrom, DateTime DateTo, string type, string accountname, string frmcaption)
        {
            frmcap = frmcaption;
            stdt = DateFrom;
            endt = DateTo;
            gtype = type;
            frmptyp = "Daily Register";
            dateTimePicker1.Value = DateFrom;
            dateTimePicker2.Value = DateTo;
            textBox1.Text = accountname;
            label3.Enabled = false;
            textBox1.Enabled = false;
            this.Text = frmptyp;
            DecsOfReport = "Daily Register, for the period of " + DateFrom.ToString(Database.dformat) + " to " + DateTo.ToString(Database.dformat);
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dtFinal = new DataTable();

            sql = "SELECT tblVoucherinfo.Vdate, tblVoucherinfo.vnumber & '' as vnumber, TblVoucherType.Vname, tblAccount.Acc_name, tblItemInfo.Item_name,  tblVoucherDet.Marka, tblVoucherDet.roomno, tblVoucherDet.slapno, tblVoucherDet.[section] as [Sec], tblVoucherDet.remark,tblVoucherDet.Quantity FROM (((tblVoucherinfo LEFT JOIN tblVoucherDet ON tblVoucherinfo.Vi_id = tblVoucherDet.Vi_id) LEFT JOIN TblVoucherType ON tblVoucherinfo.Vt_id = TblVoucherType.Vt_id) LEFT JOIN tblItemInfo ON tblVoucherDet.Item_id = tblItemInfo.Item_id) LEFT JOIN tblAccount ON tblVoucherinfo.Ac_id = tblAccount.Ac_id WHERE (((tblVoucherinfo.Vdate)>=#" + DateFrom.ToString(Database.dformat) + "# And (tblVoucherinfo.Vdate)<=#" + DateTo.ToString(Database.dformat) + "#)) ORDER BY tblVoucherinfo.Vdate, tblVoucherinfo.vnumber;";
            dt = new DataTable();



            Database.GetSqlData(sql, dt);



          

            DataRow[] drow;
            drow = dt.Select("Acc_Name is not null and Vname ='Both'");
            if (accountname != "" && gtype != "Both")
            {
                drow = dt.Select("acc_name ='" + accountname + "' and vname='" + gtype + "'");
            }
            else if (accountname == "" && gtype != "Both")
            {
                drow = dt.Select("acc_name is not null and vname='" + gtype + "'");
            }

            else if (accountname != "" && gtype == "Both")
            {
                drow = dt.Select("acc_name ='" + accountname + "' and vname is not null");
            }

            if (drow.GetLength(0) > 0)
            {
                tdt = drow.CopyToDataTable();
                tdt.DefaultView.Sort = "vdate,vnumber";
                tdt.DefaultView.ToTable();

            }
            if (tdt.Rows.Count == 0)
            {
                return false;
            }
            string[,] col = new string[1, 3]{
             { "vdate", "1", "1" }
            
           
            };


            string[,] Cwidth = new string[11, 8] { 
            { "Vdate", "", "0" ,"","","","",""},
            { "Gate Pass", "100", "0" ,"","","","",""},
            { "Vname", "100", "0" ,"","","","",""},
             { "PartyName", "150", "0" ,"","","","",""},
             { "ItemName", "100", "0" ,"","","","",""},
            
            { "Marka", "100", "0" ,"","","","",""},
            { "Room No", "75", "0" ,"Total","Day Total","","",""},
             { "Slap No", "75", "0" ,"","","","",""},
              { "Section", "100", "0" ,"","","","",""},
               { "Remark", "100", "0" ,"","","","",""},
                { "Quantity", "100", "0" ,"|sum(Quantity)","|sum(Quantity)","","",""},
            };


            CreateReport(tdt, col, Cwidth);
            return true;

        }


        public bool StockReportMarkaWise(DateTime DateFrom, DateTime DateTo, string accountname, string marka,string frmcaption)
        {
            frmcap = frmcaption;
            stdt = DateFrom;
            endt = DateTo;
            groupBox2.Visible = false;
            frmptyp = "Stock Register";
            dateTimePicker1.Value = DateFrom;
            dateTimePicker2.Value = DateTo;
            textBox1.Text = accountname;
            label3.Enabled = false;
            textBox1.Enabled = false;
            this.Text = frmptyp;
            DecsOfReport = "Stock Register, for the period of " + DateFrom.ToString(Database.dformat) + " to " + DateTo.ToString(Database.dformat);
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dtFinal = new DataTable();
           // sql = "SELECT tblAccount.Acc_name, tblItemInfo.Item_name, res.Marka, Sum(res.opn) AS SumOfopn, Sum(res.inw) AS SumOfinw, 0 AS totinw, Sum(res.Out) AS totOut, 0 AS stock FROM ((SELECT tblVoucherinfo.Vdate, tblVoucherinfo.Ac_id, tblstock.Item_id, tblstock.Marka, tblstock.Quantity AS opn, 0 AS inw, 0 AS Out FROM tblstock LEFT JOIN tblVoucherinfo ON tblstock.Vid = tblVoucherinfo.Vi_id WHERE (((tblVoucherinfo.Vdate)<#" + DateFrom.ToString(Database.dformat) + "#)) Union all SELECT tblVoucherinfo.Vdate, tblVoucherinfo.Ac_id, tblstock.Item_id, tblstock.Marka, 0 AS opn, Sum(IIf(tblstock.Quantity>0,tblstock.Quantity,0)) AS inw, Sum(IIf(tblstock.Quantity<0,-1*tblstock.Quantity,0)) AS Out FROM tblstock LEFT JOIN tblVoucherinfo ON tblstock.Vid = tblVoucherinfo.Vi_id GROUP BY tblVoucherinfo.Vdate, tblVoucherinfo.Ac_id, tblstock.Item_id, tblstock.Marka, 0 HAVING (((tblVoucherinfo.Vdate)>=#" + DateFrom.ToString(Database.dformat) + "# And (tblVoucherinfo.Vdate)<=#" + DateTo.ToString(Database.dformat) + "#)))  AS res LEFT JOIN tblAccount ON res.Ac_id = tblAccount.Ac_id) LEFT JOIN tblItemInfo ON res.Item_id = tblItemInfo.Item_id GROUP BY tblAccount.Acc_name, tblItemInfo.Item_name, res.Marka ORDER BY tblAccount.Acc_name, tblItemInfo.Item_name;";
            sql = "SELECT tblAccount.Acc_name, tblItemInfo.Item_name, res.Reffno &'' as Reffno , iif(res.Marka is null,'<Undefined>',res.Marka) as Marka, Sum(res.opn) AS SumOfopn, Sum(res.inw) AS SumOfinw, 0 AS totinw, Sum(res.Out) AS totOut, 0 AS stock FROM ((SELECT tblVoucherinfo.Vdate, tblVoucherinfo.Ac_id, tblstock.Item_id, tblstock.reffno AS Reffno, tblstock.Marka, tblstock.Quantity AS opn, 0 AS inw, 0 AS Out FROM tblstock LEFT JOIN tblVoucherinfo ON tblstock.Vid = tblVoucherinfo.Vi_id WHERE (((tblVoucherinfo.Vdate)<#" + DateFrom.ToString(Database.dformat) + "#)) Union all SELECT tblVoucherinfo.Vdate, tblVoucherinfo.Ac_id, tblstock.Item_id, tblstock.reffno, tblstock.Marka, 0 AS opn, Sum(IIf(tblstock.Quantity>0,tblstock.Quantity,0)) AS inw, Sum(IIf(tblstock.Quantity<0,-1*tblstock.Quantity,0)) AS Out";
            sql += " FROM tblstock LEFT JOIN tblVoucherinfo ON tblstock.Vid = tblVoucherinfo.Vi_id GROUP BY tblVoucherinfo.Vdate, tblVoucherinfo.Ac_id, tblstock.Item_id, tblstock.reffno, tblstock.Marka, 0 HAVING (((tblVoucherinfo.Vdate)>=#" + DateFrom.ToString(Database.dformat) + "# And (tblVoucherinfo.Vdate)<=#" + DateTo.ToString(Database.dformat) + "#)))  AS res LEFT JOIN tblAccount ON res.Ac_id = tblAccount.Ac_id) LEFT JOIN tblItemInfo ON res.Item_id = tblItemInfo.Item_id GROUP BY tblAccount.Acc_name, tblItemInfo.Item_name, res.Reffno, iif(res.Marka is null,'<Undefined>',res.Marka)  ORDER BY tblAccount.Acc_name, tblItemInfo.Item_name";
            dt = new DataTable();



            Database.GetSqlData(sql, dt);



            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["totinw"] = double.Parse(dt.Rows[i]["Sumofopn"].ToString()) + double.Parse(dt.Rows[i]["Sumofinw"].ToString());
                dt.Rows[i]["stock"] = double.Parse(dt.Rows[i]["totinw"].ToString()) - double.Parse(dt.Rows[i]["totout"].ToString());
            }

            DataRow[] drow;
            drow = dt.Select("acc_name is not null and marka is not null");
            if (accountname != "" && marka != "")
            {
                drow = dt.Select("acc_name ='" + accountname + "' and marka='" + marka + "'");
            }
            else if (accountname == "" && marka != "")
            {
                drow = dt.Select("acc_name is not null and marka='" + marka + "'");
            }

            else if (accountname != "" && marka == "")
            {
                drow = dt.Select("acc_name ='" + accountname + "' and marka is not null");
            }
          
            if (drow.GetLength(0) > 0)
            {
                tdt = drow.CopyToDataTable();
                tdt.DefaultView.Sort = "acc_name,Item_Name";
                tdt.DefaultView.ToTable();

            }
            if (tdt.Rows.Count == 0)
            {
                return false;
            }
                string[,] col = new string[2, 3]{
             { "Acc_Name", "1", "0" },
             { "Item_name", "1", "1" }
           
            };


                string[,] Cwidth = new string[10, 8] { 
            { "PartyName", "", "0" ,"","","","",""},
            { "ItemName", "0", "0" ,"","","","",""},
            { "Reffno", "325", "0" ,"","","","",""},
            { "Marka", "100", "0" ,"","","","",""},
            { "Opening", "100", "0" ,"|sum(Sumofopn)","","|sum(Sumofopn)","",""},
            { "Inward", "100", "0" ,"|sum(Sumofinw)","","|sum(Sumofinw)","",""},
            { "Total Inward", "125", "0" ,"|sum(totinw)","","|sum(totinw)","",""},
            { "Outward", "100", "0" ,"|sum(totout)","","|sum(totout)","",""},
            { "Stock", "150", "0" ,"|sum(stock)","","|sum(stock)","",""},
            { "Itemid", "0", "0" ,"","","","",""},
            };

                CreateReport(tdt, col, Cwidth);
                return true;
             
        }
        public bool StockReportItemWise(DateTime DateFrom, DateTime DateTo, string accountname, string Itemname,string frmcaption)
        {
            frmcap = frmcaption;
            stdt = DateFrom;
            endt = DateTo;
            groupBox2.Visible = false;
            frmptyp = "Stock Register";
            dateTimePicker1.Value = DateFrom;
            dateTimePicker2.Value = DateTo;
            textBox1.Text = accountname;
            label3.Enabled = false;
            textBox1.Enabled = false;
            this.Text = frmptyp;
            DecsOfReport = "Stock Register, for the period of " + DateFrom.ToString(Database.dformat) + " to " + DateTo.ToString(Database.dformat);
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dtFinal = new DataTable();
            sql = "SELECT tblAccount.Acc_name, tblItemInfo.Item_name, Sum(res.opn) AS SumOfopn, Sum(res.inw) AS SumOfinw, 0 AS totinw, Sum(res.Out) AS totOut, 0 AS stock FROM ((SELECT tblVoucherinfo.Vdate, tblVoucherinfo.Ac_id, tblstock.Item_id, tblstock.Marka, tblstock.Quantity AS opn, 0 AS inw, 0 AS Out FROM tblstock LEFT JOIN tblVoucherinfo ON tblstock.Vid = tblVoucherinfo.Vi_id WHERE (((tblVoucherinfo.Vdate)<#" + DateFrom.ToString(Database.dformat) + "#)) Union all SELECT tblVoucherinfo.Vdate, tblVoucherinfo.Ac_id, tblstock.Item_id, tblstock.Marka, 0 AS opn, Sum(IIf(tblstock.Quantity>0,tblstock.Quantity,0)) AS inw, Sum(IIf(tblstock.Quantity<0,-1*tblstock.Quantity,0)) AS Out FROM tblstock LEFT JOIN tblVoucherinfo ON tblstock.Vid = tblVoucherinfo.Vi_id GROUP BY tblVoucherinfo.Vdate, tblVoucherinfo.Ac_id, tblstock.Item_id, tblstock.Marka, 0 HAVING (((tblVoucherinfo.Vdate)>=#" + DateFrom.ToString(Database.dformat) + "# And (tblVoucherinfo.Vdate)<=#" + DateTo.ToString(Database.dformat) + "#)))  AS res LEFT JOIN tblAccount ON res.Ac_id = tblAccount.Ac_id) LEFT JOIN tblItemInfo ON res.Item_id = tblItemInfo.Item_id GROUP BY tblAccount.Acc_name, tblItemInfo.Item_name, res.Marka ORDER BY tblAccount.Acc_name, tblItemInfo.Item_name;";
            dt = new DataTable();



            Database.GetSqlData(sql, dt);



            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["totinw"] = double.Parse(dt.Rows[i]["Sumofopn"].ToString()) + double.Parse(dt.Rows[i]["Sumofinw"].ToString());
                dt.Rows[i]["stock"] = double.Parse(dt.Rows[i]["totinw"].ToString()) - double.Parse(dt.Rows[i]["totout"].ToString());
            }

            DataRow[] drow;
            drow = dt.Select("acc_name is not null and Item_name is not null");
            if (accountname != "" && Itemname != "")
            {
                drow = dt.Select("acc_name ='" + accountname + "' and Item_name='" + Itemname + "'");
            }
            else if (accountname == "" && Itemname != "")
            {
                drow = dt.Select("acc_name is not null and Item_name='" + Itemname + "'");
            }

            else if (accountname != "" && Itemname == "")
            {
                drow = dt.Select("acc_name ='" + accountname + "' and Item_name is not null");
            }

            if (drow.GetLength(0) > 0)
            {
                tdt = drow.CopyToDataTable();
                tdt.DefaultView.Sort = "acc_name,Item_Name";
                tdt.DefaultView.ToTable();

            }
            if (tdt.Rows.Count == 0)
            {
                return false;
            }
            string[,] col = new string[1, 3]{
            { "Acc_Name", "1", "0" }
           
            };


            string[,] Cwidth = new string[8, 8] { 
            { "PartyName", "", "0" ,"","","","",""},
            { "ItemName", "200", "0" ,"","","","",""},
            { "Opening", "175", "0" ,"|sum(Sumofopn)","","","",""},
            { "Inward", "175", "0" ,"|sum(Sumofinw)","","","",""},
            { "Total Inward", "150", "0" ,"|sum(totinw)","","","",""},
            { "Outward", "150", "0" ,"|sum(totout)","","","",""},
            { "Stock", "150", "0" ,"|sum(stock)","","","",""},
            { "Itemud", "0", "0" ,"","","","",""},
            };

            CreateReport(tdt, col, Cwidth);
            return true;

        }
        public bool StockReportItemLedger(DateTime DateFrom, DateTime DateTo, string itemname,string frmcaption)
        {
            item = itemname;
            frmcap = frmcaption;
            stdt = DateFrom;
            endt = DateTo;
            textBox1.Text = itemname;
            groupBox2.Visible = false;
            frmptyp = "Stock Register";
            dateTimePicker1.Value = DateFrom;
            dateTimePicker2.Value = DateTo;
            textBox1.Text = itemname;
            label3.Enabled = false;
            textBox1.Enabled = false;
            this.Text = frmptyp;
            int item_id = funs11.Select_Item_id(itemname);
            DecsOfReport = "Item Ladger, for the period of " + DateFrom.ToString(Database.dformat) + " to " + DateTo.ToString(Database.dformat);
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dtFinal = new DataTable();
            sql = "SELECT  tblItemInfo.Item_name, tblstock.Marka, 'Opening' AS type, Sum(IIf(tblstock.Quantity>0,tblstock.Quantity,0)) AS Rec, Sum(IIf(tblstock.Quantity<0,-1*tblstock.Quantity,0)) AS Isu FROM (tblstock LEFT JOIN tblVoucherinfo ON tblstock.Vid = tblVoucherinfo.Vi_id) LEFT JOIN tblItemInfo ON tblstock.Item_id = tblItemInfo.Item_id GROUP BY tblItemInfo.Item_name, tblstock.Marka, 'Opening', tblVoucherinfo.Vdate, tblstock.Item_id HAVING (((tblVoucherinfo.Vdate)<#"+DateFrom.ToString(Database.dformat)+"#) AND ((tblstock.Item_id)="+item_id+")) UNION ALL SELECT tblItemInfo.Item_name, tblstock.Marka, TblVoucherType.Vname AS Type, Sum(IIf(tblstock.Quantity>0,tblstock.Quantity,0)) AS Rec, Sum(IIf(tblstock.Quantity<0,-1*tblstock.Quantity,0)) AS Isu FROM ((tblstock LEFT JOIN tblVoucherinfo ON tblstock.Vid = tblVoucherinfo.Vi_id) LEFT JOIN TblVoucherType ON tblVoucherinfo.Vt_id = TblVoucherType.Vt_id) LEFT JOIN tblItemInfo ON tblstock.Item_id = tblItemInfo.Item_id GROUP BY tblItemInfo.Item_name, tblstock.Marka, TblVoucherType.Vname, tblVoucherinfo.Vdate, tblstock.Item_id HAVING (((tblVoucherinfo.Vdate)>=#"+DateFrom.ToString(Database.dformat)+"# And (tblVoucherinfo.Vdate)<=#"+DateTo.ToString(Database.dformat)+"#) AND ((tblstock.Item_id)="+item_id+"));";

            dt = new DataTable();


            Database.GetSqlData(sql, dt);
            if (dt.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                dt.DefaultView.Sort = "Item_name,Marka";
                dt.DefaultView.ToTable();
            }



            string[,] col = new string[2, 3]{
            { "item_Name", "1", "1" },
            { "marka", "1", "1" }
            };



            string[,] Cwidth = new string[5, 8] { 
            { "Item Name", "100", "0","","","","","" },
            { "Marka", "100", "0" ,"","","","",""},
            { "Type", "400", "0" ,"Total","","","",""},
            { "Receive", "300", "0","|sum(rec)","","|sum(rec)","",""},
            { "Issue", "300", "0" ,"|sum(Isu)","","|sum(Isu)","",""},
            //{ "Stock", "100", "0" ,"","","","",""},
           
            };

            CreateReport(dt, col, Cwidth);
            return true;

        }

    

        public bool StockReportItemLedgerDetail(DateTime DateFrom, DateTime DateTo, string itemname,string frmcaption)
        {
            frmcap = frmcaption;
            stdt = DateFrom;
            endt = DateTo;
            frmptyp = "Item wise";
            dateTimePicker1.Value = DateFrom;
            dateTimePicker2.Value = DateTo;
            textBox1.Text = itemname;
            //label3.Enabled = false;
            //textBox1.Enabled = false;
            this.Text = frmptyp;
            DecsOfReport = "Item wise, for the period of " + DateFrom.ToString(Database.dformat) + " to " + DateTo.ToString(Database.dformat);
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dtFinal = new DataTable();
            // sql = "SELECT tblItemInfo.Item_name, tblstock.Marka, TblVoucherType.Vname, tblVoucherinfo.Vdate, tblVoucherinfo.vnumber, Sum(IIf(tblstock.Quantity>0,tblstock.Quantity,0)) AS inw, Sum(IIf(tblstock.Quantity<0,-1*tblstock.Quantity,0)) AS Out,0.00 as stock, Avg(tblVoucherinfo.Vi_id) AS Vi_id FROM ((tblstock LEFT JOIN tblVoucherinfo ON tblstock.Vid = tblVoucherinfo.Vi_id) LEFT JOIN tblItemInfo ON tblstock.Item_id = tblItemInfo.Item_id) LEFT JOIN TblVoucherType ON tblVoucherinfo.Vt_id = TblVoucherType.Vt_id WHERE (((tblItemInfo.Item_name)='" + itemname + "')) GROUP BY tblItemInfo.Item_name, tblstock.Marka, TblVoucherType.Vname, tblVoucherinfo.Vdate, tblVoucherinfo.vnumber order by tblstock.Marka;";
            sql = "SELECT res.Item_name,iif(res.Marka is null,'<Undefined>',res.Marka) as Marka, res.Vname, res.Vdate, res.vnumber, Sum(res.inw) AS inw, Sum(res.Out) AS Out, res.Stock, res.Vi_id FROM (SELECT tblItemInfo.Item_name, tblstock.Marka, 'Opening' AS Vname, #2/1/1801# AS Vdate, 0 AS vnumber, IIf(Sum(tblstock.Quantity)>0,Sum(tblstock.Quantity),0) AS inw, IIf(Sum(tblstock.Quantity)<0,-1*Sum(tblstock.Quantity),0) AS Out,0.001 as Stock, 0 as Vi_id FROM ((tblstock LEFT JOIN tblVoucherinfo ON tblstock.Vid = tblVoucherinfo.Vi_id) LEFT JOIN tblItemInfo ON tblstock.Item_id = tblItemInfo.Item_id) LEFT JOIN TblVoucherType ON tblVoucherinfo.Vt_id = TblVoucherType.Vt_id WHERE (((tblVoucherinfo.Vdate)<#" + DateFrom.ToString(Database.dformat) + "#))";
            sql += " GROUP BY tblItemInfo.Item_name, tblstock.Marka, 'Opening', #2/1/1801#, 0 HAVING (((tblItemInfo.Item_name)='" + itemname + "')) ORDER BY tblstock.Marka Union all SELECT tblItemInfo.Item_name, tblstock.Marka, TblVoucherType.Vname, tblVoucherinfo.Vdate, tblVoucherinfo.vnumber, IIf(Sum(tblstock.Quantity)>0,Sum(tblstock.Quantity),0) AS inw, IIf(Sum(tblstock.Quantity)<0,-1*Sum(tblstock.Quantity),0) AS Out, 0.001 AS Stock, tblVoucherinfo.Vi_id FROM ((tblstock LEFT JOIN tblVoucherinfo ON tblstock.Vid = tblVoucherinfo.Vi_id) LEFT JOIN tblItemInfo ON tblstock.Item_id = tblItemInfo.Item_id) LEFT JOIN TblVoucherType ON tblVoucherinfo.Vt_id = TblVoucherType.Vt_id GROUP BY tblItemInfo.Item_name, tblstock.Marka, TblVoucherType.Vname, tblVoucherinfo.Vdate, tblVoucherinfo.vnumber, 0, tblVoucherinfo.Vi_id";
            sql += " HAVING (((tblItemInfo.Item_name)='" + itemname + "') AND ((tblVoucherinfo.Vdate)>=#" + DateFrom.ToString(Database.dformat) + "# And (tblVoucherinfo.Vdate)<=#" + DateTo.ToString(Database.dformat) + "#)) ORDER BY tblstock.Marka)  AS res GROUP BY res.Item_name,iif(res.Marka is null,'<Undefined>',res.Marka), res.Vname, res.Vdate, res.vnumber, res.Stock, res.Vi_id;";
            dt = new DataTable();



            Database.GetSqlData(sql, dt);

            double totdr = 0;
            double totcr = 0;
            if (dt.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                dt.DefaultView.Sort = "Marka,vdate,vnumber";
                dt = dt.DefaultView.ToTable();
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                totdr += double.Parse(dt.Rows[i]["inw"].ToString());
                totcr += double.Parse(dt.Rows[i]["out"].ToString());
                if (totdr > totcr)
                {
                    dt.Rows[i]["Stock"] = totdr - totcr;
                }
                else if (totcr > totdr)
                {
                    dt.Rows[i]["Stock"] = (totcr - totdr) * -1;
                }
                else
                {
                    dt.Rows[i]["Stock"] = "0";
                }
            }
           






            string[,] col = new string[2, 3]{
                  { "Item_Name","1", "1"},
                  { "Marka", "1", "1"}
               };
            string[,] Cwidth = new string[9, 8] { 
           
            { "ItemName", "0", "0","","","","",""},
            { "Marka", "150", "0" ,"","","","",""},
            { "Type", "150", "0" ,"","","","",""},
            { "Vdate", "100", "0" ,"","","","",""},
            { "VNumber", "150", "1" ,"","","","",""},
          
            { "Inward", "150", "0" ,"|sum(inw)","","|sum(inw)","",""},
            { "Outward", "150", "0" ,"|sum(out)","","|sum(out)","",""},
            { "Stock", "150", "0" ,"","","","",""},
            { "Vid", "0", "0" ,"","","","",""},
            };

            CreateReport(dt, col, Cwidth);
            return true;


        }



        public bool Reffnowise(DateTime DateFrom, DateTime DateTo, string reffno, string frmcaption)
        {
            frmcap = frmcaption;
            stdt = DateFrom;
            endt = DateTo;
            label3.Text = "Reff. No.";
            textBox1.Text = reffno.ToString();
            textBox1.ReadOnly = false;
            frmptyp = "Refference No wise Register";
            dateTimePicker1.Value = DateFrom;
            dateTimePicker2.Value = DateTo;
            dateTimePicker1.Visible = false;
            dateTimePicker2.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            textBox1.Text = reffno.ToString();
            //label3.Enabled = false;
            //textBox1.Enabled = false;
            this.Text = frmptyp;
            DecsOfReport = "Refference No wise Register";
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dtFinal = new DataTable();
            sql = "SELECT tblItemInfo.Item_name, iif(tblstock.Marka is null,'<Undefined>',tblstock.Marka) as Marka, tblVoucherinfo.Vdate,iif(tblstock.Quantity>0,tblstock.Quantity,0) as Inward,iif(tblstock.Quantity<0,-1*tblstock.Quantity,0) as Outward FROM (tblstock LEFT JOIN tblVoucherinfo ON tblstock.Vid = tblVoucherinfo.Vi_id) LEFT JOIN tblItemInfo ON tblstock.Item_id = tblItemInfo.Item_id WHERE (((tblstock.ssection)='" + reffno + "')) ORDER BY tblstock.Marka, tblVoucherinfo.Vdate;";
            dt = new DataTable();




            Database.GetSqlData(sql, dt);



            if (dt.Rows.Count == 0)
            {
                return false;
            }

            string[,] col = new string[2, 3]{
                 
                  { "Item_name", "1", "1"},
                  { "Marka", "1", "1"}
               };

            string[,] Cwidth = new string[5, 8] { 
           
           
            { "Item_name", "0", "0" ,"","","","",""},
           
           
            { "Marka", "0", "1" ,"","","","",""},
           { "vdate", "600", "0" ,"","","Total","",""},
            { "Inward", "200", "1" ,"|sum(Inward)","","|sum(Inward)","",""},
            { "Outward", "200", "1" ,"|sum(Outward)","","|sum(Outward)","",""},
           
            };

            CreateReport(dt, col, Cwidth);
            return true;


        }


        public bool ReffnoSummary(DateTime DateFrom, DateTime DateTo, string reffno, string frmcaption)
        {
            frmcap = frmcaption;
            stdt = DateFrom;
            endt = DateTo;
            label3.Text = "Reff. No.";
            textBox1.Text = reffno.ToString();
            textBox1.ReadOnly = false;
            frmptyp = "Refference No Summary";
            dateTimePicker1.Value = DateFrom;
            dateTimePicker2.Value = DateTo;
            dateTimePicker1.Visible = false;
            dateTimePicker2.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            textBox1.Text = reffno.ToString();
            //label3.Enabled = false;
            //textBox1.Enabled = false;
            this.Text = frmptyp;
            DecsOfReport = "Refference No Summary";
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dtFinal = new DataTable();
            sql = "SELECT tblItemInfo.Item_name, Format([tblVoucherinfo].[Vdate],'dd-mmm-yyyy') AS Vdate, Sum(IIf(tblstock.Quantity>0,tblstock.Quantity,0)) AS Inward, Sum(IIf(tblstock.Quantity<0,-1*tblstock.Quantity,0)) AS Outward FROM (tblstock LEFT JOIN tblVoucherinfo ON tblstock.Vid = tblVoucherinfo.Vi_id) LEFT JOIN tblItemInfo ON tblstock.Item_id = tblItemInfo.Item_id WHERE (((tblstock.ssection)='"+ reffno+"')) GROUP BY tblItemInfo.Item_name, Format([tblVoucherinfo].[Vdate],'dd-mmm-yyyy'), tblVoucherinfo.Vdate ORDER BY tblVoucherinfo.Vdate;";
            dt = new DataTable();




            Database.GetSqlData(sql, dt);



            if (dt.Rows.Count == 0)
            {
                return false;
            }

            string[,] col = new string[0, 0];

            string[,] Cwidth = new string[4, 8] { 
           
           
            { "Item_name", "300", "0" ,"","","","",""},
           
           
           
           { "vdate", "300", "0" ,"","","Total","",""},
            { "Inward", "200", "1" ,"|sum(Inward)","","|sum(Inward)","",""},
            { "Outward", "200", "1" ,"|sum(Outward)","","|sum(Outward)","",""},
           
            };

            CreateReport(dt, col, Cwidth);
            return true;


        }

        public bool PartyList(DateTime DateFrom, DateTime DateTo, string frmcaption)
        {
            frmcap = frmcaption;
            stdt = DateFrom;
            endt = DateTo;
            // textBox1.Text = accnmm;
            textBox1.ReadOnly = true;
            frmptyp = "Party's List";
            dateTimePicker1.Value = DateFrom;
            dateTimePicker2.Value = DateTo;
            dateTimePicker1.Enabled = false;
            dateTimePicker2.Enabled = false;

            label3.Enabled = false;
            textBox1.Enabled = false;
            this.Text = frmptyp;
            DecsOfReport = "Party's List, for the period of " + DateFrom.ToString(Database.dformat) + " to " + DateTo.ToString(Database.dformat);
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dtFinal = new DataTable();


            sql = "SELECT tblAccount.Acc_name , tblAccount.Address, tblAccount.Address2, tblAccount.GST_No, tblAccount.Mobile_No FROM tblAccount ORDER BY tblAccount.Acc_name;";
            dt = new DataTable();




            Database.GetSqlData(sql, dt);

            if (dt.Rows.Count == 0)
            {
                return false;
            }

            string[,] col = new string[0, 0];

            string[,] Cwidth = new string[5, 8] { 
           
           
            { "AccName", "250", "0" ,"","","","",""},
           
            
            { "Address1", "250", "1" ,"","","","",""},
            { "Address2", "150", "0" ,"","","","",""},
           
            { "GSTIN", "150", "1" ,"","","","",""},
            { "MobileNo", "200", "1" ,"","","","",""},
            };


            
            CreateReport(dt, col, Cwidth);
            return true;


        }


        public bool StockRegOut(DateTime DateFrom, DateTime DateTo, string frmcaption)
        {
            frmcap = frmcaption;
            stdt = DateFrom;
            endt = DateTo;
            // textBox1.Text = accnmm;
            textBox1.ReadOnly = true;
            frmptyp = "Stock Register Outward";
            dateTimePicker1.Value = DateFrom;
            dateTimePicker2.Value = DateTo;
            dateTimePicker1.Enabled = false;
          
            label3.Enabled = false;
            textBox1.Enabled = false;
            this.Text = frmptyp;
            DecsOfReport = "Stock Register Outward, for the period of " + DateFrom.ToString(Database.dformat) + " to " + DateTo.ToString(Database.dformat);
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dtFinal = new DataTable();


            sql = "TRANSFORM  Sum(-1*[Quantity]) AS Quantity SELECT tblVoucherinfo.Vdate, TBLVOUCHERTYPE.Short & ' ' & Format(tblVoucherinfo.Vdate,'yyyymmdd' & ' ' & tblVoucherinfo.Vnumber) AS DocNumber, tblAccount.Acc_name, tblstock.Marka AS Marka FROM (((tblstock LEFT JOIN tblAccount ON tblstock.Ac_id = tblAccount.Ac_id) LEFT JOIN tblItemInfo ON tblstock.Item_id = tblItemInfo.Item_id) LEFT JOIN tblVoucherinfo ON tblstock.Vid = tblVoucherinfo.Vi_id) LEFT JOIN TblVoucherType ON tblVoucherinfo.Vt_id = TblVoucherType.Vt_id WHERE TblVoucherType.type='Outward' and  tblVoucherinfo.Vdate<=#"+DateTo.ToString(Database.dformat)+"#  GROUP BY tblVoucherinfo.Vdate, TBLVOUCHERTYPE.Short & ' ' & Format(tblVoucherinfo.Vdate,'yyyymmdd' & ' ' & tblVoucherinfo.Vnumber), tblAccount.Acc_name, tblstock.Marka ORDER BY tblVoucherinfo.Vdate PIVOT tblItemInfo.Item_name;";
            dt = new DataTable();




            Database.GetSqlData(sql, dt);





            if (dt.Rows.Count == 0)
            {
                return false;
            }

            dt.Columns.Add("Total", typeof(int));
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                int total = 0;

                for (int j = 4; j < dt.Columns.Count - 1; j++)
                {
                    if (dt.Rows[i][j] == null || dt.Rows[i][j].ToString() == "")
                    {
                        dt.Rows[i][j] = 0;
                    }
                    total += int.Parse(dt.Rows[i][j].ToString());
                }
                dt.Rows[i]["total"] = total;
            }
            string[,] col = new string[1, 3]{
             { "vdate", "1", "1" }
            
           
            };


            string[,] Cwidth = new string[dt.Columns.Count, 6];
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                Cwidth[i, 0] = dt.Columns[i].ColumnName;
                if (i == 0)
                {
                    Cwidth[i, 1] = "0";
                    Cwidth[i, 2] = "0";
                    Cwidth[i, 3] = "";
                    Cwidth[i, 4] = "";
                    Cwidth[i, 5] = "";


                }
                else if (i == 1)
                {
                    Cwidth[i, 0] = "DocNumber";
                    Cwidth[i, 1] = "200";
                    Cwidth[i, 2] = "0";
                    Cwidth[i, 3] = "";
                    Cwidth[i, 4] = "";
                    Cwidth[i, 5] = "";

                }
                else if (i == 2)
                {
                    Cwidth[i, 0] = "PartyName";
                    Cwidth[i, 1] = "200";
                    Cwidth[i, 2] = "0";
                    Cwidth[i, 3] = "Total";
                    Cwidth[i, 4] = "";
                    Cwidth[i, 5] = "";

                }
                else if (i == 3)
                {
                    Cwidth[i, 0] = "Marka";
                    Cwidth[i, 1] = "100";
                    Cwidth[i, 2] = "0";
                    Cwidth[i, 3] = "";
                    Cwidth[i, 4] = "";
                    Cwidth[i, 5] = "";

                }

                else
                {
                    Cwidth[i, 1] = (500 / (dt.Columns.Count - 4)).ToString();
                    //if (i == dt.Columns.Count)
                    //{
                    //    Cwidth[i, 3] = "";
                    //    Cwidth[i, 4] = "";
                    //}
                    //else
                    //{
                    Cwidth[i, 3] = "|sum([" + dt.Columns[i].ColumnName + "])";
                    Cwidth[i, 4] = "|sum([" + dt.Columns[i].ColumnName + "])";
                    //}
                    Cwidth[i, 5] = "";

                    Cwidth[i, 2] = "1";
                }
            }
            CreateReport(dt, col, Cwidth);
            return true;


        }
        public bool StockRegIn(DateTime DateFrom, DateTime DateTo,  string frmcaption)
        {
            frmcap = frmcaption;
            stdt = DateFrom;
            endt = DateTo;
           // textBox1.Text = accnmm;
            textBox1.ReadOnly = true;
            frmptyp = "Stock Register Inward";
            dateTimePicker1.Value = DateFrom;
            dateTimePicker2.Value = DateTo;
            dateTimePicker1.Enabled = false;
            

            label3.Enabled = false;
            textBox1.Enabled = false;
            this.Text = frmptyp;
            DecsOfReport = "Stock Register Inward, for the period of " + DateFrom.ToString(Database.dformat) + " to " + DateTo.ToString(Database.dformat);
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dtFinal = new DataTable();


            sql = "TRANSFORM Sum(tblstock.Quantity) AS SumOfQuantity SELECT tblVoucherinfo.Vdate, TBLVOUCHERTYPE.Short & ' ' & Format(tblVoucherinfo.Vdate,'yyyymmdd' & ' ' & tblVoucherinfo.Vnumber) AS DocNumber, tblAccount.Acc_name, tblstock.Marka AS Marka FROM (((tblstock LEFT JOIN tblAccount ON tblstock.Ac_id = tblAccount.Ac_id) LEFT JOIN tblItemInfo ON tblstock.Item_id = tblItemInfo.Item_id) LEFT JOIN tblVoucherinfo ON tblstock.Vid = tblVoucherinfo.Vi_id) LEFT JOIN TblVoucherType ON tblVoucherinfo.Vt_id = TblVoucherType.Vt_id WHERE (((TblVoucherType.Vname)='OpeningStock' Or (TblVoucherType.type)='Inward')) And tblVoucherinfo.Vdate<=#"+DateTo.ToString(Database.dformat)+"# GROUP BY tblVoucherinfo.Vdate, TBLVOUCHERTYPE.Short & ' ' & Format(tblVoucherinfo.Vdate,'yyyymmdd' & ' ' & tblVoucherinfo.Vnumber), tblAccount.Acc_name, tblstock.Marka ORDER BY tblVoucherinfo.Vdate PIVOT tblItemInfo.Item_name;";
            dt = new DataTable();




            Database.GetSqlData(sql, dt);





            if (dt.Rows.Count == 0)
            {
                return false;
            }

            dt.Columns.Add("Total", typeof(int));
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                int total = 0;

                for (int j = 4; j < dt.Columns.Count - 1; j++)
                {
                    if (dt.Rows[i][j] == null || dt.Rows[i][j].ToString() == "")
                    {
                        dt.Rows[i][j] = 0;
                    }
                    total += int.Parse(dt.Rows[i][j].ToString());
                }
                dt.Rows[i]["total"] = total;
            }
            string[,] col = new string[1, 3]{
             { "vdate", "1", "1" }
            
           
            };


            string[,] Cwidth = new string[dt.Columns.Count, 6];
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                Cwidth[i, 0] = dt.Columns[i].ColumnName;
                if (i == 0)
                {
                    Cwidth[i, 1] = "0";
                    Cwidth[i, 2] = "0";
                    Cwidth[i, 3] = "";
                    Cwidth[i, 4] = "";
                    Cwidth[i, 5] = "";


                }
                else if (i == 1)
                {
                    Cwidth[i, 0] = "DocNumber";
                    Cwidth[i, 1] = "200";
                    Cwidth[i, 2] = "0";
                    Cwidth[i, 3] = "";
                    Cwidth[i, 4] = "";
                    Cwidth[i, 5] = "";

                }
                else if (i == 2)
                {
                    Cwidth[i, 0] = "PartyName";
                    Cwidth[i, 1] = "200";
                    Cwidth[i, 2] = "0";
                    Cwidth[i, 3] = "";
                    Cwidth[i, 4] = "";
                    Cwidth[i, 5] = "";

                }
                else if (i == 3)
                {
                    Cwidth[i, 0] = "Marka";
                    Cwidth[i, 1] = "100";
                    Cwidth[i, 2] = "0";
                    Cwidth[i, 3] = "";
                    Cwidth[i, 4] = "";
                    Cwidth[i, 5] = "";

                }
              
                else
                {
                    Cwidth[i, 1] = (500 / (dt.Columns.Count - 4)).ToString();
                    //if (i == dt.Columns.Count)
                    //{
                    //    Cwidth[i, 3] = "";
                    //    Cwidth[i, 4] = "";
                    //}
                    //else
                    //{
                    Cwidth[i, 3] = "|sum([" + dt.Columns[i].ColumnName + "])";
                    Cwidth[i, 4] = "|sum([" + dt.Columns[i].ColumnName + "])";
                    //}
                    Cwidth[i, 5] = "";

                    Cwidth[i, 2] = "1";
                }
            }
            CreateReport(dt, col, Cwidth);
            return true;

        }

        public bool InTotalStockReg(DateTime DateFrom, DateTime DateTo, string frmcaption)
        {
            frmcap = frmcaption;
            stdt = DateFrom;
            endt = DateTo;
            // textBox1.Text = accnmm;
            textBox1.ReadOnly = true;
            frmptyp = "Inward Total Stock List";
            dateTimePicker1.Value = DateFrom;
            dateTimePicker2.Value = DateTo;
            dateTimePicker1.Enabled = false;


            label3.Enabled = false;
            textBox1.Enabled = false;
            this.Text = frmptyp;
            DecsOfReport = "Inward Total Stock List, for the period of " + DateFrom.ToString(Database.dformat) + " to " + DateTo.ToString(Database.dformat);
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dtFinal = new DataTable();



            sql = "TRANSFORM Sum(tblstock.Quantity) AS Quantity SELECT tblAccount.Acc_name FROM ((tblstock LEFT JOIN tblAccount ON tblstock.Ac_id = tblAccount.Ac_id) LEFT JOIN tblItemInfo ON tblstock.Item_id = tblItemInfo.Item_id) LEFT JOIN tblVoucherinfo ON tblstock.Vid = tblVoucherinfo.Vi_id WHERE (((tblVoucherinfo.Vdate)<=#" + DateTo.ToString(Database.dformat) + "#) AND ((tblVoucherinfo.Vt_id)=1)) GROUP BY tblAccount.Acc_name PIVOT tblItemInfo.Item_name;";
            dt = new DataTable();




            Database.GetSqlData(sql, dt);





            if (dt.Rows.Count == 0)
            {
                return false;
            }
            DataColumn Col = dt.Columns.Add("Sno", typeof(int));
            Col.SetOrdinal(0);

            dt.Columns.Add("Total", typeof(int));
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                int total = 0;

                for (int j = 2; j < dt.Columns.Count - 1; j++)
                {
                    if (dt.Rows[i][j] == null || dt.Rows[i][j].ToString() == "")
                    {
                        dt.Rows[i][j] = 0;
                    }
                    total += int.Parse(dt.Rows[i][j].ToString());
                }
                dt.Rows[i]["total"] = total;
            }




            dt.DefaultView.Sort = "Acc_name";
            dt.DefaultView.ToTable();


            if (dt.Rows.Count == 0)
            {
                return false;

            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["Sno"] = i + 1;
            }



            string[,] col = new string[0, 0];

            string[,] Cwidth = new string[dt.Columns.Count, 6];
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                Cwidth[i, 0] = dt.Columns[i].ColumnName;
                if (i == 0)
                {
                    Cwidth[i, 1] = "50";
                    Cwidth[i, 2] = "0";
                    Cwidth[i, 3] = "";
                    Cwidth[i, 4] = "";
                    Cwidth[i, 5] = "";


                }
                else if (i == 1)
                {
                    Cwidth[i, 1] = "250";
                    Cwidth[i, 2] = "0";
                    Cwidth[i, 3] = "";
                    Cwidth[i, 4] = "";
                    Cwidth[i, 5] = "";


                }
               


                else
                {
                    Cwidth[i, 1] = (1700 / (dt.Columns.Count - 2)).ToString();

                    Cwidth[i, 3] = "|sum([" + dt.Columns[i].ColumnName + "])";
                    Cwidth[i, 4] = "|sum([" + dt.Columns[i].ColumnName + "])";
                    //}
                    Cwidth[i, 5] = "";

                    Cwidth[i, 2] = "1";
                }
            }
            CreateReport(dt, col, Cwidth);
            return true;


        }
        public bool StockReg(DateTime DateFrom, DateTime DateTo, string frmcaption)
        {
            frmcap = frmcaption;
            stdt = DateFrom;
            endt = DateTo;
            // textBox1.Text = accnmm;
            textBox1.ReadOnly = true;
            frmptyp = "All Stock List";
            dateTimePicker1.Value = DateFrom;
            dateTimePicker2.Value = DateTo;
            dateTimePicker1.Enabled = false;


            label3.Enabled = false;
            textBox1.Enabled = false;
            this.Text = frmptyp;
            DecsOfReport = "Stock List, for the period of " + DateFrom.ToString(Database.dformat) + " to " + DateTo.ToString(Database.dformat);
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dtFinal = new DataTable();



            sql = "TRANSFORM Sum(tblstock.Quantity) AS Quantity SELECT tblAccount.Acc_name FROM ((tblstock LEFT JOIN tblAccount ON tblstock.Ac_id = tblAccount.Ac_id) LEFT JOIN tblItemInfo ON tblstock.Item_id = tblItemInfo.Item_id) LEFT JOIN tblVoucherinfo ON tblstock.Vid = tblVoucherinfo.Vi_id WHERE (((tblVoucherinfo.Vdate)<=#" + DateTo.ToString(Database.dformat) + "#)) GROUP BY tblAccount.Acc_name PIVOT tblItemInfo.Item_name;";
            dt = new DataTable();




            Database.GetSqlData(sql, dt);





            if (dt.Rows.Count == 0)
            {
                return false;
            }
            DataColumn Col = dt.Columns.Add("Sno", typeof(int));
            Col.SetOrdinal(0);

            dt.Columns.Add("Total", typeof(int));
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                int total = 0;

                for (int j = 2; j < dt.Columns.Count - 1; j++)
                {
                    if (dt.Rows[i][j] == null || dt.Rows[i][j].ToString() == "")
                    {
                        dt.Rows[i][j] = 0;
                    }
                    total += int.Parse(dt.Rows[i][j].ToString());
                }
                dt.Rows[i]["total"] = total;
            }



          
                dt.DefaultView.Sort = "Acc_name";
                dt.DefaultView.ToTable();

         
            if (dt.Rows.Count == 0)
            {
                return false;

            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["Sno"] = i + 1;
            }



            string[,] col = new string[0, 0];

            string[,] Cwidth = new string[dt.Columns.Count, 6];
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                Cwidth[i, 0] = dt.Columns[i].ColumnName;
                if (i == 0)
                {
                    Cwidth[i, 1] = "50";
                    Cwidth[i, 2] = "0";
                    Cwidth[i, 3] = "";
                    Cwidth[i, 4] = "";
                    Cwidth[i, 5] = "";


                }
                else if (i == 1)
                {
                    Cwidth[i, 1] = "250";
                    Cwidth[i, 2] = "0";
                    Cwidth[i, 3] = "";
                    Cwidth[i, 4] = "";
                    Cwidth[i, 5] = "";


                }
                


                else
                {
                    Cwidth[i, 1] = (1700 / (dt.Columns.Count - 2)).ToString();
                   
                    Cwidth[i, 3] = "|sum([" + dt.Columns[i].ColumnName + "])";
                    Cwidth[i, 4] = "|sum([" + dt.Columns[i].ColumnName + "])";
                    //}
                    Cwidth[i, 5] = "";

                    Cwidth[i, 2] = "1";
                }
            }
            CreateReport(dt, col, Cwidth);
            return true;


        }

        public bool TotalStockReg(DateTime DateFrom, DateTime DateTo, string frmcaption)
        {
            frmcap = frmcaption;
            stdt = DateFrom;
            endt = DateTo;
            // textBox1.Text = accnmm;
            textBox1.ReadOnly = true;
            frmptyp = "Total Stock List";
            dateTimePicker1.Value = DateFrom;
            dateTimePicker2.Value = DateTo;
            dateTimePicker1.Enabled = false;
           

            label3.Enabled = false;
            textBox1.Enabled = false;
            this.Text = frmptyp;
            DecsOfReport = "Total Stock List, for the period of " + DateFrom.ToString(Database.dformat) + " to " + DateTo.ToString(Database.dformat);
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dtFinal = new DataTable();



            sql = "TRANSFORM Sum(tblstock.Quantity) AS Quantity SELECT tblAccount.Acc_name FROM ((tblstock LEFT JOIN tblAccount ON tblstock.Ac_id = tblAccount.Ac_id) LEFT JOIN tblItemInfo ON tblstock.Item_id = tblItemInfo.Item_id) LEFT JOIN tblVoucherinfo ON tblstock.Vid = tblVoucherinfo.Vi_id WHERE (((tblVoucherinfo.Vdate)<=#"+DateTo.ToString(Database.dformat)+"#)) GROUP BY tblAccount.Acc_name PIVOT tblItemInfo.Item_name;";
            dt = new DataTable();




            Database.GetSqlData(sql, dt);





            if (dt.Rows.Count == 0)
            {
                return false;
            }
          

            DataColumn Col = dt.Columns.Add("Sno", typeof(int));
            Col.SetOrdinal(0);
             
            dt.Columns.Add("Total", typeof(int));
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                int total = 0;

                for (int j = 2; j < dt.Columns.Count - 1; j++)
                {
                    if (dt.Rows[i][j] == null || dt.Rows[i][j].ToString() == "")
                    {
                        dt.Rows[i][j] = 0;
                    }
                    total += int.Parse(dt.Rows[i][j].ToString());
                }
                dt.Rows[i]["total"] = total;
            }
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //for (int j = 2; j < dt.Columns.Count-1; j++)
            //{
            //    if (dt.Columns[j].DataType.ToString() == "Double")
            //    {
            //        if (double.Parse(dt.Rows[i][j].ToString()) != 0)
            //        {

            //        }
            //    }

            //}
            //}


            DataRow[] drow;
            drow = dt.Select("total>0");
            tdt = new DataTable();
            if (drow.GetLength(0) > 0)
            {
                tdt = drow.CopyToDataTable();
                tdt.DefaultView.Sort = "Acc_name";
                tdt.DefaultView.ToTable();

            }
            if (tdt.Rows.Count == 0)
            {
                return false;

            }
            for (int i = 0; i < tdt.Rows.Count; i++)
            {
                tdt.Rows[i]["Sno"] = i + 1;
            }



            string[,] col = new string[0, 0];

            string[,] Cwidth = new string[tdt.Columns.Count, 6];
            for (int i = 0; i < tdt.Columns.Count; i++)
            {
                Cwidth[i, 0] = tdt.Columns[i].ColumnName;
                if (i == 0)
                {
                    Cwidth[i, 1] = "40";
                    Cwidth[i, 2] = "0";
                    Cwidth[i, 3] = "";
                    Cwidth[i, 4] = "";
                    Cwidth[i, 5] = "";


                }
                else if (i == 1)
                {
                    Cwidth[i, 1] = "260";
                    Cwidth[i, 2] = "0";
                    Cwidth[i, 3] = "";
                    Cwidth[i, 4] = "";
                    Cwidth[i, 5] = "";


                }
               

                else
                {
                    Cwidth[i, 1] = (1700 / (tdt.Columns.Count - 2)).ToString();
                    //if (i == dt.Columns.Count)
                    //{
                    //    Cwidth[i, 3] = "";
                    //    Cwidth[i, 4] = "";
                    //}
                    //else
                    //{
                    Cwidth[i, 3] = "|sum([" + tdt.Columns[i].ColumnName + "])";
                    Cwidth[i, 4] = "|sum([" + tdt.Columns[i].ColumnName + "])";
                    //}
                    Cwidth[i, 5] = "";

                    Cwidth[i, 2] = "1";
                }
            }
            CreateReport(tdt, col, Cwidth);
            return true;


        }
        public bool PartyRegisterItemWise(DateTime DateFrom, DateTime DateTo, string frmcaption,string accnm)
        {
            frmcap = frmcaption;
            stdt = DateFrom;
            endt = DateTo;
            textBox1.Text = accnm;
            textBox1.ReadOnly = true;
            frmptyp = "Party's Item Wise Register";
            dateTimePicker1.Value = DateFrom;
            dateTimePicker2.Value = DateTo;
            dateTimePicker1.Enabled = false;
            dateTimePicker2.Enabled = false;

            //label3.Enabled = false;
            //textBox1.Enabled = false;
            this.Text = frmptyp;
            DecsOfReport = "Party's Item Wise Register, for the period of " + DateFrom.ToString(Database.dformat) + " to " + DateTo.ToString(Database.dformat);
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dtFinal = new DataTable();

            //sql = "SELECT First(tblVoucherinfo.Vdate) AS Vdate, tblAccount.Acc_name, tblstock.reffno, tblItemInfo.Item_name, Sum(IIf(tblstock.Quantity>0,tblstock.Quantity,0)) AS Inward, Sum(IIf(tblstock.Quantity<0,-1*tblstock.Quantity,0)) AS Outward, 0 AS Stock FROM ((tblVoucherinfo LEFT JOIN tblAccount ON tblVoucherinfo.Ac_id = tblAccount.Ac_id) LEFT JOIN tblstock ON tblVoucherinfo.Vi_id = tblstock.Vid) LEFT JOIN tblItemInfo ON tblstock.Item_id = tblItemInfo.Item_id WHERE (((tblVoucherinfo.Isbilled)=False)) GROUP BY tblAccount.Acc_name, tblstock.reffno, tblItemInfo.Item_name, 0 ORDER BY First(tblVoucherinfo.Vdate),tblAccount.Acc_name, tblItemInfo.Item_name;";
          //  sql = "SELECT res.Vdate, res.Acc_name, res.reffno, res.Item_name, Sum(res.Inward) AS Inward, Sum(res.Outward) AS Outward, res.Stock FROM (SELECT First(tblVoucherinfo.Vdate) AS Vdate, tblAccount.Acc_name, tblstock.ssection as reffno, tblItemInfo.Item_name, Sum(IIf(tblstock.Quantity>0,tblstock.Quantity,0)) AS Inward, Sum(IIf(tblstock.Quantity<0,-1*tblstock.Quantity,0)) AS Outward, 0 AS Stock FROM ((tblVoucherinfo LEFT JOIN tblAccount ON tblVoucherinfo.Ac_id = tblAccount.Ac_id) LEFT JOIN tblstock ON tblVoucherinfo.Vi_id = tblstock.Vid) LEFT JOIN tblItemInfo ON tblstock.Item_id = tblItemInfo.Item_id GROUP BY tblAccount.Acc_name,tblstock.ssection, tblItemInfo.Item_name, 0 ORDER BY First(tblVoucherinfo.Vdate), tblAccount.Acc_name, tblItemInfo.Item_name )  AS res LEFT JOIN tblVoucherinfo ON clng(res.reffno) =tblVoucherinfo.vnumber WHERE (((tblVoucherinfo.isbilled)=False) AND ((tblVoucherinfo.Vt_id)=1)) GROUP BY res.Vdate, res.Acc_name, res.reffno, res.Item_name, res.Stock;";

            sql = "SELECT res.Vdate, res.Acc_name, res.reffno, res.Item_name, res.Inward AS Inward, res.Outward AS Outward, res.Stock FROM ((SELECT First(tblVoucherinfo.Vdate) AS Vdate, tblAccount.Acc_name, tblstock.ssection as reffno, tblItemInfo.Item_name, Sum(IIf(tblstock.Quantity>0,tblstock.Quantity,0)) AS Inward, Sum(IIf(tblstock.Quantity<0,-1*tblstock.Quantity,0)) AS Outward, 0 AS Stock FROM ((tblVoucherinfo LEFT JOIN tblAccount ON tblVoucherinfo.Ac_id = tblAccount.Ac_id) LEFT JOIN tblstock ON tblVoucherinfo.Vi_id = tblstock.Vid) LEFT JOIN tblItemInfo ON tblstock.Item_id = tblItemInfo.Item_id GROUP BY tblAccount.Acc_name,tblstock.ssection, tblItemInfo.Item_name, 0 ORDER BY First(tblVoucherinfo.Vdate), tblAccount.Acc_name, tblItemInfo.Item_name)   AS res LEFT JOIN tblstock ON res.reffno = tblstock.ssection) LEFT JOIN tblVoucherinfo ON tblstock.Vid = tblVoucherinfo.Vi_id WHERE (((tblVoucherinfo.isbilled)=False) AND ((tblVoucherinfo.Vt_id)=1)) GROUP BY res.Vdate, res.Acc_name, res.reffno, res.Item_name, res.Inward , res.Outward,res.Stock";
            dt = new DataTable();




            Database.GetSqlData(sql, dt);



            if (dt.Rows.Count == 0)
            {
                return false;
            }

            if (accnm != "")
            {
                dt = dt.Select("Acc_name='"+accnm+"'").CopyToDataTable();
            }


            int totdr = 0, totcr = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                totdr = int.Parse(dt.Rows[i]["Inward"].ToString());
                totcr = int.Parse(dt.Rows[i]["Outward"].ToString());
               
                    dt.Rows[i]["Stock"] = totdr - totcr;

            }

            string[,] col = new string[0, 0];

            string[,] Cwidth = new string[7, 8] { 
          
            { "Vdate", "150", "0" ,"","","","",""},
            { "PartyName", "250", "0" ,"","","","",""},
            { "Reff No", "100", "0" ,"","","","",""},
            { "ItemName", "200", "1" ,"","","Total","",""},
            { "Inward", "100", "0" ,"|sum(Inward)","","|sum(Inward)","",""},
            { "Outward", "100", "0" ,"|sum(Outward)","","|sum(Outward)","",""},
            { "Stock", "100", "0" ,"|sum(Stock)","","|sum(Stock)","",""},
            };

            CreateReport(dt, col, Cwidth);
            return true;


        }



        public bool PartyRegisterItemWiseAll(DateTime DateFrom, DateTime DateTo, string frmcaption, string accnm)
        {
            frmcap = frmcaption;
            stdt = DateFrom;
            endt = DateTo;
            textBox1.Text = accnm;
            textBox1.ReadOnly = true;
            frmptyp = "Party's Item Wise Register";
            dateTimePicker1.Value = DateFrom;
            dateTimePicker2.Value = DateTo;
            dateTimePicker1.Enabled = false;
            dateTimePicker2.Enabled = false;

            //label3.Enabled = false;
            //textBox1.Enabled = false;
            this.Text = frmptyp;
            DecsOfReport = "Party's Item Wise Register All, for the period of " + DateFrom.ToString(Database.dformat) + " to " + DateTo.ToString(Database.dformat);
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dtFinal = new DataTable();

            //sql = "SELECT First(tblVoucherinfo.Vdate) AS Vdate, tblAccount.Acc_name, tblstock.reffno, tblItemInfo.Item_name, Sum(IIf(tblstock.Quantity>0,tblstock.Quantity,0)) AS Inward, Sum(IIf(tblstock.Quantity<0,-1*tblstock.Quantity,0)) AS Outward, 0 AS Stock FROM ((tblVoucherinfo LEFT JOIN tblAccount ON tblVoucherinfo.Ac_id = tblAccount.Ac_id) LEFT JOIN tblstock ON tblVoucherinfo.Vi_id = tblstock.Vid) LEFT JOIN tblItemInfo ON tblstock.Item_id = tblItemInfo.Item_id WHERE (((tblVoucherinfo.Isbilled)=False)) GROUP BY tblAccount.Acc_name, tblstock.reffno, tblItemInfo.Item_name, 0 ORDER BY First(tblVoucherinfo.Vdate),tblAccount.Acc_name, tblItemInfo.Item_name;";
            //  sql = "SELECT res.Vdate, res.Acc_name, res.reffno, res.Item_name, Sum(res.Inward) AS Inward, Sum(res.Outward) AS Outward, res.Stock FROM (SELECT First(tblVoucherinfo.Vdate) AS Vdate, tblAccount.Acc_name, tblstock.ssection as reffno, tblItemInfo.Item_name, Sum(IIf(tblstock.Quantity>0,tblstock.Quantity,0)) AS Inward, Sum(IIf(tblstock.Quantity<0,-1*tblstock.Quantity,0)) AS Outward, 0 AS Stock FROM ((tblVoucherinfo LEFT JOIN tblAccount ON tblVoucherinfo.Ac_id = tblAccount.Ac_id) LEFT JOIN tblstock ON tblVoucherinfo.Vi_id = tblstock.Vid) LEFT JOIN tblItemInfo ON tblstock.Item_id = tblItemInfo.Item_id GROUP BY tblAccount.Acc_name,tblstock.ssection, tblItemInfo.Item_name, 0 ORDER BY First(tblVoucherinfo.Vdate), tblAccount.Acc_name, tblItemInfo.Item_name )  AS res LEFT JOIN tblVoucherinfo ON clng(res.reffno) =tblVoucherinfo.vnumber WHERE (((tblVoucherinfo.isbilled)=False) AND ((tblVoucherinfo.Vt_id)=1)) GROUP BY res.Vdate, res.Acc_name, res.reffno, res.Item_name, res.Stock;";

            sql = "SELECT res.Vdate, res.Acc_name, res.reffno, res.Item_name, res.Inward AS Inward, res.Outward AS Outward, res.Stock FROM ((SELECT First(tblVoucherinfo.Vdate) AS Vdate, tblAccount.Acc_name, tblstock.ssection as reffno, tblItemInfo.Item_name, Sum(IIf(tblstock.Quantity>0,tblstock.Quantity,0)) AS Inward, Sum(IIf(tblstock.Quantity<0,-1*tblstock.Quantity,0)) AS Outward, 0 AS Stock FROM ((tblVoucherinfo LEFT JOIN tblAccount ON tblVoucherinfo.Ac_id = tblAccount.Ac_id) LEFT JOIN tblstock ON tblVoucherinfo.Vi_id = tblstock.Vid) LEFT JOIN tblItemInfo ON tblstock.Item_id = tblItemInfo.Item_id GROUP BY tblAccount.Acc_name,tblstock.ssection, tblItemInfo.Item_name, 0 ORDER BY First(tblVoucherinfo.Vdate), tblAccount.Acc_name, tblItemInfo.Item_name)   AS res LEFT JOIN tblstock ON res.reffno = tblstock.ssection) LEFT JOIN tblVoucherinfo ON tblstock.Vid = tblVoucherinfo.Vi_id WHERE tblVoucherinfo.Vt_id=1 GROUP BY res.Vdate, res.Acc_name, res.reffno, res.Item_name, res.Inward , res.Outward,res.Stock";
            dt = new DataTable();




            Database.GetSqlData(sql, dt);



            if (dt.Rows.Count == 0)
            {
                return false;
            }

            if (accnm != "")
            {
                dt = dt.Select("Acc_name='" + accnm + "'").CopyToDataTable();
            }


            int totdr = 0, totcr = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                totdr = int.Parse(dt.Rows[i]["Inward"].ToString());
                totcr = int.Parse(dt.Rows[i]["Outward"].ToString());

                dt.Rows[i]["Stock"] = totdr - totcr;

            }

            string[,] col = new string[0, 0];

            string[,] Cwidth = new string[7, 8] { 
          
            { "Vdate", "150", "0" ,"","","","",""},
            { "PartyName", "250", "0" ,"","","","",""},
            { "Reff No", "100", "0" ,"","","","",""},
            { "ItemName", "200", "1" ,"","","Total","",""},
            { "Inward", "100", "0" ,"|sum(Inward)","","|sum(Inward)","",""},
            { "Outward", "100", "0" ,"|sum(Outward)","","|sum(Outward)","",""},
            { "Stock", "100", "0" ,"|sum(Stock)","","|sum(Stock)","",""},
            };

            CreateReport(dt, col, Cwidth);
            return true;


        }
        public bool PartyStock(DateTime DateFrom, DateTime DateTo, string accnmm, string frmcaption)
        {
            frmcap = frmcaption;
            stdt = DateFrom;
            endt = DateTo;
            textBox1.Text = accnmm;
            textBox1.ReadOnly = true;
            frmptyp = "Party's Stock";
            dateTimePicker1.Value = DateFrom;
            dateTimePicker2.Value = DateTo;
            dateTimePicker1.Enabled = false;
          

            label3.Enabled = true;
            textBox1.Enabled = true;
            this.Text = frmptyp;
            DecsOfReport = accnmm +" - Party's Stock Inward, for the period of " + DateFrom.ToString(Database.dformat) + " to " + DateTo.ToString(Database.dformat);
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dtFinal = new DataTable();

          //  sql = "SELECT tblItemInfo.Item_name &' - '& tblstock.Marka as Item, tblVoucherinfo.Vdate, TBLVOUCHERTYPE.Short & ' ' & Format(tblVoucherinfo.Vdate,'yyyymmdd' & ' ' & tblVoucherinfo.Vnumber) AS DocNumber, tblstock.Quantity FROM (((tblstock LEFT JOIN tblAccount ON tblstock.Ac_id = tblAccount.Ac_id) LEFT JOIN tblItemInfo ON tblstock.Item_id = tblItemInfo.Item_id) LEFT JOIN tblVoucherinfo ON tblstock.Vid = tblVoucherinfo.Vi_id) LEFT JOIN TblVoucherType ON tblVoucherinfo.Vt_id = TblVoucherType.Vt_id WHERE (((tblAccount.Acc_name)='" + accnmm + "'));";
            sql = "TRANSFORM Sum(tblstock.Quantity) AS Quantity SELECT TBLVOUCHERTYPE.Vname,TBLVOUCHERTYPE.Short & ' ' & tblVoucherinfo.Vnumber AS DocNumber, tblVoucherinfo.Vdate FROM (((tblstock LEFT JOIN tblAccount ON tblstock.Ac_id = tblAccount.Ac_id) LEFT JOIN tblItemInfo ON tblstock.Item_id = tblItemInfo.Item_id) LEFT JOIN tblVoucherinfo ON tblstock.Vid = tblVoucherinfo.Vi_id) LEFT JOIN TblVoucherType ON tblVoucherinfo.Vt_id = TblVoucherType.Vt_id WHERE tblVoucherinfo.Vdate<=#"+DateTo.ToString(Database.dformat)+"# AND tblAccount.Acc_name='" + accnmm + "' GROUP BY TBLVOUCHERTYPE.Vname,TBLVOUCHERTYPE.Short  & ' ' & tblVoucherinfo.Vnumber, tblVoucherinfo.Vdate PIVOT tblItemInfo.Item_name & ' - ' & tblstock.Marka;";
            dt = new DataTable();




            Database.GetSqlData(sql, dt);



            if (dt.Rows.Count == 0)
            {
                return false;
            }

            dt.Columns.Add("Total", typeof(int));
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                int total = 0;

                for (int j = 3; j < dt.Columns.Count -1; j++)
                {
                    if (dt.Rows[i][j] == null || dt.Rows[i][j].ToString() == "")
                    {
                        dt.Rows[i][j] = 0;
                    }
                    total += int.Parse(dt.Rows[i][j].ToString());
                }
                dt.Rows[i]["total"] = total;
            }
            string[,] col = new string[1, 3]{{"Vname","0","1"}};
          
            string[,] Cwidth = new string[dt.Columns.Count, 6];
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                Cwidth[i, 0] = dt.Columns[i].ColumnName;
                if (i == 0)
                {
                    Cwidth[i, 1] = "0";
                    Cwidth[i, 2] = "0";
                    Cwidth[i, 3] = "";
                    Cwidth[i, 4] = "";
                    Cwidth[i, 5] = "";


                }
                else  if (i == 1)
                {
                    Cwidth[i, 1] = "80";
                    Cwidth[i, 2] = "0";
                    Cwidth[i, 3] = "";
                    Cwidth[i, 4] = "";
                    Cwidth[i, 5] = "";


                }
                else if (i == 2)
                {
                   
                    Cwidth[i, 1] = "100";
                    Cwidth[i, 2] = "0";
                    Cwidth[i, 3] = "";
                    Cwidth[i, 4] = "";
                    Cwidth[i, 5] = "";

                }
               
                else
                {
                    Cwidth[i, 1] = (1820 / (dt.Columns.Count - 3)).ToString();
                
                    Cwidth[i, 3] = "|sum([" + dt.Columns[i].ColumnName + "])";
                    Cwidth[i, 4] = "|sum([" + dt.Columns[i].ColumnName + "])";
                   
                    Cwidth[i, 5] = "";

                    Cwidth[i, 2] = "1";
                }
            }

            CreateReport(dt, col, Cwidth);
            return true;


        }

        public bool PartyLoanReg(DateTime DateFrom, DateTime DateTo, string frmcaption)
        {
            frmcap = frmcaption;
            stdt = DateFrom;
            endt = DateTo;
            //textBox1.Text = accnmm;
            textBox1.ReadOnly = true;
            frmptyp = "Party's Loan Register";
            dateTimePicker1.Value = DateFrom;
            dateTimePicker2.Value = DateTo;
            dateTimePicker1.Enabled = false;
            dateTimePicker2.Enabled = false;

            label3.Enabled = false;
            textBox1.Enabled = false;
            this.Text = frmptyp;
            DecsOfReport = "Party's Loan Register, for the period of " + DateFrom.ToString(Database.dformat) + " to " + DateTo.ToString(Database.dformat);
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dtFinal = new DataTable();

          //  sql = "SELECT tblAccount.Acc_name, tblItemInfo.Item_name, res.Loan,  Sum(tblstock.Quantity)-res.Loan as WithoutLoan   , Sum(tblstock.Quantity) AS Total FROM (((SELECT tblVoucherinfo.Item_id, tblVoucherinfo.Ac_id, Sum(tblVoucherinfo.Totqty) AS Loan FROM tblVoucherinfo LEFT JOIN TblVoucherType ON tblVoucherinfo.Vt_id = TblVoucherType.Vt_id WHERE (((TblVoucherType.Vname)='LoanMemo' Or (TblVoucherType.Vname)='LoanSettlement')) GROUP BY tblVoucherinfo.Item_id, tblVoucherinfo.Ac_id)  AS res LEFT JOIN tblstock ON (res.Ac_id = tblstock.Ac_id) AND (res.Item_id = tblstock.Item_id)) LEFT JOIN tblItemInfo ON res.Item_id = tblItemInfo.Item_id) LEFT JOIN tblAccount ON res.Ac_id = tblAccount.Ac_id GROUP BY tblAccount.Acc_name, tblItemInfo.Item_name, res.Loan;";
            sql = "SELECT tblAccount.Acc_name, res.bankname, tblItemInfo.Item_name, res.Loan, Sum(tblstock.Quantity)-res.Loan AS WithoutLoan, Sum(tblstock.Quantity) AS Total FROM (((SELECT tblVoucherinfo.Item_id, tblVoucherinfo.Ac_id, Sum(tblVoucherinfo.Totqty) AS Loan, tblVoucherinfo.bankname FROM tblVoucherinfo LEFT JOIN TblVoucherType ON tblVoucherinfo.Vt_id = TblVoucherType.Vt_id WHERE (((TblVoucherType.Vname)='LoanMemo' Or (TblVoucherType.Vname)='LoanSettlement')) GROUP BY tblVoucherinfo.Item_id, tblVoucherinfo.Ac_id, tblVoucherinfo.bankname )  AS res LEFT JOIN tblstock ON (res.Item_id = tblstock.Item_id) AND (res.Ac_id = tblstock.Ac_id)) LEFT JOIN tblItemInfo ON res.Item_id = tblItemInfo.Item_id) LEFT JOIN tblAccount ON res.Ac_id = tblAccount.Ac_id GROUP BY tblAccount.Acc_name, res.bankname, tblItemInfo.Item_name, res.Loan;";
            dt = new DataTable();




            Database.GetSqlData(sql, dt);





            if (dt.Rows.Count == 0)
            {
                return false;
            }
            //int totdr = 0, totcr = 0;
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    totdr += int.Parse(dt.Rows[i]["Inward"].ToString());
            //    totcr += int.Parse(dt.Rows[i]["Outward"].ToString());
            //    if (totdr > totcr)
            //    {
            //        dt.Rows[i]["Stock"] = totdr - totcr;

            //    }
            //    else if (totcr > totdr)
            //    {
            //        dt.Rows[i]["Stock"] = totcr - totdr;
            //    }
            //    else
            //    {
            //        dt.Rows[i]["Stock"] = 0;
            //    }
            //}

            string[,] col = new string[0, 0];
            

            string[,] Cwidth = new string[6, 8] { 
           
           
            { "AccName", "300", "0" ,"","","","",""},
           
             { "BankName", "200", "1" ,"","","","",""},
            { "ItemName", "200", "1" ,"","","","",""},
            { "Loan", "100", "0" ,"","","","",""},
            { "WithoutLoan", "100", "0" ,"","","","",""},
            { "Total", "100", "0" ,"","","","",""},
            };

            CreateReport(dt, col, Cwidth);
            return true;
        }


        public bool PartyWhereGoods(DateTime DateFrom, DateTime DateTo, string accnmm, string frmcaption)
        {
            frmcap = frmcaption;
            stdt = DateFrom;
            endt = DateTo;
            textBox1.Text = accnmm;
            textBox1.ReadOnly = true;
            frmptyp = "Party's Where Goods";
            dateTimePicker1.Value = DateFrom;
            dateTimePicker2.Value = DateTo;
            dateTimePicker1.Enabled = false;
            dateTimePicker2.Enabled = false;

            label3.Enabled = true;
            textBox1.Enabled = true;
            this.Text = frmptyp;
            DecsOfReport = "Party's Where Goods, for the period of " + DateFrom.ToString(Database.dformat) + " to " + DateTo.ToString(Database.dformat);
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dtFinal = new DataTable();

      //     sql = "SELECT tblItemInfo.Item_name AS Item, tblstock.Marka AS Marka, tblstock.sroomno, tblstock.sslapno, tblstock.ssection, Sum(tblstock.Quantity) AS Quantity FROM (tblstock LEFT JOIN tblAccount ON tblstock.Ac_id = tblAccount.Ac_id) LEFT JOIN tblItemInfo ON tblstock.Item_id = tblItemInfo.Item_id WHERE (((tblAccount.Acc_name)='"+accnmm+"')) GROUP BY tblItemInfo.Item_name, tblstock.Marka, tblstock.sroomno, tblstock.sslapno, tblstock.ssection HAVING (((Sum(tblstock.Quantity))>0));";
            sql = "SELECT tblItemInfo.Item_name AS Item, tblstock.Marka AS Marka, tblstock.sroomno, tblstock.sslapno, tblstock.ssection, Sum(tblstock.Quantity) AS Quantity, tblVoucherDet.remark FROM ((tblstock LEFT JOIN tblAccount ON tblstock.Ac_id = tblAccount.Ac_id) LEFT JOIN tblItemInfo ON tblstock.Item_id = tblItemInfo.Item_id) LEFT JOIN tblVoucherDet ON (tblstock.Itemsr = tblVoucherDet.Itemsr) AND (tblstock.Vid = tblVoucherDet.Vi_id) WHERE (((tblAccount.Acc_name)='" + accnmm + "')) GROUP BY tblItemInfo.Item_name, tblstock.Marka, tblstock.sroomno, tblstock.sslapno, tblstock.ssection, tblVoucherDet.remark HAVING (((Sum(tblstock.Quantity))>0));";
            dt = new DataTable();





            Database.GetSqlData(sql, dt);


            if (dt.Rows.Count == 0)
            {
                return false;
            }


            string[,] col = new string[1, 3]{ { "Item", "1", "1" }};

            string[,] Cwidth = new string[7, 8] { 
           
           
            { "ItemName", "0", "0" ,"","","","",""},
           
            
            { "Marka", "200", "1" ,"","","","",""},
            { "RoomNo", "150", "1" ,"","","","",""},
            { "SlabNo", "150", "1" ,"Total","Total","","",""},
            { "Section", "150", "1" ,"","","","",""},
            { "Quantity", "200", "1" ,"|sum(Quantity)","|sum(Quantity)","","",""},
             { "Remark", "150", "1" ,"","","","",""},
            };

            CreateReport(dt, col, Cwidth);
            return true;


        }



        public bool PartyRegister(DateTime DateFrom, DateTime DateTo, string frmcaption)
        {
            frmcap = frmcaption;
            stdt = DateFrom;
            endt = DateTo;
           // textBox1.Text = accnmm;
            textBox1.ReadOnly = true;
            frmptyp = "Party's Item Wise Register";
            dateTimePicker1.Value = DateFrom;
            dateTimePicker2.Value = DateTo;
            dateTimePicker1.Enabled = false;
            dateTimePicker2.Enabled = false;

            label3.Enabled = false;
            textBox1.Enabled = false;
            this.Text = frmptyp;
            DecsOfReport = "Party's Item Wise Register, for the period of " + DateFrom.ToString(Database.dformat) + " to " + DateTo.ToString(Database.dformat);
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dtFinal = new DataTable();
           
            //sql = "SELECT First(tblVoucherinfo.Vdate) AS Vdate, tblAccount.Acc_name, tblstock.reffno, tblItemInfo.Item_name, Sum(IIf(tblstock.Quantity>0,tblstock.Quantity,0)) AS Inward, Sum(IIf(tblstock.Quantity<0,-1*tblstock.Quantity,0)) AS Outward, 0 AS Stock FROM ((tblVoucherinfo LEFT JOIN tblAccount ON tblVoucherinfo.Ac_id = tblAccount.Ac_id) LEFT JOIN tblstock ON tblVoucherinfo.Vi_id = tblstock.Vid) LEFT JOIN tblItemInfo ON tblstock.Item_id = tblItemInfo.Item_id WHERE (((tblVoucherinfo.Isbilled)=False)) GROUP BY tblAccount.Acc_name, tblstock.reffno, tblItemInfo.Item_name, 0 ORDER BY First(tblVoucherinfo.Vdate),tblAccount.Acc_name, tblItemInfo.Item_name;";
            sql = "SELECT res.Vdate, res.Acc_name, res.reffno, res.Item_name, Sum(res.Inward) AS Inward, Sum(res.Outward) AS Outward, res.Stock FROM (SELECT First(tblVoucherinfo.Vdate) AS Vdate, tblAccount.Acc_name, tblstock.reffno, tblItemInfo.Item_name, Sum(IIf(tblstock.Quantity>0,tblstock.Quantity,0)) AS Inward, Sum(IIf(tblstock.Quantity<0,-1*tblstock.Quantity,0)) AS Outward, 0 AS Stock FROM ((tblVoucherinfo LEFT JOIN tblAccount ON tblVoucherinfo.Ac_id = tblAccount.Ac_id) LEFT JOIN tblstock ON tblVoucherinfo.Vi_id = tblstock.Vid) LEFT JOIN tblItemInfo ON tblstock.Item_id = tblItemInfo.Item_id GROUP BY tblAccount.Acc_name, tblstock.reffno, tblItemInfo.Item_name, 0 ORDER BY First(tblVoucherinfo.Vdate), tblAccount.Acc_name, tblItemInfo.Item_name )  AS res LEFT JOIN tblVoucherinfo ON res.reffno = tblVoucherinfo.vnumber WHERE (((tblVoucherinfo.isbilled)=False) AND ((tblVoucherinfo.Vt_id)=1)) GROUP BY res.Vdate, res.Acc_name, res.reffno, res.Item_name, res.Stock;";
            dt = new DataTable();




            Database.GetSqlData(sql, dt);





            if (dt.Rows.Count == 0)
            {
                return false;
            }
            int totdr = 0, totcr = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                totdr = int.Parse(dt.Rows[i]["Inward"].ToString());
                totcr = int.Parse(dt.Rows[i]["Outward"].ToString());
                if (totdr > totcr)
                {
                    dt.Rows[i]["Stock"] = totdr - totcr;

                }
                else if (totcr > totdr)
                {
                    dt.Rows[i]["Stock"] = totcr - totdr;
                }
                else
                {
                    dt.Rows[i]["Stock"] = 0;
                }
            }

            string[,] col = new string[0, 0];

            string[,] Cwidth = new string[7, 8] { 
           
            { "Vdate", "150", "0" ,"","","","",""},
            { "PartyName", "250", "0" ,"","","","",""},
            { "Reff No", "100", "0" ,"","","","",""},
            { "ItemName", "200", "1" ,"","","Total","",""},
           
           
            { "Inward", "100", "0" ,"|sum(Inward)","","|sum(Inward)","",""},
            { "Outward", "100", "0" ,"|sum(Outward)","","|sum(Outward)","",""},
            { "Stock", "100", "0" ,"|sum(Stock)","","|sum(Stock)","",""},
            };

            CreateReport(dt, col, Cwidth);
            return true;


        }

        private void Report_Load(object sender, EventArgs e)
        {
            dateTimePicker1.CustomFormat = Database.dformat;
            dateTimePicker2.CustomFormat = Database.dformat;
           // dateTimePicker1.MaxDate = Database.enDate;
           // dateTimePicker2.MaxDate = Database.enDate;
            dateTimePicker1.MinDate = Database.stDate;
            dateTimePicker2.MinDate = Database.stDate;

            this.WindowState = FormWindowState.Maximized;
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            if (dataGridView1.Rows.Count == 0)
            {
                button1.Visible = false;
                button2.Visible = false;
                button4.Visible = false;
                button6.Visible = false;
            }           
        }

  
   


        


      

       
        private string getmonth(int Month)
        {
            string month = new DateTime(1900, Month, 1).ToString("MMMM");
            return month;
        }

       


 
        
        private void CreateReport(DataTable dt, string[,] col, string[,] Cwidth)
        {
            DateTime start;
            TimeSpan time;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            start = DateTime.Now;
            double TotBrokerage = 0;
            double TotRunn = 0;
           
            dataGridView1.Columns.Clear();
            for (int i1 = 0; i1 < dt.Columns.Count; i1++)
            {
                if (i1 >= col.GetLength(0) || col[i1, 1] == "0")
                {
                    dataGridView1.Columns.Add(dt.Columns[i1].ColumnName, Cwidth[i1, 0]);
                    dataGridView1.Columns[dt.Columns[i1].ColumnName].Width = int.Parse(Cwidth[i1, 1]);
                    if (int.Parse(Cwidth[i1, 1]) == 0)
                    {
                        dataGridView1.Columns[dt.Columns[i1].ColumnName].Visible = false;

                    }
                    if (dt.Columns[i1].DataType.Name == "Decimal")
                    {
                        dataGridView1.Columns[dt.Columns[i1].ColumnName].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dataGridView1.Columns[dt.Columns[i1].ColumnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    }
                    if (dt.Columns[i1].DataType.Name == "Int32")
                    {
                        dataGridView1.Columns[dt.Columns[i1].ColumnName].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dataGridView1.Columns[dt.Columns[i1].ColumnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    }
                    if (dt.Columns[i1].DataType.Name == "Double")
                    {
                        dataGridView1.Columns[dt.Columns[i1].ColumnName].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dataGridView1.Columns[dt.Columns[i1].ColumnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    }
                }

            }

            dataGridView1.Rows.Clear();
            dataGridView1.Rows.Add();
            string diff = "";
           
            if (col.GetLength(0) > 0)
            {
                DataTable dtGp1 = dt.DefaultView.ToTable(true, col[0, 0]);
                for (int i1 = 0; i1 < dtGp1.Rows.Count; i1++)
                {
                    DataRow[] dr1 = dt.Select(col[0, 0] + "='" + dtGp1.Rows[i1][0] + "'");
                    if (col[0, 1] == "1")//Group one Header
                    {
                        if (dt.Columns[0].DataType.Name == "DateTime" && DateTime.Parse(dtGp1.Rows[i1][col[0, 0]].ToString()).ToString("yyyy") == "1801")
                        
                        {
                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value = "";
                        }
                        else if (dt.Columns[0].DataType.Name == "DateTime")
                        {
                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value = DateTime.Parse(dtGp1.Rows[i1][col[0, 0]].ToString()).ToString("dd-MMM-yyyy");
                        }
                        else
                        {
                            DataTable dtSumh1 = dr1.CopyToDataTable();
                            for (int k1 = 0; k1 < dtSumh1.Columns.Count; k1++)
                            {
                                if (dtSumh1.Columns[dtSumh1.Columns[k1].ColumnName].DataType.Name == "DateTime" && DateTime.Parse(dtSumh1.Rows[0][dtSumh1.Columns[k1].ColumnName].ToString()).ToString("yyyy") == "1801")
                                {
                                    break;
                                }
                                if (Cwidth[k1, 6] == "")
                                {
                                }
                                else if (Cwidth[k1, 6].ToString().Substring(0, 1) == "|")
                                {
                                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dtSumh1.Columns[k1].ColumnName].Value =dtSumh1.Compute(Cwidth[k1, 6].ToString().TrimStart('|'), "");
                                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dtSumh1.Columns[k1].ColumnName].Style.Font = new System.Drawing.Font(dataGridView1.Font, FontStyle.Bold);
                                }
                                else if (Cwidth[k1, 6].ToString().Substring(0, 1) == "+")
                                {
                                    double val = double.Parse(dtSumh1.Compute(Cwidth[k1, 6].ToString().TrimStart('+'), "").ToString());
                                    if (val > 0)
                                    {
                                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dtSumh1.Columns[k1].ColumnName].Value = funs11.IndianCurr(val);
                                    }
                                    else
                                    {
                                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dtSumh1.Columns[k1].ColumnName].Value = funs11.IndianCurr(0);
                                    }
                                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dtSumh1.Columns[k1].ColumnName].Style.Font = new System.Drawing.Font(dataGridView1.Font, FontStyle.Bold);
                                }
                                else
                                {
                                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dtSumh1.Columns[k1].ColumnName].Value = Cwidth[k1, 6].ToString();
                                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dtSumh1.Columns[k1].ColumnName].Style.Font = new System.Drawing.Font(dataGridView1.Font, FontStyle.Bold);
                                }

                            }
                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value = dtGp1.Rows[i1][col[0, 0]].ToString();
                        }

                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Style.Font = new System.Drawing.Font(dataGridView1.Font, FontStyle.Bold);
                        dataGridView1.Rows.Add();
                    }

                  
                    if (col.GetLength(0) > 1)
                    {
                        DataTable dt2 = dr1.CopyToDataTable();
                        DataTable dtGp2 = dt2.DefaultView.ToTable(true, col[1, 0]);
                        for (int i2 = 0; i2 < dtGp2.Rows.Count; i2++)
                        {
                            DataRow[] dr2 = dt2.Select(col[1, 0] + "='" + dtGp2.Rows[i2][0] + "'");
                            if (col[1, 1] == "1") //Group Two Header
                            {
                                if (dt2.Columns[1].DataType.Name == "DateTime" && DateTime.Parse(dtGp2.Rows[i2][col[1, 0]].ToString()).ToString("yyyy") == "1801")
                                {
                                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value = "";
                                }
                                else if (dt2.Columns[1].DataType.Name == "DateTime")
                                {
                                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value = DateTime.Parse(dtGp2.Rows[i2][col[1, 0]].ToString()).ToString("dd-MMM-yyyy");
                                }
                                else
                                {
                                    DataTable dtSumh2 = dr2.CopyToDataTable();
                                    for (int k1 = 0; k1 < dtSumh2.Columns.Count; k1++)
                                    {
                                        if (dtSumh2.Columns[dtSumh2.Columns[k1].ColumnName].DataType.Name == "DateTime" && DateTime.Parse(dtSumh2.Rows[0][dtSumh2.Columns[k1].ColumnName].ToString()).ToString("yyyy") == "1801")
                                        {
                                            break;
                                        }
                                        if (Cwidth[k1, 7] == "")
                                        {
                                        }
                                        else if (Cwidth[k1, 7].ToString().Substring(0, 1) == "|")
                                        {                                            
                                                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dtSumh2.Columns[k1].ColumnName].Value = funs11.IndianCurr(double.Parse(dtSumh2.Compute(Cwidth[k1, 7].ToString().TrimStart('|'), "").ToString()));

                                                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dtSumh2.Columns[k1].ColumnName].Style.Font = new System.Drawing.Font(dataGridView1.Font, FontStyle.Bold);
                                                if (dtSumh2.Columns[dtSumh2.Columns[k1].ColumnName].DataType.Name == "Decimal" && Cwidth[k1, 2] == "1")
                                                {
                                                    if (double.Parse(dtSumh2.Compute(Cwidth[k1, 7].ToString().TrimStart('|'), "").ToString()) == 0)
                                                    {
                                                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dtSumh2.Columns[k1].ColumnName].Value = "";
                                                    }

                                                }
                                        }
                                       
                                        else if (Cwidth[k1, 7].ToString().Substring(0, 1) == "+")
                                        {
                                            double val = double.Parse(dtSumh2.Compute(Cwidth[k1, 7].ToString().TrimStart('+'), "").ToString());
                                            if (val > 0)
                                            {
                                                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dtSumh2.Columns[k1].ColumnName].Value = funs11.IndianCurr(val);
                                            }
                                            else
                                            {
                                                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dtSumh2.Columns[k1].ColumnName].Value = funs11.IndianCurr(0);
                                            }
                                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dtSumh2.Columns[k1].ColumnName].Style.Font = new System.Drawing.Font(dataGridView1.Font, FontStyle.Bold);
                                        }
                                        else if (dt.Columns[dt.Columns[k1].ColumnName].DataType.Name == "Decimal" && Cwidth[k1, 2] == "1")
                                        {

                                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dtSumh2.Columns[k1].ColumnName].Value = "";
                                        }
                                        else
                                        {
                                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dtSumh2.Columns[k1].ColumnName].Value = Cwidth[k1, 7].ToString();
                                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dtSumh2.Columns[k1].ColumnName].Style.Font = new System.Drawing.Font(dataGridView1.Font, FontStyle.Bold);
                                        }

                                    }
                                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value = dtGp2.Rows[i2][col[1, 0]].ToString();
                                }
                               
                                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Style.Font = new System.Drawing.Font(dataGridView1.Font, FontStyle.Bold);
                                dataGridView1.Rows.Add();
                            }

                            //detail section if two group
                            for (int j2 = 0; j2 < dr2.Length; j2++)
                            {
                                for (int k2 = 0; k2 < dt2.Columns.Count; k2++)
                                {
                                    if (k2 >= col.GetLength(0) || col[k2, 1] == "0")
                                    {
                                        if (j2 != 0 && dr2[j2][k2].ToString() == dr2[j2 - 1][k2].ToString() && k2 < 2)
                                        {
                                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dt2.Columns[k2].ColumnName].Value = "";
                                        }
                                        else if (dt.Columns[dt2.Columns[k2].ColumnName].DataType.Name == "DateTime" && DateTime.Parse(dr2[j2][k2].ToString()).ToString("yyyy") == "1801")
                                        {
                                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dt2.Columns[k2].ColumnName].Value = "";
                                        }

                                        else if (dt.Columns[dt2.Columns[k2].ColumnName].DataType.Name == "DateTime")
                                        {
                                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dt2.Columns[k2].ColumnName].Value = DateTime.Parse(dr2[j2][k2].ToString()).ToString("dd-MMM-yyyy");
                                        }
                                       
                                        else if (dt.Columns[dt2.Columns[k2].ColumnName].DataType.Name == "Decimal")
                                        {
                                            if (dr2[j2][k2].ToString() == "")
                                            {
                                                dr2[j2][k2] = 0;
                                            }
                                            if (Cwidth[k2, 2] == "1" && double.Parse(dr2[j2][k2].ToString()) == 0)
                                            {
                                                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dt2.Columns[k2].ColumnName].Value = "";
                                            }
                                            else
                                            {
                                                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dt2.Columns[k2].ColumnName].Value = dr2[j2][k2].ToString();
                                            }

                                        }
                                        else if (dt.Columns[dt2.Columns[k2].ColumnName].DataType.Name == "Int32" && Cwidth[k2, 2] == "1" && double.Parse(dr2[j2][k2].ToString()) == 0)
                                        {
                                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dt2.Columns[k2].ColumnName].Value = "";
                                        }
                                        
                                        else if (dt.Columns[dt2.Columns[k2].ColumnName].DataType.Name == "Double")
                                        {
                                            if (dr2[j2][k2].ToString() == "")
                                            {
                                                dr2[j2][k2] = 0;
                                            }
                                            if (Cwidth[k2, 2] == "1" && double.Parse(dr2[j2][k2].ToString()) == 0)
                                            {
                                                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dt2.Columns[k2].ColumnName].Value = "";
                                            }
                                            else
                                            {
                                                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dt2.Columns[k2].ColumnName].Value =dr2[j2][k2].ToString();
                                            }
                                        }

                                        else
                                        {
                                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dt2.Columns[k2].ColumnName].Value = dr2[j2][k2].ToString();
                                        }

                                    }
                                }
                                dataGridView1.Rows.Add();

                            }
                           
                            if (col[1, 2] == "1") //Group two Footer
                            {
                                DataTable dtSum2 = dr2.CopyToDataTable();
                                for (int k2 = 0; k2 < dtSum2.Columns.Count; k2++)
                                {
                                    if (Cwidth[k2, 5] == "")
                                    {

                                    }
                                    else if (Cwidth[k2, 5].ToString().Substring(0, 1) == "|")
                                    {
                                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dtSum2.Columns[k2].ColumnName].Value = dtSum2.Compute(Cwidth[k2, 5].ToString().TrimStart('|'), "").ToString();
                                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dtSum2.Columns[k2].ColumnName].Style.Font = new System.Drawing.Font(dataGridView1.Font, FontStyle.Bold);
                                    }
                                    else if (Cwidth[k2, 5].ToString().Substring(0, 1) == "+")
                                    {
                                        double val = double.Parse(dtSum2.Compute(Cwidth[k2, 5].ToString().TrimStart('+'), "").ToString());
                                        if (val > 0)
                                        {
                                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dtSum2.Columns[k2].ColumnName].Value = funs11.IndianCurr(val);
                                        }
                                        else
                                        {
                                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dtSum2.Columns[k2].ColumnName].Value = funs11.IndianCurr(0);
                                        }
                                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dtSum2.Columns[k2].ColumnName].Style.Font = new System.Drawing.Font(dataGridView1.Font, FontStyle.Bold);
                                    }
                                    else if (Cwidth[k2, 5].ToString().Substring(0, 1) == ">")
                                    {

                                        double val = double.Parse(dtSum2.Compute(Cwidth[k2, 5].ToString().Split('>')[2], "").ToString());
                                        if (val <= 0 || val > double.Parse(Cwidth[k2, 5].ToString().Split('>')[1]))
                                        {
                                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dtSum2.Columns[k2].ColumnName].Value = funs11.IndianCurr(val);
                                            TotBrokerage += val;
                                        }
                                        else
                                        {
                                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dtSum2.Columns[k2].ColumnName].Value = funs11.IndianCurr(0);
                                        }
                                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dtSum2.Columns[k2].ColumnName].Style.Font = new System.Drawing.Font(dataGridView1.Font, FontStyle.Bold);

                                    }
                                    else if (Cwidth[k2, 5].ToString().Substring(0, 1) == "^")
                                    {
                                        double val = double.Parse(dtSum2.Compute(Cwidth[k2, 5].ToString().TrimStart('^'), "").ToString());
                                        TotRunn = TotRunn + val;
                                        if (TotRunn > 0)
                                        {
                                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dtSum2.Columns[k2].ColumnName].Value = funs11.IndianCurr(TotRunn);
                                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["Dr/Cr"].Value = "Dr.";
                                        }
                                        else
                                        {
                                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dtSum2.Columns[k2].ColumnName].Value = funs11.IndianCurr(-1 * TotRunn);
                                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["Dr/Cr"].Value = "Cr.";
                                        }
                                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dtSum2.Columns[k2].ColumnName].Style.Font = new System.Drawing.Font(dataGridView1.Font, FontStyle.Bold);
                                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["Dr/Cr"].Style.Font = new System.Drawing.Font(dataGridView1.Font, FontStyle.Bold);
                                    }
                                    else
                                    {
                                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dtSum2.Columns[k2].ColumnName].Value = Cwidth[k2, 5].ToString();
                                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dtSum2.Columns[k2].ColumnName].Style.Font = new System.Drawing.Font(dataGridView1.Font, FontStyle.Bold);


                                    }
                                }

                                  dataGridView1.Rows.Add();
                            }
                        }
                    }

                    else //detail section if only one group
                    {
                        for (int j1 = 0; j1 < dr1.Length; j1++)
                        {
                            for (int k1 = 0; k1 < dt.Columns.Count; k1++)
                            {
                                if (k1 >= col.GetLength(0) || col[k1, 0] == "0")
                                {

                                    if (j1 != 0 && dr1[j1][k1].ToString() == dr1[j1 - 1][k1].ToString() && k1 < 1)
                                    {
                                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dt.Columns[k1].ColumnName].Value = "";
                                    }

                                    else if (dt.Columns[dt.Columns[k1].ColumnName].DataType.Name == "DateTime" && DateTime.Parse(dr1[j1][k1].ToString()).ToString("yyyy") == "1801")
                                    {
                                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dt.Columns[k1].ColumnName].Value = "";
                                    }
                                    else if (dt.Columns[dt.Columns[k1].ColumnName].DataType.Name == "DateTime")
                                    {
                                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dt.Columns[k1].ColumnName].Value = DateTime.Parse(dr1[j1][k1].ToString()).ToString("dd-MMM-yyyy");
                                    }
                                    else if (dt.Columns[dt.Columns[k1].ColumnName].DataType.Name == "Decimal")
                                    {
                                        if (dr1[j1][k1].ToString() == "") dr1[j1][k1] = 0;
                                        if (Cwidth[k1, 2] == "1" && double.Parse(dr1[j1][k1].ToString()) == 0)
                                        {
                                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dt.Columns[k1].ColumnName].Value = "";
                                        }
                                        else
                                        {
                                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dt.Columns[k1].ColumnName].Value = funs11.IndianCurr(double.Parse(dr1[j1][k1].ToString()));
                                        }
                                    }
                                    
                                    else if (dt.Columns[dt.Columns[k1].ColumnName].DataType.Name == "Int32" && Cwidth[k1, 2] == "1" && double.Parse(dr1[j1][k1].ToString()) == 0)
                                    {

                                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dt.Columns[k1].ColumnName].Value = "";
                                    }
                                    else if (dt.Columns[dt.Columns[k1].ColumnName].DataType.Name == "Int32")
                                    {

                                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dt.Columns[k1].ColumnName].Value = dr1[j1][k1].ToString();
                                    }
                                    else if (dt.Columns[dt.Columns[k1].ColumnName].DataType.Name == "Double")
                                    {
                                        if (dr1[j1][k1].ToString() == "") dr1[j1][k1] = 0.0;
                                        if (Cwidth[k1, 2] == "1" && double.Parse(dr1[j1][k1].ToString()) == 0)
                                        {
                                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dt.Columns[k1].ColumnName].Value = "";
                                        }
                                        else
                                        {
                                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dt.Columns[k1].ColumnName].Value =dr1[j1][k1].ToString();
                                        }
                                    }
                                    
                                    else
                                    {
                                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dt.Columns[k1].ColumnName].Value = dr1[j1][k1].ToString();
                                    }

                                }

                            }
                            dataGridView1.Rows.Add();

                        }
                    }

                    if (col[0, 2] == "1")//Group one Footer
                    {
                        DataTable dtSum1 = dr1.CopyToDataTable();
                        for (int k1 = 0; k1 < dtSum1.Columns.Count; k1++)
                        {
                            if (Cwidth[k1, 4] == "")
                            {

                            }
                            else if (Cwidth[k1, 4].ToString().Substring(0, 1) == "|")
                            {
                                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dtSum1.Columns[k1].ColumnName].Value = dtSum1.Compute(Cwidth[k1, 4].ToString().TrimStart('|'), "").ToString();
                                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dtSum1.Columns[k1].ColumnName].Style.Font = new System.Drawing.Font(dataGridView1.Font, FontStyle.Bold);
                            }
                            else if (Cwidth[k1, 4].ToString().Substring(0, 1) == "+")
                            {
                                double val = double.Parse(dtSum1.Compute(Cwidth[k1, 4].ToString().TrimStart('+'), "").ToString());
                                if (val > 0)
                                {
                                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dtSum1.Columns[k1].ColumnName].Value = funs11.IndianCurr(val);
                                }
                                else
                                {
                                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dtSum1.Columns[k1].ColumnName].Value = funs11.IndianCurr(0);
                                }
                                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dtSum1.Columns[k1].ColumnName].Style.Font = new System.Drawing.Font(dataGridView1.Font, FontStyle.Bold);
                            }
                            else
                            {
                                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dtSum1.Columns[k1].ColumnName].Value = Cwidth[k1, 4].ToString();
                                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dtSum1.Columns[k1].ColumnName].Style.Font = new System.Drawing.Font(dataGridView1.Font, FontStyle.Bold);
                            }

                        }

                        dataGridView1.Rows.Add();

                    }

                }

            }

            else //detail section if no group valable
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (i != 0 && dt.Rows[i][j].ToString() == dt.Rows[i - 1][j].ToString() && j < 1)
                        {
                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dt.Columns[j].ColumnName].Value = "";
                        }
                             //if (dt.Columns[0].DataType.Name == "DateTime" && DateTime.Parse(dtGp1.Rows[i1][col[0, 0]].ToString()).ToString() == Database.stDate.AddDays(-1).ToString(Database.dformat) && frmptyp == "Ledger")
                        else if (dt.Columns[dt.Columns[j].ColumnName].DataType.Name == "DateTime" && DateTime.Parse(dt.Rows[i][j].ToString()).ToString() == Database.stDate.AddDays(-1).ToString() && frmptyp == "Ledger")
                        {
                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dt.Columns[j].ColumnName].Value = "";
                        }


                        else if (dt.Columns[dt.Columns[j].ColumnName].DataType.Name == "DateTime")
                        {
                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dt.Columns[j].ColumnName].Value = DateTime.Parse(dt.Rows[i][j].ToString()).ToString("dd-MMM-yyyy").Replace("01-Feb-1801", "");
                        }
                        else if (dt.Columns[dt.Columns[j].ColumnName].DataType.Name == "Decimal" && Cwidth[j, 2] == "1" && double.Parse(dt.Rows[i][j].ToString()) == 0)
                        {
                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dt.Columns[j].ColumnName].Value = "";
                        }
                        else if (dt.Columns[dt.Columns[j].ColumnName].DataType.Name == "Decimal")
                        {
                            if (dt.Rows[i][j].ToString() == "")
                            {
                                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dt.Columns[j].ColumnName].Value = "";
                            }
                            else
                            {
                                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dt.Columns[j].ColumnName].Value = funs11.IndianCurr(double.Parse(dt.Rows[i][j].ToString()));
                            }

                        }
                        else if (dt.Columns[dt.Columns[j].ColumnName].DataType.Name == "Int32" && Cwidth[j, 2] == "1" && double.Parse(dt.Rows[i][j].ToString()) == 0)
                        {

                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dt.Columns[j].ColumnName].Value = "";
                        }
                        else if (dt.Columns[dt.Columns[j].ColumnName].DataType.Name == "Int32")
                        {

                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dt.Columns[j].ColumnName].Value = dt.Rows[i][j].ToString();

                        }
                        else if (dt.Columns[dt.Columns[j].ColumnName].DataType.Name == "Double" && Cwidth[j, 2] == "1" && double.Parse(dt.Rows[i][j].ToString()) == 0)
                        {

                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dt.Columns[j].ColumnName].Value = "";
                        }

                        else if (dt.Columns[dt.Columns[j].ColumnName].DataType.Name == "Double")
                        {
                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dt.Columns[j].ColumnName].Value = dt.Rows[i][j].ToString();

                        }
                        else if (dt.Rows[i][j].ToString().IndexOf("<b>") > -1)
                        {
                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dt.Columns[j].ColumnName].Value = dt.Rows[i][j].ToString().Replace("<b>", "");
                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dt.Columns[j].ColumnName].Style.Font = new System.Drawing.Font(dataGridView1.Font, FontStyle.Bold);
                        }
                        else
                        {
                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dt.Columns[j].ColumnName].Value = dt.Rows[i][j].ToString();
                        }

                    }
                    dataGridView1.Rows.Add();
                }

            }


            for (int k = 0; k < dt.Columns.Count; k++)
            {

                if (Cwidth[k, 3] == "")
                {

                }
                else if (Cwidth[k, 3].ToString().Substring(0, 1) == "|")
                {
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dt.Columns[k].ColumnName].Value = dt.Compute(Cwidth[k, 3].ToString().TrimStart('|'), "").ToString();
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dt.Columns[k].ColumnName].Style.Font = new System.Drawing.Font(dataGridView1.Font, FontStyle.Bold);
                }
                else if (Cwidth[k, 3].ToString().Substring(0, 1) == "+")
                {
                    double val = double.Parse(dt.Compute(Cwidth[k, 3].ToString().TrimStart('|'), "").ToString());
                    if (val > 0)
                    {
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dt.Columns[k].ColumnName].Value = funs11.IndianCurr(val);
                    }
                    else
                    {
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dt.Columns[k].ColumnName].Value = funs11.IndianCurr(0);
                    }
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dt.Columns[k].ColumnName].Style.Font = new System.Drawing.Font(dataGridView1.Font, FontStyle.Bold);
                }
                else if (Cwidth[k, 3].ToString().Substring(0, 1) == ">")
                {
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dt.Columns[k].ColumnName].Value = funs11.IndianCurr(TotBrokerage);
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dt.Columns[k].ColumnName].Style.Font = new System.Drawing.Font(dataGridView1.Font, FontStyle.Bold);
                }
                else
                {
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dt.Columns[k].ColumnName].Value = Cwidth[k, 3].ToString();
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[dt.Columns[k].ColumnName].Style.Font = new System.Drawing.Font(dataGridView1.Font, FontStyle.Bold);


                }

            }

            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

        }

        private void Report_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                this.Dispose();
            }
            else if (e.Control && e.KeyCode == Keys.P)
            {
                if (dataGridView1.Rows.Count == 0)
                {
                    return;
                }
                string tPath = Path.GetTempPath() + DateTime.Now.ToString("yyMMddhmmssfff") + ".pdf";
               // ExportToPdf(tPath);
                GC.Collect();
              
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private int IsDocumentNumber(String str)
        {

            return Database.GetScalarInt("SELECT DISTINCT tblVoucherinfo.Vi_id, TBLVOUCHERTYPE.Short & ' ' & Format(tblVoucherinfo.Vdate,'yyyymmdd' & ' ' & tblVoucherinfo.Vnumber) AS DocNumber FROM (tblVoucherinfo LEFT JOIN TblACCOUNT ON tblVoucherinfo.Ac_id = TblACCOUNT.Ac_id) LEFT JOIN TBLVOUCHERTYPE ON TBLVOUCHERINFO.Vt_id = TBLVOUCHERTYPE.Vt_id WHERE (((TBLVOUCHERINFO.Vt_id)=[TBLVOUCHERINFO].[Vt_id]) AND (TBLVOUCHERTYPE.Short & ' ' & Format(tblVoucherinfo.Vdate,'yyyymmdd' & ' ' & tblVoucherinfo.Vnumber)='" + str + "'))");
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            String clkStr = "";
            if (dataGridView1.CurrentCell.Value != null)
            {
                clkStr = dataGridView1.CurrentCell.Value.ToString();
            }
            if (IsDocumentNumber(clkStr) != 0)
            {
                int vid = IsDocumentNumber(clkStr);
                if (Database.GetScalarInt("Select F_id from tblvoucherinfo where vi_id=" + vid) == Database.F_id)
                {
                    funs11.OpenFrm(this, IsDocumentNumber(clkStr));
                }
                else
                {
                    MessageBox.Show("This voucher can't be Open Because this  is not related to current Financial Year");
                }

             
            }
            if (frmcap == "Party's Item Wise Register")
            {
                int vid = 0;
                vid = Database.GetScalarInt("SELECT tblVoucherinfo.Vi_id FROM tblVoucherinfo LEFT JOIN tblAccount ON tblVoucherinfo.Ac_id = tblAccount.Ac_id WHERE (((tblVoucherinfo.Vt_id)=1) AND  ((tblVoucherinfo.vnumber)=" + dataGridView1.CurrentRow.Cells["reffno"].Value.ToString() + "))");


                if (Database.GetScalarInt("Select F_id from tblvoucherinfo where vi_id=" + vid) == Database.F_id)
                {
                    funs11.OpenFrm(this, vid);
                }
                else
                {
                    MessageBox.Show("This voucher can't be Open Because this  is not related to current Financial Year");
                }
            }

            //if (frmcap == "Party's Item Wise Register")
            //{

            //    int vid = 0;
            //    vid = Database.GetScalarInt("SELECT tblVoucherinfo.Vi_id FROM tblVoucherinfo LEFT JOIN tblAccount ON tblVoucherinfo.Ac_id = tblAccount.Ac_id WHERE (((tblVoucherinfo.Vt_id)=1) AND  ((tblVoucherinfo.vnumber)=" + dataGridView1.CurrentRow.Cells["reffno"].Value.ToString() + "))");

            //    if (vid == 0)
            //    {
            //        return;
            //    }
            //    else
            //    {

            //        funs11.OpenFrm(this, vid, "Inward");
            //        //PartyRegister(dateTimePicker1.Value, dateTimePicker2.Value, frmcap);
            //    }
            //}
        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                return;
            }
            string tPath = Path.GetTempPath() + DateTime.Now.ToString("yyMMddhmmssfff") + ".pdf";
            ExportToPdf(tPath);
            GC.Collect();
            

        }
   

    

        public void ExportToPdf(string tPath)
        {
            frmptyp2 = frmptyp;
            DecsOfReport2 = DecsOfReport;
            str2 = str;
            dataGridView2 = dataGridView1;


            FileStream fs = new FileStream(tPath, FileMode.Create, FileAccess.Write, FileShare.None);
            iTextSharp.text.Rectangle rec;
            Document document;
            int Twidth = 0;
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                Twidth += dataGridView1.Columns[i].Width;
            }
            if (Twidth == 2000)
            {
                document = new Document(PageSize.A4.Rotate(), 20f, 10f, 20f, 10f);
            }
           
            else
            {
                document = new Document(PageSize.A4, 20f, 10f, 20f, 10f);
            }

            Pagesize = "A4";
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            writer.PageEvent = new MainTextEventsHandler();
            document.Open();
            HTMLWorker hw = new HTMLWorker(document);
             str = "";
            str += @"<body> <font size='1'><table border=1> <tr>";
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                string align = "";
                string bold = "";
                int width = 0;

                if (Twidth == 2000)
                {
                    width = dataGridView1.Columns[i].Width / 20;
                }
                else
                {
                    width = dataGridView1.Columns[i].Width / 10;
                }

                if (dataGridView1.Columns[i].HeaderCell.Style.Alignment == DataGridViewContentAlignment.MiddleRight)
                {
                    align = "text-align:right;";
                }

                bold = "font-weight: bold;";

                if (width != 0)
                {
                    str += "<th width=" + width + "%  style='" + align + bold + "'>" + dataGridView1.Columns[i].HeaderText.ToString() + "</th> ";
                }

            }

            str += "</tr>";

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                str += "<tr> ";
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    int width = 0;
                    if (Twidth == 2000)
                    {
                        width = dataGridView1.Rows[i].Cells[j].Size.Width / 20;

                    }
                    else
                    {
                        width = dataGridView1.Rows[i].Cells[j].Size.Width / 10;
                    }

                    if (width != 0)
                    {

                        if (dataGridView1.Rows[i].Cells[j].Value != null)
                        {
                            string align = "";
                            string bold = "";
                            string colspan = "";

                            if (dataGridView1.Columns[j].DefaultCellStyle.Alignment == DataGridViewContentAlignment.MiddleRight)
                            {
                                align = "text-align:right;";
                            }

                            if (dataGridView1.Rows[i].Cells[j].Style.Font != null && dataGridView1.Rows[i].Cells[j].Style.Font.Bold == true)
                            {
                                bold = "font-weight: bold;";
                            }
                            if (j == 0 && dataGridView1.Rows[i].Cells[0].Value.ToString() != "" && dataGridView1.Rows[i].Cells[1].Value == null && dataGridView1.Rows[i].Cells[2].Value == null)
                            {
                                colspan = "colspan= '2'";
                            }

                            
                            if (dataGridView1.Rows[i].Cells[j].Value.ToString().Trim() == "")
                            {
                                str += "<td> &nbsp; </td>";
                            }
                            else
                            {
                                str += "<td " + colspan + "  style='" + align + bold + "'>" + dataGridView1.Rows[i].Cells[j].Value.ToString() + "</td> ";
                            }
                            if (j == 0 && dataGridView1.Rows[i].Cells[0].Value.ToString() != "" && dataGridView1.Rows[i].Cells[1].Value == null && dataGridView1.Rows[i].Cells[2].Value == null)
                            {
                                j++;
                            }
                           
                        }
                        else
                        {
                            
                           
                                str += "<td> &nbsp; </td>";
                           
                        }
                    }
                }
                str += "</tr> ";
            }
            str += "</table></font></body>";

            StringReader sr = new StringReader(str);
            hw.Parse(sr);
            document.Close();

        }
        internal class MainTextEventsHandler : PdfPageEventHelper
        {
            public override void OnStartPage(PdfWriter writer, Document document)
            {
                base.OnStartPage(writer, document);

                bool sta = false;
                DataTable dtRheader = new DataTable();
                // Database.GetSqlData("select * from company", dtRheader);
                PdfPTable table = new PdfPTable(1);
                PdfPCell cell = new PdfPCell();

                //if (sta == false)
                //{
                //    cell.Phrase = new Phrase(dtRheader.Rows[0]["name"].ToString());
                //    cell.BorderWidth = 0f;
                //    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                //    table.AddCell(cell);
                //    cell.Phrase = new Phrase(dtRheader.Rows[0]["Address1"].ToString());
                //    table.AddCell(cell);
                //    cell.Phrase = new Phrase(dtRheader.Rows[0]["Address2"].ToString());
                //    table.AddCell(cell);
                //    cell.Phrase = new Phrase(Report.DecsOfReport2);
                //    table.AddCell(cell);
                //    cell.Phrase = new Phrase("\n");
                //    table.AddCell(cell);
                //}
                //else
                //{
                   // cell.Phrase = new Phrase("\n");
                    cell.BorderWidth = 0f;
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                   // table.AddCell(cell);
                   // cell.Phrase = new Phrase("\n");
                    //table.AddCell(cell);
                    //cell.Phrase = new Phrase("\n");
                    //table.AddCell(cell);
                    cell.Phrase = new Phrase(Report.DecsOfReport2);
                    table.AddCell(cell);
                    cell.Phrase = new Phrase("\n");
                    table.AddCell(cell);
               // }
                document.Add(table);
            }


            public override void OnEndPage(PdfWriter writer, Document document)
            {
                
                base.OnEndPage(writer, document);
                string text = "";
                text += "Page No-" + document.PageNumber;
                PdfContentByte cb = writer.DirectContent;
                cb.BeginText();
                BaseFont bf = BaseFont.CreateFont();
                cb.SetFontAndSize(bf, 8);
                if (Pagesize== "A4")
                {
                    cb.SetTextMatrix(530, 8);
                }
                else if (Pagesize == "A5")
                {
                    cb.SetTextMatrix(350, 8);
                }
              
                cb.ShowText(text);
                cb.EndText();




            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                return;
            }

            string tPath = Path.GetTempPath() + DateTime.Now.ToString("yyMMddhmmssfff") + ".pdf";
            ExportToPdf(tPath);
            GC.Collect();


            PdfReader frm = new PdfReader();
            frm.LoadFile(tPath);
            frm.Show();
            

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                return;
            }

            SaveFileDialog ofd = new SaveFileDialog();
            ofd.Filter = "Adobe Acrobat(*.pdf) | *.pdf";

            if (DialogResult.OK == ofd.ShowDialog())
            {
                ExportToPdf(ofd.FileName);
                MessageBox.Show("Export Successfully!!");
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {

            if (dataGridView1.Rows.Count == 0)
            {
                return;
            }
            Object misValue = System.Reflection.Missing.Value;
            Excel.Application apl = new Microsoft.Office.Interop.Excel.Application();
            Excel.Workbook wb = (Excel.Workbook)apl.Workbooks.Add(misValue);
            Excel.Worksheet ws;
            ws = (Excel.Worksheet)wb.Worksheets[1];

            int lno = 1;
            DataTable dtExcel = new DataTable();

            //DataTable dtRheader = new DataTable();
            //Database.GetSqlData("select * from company", dtRheader);

            //ws.Cells[lno, 1] = dtRheader.Rows[0]["name"].ToString();
            //ws.get_Range(ws.Cells[lno, 1], ws.Cells[lno, dataGridView1.Columns.Count]).Merge(Type.Missing);
            //ws.get_Range(ws.Cells[lno, 1], ws.Cells[lno, dataGridView1.Columns.Count]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //ws.get_Range(ws.Cells[lno, 1], ws.Cells[lno, dataGridView1.Columns.Count]).Font.Bold = true;
            //lno++;

            //ws.Cells[lno, 1] = dtRheader.Rows[0]["Address1"].ToString();
            //ws.get_Range(ws.Cells[lno, 1], ws.Cells[lno, dataGridView1.Columns.Count]).Merge(Type.Missing);
            //ws.get_Range(ws.Cells[lno, 1], ws.Cells[lno, dataGridView1.Columns.Count]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //ws.get_Range(ws.Cells[lno, 1], ws.Cells[lno, dataGridView1.Columns.Count]).Font.Bold = true;
            //lno++;

            //ws.Cells[lno, 1] = dtRheader.Rows[0]["Address2"].ToString();
            //ws.get_Range(ws.Cells[lno, 1], ws.Cells[lno, dataGridView1.Columns.Count]).Merge(Type.Missing);
            //ws.get_Range(ws.Cells[lno, 1], ws.Cells[lno, dataGridView1.Columns.Count]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //ws.get_Range(ws.Cells[lno, 1], ws.Cells[lno, dataGridView1.Columns.Count]).Font.Bold = true;
            //lno++;



            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {



                //if (dataGridView1.Columns[i].HeaderCell.Style.Alignment == DataGridViewContentAlignment.MiddleRight)
                //{
                //    ws.get_Range(ws.Cells[1, i + 1], ws.Cells[1, i + 1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                //    ws.get_Range(ws.Cells[1, i + 1], ws.Cells[1, i + 1]).NumberFormat = "0,0.00";
                //}
                //ws.get_Range(ws.Cells[i + 1, i + 1], ws.Cells[i + 1, i + 1]).ColumnWidth = dataGridView1.Columns[i].Width / 11.5;
                ws.Cells[1, i + 1] = dataGridView1.Columns[i].HeaderText.ToString();
               // ws.get_Range(ws.Cells[1, 1], ws.Cells[1, dataGridView1.Columns.Count]).Font.Bold = true;


            }

          
            var data = new object[dataGridView1.Rows.Count, dataGridView1.Columns.Count];



            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {

                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {

                   
                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                    {
                        data[i, j] = dataGridView1.Rows[i].Cells[j].Value.ToString().Replace(",", "");

                    }
                   
                }



            }

            var startcell = (Excel.Range)ws.Cells[2, 1];
            var endcell = (Excel.Range)ws.Cells[dataGridView1.Rows.Count + 1, dataGridView1.Columns.Count];
            var writerange = ws.Range[startcell, endcell];
            writerange.Value = data;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {

                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {

                    if (dataGridView1.Columns[j].ToString().IndexOf("date") > -1)
                    {
                   
                        if (dataGridView1.Rows[i].Cells[j].Value != null)
                        {
                            ws.Cells[i + 6, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString().Replace(",", "");
                        }
                    }
                   

                }



            }

            Excel.Range last = ws.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);
            ws.get_Range("A1", last).WrapText = true;
            apl.Visible = true;
        }

        private void Report_FormClosing(object sender, FormClosingEventArgs e)
        {

             string[] files = Directory.GetFiles(Path.GetTempPath());
             foreach (string file in files)
            {

                try
                {
                    File.Delete(file);
                }
                catch
                {

                }
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
                String strCombo;
                if (frmptyp == "Item wise")
                {
                    strCombo = "select distinct [Item_name] from tblItemInfo where Item_name<>'' order by item_name";

                    textBox1.Text = SelectCombo.ComboKeypress(this, e.KeyChar, strCombo, e.KeyChar.ToString(), 0);
                }

                else if (frmptyp == "Party's Stock")
                {
                    strCombo = "SELECT tblAccount.Acc_name as Name FROM tblAccount ORDER BY tblAccount.Acc_name";

                    textBox1.Text = SelectCombo.ComboKeypress(this, e.KeyChar, strCombo, e.KeyChar.ToString(), 0);
                }

                else if (frmptyp == "Party's Where Goods")
                {
                    strCombo = "SELECT tblAccount.Acc_name as Name FROM tblAccount ORDER BY tblAccount.Acc_name";

                    textBox1.Text = SelectCombo.ComboKeypress(this, e.KeyChar, strCombo, e.KeyChar.ToString(), 0);
                }


                else if (frmptyp == "Daily Register")
                {
                    strCombo = "SELECT tblAccount.Acc_name as Name FROM tblAccount ORDER BY tblAccount.Acc_name";

                    textBox1.Text = SelectCombo.ComboKeypress(this, e.KeyChar, strCombo, e.KeyChar.ToString(), 0);
                }

                else if (frmptyp == "Party's Item Wise Register")
                {
                    strCombo = "SELECT tblAccount.Acc_name as Name FROM tblAccount ORDER BY tblAccount.Acc_name";

                    textBox1.Text = SelectCombo.ComboKeypress(this, e.KeyChar, strCombo, e.KeyChar.ToString(), 0);
                }
                else if (frmptyp == "Party's Item Wise Register All")
                {
                    strCombo = "SELECT tblAccount.Acc_name as Name FROM tblAccount ORDER BY tblAccount.Acc_name";

                    textBox1.Text = SelectCombo.ComboKeypress(this, e.KeyChar, strCombo, e.KeyChar.ToString(), 0);
                }

        }

        private string GetPapersize()
        {
            return Database.GetScalarText("Select PaperSize from VOUCHERTYPE where Name='" + frmptyp + "' ");
        }

    
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                String clkStr = "";
                if (dataGridView1.CurrentCell.Value != null)
                {
                    clkStr = dataGridView1.CurrentCell.Value.ToString();
                }
                
                
                
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            button3_Click(sender, e);

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)

        {
            //if (frmcap == "ItemDetail")
            //{
            //    int v_id = int.Parse(tdt.Rows[e.RowIndex]["Vi_id"].ToString());
            //    string fname = tdt.Rows[e.RowIndex]["Vname"].ToString();
            //    funs11.OpenFrm(this, v_id, fname);
            //}
            //if (frmcap == "ItemLedger")
            //{
            //    Report gg = new Report();
            //    gg.StockReportItemwise(Database.stDate, Database.enDate,item, "ItemDetail");
            //    gg.Size = this.Size;
            //    gg.Show();
            //}
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

   

    }
}
