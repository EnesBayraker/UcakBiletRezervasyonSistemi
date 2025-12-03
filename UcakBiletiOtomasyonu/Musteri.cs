    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace UcakBiletiOtomasyonu
    {
        public class Musteri : Kullanici
        {
            public string Telefon { get; set; } = "";


        //POLYMORPHISM
        public override string BilgiDondur()
        {
            // base.BilgiDondur() -> Üst sınıftaki Ad Soyad'ı alır
            return $"[MÜŞTERİ]: " + base.BilgiDondur() + $" - Tel: {Telefon}";
        }
    }
    }