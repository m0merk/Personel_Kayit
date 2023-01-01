using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Personel_Kayıt
{
    public partial class FrmAnaForm : Form
    {
        public FrmAnaForm()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-O4IQFMC;Initial Catalog=PersonelVeriTabani;Integrated Security=True");

        void temizle()
        {
            txtId.Clear();
            txtAd.Clear();
            txtSyd.Clear();
            cmbSehir.ResetText();
            mskMaas.Clear();
            txtMeslek.Clear();
            label8.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            txtAd.Focus();

            //********************************

            //Böyle de yapabiliriz

            //txtId.Text = "";
            //txtAd.Text = "";
            //txtSyd.Text = "";
            //txtMeslek.Text ="";
            //mskMaas.Text = "";
            //cmbSehir.Text ="";
            //radioButton1.Checked = false;
            //radioButton2.Checked = false;
            //txtAd.Focus();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // txtId.Select(); böyle de olabilir ama "temizle" methodumuzda farklı bir şekilde yer verdik buna.
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            this.tbl_PersonelTableAdapter.Fill(this.personelVeriTabaniDataSet.Tbl_Personel);
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komutekle = new SqlCommand("insert into Tbl_Personel(PerAd,PerSoyad,Persehir,permaas,Permeslek,Perdurum) values (@p1,@p2,@p3,@p4,@p5,@p6)", baglanti);//@p1 deki p harfi değişken gibi .Yani istediğimiz harfi verebiliriz.
                                                                                                                                                                             //PerAd,persoyad büyük küçük harf duyarlılığı yok.
                                                                                                                                                                             //Parametrelerin veritabanındaki sırası önemli değil,
                                                                                                                                                                             //önemli olan parantez içindeki sırası.
            komutekle.Parameters.AddWithValue("@p1", txtAd.Text);
            komutekle.Parameters.AddWithValue("@p2", txtSyd.Text);
            komutekle.Parameters.AddWithValue("@p3", cmbSehir.Text);
            komutekle.Parameters.AddWithValue("@p4", mskMaas.Text);
            komutekle.Parameters.AddWithValue("@p5", txtMeslek.Text);
            komutekle.Parameters.AddWithValue("@p6", label8.Text);
            komutekle.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Personel Eklendi");

            this.tbl_PersonelTableAdapter.Fill(this.personelVeriTabaniDataSet.Tbl_Personel);

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                label8.Text = "True";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                label8.Text = "False";
            }
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            txtId.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtSyd.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            cmbSehir.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            mskMaas.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            label8.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            txtMeslek.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
        }

        private void label8_TextChanged(object sender, EventArgs e)
        {
            if (label8.Text == "True")
            {
                radioButton1.Checked = true;
            }
            if (label8.Text == "False")
            {
                radioButton2.Checked = true;
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komutsil = new SqlCommand("Delete From Tbl_Personel where Perid=@k1",baglanti);
            komutsil.Parameters.AddWithValue("@k1",txtId.Text);
            komutsil.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt Silindi");
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komutguncelle = new SqlCommand("Update Tbl_personel Set perad=@ad, persoyad=@soyad,Persehir=@sehir,permaas=@maas,perdurum=@durum,permeslek=@meslek where perid=@id",baglanti);
            komutguncelle.Parameters.AddWithValue("@ad",txtAd.Text);
            komutguncelle.Parameters.AddWithValue("@soyad",txtSyd.Text);
            komutguncelle.Parameters.AddWithValue("@sehir",cmbSehir.Text);
            komutguncelle.Parameters.AddWithValue("@maas",mskMaas.Text);
            komutguncelle.Parameters.AddWithValue("@durum",label8.Text);
            komutguncelle.Parameters.AddWithValue("@meslek",txtMeslek.Text);
            komutguncelle.Parameters.AddWithValue("@id",txtId.Text);
            komutguncelle.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Personel Bilgi Güncellendi");
        }

        private void btnIstatistik_Click(object sender, EventArgs e)
        {
            Frmistatistik Fri = new Frmistatistik();
            Fri.Show();
        }

        private void btnGrafikler_Click(object sender, EventArgs e)
        {
            FrmGrafikler frg = new FrmGrafikler();// yukarıdıkinin aynısı(fri) olabilir sıkıntı yok.Hata vermez.
            frg.Show();
        }
    }
}
