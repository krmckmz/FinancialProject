using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WFACari.Entity;
using WFACari.Helper;
using WFACari.Model;

namespace WFACari
{
    public partial class FrmAdmin : Form
    {
        public FrmAdmin(string userName)
        {
            InitializeComponent();
            this.userName = userName;
        }
        string userName;
        private void FrmAdmin_Load(object sender, EventArgs e)
        {
            timer1.Start();
            VeriDoldur();
            lblKarsila.Text = $"Hoşgeldiniz, {userName}";
            lblYeniSifre.Visible = false;

        }
        public Tuple<decimal, int> KarZararHesapla()
        {
            var liste = HelperSale.SaleModelListeDon();
            int adet = 0;
            decimal durum = 0;
            foreach (var item in liste)
            {
                durum += (item.Product.Satis - item.Product.Alis) * item.UrunAdedi;
                adet += item.UrunAdedi;
            }
            return Tuple.Create<decimal, int>(durum, adet);

        }
        public void VeriDoldur()
        {

            MusteriListele(HelperCustomer.MusteriListeDon());
            KategoriListele();
            KategoriSecimDoldur();
            UrunListele(HelperProduct.UrunListeDon());
       
            dgvSatislar.Rows.Clear();
            SatisListele(HelperSale.SaleModelListeDon());

            var f = KarZararHesapla();
            lblSatilanGenel.Text = f.Item2.ToString();
            lblKarGenel.Text = f.Item1.ToString();
        }
      
        public void SatisListele(List<SaleModel>sml)
        {


            dgvSatislar.Rows.Clear();
            foreach (var item in sml)
            {
                dgvSatislar.Rows.Add(item.ID, item.Customer.Adi, item.Product.Adi, item.UrunAdedi, item.Tutari, item.KayitTarihi);
            }


        }
        public void MusteriListele(List<CustomerModel>cml)
        {
            cmbMusteriler.Items.Clear();
            var f = cml;
            dgvMusteri.Rows.Clear();
            foreach (var item in cml)
            {
                dgvMusteri.Rows.Add(item.ID, item.Adi, item.Soyadi, item.Telefon, item.Adres);
            }
            foreach (var item in f)
            {
                cmbMusteriler.Items.Add(item.Adi);

            }

        }
        public void KategoriSecimTemizle()
        {
            cmbKategori.Items.Clear();
            cmbKategori2.Items.Clear();
            cmbKategorilerSatis.Items.Clear();

        }
        public void KategoriSecimDoldur()
        {
            KategoriSecimTemizle();

            var f = HelperCategory.KategoriListeDon();
            foreach (var item in f)
            {
                cmbKategori.Items.Add(item.Adi);
                cmbKategori2.Items.Add(item.Adi);
                cmbKategorilerSatis.Items.Add(item.Adi);
            }
        }
        public void KategoriListele()
        {
            var liste = HelperCategory.KategoriListeDon();
            foreach (var item in liste)
            {
                dgvKategori.Rows.Add(item.ID, item.Adi, item.Aciklama);
            }
        }
        public void UrunListele(List<ProductModel>pml)
        {
            dgvUrunler.Rows.Clear();
            foreach (var item in pml)
            {
                dgvUrunler.Rows.Add(item.ID, item.Adi, item.Category.Adi, item.Aciklama, item.Alis, item.Satis, item.Stok);
            }
            //dgvUrunler.DataSource = HelperProduct.UrunListeDon();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {

            int silinecek = Convert.ToInt32(dgvMusteri.CurrentRow.Cells["customerID"].Value);
            MusteriSil(silinecek);
            MusteriListele(HelperCustomer.MusteriListeDon());

        }
        int guncellenecek;
        private void dgvMusteri_SelectionChanged(object sender, EventArgs e)
        {
            guncellenecek = (int)(dgvMusteri.CurrentRow.Cells["customerID"].Value);

        }

