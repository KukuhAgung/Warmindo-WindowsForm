using System;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Warmindo
{
    public partial class Form1 : Form
    {
        List<Pesanan> daftarPesanan = new List<Pesanan>();
        public string nama;
        public string status;
        public int jumlahpesanan = 0;
        public int total = 0;
        public int hargaakhir = 0;
        public int noPesanan = Properties.Settings.Default.nopesanan;
        public Form1()
        {
            InitializeComponent();
            inputUser();
            lbluser.Text = nama;
        }
        public void inputUser()
        {
            nama = Interaction.InputBox("Selamat datang di Warmindo Mas FUAD,\nMasukan nama kamu", "Masukan Nama");
            MessageBox.Show("Selamat Datang Mas " + nama);
        }
        private void refreshnota()
        {
            Nota1.Rows.Clear();
            foreach (Pesanan getPesanan in daftarPesanan)
            {
                string[] newRow = { "", "", "", "" };
                newRow[0] = getPesanan.NoPesanan.ToString();
                newRow[1] = getPesanan.Nama;
                newRow[2] = getPesanan.Total.ToString();
                newRow[3] = getPesanan.Status;
                Nota1.Rows.Add(newRow);
            }
        }
        public void hitungpesanan()
        {
            Dictionary<string, int> Items = new Dictionary<string, int>
            {
                { "IndomieSoto", 10000 },
                { "IndomieKari", 10000 },
                { "IndomieAyamBawang", 10000 },
                { "IndomieGoreng", 10000 },
                { "IndomieRendang", 10000 },
                { "MartabakIndomie", 12000 },
                { "AirMineral", 3000 },
                { "EsTehManis", 5000 },
                { "EsJeruk", 10000 },
                { "rotbak1", 13000 },
                { "rotbak2", 15000 }
            };

            foreach (var pair in Items)
            {
                string namaItem = pair.Key;
                int hargaItem = pair.Value;
                CheckBox checkBox = this.Controls.Find(namaItem, true).FirstOrDefault() as CheckBox;
                if (checkBox != null && checkBox.Checked)
                {
                    switch (checkBox.Name)
                    {
                        case "IndomieSoto":
                        case "IndomieKari":
                        case "IndomieAyamBawang":
                        case "IndomieGoreng":
                        case "IndomieRendang":
                        case "MartabakIndomie":
                        case "AirMineral":
                        case "EsTehManis":
                        case "EsJeruk":
                        case "rotbak1":
                        case "rotbak2":
                            jumlahpesanan = total + hargaItem;
                            hargaakhir += hargaItem;
                            checkBox.Checked = false;
                            status = "Pending";
                            break;
                        default:
                            MessageBox.Show("Lihat kembali pesananmu!");
                            break;
                    }
                }
            }

        }
        private void Nota1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < Nota1.Rows.Count)
            {
                DataGridViewRow selectedRow = Nota1.Rows[e.RowIndex];
                string totalPesanan = selectedRow.Cells[2].Value.ToString();
                lblharga.Text = totalPesanan;
            }
        }
        private Pesanan SelectItems()
        {
            int getID = 0;
            for (int i = 0; i < Nota1.Rows.Count; i += 1)
            {
                if (Nota1.Rows[i].Selected)
                {
                    getID = int.Parse(Nota1.Rows[i].Cells[0].Value.ToString());
                    break;
                }
            }
            Pesanan getItems = new Pesanan();
            foreach (Pesanan checkItems in daftarPesanan)
            {
                if (checkItems.NoPesanan == getID)
                    getItems = checkItems;
            }
            return getItems;
        }

        private void btnpesan_Click(object sender, EventArgs e)
        {
            hitungpesanan();
            Pesanan newPesanan = new Pesanan();
            newPesanan.Pesan(noPesanan++, nama, jumlahpesanan, status);
            daftarPesanan.Add(newPesanan);
            lbltotalpesanan.Text = hargaakhir.ToString();
            lblharga.Text = jumlahpesanan.ToString();
            refreshnota();
        }

        private void btnhapus_Click(object sender, EventArgs e)
        {
            Pesanan getPesanan = SelectItems();
            if (daftarPesanan.Contains(getPesanan))
                daftarPesanan.Remove(getPesanan);
            refreshnota();
        }

        private void btnbayar_Click(object sender, EventArgs e)
        {
            Pesanan getPesanan = SelectItems();
            int bayar = 0;
            bayar = int.Parse(txtbayar.Text);
            if (daftarPesanan.Contains(getPesanan))
                lbltotalpesanan.Text = getPesanan.Total.ToString();
            refreshnota();

            if (bayar < getPesanan.Total)
            {
                MessageBox.Show("Uangmu Kurang");
                refreshnota();
            }
            else
            {
                getPesanan.Status = "Lunas";
                MessageBox.Show("Berhasil dibayar");
                refreshnota();
            }
        }

    }
}