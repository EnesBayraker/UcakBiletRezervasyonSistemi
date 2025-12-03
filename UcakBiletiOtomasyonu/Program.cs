using System;
using System.Windows.Forms;

namespace UcakBiletiOtomasyonu
{
    static class Program
    {
       
        public static RezervasyonYoneticisi Yonetici = new RezervasyonYoneticisi();

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1()); // Program Form1 ile başlayacak
        }
    }
}