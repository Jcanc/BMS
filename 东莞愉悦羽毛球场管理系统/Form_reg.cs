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
                if (textBox2.Text == "" || textBox1.Text == "" || textBox3.Text == "")
                {
                    MessageBox.Show("��Ϣû����ȫ��");
                }
                else
                {
                    if (textBox2.Text.Length < 6)
                    {
                        MessageBox.Show("����������6λ��");
                        return;
                    }
                    if (textBox4.Text.Length > 11)
                    {
                        MessageBox.Show("�ֻ����벻�ܴ���11λ��");
                        textBox4.Focus();
                        textBox4.SelectAll();
                        return;
                    }
                    foreach (char cha in textBox4.Text)
                    {
                        if (char.IsNumber(cha))
                            continue;
                        else
                        {
                            MessageBox.Show("��������ȷ���ֻ����룡");
                            textBox4.Focus();
                            textBox4.SelectAll();
                            return;
                        }
                    }
                    SqlConnection sql = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
                    SqlCommand sqlc = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
                    sqlc.Connection = sql;//���ò�ѯ�������������Ϊ��������ݿ�������
                    //��ѯ���п����Ϣ
                    sqlc.CommandText = "select * from �û� where ��¼�˺�='" + textBox1.Text + "'";
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
                        //�������
                        sqlc1.CommandText = $"insert into �û� values('{textBox2.Text}', '{textBox3.Text}', '{comboBox1.Text}', '{dateTimePicker1.Value}', '{textBox4.Text}', '��Ա', '{textBox1.Text}', '{textBox5.Text}')";
                        int result = sqlc1.ExecuteNonQuery();//ִ����䷵��Ӱ�������
                        if (result > 0)//���ִ�гɹ��򷵻�1
                        {
                            MessageBox.Show("ע��ɹ���");
                            textBox2.Text = "";
                            textBox1.Text = "";
                            textBox5.Text = "";
                            textBox3.Text = "";
                            textBox4.Text = "";
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