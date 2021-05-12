using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using Coldstore;

namespace Coldstore
{
     class funs11
    {
        OleDbCommand cmd = new OleDbCommand();

        public static String AddAccount()
        {
            String accnm;
            frm_Account frm = new frm_Account();
            frm.calledIndirect = true;
            frm.LoadData("0", "Account");
            frm.ShowDialog();
            accnm = frm.AccName;
            return accnm;
        }

        public static String EditAccount(String accnm)
        {
            String newAccnm;
            String acid;
            DataTable dtCheckAcc = new DataTable();
            Database.GetSqlData("select * from tblaccount where [acc_name]='" + accnm + "'", dtCheckAcc);
            if (dtCheckAcc.Rows.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("Account does not exist");
                return "";
            }
            else
            {
                frm_Account frm = new frm_Account();
                frm.calledIndirect = true;
                acid = Select_ac_id(accnm).ToString();
                frm.LoadData(acid, "Edit Account");
                frm.ShowDialog();
                newAccnm = frm.AccName;
                return newAccnm;
            }
        }
        public static String AddItem()
        {
            String itemnm;
            frm_Item frm = new frm_Item();
            frm.calledIndirect = true;
            frm.LoadData("0", "Item");
            frm.ShowDialog();
            itemnm = frm.itemName;
            return itemnm;
        }
        public static void OpenFrm(System.Windows.Forms.Form thisfrm, int v_id)
        {


            string frmName = Database.GetScalarText("SELECT TblVoucherType.Vname FROM tblVoucherinfo LEFT JOIN TblVoucherType ON tblVoucherinfo.Vt_id = TblVoucherType.Vt_id WHERE (((tblVoucherinfo.Vi_id)=" + v_id + "));");
             if (frmName == "Inward")
            {
                   if (v_id == 0)
                    {
                        return;
                    }
                        frm_voucher frm = new frm_voucher();
                        frm.LoadData("Inward", v_id.ToString(), "Edit Inward");
                        frm.MdiParent = thisfrm.MdiParent;
                        frm.Show();   
             }
             else if (frmName == "Outward")
             {
                 if (v_id == 0)
                 {
                     return;
                 }
                 frm_voucher frm = new frm_voucher();
                 frm.LoadData("Outward", v_id.ToString(), "Edit Outward");
                 frm.MdiParent = thisfrm.MdiParent;
                 frm.Show();
             }
             else if (frmName == "OpeningStock")
             {
                 if (v_id == 0)
                 {
                     return;
                 }
                 frm_voucher frm = new frm_voucher();
                 frm.LoadData("OpeningStock", v_id.ToString(), "Edit Opening Stock");
                 frm.MdiParent = thisfrm.MdiParent;
                 frm.Show();
             }
            

        }
        public static String EditItem(String Itemnm)
        {
            String newitemnm;
            String Item_id;
            DataTable dtCheckAcc = new DataTable();
            Database.GetSqlData("select * from tblIteminfo where [Item_name]='" + Itemnm + "'", dtCheckAcc);
            if (dtCheckAcc.Rows.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("Item does not exist");
                return "";
            }
            else
            {
                frm_Item frm = new frm_Item();
                frm.calledIndirect = true;
                Item_id = Select_Item_id(Itemnm).ToString();
                frm.LoadData(Item_id, "Edit Account");
                frm.ShowDialog();
                newitemnm = frm.itemName;
                return newitemnm;
            }
        }


        public static string IndianCurr(double o)
        {
            System.Globalization.CultureInfo cuInfo = new System.Globalization.CultureInfo("hi-IN");
            return (o.ToString("C", cuInfo)).Remove(0, 2).Trim();
        }
        public static String EditAccount(String accnm, String acctyp)
        {
            String newAccnm;
            String acid;
            DataTable dtCheckAcc = new DataTable();
            Database.GetSqlData("select * from account where [name]='" + accnm + "'", dtCheckAcc);
            if (dtCheckAcc.Rows.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("Account does not exist");
                return "";
            }
            frm_Account frm = new frm_Account();
            frm.calledIndirect = true;
            frm.AccType = acctyp;
            acid = Select_ac_id(accnm).ToString();
            frm.LoadData(acid, "Edit Account");
            frm.ShowDialog();
            newAccnm = frm.AccName;
            return newAccnm;
        }


       

        

