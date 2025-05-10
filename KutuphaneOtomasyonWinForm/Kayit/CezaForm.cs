using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KutuphaneOtomasyonWinForm.Kayit
{
    public partial class CezaForm : Form
    {
        public CezaForm()
        {
            InitializeComponent();
        }

        private void CezaForm_Load(object sender, EventArgs e)
        {


            SqlConnection conn = new SqlConnection(@"Data Source=SILANUR\MSSQLSERVER1;Initial Catalog=KutuphaneOtomasyonu;Integrated Security=True");
            conn.Open();

            string query = @"
                 SELECT 
                   k.kullanici_id,
                   k.kullanici_ad,
                   o.kitap_id,
                    o.alis_tarih,
                    DATEDIFF(DAY, o.alis_tarih, GETDATE()) - 15 AS geciken_gun,
    (DATEDIFF(DAY, o.alis_tarih, GETDATE()) - 15) * 3 AS ceza_miktari
FROM 
    Kayitlar AS o
JOIN 
    Kullanicilar AS k ON o.kullanici_id = k.kullanici_id
WHERE 
    o.durum = 0 AND 
    DATEDIFF(DAY, o.alis_tarih, GETDATE()) > 15";

            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            conn.Close();


            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Kullanıcı";
            dataGridView1.Columns[2].HeaderText = "Kaynak Id";
            dataGridView1.Columns[3].HeaderText = "Alış Tarih";
            dataGridView1.Columns[4].HeaderText = "Geciken Gün";
            dataGridView1.Columns[5].HeaderText = "Ceza Miktarı";

        }

        private void btnCezaGuncelle_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=SILANUR\MSSQLSERVER1;Initial Catalog=KutuphaneOtomasyonu;Integrated Security=True");

            conn.Open();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["kullanici_id"].Value == null)
                    continue;

                int kullaniciId = Convert.ToInt32(row.Cells["kullanici_id"].Value);
                int cezaMiktari = Convert.ToInt32(row.Cells["ceza_miktari"].Value);

                SqlCommand cmd = new SqlCommand("UPDATE Kullanicilar SET kullanici_ceza = kullanici_ceza + @ceza WHERE kullanici_id = @id", conn);
                cmd.Parameters.AddWithValue("@ceza", cezaMiktari);
                cmd.Parameters.AddWithValue("@id", kullaniciId);
                cmd.ExecuteNonQuery();
            }

            conn.Close();
            MessageBox.Show("Ceza güncellemeleri yapıldı.");
        }
    }
}
