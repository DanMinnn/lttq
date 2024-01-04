using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OnTap_Test3.Model;

namespace OnTap_Test3
{
    class Business
    {
        private static Business instance;

        internal static Business Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new Business();
                }

                return instance;
            }
        }

        public Business() { }
        public Image converByteToImg(String byteString)
        {
            byte[] imgBytes = Convert.FromBase64String(byteString);
            MemoryStream ms = new MemoryStream(imgBytes, 0, imgBytes.Length);
            ms.Write(imgBytes, 0, imgBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }

        public void Xem(ListView lv)
        {
            Form1 f1 = Application.OpenForms.OfType<Form1>().FirstOrDefault();
            
            string base64ImageData = "";

            foreach (DataRow row in DAO.Instance.Xem().Rows)
            {
                base64ImageData = row["HinhAnh"].ToString();

                ListViewItem item = new ListViewItem(row["MaDon"].ToString());
                item.SubItems.Add(row["TenPhim"].ToString());
                item.SubItems.Add(row["QuocGia"].ToString());
                item.SubItems.Add(row["NgayCongChieu"].ToString());

                lv.Items.Add(item);

            }

            if (!string.IsNullOrEmpty(base64ImageData))
            {
                f1.imgList.Images.Add(converByteToImg(base64ImageData));
                f1.imgList.ImageSize = new Size(24, 24);
                f1.lvHinhAnh.View = View.LargeIcon;

                for (int counter = 0; counter < f1.imgList.Images.Count; counter++)
                {
                    ListViewItem item = new ListViewItem();
                    item.ImageIndex = counter;
                    f1.lvHinhAnh.Items.Add(item);
                }
            }
            f1.lvHinhAnh.LargeImageList = f1.imgList;

        }

        public void Luu()
        {
            P2D p2D = new P2D();
            P3D p3D = new P3D();

            Form1 f1 = Application.OpenForms.OfType<Form1>().FirstOrDefault();

            if(f1 != null)
            {
                string theLoai = "";
                if (f1.rbtn_2d.Checked)
                {
                    
                    if (f1.rbtn_TinhCam.Checked)
                        theLoai = "Tình cảm";
                    else
                        theLoai = "Hành động";

                    p2D.maDon = f1.txtMaDon.Text;
                    p2D.tenPhim = f1.txtTenPhim.Text;
                    p2D.quocGia = f1.txtQuocGia.Text;
                    p2D.theLoai = theLoai;
                    p2D.ngayCongChieu = f1.dtp_ncc.Value;
                    p2D.phuThuGheDoi = double.Parse(f1.txtPTGheDoi.Text);
                    p2D.hinhAnh = f1.txtImg_to_Bytes.Text;

                    DAO.Instance.Save_2D(p2D);

                    string ncc = f1.dtp_ncc.Value.ToShortDateString();

                    ListViewItem li = new ListViewItem(f1.txtMaDon.Text);
                    li.SubItems.Add(f1.txtTenPhim.Text);
                    li.SubItems.Add(theLoai);
                    li.SubItems.Add(ncc);

                    f1.lvPhim.Items.Add(li);


                }
                else
                {
                    if (f1.rbtn_HanhDong.Checked)
                        theLoai = "Hành động";
                    else
                        theLoai = "Tình cảm";

                    p3D.maDon = f1.txtMaDon.Text;
                    p3D.tenPhim = f1.txtTenPhim.Text;
                    p3D.quocGia = f1.txtQuocGia.Text;
                    p3D.theLoai = theLoai;
                    p3D.ngayCongChieu = f1.dtp_ncc.Value;
                    p3D.phuThuXuatChieuDacBiet = double.Parse(f1.txtPTDB.Text);
                    p3D.hinhAnh = f1.txtImg_to_Bytes.Text;

                    DAO.Instance.Save_3D(p3D);

                    string ncc = f1.dtp_ncc.Value.ToShortDateString();
                    ListViewItem li = new ListViewItem(f1.txtMaDon.Text);
                    li.SubItems.Add(f1.txtTenPhim.Text);
                    li.SubItems.Add(theLoai);
                    li.SubItems.Add(ncc);

                    f1.lvPhim.Items.Add(li);
                }
             
            }
        }

        public void Xoa()
        {
            Form1 f_xoa = Application.OpenForms.OfType<Form1>().FirstOrDefault();

            int currentRow = f_xoa.lvPhim.SelectedIndices[0];
            if(f_xoa.lvPhim.SelectedItems.Count > 0)
            {
                string maDon = f_xoa.lvPhim.SelectedItems[0].Text;
                if (!string.IsNullOrEmpty(maDon))
                {
                    if (DAO.Instance.Xoa(maDon))
                    {
                        f_xoa.lvPhim.Items.Remove(f_xoa.lvPhim.SelectedItems[0]);
                        MessageBox.Show("Xóa thành công !");

                        if (currentRow < f_xoa.lvPhim.Items.Count)
                            f_xoa.lvPhim.Items[currentRow].Selected = true;
                    }
                    else
                        MessageBox.Show("Xóa thất bại !");
                }
            }
        }

        public void Sua()
        {
            Form1 f_sua = Application.OpenForms.OfType<Form1>().FirstOrDefault();
            P2D p2D = new P2D();
            P3D p3D = new P3D();

            if(f_sua.lvPhim.SelectedItems.Count > 0)
            {
                string maDon = f_sua.lvPhim.SelectedItems[0].Text;
                if (!string.IsNullOrEmpty(maDon))
                {
                    string theLoai = " ";
                    if (f_sua.rbtn_TinhCam.Checked)
                        theLoai = "Tình cảm";
                    else
                        theLoai = "Hành động";

                    if (f_sua.rbtn_2d.Checked)
                    {
                        p2D.tenPhim = f_sua.txtTenPhim.Text;
                        p2D.theLoai = theLoai;
                        p2D.ngayCongChieu = f_sua.dtp_ncc.Value;
                        p2D.phuThuGheDoi = double.Parse(f_sua.txtPTGheDoi.Text);

                        DAO.Instance.Sua2D(p2D, maDon);
                        MessageBox.Show("Sửa thành công !");
                    }
                    else
                    {
                        p3D.tenPhim = f_sua.txtTenPhim.Text;
                        p3D.theLoai = theLoai;
                        p3D.ngayCongChieu = f_sua.dtp_ncc.Value;
                        p3D.phuThuXuatChieuDacBiet = double.Parse(f_sua.txtPTDB.Text);

                        DAO.Instance.Sua3D(p3D, maDon);
                        MessageBox.Show("Sửa thành công !");
                    }
                }
            }
        }

        public void SapXep(ListView lv)
        {
            foreach(DataRow row in DAO.Instance.SapXep().Rows)
            {
                ListViewItem item = new ListViewItem(row["MaDon"].ToString());
                item.SubItems.Add(row["TenPhim"].ToString());
                item.SubItems.Add(row["TheLoai"].ToString());
                item.SubItems.Add(row["NgayCongChieu"].ToString());
                lv.Items.Add(item);
            }

            MessageBox.Show("Đã sắp xếp !");
        }

        public void ThongKe(out int soLuongPhim, out double tongDoanhThu2D, out double tongDoanhThu3D)
        {
            List<P2D> p2Ds = new List<P2D>();
            int soLuongPhim2D = 0;
            int soLuongPhim3D = 0;
            tongDoanhThu2D = 0;
            tongDoanhThu3D = 0;

            foreach (DataRow row in DAO.Instance.Xem().Rows)
            {
                P2D p2 = new P2D();
                if (!string.IsNullOrEmpty(row["PTGheDoi"].ToString()))
                {
                    p2.maDon = row["MaDon"].ToString();
                    p2.phuThuGheDoi = (double)row["PTGheDoi"];
                    tongDoanhThu2D += p2.GiaVe2D();
                    p2Ds.Add(p2);
                }
            }

            List<P3D> p3Ds = new List<P3D>();
            foreach (DataRow row in DAO.Instance.Xem().Rows)
            {
                P3D p3 = new P3D();
                if (!string.IsNullOrEmpty(row["PTDacBiet"].ToString()))
                {
                    p3.maDon = row["MaDon"].ToString();
                    p3.phuThuXuatChieuDacBiet = (double)row["PTDacBiet"];
                    tongDoanhThu3D += p3.GiaVe3D();
                    p3Ds.Add(p3);
                }
            }

            soLuongPhim2D = p2Ds.Count;
            soLuongPhim3D = p3Ds.Count;

            soLuongPhim = soLuongPhim2D + soLuongPhim3D;
        }
    }
}
