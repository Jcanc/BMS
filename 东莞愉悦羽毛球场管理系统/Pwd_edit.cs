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
    public partial class Pwd_edit : Form
    {
        public Pwd_edit()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("��Ϣ����Ϊ�գ�");
            }
            else
            {
                if (textBox1.Text != login.pwd)
                {
                    MessageBox.Show("ԭʼ���벻��ȷ��");
                }
                else
                {
                    if (textBox2.Text.Length < 6)
                    {
                        MessageBox.Show("����������6λ��");
                        return;
                    }
                    if (textBox2.Text.Equals(textBox3.Text))
                    {
                        SqlConnection sql = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
                        SqlCommand sqlc = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
                        sqlc.Connection = sql;//���ò�ѯ�������������Ϊ��������ݿ�������
                        //�������
                        sqlc.CommandText = "update �û� set ����='" + textBox3.Text + "' where ��¼�˺�='" + login.yh + "'";
                        sql.Open();//�����ݿ�
                        int result = sqlc.ExecuteNonQuery();//ִ����䷵��Ӱ�������
                        if (result > 0)//���ִ�гɹ��򷵻�1
                        {
                            MessageBox.Show("�޸ĳɹ���");
                            textBox1.Text = "";
                            textBox2.Text = "";
                            textBox3.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("�޸�ʧ�ܣ�");
                        }
                        sql.Close();

                    }
                    else
                    {
                        MessageBox.Show("������������벻һ�£�");
                    }
                }
            }

        }
    }
}