        private void btnEkle_Click(object sender, EventArgs e)
        {

            MusteriEkle(txtAdi.Text, txtSoyadi.Text, txtTel.Text, txtAdres.Text);
            MusteriListele(HelperCustomer.MusteriListeDon());
            Temizle();
        }
        public void MusteriEkle(string ad, string sa, string tel, string adr)
        {
            using (CariEntities db = new CariEntities())
            {
                try
                {

                    db.Customer.Add(HelperCustomer.MusteriAl(ad, sa, tel, adr).ConvertToCustomer());
                    db.SaveChanges();
                    MessageBox.Show("Müşteri kaydedildi.");

                }
                catch (Exception)
                {
                    MessageBox.Show("Müşteri kaydedilirken hata oluştu.");
                    throw;

                }

            }
        }
        public void MusteriSil(int silinecek)
        {
            using (CariEntities db = new CariEntities())
            {
                try
                {
                    db.Entry(HelperCustomer.IdAl(silinecek).ConvertToCustomer()).State = EntityState.Deleted;
                    db.SaveChanges();
                    MessageBox.Show("Müşteri silindi.");

                }
                catch (Exception)
                {
                    MessageBox.Show("Müşteri silinirken hata oluştu.");
                    throw;
                }

            }
        }
        public void MusteriGuncelle(int guncellenecek, string ad, string sa, string tel, string adr)
        {
            using (CariEntities db = new CariEntities())
            {
                try
                {
                    var c = db.Customer.Find(guncellenecek);
                    var yeni = HelperCustomer.MusteriAl(ad, sa, tel, adr);
                    c.Adi = yeni.Adi;
                    c.Soyadi = yeni.Soyadi;
                    c.Telefon = yeni.Telefon;
                    c.Adres = yeni.Adres;
                    db.SaveChanges();
                    MessageBox.Show("Müşteri güncellendi.");
                }
                catch (Exception)
                {
                    MessageBox.Show("Müşteri güncellenirken hata oluştu.");
                    throw;
                }
            }
        }
        public void Temizle()
        {
            txtAdi.Text = "";
            txtAdi2.Text = "";
            txtAdres.Text = "";
            txtAdres2.Text = "";
            txtSoyadi.Text = "";
            txtSoyadi2.Text = "";
            txtTel.Text = "";
            txtTel2.Text = "";


        }
        public void TemizleKategori()
        {
            txtAciklamaKategori.Text = "";
            txtAciklamaKategori2.Text = "";
            txtAdiKategori.Text = "";
            txtAdiKategori2.Text = "";
        }

        private void btnDuzenle_Click(object sender, EventArgs e)
        {
            MusteriGuncelle(guncellenecek, txtAdi2.Text, txtSoyadi2.Text, txtTel2.Text, txtAdres2.Text);
            MusteriListele(HelperCustomer.MusteriListeDon());
            Temizle();
        }

        int islemYapilacak;
        private void dgvUrunler_SelectionChanged(object sender, EventArgs e)
        {
            islemYapilacak = (int)(dgvUrunler.CurrentRow.Cells["urunID"].Value);

        }

        private void btnEkleUrun_Click(object sender, EventArgs e)
        {


            using (CariEntities db = new CariEntities())
            {

                string c = cmbKategori.SelectedItem.ToString();

                bool sonuc = HelperProduct.CrudUrun(HelperProduct.EklenecekUrunAl(txtAdiUrun.Text, c, txtAlisUrun.Text, txtSatisUrun.Text, txtStokUrun.Text, txtAciklamaUrun.Text), EntityState.Added);
                if (sonuc)
                {
                    MessageBox.Show("Ürün eklendi.");
                }
                else
                {
                    MessageBox.Show("Ürün eklenemedi.");
                }
                UrunListele(HelperProduct.UrunListeDon());
            }

        }


        private void btnSilUrun_Click(object sender, EventArgs e)
        {

            bool sonuc = HelperProduct.CrudUrun(HelperProduct.SilinecekUrunAl(islemYapilacak), EntityState.Deleted);
            if (sonuc)
            {
                MessageBox.Show("Ürün silindi.");
            }
            else
            {
                MessageBox.Show("Ürün silinemedi.");
            }
            UrunListele(HelperProduct.UrunListeDon());

        }

        private void btnDuzenleUrun_Click(object sender, EventArgs e)
        {

            var sonuc = HelperProduct.CrudUrun(HelperProduct.GuncellenecekUrunAl(islemYapilacak, txtAdiUrun2.Text, cmbKategori2.SelectedItem.ToString(), txtAlisUrun2.Text, txtSatisUrun2.Text, txtStokUrun2.Text, txtAciklamaUrun2.Text), EntityState.Modified);
            if (sonuc)
            {
                MessageBox.Show("Ürün güncellendi.");
            }
            else
            {
                MessageBox.Show("Ürün güncellenemedi.");

            }
            UrunListele(HelperProduct.UrunListeDon());
        }

        //private void button3_Click(object sender, EventArgs e)
        //{

        //}
        public static bool KategoriEkle(string adi, string aciklama)
        {
            return HelperCategory.CrudCategory(HelperCategory.EklenecekKategoriAl(adi, aciklama), EntityState.Added);

        }

