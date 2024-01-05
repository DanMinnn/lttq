using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestSQL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("Bạn có muốn thoát ứng dụng", "Xác nhận thoát!",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void btnXem_Click(object sender, EventArgs e)
        {
            Business.Instance.Xem(dataGridProduct);
        }

      

        private void btnTim_Click(object sender, EventArgs e)
        {
            Business.Instance.Tim(dataGridProduct, txtTim.Text);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (Business.Instance.Them(dataGridProduct))
            {
                MessageBox.Show("Thêm thành công !", "Thông báo!");
                Business.Instance.Xem(dataGridProduct);
            }
            else
                MessageBox.Show("Thêm không thành công!", "Thông báo!");
        }
    }
}
