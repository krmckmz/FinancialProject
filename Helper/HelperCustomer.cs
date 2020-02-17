using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFACari.Entity;
using WFACari.Model;

namespace WFACari.Helper
{
    public static class HelperCustomer
    {
        public static List<CustomerModel> MusteriListeDon()
        { 
            using (CariEntities db=new CariEntities())
            {
                List<CustomerModel> cml = new List<CustomerModel>();
               List<Customer> c = db.Customer.ToList();
                foreach (var item in c)
                {
                    CustomerModel cm = new CustomerModel();
                    cm.ID = item.ID;
                    cm.Adi = item.Adi;
                    cm.Soyadi = item.Soyadi;
                    cm.Adres = item.Adres;
                    cm.Telefon = item.Telefon;
                    cml.Add(cm);
                }
                return cml;
            }
        }
        public static CustomerModel MusteriAl(string ad,string sa,string tel,string adr)
        {
            CustomerModel cm = new CustomerModel();
            cm.Adi = ad;
            cm.Soyadi = sa;
            cm.Telefon = tel;
            cm.Adres = adr;
            
            return cm;
        }
        public static CustomerModel IdAl(int silinecek)
        {
            CustomerModel cm = new CustomerModel();
            return new CustomerModel { ID = silinecek };
        }

    }
}
