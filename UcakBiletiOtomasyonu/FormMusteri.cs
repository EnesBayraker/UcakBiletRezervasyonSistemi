using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UcakBiletiOtomasyonu
{
    public partial class FormMusteri : Form
    {
        public FormMusteri()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Listeleri Temizle
            listBox1.Items.Clear();
            comboKoltuk.Items.Clear();
            comboKoltuk.Text = "";

            // Kutulardaki yazıları al, boşluklarını sil, hepsini küçük harfe çevir
            string nereden = textBox1.Text.Trim().ToLower();
            string nereye = textBox2.Text.Trim().ToLower();

            var sonuclar = Program.Yonetici.UcusAra(nereden, nereye);

            foreach (var u in sonuclar)
            {
                string tarih = u.TarihSaat.ToString("dd.MM.yyyy HH:mm");
                decimal guncelFiyat = u.FiyatHesapla();
                int bosSayisi = u.BosKoltuklar.Count;

                listBox1.Items.Add($"NO: {u.UcusNo} | {u.KalkisYeri} -> {u.VarisYeri} | {tarih} | {guncelFiyat} TL | Müsait: {bosSayisi} Koltuk");
            }

            // Sonuç Yoksa
            if (listBox1.Items.Count == 0)
            {
                // Kullanıcıya uyarı
                MessageBox.Show($"Aranan: '{nereden}' -> '{nereye}'\n\nMaalesef uygun uçuş bulunamadı.\n(İpucu: 'Tümünü Listele' diyerek şehir isimlerini kontrol edin.)");
            }
        }
        

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Uçuş No, Koltuk, Ad ve Soyad girilmiş mi?
            // (Telefon opsiyonel )
            if (textBox3.Text == "" || comboKoltuk.Text == "" || txtAd.Text == "" || txtSoyad.Text == "")
            {
                MessageBox.Show("Lütfen Ad, Soyad ve Koltuk seçimini eksiksiz yapınız!");
                return;
            }

            int ucusNo = Convert.ToInt32(textBox3.Text);

            // Uçuşu sistemde bul
            var ucus = Program.Yonetici.UcusGetir(ucusNo);

            if (ucus == null)
            {
                MessageBox.Show("Uçuş bulunamadı!");
                return;
            }

            // Doluluk kontrolü
            if (ucus.BosKoltuklar.Count == 0)
            {
                MessageBox.Show("Üzgünüz, bu uçak tamamen DOLDU! Bilet alamazsınız.", "Kapasite Dolu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string koltuk = comboKoltuk.Text;

            // kutulardaki gerçek verileri sınıfa yüklüyoruz
            Musteri m = new Musteri();
            m.Ad = txtAd.Text.Trim();
            m.Soyad = txtSoyad.Text.Trim();
            m.Telefon = txtTel.Text.Trim(); 

            
            if (Program.Yonetici.RezervasyonYap(m, ucusNo, koltuk, out var yeniRez))
            {
                // Kullanıcının ismine özel mesaj veriyoruz
                MessageBox.Show($"Sayın {m.Ad} {m.Soyad}, biletleme işleminiz başarılı!\nÖdediğiniz tutar: {yeniRez.OdenenTutar} TL\nİyi uçuşlar dileriz.");

                // Seçilen koltuğu listeden siliyoruz ki tekrar seçilmesin
                comboKoltuk.Items.Remove(koltuk);
                comboKoltuk.Text = "";
            }
            else
            {
                MessageBox.Show("Hata: Bu koltuk az önce başkası tarafından alındı veya geçersiz.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Geri Dön butonu
            FormMusteriMenu menu = new FormMusteriMenu();
            menu.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Listeyi temizle
            listBox1.Items.Clear();
            comboKoltuk.Items.Clear();
            comboKoltuk.Text = "";

            foreach (var u in Program.Yonetici.UcusAra(string.Empty, string.Empty))
            {
                string tarih = u.TarihSaat.ToString("dd.MM.yyyy HH:mm");
                decimal guncelFiyat = u.FiyatHesapla();
                int bosSayisi = u.BosKoltuklar.Count;

               
                listBox1.Items.Add($"NO: {u.UcusNo} | {u.KalkisYeri} -> {u.VarisYeri} | {tarih} | {guncelFiyat} TL | Müsait: {bosSayisi} Koltuk");
            }

            if (listBox1.Items.Count == 0) MessageBox.Show("Sistemde uçuş yok.");
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboKoltuk_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Seçili satırı al
            if (listBox1.SelectedItem == null) return;
            string seciliSatir = listBox1.SelectedItem.ToString();

            // Satırdan "NO: 100" kısmını ayıklayıp Uçuş Numarasını buluyoruz
            // Metni parçalayıp boşluklara göre ayırıyoruz.
            // 2. elemanı alıyoruz yani sayıyı
            string[] parcalar = seciliSatir.Split(' ');
            int ucusNo = Convert.ToInt32(parcalar[1]);

            // Uçuşu bul
            var ucus = Program.Yonetici.UcusGetir(ucusNo);

            // Koltukları ComboBox'a doldur
            comboKoltuk.Items.Clear();
            if (ucus != null)
            {
                foreach (string k in ucus.BosKoltuklar)
                {
                    comboKoltuk.Items.Add(k);
                }
            }

            // Uçuş No kutusuna yazdıralım:
            textBox3.Text = ucusNo.ToString();
        }

        private void FormMusteri_Load(object sender, EventArgs e)
        {

        }
    }
}
