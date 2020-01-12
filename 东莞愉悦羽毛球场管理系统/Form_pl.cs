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
    public partial class Form_pl : Form
    {
        public Form_pl()
        {
            InitializeComponent();
        }

        private void Form_goods_Load(object sender, EventArgs e)
        {
            SqlConnection sql = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
            SqlCommand sqlc = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
            sqlc.Connection = sql;//���ò�ѯ�������������Ϊ��������ݿ�������
            //��ѯ������Ϣ
            //sqlc.CommandText = "select contents,times,name,phone,notes,dates,sex,address from Teacher";
            sqlc.CommandText = "select a.���� ��������,a.��ԤԼ��ʼʱ��,a.��ԤԼ����ʱ��,a.����,a.�Ա�,a.��ϵ��ʽ,a.��ע,b.���� ���� from ���� a left join ���� b on a.���غ�=b.���غ�";
            sql.Open();//�����ݿ�
            DataSet ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter(sqlc);//�������dataset���ݼ��ĺ���
            sda.Fill(ds, "t1");//������ݼ�
            dataGridView1.DataSource = ds.Tables["t1"].DefaultView;
            dataGridView1.ClearSelection();
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
                   
                    SqlConnection sql = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
                    SqlCommand sqlc = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
                    sqlc.Connection = sql;//���ò�ѯ�������������Ϊ��������ݿ�������
                    //��ѯ���п����Ϣ
                    sqlc.CommandText = "select * from Teacher where name='" + textBox4.Text + "'";
                    sql.Open();//�����ݿ�
                    DataSet ds = new DataSet();
                    SqlDataAdapter sda = new SqlDataAdapter(sqlc);//�������dataset���ݼ��ĺ���
                    sda.Fill(ds, "t1");//������ݼ�
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        MessageBox.Show("�����ظ���");
                    }
                    else
                    {
                        SqlConnection sql1 = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
                        SqlCommand sqlc1 = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
                        sqlc1.Connection = sql1;//���ò�ѯ�������������Ϊ��������ݿ�������
                        //�������
                        sqlc1.CommandText = "insert into Teacher values('" + textBox2.Text + "','" + textBox1.Text + "','" + textBox4.Text + "','" + comboBox2.Text + "','" + textBox5.Text + "','" + textBox3.Text + "','" + dateTimePicker1.Value + "','" + comboBox1.Text + "')";
                        sql1.Open();//�����ݿ�
                        int result = sqlc1.ExecuteNonQuery();//ִ����䷵��Ӱ�������
                        if (result > 0)//���ִ�гɹ��򷵻�1
                        {
                            MessageBox.Show("��ӳɹ���");
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
                SqlConnection sql1 = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
                SqlCommand sqlc1 = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
                sqlc1.Connection = sql1;//���ò�ѯ�������������Ϊ��������ݿ�������
                //�������
                sqlc1.CommandText = "update Teacher set contents='"+textBox2.Text+"',address='" + comboBox2.Text + "',times='" + textBox1.Text + "',name='" + textBox4.Text + "',phone='" + textBox5.Text + "',notes='" + textBox3.Text + "',dates='" + dateTimePicker1.Value + "',sex='" + comboBox1.Text + "' where name='" + textBox4.Text + "'";
                sql1.Open();//�����ݿ�
                int result = sqlc1.ExecuteNonQuery();//ִ����䷵��Ӱ�������
                if (result > 0)//���ִ�гɹ��򷵻�1
                {
                    MessageBox.Show("���³ɹ���");
                    button1.Text = "����";
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
                sql1.Close();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {

                if (MessageBox.Show("Ҫ�޸ĵ�ǰ��¼��", "��ʾ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                    comboBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                    textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    dateTimePicker1.Value = DateTime.Parse(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString());
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

                    sqlc.CommandText = "delete from Teacher where name='" + textBox4.Text + "'";
                    sql.Open();//�����ݿ�
                    int result = sqlc.ExecuteNonQuery();//ִ����䷵��Ӱ�������
                    if (result > 0)//���ִ�гɹ��򷵻�1
                    {

                        MessageBox.Show("�����ɹ�");
                        button1.Text = "����";
                        button2.Visible = false;
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                        textBox1.Text = "";
                        comboBox1.Text = "";
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
            
        }

        private void comboBox1_DropDownClosed(object sender, EventArgs e)
        {

        }

        private void comboBox2_DropDown(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            SqlConnection sql = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
            SqlCommand sqlc = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
            sqlc.Connection = sql;//���ò�ѯ�������������Ϊ��������ݿ�������
            //ɾ�����
            sqlc.CommandText = "select name from Address";
            sql.Open();//�����ݿ�
            SqlDataReader sdr = sqlc.ExecuteReader();
            while (sdr.Read())
            {
                comboBox2.Items.Add(sdr.GetValue(0));
            }
            sql.Close();
        }
    }
}