        private void btnSilKategori_Click(object sender, EventArgs e)
        {
            bool sonuc = KategoriSil(islemYapilacak2);
            if (sonuc)
            {
                MessageBox.Show("Kategori silindi.");
                KategoriListele();
                TemizleKategori();
            }
            else
            {
                MessageBox.Show("Kategori silinemedi.");
            }
        }
        public bool KategoriSil(int ID)
        {
            return HelperCategory.CrudCategory(HelperCategory.SilinecekKategoriAl(ID), EntityState.Deleted);
        }
        int islemYapilacak2;
        private void dgvKategori_SelectionChanged(object sender, EventArgs e)
        {
            islemYapilacak2 = (int)dgvKategori.CurrentRow.Cells["kategoriID"].Value;

        }
        public bool KategoriGuncelle(int ID, string adi, string aciklama)
        {
            return HelperCategory.CrudCategory(HelperCategory.GuncellenecekKategoriAl(ID, adi, aciklama), EntityState.Modified);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool sonuc = KategoriGuncelle(islemYapilacak2, txtAdiKategori2.Text, txtAciklamaKategori2.Text);
            if (sonuc)
            {
                MessageBox.Show("Kategori güncellendi.");
                KategoriListele();
                TemizleKategori();
            }
            else
            {
                MessageBox.Show("Kategori güncellenemedi.");
            }
        }

        private void btnDegistirKullanici_Click(object sender, EventArgs e)
        {

            SifreYenile(userName, txtYeniParola.Text, txtYeniParola2.Text);
        }

        public void SifreYenile(string userName, string yeniParola, string yeniParola2)
        {


            if (yeniParola.SayiMi())
            {
                if (yeniParola == yeniParola2)
                {
                    try
                    {
                        HelperUser.SifreGuncelle(userName, Convert.ToInt32(txtYeniParola.Text));
                        MessageBox.Show("Şifreniz güncellendi.");
                        TemizleKullanici();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Şifre güncellenemedi.");
                        TemizleKullanici();
                        throw;
                    }
                }
                else
                {
                    MessageBox.Show("Parola ve tekrarı eşleşmedi.");
                    TemizleKullanici();
                }
            }
            else
            {
                MessageBox.Show("Parola sadece rakam içermelidir.");
                TemizleKullanici();
            }

        }
        public void TemizleKullanici()
        {
            txtEskiParola.Text = "";
            txtYeniParola.Text = "";
            txtYeniParola2.Text = "";
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            var d = SifreDogrula(userName, txtEskiParola.Text);
            if (d)
            {
                txtYeniParola.Enabled = true;
                txtYeniParola2.Enabled = true;
                btnDegistirKullanici.Enabled = true;
                lblYeniSifre.Visible = true;
                lblYeniSifre.Text = "Yeni şifrenizi giriniz.";

            }
            else
            {
                MessageBox.Show("Parolanızı yanlış girdiniz.");
                txtEskiParola.Text = "";

            }
        }
        public bool SifreDogrula(string userName, string eskiParola)
        {

            bool dogruMu = HelperUser.KullaniciDogrula(HelperUser.KullaniciAl(userName, Convert.ToInt32(txtEskiParola.Text)).ConvertToUser());

            return dogruMu;
        }

        private void btnKategoriListeleSatis_Click(object sender, EventArgs e)
        {
            if (cmbKategorilerSatis.Text.Length > 0)
            {

                lblKategorSecin.Visible = false;
                lblSatilacakSecin.Visible = true;

                var f = KategoriAl(cmbKategorilerSatis.SelectedItem.ToString());
                var x= HelperProduct.UrunListeDonByID(f);
                dgvUrunlerSatis.Rows.Clear();
                foreach (var item in x)
                {
                    dgvUrunlerSatis.Rows.Add(item.ID,item.Adi,item.Kategori,item.Aciklama,item.Stok,item.Alis,item.Satis);
                }
            }
            else
            {
                MessageBox.Show("Lütfen kategori seçin.");
            }


        }
        public static int KategoriAl(string adi)
        {
            using (CariEntities db = new CariEntities())
            {

                int listelenecek = db.Category.Where(c => c.Adi == adi).First().ID;
                return listelenecek;
            }
        }
        int islemYapilacakSatis;
        private void dgvUrunlerSatis_SelectionChanged(object sender, EventArgs e)
        {
            islemYapilacakSatis = (int)dgvUrunlerSatis.CurrentRow.Cells["productID"].Value;
            using (CariEntities db = new CariEntities())
            {
                var z = db.Product.Find(islemYapilacakSatis);
                txtSecilenUrun.Text = z.Adi;
                lblSatilacakSecin.Visible = true;



            }

        }
       

