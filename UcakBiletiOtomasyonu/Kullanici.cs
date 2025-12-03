using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UcakBiletiOtomasyonu
{
    //ABSTRACTION
    // Abstract Sınıf: Tek başına new Kullanici() denilemez.
    public abstract class Kullanici
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string TcNo { get; set; }

        //virtual method ki polymorphsimde kullanabilelim 
        public virtual string BilgiDondur() 
        {
            return $"{Ad} {Soyad}";
        }
    }
}