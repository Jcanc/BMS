using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Sales
{
    public partial class Form_orders : Form
    {
        public Form_orders()
        {
            InitializeComponent();
        }

        private void Form_orders_Load(object sender, EventArgs e)
        {
            comboBox2_DropDown(sender, e);
            comboBox4_DropDown(sender, e);
            comboBox1.SelectedIndex = 0;
            textBox1.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            textBox6.Text = login.xm;

            SqlConnection sql = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
            SqlCommand sqlc = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
            sqlc.Connection = sql;//���ò�ѯ�������������Ϊ��������ݿ�������
            //��ѯ������Ϣ
            sqlc.CommandText = "select a.���ⵥ��,b.���� ��Ա,a.�µ�����,c.���� ����,a.�볡ʱ��,a.�볡ʱ��,a.ʱ��,c.ʱ��,a.�տ��,a.״̬,a.��ע,d.���� ������ from ���ⵥ a left join �û� b on a.�û���=b.�û��� left join ���� c on a.���غ�=c.���غ� left join �û� d on a.�����˺���=d.�û���";
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
                if (textBox2.Text == "")
                {
                    MessageBox.Show("ԤԼʱ�䲻��Ϊ�գ�");
                    textBox2.Focus();
                    textBox2.SelectAll();
                    return;
                }
                if (comboBox1.Text == "")
                {
                    MessageBox.Show("��Ա����Ϊ�գ�");
                    comboBox1.Focus();
                    comboBox1.SelectAll();
                    return;
                }
                if (comboBox2.Text == "")
                {
                    MessageBox.Show("ԤԼ���ز���Ϊ�գ�");
                    comboBox2.Focus();
                    comboBox2.SelectAll();
                    return;
                }

                textBox2.Text = comboBox3.Items[comboBox2.SelectedIndex].ToString();
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
                sSql = "insert into ���ⵥ values('" + textBox1.Text + "','" + comboBox5.Items[comboBox1.SelectedIndex] + "','" + DateTime.Now + "','" + comboBox4.Items[comboBox2.SelectedIndex].ToString() + "','" + dateTimePicker1.Value + "','" + dateTimePicker2.Value + "','" + textBox3.Text + "','" + textBox4.Text + "','δ���','" + textBox5.Text + "','" + login.yhh + "')";
                sqlc.CommandText = sSql;
                sql.Open();//�����ݿ�
                int result = sqlc.ExecuteNonQuery();//ִ����䷵��Ӱ�������
                if (result > 0)//���ִ�гɹ��򷵻�1
                {
                    MessageBox.Show("ԤԼ�ɹ�");

                    textBox1.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
                    comboBox2.Items.Clear();
                    comboBox3.Items.Clear();
                    comboBox4.Items.Clear();
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    Form_orders_Load(sender, e);
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

        //private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.ColumnIndex == 0)
        //    {
        //        if (MessageBox.Show("ȷ��ҪԤԼ��ǰ������", "��ʾ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //        {
        //            textBox1.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
        //            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
        //            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();

        //        }
        //    }
        //}

        private void comboBox2_DropDown(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            SqlConnection sql = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
            SqlCommand sqlc = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
            sqlc.Connection = sql;//���ò�ѯ�������������Ϊ��������ݿ�������
            sqlc.CommandText = "select ����,ʱ��,���غ� from ����";
            sql.Open();//�����ݿ�
            SqlDataReader sdr = sqlc.ExecuteReader();
            while (sdr.Read())
            {
                comboBox2.Items.Add(sdr.GetValue(0));
                comboBox3.Items.Add(sdr.GetValue(1));
                comboBox4.Items.Add(sdr.GetValue(2));
            }
            sql.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex >= 0)
            {
                textBox2.Text = comboBox3.Items[comboBox2.SelectedIndex].ToString();
                var date1 = dateTimePicker2.Value;
                var time = Math.Round(date1.Subtract(dateTimePicker1.Value).TotalHours, 2);
                if (time > 0)
                {
                    textBox4.Text = Math.Round(double.Parse(time.ToString()) * double.Parse(textBox2.Text), 2).ToString();
                }
            }
        }

        private void dateTimePicker2_Leave(object sender, EventArgs e)
        {
            var date1 = dateTimePicker2.Value;
            var time = Math.Round(date1.Subtract(dateTimePicker1.Value).TotalHours, 2);
            if (time > 0)
            {
                if (!string.IsNullOrEmpty(textBox2.Text))
                {
                    textBox3.Text = Math.Round(time, 2).ToString();
                    textBox4.Text = Math.Round(double.Parse(time.ToString()) * double.Parse(textBox2.Text), 2).ToString();
                }
                else
                {
                    MessageBox.Show("����ѡ�񳡵أ�");
                }
            }
        }

        private void dateTimePicker1_Leave(object sender, EventArgs e)
        {
            dateTimePicker2.Select();
        }

        private void comboBox4_DropDown(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox5.Items.Clear();
            SqlConnection sql = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
            SqlCommand sqlc = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
            sqlc.Connection = sql;//���ò�ѯ�������������Ϊ��������ݿ�������
            if (login.qx == "��Ա")
            {
                sqlc.CommandText = $"select ����,�û��� from �û� where �û���='{login.yhh}'";
                comboBox1.Enabled = false;
            }
            else
            {
                sqlc.CommandText = "select ����,�û��� from �û� where ��ɫ='��Ա'";
            }
            sql.Open();//�����ݿ�
            SqlDataReader sdr = sqlc.ExecuteReader();
            while (sdr.Read())
            {
                comboBox1.Items.Add(sdr.GetValue(0));
                comboBox5.Items.Add(sdr.GetValue(1));
            }
            sql.Close();
        }
    }
}