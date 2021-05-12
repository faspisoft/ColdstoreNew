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
    public partial class frmSoftwareUpdates : Form
    {
        public frmSoftwareUpdates()
        {
            InitializeComponent();
        }

        public void Update()
        {
            Database.OpenConnection();

            Voucheractotal();
            //loginAct();
            //loginFet();

            Database.CloseConnection();
        }


        private void loginAct()
        {
            if (Database.CommandExecutor("create table [Activate]([ID] AUTOINCREMENT, [column]  text(50),[Value] text(50),DisplayToUser bit)") == true)
            {
                Database.CommandExecutor("Alter table Activate Add Primary Key (ID)");

            }
        }

        private void loginFet()
        {
            if (Database.CommandExecutor("create table [Feature]([ID] AUTOINCREMENT, [Features]  text(50),Active bit)") == true)
            {
                Database.CommandExecutor("Alter table Feature Add Primary Key (ID)");
                Database.CommandExecutor("insert into Feature (Features,Active) values('Activated',False)");
            }
        }
        private void Voucheractotal()
        {


            if (Database.CommandExecutor("ALTER TABLE tblvouchertype ADD COLUMN [Type] text(50) ") == true)
            {
                Database.CommandExecutor("update tblvouchertype set Type=vname");
            }

        }
        private void frmSoftwareUpdates_Load(object sender, EventArgs e)
        {

        }
    }
}
