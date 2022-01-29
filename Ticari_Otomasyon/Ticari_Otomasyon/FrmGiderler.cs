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
    public partial class FrmGiderler : Form
    {
        public FrmGiderler()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void giderlistesi()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBL_GIDERLER ORDER BY ID ASC", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void temizle()
        {
            cmAY.Text = "";
            cmbYIL.Text = "";
            txtID.Text = "";
            txtElektrik.Text = "";
            txtSu.Text = "";
            txtDogalgaz.Text = "";
            txtInternet.Text = "";
            txtMaas.Text = "";
            txtEkstra.Text = "";
            rchNotlar.Text = "";
        }

        private void FrmGiderler_Load(object sender, EventArgs e)
        {
            giderlistesi();

            temizle();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("INSERT INTO TBL_GIDERLER (AY,YIL,ELEKTRIK,SU,DOGALGAZ,INTERNET,MAASLAR,EKSTRA,NOTLAR) VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9)", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", cmAY.Text);
            komut.Parameters.AddWithValue("@P2", cmbYIL.Text);
            komut.Parameters.AddWithValue("@P3", decimal.Parse(txtElektrik.Text));
            komut.Parameters.AddWithValue("@P4", decimal.Parse(txtSu.Text));
            komut.Parameters.AddWithValue("@P5", decimal.Parse(txtDogalgaz.Text));
            komut.Parameters.AddWithValue("@P6", decimal.Parse(txtInternet.Text));
            komut.Parameters.AddWithValue("@P7", decimal.Parse(txtMaas.Text));
            komut.Parameters.AddWithValue("@P8", decimal.Parse(txtEkstra.Text));
            komut.Parameters.AddWithValue("@P9", rchNotlar.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Giderler tabloya eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            giderlistesi();
            //temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if(dr != null)
            {
                txtID.Text = dr["ID"].ToString();
                cmAY.Text = dr["AY"].ToString();
                cmbYIL.Text = dr["YIL"].ToString();
                txtElektrik.Text = dr["ELEKTRIK"].ToString();
                txtSu.Text = dr["SU"].ToString();
                txtDogalgaz.Text = dr["DOGALGAZ"].ToString();
                txtInternet.Text = dr["INTERNET"].ToString();
                txtMaas.Text = dr["MAASLAR"].ToString();
                txtEkstra.Text = dr["EKSTRA"].ToString();
                rchNotlar.Text = dr["NOTLAR"].ToString();
            }
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("DELETE FROM TBL_GIDERLER WHERE ID=@P1", bgl.baglanti());
            komut.Parameters.AddWithValue("@P1", txtID.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Gider Listeden Silindi!!", "Uyarı!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            giderlistesi();
            temizle();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {

            SqlCommand komutguncelle = new SqlCommand("UPDATE TBL_GIDERLER SET AY=@P1,YIL=@P2,ELEKTRIK=@P3,SU=@P4,DOGALGAZ=@P5,INTERNET=@P6,MAASLAR=@P7,EKSTRA=@P8,NOTLAR=@P9 WHERE ID=@P10", bgl.baglanti());
            komutguncelle.Parameters.AddWithValue("@P1", cmAY.Text);
            komutguncelle.Parameters.AddWithValue("@P2", cmbYIL.Text);
            komutguncelle.Parameters.AddWithValue("@P3", decimal.Parse(txtElektrik.Text));
            komutguncelle.Parameters.AddWithValue("@P4", decimal.Parse(txtSu.Text));
            komutguncelle.Parameters.AddWithValue("@P5", decimal.Parse(txtDogalgaz.Text));
            komutguncelle.Parameters.AddWithValue("@P6", decimal.Parse(txtInternet.Text));
            komutguncelle.Parameters.AddWithValue("@P7", decimal.Parse(txtMaas.Text));
            komutguncelle.Parameters.AddWithValue("@P8", decimal.Parse(txtEkstra.Text));
            komutguncelle.Parameters.AddWithValue("@P9", rchNotlar.Text);
            komutguncelle.Parameters.AddWithValue("@P10", txtID.Text);
            komutguncelle.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Gider Bilgisi Güncellendi!", "Bilgi!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            giderlistesi();
            temizle();

        }

    }
}
