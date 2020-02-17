using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFACari.Entity;
using WFACari.Model;

namespace WFACari.Helper
{
    public static class Ext
    {
        public static Sale ConvertToSale(this SaleModel sm)
        {
            Sale s = new Sale();
            s.ID = sm.ID;
            s.MusteriID = sm.MusteriID;
            s.UrunID = sm.UrunID;
            s.KayitTarihi = sm.KayitTarihi;
            s.UrunAdedi = sm.UrunAdedi;
            s.Tutari = sm.Tutari;
            //s.Customer = sm.Customer.ConvertToCustomer();
            //s.Customer.ID = sm.Customer.ID;
            //s.Customer.Adi = sm.Customer.Adi;
            //s.Customer.Soyadi = sm.Customer.Soyadi;
            //s.Customer.Adres = sm.Customer.Adres;
            //s.Customer.Telefon = sm.Customer.Telefon;
            //s.Product = sm.Product.ConvertToProduct();
            //s.Product.ID = sm.Product.ID;
            //s.Product.Adi = sm.Product.Adi;
            ////s.Product.Aciklama = sm.Product.Aciklama;
            //s.Product.Category = sm.Product.Category.ConvertToCategory();
            //s.Product.Category.ID = sm.Product.Category.ID;
            //s.Product.Category.Adi = sm.Product.Category.Adi;
            //s.Product.Category.Aciklama = sm.Product.Category.Aciklama;
            //s.Product.Alis = sm.Product.Alis;
            //s.Product.Satis = sm.Product.Satis;
            //s.Product.Stok = sm.Product.Stok;

            return s;

        }
        public static CustomerModel ConvertToCustomerModel(this Customer c)
        {
            CustomerModel cm = new CustomerModel();
            cm.ID = c.ID;
            cm.Adi = c.Adi;
            cm.Soyadi = c.Soyadi;
            cm.Adres = c.Adres;
            cm.Telefon = c.Telefon;
            return cm;

        }
        public static SaleModel ConvertToSaleModel(this Sale s)
        {
            SaleModel sm = new SaleModel();
            sm.ID = s.ID;
            sm.MusteriID = s.MusteriID;
            sm.UrunID = s.UrunID;
            sm.KayitTarihi = s.KayitTarihi;
            sm.UrunAdedi = s.UrunAdedi;
            sm.Tutari = s.Tutari;
            sm.Customer = s.Customer.ConvertToCustomerModel();
            sm.Customer.ID = s.Customer.ID;
            sm.Customer.Adi = s.Customer.Adi;
            sm.Customer.Soyadi = s.Customer.Soyadi;
            sm.Customer.Adres = s.Customer.Adres;
            sm.Customer.Telefon = s.Customer.Telefon;
            sm.Product = s.Product.ConvertToProductModel();
            sm.Product.ID = s.Product.ID;
            sm.Product.Adi = s.Product.Adi;
            sm.Product.Aciklama = s.Product.Aciklama;
            sm.Product.Category = s.Product.Category.ConvertToCategoryModel();
            sm.Product.Category.ID = s.Product.Category.ID;
            sm.Product.Category.Adi = s.Product.Category.Adi;
            sm.Product.Category.Aciklama = s.Product.Category.Aciklama;
            sm.Product.Alis = s.Product.Alis;
            sm.Product.Satis = s.Product.Satis;
            sm.Product.Stok = s.Product.Stok;
            return sm;
        }
        public static User ConvertToUser(this UserModel um)
        {
            return new User { KullaniciAdi = um.KullaniciAdi, Sifre = um.Sifre };
        }
        public static Customer ConvertToCustomer(this CustomerModel cm)
        {
            return new Customer { Adi = cm.Adi, Soyadi = cm.Soyadi, Adres = cm.Adres, Telefon = cm.Telefon, ID = cm.ID };
        }
        public static Product ConvertToProduct(this ProductModel pm)
        {
            return new Product { ID = pm.ID, Adi = pm.Adi, Aciklama = pm.Aciklama, Alis = pm.Alis, Satis = pm.Satis, Stok = pm.Stok, Kategori = pm.Kategori};//!!!!HATA BURADA ÇÖZÜLDÜ!!!!
            //Product p = new Product();
            //p.ID = pm.ID;
            //p.Adi = pm.Adi;
            //p.Aciklama = pm.Aciklama;
            //p.Alis = pm.Alis;
            //p.Satis = pm.Satis;
            //p.Stok = pm.Stok;
            //p.Kategori = pm.Kategori;
            //p.Category = pm.Category.ConvertToCategory();
            //p.Category.Adi = pm.Category.Adi;
            //p.Category.Aciklama = pm.Category.Aciklama;
            //p.Category.ID = pm.Category.ID;
            //return p;
        }
        public static ProductModel ConvertToProductModel(this Product p)
        {
            return new ProductModel {ID=p.ID,Adi=p.Adi,Aciklama=p.Aciklama,Alis=p.Alis,Satis=p.Satis,Stok=p.Stok,Kategori=p.Kategori,Category=p.Category.ConvertToCategoryModel() }; 
            //ProductModel pm = new ProductModel();
            //pm.ID = p.ID;
            //pm.Adi = p.Adi;
            //pm.Aciklama = p.Aciklama;
            //pm.Alis = p.Alis;
            //pm.Satis = p.Satis;
            //pm.Stok = p.Stok;
            //pm.Kategori = p.Kategori;
            //pm.Category = p.Category.ConvertToCategoryModel();
            //pm.Category.Adi = p.Category.Adi;
            //pm.Category.Aciklama = p.Category.Aciklama;
            //pm.Category.ID = p.Category.ID;
            //return pm;
        }
        public static Category ConvertToCategory(this CategoryModel cm)
        {
            return new Category { ID=cm.ID,Adi=cm.Adi,Aciklama=cm.Aciklama};
            //Category c = new Category();
            //c.ID = cm.ID;
            //c.Adi = cm.Adi;
            //c.Aciklama = cm.Aciklama;
            //return c;
        }
        public static CategoryModel ConvertToCategoryModel(this Category c)
        {
            return new CategoryModel { ID = c.ID, Adi = c.Adi,Aciklama=c.Aciklama };
            //CategoryModel cm = new CategoryModel();
            //cm.Aciklama = c.Aciklama;
            //cm.Adi = c.Adi;
            //cm.ID = c.ID;
            //return cm;
        }
        public static bool SayiMi(this string s)
        {

            foreach (char hane in s)
            {
                if (!Char.IsNumber(hane))
                {
                    return false;

                }

            }
            return true;

        }
    }
}
