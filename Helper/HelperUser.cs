using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFACari.Entity;
using WFACari.Model;

namespace WFACari.Helper
{
    public static class HelperUser
    {
        public static UserModel KullaniciAl(string uN,int pW)
        {
          
            UserModel um = new UserModel();
            using (CariEntities db=new CariEntities())
            {
               
                um.KullaniciAdi = uN;
                um.Sifre = pW;
                return um;
            }
           
           
        }
        public static void SifreGuncelle(string userName,int yeniParola)
        {
            var k = KullaniciAl(userName, yeniParola).ConvertToUser();
            using (CariEntities db = new CariEntities())
            {
                db.User.Where(s => s.KullaniciAdi == k.KullaniciAdi).First().Sifre = k.Sifre;
                db.SaveChanges();
            }
        }
       
        public static bool KullaniciDogrula(User u)
        {
            bool sonuc;
            User us;
          
            using (CariEntities db=new CariEntities())
            {

                try
                {
                    us = db.User.Where(s => s.KullaniciAdi == u.KullaniciAdi.ToLower()).First();
                }
                catch (Exception)
                {
                    sonuc = false;
                    throw;
                }
               
                if (us.Sifre==u.Sifre)
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
    }
}
