using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVDLBusiness;

namespace WindowsFormsApp4.Applications.TestType
{
    public partial class frmListTestType : Form
    {
        public frmListTestType()
        {
            InitializeComponent();
        }
        private DataTable _TestTypeInfo;
        private void frmListTestType_Load(object sender, EventArgs e)
        {
            _TestTypeInfo = clsTestTypeBusiness.GetAllTestType();
            dgvTestType.DataSource = _TestTypeInfo;
            lblRescordCount.Text = dgvTestType.Rows.Count.ToString();
            if (dgvTestType.Rows.Count > 0)
            {
                dgvTestType.Columns[0].HeaderText = "ID";
                dgvTestType.Columns[0].Width = 110;
             
                
                dgvTestType.Columns[1].HeaderText = "Title";
                dgvTestType.Columns[1].Width = 200;
                
                dgvTestType.Columns[2].HeaderText = "Description";
                dgvTestType.Columns[2].Width = 400;
                
                dgvTestType.Columns[3].HeaderText = "Fees";
                dgvTestType.Columns[3].Width = 110;
               }
        }

        private void dgvTestType_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmEditTestType frm = new frmEditTestType((clsTestTypeBusiness.enTestType)dgvTestType.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditTestType frm = new frmEditTestType((clsTestTypeBusiness.enTestType)dgvTestType.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }
    }
}
