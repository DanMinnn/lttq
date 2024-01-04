using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTap_Test3.Model
{
    class P3D : ThongTinPhim
    {
        public double phuThuXuatChieuDacBiet { get; set; }

        public double GiaVe3D()
        {
            return base.GiaVe3D() + phuThuXuatChieuDacBiet;
        }
    }
}
