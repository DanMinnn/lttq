using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test_EntityModel
{
    public partial class Form1 : Form
    {
        QuanLyDoanhThuEntities db = new QuanLyDoanhThuEntities();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            rbtn_2d.Checked = true;
            rbtn_TinhCam.Checked = true;
            btnSua.Enabled = false;

            LoadListPhim();

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            InforFilm infor = new InforFilm();

            if (!string.IsNullOrEmpty(txtMaDon.Text)|| !string.IsNullOrEmpty(txtTenPhim.Text) || !string.IsNullOrEmpty(txtQuocGia.Text)
                ||  IsNumeric(txtDoTuoiQuiDinh.Text))
            {
                string theLoai = "";
                if (rbtn_TinhCam.Checked)
                    theLoai = "Tình cảm";
                else
                    theLoai = "Hành động";

                infor.MaDon = txtMaDon.Text;
                infor.TenPhim = txtTenPhim.Text;
                infor.QuocGia = txtQuocGia.Text;
                infor.NgayCongChieu = dtp_ncc.Value;
                infor.DoTuoiQuyDinh = int.Parse(txtDoTuoiQuiDinh.Text);
                infor.TheLoai = theLoai;

                if (rbtn_2d.Checked)
                    infor.PTGheDoi = double.Parse(txtPTGheDoi.Text);
                else
                    infor.PTDacBiet = double.Parse(txtPTDB.Text);

                db.InforFilms.Add(infor);
                db.SaveChanges();

                MessageBox.Show("Lưu thành công !");
            }
            else
            {
                MessageBox.Show("Lỗi dữ liệu !");
            }

            lvPhim.Items.Clear();
            LoadListPhim();
        }

        static bool IsNumeric(string input)
        {
            return double.TryParse(input, out _);
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

        private void LoadListPhim()
        {
            foreach(var p in db.InforFilms)
            {
                ListViewItem item = new ListViewItem(p.MaDon);
                item.SubItems.Add(p.TenPhim);
                item.SubItems.Add(p.TheLoai);
                item.SubItems.Add(p.NgayCongChieu?.ToString("dd/MM/yyyy"));

                lvPhim.Items.Add(item);
            }
        }

        private void lvPhim_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lvPhim.SelectedItems.Count > 0)
            {
                btnSua.Enabled = true;

                InforFilm infor_update = new InforFilm();
                string maDon = lvPhim.SelectedItems[0].Text;

                infor_update = db.InforFilms.Find(maDon);

                txtMaDon.Text = infor_update.MaDon;

                if (infor_update == null) return;

                txtTenPhim.Text = infor_update.TenPhim;

                if (infor_update.TheLoai.Equals("Tình cảm"))
                    rbtn_TinhCam.Checked = true;
                else if (infor_update.TheLoai.Equals("Hành động"))
                    rbtn_HanhDong.Checked = true;

                txtQuocGia.Text = infor_update.QuocGia;
                txtDoTuoiQuiDinh.Text = infor_update.DoTuoiQuyDinh.ToString();
                dtp_ncc.Value = infor_update.NgayCongChieu.Value;

                if (!infor_update.PTGheDoi.ToString().Equals(""))
                {
                    rbtn_2d.Checked = true;
                    txtPTGheDoi.Text = infor_update.PTGheDoi.ToString();
                }
                else
                {
                    txtPTDB.Text = infor_update.PTDacBiet.ToString();
                    rbtn_3d.Checked = true;
                }


            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMaDon.Text) || !string.IsNullOrEmpty(txtTenPhim.Text) || !string.IsNullOrEmpty(txtQuocGia.Text)
                || IsNumeric(txtDoTuoiQuiDinh.Text))
            {
                string theLoai = "";
                if (rbtn_TinhCam.Checked)
                    theLoai = "Tình cảm";
                else
                    theLoai = "Hành động";

                var infor_edit = db.InforFilms.Find(txtMaDon.Text);

                infor_edit.TenPhim = txtTenPhim.Text;
                infor_edit.QuocGia = txtQuocGia.Text;
                infor_edit.NgayCongChieu = dtp_ncc.Value;
                infor_edit.DoTuoiQuyDinh = int.Parse(txtDoTuoiQuiDinh.Text);
                infor_edit.TheLoai = theLoai;

                if (rbtn_2d.Checked)    
                    infor_edit.PTGheDoi = double.Parse(txtPTGheDoi.Text);
                else
                    infor_edit.PTDacBiet = double.Parse(txtPTDB.Text);

                /*db.InforFilms.Attach(infor_edit);
                db.Entry(infor_edit).State = EntityState.Modified;*/
                db.SaveChanges();

                MessageBox.Show("Sửa thành công !");
            }
            else
            {
                MessageBox.Show("Lỗi dữ liệu");
            }

            lvPhim.Items.Clear();
            LoadListPhim();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if(lvPhim.SelectedItems.Count > 0)
            {
                var del = db.InforFilms.Find(txtMaDon.Text);
                db.InforFilms.Remove(del);
                db.SaveChanges();
            }

            lvPhim.Items.Clear();
            LoadListPhim();
        }

        private void btnSapXep_Click(object sender, EventArgs e)
        {
            lvPhim.Items.Clear();

            var sort = db.InforFilms.OrderBy(p => p.NgayCongChieu).ThenBy(p => p.DoTuoiQuyDinh).ToList();

            foreach(var p in sort)
            {
                ListViewItem item = new ListViewItem(p.MaDon);
                item.SubItems.Add(p.DoTuoiQuyDinh?.ToString());
                item.SubItems.Add(p.TheLoai);
                item.SubItems.Add(p.NgayCongChieu?.ToString("dd/MM/yyyy"));

                lvPhim.Items.Add(item);
            }
        }
    }
}
