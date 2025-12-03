using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UcakBiletiOtomasyonu
{
    public class Rezervasyon
    {
        //Encapsulation
        public string PNR { get; }
        public Musteri Yolcu { get; }
        public Ucus SecilenUcus { get; }
        public bool IptalDurumu { get; set; }
        public string KoltukNo { get; }
        public decimal OdenenTutar { get; }
        public DateTime OlusturmaZamani { get; }

        public Rezervasyon(Musteri yolcu, Ucus ucus, string koltukNo, decimal odenenTutar)
        {
            PNR = Guid.NewGuid().ToString().Substring(0, 6).ToUpper();
            Yolcu = yolcu;
            SecilenUcus = ucus;
            KoltukNo = koltukNo;
            OdenenTutar = odenenTutar;
            OlusturmaZamani = DateTime.Now;
            IptalDurumu = false;
        }
    }
}