using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Sales
{
    public partial class Form_orderscheck : Form
    {
        private string copyFiles = string.Empty;
        public Form_orderscheck()
        {
            InitializeComponent();
        }

        private void Form_goods_Load(object sender, EventArgs e)
        {
            bind();
        }

        private void bind()
        {
            SqlConnection sql = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
            SqlCommand sqlc = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
            sqlc.Connection = sql;//���ò�ѯ�������������Ϊ��������ݿ�������
            //��ѯ������Ϣ
            sqlc.CommandText = "select a.���ⵥ�� ������,c.���� ԤԼ����,a.�볡ʱ��,a.�볡ʱ��,a.��ע,b.���� ԤԼ��Ա,a.״̬ ���״̬,d.���� ������ from ���ⵥ a left join �û� b on a.�û���=b.�û��� left join ���� c on a.���غ�=c.���غ� left join �û� d on a.�����˺���=d.�û��� where a.״̬='δ���'";
            sql.Open();//�����ݿ�
            DataSet ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter(sqlc);//�������dataset���ݼ��ĺ���
            sda.Fill(ds, "t1");//������ݼ�
            dataGridView1.DataSource = ds.Tables["t1"].DefaultView;
            dataGridView1.ClearSelection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (MessageBox.Show("ȷ��Ҫ���ͨ����", "��ʾ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SqlConnection sql = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
                    SqlCommand sqlc = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
                    sqlc.Connection = sql;//���ò�ѯ�������������Ϊ��������ݿ�������
                    //sqlc.CommandText = "insert into GoodsOut values('" + comboBox1.Text + "','" + comboBox2.Text + "','" + dateTimePicker1.Value.ToShortDateString() + "'," + textBox5.Text + "," + textBox4.Text + "," + textBox1.Text + ",'" + textBox6.Text + "','" + textBox2.Text + "','" + comboBox3.Text + "','" + textBox7.Text + "','" + textBox8.Text + "')";
                    sql.Open();
                    string sqltext = "update ���ⵥ set ״̬='��ͨ��' where ���ⵥ��='" + dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() + "'";
                    sqlc.CommandText = sqltext;
                    sqlc.ExecuteNonQuery();//ִ����䷵��Ӱ�������
                    MessageBox.Show("�����ͨ��");
                    bind();
                }
            }
            if (e.ColumnIndex == 1)
            {
                if (MessageBox.Show("ȷ��Ҫ�ܾ���", "��ʾ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SqlConnection sql = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
                    SqlCommand sqlc = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
                    sqlc.Connection = sql;//���ò�ѯ�������������Ϊ��������ݿ�������
                    //sqlc.CommandText = "insert into GoodsOut values('" + comboBox1.Text + "','" + comboBox2.Text + "','" + dateTimePicker1.Value.ToShortDateString() + "'," + textBox5.Text + "," + textBox4.Text + "," + textBox1.Text + ",'" + textBox6.Text + "','" + textBox2.Text + "','" + comboBox3.Text + "','" + textBox7.Text + "','" + textBox8.Text + "')";
                    sql.Open();
                    string sqltext = "update ���ⵥ set ״̬='�Ѿܾ�' where ���ⵥ��='" + dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() + "'";
                    sqlc.CommandText = sqltext;
                    sqlc.ExecuteNonQuery();//ִ����䷵��Ӱ�������
                    MessageBox.Show("�Ѿܾ�");
                    bind();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            zl();
        }

        private void zl()
        {
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        //public System.Drawing.Image GetImage(string path)
        //{
           
        //}
    }
}