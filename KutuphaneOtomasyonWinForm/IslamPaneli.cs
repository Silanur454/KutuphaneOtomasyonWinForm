using KutuphaneOtomasyonWinForm.Kayit;
using KutuphaneOtomasyonWinForm.Kaynak;
using KutuphaneOtomasyonWinForm.Kullanici;
using System;
using System.Windows.Forms;

namespace KutuphaneOtomasyonWinForm
{
    public partial class IslamPaneli : Form
    {
        public IslamPaneli()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
        }

        KutuphaneOtomasyonuEntities db = new KutuphaneOtomasyonuEntities();

        private void IslamPaneli_Load(object sender, EventArgs e)
        {
            // kullanıcı butonları girişte kapalıdır. (ekle-güncelle-sil)
            ekleKullanicibtn.Visible = false;
            guncelleKullanicibtn.Visible = false;
            silKullanicibtn.Visible = false;

            // kaynak butonları girişte kapalıdır. (ekle-güncelle-sil)
            ekleKaynakbtn.Visible = false;
            guncelleKaynakbtn.Visible = false;
            silKaynakbtn.Visible = false;
        }

        // Kullanıcı formları
        private KullaniciListeForm klisteForm;
        private KullaniciEkleForm ekleForm;
        private KullaniciSilForm silForm;
        private KullaniciGuncelleForm guncelleForm;

        private void button1_Click(object sender, EventArgs e)
        {
            if (ekleKullanicibtn.Visible == false)
            {
                ekleKullanicibtn.Visible = true;
                guncelleKullanicibtn.Visible = true;
                silKullanicibtn.Visible = true;

                if (klisteForm == null || klisteForm.IsDisposed)
                {
                    klisteForm = new KullaniciListeForm();
                    klisteForm.MdiParent = this;
                    klisteForm.FormClosed += (s, args) => klisteForm = null;
                    klisteForm.Show();
                }
            }
            else
            {
                ekleKullanicibtn.Visible = false;
                guncelleKullanicibtn.Visible = false;
                silKullanicibtn.Visible = false;

                if (klisteForm != null)
                {
                    klisteForm.Close();
                }
            }
        }

        private void ekleKullanicibtn_Click(object sender, EventArgs e)
        {
            if (ekleForm == null || ekleForm.IsDisposed)
            {
                ekleForm = new KullaniciEkleForm();
                ekleForm.MdiParent = this;
                ekleForm.FormClosed += (s, args) => ekleForm = null;
                ekleForm.Show();
            }
            else
            {
                ekleForm.Close();
            }
        }

        private void silKullanicibtn_Click(object sender, EventArgs e)
        {
            if (silForm == null || silForm.IsDisposed)
            {
                silForm = new KullaniciSilForm();
                silForm.MdiParent = this;
                silForm.FormClosed += (s, args) => silForm = null;
                silForm.Show();
            }
            else
            {
                silForm.Close();
            }
        }

        private void guncelleKullanicibtn_Click(object sender, EventArgs e)
        {
            if (guncelleForm == null || guncelleForm.IsDisposed)
            {
                guncelleForm = new KullaniciGuncelleForm();
                guncelleForm.MdiParent = this;
                guncelleForm.FormClosed += (s, args) => guncelleForm = null;
                guncelleForm.Show();
            }
            else
            {
                guncelleForm.Close();
            }
        }

        // Kaynak formları
        private KaynakListeForm kaynakListeForm;
        private KaynakEkleForm kaynakEkleForm;
        private KaynakSilForm kaynakSilForm;
        private KaynakGuncelleForm kaynakGuncelleForm;

        private void button2_Click(object sender, EventArgs e)
        {
            if (ekleKaynakbtn.Visible == false)
            {
                ekleKaynakbtn.Visible = true;
                guncelleKaynakbtn.Visible = true;
                silKaynakbtn.Visible = true;
            }
            else
            {
                ekleKaynakbtn.Visible = false;
                guncelleKaynakbtn.Visible = false;
                silKaynakbtn.Visible = false;
            }

            if (kaynakListeForm == null || kaynakListeForm.IsDisposed)
            {
                kaynakListeForm = new KaynakListeForm();
                kaynakListeForm.MdiParent = this;
                kaynakListeForm.FormClosed += (s, args) => kaynakListeForm = null;
                kaynakListeForm.Show();
            }
            else
            {
                kaynakListeForm.Close();
            }
        }

        private void ekleKaynakbtn_Click(object sender, EventArgs e)
        {
            if (kaynakEkleForm == null || kaynakEkleForm.IsDisposed)
            {
                kaynakEkleForm = new KaynakEkleForm();
                kaynakEkleForm.MdiParent = this;
                kaynakEkleForm.FormClosed += (s, args) => kaynakEkleForm = null;
                kaynakEkleForm.Show();
            }
            else
            {
                kaynakEkleForm.Close();
            }
        }

        private void silKaynakbtn_Click(object sender, EventArgs e)
        {
            if (kaynakSilForm == null || kaynakSilForm.IsDisposed)
            {
                kaynakSilForm = new KaynakSilForm();
                kaynakSilForm.MdiParent = this;
                kaynakSilForm.FormClosed += (s, args) => kaynakSilForm = null;
                kaynakSilForm.Show();
            }
            else
            {
                kaynakSilForm.Close();
            }
        }

        private void guncelleKaynakbtn_Click(object sender, EventArgs e)
        {
            if (kaynakGuncelleForm == null || kaynakGuncelleForm.IsDisposed)
            {
                kaynakGuncelleForm = new KaynakGuncelleForm();
                kaynakGuncelleForm.MdiParent = this;
                kaynakGuncelleForm.FormClosed += (s, args) => kaynakGuncelleForm = null;
                kaynakGuncelleForm.Show();
            }
            else
            {
                kaynakGuncelleForm.Close();
            }
        }

        // Diğer formlar
        private OduncVerForm oduncForm;
        private GeriAlForm geriForm;
        private CezaForm cezaForm;

        private void button3_Click(object sender, EventArgs e)
        {
            if (oduncForm == null || oduncForm.IsDisposed)
            {
                oduncForm = new OduncVerForm();
                oduncForm.MdiParent = this;
                oduncForm.FormClosed += (s, args) => oduncForm = null;
                oduncForm.Show();
            }
            else
            {
                oduncForm.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (geriForm == null || geriForm.IsDisposed)
            {
                geriForm = new GeriAlForm();
                geriForm.MdiParent = this;
                geriForm.FormClosed += (s, args) => geriForm = null;
                geriForm.Show();
            }
            else
            {
                geriForm.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (cezaForm == null || cezaForm.IsDisposed)
            {
                cezaForm = new CezaForm();
                cezaForm.MdiParent = this;
                cezaForm.FormClosed += (s, args) => cezaForm = null;
                cezaForm.Show();
            }
            else
            {
                cezaForm.Close();
            }
        }
    }
}
