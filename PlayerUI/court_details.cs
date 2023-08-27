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
    public partial class court_details : Form
    {
        dbaccess db = new dbaccess();
        public SqlCommand cmd = new SqlCommand();
        public int i;
        public string s;
        public court_details()
        {
            InitializeComponent();
        }

        public void courtclear()
        {
            foreach (Control c in panel1.Controls)
            {
                if (c is System.Windows.Forms.TextBox)
                    c.Text = "";
            }
        }

        public void courtdis()
        {
            s = "select * from court";
            dataGridView1.DataSource = db.FetchData(s);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_NEW_Click(object sender, EventArgs e)
        {
            courtclear();
        }

        private void button_list_Click(object sender, EventArgs e)
        {
            courtdis();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int cr = dataGridView1.CurrentRow.Index;
                if (dataGridView1.CurrentRow.Cells["cname"].Value != DBNull.Value)
                {
                    textBox_LYERdETILS.Text = Convert.ToString(dataGridView1[1, cr].Value);
                    textBox_ADDRESS.Text = Convert.ToString(dataGridView1[2, cr].Value);

                }
            }
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            if (textBox_LYERdETILS.Text != "" && textBox_ADDRESS.Text != "")
            {
                cmd.CommandText = ("Select * From court Where cname ='" + textBox_LYERdETILS.Text.Trim() + "'  ");
                if (db.checkexist(cmd) == false)
                {
                    SqlCommand cmd = new SqlCommand("Insert into court values(@a,@b)", db.con);
                    cmd.Parameters.AddWithValue("@a", textBox_LYERdETILS.Text);
                    cmd.Parameters.AddWithValue("@b", textBox_ADDRESS.Text);
                    int i = db.InsertData(cmd);
                    if (i == 0)
                        MessageBox.Show("saved");
                    courtclear(); 
                }
                else
                {
                    MessageBox.Show("ENTER unique court name");
                }
            }
            else
            {
                MessageBox.Show("ENTER all details");
            }
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            cmd.CommandText = ("Select * From court Where cname ='" + textBox_LYERdETILS.Text.Trim() + "'  ");
            if (db.checkexist(cmd) == true)
            {
                cmd.Parameters.Clear();
                cmd.CommandText = ("update  [court] set cname = @x, address = @b Where cname = @x  ");
                cmd.Parameters.AddWithValue("@x", textBox_LYERdETILS.Text);
                cmd.Parameters.AddWithValue("@b", textBox_ADDRESS.Text);
                db.ExecuteQuery(cmd); courtclear(); MessageBox.Show("ROW UPDATED");
            }
            else
            {
                MessageBox.Show("ENTER Known Court name");
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            cmd.CommandText = ("Select * From court Where cname ='" + textBox_LYERdETILS.Text.Trim() + "'  ");
            if (db.checkexist(cmd) == true)
            {
                cmd.CommandText = ("delete from  court  Where cname ='" + textBox_LYERdETILS.Text.Trim() + "'  ");
                db.ExecuteQuery(cmd); courtclear(); MessageBox.Show("ROW Deleted");
            }
            else
            {
                MessageBox.Show("ENTER Known Court name");
            }
        }
    }
}
