using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace OnTap_Test3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            rbtn_2d.Checked = true;
            rbtn_TinhCam.Checked = true;
            dtp_ncc.Value = DateTime.Now;

            Business.Instance.Xem(lvPhim);
        }

        private void rbtn_2d_CheckedChanged(object sender, EventArgs e)
        {
            lblPhuThuGheDoi.Visible = true;
            txtPTGheDoi.Visible = true;

            lblPTDB.Visible = false;
            txtPTDB.Visible = false;
        }

        private void rbtn_3d_CheckedChanged(object sender, EventArgs e)
        {
            lblPhuThuGheDoi.Visible = false;
            txtPTGheDoi.Visible = false;

            lblPTDB.Visible = true;
            txtPTDB.Visible = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaDon.Clear();
            txtMaDon.Focus();

            txtTenPhim.Clear();
            txtQuocGia.Clear();
            txtDoTuoiQuiDinh.Clear();
            txtPTDB.Clear();
            txtPTGheDoi.Clear();

            rbtn_2d.Checked = true;
            rbtn_TinhCam.Checked = true;
            dtp_ncc.Value = DateTime.Now;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn muốn thoát chương trình ?", "Xác nhận đóng chương trình", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            validate();
            Business.Instance.Luu();
        }

        private bool validate()
        {
            if(string.IsNullOrEmpty(txtMaDon.Text) || string.IsNullOrEmpty(txtTenPhim.Text)
                || string.IsNullOrEmpty(txtQuocGia.Text) || string.IsNullOrEmpty(txtDoTuoiQuiDinh.Text))
            {
                MessageBox.Show("Vui lòng nhập dữ liệu !");
            }


            try
            {
                int tmp = Convert.ToInt32(txtDoTuoiQuiDinh.Text);
                int pTGD = Convert.ToInt32(txtPTGheDoi.Text);
                int pTDB = Convert.ToInt32(txtPTDB.Text);
            }
            catch(Exception e) {
                MessageBox.Show("Vui lòng nhập kiểu số !");
            }
            return true;
        }

        private void lvPhim_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lvPhim.SelectedItems.Count > 0)
            {
                txtMaDon.Text = lvPhim.SelectedItems[0].Text;
                txtTenPhim.Text = lvPhim.SelectedItems[0].SubItems[1].Text;
                if (lvPhim.SelectedItems[0].SubItems[2].Text.Equals("Tình cảm"))
                    rbtn_TinhCam.Checked = true;
                else if (lvPhim.SelectedItems[0].SubItems[2].Equals("Hành động"))
                    rbtn_HanhDong.Checked = true;

                DateTime time = DateTime.ParseExact(lvPhim.SelectedItems[0].SubItems[3].Text, "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

                dtp_ncc.Value = time;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            Business.Instance.Xoa();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            Business.Instance.Sua();
        }

        private void btnSapXep_Click(object sender, EventArgs e)
        {
            lvPhim.Items.Clear();
            Business.Instance.SapXep(lvPhim);
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            int soLuongPhim;
            double tongDoanhThu2D, tongDoanhThu3D;

            Business.Instance.ThongKe(out soLuongPhim, out tongDoanhThu2D, out tongDoanhThu3D);

            string message = "Số lượng phim: " + soLuongPhim + "\n"
                   + "Tổng giá vé phim 2D: " + tongDoanhThu2D.ToString("N2") + " VNĐ\n"
                   + "Tổng giá vé phim 3D: " + tongDoanhThu3D.ToString("N2") + " VNĐ";

            MessageBox.Show(message, "Thống kê nhân viên", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