        private void tpKullanici_Selected(object sender, TabControlEventArgs e)
        {
    
            dgvKategori.Rows.Clear();
            VeriDoldur();
        }

        private void btnEkleKategori_Click(object sender, EventArgs e)
        {
            bool sonuc = KategoriEkle(txtAdiKategori.Text, txtAciklamaKategori.Text);
            if (sonuc)
            {
                MessageBox.Show("Kategori eklendi.");
                dgvKategori.Rows.Clear();
                KategoriListele();
                TemizleKategori();
                KategoriSecimDoldur();
            }
            else
            {
                MessageBox.Show("Kategori eklenemedi.");
            }
        }
        public bool ItemsDoluMu()
        {
            if (cmbKategorilerSatis.Text != "" & txtSecilenUrun.Text != "" & txtAdet.Text != "" & cmbMusteriler.Text != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void btnSatisTamamla_Click(object sender, EventArgs e)
        {
            if (ItemsDoluMu())
            {
                bool sonuc = SatisYap(cmbKategorilerSatis.SelectedItem.ToString(), islemYapilacakSatis, Convert.ToInt32(txtAdet.Text), cmbMusteriler.SelectedItem.ToString());
                if (sonuc)
                {
                    MessageBox.Show("Satış Eklendi");
                }
                else
                {
                    MessageBox.Show("Satış Eklenemedi.");
                }

            }
            else
            {
                MessageBox.Show("Gerekli alanları doldurun.");
            }
        }
        public bool SatisYap(string kAd, int urunID, int adet, string mAdi)
        {
            bool sonuc;
            using (CariEntities db = new CariEntities())
            {
                int kID = db.Category.Where(x => x.Adi == kAd).First().ID;
                int mID = db.Customer.Where(y => y.Adi == mAdi).First().ID;
                var z = (HelperSale.SatisAl(kID, islemYapilacakSatis, adet, mID)).ConvertToSale();
                sonuc = HelperSale.SatisEkle(z);
            }
            return sonuc;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblSaat.Text = DateTime.Now.ToLongTimeString();
        }

        private void rbMusteri_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbUrun_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbKategori_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtAranacak_TextChanged(object sender, EventArgs e)
        {
            if (txtAranacak.Text.Length > 0)
            {
                lblKritereGore.Visible = true;
                lblSatilan.Visible = true;
                lblKar.Visible = true;
                lblKarZarar.Visible = true;
                lblSatilanAdet.Visible = true;
               
                if (rbMusteri.Checked)
                {
                  
                    var x = HelperSale.MusteriyeGoreListele(txtAranacak.Text, dtpBaslangic.Value, dtpBitis.Value);
                    SatisListele(x);
                    //dgvSatislar.DataSource = x;
                    var xx = KarZararHesaplaKritereGore(x);
                    lblKarZarar.Text = xx.Item1.ToString();
                    lblSatilanAdet.Text = xx.Item2.ToString();

                }
                else if (rbKategori.Checked)
                {
                    
                    var y = HelperSale.KategoriyeGoreListele(txtAranacak.Text, dtpBaslangic.Value, dtpBitis.Value);
                    SatisListele(y);
                    //dgvSatislar.DataSource = y;
                    var yy = KarZararHesaplaKritereGore(y);
                    lblKarZarar.Text = yy.Item1.ToString();
                    lblSatilanAdet.Text = yy.Item2.ToString();
                }
                else
                {
                    
                    var z = HelperSale.UruneGoreListele(txtAranacak.Text, dtpBaslangic.Value, dtpBitis.Value);
                    SatisListele(z);
                    //dgvSatislar.DataSource = z;
                    var zz = KarZararHesaplaKritereGore(z);
                    lblKarZarar.Text = zz.Item1.ToString();
                    lblSatilanAdet.Text = zz.Item2.ToString();
                }
            }
            else
            {
                dgvSatislar.Rows.Clear();
                SatisListele(HelperSale.SaleModelListeDon());
            }


        }
        public Tuple<decimal, int> KarZararHesaplaKritereGore(List<SaleModel> sml)
        {

            decimal durum = 0;
            int adet = 0;

            foreach (var s in sml)
            {
                durum += (s.Product.Satis - s.Product.Alis) * s.UrunAdedi;
                adet += s.UrunAdedi;
            }
            return Tuple.Create<decimal, int>(durum, adet);

        }

        private void tpRapor_Click(object sender, EventArgs e)
        {

        }
    }
}
