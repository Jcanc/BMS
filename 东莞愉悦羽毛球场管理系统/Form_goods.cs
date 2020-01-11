using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
namespace Sales
{
    public partial class Form_goods : Form
    {
        private string _picturePrefix = "./Images/��Ʒ/";
        private string queryFilter = string.Empty;
        public Form_goods()
        {
            InitializeComponent();
        }

        private void Form_goods_Load(object sender, EventArgs e)
        {
            comboBox1_DropDown(sender, e);
            textBox1.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
            SqlConnection sql = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
            SqlCommand sqlc = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
            sqlc.Connection = sql;//���ò�ѯ�������������Ϊ��������ݿ�������
            //��ѯ������Ϣ
            sqlc.CommandText = "select a.��Ʒ��,b.���ͺ�,b.��������,a.����,a.�ϼ�����,a.�۸�,a.�������,a.��λ,a.ͼƬ,a.״̬ from ��Ʒ a left join ���� b on a.���ͺ� = b.���ͺ�" + queryFilter;
            sql.Open();//�����ݿ�
            DataSet ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter(sqlc);//�������dataset���ݼ��ĺ���
            sda.Fill(ds, "t1");//������ݼ�
            dataGridView1.DataSource = ds.Tables["t1"].DefaultView;
            dataGridView1.ClearSelection();
            queryFilter = string.Empty;
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name.Equals("ͼƬ"))
            {
                string path = e.Value.ToString();
                e.Value = GetImage(_picturePrefix + path);
            }
        }

        public Image GetImage(string path)
        {
            Image result = null;
            try
            {
                FileStream fs = new FileStream(path, FileMode.Open);
                result = Image.FromStream(fs);
                fs.Close();
            }
            catch
            {

            }
            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text == "" || textBox2.Text == "")
                {
                    MessageBox.Show("������������Ϊ�ջ���Ʒ���Ʋ���Ϊ�գ�");
                }
                else
                {
                    SqlConnection sql = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
                    SqlCommand sqlc = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
                    sqlc.Connection = sql;//���ò�ѯ�������������Ϊ��������ݿ�������

                    //��ѯ���ͺ�
                    string sSql = "";
                    int typeNo = 0;
                    sSql = $"select ���ͺ� from ���� where ��������='{comboBox1.Text}'";
                    sqlc.CommandText = sSql;
                    sql.Open();//�����ݿ�
                    DataSet ds = new DataSet();
                    SqlDataAdapter sda = new SqlDataAdapter(sqlc);
                    sda.Fill(ds, "t1");
                    typeNo = int.Parse(ds.Tables[0].Rows[0]["���ͺ�"].ToString());

                    //�������
                    if (button1.Text == "����")
                    {
                        sSql = "insert into ��Ʒ(��Ʒ��,���ͺ�,����,�ϼ�����,�۸�,�������,��λ,ͼƬ,״̬) values('" + textBox1.Text + "','" + typeNo + "','" + textBox2.Text + "','" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + pictureBox1.Tag + "','" + textBox6.Text + "')";
                    }
                    else
                    {
                        sSql = $"update ��Ʒ set ���ͺ�='" + typeNo + "',����='" + textBox2.Text + "',�ϼ�����='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "',�۸�='" + textBox3.Text + "',�������='" + textBox4.Text + "',��λ='" + textBox5.Text + "',״̬='" + textBox6.Text + "',ͼƬ='" + pictureBox1.Tag + "' where ��Ʒ��='" + textBox1.Text + "'";
                    }
                    sqlc.CommandText = sSql;
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
                        textBox6.Text = "";
                        textBox5.Text = "";
                        pictureBox1.Image = null;
                        pictureBox1.Tag = "";
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
                    textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    comboBox1.SelectedIndex = comboBox1.Items.IndexOf(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
                    textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                    textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                    dateTimePicker1.Value = DateTime.Parse(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString());
                    textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                    textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
                    pictureBox1.Tag = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                    pictureBox1.Image = GetImage(_picturePrefix + dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString()); ;
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
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

                    sqlc.CommandText = "delete from ��Ʒ where ��Ʒ��='" + textBox1.Text + "'";
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
                        textBox4.Text = "";
                        textBox6.Text = "";
                        textBox5.Text = "";
                        pictureBox1.Image = null;
                        pictureBox1.Tag = "";
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
            sqlc.CommandText = "select ��������,���ͺ� from ����";
            sql.Open();//�����ݿ�
            SqlDataReader sdr = sqlc.ExecuteReader();
            while (sdr.Read())
            {
                comboBox1.Items.Add(sdr.GetValue(0));
            }
            sql.Close();
        }

        private void buttonUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "��ѡ���ϴ���ͼƬ";
            openFileDialog.Filter = "(*.bmp, *.jpg, *jpeg) | *.bmp; *.jpg; *jpeg;";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                int position = filePath.LastIndexOf("\\");
                string fileName = filePath.Substring(position + 1);

                if (!Directory.Exists(_picturePrefix))
                    Directory.CreateDirectory(_picturePrefix);
                string saveFilePath = _picturePrefix + fileName;
                if (File.Exists(saveFilePath))
                {
                    MessageBox.Show("ͼƬ�Ѵ���");
                    return;
                }
                using (Stream stream = openFileDialog.OpenFile())
                {
                    using (FileStream fs = new FileStream(saveFilePath, FileMode.CreateNew))
                    {
                        stream.CopyTo(fs);
                        fs.Flush();
                        pictureBox1.Image = Image.FromStream(fs);
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                        pictureBox1.Tag = fileName;
                    }

                }
            }
        }

        private void buttonQuery_Click(object sender, EventArgs e)
        {
            queryFilter = $" where a.��Ʒ�� like '%{textBoxGoodsNo.Text}%' and a.���� like '%{textBoxName.Text}%'";
            Form_goods_Load(sender, e);
        }
    }
}