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
    public partial class lawyer_search : Form
    {
        dbaccess db = new dbaccess();
        public SqlCommand cmd = new SqlCommand();
        public string s;
        public void lawyerdis()
        {
            s = "select * from [lawyer]";
            dataGridView1.DataSource = db.FetchData(s);
        }
        public lawyer_search()
        {
            InitializeComponent();lawyerdis();
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
                if (dataGridView1.CurrentRow.Cells["L_id"].Value != DBNull.Value)
                {
                    label_LAWREGID.Text = Convert.ToString(dataGridView1[0, cr].Value);
                    label_lawname.Text = Convert.ToString(dataGridView1[1, cr].Value);
                    label_address.Text = Convert.ToString(dataGridView1[2, cr].Value);
                    label_pincode.Text = Convert.ToString(dataGridView1[3, cr].Value);
                    label_phone.Text = Convert.ToString(dataGridView1[4, cr].Value);
                    label8.Text = Convert.ToString(dataGridView1[5, cr].Value);
                    label_qualification.Text = Convert.ToString(dataGridView1[6, cr].Value);
                    label_designation.Text = Convert.ToString(dataGridView1[7, cr].Value);
                    label12.Text = Convert.ToString(dataGridView1[8, cr].Value);

                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            s = ("Select * From lawyer Where lawyern like '" + textBox1.Text.Trim() + "%'  ");
            dataGridView1.DataSource = db.FetchData(s);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            lawyerdis();
        }
    }
}
