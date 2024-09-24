using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Yolcu_Bilet_Rezervasyon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-32Q9FH5;Initial Catalog=DbYolcuBilet;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;");

        void Cinsiyet(Button id)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("SELECT CINSIYET FROM TBLYOLCUBİLGİ WHERE TC = @tc", baglanti);
            komut.Parameters.AddWithValue("@tc", msksefertc.Text);

            SqlDataReader dr = komut.ExecuteReader();

            if (dr.Read())
            {
                string cinsiyet = dr["CINSIYET"].ToString(); // Cinsiyeti al

                if (cinsiyet == "Erkek")
                {
                    id.BackColor = Color.Blue; // Erkek için mavi
                }
                else if (cinsiyet == "Kadın")
                {
                    id.BackColor = Color.Pink; // Kadın için pembe
                }
            }

            dr.Close();
            baglanti.Close();
        }


        void seferlistesi()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * From TBLSEFERBILGI", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        void yolculistesi()
        {

            SqlDataAdapter da = new SqlDataAdapter("Select SEFERNO, YOLCUTC, KOLTUK from TBLSEFERDETAY", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }
        void sefersay()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select COUNT(SEFERNO) from TBLSEFERBILGI ", baglanti);
            int toplam = (int)komut.ExecuteScalar(); // sefer sayısı
            lblsefersayisi.Text = toplam + "Adet Sefer Mevcut";
            baglanti.Close();
        }
        void yolcusay()
        {

            baglanti.Open();
            SqlCommand komut = new SqlCommand(" Select COUNT(YOLCUTC) from TBLSEFERDETAY", baglanti);
            int toplam = (int)komut.ExecuteScalar(); // yolcu sayısı
            lblyolcusayisi.Text = toplam + "Adet Yolcu Mevcut";
            baglanti.Close();
        }

        void DoluBos(Button id, string koltuk)
        {
            baglanti.Open();

            SqlCommand komut = new SqlCommand(
            "SELECT Y.CINSIYET FROM TBLSEFERDETAY AS S INNER JOIN TBLYOLCUBİLGİ AS Y ON Y.TC = S.YOLCUTC  WHERE S.SEFERNO = @p1 AND S.KOLTUK = @p2", baglanti);

            komut.Parameters.AddWithValue("@p1", txtsefernumara.Text);
            komut.Parameters.AddWithValue("@p2", koltuk);

            SqlDataReader dr = komut.ExecuteReader();

            if (dr.Read())
            {
                string cinsiyet = dr["CINSIYET"].ToString();

                if (cinsiyet == "Erkek")
                {
                    id.BackColor = Color.Blue;
                    id.Enabled = false;
                }
                else if (cinsiyet == "Kadın")
                {
                    id.BackColor = Color.Pink;
                    id.Enabled = false;
                }
            }
            else
            {
                id.BackColor = Color.Transparent;
                id.Enabled = true;
            }

            dr.Close();
            baglanti.Close();
        }

        void koltuk()
        {

            DoluBos(btn1, "1");
            DoluBos(btn2, "2");
            DoluBos(btn3, "3");
            DoluBos(btn4, "4");
            DoluBos(btn5, "5");
            DoluBos(btn6, "6");
            DoluBos(btn7, "7");
            DoluBos(btn8, "8");
            DoluBos(btn9, "9");
            DoluBos(btn10, "10");
            DoluBos(btn11, "11");
            DoluBos(btn12, "12");
            DoluBos(btn13, "13");
            DoluBos(btn14, "14");
            DoluBos(btn15, "15");
            DoluBos(btn16, "16");
            DoluBos(btn17, "17");
            DoluBos(btn18, "18");
            DoluBos(btn19, "19");
        }



        private void btnyolcukaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TBLYOLCUBİLGİ (AD,SOYAD,TELEFON,TC,CINSIYET,MAIL) values (@p1,@p2,@p3,@p4,@p5,@p6)", baglanti);
            komut.Parameters.AddWithValue("@p1", txtyolcuad.Text);
            komut.Parameters.AddWithValue("@p2", txtyolcusoyad.Text);
            komut.Parameters.AddWithValue("@p3", mskyolcutelefon.Text);
            komut.Parameters.AddWithValue("@p4", mskyolcutc.Text);
            komut.Parameters.AddWithValue("@p5", cmbyolcucinsiyet.Text);
            komut.Parameters.AddWithValue("@p6", txtyolcumail.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Yolcu Bilgisi Kaydedilmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnkaptankaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TBLKAPTAN (KAPTANNO,ADSOYAD,TELEFON) values (@p1,@p2,@p3)", baglanti);
            komut.Parameters.AddWithValue("@p1", txtkaptanno.Text);
            komut.Parameters.AddWithValue("@p2", txtkaptanadsoyad.Text);
            komut.Parameters.AddWithValue("@p3", mskkaptantelefon.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kaptan Bilgisi Kaydedilmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnseferolustur_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TBLSEFERBILGI (KALKIS,VARIS,TARIH,SAAT,KAPTAN,FIYAT) values (@p1,@p2,@p3,@p4,@p5,@p6)", baglanti);
            komut.Parameters.AddWithValue("@p1", txtseferkalkis.Text);
            komut.Parameters.AddWithValue("@p2", txtsefervaris.Text);
            komut.Parameters.AddWithValue("@p3", msksefertarih.Text);
            komut.Parameters.AddWithValue("@p4", msksefersaat.Text);
            komut.Parameters.AddWithValue("@p5", mskseferkaptan.Text);
            komut.Parameters.AddWithValue("@p6", txtseferfiyat.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            seferlistesi();
            sefersay();
            MessageBox.Show("Sefer Bilgisi Kaydedilmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            seferlistesi();
            sefersay();
            yolculistesi();
            yolcusay();
            koltuk();



        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtsefernumara.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            koltuk();

        }

        private void btnrezervasyonyap_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand kontrolKomut = new SqlCommand("SELECT COUNT(*) FROM TBLYOLCUBİLGİ WHERE TC = @tc", baglanti);
            kontrolKomut.Parameters.AddWithValue("@tc", msksefertc.Text);
            int yolcuSayisi = (int)kontrolKomut.ExecuteScalar();

            if (yolcuSayisi > 0)
            {
                // Aynı seferden rezervasyon yapıp yapmadığını kontrol et
                SqlCommand rezervasyonKontrol = new SqlCommand("SELECT KOLTUK FROM TBLSEFERDETAY WHERE SEFERNO = @seferno AND YOLCUTC = @yolcutc", baglanti);
                rezervasyonKontrol.Parameters.AddWithValue("@seferno", txtsefernumara.Text);
                rezervasyonKontrol.Parameters.AddWithValue("@yolcutc", msksefertc.Text);

                SqlDataReader dr = rezervasyonKontrol.ExecuteReader();

                if (dr.HasRows) // Daha önce rezervasyon yapılmışsa
                {
                    string koltuk = "";
                    while (dr.Read())
                    {
                        koltuk += dr["KOLTUK"].ToString() + ", ";
                    }
                    dr.Close();
                    koltuk = koltuk.TrimEnd(',', ' ');

                    MessageBox.Show($"Bu yolcu zaten bu sefer için rezervasyon yapmıştır. Rezervasyon yapılan koltuk: {koltuk}", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {

                    dr.Close();
                    SqlCommand komut = new SqlCommand("INSERT INTO TBLSEFERDETAY (SEFERNO, YOLCUTC, KOLTUK) VALUES (@seferno, @yolcutc, @koltuk)", baglanti);
                    komut.Parameters.AddWithValue("@seferno", txtsefernumara.Text);
                    komut.Parameters.AddWithValue("@yolcutc", msksefertc.Text);
                    komut.Parameters.AddWithValue("@koltuk", txtrezervasonkoltukno.Text);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Rezervasyon Yapılmıştır.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            else
            {
                MessageBox.Show("Girilen TC no yolcu listesinde bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            baglanti.Close();
            txtrezervasonkoltukno.Text = "";
            msksefertc.Text = "";
            txtsefernumara.Text = "";
            seferlistesi();
            sefersay();
            yolculistesi();
            yolcusay();
            koltuk();


        }

        private void btn1_Click(object sender, EventArgs e)
        {
            txtrezervasonkoltukno.Text = "1";
            Cinsiyet(btn1);
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            txtrezervasonkoltukno.Text = "2";
            Cinsiyet(btn2);
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            txtrezervasonkoltukno.Text = "3";
            Cinsiyet(btn3);
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            txtrezervasonkoltukno.Text = "4";
            Cinsiyet(btn4);
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            txtrezervasonkoltukno.Text = "5";
            Cinsiyet(btn5);
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            txtrezervasonkoltukno.Text = "6";
            Cinsiyet(btn6);
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            txtrezervasonkoltukno.Text = "7";
            Cinsiyet(btn7);
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            txtrezervasonkoltukno.Text = "8";
            Cinsiyet(btn8);
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            txtrezervasonkoltukno.Text = "9";
            Cinsiyet(btn9);
        }

        private void btn10_Click(object sender, EventArgs e)
        {
            txtrezervasonkoltukno.Text = "10";
            Cinsiyet(btn10);
        }

        private void btn11_Click(object sender, EventArgs e)
        {
            txtrezervasonkoltukno.Text = "11";
            Cinsiyet(btn11);
        }

        private void btn12_Click(object sender, EventArgs e)
        {
            txtrezervasonkoltukno.Text = "12";
            Cinsiyet(btn12);
        }

        private void btn13_Click(object sender, EventArgs e)
        {
            txtrezervasonkoltukno.Text = "13";
            Cinsiyet(btn13);
        }

        private void btn14_Click(object sender, EventArgs e)
        {
            txtrezervasonkoltukno.Text = "14";
            Cinsiyet(btn14);
        }

        private void btn15_Click(object sender, EventArgs e)
        {
            txtrezervasonkoltukno.Text = "15";
            Cinsiyet(btn15);
        }

        private void btn16_Click(object sender, EventArgs e)
        {
            txtrezervasonkoltukno.Text = "16";
            Cinsiyet(btn16);
        }

        private void btn17_Click(object sender, EventArgs e)
        {
            txtrezervasonkoltukno.Text = "17";
            Cinsiyet(btn17);
        }

        private void btn18_Click(object sender, EventArgs e)
        {
            txtrezervasonkoltukno.Text = "18";
            Cinsiyet(btn18);
        }

        private void btn19_Click(object sender, EventArgs e)
        {
            txtrezervasonkoltukno.Text = "19";
            Cinsiyet(btn19);
        }
    }
}

