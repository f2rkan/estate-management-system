using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Otomasyon.Connection;
using System.Security.Cryptography;

namespace Otomasyon
{
    public partial class formRegister : Form
    {

        public static string HashString(string passwordString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(passwordString))
                sb.Append(b.ToString("X3"));
            return sb.ToString();
        }
        public static byte[] GetHash(string passwordString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(passwordString));
        }


        public formRegister()
        {
            InitializeComponent();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            clearControls();
            usernameTextBox.Select();
        }

        private void clearControls()
        {
            foreach(TextBox tb in this.Controls.OfType<TextBox>())
            {
                tb.Text = string.Empty;
            }
        }

        private void formRegister_Load(object sender, EventArgs e)
        {
            loadUserData();
            usernameTextBox.Select();
        }

        private void loadUserData()
        {
            DataTable userData = ServerConnection.executeSQL("SELECT username, password, ulke, sehir  from LoginTbl");
            dataGridView1.DataSource = userData;

            dataGridView1.Columns[0].HeaderText = "Kullanıcı Adı";
            dataGridView1.Columns[0].Width = 100;

            dataGridView1.Columns[1].HeaderText = "Şifre";
            dataGridView1.Columns[1].Width = 100;

            dataGridView1.Columns[2].HeaderText = "Yaşadığı Ülke";
            dataGridView1.Columns[2].Width = 100;

            dataGridView1.Columns[3].HeaderText = "Yaşadığı Şehir";
            dataGridView1.Columns[3].Width = 100;


        }

        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Silmek istiyor musun?", "Sil",
                  MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                  MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    ServerConnection.executeSQL("DELETE FROM LoginTbl WHERE username = '" + dataGridView1.CurrentRow.Cells[0].Value + "'");
                    loadUserData();

                    MessageBox.Show("Silme İşlemi Başarıyla Sonuçlandı", "Sil", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            MessageBoxButtons btn = MessageBoxButtons.OK;
            MessageBoxIcon ico = MessageBoxIcon.Information;
            string caption = "Yeni Kullanıcı Kaydı";

            if (string.IsNullOrEmpty(usernameTextBox.Text))
            {
                MessageBox.Show("Kullanıcı Adı Kısmı Boş Geçilemez", caption, btn, ico);
                usernameTextBox.Select();
                return;
            }

            if (string.IsNullOrEmpty(passwordTextBox.Text))
            {
                MessageBox.Show("Şifre Kısmı Boş Geçilemez", caption, btn, ico);
                passwordTextBox.Select();
                return;
            }
            if (string.IsNullOrEmpty(confirmPasswordTextBox.Text))
            {
                MessageBox.Show("Şifreni Tekrar Et", caption, btn, ico);
                confirmPasswordTextBox.Select();
                return;
            }

           

            if (string.IsNullOrEmpty(gorevTextBox.Text))
            {
                MessageBox.Show("Yaşadığı Ülke Kısmı Boş Geçilemez", caption, btn, ico);
                gorevTextBox.Select();
                return;
            }
            if (string.IsNullOrEmpty(yetkiTextBox.Text))
            {
                MessageBox.Show("Yaşadığı Şehir Kısmı Boş Geçilemez", caption, btn, ico);
                yetkiTextBox.Select();
                return;
            }

            if (passwordTextBox.Text != confirmPasswordTextBox.Text)
            {
                MessageBox.Show("Şifreler Uyumsuz, Tekrar Dene", caption, btn, ico);
                confirmPasswordTextBox.Select();
                return;
            }
            string yourSQL = "SELECT username FROM LoginTbl WHERE username = '"+usernameTextBox.Text+"'";
            DataTable checkDuplicates = Otomasyon.Connection.ServerConnection.executeSQL(yourSQL);

            if(checkDuplicates.Rows.Count > 0)
            {
                MessageBox.Show("Böyle bir kullanıcı kayıtlı. Lütfen farklı bir kullanıcı ismi oluştur", 
                    "Tekrarlı Kullanıcı Adı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                usernameTextBox.SelectAll();
                return;
            }
            DialogResult result;
            result = MessageBox.Show("Kaydedilsin mi?", "Veri Kaydı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(result == DialogResult.Yes)
            {
                string mySQL = string.Empty;
                string crypted = HashString(passwordTextBox.Text);
                mySQL += "INSERT INTO LoginTbl(username, password, ulke, sehir)";
                mySQL += "VALUES ('"+usernameTextBox.Text+"','"+ crypted +"', '"+gorevTextBox.Text+"', '"+yetkiTextBox.Text+"' )";

                Otomasyon.Connection.ServerConnection.executeSQL(mySQL);

                MessageBox.Show("Veri Kaydı Başarılı Bir Şekilde Tamamlandı",
                                "Veri Kaydı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                loadUserData();
                clearControls();
            }
        }
    }
}
