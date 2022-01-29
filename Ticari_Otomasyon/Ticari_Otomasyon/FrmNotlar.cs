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
    public partial class FrmNotlar : Form
    {
        public FrmNotlar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBL_NOTLAR", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void temizle()
        {
            txtID.Text = "";
            mskTarih.Text = "";
            mskSaat.Text = "";
            txtBaslik.Text = "";
            txtOlusturan.Text = "";
            txtHitap.Text = "";
            rchDetay.Text = "";
        }

        private void FrmNotlar_Load(object sender, EventArgs e)
        {
            listele();

            temizle();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("INSERT INTO TBL_NOTLAR (TARIH,SAAT,BASLIK,DETAY,OLUSTURAN,HITAP) VALUES (@P1,@P2,@P3,@P4,@P5,@P6)", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", mskTarih.Text);
            komut.Parameters.AddWithValue("@P2", mskSaat.Text);
            komut.Parameters.AddWithValue("@P3", txtBaslik.Text);
            komut.Parameters.AddWithValue("@P4", rchDetay.Text);
            komut.Parameters.AddWithValue("@P5", txtOlusturan.Text);
            komut.Parameters.AddWithValue("@P6", txtHitap.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Not Sisteme Kaydedilmiştir..", "Bilgi!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtID.Text = dr["ID"].ToString();
                txtBaslik.Text = dr["BASLIK"].ToString();
                rchDetay.Text = dr["DETAY"].ToString();
                txtOlusturan.Text = dr["OLUSTURAN"].ToString();
                txtHitap.Text = dr["HITAP"].ToString();
                mskTarih.Text = dr["TARIH"].ToString();
                mskSaat.Text = dr["SAAT"].ToString();
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            switch (MessageBox.Show("Bu notu silmek istediğinize eminmisiniz?", "UYARI!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                case DialogResult.Cancel:
                    listele(); break;

                case DialogResult.OK:

                    SqlCommand komut = new SqlCommand("DELETE TBL_NOTLAR WHERE ID=@P1", bgl.baglanti());
                    komut.Parameters.AddWithValue("@P1", txtID.Text);
                    komut.ExecuteNonQuery();
                    bgl.baglanti().Close();
                    MessageBox.Show("Not Sistemden Silinmiştir..", "Bilgi!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    listele(); break;
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("UPDATE TBL_NOTLAR SET TARIH=@P1,SAAT=@P2,BASLIK=@P3,DETAY=@P4,OLUSTURAN=@P5,HITAP=@P6 WHERE ID=@P7", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", mskTarih.Text);
            komut.Parameters.AddWithValue("@P2", mskSaat.Text);
            komut.Parameters.AddWithValue("@P3", txtBaslik.Text);
            komut.Parameters.AddWithValue("@P4", rchDetay.Text);
            komut.Parameters.AddWithValue("@P5", txtOlusturan.Text);
            komut.Parameters.AddWithValue("@P6", txtHitap.Text);
            komut.Parameters.AddWithValue("@P7", txtID.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Not Bilgisi Güncellenmiştir..", "Bilgi!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmNotDetay fr = new FrmNotDetay();

            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if(dr != null)
            {
                fr.metin = dr["DETAY"].ToString();
            }
            fr.Show();
        }
    }
}
