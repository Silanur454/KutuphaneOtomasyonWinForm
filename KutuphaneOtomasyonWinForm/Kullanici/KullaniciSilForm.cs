using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KutuphaneOtomasyonWinForm.Kullanici
{
    public partial class KullaniciSilForm : Form
    {
        public KullaniciSilForm()
        {
            InitializeComponent();
        }
        KutuphaneOtomasyonuEntities db=new KutuphaneOtomasyonuEntities();
        public void Listele()
        {
            var kullanicilar = db.Kullanicilar.ToList();
            dataGridView1.DataSource = kullanicilar.ToList();


            // id ve kayıtlar kolonunu gizledik
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[8].Visible = false;

            //kalan kolonların isimlerini düzenledik
            dataGridView1.Columns[1].HeaderText = "Ad";
            dataGridView1.Columns[2].HeaderText = "Soyad";
            dataGridView1.Columns[3].HeaderText = "TC";
            dataGridView1.Columns[4].HeaderText = "Mail";
            dataGridView1.Columns[5].HeaderText = "Telefon No";
            dataGridView1.Columns[6].HeaderText = "Ceza";
            dataGridView1.Columns[7].HeaderText = "Cinsiyet";

        }

        private void KullaniciSilForm_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Lütfen silmek için bir kullanıcı seçin.");
                return;
            }

            DialogResult sonuc = MessageBox.Show("Bu kullanıcıyı silmek istediğinizden emin misiniz?",
                                                 "Onayla",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Warning);

            if (sonuc == DialogResult.Yes)
            {
                int secilenId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                var kullanici = db.Kullanicilar.FirstOrDefault(x => x.kullanici_id == secilenId);

                if (kullanici != null)
                {
                    db.Kullanicilar.Remove(kullanici);
                    db.SaveChanges();
                    MessageBox.Show("Kullanıcı başarıyla silindi.");
                    Listele(); // Yeniden listele
                }
                else
                {
                    MessageBox.Show("Kullanıcı bulunamadı.");
                }
            }
        }

    }
}
