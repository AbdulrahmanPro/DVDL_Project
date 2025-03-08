using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4.PeopleForms
{
    public partial class ShowPersonInfo : Form
    {
        public ShowPersonInfo(int PersonID)
        {
            InitializeComponent();
            personInfo1.LoadPersonInfo(PersonID);
        }
        public ShowPersonInfo(string NationalNo)
        {
            InitializeComponent();
            personInfo1.LoadPersonInfo(NationalNo);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
