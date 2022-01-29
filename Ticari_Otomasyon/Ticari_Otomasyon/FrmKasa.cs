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
using DevExpress.Charts;

namespace Ticari_Otomasyon
{
    public partial class FrmKasa : Form
    {
        public FrmKasa()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void musterihareket()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Execute MusteriHareketler", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void firmahareket()
        {
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Execute FirmaHareketler", bgl.baglanti());
            da2.Fill(dt2);
            labelsehir.DataSource = dt2;
        }

        void giderler()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From TBL_GIDERLER ORDER BY ID", bgl.baglanti());
            da.Fill(dt);
            gridControl2.DataSource = dt;
        }

        public string ad;
        private void FrmKasa_Load(object sender, EventArgs e)
        {
            lblAktifKullanici.Text = ad;
            musterihareket();
            firmahareket();
            giderler();

            // Toplam Tutar Hesaplama 
            SqlCommand komut1 = new SqlCommand("SELECT SUM(TUTAR) FROM TBL_FATURADETAY", bgl.baglanti());
            SqlDataReader dr1 = komut1.ExecuteReader();
            while(dr1.Read())
            {
                lblTutar.Text = dr1[0].ToString() + " TL";
            }
            bgl.baglanti().Close();

            // Son Ayın Faturaları

            SqlCommand komut2 = new SqlCommand("SELECT (ELEKTRIK+SU+DOGALGAZ+INTERNET+EKSTRA) FROM TBL_GIDERLER ORDER BY ID ASC ", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while(dr2.Read())
            {
                lblOdemeler.Text = dr2[0].ToString() + " TL";
            }
            bgl.baglanti().Close();

            // Son Ayın Personel Maaşları

            SqlCommand komut3 = new SqlCommand("SELECT MAASLAR FROM TBL_GIDERLER", bgl.baglanti());
            SqlDataReader dr3 = komut3.ExecuteReader();
            while(dr3.Read())
            {
                lblPersonelMaas.Text = dr3[0].ToString() +" TL";
            }
            bgl.baglanti().Close();

            // Toplam Müşteri Sayısı

            SqlCommand komut4 = new SqlCommand("SELECT COUNT(*) FROM TBL_MUSTERILER", bgl.baglanti());
            SqlDataReader dr4 = komut4.ExecuteReader();
            while (dr4.Read())
            {
                lblMusterisayisi.Text = dr4[0].ToString();
            }
            bgl.baglanti().Close();

            // Toplam Firma Sayısı

            SqlCommand komut5 = new SqlCommand("SELECT COUNT(*) FROM TBL_FIRMALAR", bgl.baglanti());
            SqlDataReader dr5 = komut5.ExecuteReader();
            while (dr5.Read())
            {
                lblFirmaSayisi.Text = dr5[0].ToString();
            }
            bgl.baglanti().Close();

            // Toplam Firma Şehir Sayısı

            SqlCommand komut6 = new SqlCommand("SELECT COUNT(DISTINCT(IL)) FROM TBL_FIRMALAR", bgl.baglanti());
            SqlDataReader dr6 = komut6.ExecuteReader();
            while (dr6.Read())
            {
                lblSehirSayisi.Text = dr6[0].ToString();
            }
            bgl.baglanti().Close();

            // Toplam Müşteri Şehir Sayısı

            SqlCommand komut8 = new SqlCommand("SELECT COUNT(DISTINCT(IL)) FROM TBL_MUSTERILER", bgl.baglanti());
            SqlDataReader dr8 = komut8.ExecuteReader();
            while (dr8.Read())
            {
                lblMusteriSehirSayisi.Text = dr8[0].ToString();
            }
            bgl.baglanti().Close();


            // Toplam Personel Sayısı

            SqlCommand komut7 = new SqlCommand("SELECT COUNT(*) FROM TBL_PERSONELLER", bgl.baglanti());
            SqlDataReader dr7 = komut7.ExecuteReader();
            while (dr7.Read())
            {
                lblPersonelSayisi.Text = dr7[0].ToString();
            }
            bgl.baglanti().Close();

            // Toplam Ürün Sayısı

            SqlCommand komut9 = new SqlCommand("SELECT SUM(ADET) FROM TBL_URUNLER", bgl.baglanti());
            SqlDataReader dr9 = komut9.ExecuteReader();
            while (dr9.Read())
            {
                stokSayisi.Text = dr9[0].ToString();
            }
            bgl.baglanti().Close();

        }


        int sayac = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            sayac++;

            // Elektrik
            if (sayac > 0 && sayac <= 5)
            {
                groupControl9.Text = "Elektrik";
                chartControl1.Series["Aylar"].Points.Clear();
                
                SqlCommand komut10 = new SqlCommand("Select top 4 AY,ELEKTRIK from TBL_GIDERLER order by ID DESC", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();
            }

            // Su
            if (sayac > 5 && sayac <= 10)
            {
                groupControl9.Text = "Su";
                chartControl1.Series["Aylar"].Points.Clear();
                
                SqlCommand komut11 = new SqlCommand("Select top 4 AY,SU from TBL_GIDERLER order by ID DESC", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();

            }
            
            // Doğalgaz
            if (sayac > 10 && sayac <= 15)
            {
                groupControl9.Text = "Doğalgaz";
                chartControl1.Series["Aylar"].Points.Clear();

                SqlCommand komut11 = new SqlCommand("Select top 4 AY,DOGALGAZ from TBL_GIDERLER order by ID DESC", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }

            // İnternet
            if (sayac > 15 && sayac <= 20)
            {
                groupControl9.Text = "İnternet";
                chartControl1.Series["Aylar"].Points.Clear();

                SqlCommand komut11 = new SqlCommand("Select top 4 AY,INTERNET from TBL_GIDERLER order by ID DESC", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }

            // Ekstra
            if (sayac > 20 && sayac <= 25)
            {
                groupControl9.Text = "Ekstra";
                chartControl1.Series["Aylar"].Points.Clear();

                SqlCommand komut11 = new SqlCommand("Select top 4 AY,EKSTRA from TBL_GIDERLER order by ID DESC", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }

            // Doğalgaz
            if (sayac > 11 && sayac <= 15)
            {
                groupControl9.Text = "Doğalgaz";
                chartControl1.Series["Aylar"].Points.Clear();

                SqlCommand komut11 = new SqlCommand("Select top 4 AY,DOGALGAZ from TBL_GIDERLER order by ID DESC", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }

            if (sayac == 26)
            {
                sayac = 0;
            }

        }

        int sayac2 = 0; 
        private void timer2_Tick(object sender, EventArgs e)
        {
            sayac2++;

            // Elektrik
            if (sayac2 > 0 && sayac2 <= 5)
            {
                groupControl10.Text = "Elektrik";
                chartControl2.Series["Aylar"].Points.Clear();

                SqlCommand komut10 = new SqlCommand("Select top 4 AY,ELEKTRIK from TBL_GIDERLER order by ID DESC", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();
            }

            // Su
            if (sayac2 > 5 && sayac2 <= 10)
            {
                groupControl10.Text = "Su";
                chartControl2.Series["Aylar"].Points.Clear();

                SqlCommand komut11 = new SqlCommand("Select top 4 AY,SU from TBL_GIDERLER order by ID DESC", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();

            }

            // Doğalgaz
            if (sayac2 > 10 && sayac2 <= 15)
            {
                groupControl10.Text = "Doğalgaz";
                chartControl2.Series["Aylar"].Points.Clear();

                SqlCommand komut11 = new SqlCommand("Select top 4 AY,DOGALGAZ from TBL_GIDERLER order by ID DESC", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }

            // İnternet
            if (sayac2 > 15 && sayac2 <= 20)
            {
                groupControl10.Text = "İnternet";
                chartControl2.Series["Aylar"].Points.Clear();

                SqlCommand komut11 = new SqlCommand("Select top 4 AY,INTERNET from TBL_GIDERLER order by ID DESC", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }

            // Ekstra
            if (sayac2 > 20 && sayac2 <= 25)
            {
                groupControl10.Text = "Ekstra";
                chartControl2.Series["Aylar"].Points.Clear();

                SqlCommand komut11 = new SqlCommand("Select top 4 AY,EKSTRA from TBL_GIDERLER order by ID DESC", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }

            // Doğalgaz
            if (sayac2 > 11 && sayac2 <= 15)
            {
                groupControl10.Text = "Doğalgaz";
                chartControl2.Series["Aylar"].Points.Clear();

                SqlCommand komut11 = new SqlCommand("Select top 4 AY,DOGALGAZ from TBL_GIDERLER order by ID DESC", bgl.baglanti());
                SqlDataReader dr11 = komut11.ExecuteReader();
                while (dr11.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr11[0], dr11[1]));
                }
                bgl.baglanti().Close();
            }

            if (sayac2 == 26)
            {
                sayac2 = 0;
            }
        }
    }
}
