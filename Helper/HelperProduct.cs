using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFACari.Entity;
using WFACari.Model;

namespace WFACari.Helper
{
    public static class HelperProduct
    {
          
        public static bool CrudUrun(ProductModel pm, EntityState state)
        {   
            bool sonuc;
            var p = pm.ConvertToProduct();
            using (CariEntities db = new CariEntities())
            {
                db.Entry(p).State = state;
                if (db.SaveChanges() > 0)
                {
                    sonuc = true;
                }
                else
                {
                    sonuc = false;
                }

            }
            return sonuc;
        }
        public static bool UrunEkle(ProductModel pm)
        {
            bool sonuc;
            var p = pm.ConvertToProduct();
            using (CariEntities db=new CariEntities())
            {
                db.Product.Add(p);
                if (db.SaveChanges()>0)
                {
                    sonuc = true;
                }
                else
                {
                    sonuc = false;
                }

            }
            return sonuc;
        }
        public static List<ProductModel> UrunListeDon()
        {
            List<ProductModel> pml = new List<ProductModel>();
            using (CariEntities db = new CariEntities())
            {
                var f = db.Product.ToList();
                foreach (var item in f)
                {
                    ProductModel pm = new ProductModel();
                    pm.Aciklama = item.Aciklama;
                    pm.ID = item.ID;
                    pm.Kategori = item.Kategori;
                    pm.Alis = item.Alis;
                    pm.Satis = item.Satis;
                    pm.Stok = item.Stok;
                    pm.Adi = item.Adi;
                    pm.Category = item.Category.ConvertToCategoryModel();
                    pm.Category.ID = item.Category.ID;
                    pm.Category.Aciklama = item.Category.Aciklama;
                    pm.Category.Adi = item.Category.Adi;
                    pml.Add(pm);
                }
                return pml;
            }
        }
        public static List<ProductModel> UrunListeDonByID(int ID)
        {
            List<ProductModel> pml = new List<ProductModel>();
            using (CariEntities db = new CariEntities())
            {
                var f = db.Product.Where(x => x.Kategori == ID);
                foreach (var item in f)
                {
                    ProductModel pm = new ProductModel();
                    pm.Aciklama = item.Aciklama;
                    pm.ID = item.ID;
                    pm.Kategori = item.Kategori;
                    pm.Alis = item.Alis;
                    pm.Satis = item.Satis;
                    pm.Stok = item.Stok;
                    pm.Adi = item.Adi;
                    pm.Category = item.Category.ConvertToCategoryModel();
                    pm.Category.ID = item.Category.ID;
                    pm.Category.Aciklama = item.Category.Aciklama;
                    pm.Category.Adi = item.Category.Adi;
                    pml.Add(pm);
                }
                return pml;
            }
        }
        public static ProductModel GuncellenecekUrunAl(int ID, string adi, string c, string alis, string satis, string stok, string aciklama)
        {
            Category ctg;
            using (CariEntities db = new CariEntities())
            {
                ctg = db.Category.Where(s => s.Adi == c).First();
            }
            ProductModel pm = new ProductModel();
            pm.Aciklama = aciklama;
            pm.ID = ID;
            pm.Kategori = ctg.ID;
            pm.Alis = Convert.ToDecimal(alis);
            pm.Satis = Convert.ToDecimal(satis);
            pm.Stok = Convert.ToInt32(stok);
            pm.Adi = adi;
            pm.Category = ctg.ConvertToCategoryModel();
            pm.Category.ID = ctg.ID;
            pm.Category.Aciklama = ctg.Aciklama;
            pm.Category.Adi = ctg.Adi;
            return pm;
        }
        public static ProductModel EklenecekUrunAl(string adi, string categoryName, string alis, string satis, string stok, string aciklama)
        {
            ProductModel pm = new ProductModel();
         
            pm.Aciklama = aciklama;
           
            pm.Alis = Convert.ToDecimal(alis);
            pm.Satis = Convert.ToDecimal(satis);
            pm.Stok = Convert.ToInt32(stok);
            pm.Adi = adi;
            using (CariEntities db = new CariEntities())
            {
                var f = db.Category.Where(c => c.Adi == categoryName).First();
                pm.Category = f.ConvertToCategoryModel();
                pm.Kategori = f.ID;
          
            }
           
            return pm;
        }
        public static ProductModel SilinecekUrunAl(int ID)
        {
            ProductModel pm = new ProductModel();
            using (CariEntities db = new CariEntities())
            {
                var silinecek = db.Product.Where(pr => pr.ID == ID).First();
                pm.ID = silinecek.ID;
                pm.Kategori = silinecek.Kategori;
                pm.Satis = silinecek.Satis;
                pm.Stok = silinecek.Stok;
                pm.Aciklama = silinecek.Aciklama;
                pm.Adi = silinecek.Adi;
                pm.Alis = silinecek.Alis;
                pm.Category = silinecek.Category.ConvertToCategoryModel();
                pm.Category.Adi = silinecek.Category.Adi;
                pm.Category.Aciklama = silinecek.Category.Adi;
                pm.Category.ID = silinecek.Category.ID;
            }
            return pm;
        }

    }
}