        public static void IsEnter(Form thisfrm, Keys keyCode)
        {
            if (keyCode == Keys.Enter)
            {
                thisfrm.SelectNextControl(thisfrm.ActiveControl, true, true, true, true);
            }
            thisfrm.Activate();
            thisfrm.TopMost = true;
        }

        public static int Select_ac_id(String accname)
        {
            return Database.GetScalarInt("select Ac_id from tblaccount where [acc_name]='" + accname + "'");
        }

        public static String Select_ac_nm(int ac_id)
        {
            return Database.GetScalarText("select [acc_name] from tblaccount where ac_id=" + ac_id);
        }

        public static int Select_Item_id(String Itemname)
        {
            return Database.GetScalarInt("select Item_id from tblIteminfo where item_name='" + Itemname + "'");
        }

        public static String Select_Item_nm(int Item_id)
        {
            return Database.GetScalarText("select Item_name from tbliteminfo where Item_id=" + Item_id);
        }

        public static int Select_act_id(String name)
        {
            return Database.GetScalarInt("select ac_id from tblaccount where [acc_name]='" + name + "'");
        }

        public static String Select_act_nm(int act_id)
        {
            return Database.GetScalarText("select name from accountype where act_id=" + act_id);
        }

        public static int Select_grp_id(String grpname)
        {
            return Database.GetScalarInt("select id from productgroup where productgroup='" + grpname + "'");
        }

        public static String Select_grp_nm(int grp_id)
        {
            return Database.GetScalarText("select productgroup from productgroup where id=" + grp_id);
        }

        public static int Select_vt_id(String vt_name)
        {
            return Database.GetScalarInt("select vt_id from tblvouchertype where [Vname]='" + vt_name + "'");
        }

        public static String Select_vt_nm(int vt_id)
        {
            return Database.GetScalarText("select [vname] from tblvouchertype where vt_id=" + vt_id);
        }

        public static int Select_vi_id(int vnm, int id, String dt)
        {
            return Database.GetScalarInt("select vi_id voucherinfo where vnumber=" + vnm + " and vt_id=" + id);
        }

        public static String DecimalPoint(Object o, int count)
        {
            string str = ".";
            for (int i = 0; i < count; i++)
            {
                str += "0";
            }
            if (count == 0)
            {
                str = "";
            }
            String conVal;
            conVal = String.Format("{0:0" + str + "}", o);
            return conVal;
        }

        public static String DecimalPoint(Object o)
        {
            String conVal;
            conVal = String.Format("{0:0.00}", o);
            return conVal;
        }

        public int chkNumType(int vtid)
        {
            return Database.GetScalarInt("select Numtype from vouchertype where vt_id=" + vtid);
        }

        public static int GenerateVno(int vtid, String dt)
        {
            int vnum;
            DataTable dtVoucherNum = new DataTable();
            DataTable dtCount = new DataTable();
            String strDate = "";
            int numType = 1;

           

            //numtype= 1 ->  Yearly (Sale)
            //numtype= 2 ->  Monthly
            //numtype= 3 ->  Daily (Purchase,Receipt,Payment)

           if (numType == 1)
            {
                strDate = "";
            }
            Database.GetSqlData("select * from tblvoucherinfo where vt_id = " + vtid + strDate, dtCount);

            if (dtCount.Rows.Count == 0)
            {
                vnum = Database.GetScalarInt("Select Startvno from tblvouchertype where vt_id="+vtid);
            }
            else
            {
                
                Database.GetSqlData("select max(Vnumber)+1 from tblvoucherinfo where vt_id = " + vtid + strDate+" and F_id="+Database.F_id, dtVoucherNum);
                if (dtVoucherNum.Rows[0][0].ToString() == "")
                {
                    vnum = 1;
                }
                else
                {
                    vnum = int.Parse(dtVoucherNum.Rows[0][0].ToString());
                }
               
            }
            return vnum;
        }

    
        public String accbal(int ac_id)
        {
            String curbal;
            double opbal = 0, bal = 0;

            DataTable dtOpenBal = new DataTable();
            Database.GetSqlData("select dr,cr from account where Ac_id=" + ac_id, dtOpenBal);
            if (dtOpenBal.Rows.Count > 0)
            {
                if (dtOpenBal.Rows[0]["Dr"].ToString() != "0")
                {
                    opbal = double.Parse(dtOpenBal.Rows[0]["Dr"].ToString());
                }
                else
                {
                    opbal = -(double.Parse(dtOpenBal.Rows[0]["Cr"].ToString()));
                }
            }

            DataTable dtBal = new DataTable();
            Database.GetSqlData("SELECT sum(dr) as Dramt,sum(cr) As Cramt from journal group by Ac_id having Ac_id=" + ac_id, dtBal);
            if (dtBal.Rows.Count > 0)
            {
                if (dtBal.Rows[0]["Cramt"].ToString() == "" || dtBal.Rows[0]["Dramt"].ToString() == "")
                {
                    dtBal.Rows[0]["Cramt"] = 0;
                }
                if (double.Parse(dtBal.Rows[0]["Dramt"].ToString()) > double.Parse(dtBal.Rows[0]["Cramt"].ToString()))
                {
                    bal = double.Parse(dtBal.Rows[0]["Dramt"].ToString()) - double.Parse(dtBal.Rows[0]["Cramt"].ToString());
                }
                else
                {
                    bal = -(double.Parse(dtBal.Rows[0]["Cramt"].ToString()) - double.Parse(dtBal.Rows[0]["Dramt"].ToString()));

                }
            }
            curbal = (opbal + bal).ToString();
            if (double.Parse(curbal) >= 0)
            {
                curbal += " Dr.";
            }
            else
            {
                curbal += " Cr.";
            }
            return curbal;
        }

