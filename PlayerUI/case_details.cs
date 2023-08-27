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
    public partial class case_details : Form
    {
        dbaccess db = new dbaccess();
        public SqlCommand cmd = new SqlCommand();
        public int i;
        public string s;
        public case_details()
        {
            InitializeComponent();caseclear();
        }

        public void caseclear()
        {
            foreach (Control c in panel1.Controls)
            {
                if (c is System.Windows.Forms.TextBox)
                    c.Text = "";
                else if (c is System.Windows.Forms.ComboBox)
                    panel1.Controls.SetChildIndex(c, -1);
            }
            sendcall();
        }

        public void casedis()
        {
            s = "select * from [case_det]";
            dataGridView1.DataSource = db.FetchData(s);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_NEW_Click(object sender, EventArgs e)
        {
            caseclear();
        }

        private void button_list_Click(object sender, EventArgs e)
        {
            casedis();
        }
              
        public void alldis(ComboBox c , String s ,String a)
        {
            try
            {
                SqlDataAdapter sqlDa = new SqlDataAdapter(s, db.con);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                c.DisplayMember = a;
                c.ValueMember = a;

                DataRow topItem = dtbl.NewRow();
                topItem[0] = null;

                dtbl.Rows.InsertAt(topItem, 0);
                c.DataSource = dtbl;
            }
            catch (Exception ex)
            {
                // write exception info to log or anything else
                MessageBox.Show(ex.Message, "Error occured!");
            }
        }

        public void sendcall()
        {
            sendstaus();sendlawyer();sendcourt();sendcasetype();
        }
        public void sendstaus()
        {
            alldis(comboBox1, "select staus from [Table]","staus");
        }

        public void sendcourt()
        {
            alldis(comboBox4, "select cname from court", "cname");
        }

        public void sendlawyer()
        {
            alldis(comboBox3, "select lawyern from [lawyer]", "lawyern");
        }
        public void sendcasetype()
        {
            alldis(comboBox2, "select casetype from [case]", "casetype");
        }
       

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int cr = dataGridView1.CurrentRow.Index;
                if (dataGridView1.CurrentRow.Cells["c_id"].Value != DBNull.Value)
                {
                    textBox_LYERdETILS.Text = Convert.ToString(dataGridView1[0, cr].Value);
                    comboBox2.SelectedValue = Convert.ToString(dataGridView1[1, cr].Value);
                    textBox_ADDRESS.Text = Convert.ToString(dataGridView1[2, cr].Value);
                    textBox_PINCODE.Text = Convert.ToString(dataGridView1[3, cr].Value);
                    textBox_PHONE.Text = Convert.ToString(dataGridView1[4, cr].Value);
                    comboBox4.SelectedValue = Convert.ToString(dataGridView1[5, cr].Value);
                    textBox_QUALIFICATION.Text = Convert.ToString(dataGridView1[6, cr].Value);
                    comboBox1.SelectedValue = Convert.ToString(dataGridView1[7, cr].Value);
                    comboBox3.SelectedValue = Convert.ToString(dataGridView1[8, cr].Value);           

                }
            }
        }

        private void textBox_LYERdETILS_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            if (textBox_LYERdETILS.Text != "" && comboBox2.SelectedValue != null  && textBox_ADDRESS.Text != "" && textBox_PINCODE.Text != "" && textBox_PHONE.Text.Length == 10 && comboBox4.SelectedValue != null && textBox_QUALIFICATION.Text != "" && comboBox1.Text != "" &&   comboBox3.SelectedValue != null)
            {
                cmd.CommandText = ("Select * From case_det Where c_id ='" + textBox_LYERdETILS.Text.Trim() + "'  ");
                if (db.checkexist(cmd) == false)
                {
                    SqlCommand cmd = new SqlCommand("Insert into case_det values( @a,@b,@c,@d,@e,@f,@g,@h,@i)", db.con);
                    cmd.Parameters.AddWithValue("@a", textBox_LYERdETILS.Text);
                    cmd.Parameters.AddWithValue("@b", comboBox2.SelectedValue);
                    cmd.Parameters.AddWithValue("@c", textBox_ADDRESS.Text);
                    cmd.Parameters.AddWithValue("@d", textBox_PINCODE.Text);
                    cmd.Parameters.AddWithValue("@e", textBox_PHONE.Text);
                    cmd.Parameters.AddWithValue("@f", comboBox4.SelectedValue);
                    cmd.Parameters.AddWithValue("@g", textBox_QUALIFICATION.Text);
                    cmd.Parameters.AddWithValue("@h", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@i", comboBox3.SelectedValue);

                    i = db.InsertData(cmd);
                    if (i == 1)
                        MessageBox.Show("saved");
                    caseclear();
                }
                else
                {
                    MessageBox.Show("ENTER unique Case ID");
                }
            }
            else
            {
                MessageBox.Show("ENTER ALL DETAILS");
            }
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            cmd.CommandText = ("Select * From case_det Where c_id ='" + textBox_LYERdETILS.Text.Trim() + "'  ");
            if (db.checkexist(cmd) == true)
            {
                if (textBox_LYERdETILS.Text != "" && comboBox2.SelectedValue != null && textBox_ADDRESS.Text != "" && textBox_PINCODE.Text != "" && textBox_PHONE.Text.Length == 10 && comboBox4.SelectedValue != null && textBox_QUALIFICATION.Text != "" && comboBox1.SelectedValue != null && comboBox3.SelectedValue != null)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = ("update  case_det set c_id= @a ,c_type = @b , address = @c , client = @d ,phone = @e, court = @f, opponent = @g, status = @h ,lawyer=@i Where c_id = @a");
                    cmd.Parameters.AddWithValue("@a", textBox_LYERdETILS.Text);
                    cmd.Parameters.AddWithValue("@b", comboBox2.SelectedValue);
                    cmd.Parameters.AddWithValue("@c", textBox_ADDRESS.Text);
                    cmd.Parameters.AddWithValue("@d", textBox_PINCODE.Text);
                    cmd.Parameters.AddWithValue("@e", textBox_PHONE.Text);
                    cmd.Parameters.AddWithValue("@f", comboBox4.SelectedValue);
                    cmd.Parameters.AddWithValue("@g", textBox_QUALIFICATION.Text);
                    cmd.Parameters.AddWithValue("@h", comboBox1.SelectedValue);
                    cmd.Parameters.AddWithValue("@i", comboBox3.SelectedValue);
                    db.ExecuteQuery(cmd);
                     MessageBox.Show("ROW UPDATED");
                }
                 
            }
            else
            {
                MessageBox.Show("ENTER Known Case ID");
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            cmd.CommandText = ("Select * From case_det Where c_id ='" + textBox_LYERdETILS.Text.Trim() + "'  ");
            if (db.checkexist(cmd) == true)
            {
                cmd.CommandText = ("delete from  case_det  Where c_id ='" + textBox_LYERdETILS.Text.Trim() + "'  ");
                db.ExecuteQuery(cmd); caseclear(); MessageBox.Show("ROW Deleted");
            }
            else
            {
                MessageBox.Show("ENTER Known Case ID");
            }
        }
    }
}
