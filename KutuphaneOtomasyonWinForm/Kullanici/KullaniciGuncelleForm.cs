﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KutuphaneOtomasyonWinForm.Kullanici
{
    public partial class KullaniciGuncelleForm : Form
    {
        public KullaniciGuncelleForm()
        {
            InitializeComponent();
        }

        KutuphaneOtomasyonuEntities db = new KutuphaneOtomasyonuEntities();

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

        private void KullaniciGuncelleForm_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            kullaniciAdtxt.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            kullaniciSoyadtxt.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            kullaniciTCtxt.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            kullaniciMailtxt.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            kullaniciTeltxt.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            kullaniciCezatxt.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();

            if (dataGridView1.CurrentRow.Cells[7].Value.ToString().Equals("E"))
                radioE.Checked = true;
            else radioK.Checked = true;
        }
    }
}
