using System;
using System.Windows.Forms;

namespace UcakBiletiOtomasyonu
{
    public partial class FormRezervasyonlarim : Form
    {
        public FormRezervasyonlarim()
        {
            InitializeComponent();

            if (dataGridView1.Columns.Count == 0) 
            {
                dataGridView1.ColumnCount = 8;
                dataGridView1.Columns[0].Name = "Durum";
                dataGridView1.Columns[1].Name = "PNR";
                dataGridView1.Columns[2].Name = "Yolcu Adı";
                dataGridView1.Columns[3].Name = "Telefon";
                dataGridView1.Columns[4].Name = "Rota";
                dataGridView1.Columns[5].Name = "Koltuk";
                dataGridView1.Columns[6].Name = "Tutar";
                dataGridView1.Columns[7].Name = "Oluşturma";

                // Tablo Ayarları
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Ekrana yay
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Satır seç
                dataGridView1.ReadOnly = true; // Sadece okunur
                dataGridView1.AllowUserToAddRows = false; // Boş satır eklemesin
                dataGridView1.RowHeadersVisible = false;
            }

            RezervasyonlariListele();
        }

        private void FormRezervasyonlarim_Load(object sender, EventArgs e) { }

        void RezervasyonlariListele()
        {
            // Tabloyu temizle
            dataGridView1.Rows.Clear();

            foreach (var r in Program.Yonetici.Rezervasyonlar)
            {
                string durum = r.IptalDurumu ? "İPTAL" : "AKTİF";

                // Güvenli Yolcu Bilgisi
                string adSoyad = "Misafir";
                string tel = "-";
                if (r.Yolcu != null)
                {
                    adSoyad = $"{r.Yolcu.Ad} {r.Yolcu.Soyad}";
                    tel = r.Yolcu.Telefon;
                }

                // Güvenli Uçuş Bilgisi
                string rota = "Belirsiz";
                if (r.SecilenUcus != null)
                {
                    rota = $"{r.SecilenUcus.KalkisYeri}-{r.SecilenUcus.VarisYeri}";
                }

                // tabloya ekle
                string tutar = $"{r.OdenenTutar:0.##} TL";
                string olusturma = r.OlusturmaZamani.ToString("dd.MM.yyyy HH:mm");

                dataGridView1.Rows.Add(durum, r.PNR, adSoyad, tel, rota, r.KoltukNo, tutar, olusturma);
            }
        }

        // geri dön butonu
        private void button3_Click(object sender, EventArgs e) 
        {
            FormMusteriMenu menu = new FormMusteriMenu();
            menu.Show();
            this.Close();
        }

        // İptal et butonu
        private void button4_Click(object sender, EventArgs e) 
        {
            // Tablodan seçim yapılmış mı?
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen iptal etmek için tablodan bir satır seçin.");
                return;
            }

            // Seçili satırı al
            DataGridViewRow seciliSatir = dataGridView1.SelectedRows[0];

            // PNR kodunu direkt sütundan alıyoruz 
            string pnr = seciliSatir.Cells["PNR"].Value.ToString();
            string durum = seciliSatir.Cells["Durum"].Value.ToString();

            if (durum == "İPTAL")
            {
                MessageBox.Show("Bu bilet zaten iptal edilmiş.");
                return;
            }

            DialogResult cevap = MessageBox.Show($"PNR: {pnr} olan bileti iptal etmek istiyor musunuz?",
                                                 "İptal Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (cevap == DialogResult.Yes)
            {
                if (Program.Yonetici.RezervasyonIptal(pnr))
                {
                    MessageBox.Show("Bilet iptal edildi.");
                    RezervasyonlariListele(); // Tabloyu yenile
                }
                else
                {
                    MessageBox.Show("İptal işlemi başarısız oldu. Kayıt bulunamadı.");
                }
            }
        }
    }
}