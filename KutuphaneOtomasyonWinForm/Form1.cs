using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace KutuphaneOtomasyonWinForm
{
    public partial class Form1 : Form
    {
        KutuphaneOtomasyonuEntities db = new KutuphaneOtomasyonuEntities();

        public Form1()
        {
            InitializeComponent();
        }

        private void personelGirisbtn_Click(object sender, EventArgs e)
        {
            string gelenAd = adGiristxt.Text;
            string gelenSifre = sifreGiristxt.Text;

            var personel = db.Personeller
                             .Where(x => x.personel_ad.Equals(gelenAd) && x.personel_sifre.Equals(gelenSifre))
                             .FirstOrDefault();


            if (personel == null)
            {
                MessageBox.Show(text:"Kullanıcı adı veya şifre hatalı");
            }
            else
            {
                MessageBox.Show(text:"Başarılı!");
                IslamPaneli panel = new IslamPaneli();
                panel.Show();
                this.Hide();

            }


        }

    }
}

