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
    public partial class CITYDETLS : Form
    {
        dbaccess db = new dbaccess();
        public SqlCommand cmd = new SqlCommand();
        public int i;
        public string s;
        public CITYDETLS()
        {
            InitializeComponent();
        }

        public void cityclear()
        {
            foreach (Control c in panel1.Controls)
            {
                if (c is System.Windows.Forms.TextBox)
                    c.Text = "";
            }
        }

        public void citydis()
        {
            s = "select * from city";
            dataGridView1.DataSource = db.FetchData(s);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_NEW_Click(object sender, EventArgs e)
        {
            cityclear();
        }

        private void button_list_Click(object sender, EventArgs e)
        {
            citydis();
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            if (textBox_cityname.Text != "" && textBox_district.Text != "")
            {
                cmd.CommandText = ("Select * From city Where cityn ='" + textBox_cityname.Text.Trim() + "'  ");
                if (db.checkexist(cmd) == false)
                {
                    SqlCommand cmd = new SqlCommand("Insert into city values(@a,@b)", db.con);
                    cmd.Parameters.AddWithValue("@a", textBox_cityname.Text);
                    cmd.Parameters.AddWithValue("@b", textBox_district.Text);                    
                    int i = db.InsertData(cmd);
                    if (i == 0)
                        MessageBox.Show("saved");
                    cityclear();
                }
                else
                {
                    MessageBox.Show("ENTER unique CITY NAME");
                }
            }
            else
            {
                MessageBox.Show("ENTER all details");
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int cr = dataGridView1.CurrentRow.Index;
                if (dataGridView1.CurrentRow.Cells["cityn"].Value != DBNull.Value)
                {
                    textBox_cityname.Text = Convert.ToString(dataGridView1[1, cr].Value);
                    textBox_district.Text = Convert.ToString(dataGridView1[2, cr].Value);
                    

                }
            }
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            cmd.Parameters.Clear();
            cmd.CommandText = ("Select * From [city] Where cityn ='" + textBox_cityname.Text.Trim() + "'  ");
            if (db.checkexist(cmd) == true)
            {
                cmd.Parameters.Clear();
                cmd.CommandText = ("update  [city] set cityn = @x, district = @b Where cityn = @a");
                cmd.Parameters.AddWithValue("@x", textBox_cityname.Text);
                cmd.Parameters.AddWithValue("@b", textBox_district.Text);        
                cmd.Parameters.AddWithValue("@a", textBox_cityname.Text);

                db.ExecuteQuery(cmd); cityclear(); MessageBox.Show("ROW UPDATED");
            }
            else
            {
                MessageBox.Show("ENTER Known CITY NAME");
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            cmd.CommandText = ("Select * From city Where cityn ='" + textBox_cityname.Text.Trim() + "'  ");
            if (db.checkexist(cmd) == true)
            {
                cmd.CommandText = ("delete from  city  Where cityn ='" + textBox_cityname.Text.Trim() + "'  ");
                db.ExecuteQuery(cmd);cityclear(); MessageBox.Show("ROW Deleted");
            }
            else
            {
                MessageBox.Show("ENTER Known CITY");
            }
        }
    }
}
