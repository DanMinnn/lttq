using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTap_Test3.Model
{
    class ThongTinPhim
    {
        public string maDon { get; set; }
        public string tenPhim { get; set; }
        public string quocGia { get; set; }
        public string theLoai { get; set; }
        public DateTime ngayCongChieu { get; set; }
        public int doTuoiQuiDinh { get; set; }
        public string hinhAnh { get; set; }

        public double GiaVe2D()
        {
            return 110000;
        }   
        
        public double GiaVe3D()
        {
            return 210000;
        }
    }
}
