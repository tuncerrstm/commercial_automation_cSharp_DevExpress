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
    public partial class FrmAyarlar : Form
    {
        public FrmAyarlar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TBL_ADMIN", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        private void FrmAyarlar_Load(object sender, EventArgs e)
        {
            listele();
            txtKullanici.Text = "";
            txtSifre.Text = "";
        }

        private void btnIslem_Click(object sender, EventArgs e)
        {
            if (btnIslem.Text == "Kaydet")
            {

                SqlCommand komut = new SqlCommand("INSERT INTO TBL_ADMIN VALUES (@P1,@P2)", bgl.baglanti());
                komut.Parameters.AddWithValue("@P1", txtKullanici.Text);
                komut.Parameters.AddWithValue("@P2", txtSifre.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Yeni Admin Sisteme Kaydedildi..", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();

            }
            if (btnIslem.Text == "Güncelle")
            {
                SqlCommand komut1 = new SqlCommand("UPDATE TBL_ADMIN SET SIFRE=@P2 WHERE KULLANICIAD=@P1", bgl.baglanti());
                komut1.Parameters.AddWithValue("@P1", txtKullanici.Text);
                komut1.Parameters.AddWithValue("@P2", txtSifre.Text);
                komut1.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Kayıt Güncellendi..", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                listele();
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtKullanici.Text = dr["KULLANICIAD"].ToString();
                txtSifre.Text = dr["SIFRE"].ToString();
            }
        }

        private void txtKullanici_TextChanged(object sender, EventArgs e)
        {
            if (txtKullanici.Text != "")
            {
                btnIslem.Text = "Güncelle";
                btnIslem.BackColor = Color.GreenYellow;
            }
            else
            {
                btnIslem.Text = "Kaydet";
                btnIslem.BackColor = Color.LightGray;
                ;
            }
        }
    }
}
