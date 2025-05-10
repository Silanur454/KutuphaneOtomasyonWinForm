using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KutuphaneOtomasyonWinForm.Kayit
{
    public partial class OduncVerForm : Form
    {
        public OduncVerForm()
        {
            InitializeComponent();
        }
        KutuphaneOtomasyonuEntities db = new KutuphaneOtomasyonuEntities();

        private bool KullaniciCezaKontrol(int kullaniciId)
        {
            using (var yeniDb = new KutuphaneOtomasyonuEntities())
            {
                var kullanici = yeniDb.Kullanicilar.FirstOrDefault(k => k.kullanici_id == kullaniciId);
                if (kullanici == null)
                    return true;

                return kullanici.kullanici_ceza < 30;
            }
        }


        private void OduncVerForm_Load(object sender, EventArgs e)
        {

            //listeledik (kayıtlar)
            var kayitListe= db.Kayitlar.ToList();
            dataGridView1.DataSource = kayitListe.ToList();


            //listeledik (kaynaklar)
            var kaynakList = db.Kaynaklar.ToList();
            dataGridView2.DataSource = kaynakList.ToList();



            //istenmeyen kolonları gizledik
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;


            //kolon adlarını düzenledik
            dataGridView1.Columns[0].HeaderText = "Kayit Id";
            dataGridView1.Columns[1].HeaderText = "Kullanıcı";
            dataGridView1.Columns[2].HeaderText = "Kaynak Ad";
            dataGridView1.Columns[3].HeaderText = "Alış Tarih";
            dataGridView1.Columns[4].HeaderText = "Son Tarih";
            dataGridView1.Columns[5].HeaderText = "Durum";


            dataGridView2.Columns[6].Visible = false;
            dataGridView2.Columns[0].HeaderText = "Kaynak Id";
            dataGridView2.Columns[1].HeaderText = "Kitap Adı";
            dataGridView2.Columns[2].HeaderText = "Yazar Adı";
            dataGridView2.Columns[3].HeaderText = "Yayınevi";
            dataGridView2.Columns[4].HeaderText = "Sayfa Sayısı";
            dataGridView2.Columns[5].HeaderText = "Basım Tarihi";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string arananSecilen = TCBultxt.Text;
            var kullaniciVarMi = db.Kullanicilar.Where(x=>x.kullanici_tc==arananSecilen).FirstOrDefault();

            if (kullaniciVarMi != null)
                label2.Text = kullaniciVarMi.kullanici_ad + "" + kullaniciVarMi.kullanici_soyad;
            else
                label2.Text = "Böyle Bir Kullanını Bulunamadı!!!";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string gelenAd = textBox1.Text;
            var bulunanKaynaklar = db.Kaynaklar.Where(x => x.kaynak_ad.Contains(gelenAd)).ToList();
            dataGridView2.DataSource = bulunanKaynaklar;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //kişiyi aldık
            string secilenKisiTC = TCBultxt.Text;
            var secilenKisi = db.Kullanicilar.Where(x => x.kullanici_tc.Equals(secilenKisiTC)).FirstOrDefault();

            //kitabi aldık
            int secilenKitapId = Convert.ToInt16(dataGridView2.CurrentRow.Cells[0].Value);
            var secilenKitap = db.Kaynaklar.Where(x => x.kaynak_id == secilenKitapId).FirstOrDefault();

            // Ceza kontrolü yapalım
            if (!KullaniciCezaKontrol(secilenKisi.kullanici_id))
            {
                MessageBox.Show("Bu kullanıcıya cezası 30 TL veya üzeri olduğu için kitap ödünç verilemez!");
                return;
            }


            Kayitlar yeniKayit = new Kayitlar();
            yeniKayit.kitap_id = secilenKitap.kaynak_id;
            yeniKayit.kullanici_id = secilenKisi.kullanici_id;
            yeniKayit.alis_tarih = DateTime.Today;
            yeniKayit.son_tarih = DateTime.Today.AddDays(15);
            yeniKayit.durum = false;
            db.Kayitlar.Add(yeniKayit);
            db.SaveChanges();

            var kayitListe = db.Kayitlar.ToList();
            dataGridView1.DataSource = kayitListe.ToList();
        }

    }
    }