        public String select_rpt_copy(int vtid, int cpy)
        {
            String cpyNm = "";
            DataTable dtOptions = new DataTable();
            dtOptions.Clear();
            if (cpy == 1)
            {
                Database.GetSqlData("select Default1 FROM VOUCHERTYPE WHERE Vt_id=" + vtid, dtOptions);
            }
            else if (cpy == 2)
            {
                Database.GetSqlData("select Default2 FROM VOUCHERTYPE WHERE Vt_id=" + vtid, dtOptions);
            }
            else if (cpy == 3)
            {
                Database.GetSqlData("select Default3 FROM VOUCHERTYPE WHERE Vt_id=" + vtid, dtOptions);
            }
            if (dtOptions.Rows.Count > 0)
            {
                cpyNm = dtOptions.Rows[0][0].ToString();
            }
            return cpyNm;
        }

        public double IntrestCalculator(String AccName, string DealNo, DateTime CDt)
        {
            DataTable dtf = new DataTable();
            funs11 fObj = new funs11();

            Database.GetSqlData("SELECT journal.Vdate, journal.Narr, journal.Dr, journal.Cr, VOUCHERINFO.dealno FROM journal INNER JOIN VOUCHERINFO ON journal.Vi_id = VOUCHERINFO.Vi_id WHERE (journal.[Vi_id]=" + DealNo + " or dealno=" + DealNo + " )AND journal.[Ac_id]=" + funs11.Select_ac_id(AccName) + " and journal.Vdate<= #" + CDt.ToString("dd-MMM-yyyy") + "# ORDER BY journal.Vdate", dtf);

            dtf.Columns.Add("Balance", typeof(double));
            dtf.Columns.Add("Days", typeof(int));
            dtf.Columns.Add("Wbal", typeof(double));

            dtf.Rows.Add(CDt.ToString("dd-MMM-yyyy"), "", 0, 0, 0, 0, 0);
            double rbalance = 0;
            for (int i = 0; i < dtf.Rows.Count; i++)
            {

                if (double.Parse(dtf.Rows[i]["Dr"].ToString()) > double.Parse(dtf.Rows[i]["Cr"].ToString()))
                {
                    rbalance = rbalance + double.Parse(dtf.Rows[i]["Dr"].ToString());
                    dtf.Rows[i]["Balance"] = rbalance;
                }
                else if (double.Parse(dtf.Rows[i]["Cr"].ToString()) > double.Parse(dtf.Rows[i]["Dr"].ToString()))
                {
                    rbalance = rbalance - double.Parse(dtf.Rows[i]["Cr"].ToString());
                    dtf.Rows[i]["Balance"] = rbalance;

                }
                else
                {

                    dtf.Rows[i]["Balance"] = rbalance;

                }

                if (i < dtf.Rows.Count - 1)
                {

                    dtf.Rows[i]["Days"] = ((DateTime)dtf.Rows[i + 1]["Vdate"] - (DateTime)dtf.Rows[i]["Vdate"]).Days.ToString();
                }
                else
                {
                    dtf.Rows[i]["Days"] = "0";

                }
                dtf.Rows[i]["Wbal"] = double.Parse(dtf.Rows[i]["Balance"].ToString()) * double.Parse(dtf.Rows[i]["Days"].ToString());
            }

            int tdays = int.Parse(dtf.Compute("Sum(Days)", "").ToString());
            double twbal = int.Parse(dtf.Compute("Sum(Wbal)", "").ToString());
            double abalance;
            if (tdays == 0)
            {
                abalance = 0;
            }
            else
            {
                abalance = twbal / tdays;
            }
            double irate = 0;
            irate = Database.GetScalarDecimal("select int_rate from voucherinfo where Vi_id=" + DealNo);



            var dateSpan = DateTimeSpan.CompareDates((DateTime)dtf.Compute("max(Vdate)", ""), (DateTime)dtf.Compute("min(Vdate)", ""));

            double Months = ((dateSpan.Years * 12) + dateSpan.Months + (double.Parse(dateSpan.Days.ToString()) / 30));

            double interest = abalance * irate * Months / 100;


            //dataGridView1.DataSource = dtf;
            //dataGridView1.Columns["Vdate"].Width = 100;
            //dataGridView1.Columns["Narr"].Width = 100;
            //dataGridView1.Columns["Dr"].Width = 100;
            //dataGridView1.Columns["Cr"].Width = 100;
            //dataGridView1.Columns["Narr"].HeaderText = "Particular";
            //dataGridView1.Columns["Vdate"].DisplayIndex = 0;
            //dataGridView1.Columns["Narr"].DisplayIndex = 1;
            //dataGridView1.Columns["Dr"].DisplayIndex = 2;
            //dataGridView1.Columns["Cr"].DisplayIndex = 3;
            if (double.Parse(funs11.DecimalPoint(interest.ToString())) < 0)
            {
                return -1 * double.Parse(funs11.DecimalPoint(interest.ToString()));
            }
            else
            {

                return double.Parse(interest.ToString());
            }


        }

