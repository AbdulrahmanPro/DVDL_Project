using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4.Applications.LocalDrivingLicense
{
    public partial class frmShowLocalDrivingLicenseInfo : Form
    {
        private int _ApplicationID=-1;
        public frmShowLocalDrivingLicenseInfo(int ApplicationID)
        {
            InitializeComponent();
            _ApplicationID = ApplicationID;
        }

        private void frmShowLocalDrivingLicenseInfo_Load(object sender, EventArgs e)
        {
            ctrLocalDrivingInfo1.LoadApplicationInfoByLocalDrivingAppID(_ApplicationID);
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
