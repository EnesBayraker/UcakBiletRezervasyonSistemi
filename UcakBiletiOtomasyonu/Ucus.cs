using System;
using System.Collections.Generic;

namespace UcakBiletiOtomasyonu
{
    public class Ucus
    {
        private static int _sayac = 100;

        // Bu uçuş için gerçekte kaç koltuk oluşturulduğunu tutacak değişken
        private int _olusturulanKoltukKapasitesi;

        public int UcusNo { get; private set; }
        public string KalkisYeri { get; set; }
        public string VarisYeri { get; set; }
        public DateTime TarihSaat { get; set; }
        public Ucak AtananUcak { get; set; }
        public decimal TabanFiyat { get; set; }
        public List<string> BosKoltuklar { get; set; }

        public int DoluKoltukSayisi
        {
            get
            {               
                return _olusturulanKoltukKapasitesi - BosKoltuklar.Count;
            }
        }

        public Ucus(string kalkis, string varis, DateTime tarih, Ucak ucak, decimal fiyat)
        {
            UcusNo = _sayac++;
            KalkisYeri = kalkis;
            VarisYeri = varis;
            TarihSaat = tarih;
            AtananUcak = ucak;
            TabanFiyat = fiyat;

            BosKoltuklar = new List<string>();

            int siraSayisi = ucak.Kapasite / 4;
            string[] harfler = { "A", "B", "C", "D" };

            for (int i = 1; i <= siraSayisi; i++)
            {
                foreach (string harf in harfler)
                {
                    BosKoltuklar.Add(i + harf);
                }
            }

        
            _olusturulanKoltukKapasitesi = BosKoltuklar.Count;
        }

        // TODO Final Buraya uçuşa kalan gün bazlı ve sezonluk fiyat değişimleri eklenecek.
        public decimal FiyatHesapla()
        {
            // Eğer 2 veya daha fazla koltuk satıldıysa zam yap
            if (DoluKoltukSayisi >= 2)
            {
                return TabanFiyat * 1.5m;
            }

            return TabanFiyat;
        }
    }
}