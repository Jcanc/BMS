using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Sales
{
    public partial class Form_customer : Form
    {
        private string queryFilter = string.Empty;

        public Form_customer()
        {
            InitializeComponent();
        }

        private void Form_goods_Load(object sender, EventArgs e)
        {
            SqlConnection sql = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
            SqlCommand sqlc = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
            sqlc.Connection = sql;//���ò�ѯ�������������Ϊ��������ݿ�������
            //��ѯ������Ϣ
            string sSqlText = "select ��¼�˺�,����,����,�绰����,��ע,��������,�Ա�,��ɫ,�û��� from �û� where ��ɫ<>'����Ա'" + queryFilter;
            if (login.qx != "����Ա")
            {
                sSqlText += " and ��¼�˺�='" + login.yh + "'";
                comboBox2.Enabled = false;
                button2.Visible = false;
            }
            sqlc.CommandText = sSqlText;
            sql.Open();//�����ݿ�
            DataSet ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter(sqlc);//�������dataset���ݼ��ĺ���
            sda.Fill(ds, "t1");//������ݼ�
            dataGridView1.DataSource = ds.Tables["t1"].DefaultView;
            dataGridView1.ClearSelection();
            queryFilter = string.Empty;
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
                    if (textBox5.Text.Length < 11)
                    {
                        MessageBox.Show("�ֻ����벻�ܴ���11λ��");
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
                        ////��ѯ�����û���
                        //sqlc1.CommandText = "select max(�û���) maxId from �û�";
                        //SqlDataAdapter sda1 = new SqlDataAdapter(sqlc1);
                        //sda1.Fill(ds, "t1");
                        //int maxId = 0;
                        //if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["maxId"].ToString()) && ds.Tables[0].Rows[0]["maxId"].ToString() != "0")
                        //{
                        //    maxId = int.Parse(ds.Tables[0].Rows[0]["maxId"].ToString());
                        //}

                        SqlCommand sqlc2 = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
                        sqlc2.Connection = sql;//���ò�ѯ�������������Ϊ��������ݿ�������
                        //�������
                        sqlc2.CommandText = $"insert into �û� values('{textBox1.Text}', '{textBox4.Text}', '{comboBox1.Text}', '{dateTimePicker1.Value}', '{textBox5.Text}', '��Ա', '{textBox2.Text}', '{textBox3.Text}')";
                        int result = sqlc2.ExecuteNonQuery();//ִ����䷵��Ӱ�������
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
                sqlc1.CommandText = "update �û� set ��¼�˺�='" + textBox2.Text + "',��ɫ='" + comboBox2.Text + "',����='" + textBox1.Text + "',����='" + textBox4.Text + "',�绰����='" + textBox5.Text + "',��ע='" + textBox3.Text + "',��������='" + dateTimePicker1.Value + "',�Ա�='" + comboBox1.Text + "' where �û���='" + textBox2.Tag + "'";
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
                    textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value?.ToString();
                    textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    dateTimePicker1.Value = DateTime.Parse(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString());
                    textBox2.Tag = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                    button1.Text = "����";
                    if (login.qx != "����Ա")
                    {
                        button2.Visible = false;
                    }
                    else
                    {
                        button2.Visible = true;
                    }

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

                    sqlc.CommandText = "delete from �û� where ��¼�˺�='" + textBox2.Text + "'";
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

        private void buttonQuery_Click(object sender, EventArgs e)
        {
            queryFilter = $" and ���� like '%{textBoxName.Text}%' and ��¼�˺� like '%{textBoxUser.Text}%'";
            Form_goods_Load(sender, e);
        }
    }
}