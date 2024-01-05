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

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("Bạn có chắc muốn thoát ứng dụng?", "Xác nhận thoát!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            Business.Instance.Xem(dtvSp);
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            Business.Instance.Tim(dtvSp, txtKeyWord.Text);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (Business.Instance.Them(dtvSp))
            {
                MessageBox.Show("Thêm thành công !", "Thông báo !");
                Business.Instance.Xem(dtvSp);
            }
            else
                MessageBox.Show("Thêm không thành công !", "Thông báo !");
        }

        private void Xoa_Click(object sender, EventArgs e)
        {
            if (Business.Instance.Xoa(dtvSp))
            {
                MessageBox.Show("Xóa thành công !", "Thông báo !");
                Business.Instance.Xem(dtvSp);
            }
            else
                MessageBox.Show("Xóa không thành công !", "Thông báo !");
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (Business.Instance.Sua(dtvSp))
            {
                Business.Instance.Xem(dtvSp);
                MessageBox.Show("Sửa thành công !", "Thông báo !");
            }
            else
                MessageBox.Show("Sửa không thành công !", "Thông báo !");
        }
    }
}
