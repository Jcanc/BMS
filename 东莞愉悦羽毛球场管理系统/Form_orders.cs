using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;

namespace Sales
{
    public partial class Form_orders : Form
    {
        private string _picturePrefix = "./Images/����/";
        public Form_orders()
        {
            InitializeComponent();
        }

        private void Form_orders_Load(object sender, EventArgs e)
        {
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
            sqlc.CommandText = "select ���غ�,���� ����,���,ʱ��,ͼƬ,��ע from ����";
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
                if (comboBox1.Text == "")
                {
                    MessageBox.Show("��Ա����Ϊ�գ�");
                    comboBox1.Focus();
                    comboBox1.SelectAll();
                    return;
                }
                if (textBox2.Text == "")
                {
                    MessageBox.Show("ԤԼ���ز���Ϊ�գ�");
                    textBox2.Focus();
                    textBox2.SelectAll();
                    return;
                }

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
                string sSql = "";
                //����ʱ����Ƿ�����ͨ����˵�ԤԼ
                sSql = $"select * from ���ⵥ where ���غ�='{textBox2.Tag}' and (�볡ʱ�� between '{dateTimePicker1.Value}' and '{dateTimePicker2.Value}' or �볡ʱ�� between '{dateTimePicker1.Value}' and '{dateTimePicker2.Value}')";
                sqlc.CommandText = sSql;
                sql.Open();//�����ݿ�
                DataSet ds = new DataSet();
                SqlDataAdapter sda = new SqlDataAdapter(sqlc);//�������dataset���ݼ��ĺ���
                sda.Fill(ds, "t1");//������ݼ�
                if (ds.Tables[0].Rows.Count > 0)
                {
                    MessageBox.Show("��ʱ�������ԤԼ��");
                    return;
                }

                //�������
                sSql = "insert into ���ⵥ values('" + textBox1.Text + "','" + comboBox5.Items[comboBox1.SelectedIndex] + "','" + DateTime.Now + "','" + textBox2.Tag + "','" + dateTimePicker1.Value + "','" + dateTimePicker2.Value + "','" + textBox4.Text + "','" + textBox5.Text + "','δ���','" + textBox7.Text + "','" + login.yhh + "')";
                sqlc.CommandText = sSql;
                int result = sqlc.ExecuteNonQuery();//ִ����䷵��Ӱ�������
                if (result > 0)//���ִ�гɹ��򷵻�1
                {
                    MessageBox.Show("ԤԼ�ɹ�");

                    textBox1.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
                    textBox2.Text = "";
                    textBox2.Tag = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    textBox7.Text = "";
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


        private void dateTimePicker2_Leave(object sender, EventArgs e)
        {
            var date1 = dateTimePicker2.Value;
            var time = Math.Round(date1.Subtract(dateTimePicker1.Value).TotalHours, 2);
            if (time > 0)
            {
                if (!string.IsNullOrEmpty(textBox3.Text))
                {
                    textBox4.Text = Math.Round(time, 2).ToString();
                    textBox5.Text = Math.Round(double.Parse(time.ToString()) * double.Parse(textBox3.Text), 2).ToString();
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

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name.Equals("ͼƬ"))
            {
                string path = e.Value.ToString();
                e.Value = GetImage(_picturePrefix + path);
            }
        }

        public Image GetImage(string path)
        {
            Image result = null;
            try
            {
                FileStream fs = new FileStream(path, FileMode.Open);
                result = Image.FromStream(fs);
                fs.Close();
            }
            catch
            {

            }
            return result;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (MessageBox.Show("ȷ��ҪԤԼ�˳�����", "��ʾ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    textBox2.Tag = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                }
            }
        }
    }
}