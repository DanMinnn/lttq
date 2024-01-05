using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3
{
    internal class ProductModel
    {
        private int productCode;
        private string productName;
        private float productPrice;

        public int ProductCode { get => productCode; set => productCode = value; }
        public string ProductName { get => productName; set => productName = value; }
        public float ProductPrice { get => productPrice; set => productPrice = value; }
    }
}
