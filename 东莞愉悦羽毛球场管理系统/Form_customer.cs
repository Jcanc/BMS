using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Sales
{
    public partial class Form_customer : Form
    {
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
            string sSqlText = "select ��¼�˺�,����,����,�绰����,��ע,��������,�Ա�,��ɫ,�û��� from �û� where ��ɫ<>'����Ա'";
            if (login.qx != "����Ա")
            {
                sSqlText += " and �û���='" + login.yhh + "'";
                comboBox2.Enabled = false;
                button2.Visible = false;
            }
            if (!string.IsNullOrWhiteSpace(textBox1.Text) || !string.IsNullOrWhiteSpace(textBox2.Text))
            {
                sSqlText += $" and ���� like '%{textBox1.Text}%' and ��¼�˺� like '%{textBox2.Text}%'";
            }
            sqlc.CommandText = sSqlText;
            sql.Open();//�����ݿ�
            DataSet ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter(sqlc);//�������dataset���ݼ��ĺ���
            sda.Fill(ds, "t1");//������ݼ�
            dataGridView1.DataSource = ds.Tables["t1"].DefaultView;
            dataGridView1.ClearSelection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "" || textBox3.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("��Ϣû����ȫ��");
                return;
            }
            if (textBox4.Text.Length < 6)
            {
                MessageBox.Show("����������6λ��");
                textBox4.Focus();
                textBox4.SelectAll();
                return;
            }
            if (textBox6.Text.Length > 11)
            {
                MessageBox.Show("�ֻ����벻�ܴ���11λ��");
                textBox6.Focus();
                textBox6.SelectAll();
                return;
            }
            foreach (char cha in textBox6.Text)
            {
                if (char.IsNumber(cha))
                    continue;
                else
                {
                    MessageBox.Show("��������ȷ���ֻ����룡");
                    textBox6.Focus();
                    textBox6.SelectAll();
                    return;
                }
            }
            SqlConnection sql = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
            SqlCommand sqlc = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
            sqlc.Connection = sql;//���ò�ѯ�������������Ϊ��������ݿ�������
            sqlc.CommandText = $"select * from �û� where �û���<>'{textBox3.Tag}' and ��¼�˺�='" + textBox3.Text + "'";
            sql.Open();//�����ݿ�
            DataSet ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter(sqlc);//�������dataset���ݼ��ĺ���
            sda.Fill(ds, "t1");//������ݼ�
            if (ds.Tables[0].Rows.Count > 0)
            {
                MessageBox.Show("�˺��ظ���");
                textBox3.Focus();
                textBox3.SelectAll();
                return;
            }
            if (button1.Text != "����")
            {
                SqlCommand sqlc1 = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
                sqlc1.Connection = sql;

                SqlCommand sqlc2 = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
                sqlc2.Connection = sql;//���ò�ѯ�������������Ϊ��������ݿ�������
                                       //�������
                sqlc2.CommandText = $"insert into �û� values('{textBox4.Text}', '{textBox5.Text}', '{comboBox1.Text}', '{dateTimePicker1.Value}', '{textBox6.Text}', '��Ա', '{textBox3.Text}', '{textBox7.Text}')";
                int result = sqlc2.ExecuteNonQuery();//ִ����䷵��Ӱ�������
                if (result > 0)//���ִ�гɹ��򷵻�1
                {
                    MessageBox.Show("��ӳɹ���");
                    textBox4.Text = "";
                    textBox3.Text = "";
                    textBox3.Tag = "";
                    textBox7.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = "";
                    Form_goods_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("���ʧ�ܣ�");
                }
                sql.Close();
            }
            else
            {
                SqlConnection sql1 = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
                SqlCommand sqlc1 = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
                sqlc1.Connection = sql1;//���ò�ѯ�������������Ϊ��������ݿ�������
                //�������
                sqlc1.CommandText = "update �û� set ��¼�˺�='" + textBox3.Text + "',��ɫ='" + comboBox2.Text + "',����='" + textBox4.Text + "',����='" + textBox5.Text + "',�绰����='" + textBox6.Text + "',��ע='" + textBox7.Text + "',��������='" + dateTimePicker1.Value + "',�Ա�='" + comboBox1.Text + "' where �û���='" + textBox3.Tag + "'";
                sql1.Open();//�����ݿ�
                int result = sqlc1.ExecuteNonQuery();//ִ����䷵��Ӱ�������
                if (result > 0)//���ִ�гɹ��򷵻�1
                {
                    MessageBox.Show("���³ɹ���");
                    button1.Text = "����";
                    textBox4.Text = "";
                    textBox3.Text = "";
                    textBox3.Tag = "";
                    textBox7.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = "";
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
                    textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value?.ToString();
                    textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    textBox7.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    dateTimePicker1.Value = DateTime.Parse(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString());
                    comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                    comboBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                    textBox3.Tag = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
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
            if (textBox3.Text != "")
            {
                if (MessageBox.Show("ȷ��Ҫɾ����ǰ��Ϣ��", "��ʾ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SqlConnection sql = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
                    SqlCommand sqlc = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
                    sqlc.Connection = sql;//���ò�ѯ�������������Ϊ��������ݿ�������
                    //ɾ�����

                    sqlc.CommandText = "delete from �û� where �û���='" + textBox3.Tag + "'";
                    sql.Open();//�����ݿ�
                    int result = sqlc.ExecuteNonQuery();//ִ����䷵��Ӱ�������
                    if (result > 0)//���ִ�гɹ��򷵻�1
                    {

                        MessageBox.Show("�����ɹ�");
                        button1.Text = "����";
                        button2.Visible = false;
                        textBox3.Text = "";
                        textBox3.Tag = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                        textBox6.Text = "";
                        textBox7.Text = "";
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
            Form_goods_Load(sender, e);
        }
    }
}