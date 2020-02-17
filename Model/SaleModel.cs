using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFACari.Model
{
    public class SaleModel
    {
        public int ID { get; set; }
        public int MusteriID { get; set; }
        public int UrunID { get; set; }
        public int UrunAdedi { get; set; }
        public decimal Tutari { get; set; }
        public DateTime KayitTarihi { get; set; }

        public  CustomerModel Customer { get; set; }
        public  ProductModel Product { get; set; }

    }
}
