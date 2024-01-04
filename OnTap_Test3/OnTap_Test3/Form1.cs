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
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;

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

        private void btnXuatBc_Click(object sender, EventArgs e)
        {
            bc dataset = new bc();
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-DANGMIN;Initial Catalog=QuanLyDoanhThu;Integrated Security=True");
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM InforFilm", connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dataset.SetDataSource(ds.Tables[0]);

            Form2 f2 = new Form2();
            f2.rpt.ReportSource = dataset;

            f2.ShowDialog();

        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook excelWb = excelApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
            Excel.Worksheet excelWS = excelWb.Worksheets[1];

            Excel.Range excelRange = excelWS.Cells[1, 1];
            excelRange.Font.Size = 16;
            excelRange.Font.Bold = true;
            excelRange.Font.Color = Color.Blue;
            excelRange.Value = "DANH MỤC PHIM";

            int row = 2;
            foreach(DataRow rows in DAO.Instance.Xem().Rows)
            {
                excelWS.Range["A" + row].Font.Bold = true;
                excelWS.Range["A" + row].Value = rows["TheLoai"].ToString();
                row++;

                foreach(DataRow ph in DAO.Instance.Xem().Rows)
                {
                    excelWS.Range["A" + row].Value = ph["MaDon"].ToString();
                    excelWS.Range["B" + row].ColumnWidth = 50;
                    excelWS.Range["B" + row].Value = ph["TenPhim"].ToString();
                    excelWS.Range["C" + row].Value = ph["QuocGia"].ToString();
                    excelWS.Range["D" + row].Value = ph["TheLoai"].ToString();
                    excelWS.Range["E" + row].Value = ph["NgayCongChieu"].ToString();
                    excelWS.Range["F" + row].Value = ph["DoTuoiQuyDinh"].ToString();
                    excelWS.Range["G" + row].Value = ph["PTGheDoi"].ToString();
                    excelWS.Range["H" + row].Value = ph["PTDacBiet"].ToString();
                    row++;
                }
            }
            excelWS.Name = "Danh sách phim";
            excelWb.Activate();

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                excelWb.SaveAs(saveFileDialog.FileName);
            excelApp.Quit();
            MessageBox.Show("Đã xuất file Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
