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
            string sSqlText = "select a.����,a.������,a.�Ա�,b.���� ����,a.��ԤԼ��ʼʱ��,a.��ԤԼ����ʱ��,a.���� �������� from ���� a left join ���� b on a.���غ�=b.���غ� where 1=1";
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
                sSql = "insert into ����ԤԼ values('" + textBox1.Text + "','" + login.yhh + "','" + textBox3.Tag + "','" + DateTime.Now + "','" + dateTimePicker1.Value + "','" + dateTimePicker2.Value + "','δ���','" + textBox5.Text + "')";
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
                    textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    textBox3.Tag = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    dateTimePicker1.Tag = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    dateTimePicker2.Tag = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
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

        //public System.Drawing.Image GetImage(string path)
        //{
           
        //}
    }
}