# Kütüphane Otomasyon Sistemi (WinForm)

Bu proje, küçük ölçekli bir kütüphane için geliştirilen bir otomasyon sistemidir. Kullanıcı, kaynak ve ödünç işlemlerinin yönetimini basit ve anlaşılır bir arayüzle sağlar.

## 🚀 Proje Özellikleri

- **Kullanıcı Girişi:** SQL Server veritabanına bağlı olarak personel adı ve şifre ile giriş yapılabilir.
- **Kullanıcı Yönetimi:** Kullanıcılar eklenebilir, silinebilir ve güncellenebilir.
- **Kaynak Yönetimi:** Kitap/kaynaklar üzerinde ekleme, güncelleme ve silme işlemleri yapılabilir.
- **Ödünç Verme Sistemi:**
  - Kullanıcı ve kitap seçerek ödünç verme işlemi yapılabilir.
  - Ödünç alınan ve alınmamış kitaplar listelenir.
- **Geri Alma Sistemi:** Teslim edilen kitaplar işlenebilir.
- **Ceza Sistemi:**
  - Kitaplar 10 gün içerisinde teslim edilmelidir.
  - Geciken her gün için 3₺ ceza uygulanır.
  - 30₺ ceza limitini aşan kullanıcılar, kitap alamaz.
  - Ceza listesi ayrı bir panelde görüntülenebilir.

## 🛠 Kullanılan Teknolojiler

- **Dil:** C#
- **Veritabanı:** Microsoft SQL Server
- **Arayüz:** WinForms

## 📂 Kurulum

1. Bu repoyu klonlayın:
   ```bash
   git clone https://github.com/Silanur454/KutuphaneOtomasyonWinForm.git
