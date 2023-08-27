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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PlayerUI
{
    public partial class casesearch : Form
    {
        dbaccess db = new dbaccess();
        public SqlCommand cmd = new SqlCommand();
        public int i;
        public string s;

        public casesearch()
        {
            InitializeComponent();casedis();
        }

        
        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void casedis()
        {
            s = "select * from [case_det]";
            dataGridView1.DataSource = db.FetchData(s);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            casedis();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int cr = dataGridView1.CurrentRow.Index;
                if (dataGridView1.CurrentRow.Cells["c_id"].Value != DBNull.Value)
                {
                    label_caseid.Text = Convert.ToString(dataGridView1[0, cr].Value);
                    label_casetyp.Text= Convert.ToString(dataGridView1[1, cr].Value);
                    label_address.Text = Convert.ToString(dataGridView1[2, cr].Value);
                    label_casename.Text = Convert.ToString(dataGridView1[3, cr].Value);
                    label_phone.Text = Convert.ToString(dataGridView1[4, cr].Value);
                    label_gmail.Text= Convert.ToString(dataGridView1[5, cr].Value);
                    label_opponet.Text = Convert.ToString(dataGridView1[6, cr].Value);
                    label_winning.Text= Convert.ToString(dataGridView1[7, cr].Value);
                    label10.Text = Convert.ToString(dataGridView1[8, cr].Value);

                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            s = ("Select * From case_det Where c_id like '" + textBox1.Text.Trim() + "%'");
            dataGridView1.DataSource = db.FetchData(s);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
