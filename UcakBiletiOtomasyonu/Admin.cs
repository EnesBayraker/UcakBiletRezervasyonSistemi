using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UcakBiletiOtomasyonu
{
    // Inheritance: Admin aynı zamanda bir Kullanıcıdır.
    public class Admin : Kullanici
    {
        //POLYMORPHISM
        public override string BilgiDondur()
        {
            return $"[YÖNETİCİ]: {Ad} {Soyad} (Sistem Yetkili)";
        }
    }
}