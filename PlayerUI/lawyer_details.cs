using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PlayerUI
{
    public partial class lawyer_details : Form
    {
        dbaccess db = new dbaccess();
        public SqlCommand cmd = new SqlCommand();
        public int i;
        public string s;
        public string pat = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
        public lawyer_details()
        {
            InitializeComponent();qualdis();citydis();
        }

        public void lawclear()
        {
            foreach (Control c in panel1.Controls)
            {
                if (c is System.Windows.Forms.TextBox)
                    c.Text = "";
                else if (c is System.Windows.Forms.ComboBox)
                    panel1.Controls.SetChildIndex(c, -1);
            }
            qualdis();citydis();
        }

        public void lawidis()
        {
            s = "select * from lawyer";
            dataGridView1.DataSource = db.FetchData(s);
        }

        public void qualdis()
        {
            try
            {
                SqlDataAdapter sqlDa = new SqlDataAdapter("select qualn from qual", db.con);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                comboBox2.DisplayMember = "qualn";
                comboBox2.ValueMember = "qualn";

                DataRow topItem = dtbl.NewRow();
                topItem[0] = null;

                dtbl.Rows.InsertAt(topItem, 0);
                comboBox2.DataSource = dtbl;
            }
            catch (Exception ex)
            {
                // write exception info to log or anything else
                MessageBox.Show(ex.Message, "Error occured!");
            }
        }

        public void citydis()
        {
            try
            {
                SqlDataAdapter sqlDa = new SqlDataAdapter("select cityn from city", db.con);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                comboBox1.DisplayMember = "cityn";
                comboBox1.ValueMember = "cityn";

                DataRow topItem = dtbl.NewRow();
                topItem[0] = null;

                dtbl.Rows.InsertAt(topItem, 0);
                comboBox1.DataSource = dtbl;
            }
            catch (Exception ex)
            {
                // write exception info to log or anything else
                MessageBox.Show(ex.Message, "Error occured!");
            }
        }
        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void button_NEW_Click(object sender, EventArgs e)
        {
            lawclear();
        }

        private void textBox_PINCODE_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsControl(e.KeyChar)&&!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox_PHONE_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox_GMAIL_Leave(object sender, EventArgs e)
        {
            if (Regex.IsMatch(textBox_GMAIL.Text, pat) == false)
            {
                textBox_GMAIL.Focus();
                errorProvider1.SetError(this.textBox_GMAIL, "INVALID EMAIL");

            }
            else
            {
                errorProvider1.Clear();
            }

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
                if (dataGridView1.CurrentRow.Cells["L_id"].Value != DBNull.Value)
                {
                    textBox_LYERid.Text = Convert.ToString(dataGridView1[0, cr].Value);                    
                    textBox_LAWNAME.Text = Convert.ToString(dataGridView1[1, cr].Value);
                    textBox_ADDRESS.Text = Convert.ToString(dataGridView1[2, cr].Value);
                    textBox_PINCODE.Text = Convert.ToString(dataGridView1[3, cr].Value);
                    textBox_PHONE.Text = Convert.ToString(dataGridView1[4, cr].Value);
                    textBox_GMAIL.Text = Convert.ToString(dataGridView1[5, cr].Value);
                    comboBox2.SelectedValue = Convert.ToString(dataGridView1[6, cr].Value);
                    textBox_DESIG.Text = Convert.ToString(dataGridView1[7, cr].Value);
                    comboBox1.SelectedValue = Convert.ToString(dataGridView1[8, cr].Value);


                }
            }
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            if (textBox_LYERid.Text != "" && textBox_LAWNAME.Text != "" && textBox_ADDRESS.Text != "" && textBox_PINCODE.Text.Length == 6 && textBox_PHONE.Text.Length == 10 && textBox_GMAIL.Text != "" && comboBox2.SelectedValue != null && textBox_DESIG.Text != "" && comboBox1.SelectedValue != null)
            {
                cmd.CommandText = ("Select * From lawyer Where L_id ='" + textBox_LYERid.Text.Trim() + "'  ");
                if (db.checkexist(cmd) == false)
                {
                    SqlCommand cmd = new SqlCommand("Insert into lawyer values( @a,@b,@c,@d,@e,@f,@g,@h,@i)", db.con);
                    cmd.Parameters.AddWithValue("@a", Convert.ToInt32(textBox_LYERid.Text));
                    cmd.Parameters.AddWithValue("@b", textBox_LAWNAME.Text);
                    cmd.Parameters.AddWithValue("@c", textBox_ADDRESS.Text);
                    cmd.Parameters.AddWithValue("@d", Convert.ToInt32(textBox_PINCODE.Text));
                    cmd.Parameters.AddWithValue("@e", textBox_PHONE.Text);
                    cmd.Parameters.AddWithValue("@f", textBox_GMAIL.Text);
                    cmd.Parameters.AddWithValue("@g", comboBox2.SelectedValue);
                    cmd.Parameters.AddWithValue("@h", textBox_DESIG.Text);
                    cmd.Parameters.AddWithValue("@i", comboBox1.SelectedValue);

                    i = db.InsertData(cmd);
                    if (i == 1)
                        MessageBox.Show("saved"); lawclear();
                }
                else
                {
                    MessageBox.Show("ENTER unique Laywer ID");
                }
            }
            else
            {
                MessageBox.Show("ENTER ALL DETAILS");
            }
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            cmd.CommandText = ("Select * From lawyer Where L_id ='" + textBox_LYERid.Text.Trim() + "'  ");
            if (db.checkexist(cmd) == true)
            {
                if (textBox_LYERid.Text != "" && textBox_LAWNAME.Text != "" && textBox_ADDRESS.Text != "" && textBox_PINCODE.Text.Length == 6 && textBox_PHONE.Text.Length == 10 && textBox_GMAIL.Text != "" && comboBox2.SelectedValue != null && textBox_DESIG.Text != "" && comboBox1.SelectedValue != null)

                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = ("update  lawyer set L_id= @a ,lawyern = @b , address = @c , pin = @d ,phone = @e, email=@f,qual=@g,desig=@h,city=@i Where L_id = @x");
                    cmd.Parameters.AddWithValue("@a", textBox_LYERid.Text);
                    cmd.Parameters.AddWithValue("@b", textBox_LAWNAME.Text);
                    cmd.Parameters.AddWithValue("@c", textBox_ADDRESS.Text);
                    cmd.Parameters.AddWithValue("@d", textBox_PINCODE.Text);
                    cmd.Parameters.AddWithValue("@e", textBox_PHONE.Text);
                    cmd.Parameters.AddWithValue("@f", textBox_GMAIL.Text);
                    cmd.Parameters.AddWithValue("@g", comboBox2.SelectedValue);
                    cmd.Parameters.AddWithValue("@h", textBox_DESIG.Text);
                    cmd.Parameters.AddWithValue("@i", comboBox1.SelectedValue);
                    cmd.Parameters.AddWithValue("@x", textBox_LYERid.Text);
                    db.ExecuteQuery(cmd); 
                    lawclear(); MessageBox.Show("ROW UPDATED");
                }                
            }
            else
            {
                MessageBox.Show("ENTER Known LAWYER ID");
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            cmd.CommandText = ("Select * From lawyer Where L_id ='" + textBox_LYERid.Text.Trim() + "'  ");
            if (db.checkexist(cmd) == true)
            {
                cmd.CommandText = ("delete from  lawyer  Where L_id ='" + textBox_LYERid.Text.Trim() + "'  ");
                db.ExecuteQuery(cmd); lawclear(); MessageBox.Show("ROW Deleted");
            }
            else
            {
                MessageBox.Show("ENTER Known LAWYER ID");
            }
        }

        private void textBox_LYERid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
