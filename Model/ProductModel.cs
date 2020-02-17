using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFACari.Model
{
    public class ProductModel
    {
        public int ID { get; set; }
        public string Adi { get; set; }
        public int Kategori { get; set; }
        public decimal Alis { get; set; }
        public decimal Satis { get; set; }
        public int Stok { get; set; }
        public string Aciklama { get; set; }
  
      public CategoryModel Category = new CategoryModel();

    }
}
