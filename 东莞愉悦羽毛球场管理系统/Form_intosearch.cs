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
    public partial class Form_intosearch : Form
    {
        DataSet ds;
        public Form_intosearch()
        {
            InitializeComponent();
        }

        private void Form_intosearch_Load(object sender, EventArgs e)
        {
            SqlConnection sql = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
            SqlCommand sqlc = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
            sqlc.Connection = sql;//���ò�ѯ�������������Ϊ��������ݿ�������
            //��ѯ���п����Ϣ
            sqlc.CommandText = "select b.�������� ����, a.���� ��Ʒ����,a.�۸�,a.�ϼ����� ������� from ��Ʒ a left join ���� b on a.���ͺ�=b.���ͺ�";
            sql.Open();//�����ݿ�
            ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter(sqlc);//�������dataset���ݼ��ĺ���
            sda.Fill(ds, "t1");//������ݼ�
            dataGridView1.DataSource = ds.Tables["t1"].DefaultView;
            dataGridView1.ClearSelection();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sSql = "select goodsname,sort,prices,dates from Goods where 1=1 ";
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                sSql += " and goodsname like '%" + textBox1.Text + "%'";
            }
            if (!string.IsNullOrEmpty(textBox3.Text))
            {
                sSql += " and sort ='" + textBox3.Text + "'";
            }
            SqlConnection sql = new SqlConnection(login.sqlstr);//ʵ��һ�����ݿ�������
            SqlCommand sqlc = new SqlCommand();//ʵ��һ�����ݿ��ѯ������
            sqlc.Connection = sql;//���ò�ѯ�������������Ϊ��������ݿ�������
            //��ѯ���п����Ϣ
            sqlc.CommandText = sSql;
            sql.Open();//�����ݿ�
            ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter(sqlc);//�������dataset���ݼ��ĺ���
            sda.Fill(ds, "t1");//������ݼ�
            dataGridView1.DataSource = ds.Tables["t1"].DefaultView;
            dataGridView1.ClearSelection();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            //if (e.RowIndex >= dataGridView1.Rows.Count - 1)
            //    return;
            DataGridViewRow dgr = dataGridView1.Rows[e.RowIndex];
            try
            {
                //dgr.Cells[0]�ǵ�ǰ�Ա��е�����ֵ������ȷ���ж���һ�е�ֵ
                if (double.Parse(dgr.Cells[3].Value.ToString())< 10)
                {
                    //���廭�ʣ�ʹ����ɫ����ҡ�
                    using (SolidBrush brush = new SolidBrush(Color.Red))
                    {
                        //���û�����䵱ǰ��
                        e.Graphics.FillRectangle(brush, e.RowBounds);
                        //��ֵ����д�ص�ǰ�С�
                        e.PaintCellsContent(e.ClipBounds);
                        e.Handled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        
    }
}