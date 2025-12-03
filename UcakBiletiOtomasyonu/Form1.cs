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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Yönetici giriş butonum
        
        private void button1_Click(object sender, EventArgs e)
        {
            // Güvenlik kontrolü

            
            Form sifreEkrani = new Form();
            sifreEkrani.Width = 300;
            sifreEkrani.Height = 150;
            sifreEkrani.Text = "Yönetici Doğrulama";
            sifreEkrani.StartPosition = FormStartPosition.CenterScreen;
            sifreEkrani.FormBorderStyle = FormBorderStyle.FixedDialog;
            sifreEkrani.MaximizeBox = false;
            sifreEkrani.MinimizeBox = false;

            Label etiket = new Label() { Left = 20, Top = 20, Text = "Yönetici Şifresi:" };
            TextBox kutu = new TextBox() { Left = 120, Top = 20, Width = 150, PasswordChar = '*' }; 
            Button buton = new Button() { Left = 100, Top = 60, Width = 100, Text = "Giriş Yap", DialogResult = DialogResult.OK };
            sifreEkrani.AcceptButton = buton; // Enter'a basınca buton çalışsın

            sifreEkrani.Controls.Add(etiket);
            sifreEkrani.Controls.Add(kutu);
            sifreEkrani.Controls.Add(buton);

            // Pencereyi göster ve sonucu bekle
            if (sifreEkrani.ShowDialog() == DialogResult.OK)
            {
                // Şifre Kontrolü
                if (kutu.Text == "1234")
                {
                    // Şifre doğruysa Admin ekranını aç
                    FormAdmin adminEkrani = new FormAdmin();
                    adminEkrani.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Hatalı şifre! Giriş reddedildi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Müşteri girişi
            FormMusteriMenu menu = new FormMusteriMenu();
            menu.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}