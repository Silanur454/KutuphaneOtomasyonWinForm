﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KutuphaneOtomasyonWinForm.Kaynak
{
    public partial class KaynakGuncelleForm : Form
    {
        public KaynakGuncelleForm()
        {
            InitializeComponent();
        }
        KutuphaneOtomasyonuEntities db = new KutuphaneOtomasyonuEntities();

    private void KaynakGuncelleForm_Load(object sender, EventArgs e)
        {
            numericUpDown1.Minimum = 1;
            numericUpDown1.Maximum = 2000;
            numericUpDown1.Increment = 1;

            var kaynaklar = db.Kaynaklar.ToList();
            dataGridView1.DataSource = kaynaklar.ToList();

            // id ve kayıtlar kolonunu gizledik
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[6].Visible = false;

            //kalan kolonların isimlerini düzenledik
            dataGridView1.Columns[1].HeaderText = "Kitap Adı";
            dataGridView1.Columns[2].HeaderText = "Yazar Adı";
            dataGridView1.Columns[3].HeaderText = "Yayınevi";
            dataGridView1.Columns[4].HeaderText = "Sayfa Sayısı";
            dataGridView1.Columns[5].HeaderText = "Basım Tarihi";
        }


    private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            adKaynaktxt.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            yazarKaynaktxt.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            yayinciKaynaktxt.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            numericUpDown1.Value = Convert.ToDecimal(dataGridView1.CurrentRow.Cells[4].Value);
            dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[5].Value);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int secilenKaynak = Convert.ToInt16(dataGridView1.CurrentRow.Cells[0].Value);
            var guncellenecekKaynak = db.Kaynaklar.Where(x => x.kaynak_id == secilenKaynak).FirstOrDefault();
            guncellenecekKaynak.kaynak_ad = adKaynaktxt.Text;
            guncellenecekKaynak.kaynak_yazar = yazarKaynaktxt.Text;
            guncellenecekKaynak.kaynak_yayıncı = yazarKaynaktxt.Text;
            guncellenecekKaynak.kaynak_sayfasayisi = Convert.ToInt16(numericUpDown1.Value);
            guncellenecekKaynak.kaynak_basımtarihi = dateTimePicker1.Value;
            db.SaveChanges();

            var kaynaklar = db.Kaynaklar.ToList();
            dataGridView1.DataSource = kaynaklar.ToList();
        }
    }
}
