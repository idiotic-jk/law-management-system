using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlayerUI
{
    public partial class Court_search : Form
    {
        dbaccess db = new dbaccess();
        public SqlCommand cmd = new SqlCommand();
        public int i;
        public string s;
        public Court_search()
        {
            InitializeComponent();courtdis();
        }

        public void courtdis()
        {
            s = "select * from [court]";
            dataGridView1.DataSource = db.FetchData(s);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            courtdis();
        }       

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            s = ("Select * From court Where cname like'" + textBox1.Text.Trim() + "%'  ");
            dataGridView1.DataSource = db.FetchData(s);
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int cr = dataGridView1.CurrentRow.Index;
                if (dataGridView1.CurrentRow.Cells["cname"].Value != DBNull.Value)
                {
                    label_court_name.Text = Convert.ToString(dataGridView1[1, cr].Value);
                    label_adress.Text = Convert.ToString(dataGridView1[2, cr].Value);                    
                }
            }
        }
    }
}
