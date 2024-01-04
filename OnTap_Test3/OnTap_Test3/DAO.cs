using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnTap_Test3.Model;

namespace OnTap_Test3
{
    class DAO
    {
        private static DAO instance;

        internal static DAO Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new DAO();
                }

                return instance;
            }
        }

        private DAO() { }

        public DataTable Xem()
        {
            string sql = "SELECT * FROM InforFilm";
            return DataProvider.Instance.execSql(sql);
        }

        public bool Save_2D(P2D p2D)
        {
            string sql = "INSERT INTO InforFilm(MaDon, TenPhim, QuocGia, TheLoai, NgayCongChieu, DoTuoiQuyDinh, PTGheDoi) " +
                 "VALUES (@MaDon, @TenPhim, @QuocGia, @TheLoai, @NgayCongChieu, @DoTuoiQuyDinh, @PTGheDoi )";
            object[] prms = new object[] { p2D.maDon, p2D.tenPhim, p2D.quocGia, p2D.theLoai, p2D.ngayCongChieu, p2D.doTuoiQuiDinh, p2D.phuThuGheDoi };
            return DataProvider.Instance.execNonSql(sql, prms) > 0;
        }

        public bool Save_3D(P3D p3D)
        {
            string sql = "INSERT INTO InforFilm(MaDon, TenPhim, QuocGia, TheLoai, NgayCongChieu, DoTuoiQuyDinh, PTDacBiet) " +
                 "VALUES (@MaDon, @TenPhim, @QuocGia, @TheLoai, @NgayCongChieu, @DoTuoiQuyDinh, @PTDacBiet )";
            object[] prms = new object[] { p3D.maDon, p3D.tenPhim, p3D.quocGia, p3D.theLoai, p3D.ngayCongChieu, p3D.doTuoiQuiDinh, p3D.phuThuXuatChieuDacBiet };
            return DataProvider.Instance.execNonSql(sql, prms) > 0;
        }

        public bool Xoa(string maDon)
        {
            string sql = "DELETE FROM InforFilm WHERE MaDon = @MaDon";
            object[] prms = new object[] { maDon };
            return DataProvider.Instance.execNonSql(sql, prms) > 0;
        }

        public bool Sua2D(P2D p2D, string maDon)
        {
            string sql = "UPDATE InforFilm SET TenPhim = @TenPhim, TheLoai = @TheLoai, NgayCongChieu = @NgayCongChieu, PTGheDoi = @PTGheDoi " +
                " WHERE MaDon = @MaDon";
            object[] prms = new object[] { p2D.tenPhim, p2D.theLoai, p2D.ngayCongChieu, p2D.phuThuGheDoi, maDon };
            return DataProvider.Instance.execNonSql(sql, prms) > 0;
        }

        public bool Sua3D(P3D p3D, string maDon)
        {
            string sql = "UPDATE InforFilm SET TenPhim = @TenPhim, TheLoai = @TheLoai, NgayCongChieu = @NgayCongChieu, PTDacBiet = @PTDacBiet " +
                " WHERE MaDon = @MaDon";
            object[] prms = new object[] { p3D.tenPhim, p3D.theLoai, p3D.ngayCongChieu, p3D.phuThuXuatChieuDacBiet, maDon };
            return DataProvider.Instance.execNonSql(sql, prms) > 0;
        }

        public DataTable SapXep()
        {
            string sql = $"SELECT * FROM InforFilm ORDER BY NgayCongChieu ASC, DoTuoiQuyDinh DESC";
            return DataProvider.Instance.execSql(sql);
        }

    }
}