        public struct DateTimeSpan
        {
            private readonly int years;
            private readonly int months;
            private readonly int days;
            private readonly int hours;
            private readonly int minutes;
            private readonly int seconds;
            private readonly int milliseconds;

            public DateTimeSpan(int years, int months, int days, int hours, int minutes, int seconds, int milliseconds)
            {
                this.years = years;
                this.months = months;
                this.days = days;
                this.hours = hours;
                this.minutes = minutes;
                this.seconds = seconds;
                this.milliseconds = milliseconds;
            }

            public int Years { get { return years; } }
            public int Months { get { return months; } }
            public int Days { get { return days; } }
            public int Hours { get { return hours; } }
            public int Minutes { get { return minutes; } }
            public int Seconds { get { return seconds; } }
            public int Milliseconds { get { return milliseconds; } }

            enum Phase { Years, Months, Days, Done }

            public static DateTimeSpan CompareDates(DateTime date1, DateTime date2)
            {
                if (date2 < date1)
                {
                    var sub = date1;
                    date1 = date2;
                    date2 = sub;
                }

                DateTime current = date1;
                int years = 0;
                int months = 0;
                int days = 0;

                Phase phase = Phase.Years;
                DateTimeSpan span = new DateTimeSpan();



                while (phase != Phase.Done)
                {
                    switch (phase)
                    {
                        case Phase.Years:
                            if (current.AddYears(years + 1) > date2)
                            {
                                phase = Phase.Months;
                                current = current.AddYears(years);
                            }
                            else
                            {
                                years++;
                            }
                            break;
                        case Phase.Months:
                            if (current.AddMonths(months + 1) > date2)
                            {
                                phase = Phase.Days;
                                current = current.AddMonths(months);
                            }
                            else
                            {
                                months++;
                            }
                            break;
                        case Phase.Days:
                            if (current.AddDays(days + 1) > date2)
                            {
                                current = current.AddDays(days);
                                var timespan = date2 - current;
                                span = new DateTimeSpan(years, months, days, timespan.Hours, timespan.Minutes, timespan.Seconds, timespan.Milliseconds);
                                phase = Phase.Done;
                            }
                            else
                            {
                                days++;
                            }
                            break;
                    }
                }

                return span;
            }
            
           
        }
    }
}
