using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlayerUI
{
    public partial class law_search : Form
    {
        dbaccess db = new dbaccess();
        public SqlCommand cmd = new SqlCommand();
        public string s;
        public law_search()
        {
            InitializeComponent();lawdis();
        }

        public void lawdis()
        {
            s = "select * from [law]";
            dataGridView1.DataSource = db.FetchData(s);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int cr = dataGridView1.CurrentRow.Index;
                if (dataGridView1.CurrentRow.Cells["law_no"].Value != DBNull.Value)
                {
                    label_Law_no.Text = Convert.ToString(dataGridView1[0, cr].Value);
                    labe_law_typ.Text = Convert.ToString(dataGridView1[1, cr].Value);
                    label_details.Text = Convert.ToString(dataGridView1[2, cr].Value);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            lawdis();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            s = ("Select * From law Where law_no like '" + textBox1.Text.Trim() + "%'  ");
            dataGridView1.DataSource = db.FetchData(s);
        }
    }
}
