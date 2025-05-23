using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;


namespace KutuphaneOtomasyonWinForm.Kayit
{
    public partial class OduncVerForm : Form
    {
        public OduncVerForm()
        {
            InitializeComponent();
            dataGridView1.CellFormatting += dataGridView1_CellFormatting; // Renk için event
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
            var kayitList = from kayit in db.Kayitlar
                            select new
                            {
                                Kullanici = kayit.Kullanicilar.kullanici_ad + " " + kayit.Kullanicilar.kullanici_soyad,
                                Kaynak = kayit.Kaynaklar.kaynak_ad,
                                AlisTarihi = kayit.alis_tarih,
                                SonTarih = kayit.son_tarih,
                                Durum = (kayit.durum ?? false) ? "İade Gerçekleşti" : "İade Gerçekleşmedi"
                            };

            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = kayitList.ToList();
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = kayitList.ToList();

            // DURUM sütununu genişlet
            dataGridView1.Columns["Durum"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;


            var kaynakList = db.Kaynaklar.ToList();
            dataGridView2.DataSource = kaynakList;

            // Kolon başlıklarını ayarla (dataGridView1)
            dataGridView1.Columns[0].HeaderText = "Kullanıcı";
            dataGridView1.Columns[1].HeaderText = "Kaynak";
            dataGridView1.Columns[2].HeaderText = "Alış Tarihi";
            dataGridView1.Columns[3].HeaderText = "Son Tarihi";
            dataGridView1.Columns[4].HeaderText = "Durum";

            // dataGridView2 başlıklarını ayarla
            dataGridView2.Columns[0].HeaderText = "Kaynak Id";
            dataGridView2.Columns[1].HeaderText = "Kitap Adı";
            dataGridView2.Columns[2].HeaderText = "Yazar Adı";
            dataGridView2.Columns[3].HeaderText = "Yayınevi";
            dataGridView2.Columns[4].HeaderText = "Sayfa Sayısı";
            dataGridView2.Columns[5].HeaderText = "Basım Tarihi";

            // Fazladan kolon varsa gizle
            if (dataGridView2.Columns.Count > 6)
                dataGridView2.Columns[6].Visible = false;


            // Bugün alınacak ve gecikmiş kitap sayısını hesapla
            int bugunAlinacak = db.Kayitlar.Count(k =>
                (k.durum == false || k.durum == null) &&
                DbFunctions.TruncateTime(k.son_tarih) == DateTime.Today);

            int geciken = db.Kayitlar.Count(k =>
                (k.durum == false || k.durum == null) &&
                DbFunctions.TruncateTime(k.son_tarih) < DateTime.Today);

            lblBugunSayisi.Text = bugunAlinacak.ToString();
            lblGecikmeSayisi.Text = geciken.ToString();

            // Renk ve stil ayarı
            lblBugunSayisi.ForeColor = Color.Blue;
            lblGecikmeSayisi.ForeColor = Color.Red;
            lblBugunSayisi.Font = new Font(lblBugunSayisi.Font, FontStyle.Bold);
            lblGecikmeSayisi.Font = new Font(lblGecikmeSayisi.Font, FontStyle.Bold);

        }

    private void button1_Click(object sender, EventArgs e)
        {
            string arananSecilen = TCBultxt.Text;
            var kullaniciVarMi = db.Kullanicilar.FirstOrDefault(x => x.kullanici_tc == arananSecilen);

            if (kullaniciVarMi != null)
                label2.Text = kullaniciVarMi.kullanici_ad + " " + kullaniciVarMi.kullanici_soyad;
            else
                label2.Text = "Böyle Bir Kullanıcı Bulunamadı!!!";
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
            var secilenKisi = db.Kullanicilar.FirstOrDefault(x => x.kullanici_tc.Equals(secilenKisiTC));

            if (secilenKisi == null)
            {
                MessageBox.Show("Seçilen kullanıcı bulunamadı.");
                return;
            }

            //kitabı aldık
            if (dataGridView2.CurrentRow == null)
            {
                MessageBox.Show("Lütfen bir kitap seçin.");
                return;
            }

            int secilenKitapId = Convert.ToInt32(dataGridView2.CurrentRow.Cells[0].Value);
            var secilenKitap = db.Kaynaklar.FirstOrDefault(x => x.kaynak_id == secilenKitapId);

            if (secilenKitap == null)
            {
                MessageBox.Show("Seçilen kitap veritabanında bulunamadı.");
                return;
            }

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

            var guncelKayitList = from kayit in db.Kayitlar
                                  select new
                                  {
                                      Kullanici = kayit.Kullanicilar.kullanici_ad + " " + kayit.Kullanicilar.kullanici_soyad,
                                      Kaynak = kayit.Kaynaklar.kaynak_ad,
                                      AlisTarihi = kayit.alis_tarih,
                                      SonTarih = kayit.son_tarih,
                                      Durum = (kayit.durum ?? false) ? "İade Gerçekleşti" : "İade Gerçekleşmedi"
                                  };

            dataGridView1.DataSource = guncelKayitList.ToList();


            // Güncel sayıları tekrar hesapla
            int bugunAlinacak = db.Kayitlar.Count(k =>
                (k.durum == false || k.durum == null) &&
                DbFunctions.TruncateTime(k.son_tarih) == DateTime.Today);

            int geciken = db.Kayitlar.Count(k =>
                (k.durum == false || k.durum == null) &&
                DbFunctions.TruncateTime(k.son_tarih) < DateTime.Today);

            lblBugunSayisi.Text = bugunAlinacak.ToString();
            lblGecikmeSayisi.Text = geciken.ToString();



        }

        //Durum sütununu renklendir
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].HeaderText == "Durum")
            {
                if (e.Value != null)
                {
                    string durum = e.Value.ToString();
                    if (durum == "İade Gerçekleşti")
                    {
                        e.CellStyle.ForeColor = Color.Green;
                        e.CellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
                    }
                    else if (durum == "İade Gerçekleşmedi")
                    {
                        e.CellStyle.ForeColor = Color.Red;
                        e.CellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
                    }
                }
            }
        }
    }
}
