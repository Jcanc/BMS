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
    public partial class Form_goods : Form
    {
        private string copyFiles = string.Empty;
        public Form_goods()
        {
            InitializeComponent();
        }

        private void Form_goods_Load(object sender, EventArgs e)
        {
            textBox1.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
            SqlConnection sql = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
            SqlCommand sqlc = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
            sqlc.Connection = sql;//���ò�ѯ�������������Ϊ��������ݿ�������
            //��ѯ������Ϣ
            sqlc.CommandText = "select sort,goodsname,prices,dates,num,sums from Goods";
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
                if (comboBox1.Text  == ""||textBox2.Text=="")
                {
                    MessageBox.Show("��Ʒ���಻��Ϊ�ջ���Ʒ���Ʋ���Ϊ�գ�");
                }
                else
                {

                    SqlConnection sql = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
                    SqlCommand sqlc = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
                    sqlc.Connection = sql;//���ò�ѯ�������������Ϊ��������ݿ�������
                    //�������
                    string sSql = "";
                    if (button1.Text == "����")
                    {
                        sSql = "insert into Goods(goodsname,sort,prices,dates,num,sums) values('" + textBox2.Text + "','" + comboBox1.Text + "','" + textBox3.Text + "','" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "','" + textBox1.Text + "','" + textBox4.Text + "')";
                    }
                    else
                    {
                        sSql = "update Goods set sums='"+textBox4.Text+"',goodsname='" + textBox2.Text + "',sort='" + comboBox1.Text + "',prices='" + textBox3.Text + "',dates='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' where num='" + textBox1.Text + "'";
                    }
                    sqlc.CommandText = sSql;
                    sql.Open();//�����ݿ�
                    int result = sqlc.ExecuteNonQuery();//ִ����䷵��Ӱ�������
                    if (result > 0)//���ִ�гɹ��򷵻�1
                    {

                        MessageBox.Show("�����ɹ�");
                        button1.Text = "����";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox1.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
                        comboBox1.Text = "";
                        button2.Visible = false;
                        Form_goods_Load(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("����ʧ�ܣ�");
                    }
                    sql.Close();



                }
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

                if (MessageBox.Show("Ҫ�޸ĵ�ǰ��¼��", "��ʾ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                    dateTimePicker1.Value =DateTime.Parse(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
                    textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    button1.Text = "����";
                    button2.Visible = true;

                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                if (MessageBox.Show("ȷ��Ҫɾ����ǰ��Ϣ��", "��ʾ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SqlConnection sql = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
                    SqlCommand sqlc = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
                    sqlc.Connection = sql;//���ò�ѯ�������������Ϊ��������ݿ�������
                    //ɾ�����

                    sqlc.CommandText = "delete from Goods where num='" + textBox1.Text + "'";
                    sql.Open();//�����ݿ�
                    int result = sqlc.ExecuteNonQuery();//ִ����䷵��Ӱ�������
                    if (result > 0)//���ִ�гɹ��򷵻�1
                    {

                        MessageBox.Show("�����ɹ�");
                        button1.Text = "����";
                        button2.Visible = false;
                        textBox2.Text = "";
                        textBox3.Text = "";
                        comboBox1.Text = "";
                        textBox1.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
                        Form_goods_Load(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("����ʧ�ܣ�");
                    }
                    sql.Close();
                }

            }
            else
            {
                MessageBox.Show("��ѡ��Ҫɾ���ļ�¼��");
            }
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            SqlConnection sql = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
            SqlCommand sqlc = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
            sqlc.Connection = sql;//���ò�ѯ�������������Ϊ��������ݿ�������
            //ɾ�����
            sqlc.CommandText = "select sort from Sort" ;
            sql.Open();//�����ݿ�
            SqlDataReader sdr = sqlc.ExecuteReader();
            while (sdr.Read())
            {
                comboBox1.Items.Add(sdr.GetValue(0));
            }
            sql.Close();
        }

        private void comboBox1_DropDownClosed(object sender, EventArgs e)
        {

        }

        

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
        }

        //public System.Drawing.Image GetImage(string path)
        //{
           
        //}
    }
}