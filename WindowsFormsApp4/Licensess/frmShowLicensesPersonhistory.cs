using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4.Licensess
{
    public partial class frmShowLicensesPersonhistory : Form
    {
        private int _PersonID = -1;
        public frmShowLicensesPersonhistory()
        {
            InitializeComponent();
        }
        public frmShowLicensesPersonhistory(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
        }

        private void frmShowLicensesPersonhistory_Load(object sender, EventArgs e)
        {
            if (_PersonID != -1)
            {
                personInfoWithFilters1.LoadpersonInfo(_PersonID);
                personInfoWithFilters1.FilterEnable = false;
                ctrDriverLicenses1.LoadInfoByPersonID(_PersonID);
            }
            else
            {
                personInfoWithFilters1.FilterEnable = true;
                personInfoWithFilters1.FilterFocus();
            }
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void personInfoWithFilters1_OnPersonSelected(int obj)
        {
            _PersonID = obj;
            if (_PersonID == -1)
            {
                ctrDriverLicenses1.Clear();
            }
            else
                ctrDriverLicenses1.LoadInfoByPersonID(_PersonID);

        }
    }
}
