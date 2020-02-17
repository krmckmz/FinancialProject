using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFACari.Entity;
using WFACari.Model;

namespace WFACari.Helper
{
   public static class HelperSale
    {
        public static List<SaleModel> SaleModelListeDon()
        {
            List<SaleModel> sml = new List<SaleModel>();
            using (CariEntities db=new CariEntities())
            {
                var f = db.Sale.ToList();
                foreach (var item in f)
                {
                    SaleModel sm = new SaleModel();
                    sm=item.ConvertToSaleModel();
                    sml.Add(sm);
                }
                return sml;
            }
        }
        public static SaleModel SatisAl(int kategoriID,int urunID,int adet,int MusteriID)
        {
            SaleModel sm = new SaleModel();
            using (CariEntities db=new CariEntities())
            {
                sm.KayitTarihi = DateTime.Now;
                sm.MusteriID = MusteriID;
                sm.UrunAdedi = adet;
                sm.UrunID = urunID;
                var p = (db.Product.Where(x=>x.ID==urunID).First().ConvertToProductModel());
                sm.Product = p;
                sm.Product.ID = p.ID;
                sm.Product.Kategori = p.Kategori;
                sm.Product.Satis = p.Satis;
                sm.Product.Stok = p.Stok;
                sm.Product.Aciklama = p.Aciklama;
                sm.Product.Adi = p.Adi;
                sm.Product.Alis = p.Alis;
                sm.Tutari = sm.Product.Satis * sm.UrunAdedi;
                var c = db.Customer.Find(MusteriID).ConvertToCustomerModel();
                sm.Customer = c;
                sm.Customer.Adi = c.Adi;
                sm.Customer.Adres = c.Adres;
                sm.Customer.ID = c.ID;
                sm.Customer.Soyadi = c.Soyadi;
                sm.Customer.Telefon = c.Telefon;

            }
            return sm;
        }
        public static bool SatisEkle(Sale s)
        {
            using (CariEntities db=new CariEntities())
            {
                try
                {
                    db.Sale.Add(s);
                    if (db.SaveChanges() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (DbEntityValidationException eve)
                {
                    foreach (var item in eve.EntityValidationErrors)
                    {
                        var x = item;
                       
                    }
                    throw;
                }
               
            }
        }
        public static List<SaleModel> MusteriyeGoreListele(string musteriAdi,DateTime bas,DateTime son)
        {
            List<SaleModel> sml = new List<SaleModel>();
            using (CariEntities db=new CariEntities())
            {
                var ml = db.Sale.Where(x => x.Customer.Adi.Contains(musteriAdi)).ToList();
                
                SaleModel sm = new SaleModel();
                foreach (var item in ml)
                {
                    if (item.KayitTarihi>bas&&item.KayitTarihi<son)
                    {
                        sm = item.ConvertToSaleModel();
                        sml.Add(sm);
                    }
                    else
                    {
                        continue;
                    }
                
                }
                return sml;
            }
        }
        public static List<SaleModel> KategoriyeGoreListele(string kategoriAdi,DateTime bas,DateTime son)
        {
            List<SaleModel> sml = new List<SaleModel>();
            using (CariEntities db=new CariEntities())
            {
                var kl = db.Sale.Where(y => y.Product.Category.Adi.Contains(kategoriAdi)).ToList();
                SaleModel sm = new SaleModel();
                foreach (var item in kl)
                {
                    if (item.KayitTarihi > bas && item.KayitTarihi < son)
                    {
                        sm = item.ConvertToSaleModel();
                        sml.Add(sm);
                    }
                    else
                    {
                        continue;
                    }
                   
                }
                return sml;
            }
        }
        public static List<SaleModel>UruneGoreListele(string urunAdi,DateTime bas,DateTime son)
        {
            List<SaleModel> sml = new List<SaleModel>();
            using (CariEntities db=new CariEntities())
            {
                var ul = db.Sale.Where(z => z.Product.Adi.Contains(urunAdi)).ToList();
                SaleModel sm = new SaleModel();
                foreach (var item in ul)
                {
                    if (item.KayitTarihi > bas && item.KayitTarihi < son)
                    {
                        sm = item.ConvertToSaleModel();
                        sml.Add(sm);
                    }
               
                }
                return sml;
            }
        }
    }
}
