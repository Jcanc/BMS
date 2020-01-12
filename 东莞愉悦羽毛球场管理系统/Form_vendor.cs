using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Sales
{
    public partial class Form_vendor : Form
    {
        private string queryFilter = string.Empty;
        public Form_vendor()
        {
            InitializeComponent();
        }

        private void Form_vendor_Load(object sender, EventArgs e)
        {
            SqlConnection sql = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
            SqlCommand sqlc = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
            sqlc.Connection = sql;//���ò�ѯ�������������Ϊ��������ݿ�������
            //��ѯ������Ϣ
            sqlc.CommandText = "select ��Ӧ�̱��,����,��ϵ��,����,��ϵ��ʽ from ��Ӧ��" + queryFilter;
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
            try
            {
                if (textBox3.Text == "" || textBox2.Text == "")
                {
                    MessageBox.Show("���Ʋ���Ϊ�ջ���ϵ�˲���Ϊ�գ�");
                }
                else
                {
                    if (textBox5.Text.Length > 11)
                    {
                        MessageBox.Show("�ֻ����벻�ܴ���11λ��");
                        return;
                    }

                    SqlConnection sql = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
                    SqlCommand sqlc = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
                    sqlc.Connection = sql;//���ò�ѯ�������������Ϊ��������ݿ�������
                    string sSql = string.Empty;

                    //�������
                    if (button1.Text == "����")
                    {
                        sSql = "insert into ��Ӧ�� values('" + textBox3.Text + "','" + textBox2.Text + "','" + textBox4.Text + "','" + textBox5.Text + "')";
                    }
                    else
                    {
                        sSql = $"update ��Ӧ�� set ����='" + textBox3.Text + "',��ϵ��='" + textBox2.Text + "',��ϵ��ʽ='" + textBox5.Text + "',����='" + textBox4.Text + "' where ��Ӧ�̱��='" + textBox3.Tag + "'";
                    }
                    sqlc.CommandText = sSql;
                    sql.Open();
                    int result = sqlc.ExecuteNonQuery();//ִ����䷵��Ӱ�������
                    if (result > 0)//���ִ�гɹ��򷵻�1
                    {
                        MessageBox.Show("�����ɹ�");
                        button1.Text = "����";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox3.Tag = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                        button2.Visible = false;
                        Form_vendor_Load(sender, e);
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
                    textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    textBox3.Tag = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
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

                    sqlc.CommandText = "delete from ��Ӧ�� where ��Ӧ�̱��='" + textBox3.Tag + "'";
                    sql.Open();//�����ݿ�
                    int result = sqlc.ExecuteNonQuery();//ִ����䷵��Ӱ�������
                    if (result > 0)//���ִ�гɹ��򷵻�1
                    {
                        MessageBox.Show("�����ɹ�");
                        button1.Text = "����";
                        button2.Visible = false;
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox3.Tag = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                        Form_vendor_Load(sender, e);
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

        private void buttonQuery_Click(object sender, EventArgs e)
        {
            queryFilter = $" where ��ϵ�� like '%{textBox1.Text}%' and ���� like '%{textBoxName.Text}%'";
            Form_vendor_Load(sender, e);
        }
    }
}