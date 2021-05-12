using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace Coldstore
{
    class Select
    {
        public String SelectCombo(System.Windows.Forms.Form thisFrm, Keys keyCode, String query, String selectedText, int uptoIndex)
        {
            //keydown
            String str = "";
            if (keyCode == Keys.F4 || keyCode == Keys.Down || keyCode == Keys.F10)
            {
                str = callFrm(thisFrm, query, selectedText, uptoIndex);
            }
            else if (keyCode == Keys.Delete)
            {
                str = "";
            }
            else
            {
                str = selectedText;
            }

            return str;
        }

        public String SelectCombo(System.Windows.Forms.Form thisFrm, char keyChar, String query, String selectedText, int uptoIndex)
        {
            //keypress
            String str = "";
            if (char.IsLetter(keyChar) || char.IsNumber(keyChar) || keyChar == ' ' || Convert.ToInt32(keyChar) == 13)
            {

                str = callFrm(thisFrm, query, selectedText, uptoIndex);
            }


            if (str != "" && thisFrm.ActiveControl.GetType() != typeof(faspiGrid.ansGridView))
            {
                thisFrm.SelectNextControl(thisFrm.ActiveControl, true, true, true, true);
            }

            thisFrm.Activate();
            thisFrm.TopMost = true;
            return str;


        }

        private String callFrm(System.Windows.Forms.Form thisFrm, String query, String selectedText, int uptoIndex)
        {
            String str;
            DataTable dtFirm = new DataTable();
            if (thisFrm.Name == "Login")
            {          

                Database.GetOtherSqlData(query, dtFirm);
            }
            else
            {
                Database.GetSqlData(query, dtFirm);
            }
            //SelectAcc frm;
            //if (selectedText == "")
            //{
            SelectAcc frm = new SelectAcc(dtFirm, selectedText, uptoIndex);
            //}
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog(thisFrm);
            thisFrm.Activate();
            if (frm.outStr != null)
            {
                str = frm.outStr;
            }
            else
            {
                str = "";
            }

            return str;
        }


        public void IsEnter(Form thisfrm, Keys keyCode)
        {
            if (keyCode == Keys.Enter)
            {
                thisfrm.SelectNextControl(thisfrm.ActiveControl, true, true, true, true);
            }
            thisfrm.Activate();
            thisfrm.TopMost = true;
        }
    }
}
