using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    internal class Business
    {
        private static Business instance;

        internal static Business Instance {
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
            dgv.Columns[0].ReadOnly = true;
        }   
        
        public void Tim(DataGridView dgv, string productName)
        {
            dgv.DataSource = DAO.Instance.Tim(productName);
        }

        public bool Them(DataGridView dgv)
        {
            DataGridViewRow row = dgv.Rows[dgv.CurrentRow.Index];
            ProductModel p = new ProductModel();
            p.ProductName = row.Cells["ProductName"].Value.ToString();
            p.ProductPrice = Convert.ToSingle(row.Cells["ProductPrice"].Value.ToString());
            return DAO.Instance.Them(p);
        }

        public bool Xoa(DataGridView dgv)
        {
            string code = dgv.Rows[dgv.CurrentRow.Index].Cells[0].Value + "";

            return DAO.Instance.Xoa(code);
        }

        public bool Sua(DataGridView dgv)
        {
            DataGridViewRow row = dgv.Rows[dgv.CurrentRow.Index];
            string code = row.Cells["ProductCode"].Value.ToString();

            ProductModel productModel = new ProductModel()
            {
                ProductName = row.Cells["ProductName"].Value.ToString(),
                ProductPrice = float.Parse(row.Cells["ProductPrice"].Value.ToString())
            };

            return DAO.Instance.Sua(code, productModel);
        }
    }
}
