using System;
using System.Data;
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
            comboBox2_DropDown(sender, e);
            SqlConnection sql = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
            SqlCommand sqlc = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
            sqlc.Connection = sql;//���ò�ѯ�������������Ϊ��������ݿ�������
            //��ѯ������Ϣ
            sqlc.CommandText = "select a.������,a.���� ��������,a.��ԤԼ��ʼʱ��,a.��ԤԼ����ʱ��,a.����,a.�Ա�,a.��ϵ��ʽ,a.��ע,b.���� ����,a.ʱ�� from ���� a left join ���� b on a.���غ�=b.���غ�";
            sql.Open();//�����ݿ�
            DataSet ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter(sqlc);//�������dataset���ݼ��ĺ���
            sda.Fill(ds, "t1");//������ݼ�
            dataGridView1.DataSource = ds.Tables["t1"].DefaultView;
            dataGridView1.ClearSelection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Length > 11)
            {
                MessageBox.Show("�ֻ����벻�ܴ���11λ��");
                return;
            }
            if (dateTimePicker1.Value >= dateTimePicker2.Value)
            {
                MessageBox.Show("��ʼʱ�䲻�ܴ��ڽ���ʱ�䣡");
                return;
            }
            SqlConnection sql = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
            SqlCommand sqlc = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
            sqlc.Connection = sql;//���ò�ѯ�������������Ϊ��������ݿ�������
                                  //��ѯ���п����Ϣ
            sqlc.CommandText = $"select * from ���� where ������ <> '{textBox1.Tag}' ����='{textBox1.Text}'";
            sql.Open();//�����ݿ�
            DataSet ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter(sqlc);//�������dataset���ݼ��ĺ���
            sda.Fill(ds, "t1");//������ݼ�
            if (ds.Tables[0].Rows.Count > 0)
            {
                MessageBox.Show("�����ظ���");
            }
            if (button1.Text != "����")
            {
                if (comboBox2.Text == "" || textBox2.Text == "" || textBox1.Text == "")
                {
                    MessageBox.Show("��Ϣû����ȫ��");
                }
                else
                {
                    SqlConnection sql1 = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
                    SqlCommand sqlc1 = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
                    sqlc1.Connection = sql1;//���ò�ѯ�������������Ϊ��������ݿ�������
                                            //�������
                    sqlc1.CommandText = "insert into ���� values('" + textBox1.Text + "','" + comboBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + dateTimePicker1.Value + "','" + dateTimePicker2.Value + "','" + comboBox3.Items[comboBox2.SelectedIndex] + "','" + textBox5.Text + "','" + textBox4.Text + "')";
                    sql1.Open();//�����ݿ�
                    int result = sqlc1.ExecuteNonQuery();//ִ����䷵��Ӱ�������
                    if (result > 0)//���ִ�гɹ��򷵻�1
                    {
                        MessageBox.Show("��ӳɹ���");
                        textBox2.Text = "";
                        textBox5.Text = "";
                        textBox1.Text = "";
                        textBox3.Text = "";
                        Form_goods_Load(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("���ʧ�ܣ�");
                    }
                    sql.Close();
                }
            }
            else
            {
                SqlConnection sql1 = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
                SqlCommand sqlc1 = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
                sqlc1.Connection = sql1;//���ò�ѯ�������������Ϊ��������ݿ�������
                //�������
                sqlc1.CommandText = "update ���� set ����='" + textBox1.Text + "',�Ա�='" + comboBox1.Text + "',����='" + textBox2.Text + "',��ϵ��ʽ='" + textBox3.Text + "',��ԤԼ��ʼʱ��='" + dateTimePicker1.Value + "',��ԤԼ����ʱ��='" + dateTimePicker2.Value + "',���غ�='" + comboBox3.Items[comboBox2.SelectedIndex] + "',��ע='" + textBox5.Text + "',ʱ��='" + textBox4.Text + "' where ������='" + textBox1.Tag + "'";
                sql1.Open();//�����ݿ�
                int result = sqlc1.ExecuteNonQuery();//ִ����䷵��Ӱ�������
                if (result > 0)//���ִ�гɹ��򷵻�1
                {
                    MessageBox.Show("���³ɹ���");
                    button1.Text = "����";
                    textBox2.Text = "";
                    textBox5.Text = "";
                    textBox1.Text = "";
                    textBox1.Tag = "";
                    textBox3.Text = "";
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
                    textBox1.Tag = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    dateTimePicker1.Value = DateTime.Parse(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
                    dateTimePicker2.Value = DateTime.Parse(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
                    textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                    textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                    textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                    //comboBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                    comboBox2.SelectedIndex = comboBox2.Items.IndexOf(dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString());
                    textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();

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

                    sqlc.CommandText = "delete from ���� where ������='" + textBox1.Tag + "'";
                    sql.Open();//�����ݿ�
                    int result = sqlc.ExecuteNonQuery();//ִ����䷵��Ӱ�������
                    if (result > 0)//���ִ�гɹ��򷵻�1
                    {

                        MessageBox.Show("�����ɹ�");
                        button1.Text = "����";
                        button2.Visible = false;
                        textBox2.Text = "";
                        textBox5.Text = "";
                        textBox1.Text = "";
                        textBox1.Tag = "";
                        textBox3.Text = "";
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

        private void comboBox2_DropDown(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            SqlConnection sql = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
            SqlCommand sqlc = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
            sqlc.Connection = sql;//���ò�ѯ�������������Ϊ��������ݿ�������
            //ɾ�����
            sqlc.CommandText = "select ����,���غ� from ����";
            sql.Open();//�����ݿ�
            SqlDataReader sdr = sqlc.ExecuteReader();
            while (sdr.Read())
            {
                comboBox2.Items.Add(sdr.GetValue(0));
                comboBox3.Items.Add(sdr.GetValue(1));
            }
            sql.Close();
        }
    }
}