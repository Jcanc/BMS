using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
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
            sqlc.CommandText = "select num,name,moneys,pay,orderdates,users from GoodsOrders";
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
                sqlc.CommandText = "select num,name,moneys,pay,orderdates,users from GoodsOrders where orderdates>='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and orderdates<='" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "'";
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