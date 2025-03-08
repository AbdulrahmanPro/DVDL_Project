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
    public partial class FindPerson : Form
    {
        public FindPerson()
        {
            InitializeComponent();
        }
        public delegate void DatabackEventHandelr(object sender, int PersonID);
        public event DatabackEventHandelr DataBack;
        private void FindPerson_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            DataBack?.Invoke(this, personInfoWithFilters1.PersonID);
        }

        private void personInfoWithFilters1_Load(object sender, EventArgs e)
        {

        }
    }
}
