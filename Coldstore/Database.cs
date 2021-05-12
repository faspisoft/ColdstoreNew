using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Coldstore
{
    class Database
    {
        public static DateTime ExeDate= DateTime.Parse("05-Sep-2018");
        public static string prevUsr;
        public static bool activated;
        public static string fname;
        public static string fyear;
        public static string uname;
        public static string utype;
        public static string upass;
        public static string databaseName;
        public static string SoftwareName;
        public static string DatabaseType = "";
        public static int OTP;
        public static int F_id;
        public static int Depaccesstouser;
        public static string LocationId;
        //public static int BranchId;
        public static string dformat = "dd-MMM-yyyy";
        public static DateTime ldate = new DateTime();
        public static DateTime stDate;
        public static DateTime enDate ;

        public static SqlConnection SqlConn = new SqlConnection();
        public static SqlConnection SqlCnn = new SqlConnection();

        public static OleDbConnection AccessConn = new OleDbConnection();
        public static OleDbConnection AccessCnn = new OleDbConnection();
        public static OleDbConnection MultiConn = new OleDbConnection();
        private static SqlCommand sqlcmd;

        public static string inipathfile = "";
        public static string loginfoName;
        public static string inipath = "";
        public static string sqlseverpwd = "";
        public static int user_id;
        private static OleDbCommand accesscmd;
        public static SqlTransaction sqlTran;
        private static SqlTransaction sqlTrana;
        private static OleDbTransaction AccessTran;
        private static OleDbTransaction AccessTrana;
        public static string ServerPath = "";
        public static string LastError = "";
        public static int CompanyState_id = 0;
        public static bool IsKacha= false;
        public static bool LoginbyDb = false;


        public static void setVariable(String fnm, String fyr, String unm, String upss, String utyp, String dbName, DateTime dt1, DateTime dt2)
        {
            fname = fnm;
            fyear = fyr;
            uname = unm;
            utype = utyp;
            upass = upss;

            databaseName = dbName;
            stDate = DateTime.Parse("01-Apr-2018");
            enDate = DateTime.Parse("31-Mar-2019");
           

            CompanyState_id = GetScalarInt("SELECT CState_id FROM COMPANY");
            user_id = Database.GetScalarInt("Select U_id from Userinfo where Uname='" + uname + "'");
            Depaccesstouser = Database.GetScalarInt("Select Department_id from Userinfo where Uname='" + uname + "'");
            utype = Database.GetScalarText("Select Utype from Userinfo where uname='" + uname + "' and Upass='" + upass + "'");
        }

        public static void OpenConnection()
        {
            if (DatabaseType == "sql")
            {               
                if (SqlCnn.State == ConnectionState.Closed)
                {
                    SqlCnn.ConnectionString = @"Data Source=" + inipath + ";Initial Catalog=loginfo;Persist Security Info=True;User ID=sa;password=" + sqlseverpwd + ";Connection Timeout=600";
                  
                    SqlCnn.Open();
                }
                else if (SqlConn.State == ConnectionState.Closed && databaseName != null && databaseName != "")
                {
                    SqlConn.ConnectionString = @"Data Source=" + inipath + ";Initial Catalog=" + Database.databaseName + ";Persist Security Info=True;User ID=sa;password="+ sqlseverpwd+";Connection Timeout=600";
                    SqlConn.Open();
                }
            }
            else
            {
                SetPath();
                //if (AccessCnn.State == ConnectionState.Closed)
                //{
                //    //Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\Coldstore\Coldstore\bin\Debug\Database\ColdStorage.mdb
                //     AccessCnn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ServerPath + "\\loginfo\\loginfo.mdb;Persist Security Info=true;Jet OLEDB:Database Password=ptsoft9358524971";
                //  //  AccessCnn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ServerPath + "\\loginfo\\loginfo.mdb";

                //    AccessCnn.Open();
                //}

                if (AccessConn.State == ConnectionState.Closed)
                {
                    //Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\Coldstore\Coldstore\bin\Debug\Database\ColdStorage.mdb
                    AccessConn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ServerPath + "\\Database\\ColdStorage.mdb;Persist Security Info=true;Jet OLEDB:Database Password=ptsoft9358524971";
                   // AccessConn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ServerPath + "\\Database\\ColdStorage.mdb";

                    AccessConn.Open();
                }
            }
        }

        public static void SetPath()
        {
              ServerPath = Application.StartupPath;      
        }

        public static void CloseConnection()
        {
            if (DatabaseType == "sql")
            {
                SqlConn.Close();
            }
            else
            {
                AccessConn.Close();
            }
        }

        public static bool CommandExecutor(String str)
        {
            OpenConnection();
            if (DatabaseType == "sql")
            {
                sqlcmd = new SqlCommand(str, SqlConn);
                try
                {
                    sqlcmd.Transaction = sqlTran;
                    if (sqlcmd.ExecuteScalar() != null)
                    {
                        sqlcmd.ExecuteNonQuery();
                    }
                      return true;
                }
                catch (SqlException ex)
                {
                    if (ex.Message.Substring(0, 5) != "Table" && (ex.Message != "Primary key already exists.") && ex.Message.Substring(0, 5) != "Field" && ex.Message.Substring(0, 10) == "The change")
                    { 
                     
                    }
                    return false;
                }              
            }
            else
            {
                accesscmd = new OleDbCommand(str, AccessConn);
                try
                {
                    accesscmd.Transaction = AccessTran;
                    accesscmd.ExecuteNonQuery();
                    return true;
                }
                catch (OleDbException ex)
                {
                    if (ex.Message.Substring(0, 5) != "Table" && (ex.Message != "Primary key already exists.") && ex.Message.Substring(0, 5) != "Field" && ex.Message.Substring(0, 10) == "The change")
                    {                       
                    }
                    return false;
                }               
            }            
        }

        public static bool CommandExecutorOther(String str)
        {
            OpenConnection();
            if (DatabaseType == "sql")
            {
                sqlcmd = new SqlCommand(str, SqlCnn);
                try
                {
                    sqlcmd.ExecuteNonQuery();
                    return true;
                }
                catch (OleDbException ex)
                {
                    if (ex.Message.Substring(0, 5) != "Table" && (ex.Message != "Primary key already exists.") && ex.Message.Substring(0, 5) != "Field" && ex.Message.Substring(0, 10) == "The change")
                    {
                        MessageBox.Show(ex.Message);
                    }
                    return false;
                }
            }
            else
            {
                accesscmd = new OleDbCommand(str, AccessCnn);
                try
                {
                    accesscmd.ExecuteNonQuery();
                    return true;
                }
                catch (OleDbException ex)
                {
                    if (ex.Message.Substring(0, 5) != "Table" && (ex.Message != "Primary key already exists.") && ex.Message.Substring(0, 5) != "Field" && ex.Message.Substring(0, 10) == "The change")
                    {

                    }
                    return false;
                }
            }
        }

        public static int GetOtherScalarInt(String str)
        {
            int res = 0;
            OpenConnection();
            if (DatabaseType == "sql")
            {         
                SqlCommand cmd = new SqlCommand(str, SqlCnn);
                cmd.Transaction = sqlTrana;
                if (cmd.ExecuteScalar() != null && cmd.ExecuteScalar().ToString() != "")
                {
                    res = int.Parse(cmd.ExecuteScalar().ToString());
                }
                else
                {
                    res = 0;
                }
                if (sqlTrana == null || sqlTrana.Connection == null)
                {
                    CloseConnection();
                }
                cmd.Dispose();
            }
            else
            {
                OleDbCommand cmd = new OleDbCommand(str, AccessCnn);
                cmd.Transaction = AccessTrana;
                if (cmd.ExecuteScalar() != null && cmd.ExecuteScalar().ToString() != "")
                {
                    res = int.Parse(cmd.ExecuteScalar().ToString());
                }
                else
                {
                    res = 0;
                }
                if (AccessTrana == null || AccessTrana.Connection == null)
                {
                    CloseConnection();
                }
                cmd.Dispose();
            }
            return res;
        }

        public static long GetScalarLong(String str)
        {
            long  res = 0;
            OpenConnection();
            if (DatabaseType == "sql")
            {
                SqlCommand cmd = new SqlCommand(str, SqlConn);
                cmd.Transaction = sqlTran;
                if (cmd.ExecuteScalar() != null)
                {
                    long x = 0;
                    if (long.TryParse(cmd.ExecuteScalar().ToString(), out x) == true)
                    {
                        res = long.Parse(cmd.ExecuteScalar().ToString());
                    }
                }
                cmd.Dispose();
            }
            else
            {
                OleDbCommand cmd = new OleDbCommand(str, AccessConn);
                cmd.Transaction = AccessTran;
                if (cmd.ExecuteScalar() != null)
                {
                    long x = 0;
                    if (long.TryParse(cmd.ExecuteScalar().ToString(), out x) == true)
                    {
                        res = long.Parse(cmd.ExecuteScalar().ToString());
                    }
                    else
                    {
                        res = 0;
                    }

                }
                cmd.Dispose();
            }
            if (AccessTran == null || AccessTran.Connection == null)
            {
                CloseConnection();
            }
            if (sqlTran == null || sqlTran.Connection == null)
            {
                CloseConnection();
            }
            return res;
        }

        public static int CommandExecutorInt(String str)
        {
            OpenConnection();
            if (DatabaseType == "sql")
            {
                sqlcmd = new SqlCommand(str, SqlConn);
                try
                {
                    sqlcmd.Transaction = sqlTran;
                    return sqlcmd.ExecuteNonQuery();                   
                }
                catch (SqlException ex)
                {
                    return 0;
                }
            }
            else
            {
                accesscmd = new OleDbCommand(str, AccessConn);
                try
                {
                    accesscmd.Transaction = AccessTran;
                    return accesscmd.ExecuteNonQuery(); 
                }
                catch (OleDbException ex)
                {
                    return 0;
                }
            }
        }

        public static bool OtherCommandExecutor(String str)
        {
            OpenConnection();
            accesscmd = new OleDbCommand(str, AccessCnn);
            try
             {
                    accesscmd.ExecuteNonQuery();
                    return true;
             }
            catch (OleDbException ex)
            {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            return false;
        }

        public static void SaveOtherData(DataTable dt)
        {
            if (DatabaseType == "sql")
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from " + dt.TableName, SqlCnn);
                SqlCommandBuilder cb = new SqlCommandBuilder();
                cb.QuotePrefix = "[";
                cb.QuoteSuffix = "]";
                cb.DataAdapter = da;
                da.Update(dt);
            }
            else
            {
                OleDbDataAdapter da = new OleDbDataAdapter("select * from " + dt.TableName, AccessCnn);
                OleDbCommandBuilder cb = new OleDbCommandBuilder();
                cb.QuotePrefix = "[";
                cb.QuoteSuffix = "]";
                cb.DataAdapter = da;
                da.Update(dt);
            }
        }

        public static void SaveData(DataTable dt)
        {
            if (DatabaseType == "sql")
            {
                SqlDataAdapter da = new SqlDataAdapter("select * from " + dt.TableName, SqlConn);
                SqlCommandBuilder cb = new SqlCommandBuilder();
                cb.QuotePrefix = "[";
                cb.QuoteSuffix = "]";
                cb.DataAdapter = da;
                da.SelectCommand.Transaction = sqlTran;                
                da.Update(dt);
            }
            else
            {
                OleDbDataAdapter da = new OleDbDataAdapter("select * from " + dt.TableName, AccessConn);
                OleDbCommandBuilder cb = new OleDbCommandBuilder();
                cb.QuotePrefix = "[";
                cb.QuoteSuffix = "]";
                cb.DataAdapter = da;
                da.SelectCommand.Transaction = AccessTran;                
                da.Update(dt);
            }
        }

        public static void SaveData(DataTable dt, String str)
        {
            if (DatabaseType == "sql")
            {
                SqlDataAdapter da = new SqlDataAdapter(str, SqlConn);
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                da.SelectCommand.Transaction = sqlTran;
                da.Update(dt);
            }
            else
            {
                OleDbDataAdapter da = new OleDbDataAdapter(str, AccessConn);
                OleDbCommandBuilder cb = new OleDbCommandBuilder(da);
                da.SelectCommand.Transaction = AccessTran;
                da.Update(dt);
            }
        }
        
        public static void CommitTran()
        {
            if (DatabaseType == "sql")
            {
                 sqlTran.Commit();
            }
            else
            {
                AccessTran.Commit();
            }
        }

        public static void RollbackTran()
        {
            if (DatabaseType == "sql")
            {
                 sqlTran.Rollback();
            }
            else
            {
                AccessTran.Rollback();
            }
        }
        
        public static void BeginTran()
        {
            if (DatabaseType == "sql")
            {
                if (SqlConn.State == ConnectionState.Closed)
                {
                    SqlConn.Open();
                }
                sqlTran = SqlConn.BeginTransaction();
            }
            else
            {
                if (AccessConn.State == ConnectionState.Closed)
                {
                    AccessConn.Open();
                }
                AccessTran = AccessConn.BeginTransaction();
            }
        }

        public static void GetSqlData(String str, DataTable dt)
        {
            dt.Clear();
            if (DatabaseType == "sql")
            {
                SqlDataAdapter da = new SqlDataAdapter(str, SqlConn);
                da.SelectCommand.CommandTimeout = 180;
                da.SelectCommand.Transaction = sqlTran;
                da.Fill(dt);
            }
            else
            {
                //SetPath();
              
   
                //if (AccessConn.ConnectionString == "")
                //{
                //   // AccessConn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ServerPath + "\\Database\\ColdStorage.mdb";
                //    AccessConn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ServerPath + "\\Database\\ColdStorage.mdb;Persist Security Info=true;Jet OLEDB:Database Password=ptsoft9358524971";
                //}
              //  Database.OpenConnection();
                OleDbDataAdapter da = new OleDbDataAdapter(str, AccessConn);
                da.SelectCommand.Transaction = AccessTran;
                da.Fill(dt);
               // Database.CloseConnection();
            }
        }

        public static void GetOtherSqlData(String str, DataTable dt)
        {
            dt.Clear();
            SetPath();
            if (DatabaseType == "sql")
            {
                if (SqlCnn.ConnectionString == "")
                {
                    SqlCnn.ConnectionString = @"Data Source=" + inipath + ";Initial Catalog=loginfo;Persist Security Info=True;User ID=sa;password=" + sqlseverpwd + ";Connection Timeout=100";
                }
                SqlDataAdapter da = new SqlDataAdapter(str, SqlCnn);                
                da.Fill(dt);
            }
            else
            {
                if (AccessCnn.ConnectionString == "")
                {
                   // AccessCnn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ServerPath + "\\loginfo\\loginfo.mdb;";
                    AccessCnn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ServerPath + "\\loginfo\\loginfo.mdb;Persist Security Info=true;Jet OLEDB:Database Password=ptsoft9358524971";
                }
                OleDbDataAdapter da = new OleDbDataAdapter(str, AccessCnn);
                da.Fill(dt);
            }
        }

        public static int GetScalar(String str)
        {
            int res;
            OpenConnection();
            if (DatabaseType == "sql")
            {
                SqlCommand cmd = new SqlCommand(str, SqlConn);
                cmd.Transaction = sqlTran;
                res = int.Parse(cmd.ExecuteScalar().ToString());
                cmd.Dispose();

                if (sqlTran == null || sqlTran.Connection == null)
                {
                    CloseConnection();
                }
            }
            else
            {
                OleDbCommand cmd = new OleDbCommand(str, AccessConn);
                cmd.Transaction = AccessTran;
                res = int.Parse(cmd.ExecuteScalar().ToString());
                cmd.Dispose();
                if (AccessTran == null || AccessTran.Connection == null)
                {
                    CloseConnection();
                }
            }
          
            return res;
        }

        public static String GetScalarText(String str)
        {
            String res = "";
            OpenConnection();
            if (DatabaseType == "sql")
            {
                SqlCommand cmd = new SqlCommand(str, SqlConn);
                cmd.Transaction = sqlTran;
                if (cmd.ExecuteScalar() != null)
                {
                    res = cmd.ExecuteScalar().ToString();
                }
                cmd.Dispose();
                if (sqlTran == null || sqlTran.Connection == null)
                {
                    CloseConnection();
                }
            }
            else
            {
                OleDbCommand cmd = new OleDbCommand(str, AccessConn);
                cmd.Transaction = AccessTran;
                if (cmd.ExecuteScalar() != null)
                {
                    res = cmd.ExecuteScalar().ToString();
                }
                cmd.Dispose();
                if (AccessTran == null || AccessTran.Connection == null)
                {
                    CloseConnection();
                }
            }
            return res;
        }

        public static String GetOtherScalarText(String str)
        {
            String res = "";
            OpenConnection();
            if (DatabaseType == "sql")
            {
                SqlCommand cmd = new SqlCommand(str, SqlCnn);
                if (cmd.ExecuteScalar() != null)
                {
                    res = cmd.ExecuteScalar().ToString();
                }
                cmd.Dispose();
                if (sqlTran == null || sqlTran.Connection == null)
                {
                    CloseConnection();
                }
            }
            else
            {
                OleDbCommand cmd = new OleDbCommand(str, AccessCnn);
                if (cmd.ExecuteScalar() != null)
                {
                    res = cmd.ExecuteScalar().ToString();
                }
                cmd.Dispose();
                if (AccessTran == null || AccessTran.Connection == null)
                {
                    CloseConnection();
                }
            }
            return res;
        }
     
        public static int GetScalarInt(String str)
        {
            int res = 0;
            OpenConnection();
            if (DatabaseType == "sql")
            {
                SqlCommand cmd = new SqlCommand(str, SqlConn);
                cmd.Transaction = sqlTran;

                if (cmd.ExecuteScalar() != null && cmd.ExecuteScalar().ToString() != "")
                {
                    res = int.Parse(cmd.ExecuteScalar().ToString());
                }
                else
                {
                    res = 0;
                }
                cmd.Dispose();
                if (sqlTran == null || sqlTran.Connection == null)
                {
                    CloseConnection();
                }
            }
            else
            {
                OleDbCommand cmd = new OleDbCommand(str, AccessConn);
                cmd.Transaction = AccessTran;
                if (cmd.ExecuteScalar() != null && cmd.ExecuteScalar().ToString() != "")
                {
                    res = int.Parse(cmd.ExecuteScalar().ToString());
                }
                else
                {
                    res = 0;
                }
                cmd.Dispose();
                if (AccessTran == null || AccessTran.Connection == null)
                {
                    CloseConnection();
                }
            }           
            return res;
        }

        public static String GetScalarDate(String str)
        {
            String res = "";
            OpenConnection();
            if (DatabaseType == "sql")
            {
                SqlCommand cmd = new SqlCommand(str, SqlConn);
                cmd.Transaction = sqlTran;
               
                if (cmd.ExecuteScalar().ToString() != null && cmd.ExecuteScalar().ToString() != "")
                {                    
                    res = DateTime.Parse(cmd.ExecuteScalar().ToString()).ToString("dd-MMM-yyyy");
                }
                else
                {
                    res = "";
                }
                cmd.Dispose();
                if (sqlTran == null || sqlTran.Connection == null)
                {
                    CloseConnection();
                }
            }
            else
            {
                OleDbCommand cmd = new OleDbCommand(str, AccessConn);
                cmd.Transaction = AccessTran;
                if (cmd.ExecuteScalar() != null && cmd.ExecuteScalar().ToString() != "")
                {
                    res = DateTime.Parse(cmd.ExecuteScalar().ToString()).ToString("dd-MMM-yyyy");
                }
                else
                {
                    res = "";
                }
                cmd.Dispose();
                if (AccessTran == null || AccessTran.Connection == null)
                {
                    CloseConnection();
                }
            }
            return res;
        }

        public static bool GetScalarBool(String str)
        {
            bool res = false;
            OpenConnection();
            if (DatabaseType == "sql")
            {
                SqlCommand cmd = new SqlCommand(str, SqlConn);
                cmd.Transaction = sqlTran;
                if (cmd.ExecuteScalar() != null && cmd.ExecuteScalar().ToString() != "")
                {
                    res = bool.Parse(cmd.ExecuteScalar().ToString());
                }
                else
                {
                    res = false;
                }
                cmd.Dispose();
                if (sqlTran == null || sqlTran.Connection == null)
                {
                    CloseConnection();
                }
            }
            else
            {
                OleDbCommand cmd = new OleDbCommand(str, AccessConn);
                cmd.Transaction = AccessTran;
                if (cmd.ExecuteScalar() != null && cmd.ExecuteScalar().ToString() != "")
                {
                    res = bool.Parse(cmd.ExecuteScalar().ToString());
                }
                else
                {
                    res = false;
                }
                cmd.Dispose();
                if (AccessTran == null || AccessTran.Connection == null)
                {
                    CloseConnection();
                }
            }
            return res;
        }

        public static bool GetOtherScalarBool(String str)
        
        {      
            bool res = false;
            OpenConnection();
            if (DatabaseType == "sql")
            {
                SqlCommand cmd = new SqlCommand(str, SqlCnn);
                cmd.Transaction = sqlTran;
                if (cmd.ExecuteScalar() != null && cmd.ExecuteScalar().ToString() != "")
                {
                    res = bool.Parse(cmd.ExecuteScalar().ToString());
                }
                else
                {
                    res = false;
                }
                cmd.Dispose();
                if (sqlTran == null || sqlTran.Connection == null)
                {
                    CloseConnection();
                }
            }
            else
            {
                OleDbCommand cmd = new OleDbCommand(str, AccessCnn);
                cmd.Transaction = AccessTran;
                if (cmd.ExecuteScalar() != null && cmd.ExecuteScalar().ToString() != "")
                {
                    res = bool.Parse(cmd.ExecuteScalar().ToString());
                }
                else
                {
                    res = false;
                }
                cmd.Dispose();
                if (AccessTran == null || AccessTran.Connection == null)
                {
                    CloseConnection();
                }
            }
            return res;
        }

        public static Double GetScalarDecimal(String str)
        {
            Double res = 0;
            OpenConnection();
            if (DatabaseType == "sql")
            {
                SqlCommand cmd = new SqlCommand(str, SqlConn);
                cmd.Transaction = sqlTran;
                if (cmd.ExecuteScalar() != null)
                {
                    
                        res = Double.Parse(cmd.ExecuteScalar().ToString());
                    
                }
                cmd.Dispose();
                if (sqlTran == null || sqlTran.Connection == null)
                {
                    CloseConnection();
                }
            }
            else
            {
                OleDbCommand cmd = new OleDbCommand(str, AccessConn);
                cmd.Transaction = AccessTran;
                if (cmd.ExecuteScalar() != null)
                {
                    
                        res = Double.Parse(cmd.ExecuteScalar().ToString());
                    
                }
                cmd.Dispose();
                if (AccessTran == null || AccessTran.Connection == null)
                {
                    CloseConnection();
                }
            }
            return res;           
        }

        public static void setFocus(TextBox tb)
        {
            tb.BackColor = System.Drawing.Color.AntiqueWhite;
            tb.ForeColor = System.Drawing.Color.Black;
        }

        public static void lostFocus(TextBox tb)
        {
            tb.BackColor = System.Drawing.Color.White;
            tb.ForeColor = System.Drawing.Color.Black;
        }

        public static void setFocus(DateTimePicker dtp)
        {
            dtp.BackColor = System.Drawing.Color.AntiqueWhite;
            dtp.CalendarMonthBackground = System.Drawing.Color.Black;
        }

        public static void lostFocus(DateTimePicker dtp)
        {
            dtp.BackColor = System.Drawing.Color.White;
            dtp.ForeColor = System.Drawing.Color.Black;
        }

        public static void setFocus(DataGridViewCell cell)
        {
            cell.Style.BackColor = System.Drawing.Color.AntiqueWhite;
            cell.Style.ForeColor = System.Drawing.Color.Black;
        }

        public static void lostFocus(DataGridViewCell cell)
        {
            cell.Style.BackColor = System.Drawing.Color.White;
            cell.Style.ForeColor = System.Drawing.Color.Black;
        }

        public static void FillList(ListBox lb, String str)
        {
            DataTable dtList = new DataTable();
            dtList.Clear();
            GetSqlData(str, dtList);
            lb.DataSource = dtList;
            lb.DisplayMember = dtList.Columns[0].ColumnName;
        }

        public static void FillCombo(ComboBox cb, String str)
        {
            DataTable dtCombo = new DataTable();
            dtCombo.Clear();
            GetSqlData(str, dtCombo);
            cb.DataSource = dtCombo;
            cb.DisplayMember = dtCombo.Columns[0].ColumnName;
        }

        public static void FillCombo(DataGridViewComboBoxColumn gvcb, String str)
        {
            DataTable dtCombo = new DataTable();
            dtCombo.Clear();
            GetSqlData(str, dtCombo);
            gvcb.DataSource = dtCombo;
            gvcb.DisplayMember = dtCombo.Columns[0].ColumnName;
        }

        public static void FillCombo(ComboBox cb, DataTable dtStr, String colName)
        {
            cb.DataSource = dtStr;
            cb.DisplayMember = dtStr.Columns[colName].ColumnName;
        }
    }
}