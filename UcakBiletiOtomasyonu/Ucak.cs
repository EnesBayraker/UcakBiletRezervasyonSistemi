using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UcakBiletiOtomasyonu
{
    public class Ucak
    {
        public string Model { get; set; }
        public string SeriNo { get; set; }
        public int Kapasite { get; set; }

        public Ucak(string model, string seriNo, int kapasite)
        {
            Model = model;
            SeriNo = seriNo;
            Kapasite = kapasite;

        }
    }
}