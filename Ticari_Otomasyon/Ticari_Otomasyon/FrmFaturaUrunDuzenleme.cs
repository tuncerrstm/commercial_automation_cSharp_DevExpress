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

namespace Ticari_Otomasyon
{
    public partial class FrmFaturaUrunDuzenleme : Form
    {
        public FrmFaturaUrunDuzenleme()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        public string urunid;
        private void FrmFaturaUrunDuzenleme_Load(object sender, EventArgs e)
        {
            txtURUNID.Text = urunid;

            SqlCommand komut = new SqlCommand("SELECT * FROM TBL_FATURADETAY WHERE FATURAURUNID=@P1", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", txtURUNID.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                txtFiyat.Text = dr[3].ToString();
                txtMiktar.Text = dr[2].ToString();
                txtTutar.Text = dr[4].ToString();
                txtUrunAd.Text = dr[1].ToString();

                bgl.baglanti().Close();
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {

            double miktar, tutar, fiyat;
            fiyat = Convert.ToDouble(txtFiyat.Text);
            miktar = Convert.ToDouble(txtMiktar.Text);
            tutar = miktar * fiyat;
            txtTutar.Text = tutar.ToString();

            SqlCommand komut = new SqlCommand("UPDATE TBL_FATURADETAY SET URUNAD=@P1,MIKTAR=@P2,FIYAT=@P3,TUTAR=@P4 WHERE FATURAURUNID=@P5", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", txtUrunAd.Text);
            komut.Parameters.AddWithValue("@P2", txtMiktar.Text);
            komut.Parameters.AddWithValue("@P3", decimal.Parse(txtFiyat.Text));
            komut.Parameters.AddWithValue("@P4", decimal.Parse(txtTutar.Text));
            komut.Parameters.AddWithValue("@P5", txtURUNID.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Değişiklikler Kaydedildi...", "Bilgi!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            switch (MessageBox.Show("Bu bilgiyi silmek istediğinizden eminmisiniz?", "UYARI!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                case DialogResult.Cancel:
                    break;

                case DialogResult.OK:

                    SqlCommand komut = new SqlCommand("DELETE FROM TBL_FATURADETAY WHERE FATURAURUNID=@P1", bgl.baglanti());
                    komut.Parameters.AddWithValue("@P1", txtURUNID.Text);
                    komut.ExecuteNonQuery();
                    bgl.baglanti().Close(); 
                    MessageBox.Show("Ürün Silinmiştir...", "Bilgi!", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    break;
            }
        }


    }
}
