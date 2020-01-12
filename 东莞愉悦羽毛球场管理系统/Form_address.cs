using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace Sales
{
    public partial class Form_address : Form
    {
        private string _picturePrefix = "./Images/����/";
        public Form_address()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("�������Ʋ���Ϊ�գ�");
                }
                else
                {

                    SqlConnection sql = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
                    SqlCommand sqlc = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
                    sqlc.Connection = sql;//���ò�ѯ�������������Ϊ��������ݿ�������
                    //�������
                    string sSql = "";
                    if (button1.Text == "����")
                    {
                        sSql = "insert into ���� values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + pictureBox1.Tag + "')";
                    }
                    else
                    {
                        sSql = "update ���� set ����='" + textBox1.Text + "',���='"+textBox2.Text+"',ʱ��='"+textBox3.Text+ "',״̬='" + textBox4.Text + "',ͼƬ='" + pictureBox1.Tag + "' where ���غ�='" + dataGridView1.CurrentRow.Cells[1].Value.ToString() + "'";
                    }
                    sqlc.CommandText = sSql;
                    sql.Open();//�����ݿ�
                    int result = sqlc.ExecuteNonQuery();//ִ����䷵��Ӱ�������
                    if (result > 0)//���ִ�гɹ��򷵻�1
                    {
                        MessageBox.Show("�����ɹ�");
                        button1.Text = "����";
                        textBox1.Tag = "";
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        pictureBox1.Image = null;
                        pictureBox1.Tag = "";
                        button2.Visible = false;
                        Form_sort_Load(sender, e);
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

        private void Form_sort_Load(object sender, EventArgs e)
        {
            SqlConnection sql = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
            SqlCommand sqlc = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
            sqlc.Connection = sql;//���ò�ѯ�������������Ϊ��������ݿ�������
            //��ѯ������Ϣ
            sqlc.CommandText = "select ���غ�,����,���,ʱ��,״̬,ͼƬ from ����";
            sql.Open();//�����ݿ�
            DataSet ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter(sqlc);//�������dataset���ݼ��ĺ���
            sda.Fill(ds,"t1");//������ݼ�
            dataGridView1.DataSource = ds.Tables["t1"].DefaultView;
            dataGridView1.ClearSelection();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
              
                if (MessageBox.Show("Ҫ�޸ĵ�ǰ��¼��", "��ʾ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    textBox1.Tag = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    pictureBox1.Tag = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                    pictureBox1.Image = GetImage(_picturePrefix + dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString()); ;

                    button1.Text = "����";
                    button2.Visible = true;

                }
               
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (MessageBox.Show("ȷ��Ҫɾ����ǰ��Ϣ��", "��ʾ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SqlConnection sql = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
                    SqlCommand sqlc = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
                    sqlc.Connection = sql;//���ò�ѯ�������������Ϊ��������ݿ�������
                    //ɾ�����
                    sqlc.CommandText = $"delete from ���� where ���غ�='{textBox1.Tag}'";
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
                        pictureBox1.Image = null;
                        pictureBox1.Tag = "";

                        Form_sort_Load(sender, e);
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

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name.Equals("ͼƬ"))
            {
                string path = e.Value.ToString();
                e.Value = GetImage(_picturePrefix + path);
            }
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