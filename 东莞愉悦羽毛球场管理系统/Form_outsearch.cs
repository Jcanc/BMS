using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Sales
{
    public partial class Form_outsearch : Form
    {
        DataSet ds;
        public Form_outsearch()
        {
            InitializeComponent();
        }

        private void Form_outsearch_Load(object sender, EventArgs e)
        {
            SqlConnection sql = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
            SqlCommand sqlc = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
            sqlc.Connection = sql;//���ò�ѯ�������������Ϊ��������ݿ�������
            //��ѯ���п����Ϣ
            sqlc.CommandText = "select a.���۵��� ��ˮ,b.���� ��Ա,a.�տ�� ���ѽ��,a.���ʽ,a.����ʱ�� ��������,c.���� ������ from ���۵� a left join �û� b on a.�û���=b.�û��� left join �û� c on a.�����˺���=c.�û���";
            sql.Open();//�����ݿ�
            ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter(sqlc);//�������dataset���ݼ��ĺ���
            sda.Fill(ds, "t1");//������ݼ�
            dataGridView1.DataSource = ds.Tables["t1"].DefaultView;
            dataGridView1.ClearSelection();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
                SqlConnection sql = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
                SqlCommand sqlc = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
                sqlc.Connection = sql;//���ò�ѯ�������������Ϊ��������ݿ�������
                //��ѯ���п����Ϣ
                sqlc.CommandText = "select a.���۵��� ��ˮ,b.���� ��Ա,a.�տ�� ���ѽ��,a.���ʽ,a.����ʱ�� ��������,c.���� ������ from ���۵� a left join �û� b on a.�û��� = b.�û��� left join �û� c on a.�����˺��� = c.�û��� where a.����ʱ�� >= '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and a.����ʱ�� <= '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "'";
                sql.Open();//�����ݿ�
                ds = new DataSet();
                SqlDataAdapter sda = new SqlDataAdapter(sqlc);//�������dataset���ݼ��ĺ���
                sda.Fill(ds, "t1");//������ݼ�
                dataGridView1.DataSource = ds.Tables["t1"].DefaultView;
                dataGridView1.ClearSelection();
           
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }
    }
}