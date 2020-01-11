using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Sales
{
    public partial class Form_orders : Form
    {
        private string copyFiles = string.Empty;
        public Form_orders()
        {
            InitializeComponent();
        }

        private void Form_orders_Load(object sender, EventArgs e)
        {
            comboBox2_DropDown(sender, e);
            textBox1.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
            textBoxUserName.Text = login.xm;
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;

            SqlConnection sql = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
            SqlCommand sqlc = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
            sqlc.Connection = sql;//���ò�ѯ�������������Ϊ��������ݿ�������
            //��ѯ������Ϣ
            sqlc.CommandText = "select a.���ⵥ��,b.����,a.�µ�����,c.����,a.�볡ʱ��,a.�볡ʱ��,a.ʱ��,a.ʱ��,a.�տ��,a.״̬,a.��ע from ���ⵥ a left join �û� b on a.�û���=b.�û��� left join ���� c on a.���غ�=c.���غ�";
            sql.Open();//�����ݿ�
            DataSet ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter(sqlc);//�������dataset���ݼ��ĺ���
            sda.Fill(ds, "t1");//������ݼ�
            dataGridView1.DataSource = ds.Tables["t1"].DefaultView;
            dataGridView1.ClearSelection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxTimeRent.Text == "")
                {
                    MessageBox.Show("ԤԼʱ�䲻��Ϊ�գ�");
                    return;
                }
                textBoxTimeRent.Text = comboBox1.Items[comboBox2.SelectedIndex].ToString();
                var date1 = dateTimePicker2.Value;
                var time = date1.Subtract(dateTimePicker1.Value).TotalHours;
                if (time <= 0)
                {
                    MessageBox.Show("�볡ʱ��Ӧ�ñ��볡ʱ���");
                    return;
                }
                SqlConnection sql = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
                SqlCommand sqlc = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
                sqlc.Connection = sql;//���ò�ѯ�������������Ϊ��������ݿ�������
                //�������
                string sSql = "";
                sSql = "insert into ���ⵥ values('" + textBox1.Text + "','" + login.yhh + "','" + DateTime.Now.Date + "','" + comboBox3.Items[comboBox2.SelectedIndex].ToString() + "','" + dateTimePicker1.Value + "','" + dateTimePicker2.Value + "','" + textBox2.Text + "','" + textBoxTimeRent.Text + "','" + textBox3.Text + "','δ���','" + textBox4.Text + "')";
                sqlc.CommandText = sSql;
                sql.Open();//�����ݿ�
                int result = sqlc.ExecuteNonQuery();//ִ����䷵��Ӱ�������
                if (result > 0)//���ִ�гɹ��򷵻�1
                {
                    MessageBox.Show("ԤԼ�ɹ�");

                    textBox1.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
                    textBox2.Text = "";
                    textBoxTimeRent.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                }
                else
                {
                    MessageBox.Show("����ʧ�ܣ�");
                }
                sql.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("����ʧ�ܣ����ݿ����Ѵ��ڸü�¼��");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (MessageBox.Show("ȷ��ҪԤԼ��ǰ������", "��ʾ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    textBox1.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
                    textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();

                }
            }
        }

        private void comboBox2_DropDown(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            comboBox1.Items.Clear();
            SqlConnection sql = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
            SqlCommand sqlc = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
            sqlc.Connection = sql;//���ò�ѯ�������������Ϊ��������ݿ�������
            sqlc.CommandText = "select ����,ʱ��,���غ� from ����";
            sql.Open();//�����ݿ�
            SqlDataReader sdr = sqlc.ExecuteReader();
            while (sdr.Read())
            {
                comboBox2.Items.Add(sdr.GetValue(0));
                comboBox1.Items.Add(sdr.GetValue(1));
                comboBox3.Items.Add(sdr.GetValue(2));
            }
            sql.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex >= 0)
            {
                textBoxTimeRent.Text = comboBox1.Items[comboBox2.SelectedIndex].ToString();
                var date1 = dateTimePicker2.Value;
                var time = date1.Subtract(dateTimePicker1.Value).TotalHours;
                if (time > 0)
                {
                    textBox3.Text = Math.Round(double.Parse(time.ToString()) * double.Parse(textBoxTimeRent.Text), 2).ToString();
                }
            }
        }

        private void dateTimePicker2_Leave(object sender, EventArgs e)
        {
            var date1 = dateTimePicker2.Value;
            var time = date1.Subtract(dateTimePicker1.Value).TotalHours;
            if (time > 0)
            {
                if (!string.IsNullOrEmpty(textBoxTimeRent.Text))
                {
                    textBox2.Text = Math.Round(time, 2).ToString();
                    textBox3.Text = Math.Round(double.Parse(time.ToString()) * double.Parse(textBoxTimeRent.Text), 2).ToString();
                }
                else
                {
                    MessageBox.Show("����ѡ�񳡵أ�");
                }
            }
            else
            {
                MessageBox.Show("�볡ʱ��Ӧ�ñ��볡ʱ���");
            }
        }

        private void dateTimePicker1_Leave(object sender, EventArgs e)
        {
            dateTimePicker2.Select();
        }
    }
}