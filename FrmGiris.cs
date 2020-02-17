using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WFACari.Helper;
namespace WFACari
{
    public partial class FrmGiris : Form
    {
        public FrmGiris()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            bool sayiMi = txtPassword.Text.SayiMi();
            if (sayiMi)
            {
                bool sonuc = HelperUser.KullaniciDogrula(HelperUser.KullaniciAl(txtUser.Text, Convert.ToInt32(txtPassword.Text)).ConvertToUser());
                if (sonuc)
                {

                    this.Hide();
                    FrmAdmin fa = new FrmAdmin(txtUser.Text);
                    fa.Show();


                }
                else
                {
                    MessageBox.Show("Kullanıcı adı veya şifrenizi yanlış girdiniz.");
                    Temizle(sayiMi);
                }
            }
            else
            {
                MessageBox.Show("Parola sadece sayı içermelidir.");
                Temizle(sayiMi);
            }
        
        }
        public void Temizle(bool sayiMi)
        {
            if (sayiMi)
            {
                txtUser.Text = "";
                txtPassword.Text = "";
            }
            else
            {
                txtPassword.Text = "";
            }
          
        }
        

    }
}
