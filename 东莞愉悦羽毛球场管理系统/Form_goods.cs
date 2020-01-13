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
        public Form_goods()
        {
            InitializeComponent();
        }

        private void Form_goods_Load(object sender, EventArgs e)
        {
            comboBox1_DropDown(sender, e);
            textBox3.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
            SqlConnection sql = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
            SqlCommand sqlc = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
            sqlc.Connection = sql;//���ò�ѯ�������������Ϊ��������ݿ�������
            //��ѯ������Ϣ
            sqlc.CommandText = "select a.��Ʒ��,b.���ͺ�,b.��������,a.����,a.�ϼ�����,a.�۸�,a.�������,a.��λ,a.ͼƬ from ��Ʒ a left join ���� b on a.���ͺ� = b.���ͺ� where 1=1";
            if (!string.IsNullOrWhiteSpace(textBox1.Text) || !string.IsNullOrWhiteSpace(textBox2.Text))
            {
                sqlc.CommandText += $" and a.��Ʒ�� like '%{textBox1.Text}%' and a.���� like '%{textBox2.Text}%'";
            }
            sql.Open();//�����ݿ�
            DataSet ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter(sqlc);//�������dataset���ݼ��ĺ���
            sda.Fill(ds, "t1");//������ݼ�
            dataGridView1.DataSource = ds.Tables["t1"].DefaultView;
            dataGridView1.ClearSelection();
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
                if (comboBox1.Text == "" || textBox4.Text == "")
                {
                    MessageBox.Show("������������Ϊ�ջ���Ʒ���Ʋ���Ϊ�գ�");
                }
                else
                {
                    SqlConnection sql = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
                    SqlCommand sqlc = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
                    sqlc.Connection = sql;//���ò�ѯ�������������Ϊ��������ݿ�������
                    //�������
                    if (button2.Text == "����")
                    {
                        sqlc.CommandText = "insert into ��Ʒ(��Ʒ��,���ͺ�,����,�ϼ�����,�۸�,�������,��λ,ͼƬ) values('" + textBox3.Text + "','" + comboBox2.Items[comboBox1.SelectedIndex] + "','" + textBox4.Text + "','" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "','" + textBox5.Text + "','" + textBox7.Text + "','" + textBox6.Text + "','" + pictureBox1.Tag + "')";
                    }
                    else
                    {
                        sqlc.CommandText = $"update ��Ʒ set ���ͺ�='" + comboBox2.Items[comboBox1.SelectedIndex] + "',����='" + textBox4.Text + "',�ϼ�����='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "',�۸�='" + textBox5.Text + "',�������='" + textBox7.Text + "',��λ='" + textBox6.Text + "',ͼƬ='" + pictureBox1.Tag + "' where ��Ʒ��='" + textBox3.Text + "'";
                    }
                    sql.Open();
                    int result = sqlc.ExecuteNonQuery();//ִ����䷵��Ӱ�������
                    if (result > 0)//���ִ�гɹ��򷵻�1
                    {
                        MessageBox.Show("�����ɹ�");
                        button2.Text = "����";
                        button3.Visible = false;
                        textBox3.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
                        textBox4.Text = "";
                        textBox5.Text = "";
                        textBox6.Text = "";
                        textBox7.Text = "";
                        comboBox1.Text = "";
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
                    textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    comboBox1.SelectedIndex = comboBox1.Items.IndexOf(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
                    textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    dateTimePicker1.Value = DateTime.Parse(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString());
                    textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                    textBox7.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                    textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                    pictureBox1.Tag = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                    pictureBox1.Image = GetImage(_picturePrefix + dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString()); ;
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    button2.Text = "����";
                    button3.Visible = true;
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != "")
            {
                if (MessageBox.Show("ȷ��Ҫɾ����ǰ��Ϣ��", "��ʾ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SqlConnection sql = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
                    SqlCommand sqlc = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
                    sqlc.Connection = sql;//���ò�ѯ�������������Ϊ��������ݿ�������
                    //ɾ�����
                    sqlc.CommandText = "delete from ��Ʒ where ��Ʒ��='" + textBox3.Text + "'";
                    sql.Open();//�����ݿ�
                    int result = sqlc.ExecuteNonQuery();//ִ����䷵��Ӱ�������
                    if (result > 0)//���ִ�гɹ��򷵻�1
                    {
                        MessageBox.Show("�����ɹ�");
                        button2.Text = "����";
                        button3.Visible = false;
                        textBox3.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
                        textBox4.Text = "";
                        textBox5.Text = "";
                        textBox6.Text = "";
                        textBox7.Text = "";
                        comboBox1.Text = "";
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
            comboBox2.Items.Clear();
            SqlConnection sql = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
            SqlCommand sqlc = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
            sqlc.Connection = sql;//���ò�ѯ�������������Ϊ��������ݿ�������
            sqlc.CommandText = "select ��������,���ͺ� from ����";
            sql.Open();//�����ݿ�
            SqlDataReader sdr = sqlc.ExecuteReader();
            while (sdr.Read())
            {
                comboBox1.Items.Add(sdr.GetValue(0));
                comboBox2.Items.Add(sdr.GetValue(1));
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
                    pictureBox1.Image = GetImage(saveFilePath);
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    pictureBox1.Tag = fileName;
                    return;
                }
                using (Stream stream = openFileDialog.OpenFile())
                {
                    using (FileStream fs = new FileStream(saveFilePath, FileMode.CreateNew))
                    {
                        stream.CopyTo(fs);
                        fs.Flush();
                        pictureBox1.Image = Image.FromStream(fs);
                        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                        pictureBox1.Tag = fileName;
                    }

                }
            }
        }

        private void buttonQuery_Click(object sender, EventArgs e)
        {
            Form_goods_Load(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string filePath = _picturePrefix + pictureBox1.Tag;
            if (File.Exists(filePath))
                File.Delete(filePath);
            pictureBox1.Image = null;
            pictureBox1.Tag = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form_Photo f = new Form_Photo();
            Form_Photo.PHOTOPATH = _picturePrefix + pictureBox1.Tag.ToString();
            f.MdiParent = this.ParentForm;
            f.Show();
        }
    }
}