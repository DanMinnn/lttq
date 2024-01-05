using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3
{
    internal class DAO
    {
        private static DAO instance;

        internal static DAO Instance {
            get
            {
                if (instance == null)
                    instance = new DAO();
                return instance;
            }
        }
        private DAO() { }

        public DataTable Xem()
        {
            string sql = "SELECT * FROM Product";
            return DataProvider.Instance.execSql(sql);
        }

        public DataTable Tim(string name)
        {
            string sql = " SELECT * FROM Product WHERE ProductName LIKE @keyword ";
            Object[] prms = new object[] { "%" + name + "%"};
            return DataProvider.Instance.execSql(sql, prms);
        }

        public bool Them(ProductModel p)
        {
            string sql = "INSERT INTO Product VALUES( @Name, @Price )";
            Object[] prms = new object[] { p.ProductName, p.ProductPrice};
            return DataProvider.Instance.execNonSql(sql, prms) > 0;
        }

        public bool Xoa(string pCode)
        {
            string sql = "DELETE Product WHERE ProductCode = @Code";
            object[] prms = new object[] { pCode };
            return DataProvider.Instance.execNonSql(sql, prms) > 0;
        }

        public bool Sua(string code, ProductModel product)
        {
            string sql = "UPDATE Product SET ProductName = @Name, ProductPrice = @Price WHERE ProductCode = @Code";
            object[] prms = new object[] {product.ProductName,
            product.ProductPrice, code};
            return DataProvider.Instance.execNonSql(sql, prms) > 0;
        }
    }

}
