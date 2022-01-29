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
    public partial class FrmBankalar : Form
    {
        public FrmBankalar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("EXECUTE BANKABILGILERI", bgl.baglanti());
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

        void firmaListesi()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT ID,AD FROM TBL_FIRMALAR", bgl.baglanti());
            da.Fill(dt);
            lookUpFirma.Properties.NullText = "Bir Firma Seçiniz..";
            lookUpFirma.Properties.ValueMember = "ID";
            lookUpFirma.Properties.DisplayMember = "AD";
            lookUpFirma.Properties.DataSource = dt;
        }

        void temizle()
        {
            txtID.Text = "";
            txtBANKAAD.Text = "";
            cmbIL.Text = "";
            cmbILCE.Text = "";
            txtSube.Text = "";
            mskIBAN.Text = "";
            mskHesapNo.Text = "";
            txtYetkili.Text = "";
            mskTELEFON.Text = "";
            mskTarih.Text = "";
            txtHesapTuru.Text = "";
        }

        private void FrmBankalar_Load(object sender, EventArgs e)
        {
            listele();

            sehirlistesi();

            temizle();

            firmaListesi();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
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

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("INSERT INTO TBL_BANKALAR (BANKAADI,IL,ILCE,SUBE,IBAN,HESAPNO,YETKILI,TELEFON,TARIH,HESAPTURU,FIRMAID) VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11)", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", txtBANKAAD.Text);
            komut.Parameters.AddWithValue("@p2", cmbIL.Text);
            komut.Parameters.AddWithValue("@p3", cmbILCE.Text);
            komut.Parameters.AddWithValue("@P4", txtSube.Text);
            komut.Parameters.AddWithValue("@P5", mskIBAN.Text);
            komut.Parameters.AddWithValue("@P6", mskHesapNo.Text);
            komut.Parameters.AddWithValue("@P7", txtYetkili.Text);
            komut.Parameters.AddWithValue("@P8", mskTELEFON.Text);
            komut.Parameters.AddWithValue("@P9", mskTarih.Text);
            komut.Parameters.AddWithValue("@P10", txtHesapTuru.Text);
            komut.Parameters.AddWithValue("@P11", lookUpFirma.EditValue);
            komut.ExecuteNonQuery();
            listele();
            bgl.baglanti().Close();
            MessageBox.Show("Banka Bilgisi Sisteme Eklendi!", "Bilgi!", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtID.Text = dr["ID"].ToString();
                txtBANKAAD.Text = dr["BANKAADI"].ToString();
                cmbIL.Text = dr["IL"].ToString();
                cmbILCE.Text = dr["ILCE"].ToString();
                txtSube.Text = dr["SUBE"].ToString();
                mskIBAN.Text = dr["IBAN"].ToString();
                mskHesapNo.Text = dr["HESAPNO"].ToString();
                txtYetkili.Text = dr["YETKILI"].ToString();
                mskTELEFON.Text = dr["TELEFON"].ToString();
                mskTarih.Text = dr["TARIH"].ToString();
                txtHesapTuru.Text = dr["HESAPTURU"].ToString();

            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("DELETE FROM TBL_BANKALAR WHERE ID=@P1", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", txtID.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            temizle();
            MessageBox.Show("Banka Bilgisi Sistemden Silinmiştir..!", "Bilgi!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            listele();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("UPDATE TBL_BANKALAR SET BANKAADI=@P1,IL=@P2,ILCE=@P3,SUBE=@P4,IBAN=@P5,HESAPNO=@P6,YETKILI=@P7,TELEFON=@P8,TARIH=@P9,HESAPTURU=@P10,FIRMAID=@P11 WHERE ID=@P12", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", txtBANKAAD.Text);
            komut.Parameters.AddWithValue("@p2", cmbIL.Text);
            komut.Parameters.AddWithValue("@p3", cmbILCE.Text);
            komut.Parameters.AddWithValue("@P4", txtSube.Text);
            komut.Parameters.AddWithValue("@P5", mskIBAN.Text);
            komut.Parameters.AddWithValue("@P6", mskHesapNo.Text);
            komut.Parameters.AddWithValue("@P7", txtYetkili.Text);
            komut.Parameters.AddWithValue("@P8", mskTELEFON.Text);
            komut.Parameters.AddWithValue("@P9", mskTarih.Text);
            komut.Parameters.AddWithValue("@P10", txtHesapTuru.Text);
            komut.Parameters.AddWithValue("@P11", lookUpFirma.EditValue);
            komut.Parameters.AddWithValue("@P12", txtID.Text);
            komut.ExecuteNonQuery();
            listele();
            bgl.baglanti().Close();
            MessageBox.Show("Banka Bilgisi Güncellenmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }
    }
}
