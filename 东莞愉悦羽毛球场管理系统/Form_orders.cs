using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
namespace Sales
{
    public partial class Form_orders : Form
    {
        private string copyFiles = string.Empty;
        public Form_orders()
        {
            InitializeComponent();
        }

        private void Form_goods_Load(object sender, EventArgs e)
        {
            SqlConnection sql = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
            SqlCommand sqlc = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
            sqlc.Connection = sql;//���ò�ѯ�������������Ϊ��������ݿ�������
            //��ѯ������Ϣ
            sqlc.CommandText = "select name,dates,moneys from Address";
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
                    return;
                }
                SqlConnection sql = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
                SqlCommand sqlc = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
                sqlc.Connection = sql;//���ò�ѯ�������������Ϊ��������ݿ�������
                //�������
                string sSql = "";
                sSql = "insert into Orders values('" + textBox1.Text + "','" + textBox3.Text + "','" + textBox4.Text+"','"+ textBox2.Text+"','"+textBox5.Text+"','"+ login.yh + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','δ���')";
                sqlc.CommandText = sSql;
                sql.Open();//�����ݿ�
                int result = sqlc.ExecuteNonQuery();//ִ����䷵��Ӱ�������
                if (result > 0)//���ִ�гɹ��򷵻�1
                {
                    MessageBox.Show("ԤԼ�ɹ�");

                    textBox1.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
                    textBox3.Text = "";
                    textBox2.Text = "";
                    textBox4.Text = "";
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
                    textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();

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

        //public System.Drawing.Image GetImage(string path)
        //{
           
        //}
    }
}