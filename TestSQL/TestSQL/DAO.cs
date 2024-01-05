using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSQL
{
    class DAO
    {
        private static DAO instance;
        internal static DAO Instance
        {
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
            string sql = "SELECT * FROM Product WHERE ProductName LIKE @keyword";
            object[] prms = new object[] { "%" + name + "%" };
            return DataProvider.Instance.execSql(sql, prms);
        }

        public bool Them(ProductModel p)
        {
            string sql = "INSERT INTO Product VALUES (@Name, @Price)";
            Object[] prms = new object[] {p.ProductName1, p.ProductPrice1 };
            return DataProvider.Instance.execNonSql(sql, prms) > 0;
        }   
    }
}
