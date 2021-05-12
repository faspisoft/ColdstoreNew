using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Coldstore
{
    public partial class str_main : Form
    {
        public str_main()
        {
            InitializeComponent();
        }
        private void setmenu()
        {

                DataTable dtdiff = new DataTable();
                Database.GetSqlData("select distinct vdate from tblvoucherinfo", dtdiff);
                int count = 0;
                count = dtdiff.Rows.Count;
                activateToolStripMenuItem.Visible = true;
                if (count >= 1000)
                {
                    listToolStripMenuItem.Visible = false;
                    transactionsToolStripMenuItem.Visible = false;
                    reportsToolStripMenuItem.Visible = false;
                    // activateToolStripMenuItem.Visible = true;
                    //MessageBox.Show("Please Contact With Administrative");
                    //menuStrip1.Visible = false;
                }
            
        }
        private void accountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMaster frm = new frmMaster();
            frm.MdiParent = this;
            frm.LoadData("Account", "Account");
            frm.Size = this.Size;
            frm.Show();
        }

        private void listToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void productsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMaster frm = new frmMaster();
            frm.MdiParent = this;
            frm.LoadData("Item", "Item");
            frm.Size = this.Size;
            frm.Show();

        }

        private void str_main_Load(object sender, EventArgs e)
        {
            if (Database.activated == false)
            {
                setmenu();
            }


            frmSoftwareUpdates frm = new frmSoftwareUpdates();
            frm.Update();
        }

        private void inwordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMaster frm = new frmMaster();
            frm.MdiParent = this;
            frm.LoadData("Inward", "Inward");
            frm.Size = this.Size;
            frm.Show();
        }

        private void outwordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMaster frm = new frmMaster();
            frm.MdiParent = this;
            frm.LoadData("Outward", "Outward");
            frm.Size = this.Size;
            frm.Show();
        }

        private void openingStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMaster frm = new frmMaster();
            frm.MdiParent = this;
            frm.LoadData("OpeningStock", "Opening Stock");
            frm.Size = this.Size;
            frm.Show();
        }

        private void stockRegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_StockReport frm = new frm_StockReport();
            frm.MdiParent = this;
            //frm.LoadData("Stock", "Opening Stock");
            //frm.Size = this.Size;
            frm.Show(); 
        }

        private void str_main_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show(null,"Are you want to Exit", "Exit", MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                File.Copy(Application.StartupPath + "\\Database\\" + Database.databaseName + ".mdb", Application.StartupPath + "\\Backup\\" + Database.databaseName + "M" + DateTime.Now.ToString("MM"), true);
                File.Copy(Application.StartupPath + "\\Database\\" + Database.databaseName + ".mdb", Application.StartupPath + "\\Backup\\" + Database.databaseName + "D" + DateTime.Now.ToString("dd"), true);
                //this.Dispose();
                //this.Close();
                Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }
            
        }

        private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void partyStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void itemWiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Report gg = new Report();
            string strCombo = "Select item_name from tbliteminfo order by item_name";
            string selected = SelectCombo.ComboKeypress(this, 'a', strCombo, "", 1);
            gg.StockReportItemLedger(Database.stDate, Database.enDate, selected,"ItemLedger");
            gg.MdiParent = this;
            gg.Show();
        }

        private void itemLedgerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Report gg = new Report();
            string strCombo = "Select item_name from tbliteminfo order by item_name";
            string selected = SelectCombo.ComboKeypress(this, 'a', strCombo, "", 1);
            gg.StockReportItemLedger(Database.stDate, Database.enDate, selected,"ItemLedger");
            gg.MdiParent = this;
            gg.Show();
        }

        private void itemDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Report gg = new Report();
            string strcombo = "Select item_name from tblIteminfo order by Item_name";
            string selected = SelectCombo.ComboKeypress(this, 'a', strcombo, "", 1);
            gg.StockReportItemLedgerDetail(Database.stDate, Database.enDate, selected,"ItemDetail");
            gg.MdiParent = this;
            gg.Show();
        }

        private void markaWiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void itemWiseToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frm_StockReportItemwise frm = new frm_StockReportItemwise();
            frm.MdiParent = this;
            //frm.LoadData("Stock", "Opening Stock");
            //frm.Size = this.Size;
            frm.Show();
        }

        private void refferenceNoWiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form_reffno frm = new form_reffno();
            frm.MdiParent = this;
            frm.Show();
        }

    

        private void partysItemWiseRegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Report gg = new Report();

            gg.PartyRegister(Database.stDate, Database.ldate, "Party's Item Wise Register");
                gg.MdiParent = this;
            gg.Show();
        }

        private void dailyRegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fum_inoutreg frm = new fum_inoutreg();
            frm.MdiParent = this;
            frm.Show();
        }

        private void loanAdvisorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMaster frm = new frmMaster();
            frm.MdiParent = this;
            frm.LoadData("LoanM", "LoanM");
            frm.Size = this.Size;
            frm.Show();
        }

        private void loanSettlementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMaster frm = new frmMaster();
            frm.MdiParent = this;
            frm.LoadData("LoanS", "LoanS");
            frm.Size = this.Size;
            frm.Show();
        }

        private void menuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void exitToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DialogResult ch = MessageBox.Show(null, "Are you sure to exit?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (ch == DialogResult.OK)
            {
                
                           
                               

                                
               
                GC.Collect();
                Environment.Exit(0);
            }
        }

        private void dataBackupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
                DialogResult val = fbd.ShowDialog(this);
                if (val == DialogResult.OK)
                {
                    string pathtobackup = fbd.SelectedPath.ToString() + "\\" + Database.databaseName + DateTime.Now.ToString("yyyyMMddHHmmss") + ".mdb";
                    if (Database.AccessConn.State == ConnectionState.Open)
                    {
                        Database.CloseConnection();
                    }


                    File.Copy(Application.StartupPath + "\\Database\\" + Database.databaseName + ".mdb", pathtobackup, true);
                    MessageBox.Show("Backup Successfull");

                    Database.OpenConnection();
                }
            
        }

        private void partysStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Report gg = new Report();
            string strCombo = "SELECT tblAccount.Acc_name as Name FROM tblAccount ORDER BY tblAccount.Acc_name;";

            char cg = 'a';
            string selected = SelectCombo.ComboKeypress(this, cg, strCombo, "", 0);
            if (selected != "")
            {
                gg.PartyStock(Database.stDate, Database.ldate, selected, "Party's Stock");
            }
            gg.Show();
        }

        private void stockRegisterToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Report gg = new Report();
            gg.MdiParent = this;
            gg.StockRegIn(Database.stDate, Database.ldate, "Stock Register Inward");
            
            gg.Show();
        }

        private void partysListToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void partysWareGoodsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Report gg = new Report();
            string strCombo = "SELECT tblAccount.Acc_name as Name FROM tblAccount ORDER BY tblAccount.Acc_name;";

            char cg = 'a';
            string selected = SelectCombo.ComboKeypress(this, cg, strCombo, "", 0);
            if (selected != "")
            {
                gg.PartyWhereGoods(Database.stDate, Database.ldate, selected, "Party's Where Goods");
            }
            gg.Show();
        }

        private void partysLoanRegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Report gg = new Report();
            gg.MdiParent = this;
            gg.PartyLoanReg(Database.stDate, Database.ldate, "Party's Loan Register");

            gg.Show();
        }

        private void totalStockListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Report gg = new Report();
            gg.MdiParent = this;
            gg.TotalStockReg(Database.stDate, Database.ldate, "Total Stock List");

            gg.Show();
        }

        private void partysListToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Report gg = new Report();
            gg.MdiParent = this;
            gg.PartyList(Database.stDate, Database.ldate, "Stock Register");

            gg.Show();
        }

        private void stockRegisterOutwardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Report gg = new Report();
            gg.MdiParent = this;
            gg.StockRegOut(Database.stDate, Database.ldate, "Stock Register Outward");

            gg.Show();
        }

        private void activateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please Contact With Administrative");
        }

        private void partysItemWiseRegisterToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Report gg = new Report();

            gg.PartyRegisterItemWise(Database.stDate, Database.ldate, "Party's Item Wise Register","");
            gg.MdiParent = this;
            gg.Show();
        }

        private void referrenceNoWiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form_reffno frm = new form_reffno();
            frm.reportname = "Referrence No Wise";
            frm.MdiParent = this;
            frm.Show();
        }

        private void allStockListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Report gg = new Report();
            gg.MdiParent = this;
            gg.StockReg(Database.stDate, Database.ldate, "All Stock List");

            gg.Show();
        }

        private void totalInwardStockListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Report gg = new Report();
            gg.MdiParent = this;
            gg.InTotalStockReg(Database.stDate, Database.ldate, "Inward Total Stock List");

            gg.Show();
        }

        private void partysItemWiseRegisterAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Report gg = new Report();

            gg.PartyRegisterItemWiseAll(Database.stDate, Database.ldate, "Party's Item Wise Register All", "");
            gg.MdiParent = this;
            gg.Show();
        }

        private void referrenceNoSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form_reffno frm = new form_reffno();
            frm.reportname = "Referrence No Summary";
            frm.MdiParent = this;
            frm.Show();
        }

        private void switchFYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form[] frms = this.MdiChildren;
            foreach (Form frm in frms)
            {
                frm.Dispose();
            }
            //Database.prevUsr = statusStrip1.Items[2].Text;
            //Database.fyear = "";
            //this.Text = "";
            //statusStrip1.Items[2].Text = "";
            //statusStrip1.Items[4].Text = "";
            //statusStrip1.Items[9].Text = "+91 83070 71699";
            //Database.databaseName = "";
            setmenu();
            Database.CloseConnection();
            frmbackup frm1 = new frmbackup();

            frm1.frmMenuTyp = "Use";
            frm1.Text = "Login as";
            frm1.ShowDialog(this);
            bool ch = frm1.ret;
            if (ch == true)
            {

                setmenu();
                //statusStrip1.Items[2].Text = Database.ExeDate.ToString("yy.M.d");
                //statusStrip1.Items[4].Text = Database.ldate.ToString(Database.dformat);
                //statusStrip1.Items[9].Text = "+91 83070 71699";
                //this.Text = Database.fname + "[" + Database.fyear + "]";

            }
        }

       
    }
}
