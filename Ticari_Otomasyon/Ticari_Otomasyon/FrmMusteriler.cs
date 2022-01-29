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
    public partial class FrmMusteriler : Form
    {
        public FrmMusteriler()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From TBL_MUSTERILER ORDER BY ID ASC", bgl.baglanti());
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

        void temizle()
        {
            txtID.Text = "";
            txtAD.Text = "";
            txtSoyad.Text = "";
            mskTELEFON1.Text = "";
            mskTELEFON2.Text = "";
            mskTC.Text = "";
            txtMail.Text = "";
            cmbIL.Text ="";
            cmbILCE.Text = "";
            txtVERGIDAIRE.Text = "";
            rchAdres.Text = "";
        }

        private void FrmMusteriler_Load(object sender, EventArgs e)
        {
            listele();

            sehirlistesi();

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
            SqlCommand komut = new SqlCommand("INSERT INTO TBL_MUSTERILER (AD,SOYAD,TELEFON,TELEFON2,TC,MAIL,IL,ILCE,ADRES,VERGIDAIRE) VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10)", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", txtAD.Text);
            komut.Parameters.AddWithValue("@P2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@P3", mskTELEFON1.Text);
            komut.Parameters.AddWithValue("@P4", mskTELEFON2.Text);
            komut.Parameters.AddWithValue("@P5", mskTC.Text);
            komut.Parameters.AddWithValue("@P6", txtMail.Text);
            komut.Parameters.AddWithValue("@P7", cmbIL.Text);
            komut.Parameters.AddWithValue("@P8", cmbILCE.Text);
            komut.Parameters.AddWithValue("@P9", txtVERGIDAIRE.Text);
            komut.Parameters.AddWithValue("@P10", rchAdres.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Müşteri Sisteme Eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();

        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtID.Text = dr["ID"].ToString();
                txtAD.Text = dr["AD"].ToString();
                txtSoyad.Text = dr["SOYAD"].ToString();
                mskTELEFON1.Text = dr["TELEFON"].ToString();
                mskTELEFON2.Text = dr["TELEFON2"].ToString();
                mskTC.Text = dr["TC"].ToString();
                txtMail.Text = dr["MAIL"].ToString();
                cmbIL.Text = dr["IL"].ToString();
                cmbILCE.Text = dr["ILCE"].ToString();
                txtVERGIDAIRE.Text = dr["VERGIDAIRE"].ToString();
                rchAdres.Text = dr["ADRES"].ToString();

            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            switch (MessageBox.Show("Bu müşteriyi silmek istediğinizden emin misiniz?", "UYARI!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                case DialogResult.Cancel:
                    listele(); break;

                case DialogResult.OK:

                    SqlCommand komut = new SqlCommand("DELETE FROM TBL_MUSTERILER WHERE ID=@P1", bgl.baglanti());
                    komut.Parameters.AddWithValue("@P1", txtID.Text);
                    komut.ExecuteNonQuery();
                    bgl.baglanti().Close();
                    MessageBox.Show("Müşteri Silindi.!", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    listele(); break;
            }

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("UPDATE TBL_MUSTERILER SET AD=@P1,SOYAD=@P2,TELEFON=@P3,TELEFON2=@P4,TC=@P5,MAIL=@P6,IL=@P7,ILCE=@P8,VERGIDAIRE=@P9,ADRES=@P10 WHERE ID=@P11", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", txtAD.Text);
            komut.Parameters.AddWithValue("@P2", txtSoyad.Text);
            komut.Parameters.AddWithValue("@P3", mskTELEFON1.Text);
            komut.Parameters.AddWithValue("@P4", mskTELEFON2.Text);
            komut.Parameters.AddWithValue("@P5", mskTC.Text);
            komut.Parameters.AddWithValue("@P6", txtMail.Text);
            komut.Parameters.AddWithValue("@P7", cmbIL.Text);
            komut.Parameters.AddWithValue("@P8", cmbILCE.Text);
            komut.Parameters.AddWithValue("@P9", txtVERGIDAIRE.Text);
            komut.Parameters.AddWithValue("@P10", rchAdres.Text);
            komut.Parameters.AddWithValue("@P11", txtID.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Müşteri Bilgileri Güncellenmiştir.", "Bilgi!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
        }

    }
}
