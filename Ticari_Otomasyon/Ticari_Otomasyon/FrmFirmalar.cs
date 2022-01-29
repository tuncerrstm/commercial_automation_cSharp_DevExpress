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
    public partial class FrmFirmalar : Form
    {
        public FrmFirmalar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void firmalistesi()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBL_FIRMALAR", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void sehirlistesi()
        {
            SqlCommand komut = new SqlCommand("SELECT SEHIR FROM TBL_ILLER", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbIL.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        void carikodacikamalar()
        {
            SqlCommand komut = new SqlCommand("SELECT FIRMAKOD1 FROM TBL_KODLAR ", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                rchKod1.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();
        }

        void temizle()
        {
            txtAD.Text = "";
            txtID.Text = "";
            txtSektor.Text = "";
            txtVERGIDAIRE.Text = "";
            txtYetkili.Text = "";
            txtYetkiliGorev.Text = "";
            txtKod1.Text = "";
            txtKod2.Text = "";
            txtKod3.Text = "";
            mskFAX.Text = "";
            txtMail.Text = "";
            mskTELEFON1.Text = "";
            mskTELEFON2.Text = "";
            mskTELEFON3.Text = "";
            mskYetkiliTC.Text = "";
            rchAdres.Text = "";
            cmbIL.Text = "";
            cmbILCE.Text = "";


        }

        private void FrmFirmalar_Load(object sender, EventArgs e)
        {
            firmalistesi();
            
            sehirlistesi();

            carikodacikamalar();

            temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
             DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
             if (dr != null)
             {
                 txtID.Text = dr["ID"].ToString();
                 txtAD.Text = dr["AD"].ToString();
                 txtYetkiliGorev.Text = dr["YETKILISTATU"].ToString();
                 txtYetkili.Text = dr["YETKILIADSOYAD"].ToString();
                 mskYetkiliTC.Text = dr["YETKILITC"].ToString();
                 txtSektor.Text = dr["SEKTOR"].ToString();
                 mskTELEFON1.Text = dr["TELEFON1"].ToString();
                 mskTELEFON2.Text = dr["TELEFON2"].ToString();
                 mskTELEFON3.Text = dr["TELEFON3"].ToString();
                 txtMail.Text = dr["MAIL"].ToString();
                 mskFAX.Text = dr["FAX"].ToString();
                 cmbIL.Text = dr["IL"].ToString();
                 cmbILCE.Text = dr["ILCE"].ToString();
                 txtVERGIDAIRE.Text = dr["VERGIDAIRE"].ToString();
                 rchAdres.Text = dr["ADRES"].ToString();
                 txtKod1.Text = dr["OZELKOD1"].ToString();
                 txtKod2.Text = dr["OZELKOD2"].ToString();
                 txtKod3.Text = dr["OZELKOD3"].ToString();
             }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("INSERT INTO TBL_FIRMALAR (AD,YETKILISTATU,YETKILIADSOYAD,YETKILITC,SEKTOR,TELEFON1,TELEFON2,TELEFON3,MAIL,FAX,IL,ILCE,VERGIDAIRE,ADRES,OZELKOD1,OZELKOD2,OZELKOD3) VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@P12,@P13,@P14,@P15,@P16,@P17)", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", txtAD.Text);
            komut.Parameters.AddWithValue("@P2", txtYetkiliGorev.Text);
            komut.Parameters.AddWithValue("@P3", txtYetkili.Text);
            komut.Parameters.AddWithValue("@P4", mskYetkiliTC.Text);
            komut.Parameters.AddWithValue("@P5", txtSektor.Text);
            komut.Parameters.AddWithValue("@P6", mskTELEFON1.Text);
            komut.Parameters.AddWithValue("@P7", mskTELEFON2.Text);
            komut.Parameters.AddWithValue("@P8", mskTELEFON3.Text);
            komut.Parameters.AddWithValue("@P9", txtMail.Text);
            komut.Parameters.AddWithValue("@P10", mskFAX.Text);
            komut.Parameters.AddWithValue("@P11", cmbIL.Text);
            komut.Parameters.AddWithValue("@P12", cmbILCE.Text);
            komut.Parameters.AddWithValue("@P13", txtVERGIDAIRE.Text);
            komut.Parameters.AddWithValue("@P14", rchAdres.Text);
            komut.Parameters.AddWithValue("@P15", txtKod1.Text);
            komut.Parameters.AddWithValue("@P16", txtKod2.Text);
            komut.Parameters.AddWithValue("@P17", txtKod3.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Firma Sisteme Kaydedildi", "Bilgi!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            firmalistesi();
            temizle();
        }

        private void cmbIL_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbILCE.Properties.Items.Clear();
            SqlCommand komut = new SqlCommand("SELECT ILCE FROM TBL_ILCELER WHERE SEHIR=@P1", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", cmbIL.SelectedIndex + 1);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbILCE.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            switch (MessageBox.Show("Bu Firmayı Kalıcı Olarak Silmek İstediğinizden Eminmisiniz?", "UYARI!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                case DialogResult.Cancel:
                    firmalistesi(); break;

                case DialogResult.OK:

                    SqlCommand komut = new SqlCommand("DELETE FROM TBL_FIRMALAR WHERE ID=@P1", bgl.baglanti());
                    komut.Parameters.AddWithValue("@P1", txtID.Text);
                    komut.ExecuteNonQuery();
                    bgl.baglanti().Close();
                    firmalistesi();
                    MessageBox.Show("Firma Listeden Silinmiştir!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    temizle(); break;
                    
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("UPDATE TBL_FIRMALAR SET AD=@P1,YETKILISTATU=@P2,YETKILIADSOYAD=@P3,YETKILITC=@P4,SEKTOR=@P5,TELEFON1=@P6,TELEFON2=@P7,TELEFON3=@P8,MAIL=@P9,FAX=@P10,IL=@P11,ILCE=@P12,VERGIDAIRE=@P13,ADRES=@P14,OZELKOD1=@P15,OZELKOD2=@P16,OZELKOD3=@P17 WHERE ID=@P18", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", txtAD.Text);
            komut.Parameters.AddWithValue("@P2", txtYetkiliGorev.Text);
            komut.Parameters.AddWithValue("@P3", txtYetkili.Text);
            komut.Parameters.AddWithValue("@P4", mskYetkiliTC.Text);
            komut.Parameters.AddWithValue("@P5", txtSektor.Text);
            komut.Parameters.AddWithValue("@P6", mskTELEFON1.Text);
            komut.Parameters.AddWithValue("@P7", mskTELEFON2.Text);
            komut.Parameters.AddWithValue("@P8", mskTELEFON3.Text);
            komut.Parameters.AddWithValue("@P9", txtMail.Text);
            komut.Parameters.AddWithValue("@P10", mskFAX.Text);
            komut.Parameters.AddWithValue("@P11", cmbIL.Text);
            komut.Parameters.AddWithValue("@P12", cmbILCE.Text);
            komut.Parameters.AddWithValue("@P13", txtVERGIDAIRE.Text);
            komut.Parameters.AddWithValue("@P14", rchAdres.Text);
            komut.Parameters.AddWithValue("@P15", txtKod1.Text);
            komut.Parameters.AddWithValue("@P16", txtKod2.Text);
            komut.Parameters.AddWithValue("@P17", txtKod3.Text);
            komut.Parameters.AddWithValue("@P18", txtID.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Firma Bilgileri Güncellenmiştir.", "Bilgi!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            firmalistesi();
            temizle();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}
