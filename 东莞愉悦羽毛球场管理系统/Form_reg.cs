using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Sales
{
    public partial class Form_reg : Form
    {
        public Form_reg()
        {
            InitializeComponent();
        }

        private void Form_goods_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text != "����")
            {
                if (textBox1.Text == "" || textBox2.Text == "" || textBox4.Text == "")
                {
                    MessageBox.Show("��Ϣû����ȫ��");
                }
                else
                {
                    if (textBox1.Text.Length < 6)
                    {
                        MessageBox.Show("����������6λ��");
                        return;
                    }
                    SqlConnection sql = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
                    SqlCommand sqlc = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
                    sqlc.Connection = sql;//���ò�ѯ�������������Ϊ��������ݿ�������
                    //��ѯ���п����Ϣ
                    sqlc.CommandText = "select * from �û� where ��¼�˺�='" + textBox2.Text + "'";
                    sql.Open();//�����ݿ�
                    DataSet ds = new DataSet();
                    SqlDataAdapter sda = new SqlDataAdapter(sqlc);//�������dataset���ݼ��ĺ���
                    sda.Fill(ds, "t1");//������ݼ�
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        MessageBox.Show("�˺��ظ���");
                    }
                    else
                    {
                        SqlCommand sqlc1 = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
                        sqlc1.Connection = sql;
                        //��ѯ�����û���
                        sqlc1.CommandText = "select max(�û���) maxId from �û�";
                        SqlDataAdapter sda1 = new SqlDataAdapter(sqlc1);
                        sda1.Fill(ds, "t1");
                        int maxId = 0;
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["maxId"].ToString()) && ds.Tables[0].Rows[0]["maxId"].ToString() != "0")
                        {
                            maxId = int.Parse(ds.Tables[0].Rows[0]["maxId"].ToString());
                        }
                        SqlCommand sqlc2 = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
                        sqlc2.Connection = sql;
                        //�������
                        //sqlc1.CommandText = "insert into users values('" + textBox2.Text + "','" + textBox1.Text + "','" + textBox4.Text + "','��Ա','" + textBox5.Text + "','" + textBox3.Text + "','" + dateTimePicker1.Value + "','" + comboBox1.Text + "')";
                        sqlc2.CommandText = $"insert into �û� values({maxId + 1}, '{textBox1.Text}', '{textBox4.Text}', '{comboBox1.Text}', '{dateTimePicker1.Value}', '{textBox5.Text}', {maxId + 1}, '��Ա', '{textBox2.Text}', '{textBox3.Text}')";
                        int result = sqlc2.ExecuteNonQuery();//ִ����䷵��Ӱ�������
                        if (result > 0)//���ִ�гɹ��򷵻�1
                        {
                            MessageBox.Show("ע��ɹ���");
                            textBox1.Text = "";
                            textBox2.Text = "";
                            textBox3.Text = "";
                            textBox4.Text = "";
                            textBox5.Text = "";
                            Form_goods_Load(sender, e);
                        }
                        else
                        {
                            MessageBox.Show("���ʧ�ܣ�");
                        }
                        sql.Close();
                    }
                }
            }
            else
            {
                
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_DropDownClosed(object sender, EventArgs e)
        {

        }
    }
}