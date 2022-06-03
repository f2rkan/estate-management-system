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
using System.Configuration;

namespace Otomasyon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=FURKAN;Initial Catalog=Otomasyon;Integrated Security=True");

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            con.Open();
            String kayit = "insert into konutlar(kullanici, konutTuru, konutFiyati, sehir, ilce, metreKare, odaSayisi, binaYasi, isitma, islemTarihi) values (@kullanici,  @konutTuru, @konutFiyati, @sehir, @ilce, @metreKare, @odaSayisi, @binaYasi, @isitma, @islemTarihi)";
            SqlCommand komut = new SqlCommand(kayit, con);
            
            komut.Parameters.AddWithValue("@kullanici", textBox2.Text);
            komut.Parameters.AddWithValue("@konutTuru", textBox6.Text);
            komut.Parameters.AddWithValue("@konutFiyati", textBox3.Text);
            komut.Parameters.AddWithValue("@sehir", textBox4.Text);
            komut.Parameters.AddWithValue("@ilce", textBox7.Text);
            komut.Parameters.AddWithValue("@metreKare", textBox8.Text);
            komut.Parameters.AddWithValue("@odaSayisi", textBox9.Text);
            komut.Parameters.AddWithValue("@binaYasi", textBox10.Text);
            komut.Parameters.AddWithValue("@isitma", textBox11.Text);
            komut.Parameters.AddWithValue("@islemTarihi", dateTimePicker1.Value.Date);

            komut.ExecuteNonQuery();
            MessageBox.Show("veri ekleme basarili");
            con.Close();
            BindData();
        }
        void BindData()
        {
            SqlCommand command = new SqlCommand("select * from konutlar ", con);
            SqlDataAdapter sd = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
                sd.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            this.konutlarTableAdapter.Fill(this.otomasyonDataSet.konutlar);
            BindData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            String kayit = "update konutlar set kullanici = @kullanici, konutTuru = @konutTuru, konutFiyati = @konutFiyati, sehir = @sehir, ilce = @ilce, metreKare = @metreKare, odaSayisi = @odaSayisi, binaYasi = @binaYasi, isitma = @isitma, islemTarihi = @islemTarihi where konutID = @konutID";
            SqlCommand komut = new SqlCommand(kayit, con);

            komut.Parameters.AddWithValue("konutID", int.Parse(textBox1.Text));
            komut.Parameters.AddWithValue("@kullanici", textBox2.Text);
            komut.Parameters.AddWithValue("@konutTuru", textBox6.Text);
            komut.Parameters.AddWithValue("@konutFiyati", textBox3.Text);
            komut.Parameters.AddWithValue("@sehir", textBox4.Text);
            komut.Parameters.AddWithValue("@ilce", textBox7.Text);
            komut.Parameters.AddWithValue("@metreKare", textBox8.Text);
            komut.Parameters.AddWithValue("@odaSayisi", textBox9.Text);
            komut.Parameters.AddWithValue("@binaYasi", textBox10.Text);
            komut.Parameters.AddWithValue("@isitma", textBox11.Text);
            komut.Parameters.AddWithValue("@islemTarihi", dateTimePicker1.Value.Date);
            komut.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("guncelleme islemi basarili");
            BindData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "")
            {
                MessageBox.Show("Geçerli bir ürün ID gir.");
            }
            else
            {
                con.Open();
                String kayit = "delete konutlar where konutID = @konutID";
                SqlCommand komut = new SqlCommand(kayit, con);

                komut.Parameters.AddWithValue("konutID", int.Parse(textBox1.Text));
                komut.Parameters.AddWithValue("@kullanici", textBox2.Text);
                komut.Parameters.AddWithValue("@konutTuru", textBox6.Text);
                komut.Parameters.AddWithValue("@konutFiyati", textBox3.Text);
                komut.Parameters.AddWithValue("@sehir", textBox4.Text);
                komut.Parameters.AddWithValue("@ilce", textBox7.Text);
                komut.Parameters.AddWithValue("@metreKare", textBox8.Text);
                komut.Parameters.AddWithValue("@odaSayisi", textBox9.Text);
                komut.Parameters.AddWithValue("@binaYasi", textBox10.Text);
                komut.Parameters.AddWithValue("@isitma", textBox11.Text);
                komut.Parameters.AddWithValue("@islemTarihi", dateTimePicker1.Value.Date);
                komut.ExecuteNonQuery();
                BindData();
                MessageBox.Show("silme işlemi başarılı");
                con.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["cn"].ConnectionString))
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();
                    using(DataTable dt = new DataTable("konutlar"))
                    {
                        using(SqlCommand cmd = new SqlCommand("select * from konutlar where kullanici like @kullanici or konutTuru like @konutTuru or konutFiyati like @konutFiyati or sehir like @sehir or ilce like @ilce or metreKare like @metreKare or odaSayisi like @odaSayisi or binaYasi like @binaYasi or isitma like @isitma", cn))
                        {
                            
                            cmd.Parameters.AddWithValue("kullanici", string.Format("%{0}%", textBox5.Text));
                            cmd.Parameters.AddWithValue("konutTuru", string.Format("%{0}%", textBox5.Text));
                            cmd.Parameters.AddWithValue("konutFiyati", string.Format("%{0}%", textBox5.Text));
                            cmd.Parameters.AddWithValue("sehir", string.Format("%{0}%", textBox5.Text));
                            cmd.Parameters.AddWithValue("ilce", string.Format("%{0}%", textBox5.Text));
                            cmd.Parameters.AddWithValue("metreKare", string.Format("%{0}%", textBox5.Text));
                            cmd.Parameters.AddWithValue("odaSayisi", string.Format("%{0}%", textBox5.Text));
                            cmd.Parameters.AddWithValue("binaYasi", string.Format("%{0}%", textBox5.Text));
                            cmd.Parameters.AddWithValue("isitma", string.Format("%{0}%", textBox5.Text));
                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            adapter.Fill(dt);
                            dataGridView1.DataSource = dt;
                            label7.Text = $"Bulunan Toplam Kayıt: {dataGridView1.RowCount - 1}";
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                buttonSearch.PerformClick();
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.ReadOnly = true;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
