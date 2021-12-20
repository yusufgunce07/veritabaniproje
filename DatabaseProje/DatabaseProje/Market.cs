using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace DatabaseProje
{
    public partial class Market : Form
    {
        public Market()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost; port=5432;Database=odev1; user Id=postgres; password=123456");
        private void tabPage1_Click(object sender, EventArgs e)
        {
           
        }
        
        private void TxtUrun_TextChanged(object sender, EventArgs e)
        {
            TxtUrungrup.Text = ComboBox1.SelectedValue.ToString();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            TxtUrungrup.Text = ComboBox1.SelectedValue.ToString();
        }

        private void BtnListele_Click(object sender, EventArgs e)
        {

            string sorgu = "select * from urunler";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut2 = new NpgsqlCommand("insert into urunler(urun_no,grup_no,urun_isim,marka,fiyat)values(@p1,@p2,@p3,@p4,@p5)", baglanti);
            komut2.Parameters.AddWithValue("@p1", Convert.ToDecimal(TxtUrun.Text));
            komut2.Parameters.AddWithValue("@p2", Convert.ToDecimal(TxtUrungrup.Text));
            komut2.Parameters.AddWithValue("@p3", Txturunisim.Text);
            komut2.Parameters.AddWithValue("@p4", TxtMarka.Text);
            komut2.Parameters.AddWithValue("@p5", Convert.ToDecimal(TxtFiyat.Text));
            komut2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ekleme işlemi başarılı.");
        }

        private void BtnSilme_Click(object sender, EventArgs e)
        {

            baglanti.Open();
            NpgsqlCommand komut2 = new NpgsqlCommand("delete From urunler where urun_isim=@p1", baglanti);
            komut2.Parameters.AddWithValue("@p1", Txturunisim.Text);
            komut2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Silme işlemi başarılı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut3 = new NpgsqlCommand("update urunler set grup_no=@p1,urun_isim=@p2,marka=@p3,fiyat=@p4 where urun_no=@p5", baglanti);
            komut3.Parameters.AddWithValue("@p1", Convert.ToDecimal(TxtUrungrup.Text));
            komut3.Parameters.AddWithValue("@p2", Txturunisim.Text);
            komut3.Parameters.AddWithValue("@p3", TxtMarka.Text);
            komut3.Parameters.AddWithValue("@p4", Convert.ToDecimal(TxtFiyat.Text));
            komut3.Parameters.AddWithValue("@p5", Convert.ToDecimal(TxtUrun.Text));
            komut3.ExecuteNonQuery();
            MessageBox.Show("Güncelleme işlemi başarılı.", " Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            baglanti.Close();
        }

        private void BtnArama_Click(object sender, EventArgs e)
        {
            string sorgu = "select* from calisanlar where calisan_isim like '%" + TxtArama.Text + "%'";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void TxtUrungrup_TextChanged(object sender, EventArgs e)
        {

        }

        private void Market_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlDataAdapter dat = new NpgsqlDataAdapter("select * from urun_gruplari", baglanti);
            DataTable dta = new DataTable();
            dat.Fill(dta);
            ComboBox1.DisplayMember = "grup_no_isim";
            ComboBox1.ValueMember = "grup_no";
            ComboBox1.DataSource = dta;
           
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from depatmanlar", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            CmbDepartman.DisplayMember = "departman_isim";
            CmbDepartman.ValueMember = "departman_no";
            CmbDepartman.DataSource = dt;
            NpgsqlDataAdapter da1 = new NpgsqlDataAdapter("select * from subeler", baglanti);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            CmbSube.DisplayMember = "sube_adi";
            CmbSube.ValueMember = "sube_no";
            CmbSube.DataSource = dt1;
            baglanti.Close();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

            string sorgu = "select * from musteri";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut5 = new NpgsqlCommand("update musteri set musteri_ad=@p2,musteri_soyadi=@p3,musteri_dogum_tarihi=@p4,musteri_araba_sayisi=@p5 where musteri_card_no=@p1", baglanti);
            komut5.Parameters.AddWithValue("@p1", TxtMustericard.Text);
            komut5.Parameters.AddWithValue("@p2", TxtMad.Text);
            komut5.Parameters.AddWithValue("@p3", TxtMsoyad.Text);
            komut5.Parameters.AddWithValue("@p4", Convert.ToDateTime(dateTimePicker1.Text));
            komut5.Parameters.AddWithValue("@p5", Convert.ToDecimal(numericUpDown1.Text));
            komut5.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Güncelleme işlemi başarılı.");
        }

        private void button3_Click(object sender, EventArgs e)
        {

            baglanti.Open();
            NpgsqlCommand komut2 = new NpgsqlCommand("delete From musteri where musteri_card_no=@p1", baglanti);
            komut2.Parameters.AddWithValue("@p1", TxtMustericard.Text);
            komut2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Silme işlemi başarili.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut4 = new NpgsqlCommand("insert into musteri(musteri_card_no,musteri_ad,musteri_soyadi,musteri_dogum_tarihi,musteri_araba_sayisi)values(@p1,@p2,@p3,@p4,@p5)", baglanti);
            komut4.Parameters.AddWithValue("@p1", Convert.ToDecimal(TxtMustericard.Text));
            komut4.Parameters.AddWithValue("@p2", TxtMad.Text);
            komut4.Parameters.AddWithValue("@p3", TxtMsoyad.Text);
            komut4.Parameters.AddWithValue("@p4", Convert.ToDateTime(dateTimePicker1.Text));
            komut4.Parameters.AddWithValue("@p5", Convert.ToDecimal(numericUpDown1.Text));
            komut4.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ekleme işlemi başarılı.");
        }

        private void button1_Click(object sender, EventArgs e)
        {   
            string sorgu = "select * from musteri where musteri_ad like '%" + textBox1.Text + "%'";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];
        }

        private void BtnListeleC_Click(object sender, EventArgs e)
        {

            string sorgu = "select * from calisanlar order by calisan_no ";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView3.DataSource = ds.Tables[0];
        }

        private void BtnEkleC_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut2 = new NpgsqlCommand("insert into calisanlar(calisan_no,departman_no,sube_no,calisan_isim,calisan_soyisim,calisan_dogum_tarihi)values(@p1,@p2,@p3,@p4,@p5,@p6)", baglanti);
            komut2.Parameters.AddWithValue("@p1", Convert.ToDecimal(TxtCalisanno.Text));
            komut2.Parameters.AddWithValue("@p2", Convert.ToDecimal(TxtDepartmanx.Text));
            komut2.Parameters.AddWithValue("@p3", Convert.ToDecimal(TxtSubex.Text));
            komut2.Parameters.AddWithValue("@p4", TxtCAd.Text);
            komut2.Parameters.AddWithValue("@p5", TxtCSoyad.Text);
            komut2.Parameters.AddWithValue("@p6", Convert.ToDateTime(DatatimeCA.Text));
            komut2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ekleme işlemi başarılı.");
        }

        private void CmbDepartman_SelectedIndexChanged(object sender, EventArgs e)
        {
            TxtDepartmanx.Text = CmbDepartman.SelectedValue.ToString();
        }

        private void TxtDepartmanx_TextChanged(object sender, EventArgs e)
        {
            TxtDepartmanx.Text = CmbDepartman.SelectedValue.ToString();
        }

        private void TxtSubex_TextChanged(object sender, EventArgs e)
        {
            TxtSubex.Text = CmbSube.SelectedValue.ToString();
        }

        private void CmbSube_SelectedIndexChanged(object sender, EventArgs e)
        {
            TxtSubex.Text = CmbSube.SelectedValue.ToString();
        }

        private void BtnSilC_Click(object sender, EventArgs e)
        {

            baglanti.Open();
            NpgsqlCommand komut6 = new NpgsqlCommand("delete From calisanlar where calisan_NO=@p1", baglanti);
            komut6.Parameters.AddWithValue("@p1", Convert.ToDecimal(TxtCalisanno.Text));
            komut6.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Silme işlemi başarılı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        private void BtnGuncelleC_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut2 = new NpgsqlCommand("update calisanlar set departman_no=@p2,sube_no=@p3,calisan_isim=@p4,calisan_soyisim=@p5,calisan_dogum_tarihi=@p6 where calisan_no=@p1", baglanti);
            komut2.Parameters.AddWithValue("@p1", Convert.ToDecimal(TxtCalisanno.Text));
            komut2.Parameters.AddWithValue("@p2", Convert.ToDecimal(TxtDepartmanx.Text));
            komut2.Parameters.AddWithValue("@p3", Convert.ToDecimal(TxtSubex.Text));
            komut2.Parameters.AddWithValue("@p4", TxtCAd.Text);
            komut2.Parameters.AddWithValue("@p5", TxtCSoyad.Text);
            komut2.Parameters.AddWithValue("@p6", Convert.ToDateTime(DatatimeCA.Text));
            komut2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Güncelleme işlemi başarılı.");
        }

        private void BtnAramaC_Click(object sender, EventArgs e)
        {

            string sorgu = "select* from calisanlar where calisan_isim like '%" + TxtARA.Text + "%'";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView3.DataSource = ds.Tables[0];
        }

        private void BtnTlistele_Click(object sender, EventArgs e)
        {

            string sorgu = "select * from tedarikciler";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView4.DataSource = ds.Tables[0];
        }

        private void BtnTekle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut2 = new NpgsqlCommand("insert into tedarikciler(tedarikci_no,tedarikci_firma_ismi,tedarikci_adres)values(@p1,@p2,@p3)", baglanti);
            komut2.Parameters.AddWithValue("@p1", Convert.ToDecimal(TxtTedarik.Text));
            komut2.Parameters.AddWithValue("@p2", TxtFirma.Text);
            komut2.Parameters.AddWithValue("@p3", TxtAdres.Text);
            komut2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ekleme işlemi başarılı.");
        }

        private void BtnTsil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut2 = new NpgsqlCommand("delete From tedarikciler where tedarikci_no=@p1", baglanti);
            komut2.Parameters.AddWithValue("@p1", Convert.ToDecimal(TxtTedarik.Text));
            komut2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Silme işlemi başarılı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Stop);

        }

        private void BtnTgüncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut3 = new NpgsqlCommand("update tedarikciler set tedarikci_firma_ismi=@p2,tedarikci_adres=@p3 where tedarikci_no=@p1", baglanti);
            komut3.Parameters.AddWithValue("@p1", Convert.ToDecimal(TxtTedarik.Text));
            komut3.Parameters.AddWithValue("@p2", TxtFirma.Text);
            komut3.Parameters.AddWithValue("@p3", TxtAdres.Text);
            komut3.ExecuteNonQuery();
            MessageBox.Show("Güncelleme tamamlandi.", " Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            baglanti.Close();
        }

        private void BtnTarama_Click(object sender, EventArgs e)
        {
            string sorgu = "select * from tedarikciler where  tedarikci_firma_ismi like '%" + TxtTarama.Text + "%'";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView4.DataSource = ds.Tables[0];
        }
    }
}
