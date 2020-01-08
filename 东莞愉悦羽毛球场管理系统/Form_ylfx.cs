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
    public partial class Form_ylfx : Form
    {
        DataSet ds;
        public Form_ylfx()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bind();
        }

        private void bind()
        {
            chart1.Width = 600; //ͼƬ���
            chart1.Height = 400; //ͼƬ�߶�
            chart1.BackColor = Color.Honeydew; //ͼƬ����ɫ
            //����DataTable��
            string sSql = "select sum(cast(�۸� as float)) as je,left(convert(varchar,����ʱ��,120),10) as rq from ���۵� where left(convert(varchar,����ʱ��,120),10)>='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' and left(convert(varchar,����ʱ��,120),10)<='" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' group by left(convert(varchar,����ʱ��,120),10) order by rq asc";
            SQL s = new SQL();
            DataSet ds = s.DSSearch(sSql);
            chart1.DataSource = ds.Tables[0];

            //X��---dataTable����day�ֶΣ�ʵ��������Сʱ
            chart1.Series[0].XValueMember = "rq";
            //Y��
            chart1.Series[0].YValueMembers = "je";
            chart1.Series[0].IsValueShownAsLabel = true;
            chart1.Series[0].Label = "#VAL";
            chart1.DataBind();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
           
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            
        }

        private void Form_ylfx_Load(object sender, EventArgs e)
        {
            bind();
        }
    }
}