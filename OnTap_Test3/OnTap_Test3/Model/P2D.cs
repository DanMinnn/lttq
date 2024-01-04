using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTap_Test3.Model
{
    class P2D : ThongTinPhim
    {
        public double phuThuGheDoi { get; set; }

        public double GiaVe2D()
        {
            return base.GiaVe2D() + phuThuGheDoi;
        }
    }
}
