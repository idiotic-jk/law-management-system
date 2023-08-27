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
    public partial class Lawyer_qualification : Form
    {
        dbaccess db = new dbaccess();
        public SqlCommand cmd = new SqlCommand();
        public int i;
        public string s;
        public Lawyer_qualification()
        {
            InitializeComponent();
        }

        public void lawclear()
        {
            foreach (Control c in panel1.Controls)
            {
                if (c is System.Windows.Forms.TextBox)
                    c.Text = "";
            }
        }

        public void lawidis()
        {
            s = "select * from qual";
            dataGridView1.DataSource = db.FetchData(s);
        }

        private void button_NEW_Click(object sender, EventArgs e)
        {
            lawclear();
        }

        private void button_list_Click(object sender, EventArgs e)
        {
            lawidis();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int cr = dataGridView1.CurrentRow.Index;
                if (dataGridView1.CurrentRow.Cells["qualn"].Value != DBNull.Value)
                {
                    textBox_LYERdETILS.Text = Convert.ToString(dataGridView1[1, cr].Value);
                    textBox_LAWNAME.Text = Convert.ToString(dataGridView1[2, cr].Value);                    

                }
            }
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            if (textBox_LYERdETILS.Text != "" && textBox_LAWNAME.Text != "" )
            {
                cmd.CommandText = ("Select * From qual Where qualn ='" + textBox_LYERdETILS.Text.Trim() + "'  ");
                if (db.checkexist(cmd) == false)
                {
                    SqlCommand cmd = new SqlCommand("Insert into qual values(@a,@b)", db.con);
                    cmd.Parameters.AddWithValue("@a", textBox_LYERdETILS.Text);
                    cmd.Parameters.AddWithValue("@b", textBox_LAWNAME.Text);                    
                    int i = db.InsertData(cmd);
                    if (i == 0)
                        MessageBox.Show("saved");
                    lawclear();
                }
                else
                {
                    MessageBox.Show("ENTER unique qualification");
                }
            }
            else
            {
                MessageBox.Show("ENTER all details");
            }
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            cmd.CommandText = ("Select * From qual Where qualn ='" + textBox_LYERdETILS.Text.Trim() + "'  ");
            if (db.checkexist(cmd) == true)
            {
                cmd.Parameters.Clear();
                cmd.CommandText = ("update  [qual] set qualn = @x, details = @b Where qualn = @x  ");
                cmd.Parameters.AddWithValue("@x", textBox_LYERdETILS.Text);
                cmd.Parameters.AddWithValue("@b", textBox_LAWNAME.Text);                
                db.ExecuteQuery(cmd); lawclear(); MessageBox.Show("ROW UPDATED");
            }
            else
            {
                MessageBox.Show("ENTER Known Qualification");
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            cmd.CommandText = ("Select * From qual Where qualn ='" + textBox_LYERdETILS.Text.Trim() + "'  ");
            if (db.checkexist(cmd) == true)
            {
                cmd.CommandText = ("delete from  qual  Where qualn ='" + textBox_LYERdETILS.Text.Trim() + "'  ");
                db.ExecuteQuery(cmd); lawclear(); MessageBox.Show("ROW Deleted");
            }
            else
            {
                MessageBox.Show("ENTER Known Qualification");
            }
        }
    }
}
