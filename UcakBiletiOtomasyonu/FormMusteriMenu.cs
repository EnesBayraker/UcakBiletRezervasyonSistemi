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
    public partial class FormMusteriMenu : Form
    {
        public FormMusteriMenu()
        {
            InitializeComponent();
        }

        // Bilet al Butonu
        private void button1_Click(object sender, EventArgs e)
        {
            // Var olan arama ekranına gönderiyoruz
            FormMusteri aramaEkrani = new FormMusteri();
            aramaEkrani.Show();
            this.Hide();
        }

        // Rezervasyonlarım Butonu
        private void button2_Click(object sender, EventArgs e)
        {
            
            FormRezervasyonlarim rezEkrani = new FormRezervasyonlarim();
            rezEkrani.Show();
            this.Hide();
        }

        // Ana menüye dön butonu
        private void button3_Click(object sender, EventArgs e)
        {
            Form1 anaMenu = new Form1();
            anaMenu.Show();
            this.Close();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            // Giriş ekranını açıyoruz
            Form1 giris = new Form1();
            giris.Show();

            // Bu menüyü kapatıyoruz
            this.Close();
        }

        private void FormMusteriMenu_Load(object sender, EventArgs e)
        {

        }
    }
}