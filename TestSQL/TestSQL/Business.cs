using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestSQL
{
    class Business
    {
        private static Business instance;

        internal static Business Instance
        {
            get
            {
                if (instance == null)
                    instance = new Business();
                return instance;
            }
        }

        public Business() { }

        public void Xem(DataGridView dgv)
        {
            dgv.DataSource = DAO.Instance.Xem();
        }

        public void Tim(DataGridView dgv, string productName)
        {
            dgv.DataSource = DAO.Instance.Tim(productName);
        }

        public bool Them(DataGridView dgv)
        {
            DataGridViewRow rows = dgv.Rows[dgv.CurrentRow.Index];

            ProductModel p = new ProductModel();
            p.ProductName1 = rows.Cells["ProductName"].Value.ToString();
            p.ProductPrice1 = Convert.ToSingle(rows.Cells["ProductPrice"].Value.ToString());
            return DAO.Instance.Them(p);
        }
    }
}
