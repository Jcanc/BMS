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
            SqlConnection sql = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
            SqlCommand sqlc = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
            sqlc.Connection = sql;//���ò�ѯ�������������Ϊ��������ݿ�������
            //��ѯ������Ϣ
            sqlc.CommandText = "select a.������,a.����,a.�Ա�,a.���� ��������,a.��ϵ��ʽ,a.ʱ��,a.��ע from ���� a";
            sql.Open();//�����ݿ�
            DataSet ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter(sqlc);//�������dataset���ݼ��ĺ���
            sda.Fill(ds, "t1");//������ݼ�
            dataGridView1.DataSource = ds.Tables["t1"].DefaultView;
            dataGridView1.ClearSelection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            #region ���������Ϣ����Ч��
            if (textBox1.Text == "")
            {
                MessageBox.Show("��������Ϊ�գ�");
                textBox1.Focus();
                textBox1.SelectAll();
                return;
            }
            if (textBox2.Text == "")
            {
                MessageBox.Show("�������ݲ���Ϊ�գ�");
                textBox2.Focus();
                textBox2.SelectAll();
                return;
            }
            if (textBox3.Text.Length > 11)
            {
                MessageBox.Show("�ֻ����벻�ܴ���11λ��");
                textBox3.Focus();
                textBox3.SelectAll();
                return;
            }
            foreach (char cha in textBox3.Text)
            {
                if (char.IsNumber(cha))
                    continue;
                else
                {
                    MessageBox.Show("��������ȷ���ֻ����룡");
                    textBox3.Focus();
                    textBox3.SelectAll();
                    return;
                }
            }
            if (textBox4.Text == "")
            {
                MessageBox.Show("ʱ�ⲻ��Ϊ�գ�");
                textBox4.Focus();
                textBox4.SelectAll();
                return;
            }
            double.TryParse(textBox4.Text, out double timesRent);
            if (timesRent <= 0)
            {
                MessageBox.Show("��������ȷ��ʱ�⣡");
                textBox4.Focus();
                textBox4.SelectAll();
                return;
            }
            #endregion

            #region �������Ƿ��Ѵ���
            SqlConnection sql = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
            SqlCommand sqlc = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
            sqlc.Connection = sql;//���ò�ѯ�������������Ϊ��������ݿ�������
                                  //��ѯ���п����Ϣ
            sqlc.CommandText = $"select * from ���� where ������ <> '{textBox1.Tag}' and ����='{textBox1.Text}'";
            sql.Open();//�����ݿ�
            DataSet ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter(sqlc);//�������dataset���ݼ��ĺ���
            sda.Fill(ds, "t1");//������ݼ�
            if (ds.Tables[0].Rows.Count > 0)
            {
                MessageBox.Show("�����ظ���");
            }
            #endregion

            if (button1.Text != "����")
            {
                SqlConnection sql1 = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
                SqlCommand sqlc1 = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
                sqlc1.Connection = sql1;//���ò�ѯ�������������Ϊ��������ݿ�������
                                        //�������
                sqlc1.CommandText = "insert into ���� values('" + textBox1.Text + "','" + comboBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox5.Text + "','" + textBox4.Text + "')";
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
            else
            {
                SqlConnection sql1 = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
                SqlCommand sqlc1 = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
                sqlc1.Connection = sql1;//���ò�ѯ�������������Ϊ��������ݿ�������
                //�������
                sqlc1.CommandText = "update ���� set ����='" + textBox1.Text + "',�Ա�='" + comboBox1.Text + "',����='" + textBox2.Text + "',��ϵ��ʽ='" + textBox3.Text + "',��ע='" + textBox5.Text + "',ʱ��='" + textBox4.Text + "' where ������='" + textBox1.Tag + "'";
                sql1.Open();//�����ݿ�
                int result = sqlc1.ExecuteNonQuery();//ִ����䷵��Ӱ�������
                if (result > 0)//���ִ�гɹ��򷵻�1
                {
                    MessageBox.Show("���³ɹ���");
                    button1.Text = "����";
                    textBox1.Tag = "";
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
                    textBox1.Tag = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    comboBox1.SelectedIndex = comboBox1.Items.IndexOf(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
                    textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                    textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                    //comboBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                    textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();

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
                        textBox1.Text = "";
                        textBox1.Tag = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
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

        //private void comboBox2_DropDown(object sender, EventArgs e)
        //{
        //    SqlConnection sql = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
        //    SqlCommand sqlc = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
        //    sqlc.Connection = sql;//���ò�ѯ�������������Ϊ��������ݿ�������
        //    //ɾ�����
        //    sqlc.CommandText = "select ����,���غ� from ����";
        //    sql.Open();//�����ݿ�
        //    SqlDataReader sdr = sqlc.ExecuteReader();
        //    while (sdr.Read())
        //    {
        //        comboBox2.Items.Add(sdr.GetValue(0));
        //        comboBox3.Items.Add(sdr.GetValue(1));
        //    }
        //    sql.Close();
        //}
    }
}