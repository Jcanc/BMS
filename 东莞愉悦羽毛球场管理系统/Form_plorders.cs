using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Sales
{
    public partial class Form_plorders : Form
    {
        private string copyFiles = string.Empty;
        public Form_plorders()
        {
            InitializeComponent();
        }

        private void bind()
        {
            SqlConnection sql = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
            SqlCommand sqlc = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
            sqlc.Connection = sql;//���ò�ѯ�������������Ϊ��������ݿ�������
            //��ѯ������Ϣ
            string sSqlText = "select a.����,a.������,a.�Ա�,b.���� ����,a.��ԤԼ��ʼʱ��,a.��ԤԼ����ʱ��,a.���� ��������,a.ʱ�� from ���� a left join ���� b on a.���غ�=b.���غ� where 1=1";
            if (textBox4.Text != "")
            {
                sSqlText += " and a.���� like '%" + textBox4.Text + "%'";
            }
            sqlc.CommandText = sSqlText;

            sql.Open();//�����ݿ�
            DataSet ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter(sqlc);//�������dataset���ݼ��ĺ���
            sda.Fill(ds, "t1");//������ݼ�
            dataGridView1.DataSource = ds.Tables["t1"].DefaultView;
            dataGridView1.ClearSelection();
        }

        private void Form_goods_Load(object sender, EventArgs e)
        {
            comboBox1_DropDown(sender, e);
            comboBox1.SelectedIndex = 0;
            textBox9.Text = login.xm;
            bind();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "" || textBox3.Text == "")
                {
                    MessageBox.Show("��ѡ��ԤԼ������");
                    return;
                }
                if (dateTimePicker1.Value < DateTime.Parse(dateTimePicker1.Tag.ToString()) || dateTimePicker2.Value > DateTime.Parse(dateTimePicker2.Tag.ToString()))
                {
                    MessageBox.Show("��ѡ����Ч��ԤԼʱ�䣡");
                    return;
                }
                SqlConnection sql = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
                SqlCommand sqlc = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
                sqlc.Connection = sql;//���ò�ѯ�������������Ϊ��������ݿ�������
                //�������
                string sSql = "";
                sSql = "insert into ����ԤԼ values('" + textBox1.Text + "','" + comboBox2.Items[comboBox1.SelectedIndex] + "','" + textBox3.Tag + "','" + DateTime.Now + "','" + dateTimePicker1.Value + "','" + dateTimePicker2.Value + "','δ���','" + textBox5.Text + "', '" + textBox2.Text + "', '" + login.yhh + "')";
                sqlc.CommandText = sSql;
                sql.Open();//�����ݿ�
                int result = sqlc.ExecuteNonQuery();//ִ����䷵��Ӱ�������
                if (result > 0)//���ִ�гɹ��򷵻�1
                {
                    MessageBox.Show("ԤԼ�ɹ�");

                    textBox1.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
                    textBox3.Text = "";
                    textBox3.Tag = "";
                    textBox5.Text = "";
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
                    textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    textBox3.Tag = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    dateTimePicker1.Tag = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                    dateTimePicker2.Tag = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                    textBox7.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                    textBox8.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            zl();
        }

        private void zl()
        {
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            bind();
        }

        private void dateTimePicker1_Leave(object sender, EventArgs e)
        {
            dateTimePicker2.Select();
        }

        private void dateTimePicker2_Leave(object sender, EventArgs e)
        {
            var date1 = dateTimePicker2.Value;
            var time = Math.Round(date1.Subtract(dateTimePicker1.Value).TotalHours, 2);
            if (time > 0)
            {
                if (!string.IsNullOrEmpty(textBox7.Text))
                {
                    textBox6.Text = Math.Round(time, 2).ToString();
                    textBox2.Text = Math.Round(double.Parse(time.ToString()) * double.Parse(textBox7.Text), 2).ToString();
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

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            SqlConnection sql = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
            SqlCommand sqlc = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
            sqlc.Connection = sql;//���ò�ѯ�������������Ϊ��������ݿ�������
            //ɾ�����
            if (login.qx == "��Ա")
            {
                sqlc.CommandText = $"select ����,�û��� from �û� where �û���={login.yhh}";
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
                comboBox2.Items.Add(sdr.GetValue(1));
            }
            sql.Close();
        }

        //public System.Drawing.Image GetImage(string path)
        //{

        //}
    }
}