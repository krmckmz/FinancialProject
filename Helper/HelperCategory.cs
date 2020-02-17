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
    public static class HelperCategory
    {
        public static List<CategoryModel> KategoriListeDon()
        {
            List<CategoryModel> cml = new List<CategoryModel>();
            using (CariEntities db=new CariEntities())
            {
             
                var ff= db.Category.ToList();
                foreach (var item in ff)
                {
                    CategoryModel cm = new CategoryModel();
                    cm.ID = item.ID;
                    cm.Adi = item.Adi;
                    cm.Aciklama = item.Aciklama;
                   
                    cml.Add(cm);
                }
            }
            return cml;
        }
        public static bool CrudCategory(CategoryModel cm,EntityState state)
        {
            var c = cm.ConvertToCategory();
            bool sonuc;
            using (CariEntities db=new CariEntities())
            {
                db.Entry(c).State = state;
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
        public static CategoryModel EklenecekKategoriAl(string adi,string aciklama)
        {
            CategoryModel cm = new CategoryModel();
            cm.Adi = adi;
            cm.Aciklama = aciklama;
            return cm;
           
        }
        public static CategoryModel SilinecekKategoriAl(int ID)
        {
            CategoryModel cm = new CategoryModel();
            using (CariEntities db=new CariEntities())
            {
               var f= db.Category.Where(c => c.ID == ID).First();
                cm.ID = f.ID;
                cm.Adi = f.Adi;
                cm.Aciklama = f.Aciklama;

            }
            return cm;
        }
        //public static CategoryModel UrunKategorisiAl(string ad)
        //{
        //        CategoryModel cm=new CategoryModel();

           
        //    return cm;
        //}
        public static CategoryModel GuncellenecekKategoriAl(int ID,string adi,string aciklama)
        {
            CategoryModel cm = new CategoryModel();
            using (CariEntities db=new CariEntities())
            {
                var f = db.Category.Where(c => c.ID == ID).First();
                cm.ID = f.ID;
                cm.Adi = adi;
                cm.Aciklama = aciklama;
            }
            return cm;
        }
    }
}
