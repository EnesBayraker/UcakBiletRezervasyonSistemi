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
    public partial class FormAdmin : Form
    {
        public FormAdmin()
        {
            InitializeComponent();
            ListeyiGuncelle();

            // Polymorphism 

            // Sanki giriş yapmış bir Admin varmış gibi nesne oluşturuyoruz
            Admin aktifAdmin = new Admin { Ad = "Enes", Soyad = "Bayraker", TcNo = " -" };

            // pencerenin başlığına yazdır
            this.Text = aktifAdmin.BilgiDondur();

            // Admin hoşgeldin mesajı
            MessageBox.Show("Giriş Yapıldı: " + aktifAdmin.BilgiDondur());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string kalkis = textBox1.Text.Trim();
            string varis = textBox2.Text.Trim();
            string tarihText = textBox3.Text.Trim();
            decimal fiyat = 0;

            // sadece sayı girmek için uyarı
            if (!decimal.TryParse(textBox4.Text, out fiyat))
            {
                MessageBox.Show("Fiyat kısmına sayı giriniz.");
                return;
            }

            DateTime tarih;
            // Kullanıcıya saati de yazabileceğini hatırlatıyoruz
            if (!DateTime.TryParse(tarihText, out tarih))
            {
                MessageBox.Show("Lütfen tarihi ve saati düzgün giriniz!\nÖrnek: 24.10.2026 14:30");
                return;
            }

            // Geçmiş tarih kontrolü
            if (tarih < DateTime.Now)
            {
                MessageBox.Show("Hata: Geçmiş bir tarihe uçuş ekleyemezsiniz!");
                return; // İşlemi durdur, aşağıya inme
            }

            // Eksi fiyat kontrolü
            if (fiyat < 0)
            {
                MessageBox.Show("Hata: Fiyat 0'dan küçük olamaz!");
                return;
            }

            // Ekleme
            Program.Yonetici.UcusEkle(kalkis, varis, tarih, fiyat);
            MessageBox.Show("Uçuş Başarıyla Eklendi!");

            // Listeyi yenile
            ListeyiGuncelle();
        }

        void ListeyiGuncelle()
        {
            listBox1.Items.Clear();
            foreach (var u in Program.Yonetici.Ucuslar)
            {
                string tarihFormatli = u.TarihSaat.ToString("dd.MM.yyyy HH:mm");

                listBox1.Items.Add($"NO: {u.UcusNo} | {u.KalkisYeri} -> {u.VarisYeri} | {tarihFormatli} | Fiyat: {u.TabanFiyat} TL | Uçak: {u.AtananUcak.Model}");
            }
        }

        private void button2_Click(object sender, EventArgs e) // Geri butonu
        {
            Form1 anaMenu = new Form1();
            anaMenu.Show();
            this.Close();
        }

        private void FormAdmin_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null) return;

            // Satırdan Uçuş Numarasını alıyoruz
            // Formatımız: "NO: 100 
            string satir = listBox1.SelectedItem.ToString();
            string[] parcalar = satir.Split(' ');
            int ucusNo = Convert.ToInt32(parcalar[1]); // "100"ü aldık

            // Uçuşu bul
            var ucus = Program.Yonetici.UcusGetir(ucusNo);

            if (ucus != null)
            {
                // Kutuları dolduruyoruz
                textBox1.Text = ucus.KalkisYeri;
                textBox2.Text = ucus.VarisYeri;
                textBox3.Text = ucus.TarihSaat.ToString("dd.MM.yyyy HH:mm");
                textBox4.Text = ucus.TabanFiyat.ToString();

                
                // Biz Formun başlığına seçili uçuşu koyuyoruz
                this.Text = "Yönetici Paneli - Seçili Uçuş: " + ucusNo;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Önce listeden bir şey seçili mi diye kontrol edelim
            if (listBox1.SelectedItem == null)
            {
                MessageBox.Show("Lütfen listeden güncellenecek uçuşu seçin!");
                return;
            }

            // Seçili satırdan ID'yi bulalım
            string satir = listBox1.SelectedItem.ToString();
            string[] parcalar = satir.Split(' ');
            int ucusNo = Convert.ToInt32(parcalar[1]); // 100'ü aldık

            // O uçuşu sistemde bulalım
            var ucus = Program.Yonetici.UcusGetir(ucusNo);

            if (ucus != null)
            {
                // Kutulardaki YENİ verileri alalım
                string yeniKalkis = textBox1.Text.Trim();
                string yeniVaris = textBox2.Text.Trim();
                string yeniTarihText = textBox3.Text.Trim();
                decimal yeniFiyat = 0;

                // Fiyat ve Tarih Kontrolü
                if (!decimal.TryParse(textBox4.Text, out yeniFiyat))
                {
                    MessageBox.Show("Fiyat hatalı!");
                    return;
                }

                DateTime yeniTarih;
                if (!DateTime.TryParse(yeniTarihText, out yeniTarih))
                {
                    MessageBox.Show("Tarih hatalı! (Örn: 24.10.2026 15:00)");
                    return;
                }

                // Değerleri güncelliyoruz 
                ucus.KalkisYeri = yeniKalkis;
                ucus.VarisYeri = yeniVaris;
                ucus.TarihSaat = yeniTarih;
                ucus.TabanFiyat = yeniFiyat;

                MessageBox.Show("Uçuş Bilgileri Güncellendi!");

                // Listeyi yenile
                ListeyiGuncelle();

                // Kutuları temizle
                textBox1.Text = ""; textBox2.Text = ""; textBox3.Text = ""; textBox4.Text = "";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}