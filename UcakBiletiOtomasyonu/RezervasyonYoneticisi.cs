using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UcakBiletiOtomasyonu
{
    public class RezervasyonYoneticisi
    {
        // / VERİ YÖNETİMİ
        // Vize Aşaması: Veriler çalışma zamanında RAM üzerinde List<> koleksiyonlarında tutuluyor şuan
        // TODO Final: Veri kalıcılığı için bu listeler JSON dosyasına yazılacak veya SQL veritabanına bağlanacak.
        public List<Ucus> Ucuslar { get; set; } = new List<Ucus>();
        public List<Rezervasyon> Rezervasyonlar { get; set; } = new List<Rezervasyon>();
        public List<Ucak> Ucaklar { get; set; } = new List<Ucak>();

        // kurucu metod
        public RezervasyonYoneticisi()
        {
            // Uçaklar
            Ucak buyukUcak = new Ucak("Boeing 737", "TC-THY", 50);
            Ucak kucukUcak = new Ucak("Cesna 172", "TC-MINI", 2); // 2 Kişilik

            Ucaklar.Add(buyukUcak);
            Ucaklar.Add(kucukUcak);

            //Fiyat testi uçuşu
            UcusEkle("Izmir", "Antalya", DateTime.Now.AddDays(5), buyukUcak, 800);

            // kapasite testi uçuşu
            UcusEkle("Bursa", "Canakkale", DateTime.Now.AddDays(2), kucukUcak, 500);

            // Normal Uçuş
            UcusEkle("Istanbul", "Ankara", DateTime.Now.AddDays(1), buyukUcak, 1200);
        }


        // Admin Panelinden Gelen Ekleme
        public void UcusEkle(string kalkis, string varis, DateTime tarih, decimal fiyat)
        {
            // Admin panelinde uçak seçimi yoksa ilk uçağı kullanırız
            Ucak varsayilan = Ucaklar[0];
            Ucuslar.Add(new Ucus(kalkis, varis, tarih, varsayilan, fiyat));
        }

        // kod içinde özel uçak tipiyle ekleme yapma 
        public void UcusEkle(string kalkis, string varis, DateTime tarih, Ucak ucak, decimal fiyat)
        {
            Ucuslar.Add(new Ucus(kalkis, varis, tarih, ucak, fiyat));
        }

        //LINQ
        public IEnumerable<Ucus> UcusAra(string nereden, string nereye, DateTime? tarih = null)
        {
            string kalkisArama = (nereden ?? string.Empty).Trim().ToLower();
            string varisArama = (nereye ?? string.Empty).Trim().ToLower();

            return Ucuslar
                .Where(u =>
                    (string.IsNullOrEmpty(kalkisArama) || u.KalkisYeri.Trim().ToLower().Contains(kalkisArama)) &&
                    (string.IsNullOrEmpty(varisArama) || u.VarisYeri.Trim().ToLower().Contains(varisArama)) &&
                    (!tarih.HasValue || u.TarihSaat.Date == tarih.Value.Date))
                .OrderBy(u => u.TarihSaat)
                .ToList();
        }

        public Ucus UcusGetir(int ucusNo)
        {
            return Ucuslar.FirstOrDefault(u => u.UcusNo == ucusNo);
        }

        public bool RezervasyonYap(Musteri musteri, int ucusNo, string secilenKoltuk, out Rezervasyon yeniRezervasyon)
        {
            yeniRezervasyon = null;
            var ucus = UcusGetir(ucusNo);

            if (ucus == null) return false;

            // Koltuk kontrolü
            if (!ucus.BosKoltuklar.Contains(secilenKoltuk)) return false;

            // Koltuğu boştan çıkar
            ucus.BosKoltuklar.Remove(secilenKoltuk);

            decimal odenenTutar = ucus.FiyatHesapla();

            // Kaydet
            yeniRezervasyon = new Rezervasyon(musteri, ucus, secilenKoltuk, odenenTutar);
            Rezervasyonlar.Add(yeniRezervasyon);
            return true;
        }
            
        public bool RezervasyonIptal(string pnr)
        {
            var rez = Rezervasyonlar.FirstOrDefault(r => r.PNR == pnr);

            if (rez != null && !rez.IptalDurumu)
            {
                rez.IptalDurumu = true;

                // İptal edilen koltuğu geri listeye ekle
                rez.SecilenUcus.BosKoltuklar.Add(rez.KoltukNo);
                rez.SecilenUcus.BosKoltuklar.Sort();
                return true;
            }

            return false;
        }
    }
}